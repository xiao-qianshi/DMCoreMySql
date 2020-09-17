var patientList = [];
//var records = [];
var gridData = [];
$(function () {
    initControl();
    gridList();
    btn_query();
})
function initControl() {
    //添加日期选项
    var ajax1 = $.ajax({
        url: '/DataStatistics/JSReport/GetMonthOptionsJson',
        dataType: 'json',
        async: false,
        success: function (months) {
            var arrtemp = [];
            $.each(months, function (key, value) {
                arrtemp.push({ id: key, text: value });
            });
            arrtemp.sort(function (a, b) {
                return a.id - b.id;
            });
            var $target = $("ul.dropdown-menu");
            $.each(arrtemp, function (index, value) {
                if (value.id == 0) {
                    $("#txt_condition .dropdown-text").html(value.text).attr('data-value', value.text);
                }
                $target.append('<li><a data-value="' + value.text + '">' + value.text + '</a></li>');
            });
        }
    })
    var ajax2 = $.ajax({
        url: '/PatientManage/Patient/GetSelectJson',
        dataType: 'json',
        async: false,
        success: function (data) {
            if ($.isArray(data)) {
                patientList = data;
                patientList.sort(function (a, b) {
                    return a.name - b.name;
                });
            }
        }
    });
    $.when(ajax1, ajax2);
    $("#txt_condition .dropdown-menu li").click(function () {
        var text = $(this).find('a').html();
        var value = $(this).find('a').attr('data-value');
        $("#txt_condition .dropdown-text").html(text).attr('data-value', value);
        queryRecord(value);
        $("#btn_search").trigger('click');
    });
    $("#btn_search").click(function () {
        var filterword = $("#txt_keyword").val();
        fillTable(filterword);
    });
}

function gridList() {
    var $gridList = $("#gridList");
    $gridList.jqGrid({
        datatype: 'local',
        height: $(window).height() - 128,
        colModel: [
            { label: "主键", name: "recordNo", hidden: true },
            { label: "患者主键", name: "patientId", hidden: true },
            { label: '月份', name: 'monthDesc', width: 60, align: 'left' },
            { label: '姓名', name: 'patientName', width: 60, align: 'left' },
            { label: '病历号', name: 'patientNo', width: 60, align: 'left' },
            { label: '文件名', name: 'fileName', width: 145, align: 'left' },
            { label: '文件路径', name: 'filePath', width: 370, align: 'left' },
            //{
            //    label: '状态', name: 'isCompleted', width: 60, align: 'left',
            //    formatter: function (cellvalue) {
            //        if (cellvalue == true) {
            //            return 'kk';
            //        } else {
            //            return "";
            //        }
            //    }
            //},
            {
                label: '是否下载', name: 'hasDownload', width: 60, align: 'left',
                formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == true) {
                        return '<span class=\"label label-success\">已下载</span>';
                    } else if (rowObject.isCompleted == true) {
                        return '<span class=\"label label-default\">未下载</span>';
                    } else {
                        return '';
                    }
                }
            },
            {
                label: '下载时间', name: 'downloadTime', width: 120, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i', newformat: 'Y-m-d H:i' }
            },
            {
                label: '创建时间', name: 'creatorTime', width: 120, align: 'left',
                formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i', newformat: 'Y-m-d H:i' }
            }
        ],
        pager: "#gridPager",
        sortname: 'patientName asc',
        recordpos: 'left',
        viewrecords: true,
        multiselect: true,//复选框
        onSelectRow: function (rowid) {
            var $operate = $(".operate");
            var rowIds = $("#gridList").jqGrid("getGridParam", "selarrrow");
            if (rowIds.length > 0) {
                if (true) {
                    $operate.animate({ "left": 0 }, 10);
                }
                $operate.find('li.first').find('span').text(rowIds.length);
            } else {
                $operate.animate({ "left": '-100.1%' }, 10);
            }
            $operate.find('.close').click(function () {
                $operate.animate({ "left": '-100.1%' }, 200);
            });
        },
        autowidth: true,
        rownumbers: true,
        shrinkToFit: false,
        gridview: true
    });

}
function btn_query() {
    var month = $("#txt_condition .dropdown-text").attr('data-value');
    queryRecord(month);
    $("#btn_search").trigger('click');
}
function btn_create() {
    var $gridList = $("#gridList");
    var rowIds = $gridList.jqGrid("getGridParam", "selarrrow");
    if (rowIds.length == 0) {
        return false;
    }
    var selectIds = [];
    $.each(rowIds, function (index, value) {
        var rowdata = $gridList.jqGrid('getRowData', value);
        if (!rowdata.isCompleted) {
            selectIds.push(rowdata.patientId);
        }
    });
    if (selectIds.length > 0) {
        $.submitForm({
            url: '/DataStatistics/JSReport/SubmitData',
            param: {
                month: $("#txt_condition .dropdown-text").attr('data-value'),
                ids: selectIds.join(',')
            },
            success: function (result) {
                //if ($.isArray(result.data)) {
                //}
            }
        });
    }
}

function btn_download() {
    var $gridList = $("#gridList");
    var rowIds = $gridList.jqGrid("getGridParam", "selarrrow");
    if (rowIds.length == 0) {
        return false;
    }
    var selectIds = [];
    $.each(rowIds, function (index, value) {
        var rowdata = $gridList.jqGrid('getRowData', value);
        if (!!rowdata.recordNo) {
            //selectIds.push({ id: rowdata.recordNo });
            selectIds.push(rowdata.recordNo);
        }
    });
    if (selectIds.length > 0) { //数组有数据
        //$.download("/DataStatistics/JSReport/Download", "ids=" + JSON.stringify(selectIds), 'post');
        $.download("/DataStatistics/JSReport/Download", "ids=" + selectIds.join(','), 'post');
    }
}
function btn_delete() {
    var $gridList = $("#gridList");
    var rowIds = $gridList.jqGrid("getGridParam", "selarrrow");
    if (rowIds.length == 0) {
        return false;
    }
    var selectIds = [];
    $.each(rowIds, function (index, value) {
        var rowdata = $gridList.jqGrid('getRowData', value);
        if (!!rowdata.recordNo) {
            selectIds.push(rowdata.recordNo);
        }
    });
    if (selectIds.length > 0) { //数组有数据
        $.deleteForm({
            url: "/DataStatistics/JSReport/DeleteData",
            param: { ids: selectIds.join(',') },
            success: function () {
                btn_query();
            }
        });

    }

}

function queryRecord(month) {
    $.ajax({
        url: '/DataStatistics/JSReport/GetListJson',
        dataType: 'json',
        data: {
            month: month
        },
        async: false,
        success: function (data) {
            if ($.isArray(data)) {
                //整合数据
                gridData = []
                $.each(patientList, function (index, value) {
                    var item = {
                        patientId: value.id,
                        patientName: value.name,
                        patientNo: value.recordNo,
                        py: value.py,
                        idealWeight: value.idealWeight,
                        beInfected: value.beInfected
                    };
                    var findid = -1;
                    $.each(data, function (i, v) {
                        if (item.patientId == v.F_PId) {
                            findid = i;
                            item.recordNo = v.F_Id;
                            item.monthDesc = v.F_MonthDesc;
                            item.fileName = v.F_FileName;
                            item.fileSize = v.F_FileSize;
                            item.filePath = v.F_FilePath;
                            item.isCompleted = v.F_IsCompleted;
                            item.hasDownload = v.F_HasDownload;
                            item.downloadTime = v.F_DownloadTime;
                            item.creatorTime = v.F_CreatorTime;
                            return false;
                        }
                    })
                    if (findid >= 0) {
                        data.splice(findid, 1);
                    } else {
                        item.recordNo = null;
                        item.monthDesc = month;
                        item.fileName = null;
                        item.fileSize = null;
                        item.filePath = null;
                        item.isCompleted = null;
                        item.hasDownload = null;
                        item.downloadTime = null;
                        item.creatorTime = null;
                    }
                    gridData.push(item);
                });
            }
        }
    });
}
function fillTable(filterword) {
    var fliterrows = [];
    if (!!filterword) {
        fliterrows = $.grep(gridData, function (el) {
            return el.patientName.indexOf(filterword) >= 0 || el.py.toUpperCase().indexOf(filterword.toUpperCase()) >= 0;
        });
    } else {
        fliterrows = gridData; //$.extend(true, [], gridData); //深拷贝
    }
    $("#gridList").jqGrid('clearGridData').setGridParam({ data: fliterrows }).trigger('reloadGrid');
}