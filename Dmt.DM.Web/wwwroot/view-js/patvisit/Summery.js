//当前日期所有透析记录
var list = [];
//透析记录单Id
var selectid = "";
var currentPid = "";

$(function () {
    initControl();
    $("#btn-refresh").trigger('click');
});
function initControl() {
    $('[data-toggle="tooltip"]').tooltip({
        html: true
    }).on('show.bs.tooltip', function () {
        var msg = '...';
        if (!!selectid) {
            $.ajax({
                url: "/PatientManage/PatVisit/GetWeightLogs",
                data: { keyValue: selectid },
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.length > 0) {
                        msg = '';
                        $.each(data, function (index, item) {
                            msg = msg + '<em>' + item.time + '</em>&nbsp; &nbsp; &nbsp; <b>' + item.value + '</b><br>';
                        });
                    }
                }
            });
        }
        $('[data-toggle="tooltip"]').attr('data-original-title', msg);
    })
    $("#date-list").bindSelect();
    //$("#data-list").on('change', function (e) {
    //    console.log(e);
    //    selectid = e.id;
    //    setForm();
    //})
    $("#visitNo").bindSelect();
    $("#patient-list").on("click", function (e) {
        selectid = e.target.id;
        if (selectid == "") {
            selectid = e.target.parentNode.id;
        }
        if (selectid === undefined || selectid === null || selectid === '' || selectid === 'patient-list') {
            selectid = '';
            currentPid = '';
            pgdId = '';
            return false;
        }
        //console.log(selectid); //点击li的id
        $(".list-group-item.active").removeClass('active');
        $("#" + selectid).addClass('active');
        //currentPid = list.filter(t => t.Id == selectid)[0].Pid;
        //currentPid = list.filter(t => t.Id == selectid)[0].Pid;
        $.each(list, function (i, t) {
            if (t.Id == selectid) {
                currentPid = t.Pid;
                return false;
            }
        });
        queryHistory();
        $("#date-list").val(selectid).trigger('change');
        setForm();
    });
    $("#visitDate").val(new Date().Format('yyyy-MM-dd'));

    $("#groupName").bindSelect({
        url: "/SystemManage/ItemsData/GetSelectJson?enCode=BedGroup",
        id: "id",
        text: "text",
        change: function (e) {
            refreshList();
        }
    });

    $("#leftContent").css({ "height": $(window).height() - 115 });
    $("#mainContent").css({ "height": $(window).height() - 80 });

    //刷新数据 窗口重绘
    $("#btn-refresh").on("click", function () {
        //$("#patient-list").css({ "height": $(window).height() - 180 });
        //$("tab-content").css({ "height": $(window).height() - 125 });
        handleVisitDateChange();
    });

    $("#gridListOrders").dataGrid({
        //url: "/PatientManage/Orders/GetGridJson",
        datatype: 'local',
        height: 180,
        colModel: [
            { label: '主键', name: 'F_Id', hidden: true },
            { label: '代码', name: 'F_OrderCode', hidden: true },
            { label: '名称', name: 'F_OrderText', width: 140, align: 'left' },
            { label: '规格', name: 'F_OrderSpec', width: 60, align: 'center' },
            { label: '剂量', name: 'F_OrderAmount', width: 50, align: 'center' },
            { label: '频次', name: 'F_OrderFrequency', width: 50, align: 'center' },
            { label: '途径', name: 'F_OrderAdministration', width: 60, align: 'center' },
            { label: '开单医生', name: 'F_Doctor', width: 60, align: 'center' },
            { label: '执行护士', name: 'F_Nurse', width: 60, align: 'center' },
            {
                label: '执行时间', name: 'F_NurseOperatorTime', width: 60, align: 'left',
                formatter: "time", formatoptions: { srcformat: 'Y-m-d h:m:s', newformat: 'Y-m-d h:m' }
            }
        ],
        sortname: 'F_NurseOperatorTime asc',
        viewrecords: true
    });

    //$("#gridListOrders").jqGrid("clearGridData");
    //$("#gridListOrders").setGridParam({ data: data }).trigger('reloadGrid');

    $("#gridListObs").dataGrid({
        datatype: 'local',
        height: 180,
        //caption: '单位：机温℃，血压、动脉压、静脉压、TMPmmHg，电导率ms/cm，血流速ml/min，超滤率ml/h，超滤量、剩余肝素量ml',
        colModel: [
            { label: "主键", name: "F_Id", hidden: true, key: true },
            {
                label: '时间', name: 'F_NurseOperatorTime', width: 40, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'H:i' }
            },
            {
                label: '机温', name: 'F_T', width: 35, align: 'center'
            },
            {
                label: '收缩压', name: 'F_SSY', width: 60, align: 'center', hidden: true
            },
            {
                label: '舒张压', name: 'F_SZY', width: 60, align: 'center', hidden: true
            },
            {
                label: '血压', name: 'pressure', width: 100, align: 'center',
                formatter: function (cellvalue, options, rowObject) {
                    return rowObject.F_SSY + ' / ' + rowObject.F_SZY;
                }
            },
            {
                label: '脉搏', name: 'F_HR', width: 50, align: 'center'
            },
            {
                label: '血流速', name: 'F_BF', width: 60, align: 'center'
            },
            {
                label: '动脉压', name: 'F_A', width: 60, align: 'center'
            },
            {
                label: '静脉压', name: 'F_V', width: 60, align: 'center'
            },
            {
                label: '电导率', name: 'F_C', width: 60, align: 'center'
            },
            {
                label: 'TMP', name: 'F_TMP', width: 60, align: 'center'
            },
            {
                label: '超滤率', name: 'F_UFV', width: 50, align: 'center'
            },
            {
                label: '剩余肝素量', name: 'F_GSL', width: 50, align: 'center'
            },
            {
                label: '超滤量', name: 'F_UFR', width: 50, align: 'center'
            },
            { label: '记录者', name: 'F_NurseName', width: 50, align: 'center' },
            { label: '症状及处理', name: 'F_MEMO', width: 200, align: "left", sortable: false }
        ],
        sortname: 'F_NurseOperatorTime desc',
        viewrecords: true
    });

    $.when(ajax1, ajax2, ajax3, ajax4, ajax5);
    var ajax1 = $.ajax({
        url: "/PatientManage/Patient/GetSelectJson",
        dataType: "json",
        async: false,
        success: function (data) {
            var tags = [];
            var first = {};
            first.id = "";
            first.text = "";
            tags.push(first);
            $.each(data, function (index, item) {
                var obj = {};
                obj.id = item.id;
                obj.text = item.name + "(No." + item.recordNo + ")" + item.py;
                tags.push(obj);
            }
            );
            $("#F_Pid").bindSelect({
                search: true,
                data: tags,
                change: function (e) {
                    if (!!e.id) {
                        $.ajax({
                            url: "/PatientManage/Patient/GetFormJson",
                            data: { keyValue: e.id },
                            dataType: "json",
                            async: false,
                            success: function (data) {
                                $("#F_DialysisNo").val(data.F_DialysisNo);
                                $("#F_RecordNo").val(data.F_RecordNo);
                                $("#F_Name").val(data.F_Name);
                                $("#F_Gender").val(data.F_Gender);
                                $("#F_BirthDay").val(data.F_BirthDay);
                                $("#F_InpNo").val(data.F_InpNo);
                                $("#F_BedNo").val(data.F_BedNo);
                                $("#F_DialysisNo ,#F_Name,#F_Gender").attr('readonly', 'readonly');
                            }
                        });
                    }
                }
            });
        }
    });
    $("#F_GroupName").bindSelect({
        url: "/SystemManage/ItemsData/GetSelectJson?enCode=BedGroup",
        id: "id",
        text: "text",
        change: function (e) {
            $.ajax({
                url: "/PatientManage/DialysisMachine/GetSelectJson",
                data: { enCode: e.id },
                dataType: "json",
                async: false,
                success: function (data) {
                    $("#F_DialysisBedNo").html("");
                    $("#F_DialysisBedNo").bindSelect({
                        data: data
                    });
                }
            });
        }
    });

    $("#F_VisitNo").bindSelect();
    $("#F_PatientSourse").bindSelect();
    $("#F_DialysisType").bindSelect({
        url: "/SystemManage/ItemsData/GetSelectJson?enCode=DialysisType",
        id: "id",
        text: "text",
        change: function (e) {
            if (e.id == 'HDF') {
                $("#hdhp").show();
            }
            else {
                $("#hdhp").hide();
            }
        }
    });
    var ajax2 = $.ajax({
        url: "/PatientManage/Material/GetSelectJson",
        dataType: "json",
        async: false,
        success: function (data) {
            var tags = [];
            var first = {};
            first.id = "";
            first.text = "";
            tags.push(first);
            $.each(data, function (index, item) {
                var obj = {};
                obj.id = item.id;
                obj.text = item.text;
                tags.push(obj);
            }
            );

            $("#F_DialyzerType1").bindSelect({
                data: tags
            });
        }
    });
    var ajax3 = $.ajax({
        url: "/PatientManage/Material/GetSelectByTypeJson",
        data: { keyword: '灌流器' },
        dataType: "json",
        async: false,
        success: function (data) {
            var tags = [];
            var first = {};
            first.id = "";
            first.text = "";
            tags.push(first);
            $.each(data, function (index, item) {
                var obj = {};
                obj.id = item.id;
                obj.text = item.text;
                tags.push(obj);
            }
            );
            $("#F_DialyzerType2").bindSelect({
                data: tags
            });
        }
    });
    $("#F_VascularAccess").bindSelect();
    var ajax4 = $.ajax({
        url: "/PatientManage/Drugs/GetSelectJson",
        dataType: "json",
        async: false,
        success: function (data) {
            var tags = [];
            var first = {};
            first.id = "";
            first.text = "无肝素";
            tags.push(first);
            $.each(data, function (index, item) {
                var obj = {};
                obj.id = item.id;
                obj.text = item.text;
                tags.push(obj);
            }
            );
            $("#F_HeparinType").bindSelect({
                id: 'id',
                text: 'text',
                data: tags,
                change: function (e) {
                    var drugid = e.id;
                    if (!!drugid) {
                        $.ajax({
                            url: "/PatientManage/Drugs/GetFormJson",
                            dataType: "json",
                            data: { keyValue: drugid },
                            async: false,
                            success: function (data) {
                                $("#HeparinUnit").html(data.F_DrugMiniSpec);
                                $("#HeparinAddSpeedUnit").html(data.F_DrugMiniSpec + "/h");
                                $("#F_HeparinUnit").val(data.F_DrugMiniSpec);
                                $("#F_HeparinAddSpeedUnit").val(data.F_DrugMiniSpec + "/h");

                            }
                        });
                    }
                    else {
                        $("#HeparinUnit").html("");
                        $("#HeparinAddSpeedUnit").html("");
                        $("#F_HeparinUnit").val("");
                        $("#F_HeparinAddSpeedUnit").val("");
                        $("#F_HeparinUnit").val("");
                        $("#F_HeparinAddSpeedUnit").val("");
                        $("#F_HeparinAmount").val("");
                        $("#F_HeparinAddAmount").val("");
                    }
                }

            });

        }
    });
    $("#F_AccessName").bindSelect();
    $("#F_DilutionType").bindSelect();
    $("#F_Gender").bindSelect();
    var ajax5 = $.ajax({
        url: "/SystemManage/User/GetUserSelectJson",
        data: { keyValue: null },
        dataType: "json",
        async: false,
        success: function (data) {
            var tags = [];
            var first = {};
            first.id = "";
            first.text = "     ";
            tags.push(first);
            $.each(data, function (index, item) {
                var obj = {};
                obj.id = item.id;
                obj.text = item.text;
                tags.push(obj);
            }
            );
            $("#F_RecordDoctor").bindSelect({
                id: "id",
                text: "text",
                data: tags,
                search: true
            });

            $("#F_PuncturePerson").bindSelect({
                id: "id",
                text: "text",
                data: tags,
                search: true
            });
            $("#F_StartPerson").bindSelect({
                id: "id",
                text: "text",
                data: tags,
                search: true
            });

            $("#F_CheckPerson").bindSelect({
                id: "id",
                text: "text",
                data: tags,
                search: true
            });

            $("#F_EndPerson").bindSelect({
                id: "id",
                text: "text",
                data: tags,
                search: true
            });
        }
    });
    $("#F_GroupName").trigger('change');
    $("#F_DialysisType").trigger('change');
    $("input[name='F_PGwzsz']").on('change', function (e) {
        $("#F_PGwzsz").val($("input[name='F_PGwzsz']:checked").val());
    });
    $("input[name='F_PGxftz1']").on('change', function (e) {
        $("#F_PGxftz1").val($("input[name='F_PGxftz1']:checked").val());
    });
    $("input[name='F_PGxftz3']").on('change', function (e) {
        $("#F_PGxftz3").val($("input[name='F_PGxftz3']:checked").val());
    });
    $("input[name='F_PGwzcx1']").on('change', function (e) {
        $("#F_PGwzcx1").val($("input[name='F_PGwzcx1']:checked").val());
    });
    $("input[name='F_DJMH']").on('change', function (e) {
        $("#F_DJMH").val($("input[name='F_DJMH']:checked").val());
    });
    $("input[name='F_DialyzerStatus']").on('change', function (e) {
        $("#F_DialyzerStatus").val($("input[name='F_DialyzerStatus']:checked").val());
    });
    $("input[name='F_FistulaStatus']").on('change', function (e) {
        var text = $("input[name='F_FistulaStatus']:checked").val();
        if (text == "其他") {
            $("#F_FistulaStatus").show();
        }
        else {
            $("#F_FistulaStatus").hide();
        }
        $("#F_FistulaStatus").val(text);
    });
    $("input[name='F_DuctOpeningStatus']").on('change', function (e) {
        var text = $("input[name='F_DuctOpeningStatus']:checked").val();
        if (text == "其他") {
            $("#F_DuctOpeningStatus").show();
        }
        else {
            $("#F_DuctOpeningStatus").hide();
        }
        $("#F_DuctOpeningStatus").val(text);
    });
    $("#btn-order").on('click', function () {
        if (!!selectid) {
            var keyValue = currentPid + "^" + $("#F_VisitDate").val().replace(' 00:00:00', '') + "^" + $("#F_DialysisNo").val();
            var id = currentPid;
            if (!!id) {
                $.modalOpen({
                    id: "ObDetails",
                    title: "执行医嘱",
                    url: "/PatientManage/PatVisit/ObDetails?keyValue=" + keyValue,
                    width: "950px",
                    height: "600px",
                    callBack: function (iframeId) {
                        top.frames[iframeId].submitData();
                        //$.loading(false);
                        //reQueryData();
                    }
                });
            }
        }
    });

    $("#btn-query").on('click', function () {
        var chooseId = $("#date-list").val();
        if (!!chooseId) {
            selectid = chooseId;
            setForm();
        }
    });
    //保存记录单
    $("#btn-save").on('click', function () {
        if (!!selectid) {
            $.submitForm({
                url: "/PatientManage/PatVisit/SubmitForm",
                param: {
                    entity: $("#mainContent").formSerialize(),
                    keyValue: selectid
                },
                success: function() {
                }
            });
        }
    });
    //打印记录单
    $("#btn-print").on('click', function () {
        if (!!selectid) {
            //$.post("/PatientManage/PatVisit/GetReportImage?keyValue=" + selectid,
            //    {
            //        postdata: selectid
            //    },
            //    function(result) {
            //        $("#print").html(result);
            //        $("#print").attr("style", "display:block;"); //显示div
            //        $("#print").jqprint();
            //        $("#print").attr("style", "display:none;"); //隐藏div
            //    });
            $.ajax({
                type: 'post',
                url: '/PatientManage/PatVisit/GetReportImage',
                data: JSON.stringify({ keyValue: selectid }),
                success: function(result) {
                    $("#print").html(result);
                    $("#print").attr("style", "display:block;"); //显示div
                    $("#print").jqprint();
                    $("#print").attr("style", "display:none;"); //隐藏div
                }
            });
        }
    });

    //关键字过滤 
    $("#filterkeyword").keydown(function (e) {
        if (e.keyCode === 13) {
            console.log(this, $("#filterkeyword").val());
            var filterText = $("#filterkeyword").val();
            if (!!filterText) {
                if (list == undefined || list.length == undefined || list.length === 0) return false;
                var visitno = parseInt($("#visitNo").val());
                //var statusinit = $("#statusinit")[0].checked;
                //var statusuncomplete = $("#statusuncomplete")[0].checked;
                //var statuscomplete = $("#statuscomplete")[0].checked;
                //var filterlist = list.filter(t => visitno == 0 || t.VisitNo == visitno);
                var filterlist = $.grep(list, function (t, i) {
                    return visitno == 0 || t.VisitNo == visitno;
                });

                if (filterlist.length === 0) return false;
                //if (statusinit && statusuncomplete && statuscomplete) {
                //    filterlist = filterlist;
                //} else if (statusinit && statusuncomplete) {
                //    filterlist = filterlist.filter(t => t.EndTime == "");
                //} else if (statusinit && statuscomplete) {
                //    filterlist = filterlist.filter(t => t.StartTime == "" || t.EndTime != "");
                //} else if (statusuncomplete && statuscomplete) {
                //    filterlist = filterlist.filter(t => t.StartTime != "");
                //} else if (statusinit) {
                //    filterlist = filterlist.filter(t => t.StartTime == "");
                //} else if (statusuncomplete) {
                //    filterlist = filterlist.filter(t => t.StartTime != "" && t.EndTime == "");
                //} else if (statuscomplete) {
                //    filterlist = filterlist.filter(t => t.EndTime != "");
                //} else {
                //    filterlist = [];
                //}
                //if (filterlist.length === 0) return false;
                //分组过滤
                var groupName = $("#groupName").val();
                if (!!groupName) {
                    //filterlist = filterlist.filter(t => t.GroupName === groupName);
                    filterlist = $.grep(filterlist, function (t, i) {
                        return t.GroupName === groupName;
                    });
                }
                filterlist.sort(function (a, b) {
                    return parseInt(a.DialysisBedNo) - parseInt(b.DialysisBedNo);
                }
                );
                if (filterlist.length === 0) return false;
                //filterlist = filterlist.filter(t => t.Py.indexOf(filterText) >= 0 || t.DialysisBedNo === filterText || t.Name.indexOf(filterText) >= 0 || t.DialysisNo == filterText);

                filterlist = $.grep(filterlist, function (t, i) {
                    return t.Py.indexOf(filterText) >= 0 || t.DialysisBedNo === filterText || t.Name.indexOf(filterText) >= 0 || t.DialysisNo == filterText;
                });

                if (filterlist.length > 0) {
                    var ul = $("#patient-list");
                    ul.empty();
                    filterlist.forEach(function (item) {
                        ul.append("<li class=\"list-group-item\" id=\"" + item.Id + "\">"
                            + "<span class=\"badge\">" + item.GroupName + item.DialysisBedNo + "</span>"
                            + "<h5 class=\"list-group-item-heading\">" + item.Name + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + item.Gender + "&nbsp;&nbsp;" + item.AgeDesc + "</h5>"
                            + "<p class=\"list-group-item-text\">" + item.DialysisType + "&nbsp;&nbsp;&nbsp;" + item.VascularAccess + "&nbsp;&nbsp;&nbsp;" + item.HeparinType + "&nbsp;&nbsp;&nbsp;</p>"
                            + "</li>"
                        );
                    });
                    selectid = '';
                    currentPid = '';
                }
            } else {
                refreshList();
            }
        } else if (e.keyCode === 8) {
            var filterText2 = $("#filterkeyword").val();
            if (filterText2.length === 1) {
                refreshList();
            }
        }
    });
}
function handleVisitDateChange(e) {
    if (e !== undefined) {
        //console.log(e, e.el.id);
        if (e.el.id !== "visitDate") return false;
    }
    var visitDate = $("#visitDate").val();
    if (visitDate == null || visitDate == "") {
        visitDate = new Date().Format("yyyy-MM-dd");
    }
    $.ajax({
        url: "/PatientManage/PatVisit/GetPatVisitListJson",
        dataType: "json",
        data: { visitDate: visitDate },
        async: false,
        success: function (data) {
            list = data;
        }
    });
    //更新病人列表
    refreshList();
    //$("#leftContent").css({ "height": $(window).height() - 135 });
}

function refreshList() {
    var ul = $("#patient-list");//patient-list
    ul.empty();
    var visitno = parseInt($("#visitNo").val()); //$("#visitNo").val();
    //var statusinit = $("#statusinit")[0].checked;
    //var statusuncomplete = $("#statusuncomplete")[0].checked;
    //var statuscomplete = $("#statuscomplete")[0].checked;
    if (list == undefined || list.length == undefined || list.length === 0) {
        selectid = '';
        currentPid = '';
        return;
    }
    //var filterlist = list.filter(t => visitno == 0 || t.VisitNo == visitno);
    var filterlist = $.grep(list, function (t, i) {
        return visitno == 0 || t.VisitNo == visitno
    });
    //if (statusinit && statusuncomplete && statuscomplete) {
    //    filterlist = filterlist;
    //} else if (statusinit && statusuncomplete) {
    //    filterlist = filterlist.filter(t => t.EndTime == "");
    //} else if (statusinit && statuscomplete) {
    //    filterlist = filterlist.filter(t => t.StartTime == "" || t.EndTime != "");
    //} else if (statusuncomplete && statuscomplete) {
    //    filterlist = filterlist.filter(t => t.StartTime != "");
    //} else if (statusinit) {
    //    filterlist = filterlist.filter(t => t.StartTime == "");
    //} else if (statusuncomplete) {
    //    filterlist = filterlist.filter(t => t.StartTime != "" && t.EndTime == "");
    //} else if (statuscomplete) {
    //    filterlist = filterlist.filter(t => t.EndTime != "");
    //} else {
    //    filterlist = [];
    //}
    //分组过滤
    var groupName = $("#groupName").val();
    if (!!groupName) {
        //filterlist = filterlist.filter(t => t.GroupName === groupName);
        filterlist = $.grep(filterlist, function (t, i) {
            return t.GroupName === groupName
        });
    }
    filterlist.sort(function (a, b) {
        return parseInt(a.DialysisBedNo) - parseInt(b.DialysisBedNo);
    }
    );

    filterlist.forEach(function (item) {
        ul.append("<li class=\"list-group-item\" id=\"" + item.Id + "\">"
            + "<span class=\"badge\">" + item.GroupName + item.DialysisBedNo + "</span>"
            + "<h5 class=\"list-group-item-heading\">" + item.Name + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + item.Gender + "&nbsp;&nbsp;" + item.AgeDesc + "</h5>"
            + "<p class=\"list-group-item-text\">" + item.DialysisType + "&nbsp;&nbsp;&nbsp;" + item.VascularAccess + "&nbsp;&nbsp;&nbsp;" + item.HeparinType + "&nbsp;&nbsp;&nbsp;</p>"
            + "</li>"
        );
    });

    selectid = '';
    currentPid = '';
}

function queryHistory() {
    if (!!currentPid) {
        $.ajax({
            url: "/PatientManage/PatVisit/GetHistoryRecordsJson",
            data: { keyword: currentPid },
            dataType: "json",
            async: true,
            success: function (data) {
                var $element = $("#date-list");
                $element.empty();
                $.each(data, function (i) {
                    $element.append($("<option></option>").val(data[i].id).html(data[i].text));
                });
            }
        })
    }
}

function setForm() {
    if (selectid == '') {
        return false;
    } else {
        $.ajax({
            url: "/PatientManage/PatVisit/GetFormJson",
            data: { keyValue: selectid },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#mainContent").formSerialize(data);
                $("input[type='radio']").prop("checked", false);
                if (!!data.F_PGwzsz) {
                    $("input[name='F_PGwzsz'][value=" + data.F_PGwzsz + "]").prop("checked", true);
                }
                if (!!data.F_PGxftz1) {
                    $("input[name='F_PGxftz1'][value=" + data.F_PGxftz1 + "]").prop("checked", true);
                }
                if (!!data.F_PGxftz3) {
                    $("input[name='F_PGxftz3'][value=" + data.F_PGxftz3 + "]").prop("checked", true);
                }
                if (!!data.F_PGwzcx1) {
                    $("input[name='F_PGwzcx1'][value=" + data.F_PGwzcx1 + "]").prop("checked", true);
                }
                if (!!data.F_DJMH) {
                    $("input[name='F_DJMH'][value=" + data.F_DJMH + "]").prop("checked", true);
                }
                if (!!data.F_DialyzerStatus) {
                    $("input[name='F_DialyzerStatus'][value=" + data.F_DialyzerStatus + "]").prop("checked", true);
                }
                if (!!data.F_FistulaStatus) {

                    if (data.F_FistulaStatus == "正常" || data.F_FistulaStatus == "渗血" || data.F_FistulaStatus == "肿胀") {  //正常/  渗血/ 肿胀
                        $("input[name='F_FistulaStatus'][value=" + data.F_FistulaStatus + "]").prop("checked", true);
                    }
                    else {
                        $("input[name='F_DuctOpeningStatus'][value=" + "其他" + "]").prop("checked", true);
                        $("#F_FistulaStatus").show();
                    }
                }
                if (!!data.F_DuctOpeningStatus) {

                    if (data.F_DuctOpeningStatus == "渗血" || data.F_DuctOpeningStatus == "红肿" || data.F_DuctOpeningStatus == "化脓") { //渗血/  红肿/   化脓
                        $("input[name='F_DuctOpeningStatus'][value=" + data.F_DuctOpeningStatus + "]").prop("checked", true);
                    }
                    else {
                        $("input[name='F_DuctOpeningStatus'][value=" + "其他" + "]").prop("checked", true);
                        $("#F_DuctOpeningStatus").show();

                    }

                }

                reQueryData();
            }
        });
    }
}

function reQueryData() {
    if (!!selectid) {
        //查询医嘱
       var ajaxtemp1 = $.ajax({
            url: "/PatientManage/Orders/GetPerformedOrderListJson",
            data: {
                pid: currentPid,
                visitDate: $("#F_VisitDate").val()
            },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#gridListOrders").jqGrid("clearGridData");
                $("#gridListOrders").setGridParam({ data: data }).trigger('reloadGrid');
            }
        });
        //查询观察记录
        var ajaxtemp2 = $.ajax({
            url: "/PatientManage/Nurse/GetListJson",
            data: {
                pid: currentPid,
                startDate: $("#F_VisitDate").val(),
                endDate: $("#F_VisitDate").val()
            },
            dataType: "json",
            async: false,
            success: function(data) {
                $("#gridListObs").jqGrid("clearGridData");
                $("#gridListObs").setGridParam({ data: data }).trigger('reloadGrid');
            }
        });
        $.when(ajaxtemp1, ajaxtemp2);
    } else {
        $("#gridListOrders").jqGrid("clearGridData");
        $("#gridListObs").jqGrid("clearGridData");
    }
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