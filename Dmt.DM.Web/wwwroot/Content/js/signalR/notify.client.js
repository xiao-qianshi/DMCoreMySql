"use strict";

if (!String.prototype.supplant) {
    String.prototype.supplant = function (o) {
        return this.replace(/{([^{}]*)}/g,
            function (a, b) {
                var r = o[b];
                return typeof r === 'string' || typeof r === 'number' ? r : a;
            }
        );
    };
}

$(function () {
    var hubConnection = top.hubConnection = new signalR.HubConnectionBuilder().withUrl("/chathub").build(),//$.connection.dialysisHubMini, //声明集线器代理
        tasksTable = $('#tasksModel').find('table').find('tbody'),
        bellTable = $('#bellModel').find('table').find('tbody'),
        tasksCount = $('#tasksCount'),
        bellCount = $("#bellCount");
    function init() {
        //console.log('已连接', top.clients.user.RoleId);
        hubConnection.invoke("joinGroup", top.clients.user.RoleId);
        //hubConnection.invoke("addNotify", { Title: "v啊手动阀手动阀", Content: "速度发送方大是" });
    }
    var myToast = swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: true,
        icon: 'info',
        timer: 8000
    });

    hubConnection.on("addTasksNotify", function (msg) {
        var json = JSON.parse(msg);
        myToast.fire({
            title: json.Title,
            html: json.Content
        });
        tasksTable.prepend('\
            <tr>\
                <td>'+ json.Time.substr(5, 11) + '</td>\
                <td>\
                    <strong>\
                        '+ json.Title + '\
                    </strong>\
                    &nbsp;&nbsp;'+ json.Content + '\
                </td>\
                <td>\
                    <button type="button" class="close">&times;</button>\
                </td>\
            </tr>\
            ');
        $(tasksTable[0].children[0]).find('button').on('click', function (e) {
            $(e.currentTarget).parent().parent().remove();
            tasksCount.html(tasksTable[0].children.length);
        });
        var length = tasksTable[0].children.length;
        if (length > 10) {
            $(tasksTable[0].children[length - 1]).remove();
        } else {
            tasksCount.html(length);
        }
    });

    hubConnection.on("send",
        function(msg) {
            console.log(msg);
        });

    hubConnection.on("addNotify",
        function (msg) {
            console.log(msg);
            var json = msg;
            myToast.fire({
                title: json.title,
                html: json.content,
                icon: 'warning'
            });
        });

    hubConnection.on("addBellNotify", function (msg) {
        var json = JSON.parse(msg);
        myToast.fire({
            title: json.Title,
            html: json.Content,
            icon: 'warning'
        });
        bellTable.prepend('\
            <tr>\
                <td>'+ json.Time.substr(5, 11) + '</td>\
                <td>\
                    <strong>\
                        '+ json.Title + '\
                    </strong>\
                    &nbsp;&nbsp;'+ json.Content + '\
                </td>\
                <td>\
                    <button type="button" class="close">&times;</button>\
                </td>\
            </tr>\
            ');
        $(bellTable[0].children[0]).find('button').on('click', function (e) {
            $(e.currentTarget).parent().parent().remove();
            bellCount.html(bellTable[0].children.length);
        });
        var length = bellTable[0].children.length;
        if (length > 10) {
            $(bellTable[0].children[length - 1]).remove();
        } else {
            bellCount.html(length);
        }
    });
    //创建连接
    //$.connection.hub.logging = true;//启用日志
    //$.connection.start().done(init);
    hubConnection.start().then(function () {
        //console.log("signal start.....");
        init();
    }).catch(function (err) {
        return console.error(err.toString());
    });
});