var currentId = null;//当前患者ID
var accessId = null;//血管通路主键ID
var blwsId = null;//病历主键ID
var bleditor = null;//病历编辑器
var drugList = [];//药品刷选字典
var drugsDict = [];//药品完整字典
var heparinList = [];//肝素字典
$(function () {
    $('.wrapper').height($(window).height() - 50);
    initControl();

    $("#btn-choose").on('click', function (e) {
        $.modalOpen({
            id: "Choose",
            title: "选择患者",
            url: "/PatientManage/Patient/Choose?keyValue=" + currentId,
            width: "800px",
            height: "600px",
            callBack: function (iframeId) {
                top.frames[iframeId].submitForm();
            }
        });
    });
});
function initControl() {
    $("ul.nav.nav-tabs").on('shown.bs.tab', function (e) {
        //console.log('2', e, e.target.innerText);
        if (!!currentId) {
            refresh();
        }
    });
    //1、透析参数
    $("#btn-addtxcs").on('click', function (e) {
        if (!!currentId) {
            var selectedValues = {}
            selectedValues.pid = currentId
            selectedValues.keyValue = '';
            $.modalOpen({
                id: "Form",
                title: "新建透析参数",
                url: "/PatientManage/Setting/Form?keyword=" + encodeURI(JSON.stringify(selectedValues)),
                width: "800px",
                height: "600px",
                callBack: function (iframeId) {
                    top.frames[iframeId].submitData();
                }
            });
        }
    });
    $("#btn-addmodeltxcs").on('click', function (e) {
        if (!!currentId) {
            $.submitForm({
                url: "/PatientManage/Setting/CopyModel",
                param: { keyValue: currentId },
                success: function () {
                    settxcs();
                }
            });
        }
        event.preventDefault();
    });
    //2、血管通路
    $("#access-list").height($(window).height() - 170);
    $("#B_Type").bindSelect();
    $("#B_VascularAccess").bindSelect();
    $("#B_BodyPart").bindSelect();
    $("#B_BodyPosition").bindSelect();

    $("#formaccess").height($(window).height() - 190);
    $("#formaccess").find('#accessimg').first().height($(window).height() - 270);

    $("#B_EnabledMark").on("change", function (e) {
        console.log(e)
        if (e.target.checked) {
            $("#B_DisabledRemark").attr("style", "display: none;")
        } else {
            $("#B_DisabledRemark").attr("style", "display: block;")
        }
    });
    $("#btn-uploadaccess").on('click', function (e) {
        if (!!accessId) {
            $.modalOpen({
                id: "Form",
                title: "上传图片",
                url: "/PatientManage/VascularAccess/Form?keyValue=" + accessId,
                width: "800px",
                height: "600px",
                callBack: function (iframeId) {
                    top.frames[iframeId].submitForm();
                }
            });
        }
    })
    $("#btn-addaccess").on('click', function (e) {
        if (!!currentId) {
            var formdata = {}
            formdata.B_Id = "";
            formdata.B_OperateTime = "";
            formdata.B_FirstUseTime = "";
            formdata.B_Type = "";
            formdata.B_VascularAccess = "";
            formdata.B_BloodSpeed = null;
            formdata.B_BloodSpeed_Idea = null;
            formdata.B_BodyPart = "";
            formdata.B_BodyPosition = "";
            formdata.B_DisabledRemark = "";
            formdata.B_Pid = "";
            formdata.B_Memo = "";
            formdata.B_Complication = "";
            $("#formaccess").formSerialize(formdata);
        } else {
            $.modalMsg('请选择患者！', 'warning');
        }
        //重置主键
        accessId = '';
    });
    $("#btn-saveaccess").on('click', function (e) {
        if (!!currentId) {
            if (!$('#formaccess').formValid()) {
                return false;
            }
            var formdata = {}
            formdata.F_Id = accessId;
            formdata.F_OperateTime = $("#B_OperateTime").val();
            formdata.F_FirstUseTime = $("#B_FirstUseTime").val();
            formdata.F_Type = $("#B_Type").val();
            formdata.F_VascularAccess = $("#B_VascularAccess").val();
            formdata.F_BloodSpeed = $("#B_BloodSpeed").val();
            formdata.F_BloodSpeed_Idea = $("#B_BloodSpeed_Idea").val();
            formdata.F_BodyPart = $("#B_BodyPart").val();
            formdata.F_BodyPosition = $("#B_BodyPosition").val();
            formdata.F_EnabledMark = $("#B_EnabledMark").prop('checked');
            formdata.F_DisabledRemark = $("#B_DisabledRemark").val();
            formdata.F_Pid = currentId;
            formdata.F_Memo = $("#B_Memo").val();
            formdata.F_Complication = $("#B_Complication").val();

            $.submitForm({
                url: "/PatientManage/VascularAccess/SubmitForm",
                param: {
                    entity: formdata,
                    keyValue: accessId
                },
                success: function () {
                    setaccess();
                }
            })

            //$("#formaccess").formSerialize(formdata);
        } else {
            $.modalMsg('请选择患者！', 'warning');
        }
    });
    $("#btn-deleteaccess").on('click', function (e) {
        if (!!accessId) {
            $.deleteForm({
                url: "/PatientManage/VascularAccess/DeleteForm",
                param: { keyValue: accessId },
                success: function () {
                    setaccess();
                }
            });
            $("#btn-addaccess").trigger('click');
        } else {
            $.modalMsg('请选择记录！', 'warning');
        }
    });
    //3、穿刺记录
    $('#ccjlimg').first().height($(window).height() - 185);
    $('#ccjl-list').first().height($(window).height() - 130);
    $("#btn-uploadccjl").on('click', function () {
        if (!!currentId) {
            $.modalOpen({
                id: "Form",
                title: "上传图片",
                url: "/PatientManage/Puncture/Upload?keyValue=" + currentId,
                width: "800px",
                height: "600px",
                callBack: function (iframeId) {
                    top.frames[iframeId].submitForm();
                    $.ajax({
                        url: "/PatientManage/Puncture/GetPuncturePicPath",
                        data: { keyValue: currentId },
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            if (data.fileName) {
                                $("#ccjlimg").attr('src', '\\' + data.filePath + '\\' + data.fileName);
                                $("#ccjlimg").attr('alt', data.fileName);
                            } else {
                                $("#ccjlimg").attr('src', '');
                                $("#ccjlimg").attr('alt', '');
                            }
                        }
                    });

                }
            });
        }
    });
    $("#btn-addccjl").on('click', function () {
        if (!!currentId) {
            var selectedValues = {}
            selectedValues.pid = currentId
            selectedValues.id = ""
            $.modalOpen({
                id: "Form",
                title: "新增",
                url: "/PatientManage/Puncture/Form?keyword=" + encodeURI(JSON.stringify(selectedValues)),
                width: "800px",
                height: "600px",
                callBack: function (iframeId) {
                    top.frames[iframeId].submitData();
                }
            });
        }
    });
    //4、病历文书
    //$("#bl-content").width = $("#bl-content").parent().width() - 460;
    $("#bl-list").height($(window).height() - 230);
    //$("#bl-editor")
    bleditor = new Simditor({
        textarea: $('#bl-editor'),
        toolbar: ['title', 'bold', 'italic', 'underline', 'strikethrough', 'fontScale', '|', 'ol', 'ul', 'blockquote', 'table', 'hr', '|', 'indent', 'outdent', 'alignment'],
        toolbarFloat: true
    });
    $("#bl-container").height($(window).height() - 145);
    $("#btn-bladd").on('click', function () {
        if (!!currentId) {
            blwsId = '';
            $("#bl-contenttime").val(new Date().Format('yyyy-MM-dd HH:mm:ss'));
            $("#bl-title").val('');
            bleditor.setValue('');
        }
    });
    $("#btn-blsave").on('click', function () {
        if (!!currentId) {
            //blwsId = '';
            //$("#bl-contenttime").val(new Date().Format('yyyy-MM-dd HH:mm:ss'));
            //$("#bl-title").val('');
            //bleditor.setValue('');
            var contentTime = $("#bl-contenttime").val();
            if (contentTime === '' || contentTime === null) {
                $.modalMsg('请填写文档时间', "warn");
                return false;
            }
            var contentTitile = $("#bl-title").val();
            if (contentTitile === '' || contentTitile === null) {
                $.modalMsg('请填写文档标题', "warn");
                return false;
            }
            var blContent = $('<div>').text(bleditor.getValue()).html();
            if (blContent === '' || blContent === null) {
                $.modalMsg('请填写病历正文', "warn");
                return false;
            }
            $.submitForm({
                url: "/PatientManage/MedicalRecord/SubmitForm",
                param: {
                    entity: {
                       F_Pid: currentId,
                       F_Title: contentTitile,
                       F_Content: blContent,
                       F_ContentTime: contentTime
                    },
                    keyValue: blwsId
                },
                success: function () {
                    setblws();
                    $("#btn-bladd").trigger('click');
                }
            })
        }
    });
    $("#btn-bldelete").on('click', function () {
        if (!!blwsId) {
            $.deleteForm({
                url: "/PatientManage/MedicalRecord/DeleteForm",
                param: { keyValue: blwsId },
                success: function () {
                    setblws();
                    $("#btn-bladd").trigger('click');
                }
            });
        }
    });
    $("#btn-blquery").on('click', function () {
        if (!!currentId) {
            setblws();
            $("#btn-bladd").trigger('click');
        }
    });
    $("#btn-blprint").on('click', function () {
        if (!!currentId) {
            $.modalOpen({
                id: "Form",
                title: "打印病历",
                url: "/PatientManage/MedicalRecord/Print?keyValue=" + currentId,
                width: "950px",
                height: "800px",
                btn: null
            });
        }
    });
    //5、药品医嘱
    var todayStr = new Date().Format('yyyy-MM-dd');
    $("#yz-startdate,#yz-enddate").val(todayStr);
    $("#yz-list").height($(window).height() - 230 - 175);
    $.ajax({
        url: "/PatientManage/Drugs/GetListJson",
        dataType: "json",
        async: false,
        success: function (data) {
            drugsDict = data;
            drugList = [];
            drugList.push({ id: '', text: '请选择药品' });
            heparinList = [];
            heparinList.push({ id: '', text: '请选择药品' });
            $.each(data, function (index, value) {
                var Name = value.F_DrugName + '(' + value.F_DrugSpell + ')';
                var Id = value.F_Id;
                drugList.push({ id: Id, text: Name });
                if (value.F_IsHeparin == true) {
                    heparinList.push({ id: Id, text: Name });
                }
            });

            

        }
    });
    $("#yz-newdata").find('select').not('#YZ_OrderCode').each(function (index, item) {
        $(item).bindSelect();
    });
    $("#yz-newdata").find('[data-toggle="tooltip"]').tooltip({
        html: true
    }).on('show.bs.tooltip', function () {
        var msg = '...';
        var selectDrugId = $('#YZ_OrderCode').val();
        if (!!selectDrugId) {
            //var findDrug = drugsDict.filter(t => t.F_Id == selectDrugId)[0];
            $.each(drugsDict, function (i, t) {
                if (t.F_Id == selectDrugId) {
                    var findDrug = t;
                    msg = '\
                            <div align="left">\
                                <p><strong>规格:</strong>&nbsp;&nbsp;'+ (findDrug.F_DrugSpec == null ? '' : findDrug.F_DrugSpec) + '</p>\
                                <p><strong>单位:</strong>&nbsp;&nbsp;'+ (findDrug.F_DrugUnit == null ? '' : findDrug.F_DrugUnit) + '</p>\
                                <p><strong>小剂量:</strong>&nbsp;&nbsp;'+ (findDrug.F_DrugMiniAmount == null ? '' : findDrug.F_DrugMiniAmount) + '</p>\
                                <p><strong>小规格:</strong>&nbsp;&nbsp;'+ (findDrug.F_DrugMiniSpec == null ? '' : findDrug.F_DrugMiniSpec) + '</p>\
                                <p><strong>单价:</strong>&nbsp;&nbsp;'+ (findDrug.F_Charges == null ? '' : findDrug.F_Charges) + '</p>\
                                <p><strong>厂商:</strong>&nbsp;&nbsp;'+ (findDrug.F_DrugSupplier == null ? '' : findDrug.F_DrugSupplier) + '</p>\
                            </div>\
                            ';
                    return false;
                }
            });
        }
        $('[data-toggle="tooltip"]').attr('data-original-title', msg);
    });
    $("#YZ_OrderCode").bindSelect({
        data: drugList,
        search: true,
        change: function (e) {
            if (!!e.id) {
                var $jlTarget = $($("#YZ_OrderCode").parent()[0].nextElementSibling);
                //var drugInfo = drugsDict.filter(t => t.F_Id == e.id)[0];
                $.each(drugsDict, function (i, t) {
                    if (t.F_Id == e.id) {
                        $jlTarget.find('input').val(t.F_DrugMiniAmount);
                        $jlTarget.find('span').html(t.F_DrugMiniSpec);
                        $("#YZ_OrderAdministration").val(t.F_DrugAdministration);
                        return false;
                    }
                });

            }
        }
    })
    $("#yz-newdata").find('i.fa').on('click', function (e) {
        var $target = $(e.currentTarget);
        if ($target.is('.fa-plus')) {
            $("#yz-newdata").append('\
                                <div class="row" style="margin-top: 3px;">\
                                    <div class="col-md-3 col-md-push-2" style="padding-left: 1px;padding-right: 1px;">\
                                        <select type="text" class="form-control required"></select>\
                                    </div>\
                                    <div class="col-md-2 col-md-push-2" style="padding-left: 1px;padding-right: 1px;">\
                                        <div class="input-group">\
                                            <input type="number" class="form-control" placeholder="剂量" aria-describedby="drug-unit">\
                                            <span class="input-group-addon" id="drug-unit">u</span>\
                                        </div>\
                                    </div>\
                                </div>\
                                ');
            var lastrow = $("#yz-newdata").find('div.row').last();
            lastrow.find('select').bindSelect({
                data: drugList,
                search: true,
                change: function (e) {
                    if (!!e.id) {
                        //var drugInfo = drugsDict.filter(t => t.F_Id == e.id)[0];
                        var $jlTarget = $(lastrow.find('div.input-group')[0]);
                        $.each(drugsDict, function (i, t) {
                            if (t.F_Id == e.id) {
                                $jlTarget.find('input').val(t.F_DrugMiniAmount);
                                $jlTarget.find('span').html(t.F_DrugMiniSpec);
                                //$jlTarget.find('input').val(t.F_DrugMiniAmount);
                                //$jlTarget.find('span').html(t.F_DrugMiniSpec);
                                return false;
                            }
                        });

                    }
                }
            });
            //console.log(lastrow);
        }
    });
    $("#btn-yzsave").on('click', function () {
        if (!!currentId) {
            var postData = {};
            postData.isTemporary = $("#YZ_IsTemporary").val() === 'true' ? true : false;
            postData.orderStartTime = $("#YZ_OrderStartTime").val();
            postData.orderFrequency = $("#YZ_OrderFrequency").val();
            postData.orderAdministration = $("#YZ_OrderAdministration").val();
            postData.pid = currentId;
            postData.items = [];
            var rows = $("#yz-newdata").find('div.row');
            $.each(rows, function (index, ele) {
                if (index === 0) { //第一行
                    var orderCode = $(ele.children[1]).find('select').val();
                    var orderAmount = $(ele.children[2]).find('input').val();
                    if (!!orderCode) {
                        postData.items.push({ orderCode: orderCode, orderAmount: orderAmount });
                    }
                }
                else {
                    var orderCode = $(ele.children[0]).find('select').val();
                    var orderAmount = $(ele.children[1]).find('input').val();
                    if (!!orderCode) {
                        postData.items.push({ orderCode: orderCode, orderAmount: orderAmount });
                    }
                }
            });
            if (postData.items.length > 0) {
                //保存医嘱信息
                //$.submitForm({
                //    url: "/PatientManage/Orders/SubmitDrugOrder",
                //    param: postData,
                //    success: function () {
                //        setypyz();
                //        //删除新增行，清空首行数据
                //        $.each($("#yz-newdata").find('div.row'), function (index, ele) {
                //            if (index === 0) { //第一行
                //                $(ele.children[1]).find('select').val('').trigger('change');
                //                $(ele.children[2]).find('input').val('');
                //            } else {
                //                $(ele).remove();
                //            }
                //        });
                //    }
                //});
                $.ajax({
                    url: "/PatientManage/Orders/SubmitDrugOrder",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    async: false,
                    type: 'post',
                    contentType: 'application/json',
                    success: function () {
                        setypyz();
                        //删除新增行，清空首行数据
                        $.each($("#yz-newdata").find('div.row'), function (index, ele) {
                            if (index === 0) { //第一行
                                $(ele.children[1]).find('select').val('').trigger('change');
                                $(ele.children[2]).find('input').val('');
                            } else {
                                $(ele).remove();
                            }
                        });
                    }
                });
            }
        }
    });
    $("#btn-yzquery").on('click', function () {
        if (!!currentId) {
            setypyz();
        }
    });
    $("#btn-yzaudit").on('click', function () {
        if (!!currentId) {
            $.modalConfirm("注：您确定要【提交】全部医嘱吗？", function (r) {
                if (r) {
                    $.submitForm({
                        url: "/PatientManage/Orders/BatchAuditDrugOrder",
                        param: {
                            patientId: currentId,
                            startDate: $("#yz-startdate").val(),
                            endDate: $("#yz-enddate").val()
                        },
                        success: function () {
                            setypyz();
                        }
                    });
                }
            });
        }
    });
    //6、检验申请
    $('#btn-daquery').on('click', function () {
        //$.submitForm({
        //    url: "/DaLabLis/DaLab/GetDaResult",
        //    param: {
        //        startDate: $('#da-startdate').val(),
        //        endDate: $('#da-enddate').val()
        //    },
        //    success: function () {
        //        //setypyz();
        //        console.log('aaaaa');
        //    }
        //});
    });

    //7.转归记录
    
    //8.月小节
    $("#yxjContent").height($(window).height() - 150);
    $.ajax({
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
            $("#H_HDDialyzerType").bindSelect({
                data: tags
            });
            $("#H_HFDialyzerType").bindSelect({
                data: tags
            });
            $("#H_HDFDialyzerType").bindSelect({
                data: tags
            });
            $("#H_HDHPDialyzerType").bindSelect({
                data: tags
            });
        }
    });
    $("#H_VascularAccess").bindSelect();
    $("#H_AccessName").bindSelect();
    $("#H_HeparinType").bindSelect({
        data: heparinList,
        search: true,
        change: function (e) {
            if (!!e.id) {
                $.each(drugsDict, function (i, t) {
                    if (t.F_Id == e.id) {
                        $('#H_HeparinUnit').html(t.F_DrugMiniSpec);
                        return false;
                    }
                });

            }
        }
    });
    $("#yxj-query").click(function () {
        if (!!currentId) {
            var month = $("#yxj-month").val();
            if (month == null || month == '') {
                month = new Date().Format('yyyy-MM');
                $("#yxj-month").val(month);
            } else {
                month = month.substr(0, 7);
            }
            $.ajax({
                url: "/PatientManage/Patient/GetMonthlySummaryJson",
                data: {
                    patientId: currentId,
                    month: month
                },
                dataType: "json",
                async: false,
                success: function (data) {
                    //console.log(data);
                    var dataConvert = JSON.parse(JSON.stringify(data).replace(/F_/g, 'H_',));
                    console.log(data, dataConvert);
                }
            });
        }
    });
}
//更新抬头
function resetTitle(keyValue) {
    if (!!keyValue) {
        $.ajax({
            url: "/PatientManage/Patient/GetSinglePatient",
            data: { keyValue: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                currentId = data.F_Id;
                $("#patientName").html(data.F_Name);
                if (data.F_Gender === '男') {
                    $("#patientGender").removeClass().addClass('fa').addClass('fa-male');
                } else if (data.F_Gender === '女') {
                    $("#patientGender").removeClass().addClass('fa').addClass('fa-female');
                } else {
                    $("#patientGender").removeClass();
                }
                $("#patientAge").html(data.F_AgeStr);
                $("#dialysisNo").html(data.F_DialysisNo);
                if (data.F_BeInfected === true) {
                    $("#beInfected").removeClass().addClass('fa').addClass('fa-star');
                } else {
                    $("#beInfected").removeClass();
                }
                refresh();
            }
        });
    }
}
//刷新明细数据
function refresh() {
    var currentTabName = $('ul.nav.nav-tabs').find('li.active')[0].innerText;
    if (currentTabName === '透析参数') {
        settxcs();
    } else if (currentTabName === '血管通路') {
        accessId = '';
        setaccess();
    } else if (currentTabName === '穿刺记录') {
        setccjl();
    } else if (currentTabName === '病历文书') {
        blwsId = '';
        setblws();
    }
    else if (currentTabName === '医嘱用药') {
        setypyz();
    }
    else if (currentTabName === '转归记录') {
        settransfer();
    }
}
//刷新透析参数内容
function settxcs() {
    $.ajax({
        url: "/PatientManage/Setting/GetList",
        data: { keyword: currentId },
        dataType: "json",
        async: false,
        success: function (data) {
            var htmlstr = '';
            $.each(data, function (index, item) {
                htmlstr = htmlstr + '\
                    <div class="col-xs-10 col-sm-9 col-md-7 col-lg-5">\
                        <div class="panel panel-default" id="' + item.F_Id + '">\
                            <div class="panel-heading">\
                                <strong style="font-size:15px;">' + item.F_DialysisType + '</strong>\
                                <div style="float: right;">\
                                    <i class="fa fa-edit fa-lg" style="padding-right: 10px;font-size: 18px;"></i>\
                                    <i class="fa fa-trash fa-lg" style="padding-right: 10px;font-size: 18px;"></i>\
                                </div>\
                            </div>\
                            <div class="panel-body">\
                                <div class="rows">\
                                    <div style="float: left;width: 49%;">\
                                        <strong>血管通路&nbsp;&nbsp;</strong>&nbsp;&nbsp;' + (item.F_VascularAccess === null ? '&nbsp;&nbsp;&nbsp;' : item.F_VascularAccess) + '\
                                    </div>\
                                    <div style="float: right;width: 50%;">\
                                        <strong>部位(名称)&nbsp;&nbsp;</strong>&nbsp;&nbsp;' + (item.F_AccessName === null ? '&nbsp;&nbsp;&nbsp;' : item.F_AccessName) + '\
                                    </div>\
                                </div>\
                                <div class="rows">\
                                    <div style="float: left;width: 49%;">\
                                        <strong>抗凝剂&nbsp;&nbsp;</strong>&nbsp;&nbsp;' + (item.F_HeparinTypeName === null ? '无' : item.F_HeparinTypeName) + '\
                                    </div>\
                                    <div style="float: right;width: 25%;">\
                                        <strong>追加&nbsp;&nbsp;</strong>&nbsp;&nbsp;' + (item.F_HeparinAddAmount === null ? '&nbsp;&nbsp;&nbsp;' : item.F_HeparinAddAmount + item.F_HeparinAddSpeedUnit) + '\
                                    </div>\
                                    <div style="float: right;width: 25%;">\
                                        <strong>首剂&nbsp;&nbsp;</strong>&nbsp;&nbsp;' + (item.F_HeparinAmount === null ? '&nbsp;&nbsp;&nbsp;' : item.F_HeparinAmount + item.F_HeparinUnit) + '&nbsp;\
                                    </div>\
                                </div>\
                                <div class="rows">\
                                    <div style="float: left;width: 49%;">\
                                        <strong>透析器&nbsp;&nbsp;</strong>&nbsp;&nbsp;' + item.F_DialyzerTypeName1 + '\
                                    </div>\
                                    <div style="float: right;width: 50%;">\
                                        <strong>灌流器&nbsp;&nbsp;</strong>&nbsp;&nbsp;' + item.F_DialyzerTypeName2 + '\
                                    </div>\
                                </div>\
                                <div class="rows">\
                                    <div style="float: left;width: 49%;">\
                                        <strong>血流速&nbsp;&nbsp;</strong>&nbsp;&nbsp;' + (item.F_BloodSpeed === null ? '&nbsp;&nbsp;&nbsp;' : item.F_BloodSpeed) + '&nbsp;ml/min\
                                    </div>\
                                    <div style="float: right;width: 25%;">\
                                        <strong>透析液温度&nbsp;&nbsp;</strong>&nbsp;&nbsp;' + (item.F_DialysateTemperature === null ? '&nbsp;&nbsp;&nbsp;' : item.F_DialysateTemperature) + '&nbsp;℃\
                                    </div>\
                                    <div style="float: right;width: 25%;">\
                                        <strong>预估时间&nbsp;&nbsp;</strong>&nbsp;&nbsp;' + (item.F_EstimateHours === null ? '&nbsp;&nbsp;&nbsp;' : item.F_EstimateHours) + '&nbsp;h\
                                    </div>\
                                </div>\
                                <div class="rows">\
                                    <div style="float: left;width: 19%;">\
                                        <strong>透析液&nbsp;&nbsp;</strong>\
                                    </div>\
                                    <div style="float: right;width: 20%;">\
                                        <strong>HCO<sub>3</sub><sup>-</sup>&nbsp;&nbsp;</strong>&nbsp;&nbsp;' + (item.F_Hco3 === null ? '&nbsp;&nbsp;&nbsp;' : item.F_Hco3) + '&nbsp;\
                                    </div>\
                                    <div style="float: right;width: 20%;">\
                                        <strong>Na&nbsp;&nbsp;</strong>&nbsp;&nbsp;' + (item.F_Na === null ? '&nbsp;&nbsp;&nbsp;' : item.F_Na) + '&nbsp;\
                                    </div>\
                                    <div style="float: right;width: 20%;">\
                                        <strong>K&nbsp;&nbsp;</strong>&nbsp;&nbsp;' + (item.F_K === null ? '&nbsp;&nbsp;&nbsp;' : item.F_K) + '&nbsp;\
                                    </div>\
                                    <div style="float: right;width: 20%;">\
                                        <strong>Ca&nbsp;&nbsp;</strong>&nbsp;&nbsp;' + (item.F_Ca === null ? '&nbsp;&nbsp;&nbsp;' : item.F_Ca) + '&nbsp;\
                                    </div>\
                                </div>\
                            </div>\
                            <div class="panel-footer">\
                                <strong>日期：' + item.F_CreatorTime + '</strong>\
                                <strong style="float: right;margin-right: 10px;">签名：' + item.F_DoctorName + '</strong>\
                                <i style="float: right;margin-right: 30px;font-size: 18px;" class="fa fa-star-o fa-lg"></i>\
                            </div>\
                        </div>\
                    </div>\
                    ';
            }
            );
            var $container = $('#a').find('div.rows').first();
            $container.empty();
            $container.html('');
            $container.html(htmlstr);
            if (data.length > 0) {
                $container.find('i.fa').on('click', function (e) {
                    var target = $(e.currentTarget);
                    var parentdiv = target.parent().parent();
                    if (target.is('.fa-trash')) {
                        parentdiv = parentdiv.parent();
                        $.modalConfirm("注：您确定要【删除】该项吗？", function (r) {
                            if (r) {
                                $.submitForm({
                                    url: "/PatientManage/Setting/DeleteForm",
                                    param: { keyValue: parentdiv[0].id },
                                    success: function () {
                                        settxcs();
                                    }
                                });
                            }
                        });
                    } else if (target.is('.fa-edit')) {
                        parentdiv = parentdiv.parent();
                        var selectedValues = {}
                        selectedValues.pid = currentId
                        selectedValues.keyValue = parentdiv[0].id;
                        $.modalOpen({
                            id: "Form",
                            title: "修改透析参数",
                            url: "/PatientManage/Setting/Form?keyValue=" + encodeURI(JSON.stringify(selectedValues)),
                            width: "800px",
                            height: "600px",
                            callBack: function (iframeId) {
                                top.frames[iframeId].submitData();
                            }
                        });
                    } else if (target.is('.fa-star-o')) {
                        $.modalConfirm("注：您确定要保存该项为模板吗？", function (r) {
                            if (r) {
                                $.submitForm({
                                    url: "/PatientManage/Setting/SaveModel",
                                    param: { keyValue: parentdiv[0].id },
                                    success: function () {
                                    }
                                });
                            }
                        });
                    }
                });
            }
        }
    });
}
//刷新血管通路内容
function setaccess() {
    $.ajax({
        url: "/PatientManage/VascularAccess/GetListJson",
        data: { keyword: currentId },
        dataType: "json",
        async: false,
        success: function (data) {
            var listgroup = $("#access-list");
            listgroup.html("")
            if (data.length > 0) {
                var strtemp = ""
                data.forEach(function (ele, index) {
                    strtemp = strtemp + "<a class=\"list-group-item\" id=\"" + ele.F_Id + "\"> " + (ele.F_EnabledMark === true ? "<span class=\"badge badge-success\">启</span>" : "<span class=\"badge\">停</span>") + "<h6 class=\"list-group-item-heading\">" + new Date(ele.F_OperateTime).Format("yyyy-MM-dd") + "</h6><p class=\"list-group-item-text\">" + ele.F_Type + "&nbsp;&nbsp;&nbsp;" + ele.F_VascularAccess + "&nbsp;&nbsp;&nbsp;" + ele.F_BodyPart + "</p></a>";
                })
                listgroup.html(strtemp)
                //添加点击事件
                listgroup.find(".list-group-item").on("click", function (e) {
                    if (!!e.currentTarget.id) {
                        accessId = e.currentTarget.id
                        $.ajax({
                            url: "/PatientManage/VascularAccess/GetFormJson",
                            data: { keyValue: accessId },
                            dataType: "json",
                            async: false,
                            success: function (data) {
                                var formdata = {}
                                formdata.B_Id = data.F_Id;
                                formdata.B_OperateTime = data.F_OperateTime;
                                formdata.B_FirstUseTime = data.F_FirstUseTime;
                                formdata.B_Type = data.F_Type;
                                formdata.B_VascularAccess = data.F_VascularAccess;
                                formdata.B_BloodSpeed = data.F_BloodSpeed;
                                formdata.B_BloodSpeed_Idea = data.F_BloodSpeed_Idea;
                                formdata.B_BodyPart = data.F_BodyPart;
                                formdata.B_BodyPosition = data.F_BodyPosition;
                                formdata.B_DisabledRemark = data.F_DisabledRemark;
                                formdata.B_Pid = data.F_Pid;
                                formdata.B_Memo = data.F_Memo;
                                formdata.B_Complication = data.F_Complication;
                                $("#formaccess").formSerialize(formdata);

                                $.ajax({
                                    url: "/PatientManage/VascularAccess/GetVascularAccessPicPath",
                                    data: { keyValue: accessId },
                                    dataType: "json",
                                    async: false,
                                    success: function (data) {
                                        if (!!data.fileName) {
                                            $("#accessimg").attr('src', '\\' + data.filePath + '\\' + data.fileName);
                                            $("#accessimg").attr('alt', data.fileName);
                                        } else {
                                            $("#accessimg").attr('src', '');
                                            $("#accessimg").attr('alt', '');
                                        }
                                    }
                                });
                            }
                        });
                    }
                })
            }
        }
    });
}
//刷新穿刺记录
function setccjl() {
    $.ajax({
        url: "/PatientManage/Puncture/GetPuncturePicPath",
        data: {
            keyValue: currentId
        },
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.fileName) {
                $("#ccjlimg").attr('src', '\\' + data.filePath + '\\' + data.fileName);
                $("#ccjlimg").attr('alt', data.fileName);
            }
            else {
                $("#ccjlimg").attr('src', '');
                $("#ccjlimg").attr('alt', '');
            }
        }
    });
    $.ajax({
        url: "/PatientManage/Puncture/GetListJson",
        data: { keyValue: currentId },
        dataType: "json",
        async: false,
        success: function (data) {
            var $target = $("#ccjl-list");
            $target.empty();
            $target.html('');
            var htmlstr = '';
            if (data.length > 0) {
                data.forEach(function (value) {
                    if (value.F_IsSuccess === true) {
                        htmlstr = htmlstr + '\
                            <div class="col-xs-6 col-sm-5 col-md-4 col-lg-3">\
                                <div class="panel panel-default">\
                                    <div class="panel-heading">\
                                        <strong style="font-size:15px;">'+ value.F_OperateTime.substr(0, 10) + '</strong>\
                                        <div style="float: right;" id="'+ value.F_Id + '">\
                                            <i class="fa fa-edit fa-lg" style="padding-right: 10px;font-size: 18px;"></i>\
                                            <i class="fa fa-trash fa-lg" style="padding-right: 10px;font-size: 18px;"></i>\
                                        </div>\
                                    </div>\
                                    <div class="panel-body" style="margin-top: -5px;margin-bottom: -5px;">\
                                        <strong>穿刺点：&nbsp;</strong>&nbsp;' + value.F_Point1 + '&nbsp;-&nbsp;' + value.F_Point2 + '&nbsp;&nbsp;&nbsp;&nbsp;' + value.F_OperateTime.substr(11, 5) + '\
                                    </div>\
                                    <div class="panel-footer">\
                                        <strong>签名：' + value.F_RealName + '</strong>\
                                    </div>\
                                </div>\
                            </div>\
                            ';
                    } else {
                        htmlstr = htmlstr + '\
                            <div class="col-xs-6 col-sm-5 col-md-4 col-lg-3">\
                                <div class="panel panel-default">\
                                    <div class="panel-heading">\
                                        <strong style="font-size:15px;">'+ value.F_OperateTime.substr(0, 10) + '</strong>\
                                        <div style="float: right;" id="'+ value.F_Id + '">\
                                            <i class="fa fa-edit fa-lg" style="padding-right: 10px;font-size: 18px;"></i>\
                                            <i class="fa fa-trash fa-lg" style="padding-right: 10px;font-size: 18px;"></i>\
                                        </div>\
                                    </div>\
                                    <div class="panel-body" style="margin-top: -5px;margin-bottom: -5px;">\
                                        <strong>穿刺点：&nbsp;</strong>&nbsp;' + value.F_Point1 + '&nbsp;-&nbsp;' + value.F_Point2 + '&nbsp;&nbsp;&nbsp;&nbsp;' + value.F_OperateTime.substr(11, 5) + '\
                                    </div>\
                                    <div class="panel-footer">\
                                        <strong>签名：' + value.F_RealName + '</strong>\
                                        <i class="fa fa-circle" style="float: right;margin-right: 8px;font-size: 18px;color: red;" data-toggle="tooltip" data-html="true" data-placement="left" title="' + value.F_Memo + '"></i>\
                                    </div>\
                                </div>\
                            </div>\
                            ';
                    }
                });
                $target.html(htmlstr);
                $target.find("[data-toggle='tooltip']").tooltip();
                //添加修改、删除点击事件
                $target.find('i.fa').on('click', function (e) {
                    var target = $(e.currentTarget);
                    var ccjlid = target.parent()[0].id;
                    if (!!ccjlid) {
                        if (target.is('.fa-trash')) {
                            $.deleteForm({
                                url: "/PatientManage/Puncture/DeleteForm",
                                param: { keyValue: ccjlid },
                                success: function () {
                                    setccjl();
                                }
                            })
                        } else if (target.is('.fa-edit')) {
                            $.modalOpen({
                                id: "Form",
                                title: "修改",
                                url: "/PatientManage/Puncture/Form?keyValue=" + encodeURI(JSON.stringify({ id: ccjlid })),
                                width: "800px",
                                height: "600px",
                                callBack: function (iframeId) {
                                    top.frames[iframeId].submitData();
                                    //setccjl();
                                }
                            });
                        }
                    }
                });
            }
        }
    });
}
//刷新病历记录
function setblws() {
    $.ajax({
        url: "/PatientManage/MedicalRecord/GetListJson",
        data: {
            patientId: currentId,
            startDate: $("#bl-startdate").val(),
            endDate: $("#bl-enddate").val()
        },
        dataType: "json",
        async: false,
        success: function (data) {
            var $target = $("#bl-list");
            $target.empty();
            $target.html('');
            var htmlstr = '';
            if (data.length > 0) {
                data.forEach(function (value) {
                    if (value.auditFlag === true) {
                        htmlstr = htmlstr + '\
                                    <a class="list-group-item" id="' + value.id + '">\
                                        <h5 class="list-group-item-heading">' + value.contentTime + '</h5>\
                                        <strong class="list-group-item-text">' + value.title + '</strong>\
                                        <i class="list-group-item-text" style="float: right;margin-right: 20px;">' + value.userName + '</i>\
                                    </a>\
                                    ';
                    } else {
                        htmlstr = htmlstr + '\
                                    <a class="list-group-item" id="' + value.id + '">\
                                        <h5 class="list-group-item-heading">' + value.contentTime + '</h5>\
                                        <strong class="list-group-item-text">' + value.title + '</strong>\
                                        <i class="fa fa-pencil-square" style="float: right;font-size: 22px;margin-top: -6px;margin-right: 5px;"></i>\
                                        <i class="list-group-item-text" style="float: right;margin-right: 20px;">' + value.userName + '</i>\
                                    </a>\
                                    ';
                    }
                });
                $target.html(htmlstr);
                //添加签名点击事件
                $target.find('i.fa').on('click', function (e) {
                    var target = $(e.currentTarget);
                    var blwsid = target.parent()[0].id;
                    if (!!blwsid) {
                        if (target.is('.fa-pencil-square')) {
                            $.modalConfirm("注：您确定要【提交】该项吗？", function (r) {
                                if (r) {
                                    $.submitForm({
                                        url: "/PatientManage/MedicalRecord/CheckMedicalRecord",
                                        param: { keyValue: blwsid },
                                        success: function () {
                                            setblws();
                                            $("#btn-bladd").trigger('click');
                                        }
                                    });
                                }
                            });
                            event.stopPropagation();
                        }
                    }
                });
                //添加点击事件
                $target.find(".list-group-item").on("click", function (e) {
                    if (!!e.currentTarget.id) {
                        blwsId = e.currentTarget.id
                        $.ajax({
                            url: "/PatientManage/MedicalRecord/GetFormJson",
                            data: { keyValue: blwsId },
                            dataType: "json",
                            async: false,
                            success: function (data) {
                                $("#bl-contenttime").val(data.F_ContentTime);
                                $("#bl-title").val(data.F_Title);
                                bleditor.setValue($('<div>').html(data.F_Content).text());
                            }
                        });
                    }
                })
            }
        }
    });
}
//刷新药品医嘱信息
function setypyz() {
    $.ajax({
        url: "/PatientManage/Orders/GetDrugsListJson",
        data: {
            patientId: currentId,
            startDate: $("#yz-startdate").val(),
            endDate: $("#yz-enddate").val()
        },
        dataType: "json",
        async: false,
        success: function (data) {
            var $target = $("#yz-list");
            $target.empty();
            $target.html('');
            var htmlstr = '';
            if (data.length > 0) {
                data.forEach(function (value) {
                    htmlstr = htmlstr + '\
                        <div class="col-xs-6 col-sm-5 col-md-4 col-lg-3">\
                        ';
                    htmlstr = htmlstr + '\
                        <div class="panel panel-default" id="'+ value.id + '">\
                        <div class="panel-heading">\
                         <strong style="font-size:15px;">'+ value.orderStartTime.substr(5, 11) + '</strong>\
                        ';
                    if (value.isTemporary === true) {
                        htmlstr = htmlstr + '\
                            <span class="badge" style="background-color: red;">长</span>\
                            ';
                    } else {
                        htmlstr = htmlstr + '\
                            <span class="badge">临</span>\
                            ';
                    }
                    htmlstr = htmlstr + '\
                        <div style="float: right;">\
                        '+ (value.isTemporary === true && value.orderStatus >= 1 && value.orderStatus < 3 ? '\
                        <i class="fa fa-stop-circle fa-lg" style="padding-right: 10px;font-size: 18px;"></i>\
                        ' : '') + '\
                        <i class="fa fa-trash fa-lg" style="padding-right: 10px;font-size: 18px;"></i>\
                        </div>\
                        ';
                    htmlstr = htmlstr + '\
                        </div>\
                        ';
                    htmlstr = htmlstr + '\
                        <div class="panel-body" style="padding: 2px 2px 1px 10px;">\
                        <div class="rows" style="height: 55px;">\
                        ';
                    $.each(value.rows, function (index, item) {
                        htmlstr = htmlstr + '\
                            <div style="float: left;width: 60%;">\
                            <strong>'+ item.orderText + '</strong>\
                            </div>\
                            <div style="float: right;width: 10%;">\
                            <strong>'+ (index === 0 ? value.orderFrequency : '&nbsp;') + '</strong>\
                            </div>\
                            <div style="float: right;width: 10%;">\
                            <strong>'+ (index === 0 ? (value.orderAdministration === undefined || value.orderAdministration === null ? '&nbsp;' : value.orderAdministration) : '&nbsp;') + '</strong>\
                            </div>\
                            <div style="float: right;width: 20%;">\
                            <strong>'+ item.orderAmount + '&nbsp;' + item.orderUnitSpec + '</strong>\
                            </div>\
                            ';

                    });
                    htmlstr = htmlstr + '\
                        </div>\
                        </div>\
                        ';
                    htmlstr = htmlstr + '\
                        <div class="panel-footer">\
                        ';
                    switch (value.orderStatus) {
                        case 0:
                            htmlstr = htmlstr + '\
                                <span class="badge">新</span>\
                                ';
                            htmlstr = htmlstr + '\
                                <strong style="float: right;margin-right: 10px;">'+ value.doctor + '</strong>\
                                <i style="float: right;margin-right: 30px;font-size: 18px;" class="fa fa-pencil fa-lg"></i>\
                                ';
                            break;
                        case 1:
                            htmlstr = htmlstr + '\
                                <strong>签名:'+ value.doctorAuditTime.substr(5, 11) + '</strong>\
                                <strong style="float: right;margin-right: 10px;">'+ value.doctor + '</strong>\
                                ';
                            break;
                        case 2:
                            htmlstr = htmlstr + '\
                                <strong>签名:'+ value.doctorAuditTime.substr(5, 11) + '</strong>\
                                <strong style="float: right;margin-right: 10px;">'+ value.doctor + '</strong>\
                                ';
                            break;
                        case 3:
                            htmlstr = htmlstr + '\
                                <strong>签名:'+ value.orderStopTime.substr(5, 11) + '</strong>\
                                <span class="badge">停</span>\
                                ';
                            htmlstr = htmlstr + '\
                                <strong style="float: right;margin-right: 10px;">'+ value.doctor + '</strong>\
                                ';
                            break;
                    }
                    htmlstr = htmlstr + '\
                        </div>\
                        ';
                    htmlstr = htmlstr + '\
                        </div>\
                        ';
                    htmlstr = htmlstr + '\
                        </div>\
                        ';
                });
                $target.html(htmlstr);
                //添加签名点击事件
                $target.find('i.fa').on('click', function (e) {
                    var target = $(e.currentTarget);
                    if (target.is('.fa-stop-circle')) {
                        var orderId = target.parent().parent().parent()[0].id;
                        if (!!orderId) {
                            $.modalConfirm("注：您确定要【停止】该项医嘱吗？", function (r) {
                                if (r) {
                                    $.submitForm({
                                        url: "/PatientManage/Orders/StopOrder",
                                        param: { keyValue: orderId },
                                        success: function () {
                                            setypyz();
                                            //$("#btn-bladd").trigger('click');
                                        }
                                    });
                                }
                            });
                        }
                    } else if (target.is('.fa-trash')) {
                        var orderId = target.parent().parent().parent()[0].id;
                        if (!!orderId) {
                            $.deleteForm({
                                url: "/PatientManage/Orders/DeleteForm",
                                param: { keyValue: orderId },
                                success: function () {
                                    setypyz();
                                }
                            });
                        }

                    } else if (target.is('.fa-pencil')) {
                        var orderId = target.parent().parent()[0].id;
                        if (!!orderId) {
                            $.modalConfirm("注：您确定要【提交】该项医嘱吗？", function (r) {
                                if (r) {
                                    $.submitForm({
                                        url: "/PatientManage/Orders/CheckOrder",
                                        param: { keyValue: orderId },
                                        success: function () {
                                            setypyz();
                                        }
                                    });
                                }
                            });
                        }
                    }

                });
                //添加点击事件
                $target.find(".list-group-item").on("click", function (e) {
                    if (!!e.currentTarget.id) {
                        blwsId = e.currentTarget.id
                        $.ajax({
                            url: "/PatientManage/MedicalRecord/GetFormJson",
                            data: { keyValue: blwsId },
                            dataType: "json",
                            async: false,
                            success: function (data) {
                                $("#bl-contenttime").val(data.F_ContentTime);
                                $("#bl-title").val(data.F_Title);
                                bleditor.setValue($('<div>').html(data.F_Content).text());
                            }
                        });
                    }
                })
            }
        }
    });
}

//刷新转归记录
function settransfer() {
    $.ajax({
        url: "/PatientManage/Patient/GetTransferListJson",
        data: {
            patientId: currentId
        },
        dataType: "json",
        async: false,
        success: function (data) {
            var $target = $("#transferContent");
            $target.empty();
            $target.html('');
            var htmlstr = '';
            if ($.isArray(data)) {
                if (data.length > 0) {
                    data.forEach(function (value) {
                        //value.orderStartTime.substr(5, 11)
                        htmlstr = htmlstr + '\
                                <span class="timeline-label">\
                                    <span class="label label-primary">'+ value.F_TransferDate.substr(0, 10) + '</span>\
                                </span>\
                                ';
                        switch (value.F_Status) {
                            case '转入':
                                htmlstr = htmlstr + '\
                                    <div class="timeline-item">\
                                        <div class="timeline-point">\
                                            <i class="fa fa-arrow-circle-right" style="color: greenyellow;"></i>\
                                        </div>\
                                        <div class="timeline-event">\
                                            <div class="timeline-heading">\
                                                <h5>转入<i class="fa fa-times fa-lg" style="float: right;margin-right: 20px;" id="'+ value.F_Id + '"></i></h5>\
                                            </div>\
                                            <div class="timeline-body">\
                                                <p>由&nbsp;'+ (value.F_LocationOut == null ? '' : value.F_LocationOut) + '&nbsp;转入' + (value.F_TransferReason == null ? '' : value.F_TransferReason) + '&nbsp;&nbsp;' + (value.F_Memo == null ? '' : value.F_Memo) + '</p>\
                                            </div>\
                                        </div>\
                                    </div>\
                                    ';
                                break;
                            case '转出':
                                htmlstr = htmlstr + '\
                                    <div class="timeline-item">\
                                        <div class="timeline-point timeline-point-primary">\
                                            <i class="fa fa-arrow-circle-left" style="color: blue;"></i>\
                                        </div>\
                                        <div class="timeline-event timeline-event-primary">\
                                            <div class="timeline-heading">\
                                                <h5>转出<i class="fa fa-times fa-lg" style="float: right;margin-right: 20px;" id="'+ value.F_Id +'"></i></h5>\
                                            </div>\
                                            <div class="timeline-body">\
                                                 <p>'+ (value.F_LocationOut == null ? '' : value.F_LocationOut) + '&nbsp;&nbsp;' + (value.F_TransferReason == null ? '' : value.F_TransferReason) + '&nbsp;&nbsp;' + (value.F_Memo == null ? '' : value.F_Memo) + '</p>\
                                            </div>\
                                        </div>\
                                    </div>\
                                    ';
                                break;
                            case '退出':
                                htmlstr = htmlstr + '\
                                    <div class="timeline-item">\
                                        <div class="timeline-point timeline-point-warning">\
                                            <i class="fa fa-sign-out"></i>\
                                        </div>\
                                        <div class="timeline-event timeline-event-warning">\
                                            <div class="timeline-heading">\
                                                <h5>退出<i class="fa fa-times fa-lg" style="float: right;margin-right: 20px;" id="'+ value.F_Id + '"></i></h5>\
                                            </div>\
                                            <div class="timeline-body">\
                                                <p>'+ (value.F_ExitType == null ? '' : value.F_ExitType) + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + (value.F_ExitReason == null ? '' : value.F_ExitReason) + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + (value.F_Memo == null ? '' : value.F_Memo) + '</p>\
                                            </div>\
                                        </div>\
                                    </div>\
                                    ';
                                break;
                            case '短期':
                                htmlstr = htmlstr + '\
                                    <div class="timeline-item">\
                                        <div class="timeline-point timeline-point-success">\
                                            <i class="fa fa-star"></i>\
                                        </div>\
                                        <div class="timeline-event timeline-event-success">\
                                            <div class="timeline-heading">\
                                                <h5>短期<i class="fa fa-times fa-lg" style="float: right;margin-right: 20px;" id="'+ value.F_Id +'"></i></h5>\
                                            </div>\
                                            <div class="timeline-body">\
                                                <p>临时透析'+ '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + (value.F_Memo == null ? '' : value.F_Memo) +'</p>\
                                            </div>\
                                        </div>\
                                    </div>\
                                    ';
                                break;
                        }
                    });
                }
            }
            htmlstr = htmlstr + '\
                        <span class="timeline-label">\
                            <a class="btn btn-primary fa-lg"><i class="fa fa-plus-circle"></i></a>\
                        </span>\
                        ';
            $target.html(htmlstr);

            $target.find('a.btn').on('click', function () {
                if (!!currentId) {
                    $.modalOpen({
                        id: "Form",
                        title: "新建转归记录",
                        url: "/PatientManage/Patient/Transfer?keyword=" + currentId,
                        width: "600px",
                        height: "500px",
                        callBack: function (iframeId) {
                            top.frames[iframeId].submitForm();
                        }
                    });
                }
            });

            $target.find('i.fa-times').on('click', function (e) {
                //console.log();
                //$.deleteForm({
                //    url: "/PatientManage/Patient/SubmitForm?keyValue=" + e.currentTarget.id,
                //    param: formdata,
                //    success: function () {
                //        settransfer();
                //    }
                //});

                $.deleteForm({
                    url: "/PatientManage/Patient/DeleteTransfer",
                    param: { keyValue: e.currentTarget.id },
                    success: function () {
                        settransfer();
                    }
                });
                
            });

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