//当前日期所有透析记录
var list = [];
//透析记录单Id
var selectid = "";
var currentPid = "";
var pgdId = "";
$(function () {
    $("#visitDate").val(new Date().Format("yyyy-MM-dd"));
    $("#patient-list").css({ "height": $(window).height() - 225 });
    $("#main-content").css({ "height": $(window).height() - 95 });
    $("#img-puncture").css({ "height": $(window).height() - 210 });
    $("#summaryContent").css({ "height": $(window).height() - 210 });
    $("#fileModel-list").css({ "height": $(window).height() - 120 });
    $("ul.nav.nav-tabs").on('shown.bs.tab', function (e) {
        //console.log('2', e, e.target.innerText);
        setForm();
    });
    $("#visitNo").bindSelect();
    $("#groupName").bindSelect({
        url: "/SystemManage/ItemsData/GetSelectJson?enCode=BedGroup",
        id: "id",
        text: "text",
        async: true,
        change: function (e) {
            refreshList();
        }
    });
    $("#visitNo").on("change", function () {
        //console.log("班次改变");
        refreshList();
    });
    //刷新数据 窗口重绘
    $("#btn-refresh").on("click", function () {
        //$("#patient-list").css({ "height": $(window).height() - 180 }); 
        //$("tab-content").css({ "height": $(window).height() - 125 });
        handleVisitDateChange();
    });
    $("#statusinit ,#statusuncomplete, #statuscomplete").on("change", function () {
        refreshList();
    });
    $("#visitNo").val("1");
    handleVisitDateChange();
    $("#patient-list").on("click", function (e) {
        selectid = e.target.id;
        if (selectid === "") {
            selectid = e.target.parentNode.id;
        }
        if (selectid === undefined || selectid === null || selectid === '' || selectid === 'patient-list') {
            selectid = '';
            currentPid = '';
            pgdId = '';
            return false;
        }
        $(".list-group-item.active").removeClass('active');
        $("#" + selectid).addClass('active');
        //var filteritem = list.filter(t => t.Id === selectid);
        //currentPid = filteritem[0].Pid;
        $.each(list, function (index, value) {
            if (value.Id === selectid) {
                currentPid = value.Pid;
                return false;
            }
        });
        setForm();
    });
    initControl();
});
//切换病人 更新form数据
function setForm() {
    if (selectid === '') {
        return false;
    } else {
        var currentAText = $("ul.nav.nav-tabs").find("li.active").find("a").first();
        var currentTabText = currentAText[0].innerText;
        //console.log(currentTabText);
        switch (currentAText[0].innerText) {
            case "透析参数":
                txjlquery();
                break;
            case "透前评估":
                pgjlquery();
                break;
            case "穿刺记录":
                ccjlquery();
                break;
            case "观察记录":
                gcjlquery();
                break;
            case "过程监测":
                gcjcquery();
                break;
            case "疗效评价":
                lxpjquery();
                break;
        }
        //console.log($("#main-content").find("div.tab-pane.active"));
        ////透析记录
        //txjlquery();
        ////评估记录
        //pgjlquery();
        ////穿刺记录
        //ccjlquery();
        ////观察记录
        //gcjlquery();
    }
    function gcjlquery() {
        $.ajax({
            url: "/PatientManage/Nurse/GetListByVisitJson",
            data: { visitId: selectid },
            dataType: "json",
            async: false,
            success: function(data) {
                $("#ob-gridList").jqGrid("clearGridData");
                $("#ob-gridList").setGridParam({ data: data }).trigger('reloadGrid');
            }
        });
    }
    function ccjlquery() {
        $.ajax({
            url: "/PatientManage/Puncture/GetPuncturePicPath",
            data: { keyValue: currentPid },
            dataType: "json",
            async: true,
            success: function(data) {
                if (data.fileName) {
                    $("#img-puncture").attr('src', '\\' + data.filePath + '\\' + data.fileName);
                    $("#img-puncture").attr('alt', data.fileName);
                }
                else {
                    $("#img-puncture").attr('src', '');
                    $("#img-puncture").attr('alt', '');
                }
            }
        });
        $.ajax({
            url: "/PatientManage/Puncture/GetListJson",
            data: { keyValue: currentPid },
            dataType: "json",
            async: false,
            success: function(data) {
                //console.log(data);
                var $target = $("#punctureItems");
                $target.find('tr').remove();
                if (data.length > 0) {
                    data.forEach(function(value) {
                        $target.append('<tr><td>' + value.F_OperateTime + '</td><td>' + value.F_Point1 + ' - ' + value.F_Point2 + '</td><td>' + value.F_RealName + '</td><td>' + value.F_Memo + '</td></tr>');
                    });
                }
            }
        });
    }
    function pgjlquery() {
        $.ajax({
            url: "/PatientManage/Evaluation/GetFormByVisitIdJson",
            dataType: "json",
            data: { keyValue: selectid },
            async: false,
            success: function(formdate) {
                var element = $('#extend');
                if (!!formdate) {
                    pgdId = formdate.F_Id;
                    for (var key in formdate) {
                        var $id = element.find('#' + key);
                        var value = $.trim(formdate[key]).replace(/&nbsp;/g, '');
                        var type = $id.attr('type');
                        if ($id.hasClass("select2-hidden-accessible")) {
                            type = "select";
                        }
                        switch (type) {
                            case "checkbox":
                                if (value == "true") {
                                    //$id.attr("checked", 'checked');
                                    $id.prop("checked", true);
                                }
                                else {
                                    $id.removeAttr("checked");
                                }
                                break;
                            case "select":
                                $id.val(value).trigger("change");
                                break;
                            default:
                                $id.val(value);
                                break;
                        }
                    }
                    return false;
                }
                else {
                    pgdId = "";
                    element.find("input[type='checkbox']:checked").prop("checked", false);
                    element.find("input[type='text']").val('');
                }
            }
        });
    }
    function txjlquery() {
        $.ajax({
            url: "/PatientManage/PatVisit/GetFormJson",
            dataType: "json",
            data: { keyValue: selectid },
            async: false,
            success: function(formdate) {
                var element = $('#basic');
                if (!!formdate) {
                    for (var key in formdate) {
                        var $id = element.find('#' + key);
                        var value = $.trim(formdate[key]).replace(/&nbsp;/g, '');
                        var type = $id.attr('type');
                        if ($id.hasClass("select2-hidden-accessible")) {
                            type = "select";
                        }
                        switch (type) {
                            case "checkbox":
                                if (value == "true") {
                                    //$id.attr("checked", 'checked');
                                    $id.prop("checked", true);
                                }
                                else {
                                    $id.removeAttr("checked");
                                }
                                break;
                            case "select":
                                $id.val(value).trigger("change");
                                break;
                            default:
                                $id.val(value);
                                break;
                        }
                    }
                    //预脱体重
                    $("#Custom_WeightYT").val(formdate.F_WeightYT);
                    //疗效结论
                    $("#summaryContent").val(formdate.F_Memo);
                    if (formdate.F_DialysisStartTime === null || formdate.F_DialysisStartTime === '' || formdate.F_DialysisStartTime === undefined) {
                        $("#btn-operator").show();
                        $("#operateTime").show();
                        $("#btn-operator").html("<i class=\"fa fa-play\"></i>上机").removeClass('btn-danger').removeClass('btn-success').addClass('btn-warning');
                        $("#operateTime").attr('placeholder', '上机时间(默认为当前时间)');
                    }
                    else if (formdate.F_DialysisEndTime === null || formdate.F_DialysisEndTime === '' || formdate.F_DialysisEndTime === undefined) {
                        if (formdate.F_CheckPerson === null || formdate.F_CheckPerson === '' || formdate.F_CheckPerson === undefined) {
                            $("#btn-operator").show();
                            $("#operateTime").show();
                            $("#btn-operator").html("<i class=\"fa fa-stop-circle-o\"></i>审核").removeClass('btn-warning').removeClass('btn-danger').addClass('btn-success');
                            $("#operateTime").attr('placeholder', '审核时间(默认为当前时间)');
                        } else {
                            $("#btn-operator").show();
                            $("#operateTime").show();
                            $("#btn-operator").html("<i class=\"fa fa-stop\"></i>下机").removeClass('btn-warning').removeClass('btn-success').addClass('btn-danger');
                            $("#operateTime").attr('placeholder', '下机时间(默认为当前时间)');
                        }  
                    } else {
                        $("#btn-operator").hide();
                        $("#operateTime").hide();
                    }

                     //else if (formdate.F_DialysisEndTime !== null && formdate.F_DialysisEndTime !== null && (formdate.F_CheckPerson === null || formdate.F_CheckPerson === '' || formdate.F_CheckPerson === undefined)) {
                    //    $("#btn-operator").show();
                    //    $("#operateTime").show();
                    //    $("#btn-operator").html("<i class=\"fa fa-stop-circle-o\"></i>审核").removeClass('btn-warning').removeClass('btn-danger').addClass('btn-success');
                    //    $("#operateTime").attr('placeholder', '审核时间(默认为当前时间)');
                    //}

                    //查询干体重
                    $.ajax({
                        url: "/PatientManage/Patient/GetIdeaWeightJsonById",
                        dataType: "json",
                        data: { keyValue: formdate.F_Pid },
                        async: true,
                        success: function (data) {
                            var ideaweight = parseFloat(data);
                            if (ideaweight > 5) {
                                $("#IdeaWeight").val(ideaweight);
                            } else {
                                $("#IdeaWeight").val("");
                            }
                        }
                    });
                } else {
                    $("#IdeaWeight").val("");
                }
            }
        });
    }
    function gcjcquery(){
        $.ajax({
            url: "/PatientManage/PatVisit/GetFormJson",
            dataType: "json",
            data: { keyValue: selectid },
            async: false,
            success: function (formdate) {
                var element = $('#extend7');
                if (!!formdate) {
                    for (var key in formdate) {
                        var $id = element.find('#' + key);
                        if ($id.length === 0) {
                            $id = element.find('input[name="' + key + '"]');
                            if ($id.length === 0) {
                                continue;
                            }
                        }
                        var value = $.trim(formdate[key]).replace(/&nbsp;/g, '');
                        var type = $id.attr('type');
                        if ($id.hasClass("select2-hidden-accessible")) {
                            type = "select";
                        }
                        switch (type) {
                            case "checkbox":
                                if (value == "true") {
                                    //$id.attr("checked", 'checked');
                                    $id.prop("checked", true);
                                }
                                else {
                                    $id.removeAttr("checked");
                                }
                                break;
                            case "select":
                                $id.val(value).trigger("change");
                                break;
                            case "radio":
                                //$id.val(value).trigger("change");
                                $id.each(function (index, ele) {
                                    if (ele.value === value) {
                                        ele.checked = true;
                                    }
                                    else {
                                        ele.checked = false;
                                    }
                                });
                                break;
                            default:
                                $id.val(value);
                                break;
                        }
                    }
                }
            }
        });
    }
    function lxpjquery() {
        $.ajax({
            url: "/PatientManage/PatVisit/GetFormJson",
            dataType: "json",
            data: { keyValue: selectid },
            async: false,
            success: function (formdate) {
                //var element = $('#extend5');
                if (!!formdate) {
                    $("#summaryContent").val(formdate.F_Memo);
                }
            }
        });
    }
}
function initControl() {
    //$('[data-toggle="tooltip"]').tooltip(); 
    //$('[data-toggle="tooltip"]').on('show.bs.tooltip', function (e) {
    //    //console.log(e);
    //    $('#' + e.currentTarget.id).attr('data-original-title', '<ul class="list-group"><li class= "list-group-item active">Cras justo odio</li><li class="list-group-item">Dapibus ac facilisis in</li><li class="list-group-item">Morbi leo risus</li><li class="list-group-item">Porta ac consectetur ac</li><li class="list-group-item">Vestibulum at eros</li></ul>');
    //}); 
    $.when(ajax1, ajax2, ajax3, ajax4, ajax5, ajax6);
    //透析参数
    $("#F_DilutionType").bindSelect();
    $("#F_DialysisType").bindSelect({
        url: "/SystemManage/ItemsData/GetSelectJson?enCode=DialysisType",
        id: "id",
        text: "text",
        change: function (e) {
            if (e.id == 'HDF') {
                //$("#hdhp").show();
                $("#F_DilutionType").val("前稀释");
            }
            else {
                //$("#hdhp").hide();
                $("#F_DilutionType").val("");
            }
        }
    });
    $("#F_VascularAccess").bindSelect();
    var ajax1 = $.ajax({
        url: "/PatientManage/Drugs/GetSelectJson",
        dataType: "json",
        async: true,
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
                    if (!(drugid == "")) {
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
    var ajax2 = $.ajax({
        url: "/PatientManage/Material/GetSelectJson",
        dataType: "json",
        async: true,
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
            //$("#F_DialyzerType2").bindSelect({
            //    data: tags
            //});
        }
    });
    var ajax3 = $.ajax({
        url: "/PatientManage/Material/GetSelectByTypeJson",
        data: { keyword : '灌流器' },
        dataType: "json",
        async: true,
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
    $("#btn-savepgd").on('click', function () {
        if (typeof selectid == "undefined" || selectid == null || selectid == "") {
            $.modalMsg("请先选择透析记录！", "warning");
        } else {
            var postdata = {};
            postdata.F_Id = pgdId;
            postdata.F_Pid = currentPid;
            postdata.F_VisitDate = $("#visitDate").val();
            if (pgdId === '') postdata.F_VisitDate = $("#visitDate").val();
            
            $("#extend").find('input,select,textarea').each(function (r) {
                var $this = $(this);
                var id = $this.attr('id');
                var type = $this.attr('type');
                //console.log(id,type);
                switch (type) {
                    case "checkbox":
                        postdata[id] = $this.is(":checked");
                        break;
                    default:
                        var value = $this.val() == "" ? "&nbsp;" : $this.val();
                        if (value == null) {
                            value = "&nbsp;";
                        }
                        if (!pgdId) {
                            value = value.replace(/&nbsp;/g, '');
                        }
                        postdata[id] = value;
                        break;
                }
            });
            $.submitForm({
                url: "/PatientManage/Evaluation/SubmitForm",
                param: {
                    entity: postdata,
                    keyValue: pgdId
                },
                success: function () { }
            });
        }
    });
    $("#btn-pgdmodel").on('click', function () {
        $("#extend").find("#Rsfs1,#Xy1,#Xl1,#Hx1,#Tw1,#Shzlnl1,#Tl1,#Ww1,#Sy1,#Yslkz1,#Sm1,#Nl1,#Cx1,#Yyqk4,#Qctxhqk1,#Tsqk1,#Nlccdqk1,#Sfsctx2,#Wytxywbs1,#Place1,#Sctxcczz1,#Xgzy1,#Nlcsxl1,#Nlsynx4,#Jkjyfs1,#Jkjyfs2,#Yszd1,#Yszd2,#Yszd3,#Yszd4,#Yszd5,#Ydzd1,#Ydzd2,#Ydzd3,#Xgtlzd1,#Xgtlzd2,#Xgtlzd3,#Xgtlzd4,#Tzgl1,#Tzgl2,#Tzgl3,#Sjz1").prop('checked', true);
        $("#extend").find("#Ddvalue1").val("1");
    });
    $("#btn-pgdclear").on('click', function () {
        $("#extend").find("input[type='checkbox']:checked").prop('checked', false);
        $("#extend").find("input[type='text']").val("");
    });
    $("#btn-pgdprint").on('click', function () {
        $.ajax({
            url: "/PatientManage/Evaluation/GetFormByVisitIdJson",
            dataType: "json",
            data: { keyValue: selectid },
            async: false,
            success: function (formdate) {
                if (!!formdate) {
                    $.post("/PatientManage/Evaluation/GetEvaluationImage?keyValue=" + formdate.F_Id
                        , function (result) {
                        $("#print").html(result);
                        $("#print").attr("style", "display:block;");//显示div
                        $("#print").jqprint();
                        $("#print").attr("style", "display:none;");//隐藏div
                    });  
                } else {
                    return false;
                }
            }
        });
    });
    $("#IdeaWeight").on('change', function (e) {
        //console.log(e, e.target.value); 
        if (typeof selectid == "undefined" || selectid == null || selectid == "") {
            $.modalMsg("请先选择透析记录！", "warning");
        } else {
            //修改干体重
            //$.modalOpen
            //$.modalConfirm("是否修改【干体重】?", function (flag) {
            //    if (flag) {
            //        console.log(1);
            //        $.modalClose();
            //    }
            //});
            var ideaWeight = parseFloat($("#IdeaWeight").val());
            if (ideaWeight < 10) return false;
            $.deleteForm({
                prompt: "注：是否修改【干体重】？",
                url: "/PatientManage/Patient/ModifyIdeaWeightForm",
                param: {
                    pid: currentPid,
                    ideaWeight: ideaWeight
                },
                loading: "正在更新数据...",
                success: function () {
                },
                close: true
            });
        }
    });

    $("#Custom_WeightYT").on('change', function (e) {
        //console.log(e, e.target.value); 
        if (typeof selectid == "undefined" || selectid == null || selectid == "") {
            $.modalMsg("请先选择透析记录！", "warning");
        } else {
            var ideaWeight = parseFloat($("#Custom_WeightYT").val());
            if (ideaWeight < 0 || ideaWeight > 5) {
                $("#Custom_WeightYT").focus();
                $.modalAlert('预脱量范围【0-5】，请核对！', 'warning');
                return false;
            }
            $.post("/PatientManage/PatVisit/ModifyYTWeight", { keyValue: selectid, value: ideaWeight }, function (data, status) {
                if (status === 'success') {
                    var result = JSON.parse(data);
                    if (result.state !== 'success') {
                        $("#Custom_WeightYT").focus();
                        $.modalAlert(result.message, 'warning');
                    }
                } else {
                    $.modalAlert(data, 'warning');
                }
            });
            //if (ideaWeight < 10) return false;
            //$.deleteForm({
            //    prompt: "注：是否修改【脱水量】？",
            //    url: "/PatientManage/Patient/ModifyYTWeight",
            //    param: {
            //        keyValue: selectid,
            //        value: ideaWeight
            //    },
            //    loading: "正在更新数据...",
            //    success: function () {
            //    },
            //    close: true
            //});
        }
    });
    $("#btn-savesetting").on('click', function () {
        if (typeof selectid == "undefined" || selectid == null || selectid == "") {
            $.modalMsg("请先选择透析记录！", "warning");
        } else {
            //var params = $("#basic").find()
            //console.log(params);
            var postdata = {};
            $("#basic").find('input,select,textarea').each(function (r) {
                //console.log(r);
                var $this = $(this);
                var id = $this.attr('id');
                var type = $this.attr('type');
                //console.log(id,type);
                switch (type) {
                    case "checkbox":
                        postdata[id] = $this.is(":checked");
                        break;
                    default:
                        //var value = $this.val() == "" ? "" : $this.val();
                        var value = $this.val() == "" ? "&nbsp;" : $this.val();
                        postdata[id] = value;
                        break;
                }
            });
            $.submitForm({
                url: "/PatientManage/PatVisit/SubmitForm",
                param: {
                    entity: postdata,
                    keyValue: selectid
                },//postdata,
                success: function () {
                    $.post("/PatientManage/PatVisit/DoctorSign", JSON.stringify({ keyValue: selectid }));
                }
            });
        }
    });
    $("#btn-operator").on('click', function (e) {
        if (typeof selectid == "undefined" || selectid == null || selectid == "") {
            $.modalMsg("请先选择透析记录！", "warning");
        } else {
            var operatetype = e.target.innerText === "" ? e.currentTarget.innerText : e.target.innerText;
            $.ajax({
                url: "/PatientManage/PatVisit/SaveOperateMachineTime",
                data: JSON.stringify({
                    keyValue: JSON.stringify({
                        id: selectid,
                        operateTime: $("#operateTime").val(),
                        operateType: e.target.innerText
                    })
                }),
                type: 'post',
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.state == "success") {
                        $.modalMsg(data.message, data.state);
                        switch (operatetype) {
                            case "上机":
                                //list.find(t => t.Id === selectid).StartTime = '111';
                                $.each(list, function (index, value) {
                                    if (value.Id === selectid) {
                                        value.StartTime = '111';
                                        return false;
                                    }
                                });
                                break;
                            case "下机":
                                //list.find(t => t.Id === selectid).EndTime = '222';
                                $.each(list, function (index, value) {
                                    if (value.Id === selectid) {
                                        value.EndTime = '222';
                                        return false;
                                    }
                                });
                                break;
                            case "审核":
                                //提示是否计费
                                $.each(list, function (index, value) {
                                    if (value.Id === selectid) {
                                        if (!value.IsAcct) {
                                            $.deleteForm({
                                                prompt: "注：是否扣减【库存】？",
                                                url: "/PatientManage/PatVisit/SubmitCharge",
                                                param: {
                                                    keyValue: selectid
                                                },
                                                loading: "正在更新数据...",
                                                success: function () {
                                                    value.IsAcct = true;
                                                },
                                                close: true
                                            });
                                        }
                                        return false;
                                    }
                                });

                                $("#btn-operator").html("<i class=\"fa fa-stop\"></i>下机").removeClass('btn-warning').removeClass('btn-success').addClass('btn-danger');
                                $("#operateTime").attr('placeholder', '下机时间(默认为当前时间)');
                                break;
                        }

                    } else {
                        $.modalAlert(data.message, data.state);
                    }
                    refreshList();
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
                var visitno = $("#visitNo").val();
                var statusinit = $("#statusinit")[0].checked;
                var statusuncomplete = $("#statusuncomplete")[0].checked;
                var statuscomplete = $("#statuscomplete")[0].checked;
                //var filterlist = list.filter(t => t.VisitNo == visitno);
                var filterlist = $.grep(list, function (value, index) {
                    return value.VisitNo == visitno;
                });
                if (filterlist.length === 0) return false;
                if (statusinit && statusuncomplete && statuscomplete) {
                    filterlist = filterlist;
                } else if (statusinit && statusuncomplete) {
                    //filterlist = filterlist.filter(t => t.EndTime == "");
                    filterlist = $.grep(filterlist, function (value, index) {
                        return value.EndTime == '';
                    });
                } else if (statusinit && statuscomplete) {
                    //filterlist = filterlist.filter(t => t.StartTime == "" || t.EndTime != "");
                    filterlist = $.grep(filterlist, function (value, index) {
                        return value.StartTime == '' || value.EndTime != '';
                    });
                } else if (statusuncomplete && statuscomplete) {
                    //filterlist = filterlist.filter(t => t.StartTime != "");
                    filterlist = $.grep(filterlist, function (value, index) {
                        return value.StartTime != '';
                    });
                } else if (statusinit) {
                    //filterlist = filterlist.filter(t => t.StartTime == "");
                    filterlist = $.grep(filterlist, function (value, index) {
                        return value.StartTime == '';
                    });
                } else if (statusuncomplete) {
                    //filterlist = filterlist.filter(t => t.StartTime != "" && t.EndTime == "");
                    filterlist = $.grep(filterlist, function (value, index) {
                        return value.StartTime != '' && value.EndTime == '';
                    });
                } else if (statuscomplete) {
                    //filterlist = filterlist.filter(t => t.EndTime != "");
                    filterlist = $.grep(filterlist, function (value, index) {
                        return value.EndTime != '';
                    });
                } else {
                    filterlist = [];
                }
                if (filterlist.length === 0) return false;
                //分组过滤
                var groupName = $("#groupName").val();
                if (!!groupName) {
                    //filterlist = filterlist.filter(t => t.GroupName === groupName);
                    filterlist = $.grep(filterlist, function (value, index) {
                        return value.GroupName == groupName;
                    });
                }
                filterlist.sort(function (a, b) {
                    return parseInt(a.DialysisBedNo) - parseInt(b.DialysisBedNo);
                }
                );
                if (filterlist.length === 0) return false;
                //filterlist = filterlist.filter(t => t.Py.indexOf(filterText) >= 0 || t.DialysisBedNo === filterText || t.Name.indexOf(filterText) >= 0 || t.DialysisNo == filterText);
                filterlist = $.grep(filterlist, function (t, index) {
                    return t.Py.indexOf(filterText) >= 0 || t.DialysisBedNo === filterText || t.Name.indexOf(filterText) >= 0 || t.DialysisNo == filterText;
                });
                if (filterlist.length > 0) {
                    var ul = $("#patient-list");
                    ul.empty();
                    filterlist.forEach(function (item) {
                        ul.append("<li class=\"list-group-item\" id=\"" + item.Id + "\">"
                            + "<span class=\"badge\">" + item.GroupName + item.DialysisBedNo + "</span>"
                            + "<h5 class=\"list-group-item-heading\">" + (item.IsArchive ? "<i class=\"fa fa-lock\"></i>&nbsp;" : "&nbsp;&nbsp;") + item.Name + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + item.Gender + "&nbsp;&nbsp;" + item.AgeDesc + "</h5>"
                            + "<p class=\"list-group-item-text\">" + item.DialysisType + "&nbsp;&nbsp;&nbsp;" + item.VascularAccess + "&nbsp;&nbsp;&nbsp;" + item.HeparinType + "&nbsp;&nbsp;&nbsp;</p>"
                            + "</li>"
                        );
                    });
                    selectid = '';
                    currentPid = '';
                    pgdId = '';
                    $("#btn-operator").hide();
                    $("#operateTime").hide();
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
    //穿刺记录
    $("#btn-upload").on('click', function () {
        if (typeof selectid == "undefined" || selectid == null || selectid == "") {
            $.modalMsg("请先选择透析记录！", "warning");
        } else {
            $.modalOpen({
                id: "Form",
                title: "上传图片",
                url: "/PatientManage/Puncture/Upload?keyValue=" + currentPid,
                width: "800px",
                height: "600px",
                callBack: function (iframeId) {
                    top.frames[iframeId].submitForm();
                    $.ajax({
                        url: "/PatientManage/Puncture/GetPuncturePicPath",
                        data: { keyValue: currentPid },
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            if (data.fileName) {
                                $("#img-puncture").attr('src', '\\' + data.filePath + '\\' + data.fileName);
                                $("#img-puncture").attr('alt', data.fileName);
                            }
                        }
                    });

                }
            });
        }
    });
    $("#btn-addpuncture").on('click', function (e) {
        //console.log(e);
        var $btnaddpuncture = $("#btn-addpuncture");
        //console.log($btnaddpuncture);
        //console.log(this);
        //[""0""].innerText
        //innerHTML: "<i class="fa fa - plus"></i>添加"
        if ($btnaddpuncture[0].innerText == '添加') {
            //[""0""].innerHTML
            $btnaddpuncture[0].innerHTML = '<i class="fa fa-save"></i>保存';
            $("#title-punctureA").show();
            $("#value-punctureA").show();
            $("#title-punctureB").show();
            $("#value-punctureB").show();
            $("#ck-punctureSuccess").show();
        }
        else {
            //保存穿刺记录数据
            if (typeof selectid == "undefined" || selectid == null || selectid == "") {
                $.modalMsg("请先选择透析记录！", "warning");
                return false;
            }
            var point1 = $("#punctureA").val();
            if (typeof point1 == "undefined" || point1 == null || point1 == "") {
                $.modalMsg("请填写动脉穿刺点！", "warning");
                $("#punctureA").focus();
                return false;
            }
            var point2 = $("#punctureB").val();
            if (typeof point2 == "undefined" || point2 == null || point2 == "") {
                $.modalMsg("请填写动脉穿刺点！", "warning");
                $("#punctureB").focus();
                return false;
            }
            var ispunctureSuccess = $("#punctureSuccess").prop('checked');
            var punctureMemo = $("#punctureMemo").val();
            if (!ispunctureSuccess) {
                if (typeof punctureMemo == "undefined" || punctureMemo == null || punctureMemo == "") {
                    $("punctureMemo").focus();
                }
            }
            //数据保存
            $.ajax({
                url: "/PatientManage/Puncture/SaveData",
                data: {
                    keyValue: JSON.stringify({
                        visitId: selectid,
                        point1: point1,
                        point2: point2,
                        memo: punctureMemo,
                        isSuccess: $("#punctureSuccess").prop('checked')
                    })
                },
                type: "post",
                dataType: "json",
                async: false,
                success: function (data) {
                    //$("#form1").formSerialize(data);
                    $("#punctureSuccess").prop('checked', true);
                    $("#table-punctureMemo").hide();
                    $("#punctureMemo").val("");
                    $.ajax({
                        url: "/PatientManage/Puncture/GetListJson",
                        data: { keyValue: currentPid },
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            var $target = $("#punctureItems");
                            $target.find('tr').remove();
                            if (data.length > 0) {
                                data.forEach(function (value) {
                                    $target.append('<tr><td>' + value.F_OperateTime + '</td><td>' + value.F_Point1 + ' - ' + value.F_Point2 + '</td><td>' + value.F_RealName + '</td><td>' + value.F_Memo + '</td></tr>');
                                });
                            }
                        }
                    });
                }
            });

            $btnaddpuncture[0].innerHTML = '<i class="fa fa-plus"></i>添加';
            $("#title-punctureA").hide();
            $("#value-punctureA").hide();
            $("#title-punctureB").hide();
            $("#value-punctureB").hide();
            $("#ck-punctureSuccess").hide();
        }
    });
    //填写穿刺失败信息
    $("#punctureSuccess").change(
        function () {
            var ischecked = $("#punctureSuccess").prop('checked');
            if (ischecked) {
                $("#table-punctureMemo").hide();
                $("#punctureMemo").val("");
            }
            else {
                $("#table-punctureMemo").show();
            }
        }
    );
    //观察记录
    var $obgridList = $("#ob-gridList");
    $obgridList.dataGrid({
        datatype: 'local',
        height: $(window).height() - 250,
        caption: '单位：机温℃，血压、动脉压、静脉压、TMPmmHg，电导率ms/cm，血流速ml/min，超滤率ml/h，超滤量、剩余肝素量ml',
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
                label: '收缩压', name: 'F_SSY', hidden: true
            },
            {
                label: '舒张压', name: 'F_SZY', hidden: true
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
                label: '血流速', name: 'F_BF', width: 50, align: 'center'
            },
            {
                label: '动脉压', name: 'F_A', width: 50, align: 'center'
            },
            {
                label: '静脉压', name: 'F_V', width: 50, align: 'center'
            },
            {
                label: '电导率', name: 'F_C', width: 50, align: 'center'
            },
            {
                label: 'TMP', name: 'F_TMP', width: 50, align: 'center'
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
            { label: '记录者', name: 'F_Nurse', width: 50, align: 'center' }
            //,{ label: '症状及处理', name: 'F_MEMO', width: 200, align: "left", sortable: false }
        ],
        pager: "#ob-gridPager",
        sortname: 'F_NurseOperatorTime desc',
        viewrecords: true
    });
    $("#btn-obsave").on('click', function () {
        if (typeof selectid == "undefined" || selectid == null || selectid == "") {
            $.modalMsg("请先选择透析记录！", "warning");
            return false;
        }
        if (!$('#formObservation').formValid()) {
            return false;
        }
        $.ajax({
            url: "/PatientManage/Nurse/SaveData",
            data: JSON.stringify({
                visitId: selectid,
                Ob_NurseOperatorTime: $("#Ob_NurseOperatorTime").val(),
                Ob_SSY: $("#Ob_SSY").val(),
                Ob_SZY: $("#Ob_SZY").val(),
                Ob_HR: $("#Ob_HR").val(),
                Ob_A: $("#Ob_A").val(),
                Ob_V: $("#Ob_V").val(),
                Ob_BF: $("#Ob_BF").val(),
                Ob_UFV: $("#Ob_UFV").val(),
                Ob_C: $("#Ob_C").val(),
                Ob_T: $("#Ob_T").val(),
                Ob_UFR: $("#Ob_UFR").val(),
                Ob_TMP: $("#Ob_TMP").val(),
                Ob_GSL: $("#Ob_GSL").val(),
                Ob_MEMO: $("#Ob_MEMO").val()
            }),
            type: "post",
            dataType: "json",
            async: false,
            success: function (data) {
                //console.log(data);
                if (data.state == "success") {
                    $.modalMsg(data.message, data.state);
                } else {
                    $.modalAlert(data.message, data.state);
                }
                $('#formObservation').find('input,textarea').each(function () {
                    var $this = $(this);
                    $this.val('');
                });
                //刷新数据
                $.ajax({
                    url: "/PatientManage/Nurse/GetListByVisitJson",
                    data: { visitId: selectid },
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        $("#ob-gridList").jqGrid("clearGridData");
                        $("#ob-gridList").setGridParam({ data: data }).trigger('reloadGrid');
                    }
                });
            }
        });
    });
    $("#btn-obcopy").on('click', function () {
        if (typeof selectid == "undefined" || selectid == null || selectid == "") {
            $.modalMsg("请先选择透析记录！", "warning");
            return false;
        }
        $.ajax({
            url: "/PatientManage/Nurse/GetSingleByVisitIdJson",
            data: {
                visitId: selectid
            },
            dataType: "json",
            async: false,
            success: function (data) {
                //$("#formObservation").formSerialize(data);
                if (!!data) {
                    $("#Ob_SSY").val(data.F_SSY);
                    $("#Ob_SZY").val(data.F_SZY);
                    $("#Ob_HR").val(data.F_HR);
                    $("#Ob_A").val(data.F_A);
                    $("#Ob_V").val(data.F_V);
                    $("#Ob_BF").val(data.F_BF);
                    $("#Ob_UFV").val(data.F_UFV);
                    $("#Ob_C").val(data.F_C);
                    $("#Ob_T").val(data.F_T);
                    $("#Ob_UFR").val(data.F_UFR);
                    $("#Ob_TMP").val(data.F_TMP);
                    //$("#Ob_GSL").val(data.F_SSY);
                }
            }
        });
    });
    //透析结论模板
    var ajax4 = $.ajax({
        url: "/PatientManage/ConclusionTemplate/GetSelectJson",
        data: { keyword: '' },
        dataType: "json",
        async: true,
        success: function (data) {
            var $target = $("#fileModel-list ul");
            $.each(data, function (index, item) {
                $target.append(
                    "<li class=\"list-group-item\">"
                    + "<h5 class=\"list-group-item-heading\">" + item.F_Title + "</h5>"
                    + "<p class=\"list-group-item-text\">" + item.F_Content
                    + "</p>"
                    + "</li>"
                );
            });
            $target.on("click", function (e) {
                //console.log(e);
                var className = e.target.localName;
                var contentText = '';
                if (className === 'p') {
                    contentText = e.target.innerText;
                } else if (className === 'h5') {
                    contentText = e.target.nextElementSibling.innerText;
                } else if (className === 'li') {
                    contentText = e.target.children[1].innerText;
                }
                //console.log(contentText);
                var tempStr = $("#summaryContent").val();
                $("#summaryContent").val(tempStr + contentText);
                $("#summaryContent").focus();
            });
        }
    });
    $("#extend5 a.btn-primary").first().on('click', function (e) {
        //保存透析结论
        if (typeof selectid == "undefined" || selectid == null || selectid == "") {
            $.modalMsg("请先选择透析记录！", "warning");
        } else {
            var json = {};
            json.id = selectid;
            json.content = $("#summaryContent").val();
            $.ajax({
                url: "/PatientManage/PatVisit/CommitMemo",
                data: JSON.stringify(json),
                dataType: "json",
                type: "post",
                async: false,
                success: function (data) {
                    $.modalAlert(data.message, data.state);
                }
            });
        }
    });
    $("#extend5 a.btn-warning").first().on('click', function (e) {
        //console.log("保存透析模板", e);
        var template = $("#summaryContent").val();
        if (template !== '') {
            $.modalOpen({
                id: "Form",
                title: "新增模板",
                url: "/PatientManage/ConclusionTemplate/Add?keyword=" + encodeURI(encodeURI(template)),
                width: "600px",
                height: "500px",
                callBack: function (iframeId) { 
                    top.frames[iframeId].submitForm();
                }
            });
        }
    });
    //化验结果查询
    var $extend6 = $("#extend6");
    var dateEnd = new Date();
    var dateStart = new Date(dateEnd.getTime() - 86400000 * 30);
    $extend6.find("#endDate").val(dateEnd.Format("yyyy-MM-dd"));
    $extend6.find("#startDate").val(dateStart.Format("yyyy-MM-dd"));  
    var ajax5 = $.ajax({
        url: "/PatientManage/QualityItem/GetSelectJson",
        dataType: "json",
        async: true,
        success: function (value) {
            var itemlist = value;
            itemlist.splice(0, 0, {
                Id: '',
                Name: '全部'
            });
            $extend6.find("#itemCode").bindSelect({
                data: value,
                id: "Id",
                text: "Name"
            });
        }
    });
    $("#gridListResult").dataGrid({
        //url: "/SystemManage/User/GetGridJson",
        datatype: 'local',
        height: $(window).height() - 248,
        colModel: [
            { label: '主键', name: 'F_Id', hidden: true},
            { label: 'ID', name: 'F_Pid', hidden: true},
            { label: '代码', name: 'F_ItemId', hidden: true},
            {
                label: '报告日期', name: 'F_ReportTime', width: 120, align: 'left',
                formatter: "time", formatoptions: { srcformat: 'Y-m-d h:m:s', newformat: 'Y-m-d h:m' }
            },
            { label: '项目代码', name: 'F_ItemCode', width: 100, align: 'left' },
            { label: '项目名称', name: 'F_ItemName', width: 150, align: 'left' },
            { label: '结果', name: 'F_Result', width: 100, align: 'left' },
            { label: '单位', name: 'F_Unit', width: 80, align: 'left' },
            { label: '备注', name: 'F_Memo', width: 300, align: 'left' }
        ],
        pager: "#gridPagerResult",
        rowNum: 20,
        rowList: [10, 20, 30, 40],
        sortname: 'F_ReportTime Desc',
        viewrecords: true,
        autowidth: false,
        onPaging: function (pageBtn) {
        }
    });
    $extend6.find("a.btn").on('click', function () {
        if (currentPid === null || currentPid === '') return false;
        var filterDateStart = $("#startDate").val();
        var filterDateEnd = $("#endDate").val();
        if (filterDateEnd === '' || filterDateStart === '') return false;
        $.ajax({
            url: "/PatientManage/QualityResult/GetListJson",
            dataType: "json",
            async: false,
            data: {
                patientId: currentPid,
                startDate: filterDateStart,
                endDate: filterDateEnd,
                itemId: $("#itemCode").val()
            },
            success: function (value) {
                var $gridList = $("#gridListResult");
                $gridList.jqGrid('clearGridData');
                $gridList.setGridParam({ data: value }).trigger('reloadGrid');
            }
        });
    });
    //过程监测
    var ajax6 = $.ajax({
        url: "/SystemManage/User/GetUserSelectJson",
        data: { keyValue: null },
        dataType: "json",
        async: true,
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
    $("#btn-savegcjc").on('click', function () {
        if (typeof selectid == "undefined" || selectid == null || selectid == "") {
            $.modalMsg("请先选择透析记录！", "warning");
        } else {
            var postdata = {};
            $("#extend7").find('input,select,textarea').each(function (r) {
                var $this = $(this);
                var id = $this.attr('id') || $this.attr('name');
                var type = $this.attr('type');
                switch (type) {
                    case "checkbox":
                        postdata[id] = $this.is(":checked");
                        break;
                    case "radio":
                        if ($this.is(":checked")) {
                            postdata[id] = $this.attr('value');
                        }
                        break;
                    default:
                        var value = $this.val() == "" ? "&nbsp;" : $this.val();
                        postdata[id] = value;
                        break;
                }
            });
            $.submitForm({
                url: "/PatientManage/PatVisit/SubmitForm",
                param: {
                    entity: postdata,
                    keyValue: selectid
                },
                success: function () { }
            });
        }
    });
}
function refreshList() {
    //var ul = $("ul .list-group");//patient-list
    var ul = $("#patient-list");//patient-list
    //console.log(ul);
    ul.empty();
    var visitno = $("#visitNo").val();
    var statusinit = $("#statusinit")[0].checked;
    var statusuncomplete = $("#statusuncomplete")[0].checked;
    var statuscomplete = $("#statuscomplete")[0].checked;
    if (list == undefined || list.length == undefined || list.length === 0) return;
    //var filterlist = list.filter(t => t.VisitNo == visitno);
    var filterlist = $.grep(list, function (value, index) {
        return value.VisitNo == visitno;
    });
    if (statusinit && statusuncomplete && statuscomplete) {
        filterlist = filterlist;
    } else if (statusinit && statusuncomplete) {
        //filterlist = filterlist.filter(t => t.EndTime == "");
        filterlist = $.grep(filterlist, function (value, index) {
            return value.EndTime == '';
        });
    } else if (statusinit && statuscomplete) {
        //filterlist = filterlist.filter(t => t.StartTime == "" || t.EndTime != "" );
        filterlist = $.grep(filterlist, function (value, index) {
            return value.StartTime == '' || value.EndTime != '';
        });
    } else if (statusuncomplete && statuscomplete) {
        //filterlist = filterlist.filter(t => t.StartTime != "");
        filterlist = $.grep(filterlist, function (value, index) {
            return value.StartTime != '';
        });
    } else if (statusinit) {
        //filterlist = filterlist.filter(t => t.StartTime == "");
        filterlist = $.grep(filterlist, function (value, index) {
            return value.StartTime == '';
        });
    } else if (statusuncomplete) {
        //filterlist = filterlist.filter(t => t.StartTime != "" && t.EndTime == "");
        filterlist = $.grep(filterlist, function (value, index) {
            return value.StartTime != '' && value.EndTime == '';
        });
    } else if (statuscomplete) {
        //filterlist = filterlist.filter(t => t.EndTime != "");
        filterlist = $.grep(filterlist, function (value, index) {
            return value.EndTime != '';
        });
    } else {
        filterlist = [];
    }
    //分组过滤
    var groupName = $("#groupName").val();
    if (!!groupName) {
        //filterlist = filterlist.filter(t => t.GroupName === groupName);
        filterlist = $.grep(filterlist, function (value, index) {
            return value.GroupName == groupName;
        });
    }
    filterlist.sort(function (a, b) {
        return parseInt(a.DialysisBedNo) - parseInt(b.DialysisBedNo);
    }
    );
    filterlist.forEach(function (item) {
        ul.append("<li class=\"list-group-item\" id=\"" + item.Id + "\">"
            + "<span class=\"badge\">" + item.GroupName + item.DialysisBedNo + "</span>"
            + "<h5 class=\"list-group-item-heading\">" + (item.IsArchive ? "<i class=\"fa fa-lock\"></i>&nbsp;" :"&nbsp;&nbsp;") + item.Name + "&nbsp;&nbsp;&nbsp;" + item.Gender + "&nbsp;&nbsp;" + item.AgeDesc + "</h5>"
            + "<p class=\"list-group-item-text\">" + item.DialysisType + "&nbsp;&nbsp;" + item.VascularAccess + "&nbsp;&nbsp;" + item.HeparinType + "&nbsp;&nbsp;</p>"
            + "</li>"
        );
    });
    selectid = '';
    currentPid = '';
    pgdId = '';
    $("#btn-operator").hide();
    $("#operateTime").hide();
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
        data: { visitDate: visitDate},
        async: false,
        success: function (data) {
            list = data;
        }
    });

    //更新病人列表
    refreshList();
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
};

