$(function () {
    gridList();
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        url: "/PatientManage/Patient/GetGridJson",
        height: $(window).height() - 128,
        colModel: [
            { label: '主键', name: 'F_Id', hidden: true },
            { label: '姓名', name: 'F_Name', width: 80, align: 'left' },
            { label: '透析号', name: 'F_DialysisNo', width: 80, align: 'left' },
            { label: '病历号', name: 'F_RecordNo', width: 80, align: 'left' },
            { label: 'HISID', name: 'F_PatientNo', width: 80, align: 'left' },
            { label: '性别', name: 'F_Gender', width: 60, align: 'center' },
            {
                label: '首次透析日期', name: 'F_DialysisStartTime', width: 80, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
            { label: '理想体重', name: 'F_IdealWeight', width: 80, align: 'left' },
            {
                label: '出生日期', name: 'F_BirthDay', width: 80, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }
            },
            { label: '婚姻状况', name: 'F_MaritalStatus', width: 80, align: 'left' },
            { label: '卡号', name: 'F_CardNo', width: 80, align: 'left' },
            {
                label: "状态", name: "F_EnabledMark", width: 60, align: "center",
                formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == 1) {
                        return '<span class=\"label label-success\">正常</span>';
                    } else if (cellvalue == 0) {
                        return '<span class=\"label label-default\">禁用</span>';
                    }
                }
            }
        ],
        pager: "#gridPager",
        sortname: 'F_DialysisNo desc,F_CreatorTime desc',
        viewrecords: true
    });
    $("#btn_search").click(function () {
        $gridList.jqGrid('setGridParam', {
            postData: { keyword: $("#txt_keyword").val() },
        }).trigger('reloadGrid');
    });
}
function btn_add() {
    $.modalOpen({
        id: "Form",
        title: "新增患者",
        url: "/PatientManage/Patient/Form",
        width: "800px",
        height: "600px",
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
            title: "修改患者",
            url: "/PatientManage/Patient/Form?keyValue=" + keyValue,
            width: "800px",
            height: "600px",
            callBack: function (iframeId) {
                top.frames[iframeId].submitForm();
            }
        });
    }

}
function btn_delete() {
    var keyValue = $("#gridList").jqGridRowValue().F_Id;
    if (!!keyValue) {
        $.deleteForm({
            url: "/PatientManage/Patient/DeleteForm",
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
            title: "查看患者",
            url: "/PatientManage/Patient/Details?keyValue=" + keyValue,
            width: "800px",
            height: "600px",
            btn: null,
        });
    }

}

function btn_weightdetails() {
    var pid = $("#gridList").jqGridRowValue().F_Id;
    var ideaWeight = $("#gridList").jqGridRowValue().F_IdealWeight;
    var keyValue = {};
    keyValue.pid = pid;
    keyValue.ideaWeight = ideaWeight;
    if (!!pid) {
        $.modalOpen({
            id: "Form",
            title: "体重历史记录",
            url: "/PatientManage/Patient/WeightDetails?keyValue=" + encodeURI(JSON.stringify(keyValue)),
            width: "800px",
            height: "600px",
            btn: null,
        });
    }
}

function btn_barcode() {
    var pid = $("#gridList").jqGridRowValue().F_Id;
    var cardno = $("#gridList").jqGridRowValue().F_CardNo;
    var dialysisNo = $("#gridList").jqGridRowValue().F_DialysisNo;

    var keyValue = {};
    keyValue.pid = pid;
    keyValue.cardno = cardno;
    keyValue.dialysisNo = dialysisNo;
    if (!!pid) {
        $.modalOpen({
            id: "Form",
            title: "二维码",
            url: "/PatientManage/Patient/Barcode?keyValue=" + pid,//encodeURI(JSON.stringify(keyValue)),
            width: "800px",
            height: "600px",
            btn: null,
        });
    }
}
function btn_vascularaccess() {
    var keyValue = $("#gridList").jqGridRowValue().F_Id;

    $.modalOpen({
        id: "Details",
        title: "血管通路记录",
        url: "/PatientManage/Patient/AccessDetails?keyValue=" + keyValue,
        width: "900px",
        height: "800px",
        btn: null
    });
}
function btn_disabled() {
    var keyValue = $("#gridList").jqGridRowValue().F_Id;
    if (!!keyValue) {
        $.modalConfirm("注：您确定要【禁用】该项账户吗？", function (r) {
            if (r) {
                $.submitForm({
                    url: "/PatientManage/Patient/DisabledAccount",
                    param: { keyValue: keyValue },
                    success: function () {
                        $.currentWindow().$("#gridList").trigger("reloadGrid");
                    }
                })
            }
        });
    }
}
function btn_enabled() {
    var keyValue = $("#gridList").jqGridRowValue().F_Id;
    if (keyValue) {
        $.modalConfirm("注：您确定要【启用】该项账户吗？", function (r) {
            if (r) {
                $.submitForm({
                    url: "/PatientManage/Patient/EnabledAccount",
                    param: { keyValue: keyValue },
                    success: function () {
                        $.currentWindow().$("#gridList").trigger("reloadGrid");
                    }
                })
            }
        });
    }
}