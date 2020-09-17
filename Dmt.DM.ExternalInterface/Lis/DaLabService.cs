using DALisService;
using System;
using System.Threading.Tasks;

namespace Dmt.DM.ExternalInterface.Lis
{
    public interface IDaLabService
    {
        Task<string> GetReport(string userId, string password, DateTime? startDate = null, DateTime? endDate = null);
    }

    public class DaLabService : IDaLabService
    {
        private readonly RasClientDetailSoap _client;

        public DaLabService()
        {
            _client = new RasClientDetailSoapClient(RasClientDetailSoapClient.EndpointConfiguration
                .RasClientDetailSoap);
        }

        public async Task<string> GetReport(string userId, string password, DateTime? startDate = null, DateTime? endDate = null)
        {
            var result = "";
            var _startDate = startDate?.Date ?? DateTime.Today;
            var _endDate = (endDate?.Date ?? DateTime.Today).AddDays(1);
            try
            {
                var response = await _client.GetDetailData5Async(new GetDetailData5Request
                {
                    Body = new GetDetailData5RequestBody
                    {
                        ClientID = userId,
                        ClientGUID = password,
                        StartDate = _startDate.ToString("yyyy-MM-dd"),
                        EndDate = _endDate.ToString("yyyy-MM-dd")
                    }
                });
                result = response.Body.GetDetailData5Result;
            }
            catch
            {
                // ignored
            }

            return result;
        }
    }
}


