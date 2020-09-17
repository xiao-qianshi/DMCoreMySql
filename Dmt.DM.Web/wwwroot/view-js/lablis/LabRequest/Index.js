var selectedPatients = [];
    var selectedItems = [];
    $(function () {
        $("#requestDate").val(new Date().Format('yyyy-MM-dd'));
        //查询开单数据
        initControl();
        $("#btn-query").trigger('click');
    })
    function initControl() {
        $("#status_horizon a").on('click', function (e) {
            $("#status_horizon a.active").removeClass("active");
            $(this).addClass("active");
            var requestStatus = $(this).attr('data-value');
            queryRequest($("#requestDate").val(), requestStatus);
        });
        $("#btn-eye").on('click', function () {
            $('#order-panel').toggle('normal', function () {
                if ($(this).css('display') === 'none') {
                    $('#btn-eye').find('i').removeClass('fa-eye-slash').addClass('fa-eye');
                } else {
                    $('#btn-eye').find('i').removeClass('fa-eye').addClass('fa-eye-slash');
                }
            });
        });
        $("#btn-choosepatient").on('click', function () {
            $.modalOpen({
                id: "ChoosePatient",
                title: "选择患者",
                url: "/LabLis/LabRequest/ChoosePatient",
                width: "800px",
                height: "600px",
                callBack: function (iframeId) {
                    top.frames[iframeId].submitForm();
                }
            });
        });
        $("#btn-choosepatientbyvisit").on('click', function () {
            $.modalOpen({
                id: "ChoosePatientByVisit",
                title: "选择患者",
                url: "/LabLis/LabRequest/ChoosePatientByVisit?keyValue=" + $("#requestDate").val(),
                width: "800px",
                height: "600px",
                callBack: function (iframeId) {
                    top.frames[iframeId].submitForm();
                }
            });
        });
        $("#btn-clearpatient").on('click', function () {
            clearpatients();
        });
        $("#btn-chooseitem").on('click', function () {
            $.modalOpen({
                id: "Form",
                title: "选择项目",
                url: "/LabLis/LabRequest/ChooseLabItem",
                width: "800px",
                height: "600px",
                callBack: function (iframeId) {
                    top.frames[iframeId].submitForm();
                }
            });
        });
        $("#btn-clearitem").on('click', function () {
            clearitems();
        });
        $("#btn-query").on('click', function () {
            var requestStatus = $("#status_horizon a.active").attr('data-value');
            queryRequest($("#requestDate").val(), requestStatus)
        });
        $("#btn-save").on('click', function () {
            var pids = [];
            var itemids = [];
            $.each(selectedPatients, function (i, v) {
                pids.push({ id: v.id })
            });
            $.each(selectedItems, function (i, v) {
                itemids.push({ id: v.id })
            });
            if (pids.length == 0 || itemids.length == 0) {
                return false;
            }
            $.submitForm({
                url: "/LabLis/LabRequest/BatchCreateSheet",
                param: {
                    pids: JSON.stringify(pids),
                    itemids: JSON.stringify(itemids)
                },
                success: function () {
                    var requestStatus = $("#status_horizon a.active").attr('data-value');
                    queryRequest($("#requestDate").val(), requestStatus)
                }
            });
        });

        $("#btn-batchsign").on('click', function () {
            $.modalConfirm("注：您确定要提交全部申请吗？", function (r) {
                if (r) {
                    var requestDate = $("#requestDate").val();
                    if (requestDate == '') {
                        requestDate = new Date().Format('yyyy-MM-dd');
                    }
                    $.submitForm({
                        url: "/LabLis/LabRequest/BatchSign",
                        param: { requestDate: requestDate },
                        success: function () {
                            var requestStatus = $("#status_horizon a.active").attr('data-value');
                            queryRequest(requestDate, requestStatus);
                        }
                    });
                }
            });
        });
    }

    //查询开单记录
    function queryRequest(requestDate, requestStatus) {
        if (requestDate == '') {
            requestDate = new Date().Format('yyyy-MM-dd');
        }
        $.ajax({
            url: "/LabLis/LabRequest/GetlistJson",
            data: {
                requestDate: requestDate,
                requestStatus: requestStatus
            },
            dataType: "json",
            async: false,
            success: function (data) {
                //console.log(data);
                fillPanel(data);
            }
        });
    }
    function fillPanel(sheets) {
        //清空
        $("#sheetsBody").empty();
        $("#sheetsBody").html('');
        $.each(sheets, function (i, v) {
            var items = [];
            $.each(v.rows, function (index, value) {
                items.push(value.shortName);
            });
            var canDelete = v.status == 1 || v.status == 2;
            var canSign = v.status == 1;
            $("#sheetsBody").append('\
                    <div class="col-xs-6 col-sm-5 col-md-4 col-lg-3" id="'+ v.id + '">\
                        <div class="panel panel-default" style="width: 95%;">\
                            <div class="panel-heading">\
                                <strong>'+ v.name + '</strong>\
                                <span class="badge">'+ v.rows.length + '</span>\
                                <strong>'+ (v.barcode == null ? '' : v.barcode) + '</strong>\
                                '+ (canDelete ? '<i class="fa fa-trash fa-lg" style="float: right;margin-right: 5px;"></i>' : '') + '\
                                '+ (canSign ? '<i class="fa fa-pencil fa-lg" style="float: right;margin-right: 15px;"></i>' : '') + '\
                            </div>\
                            <div class="panel-body">\
                                <p>\
                                    '+ items.join(',') + '\
                                </p>\
                            </div>\
                            <div class="panel-footer">\
                                <strong>时间:'+ v.orderTime.substr(5, 11) + '</strong>\
                                <strong style="float: right;">'+ v.doctorName + '</strong>\
                            </div>\
                        </div>\
                    </div>\
                    ');
        });
        $("#sheetsBody i.fa").on('click', function (e) {
            var $target = $(e.currentTarget).parent().parent().parent();
            //console.log($target[0].id, $target, this);
            var currentId = $target[0].id;
            if ($(this).is('.fa-trash')) {
                $.modalConfirm("注：您确定要【删除】该项吗？", function (r) {
                    if (r) {
                        $.submitForm({
                            url: "/LabLis/LabRequest/DeleteForm",
                            param: { keyValue: currentId },
                            success: function () {
                                $target.remove();
                            }
                        });
                    }
                });
            } else if ($(this).is('.fa-pencil')) {
                $.modalConfirm("注：您确定要【提交】该项吗？", function (r) {
                    if (r) {
                        $.submitForm({
                            url: "/LabLis/LabRequest/SignForm",
                            param: { keyValue: currentId },
                            success: function () {
                                $target.remove();
                            }
                        });
                    }
                });
            }
        });
    }
    function setpatients(appendData) {
        var target = $("#selectedPatients");
        $.each(appendData, function (i, v) {
            var findrows = $.grep(selectedPatients, function (item) {
                return item.id == v.id
            });
            if (findrows.length === 0) {
                selectedPatients.push(v);
                target.append('\
                        <div class="col-xs-3 col-sm2 col-md-1 col-lg-1">\
                            <strong>'+ v.name + (v.beInfected ? '<i class="fa fa-circle fa-sm" style="color: orangered;"></i>' : '') + '</strong>&nbsp;<i class="fa fa-times fa-lg" id="' + v.id + '"></i>\
                        </div>\
                        ');
                $("#" + v.id).on('click', function (e) {
                    $(e.currentTarget).parent().remove();
                    selectedPatients = $.grep(selectedPatients, function (v, i) {
                        return v.id != e.currentTarget.id;
                    });
                });
            }
        });
    }
    function setitems(appendData) {
        var target = $("#selectedItems");
        $.each(appendData, function (i, v) {
            var findrows = $.grep(selectedItems, function (item) {
                return item.id == v.id
            });
            if (findrows.length === 0) {
                selectedItems.push(v);
                target.append('\
                         <div class="col-xs-5 col-sm4 col-md-3 col-lg-2">\
                        <i>'+ v.name + '</i>&nbsp;&nbsp;' + (v.isExternal ? '<span class="label label-danger">外</span>' : '') + '&nbsp;&nbsp;<i class="fa fa-times fa-lg" id="' + v.id + '"></i>\
                        </div>\
                        ');
                $("#" + v.id).on('click', function (e) {
                    $(e.currentTarget).parent().remove();
                    selectedItems = $.grep(selectedItems, function (v, i) {
                        return v.id != e.currentTarget.id;
                    });
                });
            }
        });
    }
    function clearpatients() {
        var target = $("#selectedPatients");
        target.empty();
        target.html('');
        selectedPatients = [];
    }
    function clearitems() {
        var target = $("#selectedItems");
        target.empty();
        target.html('');
        selectedItems = [];
    }
    Date.prototype.Format = function (fmt) { //author: meizz
        var o = {
            "M+": this.getMonth() + 1, //月份
            "d+": this.getDate(), //日
            "H+": this.getHours(), //小时
            "m+": this.getMinutes(), //分
            "s+": this.getSeconds(), //秒
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度
            "S": this.getMilliseconds() //毫秒
        };
        if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        return fmt;
    }