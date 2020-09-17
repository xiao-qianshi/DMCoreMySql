var requestRecords = [];
var selectID = "";
$(function () {
    $("#txt_condition .dropdown-menu li").click(function () {
        var text = $(this).find('a').html();
        var value = $(this).find('a').attr('data-value');
        $("#txt_condition .dropdown-text").html(text).attr('data-value', value);
        fillTable();
    });
    gridList();
    var datenow = new Date().Format('yyyy-MM-dd');
    $("#requestDate").val(datenow);
    $("#btn_search").click('click', function () {
        queryRecords($("#requestDate").val());
        fillTable();
    });

    queryRecords(datenow);
    fillTable();
    initControl();
})
function initControl() {
    $("#NF-merge").click(function () {
        var selectRows = getSelectRows();
        if (selectRows.length == 0) {
            return false;
        } else {
            var isVal = true;
            var tempArr = [];
            var ids = [];

            $.each(selectRows, function (i, v) {
                if (tempArr.length === 0) {
                    tempArr.push(v.pid);
                } else {
                    if ($.inArray(v.pid, tempArr) >= 0) {
                    } else {
                        $.modalMsg('请操作同一患者的检验项目！', 'warning');
                        isVal = false;
                        return false;
                    }
                }
                //校验申请单状态 新申请 已提交 已采样 这三项可以继续
                var valStatus = ['1', '2', '3'];
                if (!$.inArray(v.status, valStatus)) {
                    $.modalMsg('样本已送检，不能操作！', 'warning');
                    isVal = false;
                    return false;
                }
                ids.push({ id: v.id });
            });
            if (isVal) {
                $.submitForm({
                    url: '/LabLis/LabRequest/SampleMerge',
                    param: {
                        keyValue: JSON.stringify(ids)
                    },
                    success: function (result) { //返回合并记录
                        //var deletedIndexs = [];
                        $.each(selectRows, function (i, v) {
                            if (v.id == result.data.id) { //更新行数据  更改缓存数据 data.id
                                //$.each(rowIds, function (index, value) {
                                //    if (value == v.id) {
                                //        $gridList.jqGrid('setRowData', value, result.data);
                                //    }
                                //});
                                //$gridList.jqGrid('setRowData', rowIds[i], result.data);
                                $.each(requestRecords, function (index, value) {
                                    if (value.id == v.id) {
                                        requestRecords.splice(index, 1, result.data);
                                        return false;
                                    }
                                });
                            } else { //删除行  删除对应本地缓存数据
                                //$.each(rowIds, function (index, value) {
                                //    if (value == v.id) {
                                //        $gridList.jqGrid('delRowData', value);
                                //    }
                                //});
                                //$gridList.jqGrid('delRowData', rowIds[i]);
                                $.each(requestRecords, function (index, value) {
                                    if (value.id == v.id) {
                                        requestRecords.splice(index, 1);
                                        //deletedIndexs.push(index);
                                        return false;
                                    }
                                });
                            }
                        });
                        //$.each(deletedIndexs, function (i, v) {
                        //    requestRecords.splice(v, 1);
                        //});
                        fillTable();
                    }
                });
            }
        }
    });
    $("#NF-spit").click(function () {
        var $gridList = $("#gridList");
        var rowIds = $gridList.jqGrid("getGridParam", "selarrrow");
        if ($.isArray(rowIds)) {
            if (rowIds.length == 1) {
                //打开拆分窗口 选择剔除的项目
                $.modalOpen({
                    id: "SpitForm",
                    title: "拆分项目",
                    url: "/LabLis/LabRequest/SpitForm?keyValue=" + rowIds[0],
                    width: "800px",
                    height: "600px",
                    callBack: function (iframeId) {
                        top.frames[iframeId].submitForm();
                    }
                });
            }
        } else {
            return false;
        }
    });
    $("#NF-delete").click(function () {
        var selectRows = getSelectRows();
        if (selectRows.length == 0) {
            return false;
        } else {
            var ids = [];
            $.each(selectRows, function (i, v) {
                ids.push({ id: v.id });
            });
            $.deleteForm({
                url: "/LabLis/LabRequest/BatchDelete",
                prompt: '确定删除这' + ids.length + '项吗？',
                param: { keyValue: JSON.stringify(ids) },
                success: function () {
                    $("#btn_search").trigger('click');
                }
            });
        }
    });
    $("#NF-createbarcode").click(function () {
        var selectRows = getSelectRows();
        if (selectRows.length == 0) {
            return false;
        } else {
            var ids = [];
            $.each(selectRows, function (i, v) {
                ids.push({ id: v.id });
            });
            $.submitForm({
                url: "/LabLis/LabRequest/BatchCreateBarcode",
                param: { keyValue: JSON.stringify(ids) },
                success: function () {
                    $("#btn_search").trigger('click');
                }
            });
        }
    });
    $("#NF-printbarcode").click(function () {
        var rowIds = $("#gridList").jqGrid("getGridParam", "selarrrow");
        if (rowIds.length > 0) {
            //var arr = [];
            //$.each(rowIds, function (i, v) {
            //    arr.push({ id: v });
            //});
            $.modalOpen({
                id: "PrintBarcode",
                title: "打印条码",
                //url: "/LabLis/LabRequest/PrintBarcode?keyword=" + encodeURI(JSON.stringify(arr)),
                url: "/LabLis/LabRequest/PrintBarcode?keyword=" + rowIds.join(','),
                width: "930px",
                height: "600px",
                callBack: function (iframeId) {
                    top.frames[iframeId].submitForm();
                }
            });
        }

    });
    $("#NF-matchbarcode").click(function () {
        var $gridList = $("#gridList");
        var rowIds = $gridList.jqGrid("getGridParam", "selarrrow");
        if (rowIds.length == 0) {
            $.modalMsg('未选择数据!', 'warning');
        } else {
            $.modalOpen({
                id: "MatchingBarcode",
                title: "匹配条码",
                url: "/LabLis/LabRequest/MatchingBarcode?keyword=" + rowIds.join(','),
                width: "800px",
                height: "600px",
                callBack: function (iframeId) {
                    top.frames[iframeId].submitForm();
                }
            });
        }
    });
    $("#NF-printcyd").click(function () {
        var $gridList = $("#gridList");
        var rowIds = $gridList.jqGrid("getGridParam", "selarrrow");
        if (rowIds.length == 0) {
            $.modalMsg('未选择数据!', 'warning');
        } else {
            var selectedItems = $.grep(requestRecords, function (el) {
                return $.inArray(el.id, rowIds) >= 0;
            });

            var options = {
                format: "CODE128",
                displayValue: true,
                fontSize: 20,
                height: 45,
                fontOptions: "bold",
                width: 2,
                marginTop: 5
            };
            var printTarget = $("#printContent");
            printTarget.empty();
            printTarget.html('');
            printTarget.append('<div class=\"a4-endwise\"></div>');
            var a4Container = $('div.a4-endwise');
            var requestDate = $("#requestDate").val();
            $.each(selectedItems, function (i, v) {
                var itemDesc = [];
                $.each(v.rows, function (index, value) {
                    itemDesc.push(value.shortName);
                });
                var htmlstr = '';
                //每页打印12项
                if (i % 12 == 0) {
                    if (i > 0) {
                        htmlstr = htmlstr + '<div style=\"page-break-after:always;\"></div>';
                    }
                    htmlstr = htmlstr + '\
                                <div class="page-header" style="text-align:center;margin-bottom: 25px;">\
                                  <h2>检验标本采样单<small>('+ requestDate + ')</small></h2>\
                                </div>\
                                ';

                }
                htmlstr = htmlstr + '\
                        <div class="thumbnail" style="float: left;margin-bottom: 15px;width: 33%;">\
                            <svg id="svg'+ v.id + '" />\
                            <div class="caption" style="margin-top: -2px;padding: 1px;">\
                                <p style="margin: 0 0 1px;overflow: hidden;white-space: nowrap;text-overflow: ellipsis;">\
                                     '+ v.name + '' + (v.beInfected ? '<sup>+</sup>' : '') + '&nbsp;&nbsp;&nbsp;' + v.gender + '&nbsp;&nbsp;&nbsp;' + (v.birthDay == null ? '' : byage(v.birthDay.substr(0, 10))) + '岁&nbsp;\
                                </p>\
                                <p style="margin: 0 0 1px;overflow: hidden;white-space: nowrap;text-overflow: ellipsis;">\
                                    '+ v.sampleType + '&nbsp;&nbsp;&nbsp;' + v.container + '\
                                </p>\
                                <p style="height: 50px;margin: 0 0 1px;overflow: hidden;text-overflow: ellipsis;display:-webkit-box;-webkit-box-orient: vertical;-webkit-line-clamp:4;font-size: 9px;font-weight: bold;word-break: break-all;">\
                                    '+ itemDesc.join(',') + '\
                                </p>\
                                <p style="margin: 0 0 1px;">\
                                    <string>开单:</string>&nbsp;'+ v.doctorName + '&nbsp;&nbsp;' + v.orderTime.substr(5, 11) + '\
                                </p>\
                                <p style="margin: 0 0 1px;">\
                                    <string>采样:</string>&nbsp;'+ (v.nurseName == null ? '' : v.nurseName) + '&nbsp;&nbsp;' + (v.samplingTime == null ? '' : v.samplingTime.substr(5, 11)) + '\
                                </p>\
                            </div>\
                        </div>\
                        ';
                a4Container.append(htmlstr);
                $("#svg" + v.id).JsBarcode(v.barcode, options);
            });
            printTarget.attr("style", "display:block;");//显示div
            printTarget.jqprint();
            printTarget.attr("style", "display:none;");//隐藏div
        }
    });
    $("#NF-sampling").click(function () {
        $.modalOpen({
            id: "ConfirmSample",
            title: "确认采集",
            url: "/LabLis/LabRequest/ConfirmSample",
            width: "500px",
            height: "300px",
            callBack: function (iframeId) {
                top.frames[iframeId].submitForm();
            }
        });
    });
    $("#NF-download").click(function () {
        var $gridList = $("#gridList");
        var rowIds = $gridList.jqGrid("getGridParam", "selarrrow");
        if (rowIds.length == 0) {
            $.modalMsg('未选择数据!', 'warning');
        } else {
            $.download("/LabLis/LabRequest/Download", "keyValue=" + rowIds.join(','), 'post');
        }
    });
    $("#NF-upload").click(function () {
        var $gridList = $("#gridList");
        var rowIds = $gridList.jqGrid("getGridParam", "selarrrow");
        if (rowIds.length == 0) {
            $.modalMsg('未选择数据!', 'warning');
        } else {
            //$.download("/LabLis/LabRequest/Upload", "ids=" + rowIds.join(','), 'post');
            $.submitForm({
                url: '/LabLis/LabRequest/Upload',
                param: {
                    keyValue: rowIds.join(',')
                },
                success: function (result) { //返回合并记录
                }
            });
        }
    });
}
//已勾选项目
function getSelectRows() {
    var $gridList = $("#gridList");
    var selectRows = []
    var rowIds = $gridList.jqGrid("getGridParam", "selarrrow");
    if ($.isArray(rowIds)) {
        $.each(rowIds, function (i, v) {
            selectRows.push($gridList.jqGrid('getRowData', v));
        });
    }
    return selectRows;
}

Date.prototype.Format = function (fmt) {
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

function gridList() {
    var $gridList = $("#gridList");
    $gridList.jqGrid({
        datatype: 'local',
        height: $(window).height() - 135,
        colModel: [
            { label: '主键', name: 'id', hidden: true },
            { label: '患者ID', name: 'pid', hidden: true },
            { label: '姓名', name: 'name', width: 40, align: 'left' },
            { label: '性别', name: 'gender', width: 25, align: 'center' },
            {
                label: '状态', name: 'status', width: 70, align: 'center',
                formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == 1) {
                        return '新申请';
                    } else if (cellvalue == 2) {
                        return '已提交';
                    } else if (cellvalue == 3) {
                        return '已采样';
                    } else if (cellvalue == 4) {
                        return '正在检验';
                    } else if (cellvalue == 5) {
                        return '已审核';
                    } else if (cellvalue == 6) {
                        return '已打印';
                    }
                }
            },
            {
                label: '条码号', name: 'barcode', width: 95, align: 'left',
                formatter: function (cellvalue, options, rowObject) {
                    return '<strong>' + (cellvalue == null ? '' : cellvalue) + '</strong>';
                }
            },
            {
                label: '检验项目', name: 'itemDesc', width: 250, align: 'left',
                formatter: function (cellvalue, options, rowObject) {
                    var arrtemp = [];
                    $.each(rowObject.rows, function (i, v) {
                        arrtemp.push(v.shortName);
                    });
                    return arrtemp.join(',');
                }
            },
            { label: '样本类型', name: 'sampleType', width: 70, align: 'left' },
            { label: '样本容器', name: 'container', width: 80, align: 'left' },
            {
                label: '采样时间', name: 'samplingTime', width: 120, align: 'left'
            },
            {
                label: '签收时间', name: 'testTime', width: 120, align: 'left'
            },
            {
                label: '审核时间', name: 'auditTime', width: 120, align: 'left'
            },
            {
                label: '报告时间', name: 'reportTime', width: 120, align: 'left'
            }
        ],
        pager: "#gridPager",
        sortname: 'name asc',
        //viewrecords: true,
        recordpos: 'left',
        viewrecords: true,
        multiselect: true,//复选框
        onSelectRow: function (rowid) {
            var $operate = $(".operate");
            //console.log($operate, $operate[0].attributes[1]);
            //var rowIds = $("#gridList").jqGrid('getDataIDs');[0].attributes[1].value  [1].value
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
        //rownumbers: true,
        shrinkToFit: false,
        gridview: true
    });
}

function queryRecords(requestDate) {
    //查询数据
    $.ajax({
        url: "/LabLis/LabRequest/GetlistJson",
        data: {
            requestDate: requestDate
        },
        dataType: "json",
        async: false,
        success: function (data) {
            requestRecords = data;
        }
    });
}

function requestDateChanged(e) {
    if (e.el.id == 'requestDate') {
        queryRecords(e.el.realValue);
        fillTable();
    }
}

//重新填充表格
function fillTable() {
    var filterList = []
    var requestStatus = $("#txt_condition .dropdown-text").attr('data-value');
    if (requestStatus == null || requestStatus == '') {
        filterList = requestRecords;
    } else {
        filterList = $.grep(requestRecords, function (v) {
            return v.status == requestStatus
        });
    }
    var txt_keyword = $("#txt_keyword").val();
    if (txt_keyword != null && txt_keyword != '') {
        filterList = $.grep(requestRecords, function (v) {
            return v.name.indexOf(txt_keyword) >= 0 || v.dialysisNo == txt_keyword || v.patientNo == txt_keyword || v.recordNo == txt_keyword;
        });
    }


    $("#gridList").jqGrid('clearGridData').setGridParam({ data: filterList }).trigger('reloadGrid');
}

function byage(strBirthday) {
    var returnAge;
    var strBirthdayArr = strBirthday.split("-");
    var birthYear = strBirthdayArr[0];
    var birthMonth = strBirthdayArr[1];
    var birthDay = strBirthdayArr[2];
    var d = new Date();
    var nowYear = d.getFullYear();
    var nowMonth = d.getMonth() + 1;
    var nowDay = d.getDate();
    if (nowYear == birthYear) {
        returnAge = 0;//同年 则为0岁  
    }
    else {
        var ageDiff = nowYear - birthYear; //年之差  
        if (ageDiff > 0) {
            if (nowMonth == birthMonth) {
                var dayDiff = nowDay - birthDay;//日之差  
                if (dayDiff < 0) {
                    returnAge = ageDiff - 1;
                }
                else {
                    returnAge = ageDiff;
                }
            }
            else {
                var monthDiff = nowMonth - birthMonth;//月之差  
                if (monthDiff < 0) {
                    returnAge = ageDiff - 1;
                }
                else {
                    returnAge = ageDiff;
                }
            }
        }
        else {
            returnAge = -1;//返回-1 表示出生日期输入错误 晚于今天  
        }
    }
    return returnAge;//返回周岁年龄
}
