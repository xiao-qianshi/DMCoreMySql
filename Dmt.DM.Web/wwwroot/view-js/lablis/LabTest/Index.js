var currentInstrumentId = '';
var machineList = [];
var testList = [];
var currentTestStatus = 0;
var resultList = [];

//单元格编辑
//var g_lastrow = null;
//var g_lastcol = null;
//var g_lastname = null;

$(function () {
    initControl();
    $.ajax({
        url: "/LabLis/LabInstrument/GetListJson",
        dataType: "json",
        async: false,
        success: function (data) {
            if ($.isArray(data)) {
                if (data.length == 0) {
                    $.modalMsg('请先添加检验仪器！', 'warning');
                    return false;
                }
                currentInstrumentId = data[0].F_Id;
                machineList = data;
                setHeader(data[0]);
                queryTestList();
                setSampleList();
                setHeaderCount();
            } else {
                $.modalMsg('查询检验仪器出错！', 'warning');
                return false;
            }
        }
    });
});

function initControl() {
    $("#testDate").val(new Date().Format('yyyy-MM-dd'));
    $("#sample-list").height($(window).height() - 173);
    $("#btn-choose").click(function () {
        $.modalOpen({
            id: "ChooseInstrument",
            title: "选择设备",
            url: "/LabLis/LabInstrument/ChooseInstrument",
            width: "800px",
            height: "600px",
            callBack: function (iframeId) {
                top.frames[iframeId].submitForm();
            }
        });
    });
    $("#btn-search").click(function () {
        queryTestList();
        setHeaderCount();
        setSampleList();
    });
    $("#status_horizon a.btn-default").click(function () {
        $("#status_horizon a.btn-default").removeClass("active");
        $(this).addClass("active");
        setSampleList();
    });
    $("#btn-assign").click(function () {
        var testDate = $("#testDate").val();
        if (testDate == '' || currentInstrumentId == '') {
            return false;
        } else {
            var params = [currentInstrumentId, testDate];
            $.modalOpen({
                id: "Assign",
                title: "条码编号",
                url: "/LabLis/LabTest/Assign?keyword=" + params.join(','),
                width: "450px",
                height: "300px",
                callBack: function (iframeId) {
                    top.frames[iframeId].submitForm();
                }
            });
        }
    });
    $("#btn-audit").click(function () {
        var currentId = getCurrentTestId();
        if (!!currentId) {
            $.submitForm({
                url: '/LabLis/LabTest/AuditTest',
                param: {
                    keyValue: currentId
                },
                success: function (result) {
                    //if ($.isArray(result.data)) {
                    //    resultList = result.data;
                    //    $("#gridList").jqGrid('clearGridData').setGridParam({ data: resultList }).trigger('reloadGrid');
                    //}
                }
            });
        }
    })
    gridList();
}

function btn_additem() {
    var currentId = getCurrentTestId();
    if (!!currentId) {
        if (currentTestStatus == 3) { //已审核
            $.modalAlert('已审核', 'warning');
            return false;
        }
        var itemCodeArr = [currentInstrumentId];
        $.each(resultList, function (i, v) {
            itemCodeArr.push(v.F_Code);
        });
        $.modalOpen({
            id: "AddItem",
            title: "添加子项",
            url: "/LabLis/LabTest/AddItem?keyword=" + itemCodeArr.join(','),
            width: "650px",
            height: "450px",
            callBack: function (iframeId) {
                top.frames[iframeId].submitForm();
            }
        });
    }
}

function appendItem(selectedItems) {
    var currentId = getCurrentTestId();
    if (!!currentId) {
        $.each(selectedItems, function (i, v) {
            resultList.push({
                F_Code: v.F_Code,
                F_LowRef: v.F_ReferenceMinValue,
                F_MethodName: '',
                F_Name: v.F_Name,
                F_ShortName: v.F_CnName,
                F_Sorter: v.F_Sorter,
                F_Unit: v.F_ValueUnit,
                F_UpperRef: v.F_ReferenceMaxValue,
                F_ItemId: v.F_Id,
                F_Flag: ''
            });
        });
        resultList = resultList.sort(function (a, b) {
            return a.F_Sorter - b.F_Sorter;
        });
        $("#gridList").jqGrid('clearGridData').setGridParam({ data: resultList }).trigger('reloadGrid');
    }
}

function btn_deleteitem() {
    var currentId = getCurrentTestId();
    if (!!currentId) {
        if (currentTestStatus == 3) { //已审核
            $.modalAlert('已审核', 'warning');
            return false;
        }
        var $gridList = $("#gridList");
        var rowIds = $gridList.jqGrid("getGridParam", "selarrrow");
        if (rowIds.length == 0) {
            return false;
        }
        var deleteCodeArr = [];
        $.each(rowIds, function (index, value) {
            var rowdata = $gridList.jqGrid('getRowData', value);
            deleteCodeArr.push(rowdata.F_ItemId);
        });
        resultList = $.grep(resultList, function (el) {
            return $.inArray(el.F_ItemId, deleteCodeArr) < 0;
        });
        resultList = resultList.sort(function (a, b) {
            return a.F_Sorter - b.F_Sorter;
        });
        $gridList.jqGrid('clearGridData').setGridParam({ data: resultList }).trigger('reloadGrid');
    }
}

function btn_setdefault() {

}

function btn_saveitem() {
    var currentId = getCurrentTestId();
    if (!!currentId) {
        if (currentTestStatus == 3) { //已审核
            $.modalAlert('已审核', 'warning');
            return false;
        }
        if (resultList.length == 0) {
            return false;
        } else {
            $.submitForm({
                url: '/LabLis/LabTest/SubmitItemData',
                param: {
                    testId: currentId,
                    items: JSON.stringify(resultList)
                },
                success: function (result) { //返回合并记录
                    if ($.isArray(result.data)) {
                        resultList = result.data;
                        $("#gridList").jqGrid('clearGridData').setGridParam({ data: resultList }).trigger('reloadGrid');
                    }
                }
            });
        }
    }
}

function btn_critical() {

}

function getCurrentTestId() {
    var findrows = $("#sample-list").find('a.selected-a');
    if (findrows.length == 0) {
        return '';
    } else {
        return findrows[0].id;
    }
}

function gridList() {
    var $gridList = $("#gridList");
    $gridList.jqGrid({
        datatype: 'local',
        height: $(window).height() - 220,
        colModel: [
            { label: '主键', name: 'F_Id', hidden: true },
            { label: '排序号', name: 'F_Sorter', hidden: true },
            { label: '代码', name: 'F_Code', width: 50, align: 'left' },
            { label: '名称', name: 'F_Name', width: 80, align: 'left' },
            { label: '简称', name: 'F_ShortName', width: 50, align: 'center' },
            { label: '结果', name: 'F_Result', width: 50, align: 'left', editable: true },
            { label: '单位', name: 'F_Unit', width: 40, align: 'left' },
            { label: '文本结果', name: 'F_ResultText', width: 200, align: 'left', editable: true },
            {
                label: '标识', name: 'F_Flag', width: 40, align: 'center',
                formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == 'L') {
                        return '<i class="fa fa-arrow-down" style="color:#eba589"></i>';
                    } else if (cellvalue == 'H') {
                        return '<i class="fa fa-arrow-up" style="color:#87ceeb"></i>';
                    } else {
                        return '';
                    }
                }
            },
            {
                label: '危急值', name: 'F_IsCritical', width: 40, align: 'center',
                formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == true) {
                        return '<i class="fa fa-circle" style="color: red;"></i>';
                    } else {
                        return '';
                    }
                }
            },
            { label: '参考下限', name: 'F_LowRef', width: 60, align: 'left' },
            { label: '参考上限', name: 'F_UpperRef', width: 60, align: 'left' },
            { label: '检测方法', name: 'F_MethodName', width: 150, align: 'left' },
            { label: '子项代码', name: 'F_ItemId', hidden: true }

        ],
        pager: "#gridPager",
        sortname: 'F_Sorter asc',
        recordpos: 'left',
        viewrecords: true,
        multiselect: true,//复选框,
        autowidth: true,
        rownumbers: true,
        shrinkToFit: false,
        gridview: true,
        cellEdit: true,
        cellsubmit: 'clientArray',
        beforeEditCell: function (rowid, cellname, v, iRow, iCol) {
            //console.log(rowid, cellname, v, iRow, iCol);
            //g_lastrow = iRow;
            //g_lastcol = iCol;
            //g_lastname = v;
        },
        afterSaveCell: function (id, name, val, iRow, iCol) {
            //console.log(id, name, val, iRow, iCol);
            if (iCol == 7) {
                resultList[id - 1].F_Result = val;
            } else if (iCol == 9) {
                resultList[id - 1].F_ResultText = val;
            }
        }
    });
}
function resetTitle(keyValue) {
    if (!!keyValue) {
        $.each(machineList, function (i, v) {
            if (v.F_Id == keyValue) {
                setHeader(v);
                currentInstrumentId = v.F_Id;
                queryTestList();
                setSampleList();
                setHeaderCount();
            }
        });
    }
}

function handleDateChange(e) {
    if (e !== undefined) {
        if (e.el.id == "testDate") {
            $("#btn-search").trigger('click');
        }
    }
}
//刷新样本列表
function queryTestList() {
    var testDate = $("#testDate").val();
    if (!!currentInstrumentId) {
        $.ajax({
            url: '/LabLis/LabTest/GetDetailedListJson',
            dataType: "json",
            data: {
                testDate: testDate,
                instrumentId: currentInstrumentId
            },
            async: false,
            success: function (data) {
                if ($.isArray(data)) {
                    testList = data;
                } else {
                    testList = [];
                }
            }
        })
    }
}

function setSampleList() {
    var target = $("#sample-list");
    target.empty();
    target.html('');
    var filterValue = $("#status_horizon a.active").attr('data-value');
    var filterrows = testList;
    if (!!filterValue) {
        filterrows = $.grep(testList, function (el) {
            return el.status == filterValue;
        });
    }
    $.each(filterrows, function (i, v) {
        var itemDesc = [];
        $.each(v.items, function (index, value) {
            itemDesc.push(value.itemShortName);
        });
        target.append('\
                                <a class="list-group-item" id="'+ v.id + '">\
                                    <span class="badge">'+ v.testNo + '</span>\
                                    <h5 class="list-group-item-heading">\
                                        <strong>'+ v.barcode + '</strong>&nbsp;&nbsp;&nbsp;&nbsp;\
                                       '+ v.sampleType + '\
                                    </h5>\
                                    <p class="list-group-item-text">\
                                         <strong>'+ v.patientName + '</strong>&nbsp;&nbsp;' + v.patientGender + '&nbsp;&nbsp;' + v.patientAgeStr + '&nbsp;&nbsp;' + v.dialysisNo + '&nbsp;&nbsp;\
                                    </p>\
                                    <p class="list-group-item-text">\
                                        '+ itemDesc.join(',') + '\
                                    </p>\
                                </a>\
                                ');
    });
    target.find('a').click(function (e) {
        target.find('a.selected-a').removeClass('selected-a');
        $(this).addClass('selected-a');
        setTestStatus(e.currentTarget.id);
        queryReport(e.currentTarget.id);

    });
}

function setTestStatus(id) {
    $.each(testList, function (i, v) {
        if (v.id == id) {
            currentTestStatus = v.status;
            return false
        }
    });
}

function setHeader(machine) {
    $("#h-instrumentCode").html(machine.F_Code);
    $("#h-instrumentName").html(machine.F_Name);
    if (machine.F_IsExternal) {
        $("#h-isExternal").attr('style', 'display: inline;');
    } else {
        $("#h-isExternal").attr('style', 'display: none;');
    }
    if (machine.F_IsDuplex) {
        $("#h-isDuplex").removeClass().addClass('fa').addClass('fa-arrows-h');
    } else {
        $("#h-isDuplex").removeClass().addClass('fa').addClass('fa-arrows-right');
    }
    $("#h-workStationIp").html(machine.F_WorkStationIp);
}

function setHeaderCount() {
    var count1 = 0, count2 = 0, count3 = 0;
    $.each(testList, function (i, v) {
        if (v.status == 0) {
            count1++;
        } else if (v.status == 1) {
            count2++;
        } else if (v.status == 2) {
            count3++;
        }
        $("#span-count1").html(count1);
        $("#span-count2").html(count2);
        $("#span-count3").html(count3);
    });
}

function queryReport(id) {
    $.ajax({
        url: '/LabLis/LabTest/GetResultListJson',
        dataType: "json",
        data: {
            keyValue: id
        },
        async: false,
        success: function (data) {
            if ($.isArray(data)) {
                resultList = data;
            } else {
                resultList = [];
            }
            //$("#gridList").jqGrid('clearGridData').setGridParam({
            //    data: resultList,
            //    cellEdit: currentTestStatus != 3,
            //    multiselect: currentTestStatus != 3
            //}).trigger('reloadGrid');
            var $gridList = $("#gridList");
            $gridList.jqGrid('clearGridData').setGridParam({
                data: resultList,
                cellEdit: currentTestStatus != 3
            }).trigger('reloadGrid');
            //cellEdit: currentTestStatus != 3,
            //    multiselect: currentTestStatus != 3
            //if (currentTestStatus == 3) {
            //    $gridList.setGridParam({ cellEdit: false }).trigger('reloadGrid');
            //}
        }
    })
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