var clients = [];
$(function () {
    clients = $.clientsInit();
})
$.clientsInit = function () {
    var dataJson = {
        dataItems: [],
        organize: [],
        role: [],
        duty: [],
        user: {},
        authorizeMenu: [],
        authorizeButton: []
    };
    var init = function () {
        $.ajax({
            //headers: {
            //    'Authorization': 'Bearer ' + window.localStorage.getItem('access_token') 
            //},
            url: "/ClientsData/GetClientsDataJson",
            type: "get",
            dataType: "json",
            async: false,
            //contentType: 'application/json; charset=utf-8',
            success: function (data) {
                dataJson.dataItems = data.dataItems;
                dataJson.organize = data.organize;
                dataJson.role = data.role;
                dataJson.duty = data.duty;
                dataJson.authorizeMenu = eval(data.authorizeMenu);
                dataJson.authorizeButton = data.authorizeButton;
                dataJson.user = data.user;
            },
            error: function () {
                window.location.href = "/Login/Index";
            },
            beforeSend: function () {
            },
            complete: function () {
            }
        });
    }
    init();
    return dataJson;
}