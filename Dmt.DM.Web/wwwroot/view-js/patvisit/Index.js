//var filterValues = {};
//var year = new Date().getFullYear();
//var month = new Date().getMonth() + 1;
//var day = new Date().getDate();
//filterValues.startDate = year + '-' + month + '-' + day;
//filterValues.endDate = year + '-' + month + '-' + day;
//filterValues.bcall = true; //全部班次
//filterValues.bc1 = false; //第一班
//filterValues.bc2 = false;
//filterValues.bc3 = false;
//filterValues.statusall = true;  //不设置状态      
//filterValues.statuscomplete = false; //已完成
//filterValues.statusuncomplete = false; //未完成
//filterValues.keyword = "";

$(function () {
    $("#print").attr("style", "display:none;");//隐藏div
    $("#visitNo").bindSelect();
    $("#status").bindSelect();
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/PatientManage/PatVisit/GetGridJson",
        postData: getParms(),
        height: $(window).height() - 128,
        colModel: [
            { label: '主键', name: 'F_Id', hidden: true },
            {
                label: '透析日期', name: 'F_VisitDate', width: 60, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
            { label: '班次', name: 'F_VisitNo', width: 30, align: 'center' },
            { label: '透析号', name: 'F_DialysisNo', width: 60, align: 'left' },
            { label: '姓名', name: 'F_Name', width: 40, align: 'left' },
            { label: '性别', name: 'F_Gender', width: 30, align: 'left' },
            //{ label: '来源', name: 'F_PatientSourse', width: 40, align: 'left' },
            { label: '透析方式', name: 'F_DialysisType', width: 60, align: 'left' },
            { label: '血管通路', name: 'F_VascularAccess', width: 120, align: 'left' },
            { label: '通路名称', name: 'F_AccessName', width: 90, align: 'left' },
            { label: '肝素类型', name: 'F_HeparinType', width: 120, align: 'left' },
            { label: '首剂', name: 'F_HeparinAmount', width: 40, align: 'left' },
            { label: '追加量', name: 'F_HeparinAddAmount', width: 40, align: 'left' },
            { label: '滤器', name: 'F_DialyzerType1', width: 80, align: 'left' },
            { label: '灌流器', name: 'F_DialyzerType2', width: 80, align: 'left' },
            {
                label: '记账', name: 'F_IsAcct', width: 60, align: 'center',
                formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == 1) {
                        return '<span class=\"label label-success\">已记账</span>';
                    } else {
                        return '<span class=\"label label-default\">未记账</span>';
                    }
                }
            },
            {
                label: "危重", name: "F_IsCritical", width: 40, align: "center",
                formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == 1) {
                        return '<span class=\"label label-default\">禁用</span>';
                    } else {
                        return '';
                    }
                }
            },
            { label: '组别', name: 'F_GroupName', width: 40, align: 'left' },
            { label: '床号', name: 'F_DialysisBedNo', width: 40, align: 'left' },
            { label: '仪器名', name: 'F_MachineName', width: 80, align: 'left' },
            //{ label: '默认透析方式', name: 'F_DefaultType', width: 160, align: 'center' },
            {
                label: '创建日期', name: 'F_CreatorTime', width: 80, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
            { label: '患者主键', name: 'F_Pid', hidden: true }
        ],
        pager: "#gridPager",
        sortname: 'F_CreatorTime desc',
        viewrecords: true
    });

    $("#btn_search").click(function () {
        //filterValues.keyword = $("#txt_keyword").val();
        $gridList.jqGrid('setGridParam', {
            postData: getParms()
        }).trigger('reloadGrid');
    });
}

function getParms() {
    var postData = {};
    var startDate = $("#startDate").val();
    if (!!startDate) {
        postData.startDate = startDate;
    }
    var endDate = $("#endDate").val();
    if (!!endDate) {
        postData.endDate = endDate;
    }
    postData.visitNo = $("#visitNo").val();
    postData.status = $("#status").val();
    postData.keyword = $("#txt_keyword").val();
    return postData;
}

function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增治疗单",
        url: "/PatientManage/PatVisit/Form",
        width: "920px",
        height: "800px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
}
function btn_edit() {
    var keyValue = $("#gridList").jqGridRowValue().F_Id;
    if (!!keyValue) {
        $.modalOpen({
            id: "Form",
            title: "修改治疗单",
            url: "/PatientManage/PatVisit/Form?keyValue=" + keyValue,
            width: "920px",
            height: "800px",
            callBack: function (iframeId) {
                top.frames[iframeId].submitForm();
            }
        });
    }
}
function btn_delete() {
    var keyValue = $("#gridList").jqGridRowValue().F_Id
    if (!!keyValue) {
        $.deleteForm({
            url: "/PatientManage/PatVisit/DeleteForm",
            param: { keyValue: keyValue },
            success: function () {
                $.currentWindow().$("#gridList").trigger("reloadGrid");
            }
        })
    }
}
function btn_details() {
    var keyValue = $("#gridList").jqGridRowValue().F_Id;
    if (!!keyValue) {
        $.modalOpen({
            id: "Details",
            title: "查看治疗单",
            url: "/PatientManage/PatVisit/Form?keyValue=" + keyValue,
            width: "920px",
            height: "800px",
            btn: null
        });
    }
}
function btn_nurse() {
    var keyValue = $("#gridList").jqGridRowValue().F_Pid + "^" + $("#gridList").jqGridRowValue().F_VisitDate + "^" + $("#gridList").jqGridRowValue().F_DialysisNo;

    var id = $("#gridList").jqGridRowValue().F_Pid;
    if (!!id) {
        $.modalOpen({
            id: "ObDetails",
            title: "执行医嘱",
            url: "/PatientManage/PatVisit/ObDetails?keyValue=" + keyValue,
            width: "950px",
            height: "600px",
            callBack: function (iframeId) {
                top.frames[iframeId].submitForm();
            }
        });
    }
}

function btn_print() {
    var keyValue = $("#gridList").jqGridRowValue().F_Id;
    if (!!keyValue) {
        $.post("/PatientManage/PatVisit/GetReportImage?keyValue=" + keyValue,
            {
                postdata: keyValue
            },
            function(result) {
                //$("#images").attr("src", "data:image/png;base64," + result);
                $("#print").html(result);
                //console.log(result);
                $("#print").attr("style", "display:block;"); //显示div
                $("#print").jqprint();
                $("#print").attr("style", "display:none;"); //隐藏div
            });
    }
}

function printcard(result) {
    $("#print").html(result);
    //console.log(result);
    $("#print").attr("style", "display:block;");//显示div
    $("#print").jqprint();
    $("#print").attr("style", "display:none;");//隐藏div
}

function btn_acct() {
    var keyValue = $("#gridList").jqGridRowValue().F_Id;
    if (!!keyValue) {
        $.submitForm({
            url: "/PatientManage/PatVisit/SubmitCharge",
            param: { keyValue: keyValue },
            success: function() {
                $.currentWindow().$("#gridList").jqGrid('setGridParam',
                    {
                        postData: getParms()
                    }).trigger('reloadGrid');
            }
        });
    }
}

//function btn_filter() {
//    $.modalOpen({
//        id: "Form",
//        title: "筛选条件",
//        url: "/PatientManage/PatVisit/Filter?keyValue=" + encodeURI(JSON.stringify(filterValues)),
//        width: "400px",
//        height: "300px",
//        callBack: function (iframeId) {
//            top.frames[iframeId].submitForm();
//            var filterstr = $("#filterstr").val();
//            filterValues = $.parseJSON(decodeURI(filterstr));
//            filterValues.keyword = "";
//            $("#gridList").jqGrid('setGridParam', {
//                postData: filterValues,
//            }).trigger('reloadGrid');
//        }
//    });
//}

function btn_printbatch() {
    $.modalOpen({
        id: "Form",
        title: "批量打印",
        url: "/PatientManage/PatVisit/Prepare",
        width: "800px",
        height: "600px",
        btn: null
    });
}

function btn_printbatch2() {
    $.modalOpen({
        id: "Form",
        title: "批量打印",
        url: "/PatientManage/PatVisit/PrintBatch",
        width: "800px",
        height: "600px",
        btn: null
    });
}

//function btn_filter() {
//    $.modalOpen({
//        id: "Form",
//        title: "筛选条件",
//        url: "/PatientManage/PatVisit/Filter?keyValue=" + encodeURI(JSON.stringify(filterValues)),
//        width: "400px",
//        height: "300px",
//        callBack: function (iframeId) {
//            top.frames[iframeId].submitForm();
//            var filterstr = $("#filterstr").val();
//            filterValues = $.parseJSON(decodeURI(filterstr));
//            filterValues.keyword = "";
//            $("#gridList").jqGrid('setGridParam', {
//                postData: filterValues,
//            }).trigger('reloadGrid');
//        }
//    });
//}

function btn_printcard() {
    $.modalOpen({
        id: "Form",
        title: "筛选条件",
        url: "/PatientManage/PatVisit/CheckFilter?keyValue=" + new Date().Format('yyyy-MM-dd'),
        width: "500px",
        height: "420px",
        callBack: function (iframeId) {
            top.frames[iframeId].submitForm();
        }
    });
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
