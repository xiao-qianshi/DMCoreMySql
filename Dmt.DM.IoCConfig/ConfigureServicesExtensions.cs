using Dmt.DM.Application;
using Dmt.DM.EntityFrameWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmt.DM.ExternalInterface.Lis;
using Dmt.DM.IoCConfig.MvcFilters;

namespace Dmt.DM.IoCConfig
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection AddCustomAntiforgery(this IServiceCollection services)
        {
            services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");
            services.AddMvc(options =>
            {
                //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                options.Filters.Add(new CustomActionFilterAttribute());
            });
            return services;
        }

        public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins(configuration["App:CorsOrigins"]
                            .Split(',')
                            .Select(item => item.TrimEnd('/')).ToArray())
                        //.AllowAnyOrigin()
                        //.WithOrigins("*")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed((host) => true)
                        //.SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowCredentials()
                        )
                    ;
            });
            return services;
        }

        public static IServiceCollection AddCustomJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            // Only needed for custom roles.
            services.AddAuthorization(options =>
            {
                options.AddPolicy(CustomRoles.Admin, policy => policy.RequireRole(CustomRoles.Admin));
                options.AddPolicy(CustomRoles.User, policy => policy.RequireRole(CustomRoles.User));
                options.AddPolicy(CustomRoles.Editor, policy => policy.RequireRole(CustomRoles.Editor));
            });

            // Needed for jwt auth.
            services
                .AddAuthentication(options =>
                {
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration["BearerTokens:Issuer"], // site that makes the token
                        ValidateIssuer = false, // TODO: change this to avoid forwarding attacks
                        ValidAudience = configuration["BearerTokens:Audience"], // site that consumes the token
                        ValidateAudience = false, // TODO: change this to avoid forwarding attacks
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["BearerTokens:Key"])),
                        ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                        ValidateLifetime = true, // validate the expiration
                        ClockSkew = TimeSpan.Zero // tolerance for the expiration date
                    };
                    cfg.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                            logger.LogError("Authentication failed.", context.Exception);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidatorService>();
                            return tokenValidatorService.ValidateAsync(context);
                        },
                        OnMessageReceived = context =>
                        {
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                            logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);
                            return Task.CompletedTask;
                        }
                    };
                });
            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HsDbContext>(options =>
            {
                options.UseMySql(
                    configuration.GetConnectionString("Default")
                               /* .Replace("|DataDirectory|", Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "app_data"))*/,
                    serverDbContextOptionsBuilder =>
                    {
                        var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                        serverDbContextOptionsBuilder.CommandTimeout(minutes);
                        serverDbContextOptionsBuilder.EnableRetryOnFailure();
                        //serverDbContextOptionsBuilder.UseRowNumberForPaging();
                    });
            });
            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            //HttpContext访问器
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAntiForgeryCookieService, AntiForgeryCookieService>();
            //services.AddScoped<IUnitOfWork, HsDbContext>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddSingleton<ISecurityService, SecurityService>();
            services.AddScoped<IDbInitializerService, DbInitializerService>();
            services.AddScoped<ITokenStoreService, TokenStoreService>();
            services.AddScoped<ITokenValidatorService, TokenValidatorService>();
            services.AddScoped<ITokenFactoryService, TokenFactoryService>();
            services.AddScoped<IDaLabService, DaLabService>();
            return services;
        }

        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<BearerTokensOptions>()
                .Configure(options =>
                {
                    var section = configuration.GetSection("BearerTokens");
                    options.Key = section.GetValue<string>("Key");
                    options.Issuer = section.GetValue<string>("Issuer");
                    options.AccessTokenExpirationMinutes = section.GetValue<int>("AccessTokenExpirationMinutes");
                    options.AllowMultipleLoginsFromTheSameUser = section.GetValue<bool>("AllowMultipleLoginsFromTheSameUser");
                    options.AllowSignoutAllUserActiveClients = section.GetValue<bool>("AllowSignoutAllUserActiveClients");
                    options.RefreshTokenExpirationMinutes = section.GetValue<int>("RefreshTokenExpirationMinutes");
                })
                .Validate(token =>
                {
                    return token.AccessTokenExpirationMinutes < token.RefreshTokenExpirationMinutes;
                });

            services.AddOptions<ApiSettings>()
                .Configure(options =>
                {
                    var section = configuration.GetSection("ApiSettings");
                    options.AccessTokenObjectKey = section.GetValue<string>("AccessTokenObjectKey");
                    options.AdminRoleName = section.GetValue<string>("AdminRoleName");
                    options.LogoutPath = section.GetValue<string>("LogoutPath");
                    options.LoginPath = section.GetValue<string>("LoginPath");
                    options.RefreshTokenObjectKey = section.GetValue<string>("RefreshTokenObjectKey");
                    options.RefreshTokenPath = section.GetValue<string>("RefreshTokenPath");
                });

            services.AddOptions<DASettings>()
                .Configure(options =>
                {
                    var section = configuration.GetSection("DALab");
                    options.UserId = section.GetValue<string>("DAUser");
                    options.Password = section.GetValue<string>("DAPassword");
                });

            return services;
        }

    }
}
