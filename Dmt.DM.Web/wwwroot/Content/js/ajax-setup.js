var jwtToken = localStorage.getItem("access_token");

$.ajaxSetup({
    //dataType: "json",
    //cache: false,
    headers: {
        'X-XSRF-TOKEN': getCookie('XSRF-TOKEN'), 
        'Authorization': 'Bearer ' + jwtToken
    },
    contentType: 'application/json; charset=utf-8',
    complete: function (xhr) {
        //token过期，则跳转到登录页面
        if (xhr.status === 401) {
            top.location.href = "/Login/Index";
        }
    }
});

function getCookie(name) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + name + "=");
    if (parts.length === 2) {
        var lastItem = parts.pop();
        if (lastItem) {
            var uri = lastItem.split(";").shift();
            if (uri) {
                var cookie = decodeURIComponent(uri);
                //console.log('cookie[' + name + ']', cookie);
                return cookie;
            }
        }
    }
    return "";
}