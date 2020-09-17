var combineDict = [
    { id: '1085', name: '血常规' },
    { id: '1270', name: 'PTH' },
    { id: '1330', name: 'TP-Ab' },
    { id: '137', name: '电解质三项' },
    { id: '150', name: 'TP' },
    { id: '151', name: 'ALB' },
    { id: '152', name: 'GLOB' },
    { id: '153', name: 'A/G' },
    { id: '174', name: 'Ca' },
    { id: '175', name: 'P' },
    { id: '176', name: 'Mg' },
    { id: '183', name: 'UREA' },
    { id: '184', name: 'CREA' },
    { id: '185', name: 'UA' },
    { id: '319', name: 'HCV-Ab' },
    { id: '323', name: 'HBsAg' },
    { id: '324', name: 'HBsAb' },
    { id: '325', name: 'HBeAg' },
    { id: '326', name: 'HBeAb' },
    { id: '327', name: 'HBcAb' },
    { id: '334', name: 'HIV-Ab' }
];
var daResult = [];
$(function () {
    $("#report-list").height($(window).height() - 130);
    gridList();
    $("#btn-daquery").on('click', function () {
        $.submitForm({
            url: "/LabLis/DaLabReport/FetchDaResult",
            loading: "正在查询迪安报告...",
            param: {
                startDate: $("#da-startdate").val(),
                endDate: $("#da-enddate").val()
            },
            success: function (result) {
                daResult = result.data;
                var $target = $("#report-list");
                $target.empty();
                $target.html('');
                var htmlstr = '';
                $.each(daResult, function (index1, b) {
                    $.each(b.Servgrps, function (index2, s) {
                        $.each(s, function (index3, child) {
                            var servgrp = child.Servgrp;
                            var combinesStr = '';
                            $.each(child.Rows, function (index4, r) {
                                //var findrows = combineDict.filter(f => f.id === r.TestCode);
                                var findrows = $.grep(combineDict, function (f) {
                                    return f.id === r.TestCode
                                });
                                if (findrows.length > 0) {
                                    combinesStr = combinesStr + findrows[0].name + '&nbsp;&nbsp;';
                                } else {
                                    combinesStr = combinesStr + r.TestCode + '&nbsp;&nbsp;';
                                }
                            });
                            htmlstr = htmlstr + '\
                                            <a class="list-group-item">\
                                                <span class="badge">'+ child.Rows.length + '</span>\
                                                <h5 class="list-group-item-heading"><strong>'+ b.Barcode + '</strong>&nbsp;&nbsp;&nbsp;&nbsp;<i style="float: right;margin-right: 18px;">' + servgrp + '</i></h5>\
                                                <p class="list-group-item-text">\
                                                    <div class="row">\
                                                        <div class="col-xs-4 col-sm-3 col-md-3"><strong>'+ b.PatientName + '</strong></div>\
                                                        <div class="col-xs-3 col-sm-2 col-md-2">'+ (b.Gender === '女' ? '<i class="fa fa-female"></i>' : '<i class="fa fa-male"></i>') + '</div>\
                                                        <div class="col-xs-3 col-sm-2 col-md-2">'+ b.Age + b.AgeUnit + '</div>\
                                                    </div>\
                                                </p>\
                                                <p class="list-group-item-text">'+ combinesStr + '</p>\
                                            </a>\
                                            ';
                        });

                    });
                });
                $target.html(htmlstr);
                $target.find(".list-group-item").on("click", function (e) {
                    var $selectedItem = $(e.currentTarget).find('.list-group-item-heading');
                    var barcode = $selectedItem.find('strong')[0].innerText;
                    var servgrp = $selectedItem.find('i')[0].innerText;
                    //var filterdata = daResult.filter(t => t.Barcode === barcode)[0].Servgrps;
                    var filterdata = [];
                    $.each(daResult, function (i, t) {
                        if (t.Barcode === barcode) {
                            filterdata = t.Servgrps;
                            return false;
                        }
                    });
                    var gridData = [];
                    $.each(filterdata, function (index_3, value) {
                        //var filterdata2 = value.filter(t => t.Servgrp === servgrp);
                        var filterdata2 = $.grep(value, function (t, i) {
                            return t.Servgrp === servgrp;
                        });
                        if (filterdata2.length > 0) {
                            $.each(filterdata2[0].Rows, function (index_4, row) {
                                var testName = '';
                                //var findrows = combineDict.filter(f => f.id === row.TestCode);
                                //var findrows = $.grep(combineDict, function (f, i) {
                                //    return f.id === row.TestCode;
                                //});
                                //if (findrows.length > 0) {
                                //    testName = findrows[0].name;
                                //}
                                $.each(combineDict, function (i, f) {
                                    if (f.id === row.TestCode) {
                                        testName = f.name;
                                        return false;
                                    }
                                });
                                $.each(row.Items, function (index_5, item) {
                                    gridData.push({
                                        TestCode: row.TestCode,
                                        TestName: testName,
                                        SampleType: row.SampleType,
                                        CollectDate: row.CollectDate.substr(5, 5) + ' ' + row.CollectDate.substr(11, 5),
                                        SubmitDate: row.SubmitDate.substr(5, 5) + ' ' + row.SubmitDate.substr(11, 5),
                                        ApprDate: row.ApprDate,
                                        TestDate: row.TestDate,
                                        Sinonym: item.Sinonym,
                                        ShortName: item.ShortName,
                                        Units: item.Units,
                                        Final: item.Final,
                                        Analyte: item.Analyte,
                                        DispLowHigh: item.DispLowHigh,
                                        S: item.S,
                                        SynonimEn: item.SynonimEn,
                                        LowB: item.LowB,
                                        HighB: item.HighB,
                                        Sorter: item.Sorter,
                                        LongTxt: item.LongTxt,
                                        RangeFlag: item.RangeFlag,
                                        Rn10: item.Rn10,
                                        Rn20: item.Rn20
                                    });
                                });
                            });
                        }
                    });
                    $("#gridList").jqGrid('clearGridData').setGridParam({ data: gridData }).trigger('reloadGrid');
                });
            }
        });
    });
})
function gridList() {
    var $gridList = $("#gridList");
    $gridList.dataGrid({
        //url: "/DaLabLis/LabItem/GetGridJson",
        datatype: 'local',
        height: $(window).height() - 125,
        colModel: [
            { label: '大项代码', name: 'TestCode', hidden: true },
            { label: '大项名称', name: 'TestName', width: 120, align: 'left' },
            { label: '子项简称', name: 'ShortName', width: 100, align: 'left' },
            { label: '子项名称', name: 'Sinonym', width: 150, align: 'left' },
            {
                label: '结果', name: 'Final', width: 80, align: 'center',
                formatter: function (value, options, rowData) {
                    return changeCellColor(value, options, rowData);
                }
            },
            { label: '结果描述', name: 'LongTxt', width: 80, align: 'left' },
            { label: '异常标识', name: 'Rn20', width: 80, align: 'center' },
            { label: '参考值', name: 'DispLowHigh', width: 80, align: 'left' },
            { label: '标本时间', name: 'CollectDate', width: 80, align: 'left' },
            { label: '报告时间', name: 'SubmitDate', width: 80, align: 'left' },
            { label: '样本类型', name: 'SampleType', width: 80, align: 'left' }

        ],
        pager: "#gridPager",
        viewrecords: true
    });
}

function changeCellColor(value, options, rowData) {
    if (!!value) {
        if (!isNaN(value)) {
            if (true) {
                if (rowData.Rn20 === '↓') {
                    return '<b style="color:#87ceeb">' + value + '</b>';
                } else if (rowData.Rn20 === '↑') {
                    return '<b style="color:#eba589">' + value + '</b>';
                }
            }
        }
    }
    return value;
}