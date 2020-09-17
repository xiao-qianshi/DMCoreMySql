using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dmt.DM.EntityFrameWork.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sys_Area",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_ParentId = table.Column<string>(maxLength: 50, nullable: true),
                    F_Layers = table.Column<int>(nullable: true),
                    F_EnCode = table.Column<string>(maxLength: 50, nullable: true),
                    F_FullName = table.Column<string>(maxLength: 50, nullable: true),
                    F_SimpleSpelling = table.Column<string>(maxLength: 50, nullable: true),
                    F_SortCode = table.Column<int>(nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_Description = table.Column<string>(maxLength: 500, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Area", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_AuditLog",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_ControllName = table.Column<string>(maxLength: 30, nullable: true),
                    F_MethodName = table.Column<string>(maxLength: 30, nullable: true),
                    F_Parameters = table.Column<string>(nullable: true),
                    F_Exception = table.Column<string>(nullable: true),
                    F_Result = table.Column<string>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_AuditLog", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Barcode",
                columns: table => new
                {
                    sn = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    F_Id = table.Column<long>(nullable: false),
                    BarcodeDate = table.Column<DateTime>(nullable: false),
                    Barcode = table.Column<int>(nullable: false),
                    RequestId = table.Column<long>(nullable: true),
                    BarcodeCreateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Barcode", x => x.sn);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Billing",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialylisNo = table.Column<int>(nullable: false),
                    F_PName = table.Column<string>(maxLength: 20, nullable: true),
                    F_PGender = table.Column<string>(maxLength: 10, nullable: true),
                    F_BillingDateTime = table.Column<DateTime>(nullable: false),
                    F_BillingPersonId = table.Column<string>(nullable: true),
                    F_BillingPerson = table.Column<string>(nullable: true),
                    F_ItemId = table.Column<string>(maxLength: 50, nullable: false),
                    F_ItemClass = table.Column<string>(maxLength: 15, nullable: true),
                    F_ItemCode = table.Column<string>(maxLength: 15, nullable: true),
                    F_ItemName = table.Column<string>(maxLength: 40, nullable: true),
                    F_ItemSpec = table.Column<string>(maxLength: 30, nullable: true),
                    F_ItemUnit = table.Column<string>(maxLength: 20, nullable: true),
                    F_Amount = table.Column<float>(nullable: true),
                    F_Supplier = table.Column<string>(maxLength: 60, nullable: true),
                    F_Charges = table.Column<float>(nullable: true),
                    F_Costs = table.Column<float>(nullable: true),
                    F_ItemSpell = table.Column<string>(maxLength: 15, nullable: true),
                    F_IsAcct = table.Column<bool>(nullable: true),
                    F_DisabledPerson = table.Column<string>(maxLength: 20, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Billing", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_BillingModel",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType = table.Column<string>(maxLength: 30, nullable: false),
                    F_VascularAccess = table.Column<string>(maxLength: 30, nullable: false),
                    F_ItemId = table.Column<string>(maxLength: 50, nullable: false),
                    F_ItemClass = table.Column<string>(maxLength: 15, nullable: true),
                    F_ItemCode = table.Column<string>(maxLength: 15, nullable: true),
                    F_ItemName = table.Column<string>(maxLength: 40, nullable: true),
                    F_ItemSpec = table.Column<string>(maxLength: 30, nullable: true),
                    F_ItemUnit = table.Column<string>(maxLength: 20, nullable: true),
                    F_Amount = table.Column<float>(nullable: true),
                    F_Supplier = table.Column<string>(maxLength: 60, nullable: true),
                    F_Charges = table.Column<float>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_BillingModel", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Book",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_FileIndex = table.Column<string>(maxLength: 50, nullable: false),
                    F_FileType = table.Column<string>(maxLength: 10, nullable: true),
                    F_BookName = table.Column<string>(maxLength: 200, nullable: false),
                    F_FilePath = table.Column<string>(maxLength: 200, nullable: true),
                    F_FileSize = table.Column<string>(maxLength: 30, nullable: true),
                    F_UploadPerson = table.Column<string>(maxLength: 20, nullable: true),
                    F_UploadDate = table.Column<DateTime>(nullable: false),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Book", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_ConclusionTemplate",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Title = table.Column<string>(maxLength: 50, nullable: false),
                    F_Content = table.Column<string>(maxLength: 500, nullable: false),
                    F_IsPrivate = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_ConclusionTemplate", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_CriticalValue",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_Name = table.Column<string>(maxLength: 20, nullable: true),
                    F_DialysisNo = table.Column<int>(nullable: false),
                    F_RecordNo = table.Column<string>(maxLength: 20, nullable: false),
                    F_PatientNo = table.Column<string>(maxLength: 20, nullable: false),
                    F_Gender = table.Column<string>(maxLength: 8, nullable: true),
                    F_BirthDay = table.Column<DateTime>(nullable: true),
                    F_IdNo = table.Column<string>(maxLength: 20, nullable: true),
                    F_ReportTime = table.Column<DateTime>(nullable: true),
                    F_ItemId = table.Column<string>(maxLength: 50, nullable: false),
                    F_ItemType = table.Column<string>(maxLength: 20, nullable: true),
                    F_ItemCode = table.Column<string>(maxLength: 20, nullable: true),
                    F_ItemName = table.Column<string>(maxLength: 50, nullable: true),
                    F_HisItemCode = table.Column<string>(maxLength: 20, nullable: true),
                    F_Unit = table.Column<string>(maxLength: 20, nullable: true),
                    F_Result = table.Column<string>(maxLength: 20, nullable: true),
                    F_LowerValue = table.Column<float>(nullable: true),
                    F_UpperValue = table.Column<float>(nullable: true),
                    F_LowerCriticalValue = table.Column<float>(nullable: true),
                    F_UpperCriticalValue = table.Column<float>(nullable: true),
                    F_ResultType = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_Memo = table.Column<string>(maxLength: 200, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_CriticalValue", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_DataReportJS",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_PId = table.Column<string>(maxLength: 50, nullable: true),
                    F_MonthDesc = table.Column<string>(maxLength: 10, nullable: true),
                    F_FileName = table.Column<string>(maxLength: 30, nullable: true),
                    F_FileSize = table.Column<string>(maxLength: 30, nullable: true),
                    F_FilePath = table.Column<string>(maxLength: 150, nullable: true),
                    F_IsCompleted = table.Column<bool>(nullable: false),
                    F_HasDownload = table.Column<bool>(nullable: true),
                    F_DownloadTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_DataReportJS", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_DATestReport",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    BARCODE = table.Column<string>(maxLength: 20, nullable: true),
                    SAMPLEFROM = table.Column<string>(maxLength: 50, nullable: true),
                    SAMPLETYPE = table.Column<string>(maxLength: 20, nullable: true),
                    COLLECTDDATE = table.Column<DateTime>(nullable: true),
                    SUBMITDATE = table.Column<DateTime>(nullable: true),
                    TESTCODE = table.Column<string>(maxLength: 20, nullable: true),
                    APPRDATE = table.Column<DateTime>(nullable: true),
                    DEPT = table.Column<string>(maxLength: 50, nullable: true),
                    SERVGRP = table.Column<string>(maxLength: 50, nullable: true),
                    USRNAMID = table.Column<string>(maxLength: 20, nullable: true),
                    USRNAM = table.Column<string>(maxLength: 20, nullable: true),
                    APPRVEDBYID = table.Column<string>(maxLength: 20, nullable: true),
                    APPRVEDBY = table.Column<string>(maxLength: 20, nullable: true),
                    PATIENTNAME = table.Column<string>(maxLength: 20, nullable: true),
                    PATIENTCATEGORY = table.Column<string>(maxLength: 20, nullable: true),
                    DOCTOR = table.Column<string>(maxLength: 20, nullable: true),
                    SEX = table.Column<string>(maxLength: 10, nullable: true),
                    AGE = table.Column<float>(nullable: true),
                    AGEUNIT = table.Column<string>(maxLength: 10, nullable: true),
                    SINONYM = table.Column<string>(maxLength: 50, nullable: true),
                    SHORTNAME = table.Column<string>(maxLength: 20, nullable: true),
                    UNITS = table.Column<string>(maxLength: 30, nullable: true),
                    FINAL = table.Column<string>(maxLength: 40, nullable: true),
                    ANALYTE = table.Column<string>(maxLength: 100, nullable: true),
                    DISPLOWHIGH = table.Column<string>(maxLength: 80, nullable: true),
                    DISPLOWHIGH_F = table.Column<string>(maxLength: 80, nullable: true),
                    DISPLOWHIGH_M = table.Column<string>(maxLength: 80, nullable: true),
                    RN10 = table.Column<string>(maxLength: 6, nullable: true),
                    S = table.Column<string>(maxLength: 20, nullable: true),
                    RN20 = table.Column<string>(nullable: true),
                    SYNONIM_EN = table.Column<string>(maxLength: 80, nullable: true),
                    COMMENTS = table.Column<string>(maxLength: 500, nullable: true),
                    DIAGNOSIS = table.Column<string>(maxLength: 80, nullable: true),
                    GENE_MUTATIONS_RATIO = table.Column<string>(maxLength: 20, nullable: true),
                    LOWB = table.Column<string>(maxLength: 20, nullable: true),
                    HIGHB = table.Column<string>(maxLength: 20, nullable: true),
                    CLINICNAME = table.Column<string>(maxLength: 40, nullable: true),
                    RANGE_FLG = table.Column<string>(maxLength: 6, nullable: true),
                    RANGE_DESC = table.Column<string>(maxLength: 500, nullable: true),
                    PATIENTPHONE = table.Column<string>(maxLength: 20, nullable: true),
                    SORTER = table.Column<int>(nullable: true),
                    LONGTXT = table.Column<string>(maxLength: 500, nullable: true),
                    EQUIPMENT = table.Column<string>(maxLength: 40, nullable: true),
                    METHODNAME = table.Column<string>(maxLength: 40, nullable: true),
                    DOCTORPHONE = table.Column<string>(maxLength: 20, nullable: true),
                    ANALYTE_ORIGREC = table.Column<string>(maxLength: 30, nullable: true),
                    SAMPLESTATUS = table.Column<string>(maxLength: 40, nullable: true),
                    TESTDATE = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_DATestReport", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_DbBackup",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_BackupType = table.Column<string>(maxLength: 50, nullable: true),
                    F_DbName = table.Column<string>(maxLength: 50, nullable: true),
                    F_FileName = table.Column<string>(maxLength: 50, nullable: true),
                    F_FileSize = table.Column<string>(maxLength: 50, nullable: true),
                    F_FilePath = table.Column<string>(maxLength: 500, nullable: true),
                    F_BackupTime = table.Column<DateTime>(nullable: true),
                    F_SortCode = table.Column<int>(nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_Description = table.Column<string>(maxLength: 500, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_DbBackup", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_DialysisMachine",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_GroupName = table.Column<string>(maxLength: 20, nullable: false),
                    F_ShowOrder = table.Column<int>(nullable: false),
                    F_DialylisBedNo = table.Column<string>(maxLength: 10, nullable: false),
                    F_MachineNo = table.Column<string>(maxLength: 20, nullable: false),
                    F_MachineName = table.Column<string>(maxLength: 20, nullable: false),
                    F_DefaultType = table.Column<string>(maxLength: 20, nullable: false),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_DialysisMachine", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_DialysisObservation",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: true),
                    F_VisitDate = table.Column<DateTime>(nullable: true),
                    F_VisitNo = table.Column<int>(nullable: true),
                    F_SSY = table.Column<float>(nullable: true),
                    F_SZY = table.Column<float>(nullable: true),
                    F_HR = table.Column<float>(nullable: true),
                    F_A = table.Column<float>(nullable: true),
                    F_BF = table.Column<float>(nullable: true),
                    F_UFR = table.Column<float>(nullable: true),
                    F_V = table.Column<float>(nullable: true),
                    F_C = table.Column<float>(nullable: true),
                    F_T = table.Column<float>(nullable: true),
                    F_UFV = table.Column<float>(nullable: true),
                    F_TMP = table.Column<float>(nullable: true),
                    F_GSL = table.Column<float>(nullable: true),
                    F_MEMO = table.Column<string>(maxLength: 200, nullable: true),
                    F_Nurse = table.Column<string>(maxLength: 50, nullable: true),
                    F_NurseName = table.Column<string>(maxLength: 30, nullable: true),
                    F_NurseOperatorTime = table.Column<DateTime>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_DialysisObservation", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_DialysisSchedule",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_GroupName = table.Column<string>(maxLength: 20, nullable: true),
                    F_DialysisBedNo = table.Column<string>(maxLength: 10, nullable: true),
                    F_BId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DialysisType = table.Column<string>(maxLength: 20, nullable: true),
                    F_VisitDate = table.Column<DateTime>(nullable: true),
                    F_VisitNo = table.Column<int>(nullable: true),
                    F_PId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DialysisNo = table.Column<int>(nullable: true),
                    F_Name = table.Column<string>(maxLength: 20, nullable: true),
                    F_Sort = table.Column<int>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_DialysisSchedule", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_DoseGuide",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_DrugId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DrugEnName = table.Column<string>(maxLength: 100, nullable: true),
                    F_DrugName = table.Column<string>(maxLength: 100, nullable: true),
                    F_Py = table.Column<string>(maxLength: 100, nullable: true),
                    F_Method = table.Column<string>(maxLength: 20, nullable: true),
                    F_Range1 = table.Column<string>(maxLength: 60, nullable: true),
                    F_Range2 = table.Column<string>(maxLength: 60, nullable: true),
                    F_Range3 = table.Column<string>(maxLength: 60, nullable: true),
                    F_Range4 = table.Column<string>(maxLength: 60, nullable: true),
                    F_Symptom1 = table.Column<string>(maxLength: 80, nullable: true),
                    F_Symptom2 = table.Column<string>(maxLength: 80, nullable: true),
                    F_Symptom3 = table.Column<string>(maxLength: 80, nullable: true),
                    F_Symptom4 = table.Column<string>(maxLength: 80, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_DoseGuide", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Drugs",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_DrugCode = table.Column<string>(maxLength: 15, nullable: true),
                    F_DrugName = table.Column<string>(maxLength: 40, nullable: true),
                    F_DrugSpec = table.Column<string>(maxLength: 30, nullable: true),
                    F_DrugUnit = table.Column<string>(maxLength: 20, nullable: true),
                    F_DrugMiniAmount = table.Column<float>(nullable: true),
                    F_DrugMiniSpec = table.Column<string>(maxLength: 20, nullable: true),
                    F_DrugMiniPackage = table.Column<int>(nullable: true),
                    F_DrugAdministration = table.Column<string>(maxLength: 20, nullable: true),
                    F_DrugEfficacy = table.Column<string>(maxLength: 200, nullable: true),
                    F_DrugSupplier = table.Column<string>(maxLength: 60, nullable: true),
                    F_Charges = table.Column<float>(nullable: true),
                    F_DrugSpell = table.Column<string>(maxLength: 15, nullable: true),
                    F_IsHeparin = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Drugs", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Evaluation",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_VisitDate = table.Column<DateTime>(nullable: false),
                    Rsfs1 = table.Column<bool>(nullable: true),
                    Rsfs2 = table.Column<bool>(nullable: true),
                    Rsfs3 = table.Column<bool>(nullable: true),
                    Xy1 = table.Column<bool>(nullable: true),
                    Xy2 = table.Column<bool>(nullable: true),
                    Xy3 = table.Column<bool>(nullable: true),
                    Xy4 = table.Column<bool>(nullable: true),
                    Xyvalue1 = table.Column<string>(maxLength: 150, nullable: true),
                    Xyvalue2 = table.Column<string>(maxLength: 150, nullable: true),
                    Xl1 = table.Column<bool>(nullable: true),
                    Xl2 = table.Column<bool>(nullable: true),
                    Xl3 = table.Column<bool>(nullable: true),
                    Xl4 = table.Column<bool>(nullable: true),
                    Xlvalue1 = table.Column<string>(maxLength: 150, nullable: true),
                    Xlvalue2 = table.Column<string>(maxLength: 150, nullable: true),
                    Hx1 = table.Column<bool>(nullable: true),
                    Hx2 = table.Column<bool>(nullable: true),
                    Hx3 = table.Column<bool>(nullable: true),
                    Hx4 = table.Column<bool>(nullable: true),
                    Hx5 = table.Column<bool>(nullable: true),
                    Hx6 = table.Column<bool>(nullable: true),
                    Tw1 = table.Column<bool>(nullable: true),
                    Tw2 = table.Column<bool>(nullable: true),
                    Tw3 = table.Column<bool>(nullable: true),
                    Twvalue1 = table.Column<string>(maxLength: 150, nullable: true),
                    Shzlnl1 = table.Column<bool>(nullable: true),
                    Shzlnl2 = table.Column<bool>(nullable: true),
                    Shzlnl3 = table.Column<bool>(nullable: true),
                    Tl1 = table.Column<bool>(nullable: true),
                    Tl2 = table.Column<bool>(nullable: true),
                    Tl3 = table.Column<bool>(nullable: true),
                    Ww1 = table.Column<bool>(nullable: true),
                    Ww2 = table.Column<bool>(nullable: true),
                    Ww3 = table.Column<bool>(nullable: true),
                    Sy1 = table.Column<bool>(nullable: true),
                    Sy2 = table.Column<bool>(nullable: true),
                    Sy3 = table.Column<bool>(nullable: true),
                    Yslkz1 = table.Column<bool>(nullable: true),
                    Yslkz2 = table.Column<bool>(nullable: true),
                    Yslkz3 = table.Column<bool>(nullable: true),
                    Sm1 = table.Column<bool>(nullable: true),
                    Sm2 = table.Column<bool>(nullable: true),
                    Sm3 = table.Column<bool>(nullable: true),
                    Nl1 = table.Column<bool>(nullable: true),
                    Nl2 = table.Column<bool>(nullable: true),
                    Nlvalue1 = table.Column<string>(maxLength: 150, nullable: true),
                    Dd1 = table.Column<bool>(nullable: true),
                    Dd2 = table.Column<bool>(nullable: true),
                    Ddvalue1 = table.Column<string>(maxLength: 150, nullable: true),
                    Cx1 = table.Column<bool>(nullable: true),
                    Cx2 = table.Column<bool>(nullable: true),
                    Cxvalue1 = table.Column<string>(maxLength: 150, nullable: true),
                    Yyqk1 = table.Column<bool>(nullable: true),
                    Yyqk2 = table.Column<bool>(nullable: true),
                    Yyqk3 = table.Column<bool>(nullable: true),
                    Yyqk4 = table.Column<bool>(nullable: true),
                    Yyqkvalue1 = table.Column<string>(maxLength: 150, nullable: true),
                    Qctxhqk1 = table.Column<bool>(nullable: true),
                    Qctxhqk2 = table.Column<bool>(nullable: true),
                    Qctxhqk3 = table.Column<bool>(nullable: true),
                    Qctxhqk4 = table.Column<bool>(nullable: true),
                    Qctxhqk5 = table.Column<bool>(nullable: true),
                    Qctxhqk6 = table.Column<bool>(nullable: true),
                    Qctxhqkvalue1 = table.Column<string>(maxLength: 150, nullable: true),
                    Tsqk1 = table.Column<bool>(nullable: true),
                    Tsqk2 = table.Column<bool>(nullable: true),
                    Tsqk3 = table.Column<bool>(nullable: true),
                    Tsqk4 = table.Column<bool>(nullable: true),
                    Tsqkvalue1 = table.Column<string>(maxLength: 150, nullable: true),
                    Tsqkvalue2 = table.Column<string>(maxLength: 150, nullable: true),
                    Nlccdqk1 = table.Column<bool>(nullable: true),
                    Nlccdqk2 = table.Column<bool>(nullable: true),
                    Nlccdqk3 = table.Column<bool>(nullable: true),
                    Nlccdqk4 = table.Column<bool>(nullable: true),
                    Nlccdqk5 = table.Column<bool>(nullable: true),
                    Nlccdqk6 = table.Column<bool>(nullable: true),
                    Nlccdqk7 = table.Column<bool>(nullable: true),
                    Sfsctx1 = table.Column<bool>(nullable: true),
                    Sfsctx2 = table.Column<bool>(nullable: true),
                    Sctxdate = table.Column<DateTime>(nullable: true),
                    Sfsctxvalue1 = table.Column<string>(maxLength: 150, nullable: true),
                    Sfsctxvalue2 = table.Column<string>(maxLength: 150, nullable: true),
                    Sfsctxvalue3 = table.Column<string>(maxLength: 150, nullable: true),
                    Wytxcfvalue1 = table.Column<string>(maxLength: 150, nullable: true),
                    Wytxcfvalue2 = table.Column<string>(maxLength: 150, nullable: true),
                    Wytxcfvalue3 = table.Column<string>(maxLength: 150, nullable: true),
                    Wytxywbs1 = table.Column<bool>(nullable: true),
                    Wytxywbs2 = table.Column<bool>(nullable: true),
                    Wz1 = table.Column<bool>(nullable: true),
                    Wz2 = table.Column<bool>(nullable: true),
                    Wz3 = table.Column<bool>(nullable: true),
                    Wz4 = table.Column<bool>(nullable: true),
                    Wzvalue1 = table.Column<string>(maxLength: 150, nullable: true),
                    Skwg1 = table.Column<bool>(nullable: true),
                    Skwg2 = table.Column<bool>(nullable: true),
                    Skwg3 = table.Column<bool>(nullable: true),
                    Skwg4 = table.Column<bool>(nullable: true),
                    Skwg5 = table.Column<bool>(nullable: true),
                    Skwg6 = table.Column<bool>(nullable: true),
                    Skwgvalue1 = table.Column<string>(maxLength: 150, nullable: true),
                    Hy1 = table.Column<bool>(nullable: true),
                    Hy2 = table.Column<bool>(nullable: true),
                    Dgll1 = table.Column<bool>(nullable: true),
                    Dgll2 = table.Column<bool>(nullable: true),
                    Dgll3 = table.Column<bool>(nullable: true),
                    Ywfr1 = table.Column<bool>(nullable: true),
                    Ywfr2 = table.Column<bool>(nullable: true),
                    Ywfrvalue1 = table.Column<string>(maxLength: 150, nullable: true),
                    Place1 = table.Column<bool>(nullable: true),
                    Place2 = table.Column<bool>(nullable: true),
                    Placevalue1 = table.Column<string>(maxLength: 150, nullable: true),
                    Placevalue2 = table.Column<string>(maxLength: 150, nullable: true),
                    Sctxcczz1 = table.Column<bool>(nullable: true),
                    Sctxcczz2 = table.Column<bool>(nullable: true),
                    Sctxcczz3 = table.Column<bool>(nullable: true),
                    Sctxcczz4 = table.Column<bool>(nullable: true),
                    Sctxcczz5 = table.Column<bool>(nullable: true),
                    Xgzy1 = table.Column<bool>(nullable: true),
                    Xgzy2 = table.Column<bool>(nullable: true),
                    Xgzy3 = table.Column<bool>(nullable: true),
                    Nlcsxl1 = table.Column<bool>(nullable: true),
                    Nlcsxl2 = table.Column<bool>(nullable: true),
                    Nlcsxlvalue1 = table.Column<string>(maxLength: 150, nullable: true),
                    Nlsynx1 = table.Column<bool>(nullable: true),
                    Nlsynx2 = table.Column<bool>(nullable: true),
                    Nlsynx3 = table.Column<bool>(nullable: true),
                    Nlsynx4 = table.Column<bool>(nullable: true),
                    Jkjyfs1 = table.Column<bool>(nullable: true),
                    Jkjyfs2 = table.Column<bool>(nullable: true),
                    Jkjyfs3 = table.Column<bool>(nullable: true),
                    Jkjyfs4 = table.Column<bool>(nullable: true),
                    Yszd1 = table.Column<bool>(nullable: true),
                    Yszd2 = table.Column<bool>(nullable: true),
                    Yszd3 = table.Column<bool>(nullable: true),
                    Yszd4 = table.Column<bool>(nullable: true),
                    Yszd5 = table.Column<bool>(nullable: true),
                    Ydzd1 = table.Column<bool>(nullable: true),
                    Ydzd2 = table.Column<bool>(nullable: true),
                    Ydzd3 = table.Column<bool>(nullable: true),
                    Xgtlzd1 = table.Column<bool>(nullable: true),
                    Xgtlzd2 = table.Column<bool>(nullable: true),
                    Xgtlzd3 = table.Column<bool>(nullable: true),
                    Xgtlzd4 = table.Column<bool>(nullable: true),
                    Tzgl1 = table.Column<bool>(nullable: true),
                    Tzgl2 = table.Column<bool>(nullable: true),
                    Tzgl3 = table.Column<bool>(nullable: true),
                    Sjz1 = table.Column<bool>(nullable: true),
                    Sjz2 = table.Column<bool>(nullable: true),
                    Sjzvalue1 = table.Column<string>(maxLength: 150, nullable: true),
                    Checkperson = table.Column<string>(maxLength: 50, nullable: true),
                    Checknurse = table.Column<string>(maxLength: 50, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Evaluation", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_FileIndex",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_Name = table.Column<string>(nullable: true),
                    F_DialysisNo = table.Column<int>(nullable: true),
                    F_CardNo = table.Column<string>(nullable: true),
                    F_RecordNo = table.Column<string>(nullable: true),
                    F_PatientNo = table.Column<string>(nullable: true),
                    F_Gender = table.Column<string>(nullable: true),
                    F_FileType = table.Column<string>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_RealName = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_FileIndex", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_FilterIP",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Type = table.Column<bool>(nullable: true),
                    F_StartIP = table.Column<string>(maxLength: 50, nullable: true),
                    F_EndIP = table.Column<string>(maxLength: 50, nullable: true),
                    F_SortCode = table.Column<int>(nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_Description = table.Column<string>(maxLength: 500, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_FilterIP", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_IdeaWeight",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: true),
                    F_Name = table.Column<string>(maxLength: 20, nullable: true),
                    F_DialysisNo = table.Column<int>(nullable: false),
                    F_IdealWeight = table.Column<float>(nullable: true),
                    F_OldIdealWeight = table.Column<float>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_IdeaWeight", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_ImportDetail",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_ImpId = table.Column<string>(maxLength: 50, nullable: false),
                    F_ItemId = table.Column<string>(maxLength: 50, nullable: true),
                    F_Code = table.Column<string>(maxLength: 20, nullable: false),
                    F_Name = table.Column<string>(maxLength: 50, nullable: true),
                    F_Spec = table.Column<string>(nullable: true),
                    F_Unit = table.Column<string>(nullable: true),
                    F_Charges = table.Column<float>(nullable: true),
                    F_Amount = table.Column<int>(nullable: true),
                    F_TotalCharges = table.Column<float>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_ImportDetail", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_ImportMaster",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_ImpNo = table.Column<string>(maxLength: 20, nullable: false),
                    F_ImpDate = table.Column<DateTime>(nullable: false),
                    F_ImpClass = table.Column<string>(maxLength: 30, nullable: true),
                    F_ImpType = table.Column<string>(maxLength: 30, nullable: true),
                    F_Storage = table.Column<string>(maxLength: 50, nullable: false),
                    F_IsAcct = table.Column<bool>(nullable: true),
                    F_AcctPerson = table.Column<string>(maxLength: 20, nullable: true),
                    F_Supplier = table.Column<string>(maxLength: 60, nullable: true),
                    F_SupplierPhone = table.Column<string>(maxLength: 20, nullable: true),
                    F_Contacts = table.Column<string>(maxLength: 20, nullable: true),
                    F_Costs = table.Column<float>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_ImportMaster", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Infection",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_ReportDate = table.Column<DateTime>(nullable: false),
                    F_Item1 = table.Column<float>(nullable: true),
                    F_Item2 = table.Column<float>(nullable: true),
                    F_Item3 = table.Column<float>(nullable: true),
                    F_Item4 = table.Column<float>(nullable: true),
                    F_Item5 = table.Column<float>(nullable: true),
                    F_Item6 = table.Column<float>(nullable: true),
                    F_Item7 = table.Column<float>(nullable: true),
                    F_RecordPerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_Memo = table.Column<string>(maxLength: 200, nullable: true),
                    F_ImangePath = table.Column<string>(maxLength: 100, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Infection", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Instruction",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_FileIndex = table.Column<string>(maxLength: 50, nullable: false),
                    F_FileType = table.Column<string>(maxLength: 10, nullable: true),
                    F_InstructionName = table.Column<string>(maxLength: 200, nullable: false),
                    F_FilePath = table.Column<string>(maxLength: 200, nullable: true),
                    F_FileSize = table.Column<string>(maxLength: 30, nullable: true),
                    F_UploadPerson = table.Column<string>(maxLength: 20, nullable: true),
                    F_UploadDate = table.Column<DateTime>(nullable: false),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Instruction", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Items",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_ParentId = table.Column<string>(maxLength: 50, nullable: true),
                    F_EnCode = table.Column<string>(maxLength: 50, nullable: true),
                    F_FullName = table.Column<string>(maxLength: 50, nullable: true),
                    F_IsTree = table.Column<bool>(nullable: true),
                    F_Layers = table.Column<int>(nullable: true),
                    F_SortCode = table.Column<int>(nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_Description = table.Column<string>(maxLength: 500, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Items", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_ItemsDetail",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_ItemId = table.Column<string>(maxLength: 50, nullable: true),
                    F_ParentId = table.Column<string>(maxLength: 50, nullable: true),
                    F_ItemCode = table.Column<string>(maxLength: 50, nullable: true),
                    F_ItemName = table.Column<string>(maxLength: 50, nullable: true),
                    F_SimpleSpelling = table.Column<string>(maxLength: 50, nullable: true),
                    F_IsDefault = table.Column<bool>(nullable: true),
                    F_Layers = table.Column<int>(nullable: true),
                    F_SortCode = table.Column<int>(nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_Description = table.Column<string>(maxLength: 500, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_ItemsDetail", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_LabDecodeEngine",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Name = table.Column<string>(maxLength: 30, nullable: true),
                    F_Alias = table.Column<string>(maxLength: 30, nullable: true),
                    F_HandleProcess = table.Column<string>(maxLength: 120, nullable: true),
                    F_Desc = table.Column<string>(maxLength: 30, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_LabDecodeEngine", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_LabInstrument",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Code = table.Column<string>(maxLength: 20, nullable: false),
                    F_Name = table.Column<string>(maxLength: 60, nullable: true),
                    F_SerialNo = table.Column<string>(maxLength: 40, nullable: true),
                    F_ShortName = table.Column<string>(maxLength: 30, nullable: true),
                    F_TestType = table.Column<string>(maxLength: 20, nullable: true),
                    F_CommunicateMode = table.Column<string>(maxLength: 20, nullable: true),
                    F_CommunicateConfig = table.Column<string>(maxLength: 2000, nullable: true),
                    F_IsDuplex = table.Column<bool>(nullable: true),
                    F_DecodeEngine = table.Column<string>(maxLength: 30, nullable: true),
                    F_IsExternal = table.Column<bool>(nullable: false),
                    F_Sorter = table.Column<int>(nullable: true),
                    F_WorkStationIp = table.Column<string>(maxLength: 30, nullable: true),
                    F_WorkStationPort = table.Column<int>(nullable: true),
                    F_Supplier = table.Column<string>(maxLength: 60, nullable: true),
                    F_IsRegistered = table.Column<bool>(nullable: true),
                    F_RegistKey = table.Column<string>(maxLength: 200, nullable: true),
                    F_PicPath = table.Column<string>(maxLength: 200, nullable: true),
                    F_Memo = table.Column<string>(maxLength: 500, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_LabInstrument", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_LabInstrumentItem",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_MachineId = table.Column<string>(maxLength: 50, nullable: false),
                    F_Code = table.Column<string>(maxLength: 20, nullable: true),
                    F_Name = table.Column<string>(maxLength: 60, nullable: true),
                    F_CnName = table.Column<string>(maxLength: 60, nullable: true),
                    F_EnName = table.Column<string>(maxLength: 60, nullable: true),
                    F_ResultType = table.Column<bool>(nullable: true),
                    F_Sorter = table.Column<int>(nullable: true),
                    F_KeepDecimal = table.Column<int>(nullable: true),
                    F_ValueUnit = table.Column<string>(maxLength: 30, nullable: true),
                    F_DefaultValue = table.Column<float>(nullable: true),
                    F_ReferenceMinValue = table.Column<float>(nullable: true),
                    F_ReferenceMaxValue = table.Column<float>(nullable: true),
                    F_CriticalMinValue = table.Column<float>(nullable: true),
                    F_CriticalMaxValue = table.Column<float>(nullable: true),
                    F_ReferenceTextValue = table.Column<string>(maxLength: 100, nullable: true),
                    F_IsQualityItem = table.Column<bool>(nullable: true),
                    F_QualityItemId = table.Column<string>(maxLength: 50, nullable: true),
                    F_QualityItemName = table.Column<string>(maxLength: 30, nullable: true),
                    F_ConvertCoefficient = table.Column<float>(nullable: true),
                    F_ChannelValue = table.Column<string>(nullable: true),
                    IsHiddenItem = table.Column<bool>(nullable: false),
                    F_PY = table.Column<string>(maxLength: 20, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_LabInstrumentItem", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_LabItem",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Code = table.Column<string>(maxLength: 20, nullable: true),
                    F_Name = table.Column<string>(maxLength: 60, nullable: true),
                    F_EnName = table.Column<string>(maxLength: 40, nullable: true),
                    F_ShortName = table.Column<string>(maxLength: 30, nullable: true),
                    F_SampleType = table.Column<string>(maxLength: 20, nullable: true),
                    F_Container = table.Column<string>(maxLength: 20, nullable: true),
                    F_CuvetteNo = table.Column<int>(nullable: false),
                    F_Type = table.Column<string>(maxLength: 20, nullable: true),
                    F_ThirdPartyCode = table.Column<string>(maxLength: 30, nullable: true),
                    F_IsExternal = table.Column<bool>(nullable: false),
                    F_Sorter = table.Column<int>(nullable: true),
                    F_IsPeriodic = table.Column<bool>(nullable: true),
                    F_TimeInterval = table.Column<float>(nullable: true),
                    F_Memo = table.Column<string>(maxLength: 500, nullable: true),
                    F_PY = table.Column<string>(maxLength: 20, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_LabItem", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_LabOriginalMessage",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_InstrumentId = table.Column<string>(maxLength: 50, nullable: false),
                    F_Barcode = table.Column<string>(maxLength: 20, nullable: true),
                    F_TestDate = table.Column<DateTime>(nullable: false),
                    F_TestNo = table.Column<int>(nullable: false),
                    F_MessageContent = table.Column<string>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_LabOriginalMessage", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_LabReport",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_TestId = table.Column<long>(nullable: false),
                    F_ItemId = table.Column<string>(maxLength: 50, nullable: false),
                    F_Code = table.Column<string>(nullable: true),
                    F_Name = table.Column<string>(maxLength: 40, nullable: true),
                    F_ShortName = table.Column<string>(maxLength: 20, nullable: true),
                    F_Unit = table.Column<string>(maxLength: 40, nullable: true),
                    F_Result = table.Column<float>(nullable: true),
                    F_ResultText = table.Column<string>(maxLength: 200, nullable: true),
                    F_IsCritical = table.Column<bool>(nullable: true),
                    F_Flag = table.Column<string>(maxLength: 5, nullable: true),
                    F_LowRef = table.Column<float>(nullable: true),
                    F_UpperRef = table.Column<float>(nullable: true),
                    F_MethodName = table.Column<string>(maxLength: 60, nullable: true),
                    F_Sorter = table.Column<int>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_LabReport", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_LabSheet",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_RequestDate = table.Column<DateTime>(nullable: false),
                    F_RequestId = table.Column<long>(nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_Name = table.Column<string>(maxLength: 20, nullable: true),
                    F_BeInfected = table.Column<bool>(nullable: true),
                    F_DialysisNo = table.Column<int>(nullable: true),
                    F_RecordNo = table.Column<string>(maxLength: 20, nullable: true),
                    F_PatientNo = table.Column<string>(maxLength: 20, nullable: true),
                    F_Gender = table.Column<string>(maxLength: 8, nullable: true),
                    F_BirthDay = table.Column<DateTime>(nullable: true),
                    F_InsuranceNo = table.Column<string>(maxLength: 20, nullable: true),
                    F_IdNo = table.Column<string>(maxLength: 20, nullable: true),
                    F_MaritalStatus = table.Column<string>(maxLength: 8, nullable: true),
                    F_IdealWeight = table.Column<float>(nullable: true),
                    F_Height = table.Column<float>(nullable: true),
                    F_PrimaryDisease = table.Column<string>(maxLength: 80, nullable: true),
                    F_Diagnosis = table.Column<string>(maxLength: 80, nullable: true),
                    F_Barcode = table.Column<string>(maxLength: 20, nullable: true),
                    F_SampleType = table.Column<string>(maxLength: 20, nullable: true),
                    F_Container = table.Column<string>(maxLength: 20, nullable: true),
                    F_Status = table.Column<int>(nullable: false),
                    F_DoctorId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DoctorName = table.Column<string>(maxLength: 20, nullable: true),
                    F_OrderTime = table.Column<DateTime>(nullable: true),
                    F_NurseId = table.Column<string>(maxLength: 50, nullable: true),
                    F_NurseName = table.Column<string>(maxLength: 20, nullable: true),
                    F_SamplingTime = table.Column<DateTime>(nullable: true),
                    F_TestUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_TestUserName = table.Column<string>(maxLength: 20, nullable: true),
                    F_TestTime = table.Column<DateTime>(nullable: true),
                    F_AuditUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_AuditUserName = table.Column<string>(maxLength: 20, nullable: true),
                    F_AuditTime = table.Column<DateTime>(nullable: true),
                    F_CheckUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_CheckUserName = table.Column<string>(maxLength: 20, nullable: true),
                    F_CheckTime = table.Column<DateTime>(nullable: true),
                    F_ReportUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_ReportUserName = table.Column<string>(maxLength: 20, nullable: true),
                    F_ReportTime = table.Column<DateTime>(nullable: true),
                    F_PrintTime = table.Column<DateTime>(nullable: true),
                    F_Memo = table.Column<string>(maxLength: 200, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_LabSheet", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_LabSheetItems",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_RequestDate = table.Column<DateTime>(nullable: false),
                    F_RequestId = table.Column<long>(nullable: false),
                    F_Code = table.Column<string>(maxLength: 20, nullable: true),
                    F_Name = table.Column<string>(maxLength: 60, nullable: true),
                    F_EnName = table.Column<string>(maxLength: 40, nullable: true),
                    F_ShortName = table.Column<string>(maxLength: 20, nullable: true),
                    F_SampleType = table.Column<string>(maxLength: 20, nullable: true),
                    F_Container = table.Column<string>(maxLength: 20, nullable: true),
                    F_CuvetteNo = table.Column<int>(nullable: false),
                    F_Type = table.Column<string>(maxLength: 20, nullable: true),
                    F_ThirdPartyCode = table.Column<string>(maxLength: 30, nullable: true),
                    F_IsExternal = table.Column<bool>(nullable: false),
                    F_Sorter = table.Column<int>(nullable: true),
                    F_IsPeriodic = table.Column<bool>(nullable: true),
                    F_TimeInterval = table.Column<float>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_LabSheetItems", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_LabTest",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_InstrumentId = table.Column<string>(maxLength: 50, nullable: false),
                    F_TestDate = table.Column<DateTime>(nullable: false),
                    F_TestNo = table.Column<int>(nullable: false),
                    F_TestId = table.Column<long>(nullable: false),
                    F_Barcode = table.Column<string>(maxLength: 20, nullable: true),
                    F_RequestId = table.Column<long>(nullable: false),
                    F_PatientId = table.Column<string>(maxLength: 50, nullable: true),
                    F_SampleType = table.Column<string>(maxLength: 20, nullable: true),
                    F_Status = table.Column<int>(nullable: false),
                    F_SignInTime = table.Column<DateTime>(nullable: true),
                    F_SignInPerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_TestTime = table.Column<DateTime>(nullable: true),
                    F_TestPerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_RecieveResultTime = table.Column<DateTime>(nullable: true),
                    F_AuditTime = table.Column<DateTime>(nullable: true),
                    F_AuditPerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_LabTest", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_LabTestDuplexItem",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_TestId = table.Column<long>(nullable: false),
                    F_ItemCode = table.Column<string>(nullable: true),
                    F_TestMode = table.Column<string>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_LabTestDuplexItem", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Log",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Date = table.Column<DateTime>(nullable: true),
                    F_Account = table.Column<string>(maxLength: 50, nullable: true),
                    F_NickName = table.Column<string>(maxLength: 50, nullable: true),
                    F_Type = table.Column<string>(maxLength: 50, nullable: true),
                    F_IPAddress = table.Column<string>(maxLength: 50, nullable: true),
                    F_IPAddressName = table.Column<string>(maxLength: 50, nullable: true),
                    F_ModuleId = table.Column<string>(maxLength: 50, nullable: true),
                    F_ModuleName = table.Column<string>(maxLength: 50, nullable: true),
                    F_Result = table.Column<bool>(nullable: true),
                    F_Description = table.Column<string>(maxLength: 50, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Log", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_MachineDisinfection",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Mid = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_PName = table.Column<string>(maxLength: 20, nullable: true),
                    F_PGender = table.Column<string>(maxLength: 10, nullable: true),
                    F_Vid = table.Column<string>(maxLength: 50, nullable: true),
                    F_VisitDate = table.Column<DateTime>(nullable: true),
                    F_VisitNo = table.Column<int>(nullable: true),
                    F_GroupName = table.Column<string>(maxLength: 20, nullable: true),
                    F_ShowOrder = table.Column<int>(nullable: true),
                    F_DialylisBedNo = table.Column<string>(maxLength: 10, nullable: true),
                    F_MachineNo = table.Column<string>(maxLength: 20, nullable: true),
                    F_MachineName = table.Column<string>(maxLength: 20, nullable: true),
                    F_StartTime = table.Column<DateTime>(nullable: true),
                    F_EndTime = table.Column<DateTime>(nullable: true),
                    F_WipeStartTime = table.Column<DateTime>(nullable: true),
                    F_WipeEndTime = table.Column<DateTime>(nullable: true),
                    F_Option1 = table.Column<bool>(nullable: true),
                    F_Option1Value = table.Column<string>(maxLength: 80, nullable: true),
                    F_Option2 = table.Column<bool>(nullable: true),
                    F_Option2Value = table.Column<string>(maxLength: 80, nullable: true),
                    F_Option3 = table.Column<bool>(nullable: true),
                    F_Option4 = table.Column<bool>(nullable: true),
                    F_Option5 = table.Column<bool>(nullable: true),
                    F_Option6 = table.Column<bool>(nullable: true),
                    F_Option6Value = table.Column<string>(maxLength: 80, nullable: true),
                    F_OperatePerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_CheckPerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_Memo = table.Column<string>(maxLength: 200, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_MachineDisinfection", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_MachineProcess",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Mid = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialylisNo = table.Column<int>(nullable: true),
                    F_PName = table.Column<string>(maxLength: 20, nullable: true),
                    F_PGender = table.Column<string>(maxLength: 10, nullable: true),
                    F_Vid = table.Column<string>(maxLength: 50, nullable: false),
                    F_VisitDate = table.Column<DateTime>(nullable: true),
                    F_VisitNo = table.Column<int>(nullable: true),
                    F_GroupName = table.Column<string>(maxLength: 20, nullable: true),
                    F_ShowOrder = table.Column<int>(nullable: true),
                    F_DialylisBedNo = table.Column<string>(maxLength: 10, nullable: true),
                    F_MachineNo = table.Column<string>(maxLength: 20, nullable: true),
                    F_MachineName = table.Column<string>(maxLength: 20, nullable: true),
                    F_Option1 = table.Column<bool>(nullable: true),
                    F_Option2 = table.Column<bool>(nullable: true),
                    F_Option3 = table.Column<bool>(nullable: true),
                    F_Option4 = table.Column<bool>(nullable: true),
                    F_Option5 = table.Column<string>(nullable: true),
                    F_Option6 = table.Column<string>(nullable: true),
                    F_OperateTime = table.Column<DateTime>(nullable: true),
                    F_OperatePerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_Memo = table.Column<string>(maxLength: 200, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_MachineProcess", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Material",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_MaterialType = table.Column<string>(maxLength: 15, nullable: true),
                    F_MaterialCode = table.Column<string>(maxLength: 15, nullable: true),
                    F_MaterialName = table.Column<string>(maxLength: 40, nullable: true),
                    F_MaterialSpec = table.Column<string>(maxLength: 30, nullable: true),
                    F_MaterialUnit = table.Column<string>(maxLength: 20, nullable: true),
                    F_MaterialSupplier = table.Column<string>(maxLength: 80, nullable: true),
                    F_Charges = table.Column<float>(nullable: true),
                    F_MaterialSpell = table.Column<string>(maxLength: 15, nullable: true),
                    F_IsDialyzer = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Material", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_MedicalRecord",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_Title = table.Column<string>(maxLength: 80, nullable: true),
                    F_Content = table.Column<string>(nullable: true),
                    F_AuditFlag = table.Column<bool>(nullable: true),
                    F_ContentTime = table.Column<DateTime>(nullable: true),
                    F_AuditTime = table.Column<DateTime>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_MedicalRecord", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Module",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_ParentId = table.Column<string>(maxLength: 50, nullable: true),
                    F_Layers = table.Column<int>(nullable: true),
                    F_EnCode = table.Column<string>(maxLength: 50, nullable: true),
                    F_FullName = table.Column<string>(maxLength: 50, nullable: true),
                    F_Icon = table.Column<string>(maxLength: 50, nullable: true),
                    F_UrlAddress = table.Column<string>(maxLength: 50, nullable: true),
                    F_Target = table.Column<string>(maxLength: 50, nullable: true),
                    F_IsMenu = table.Column<bool>(nullable: true),
                    F_IsExpand = table.Column<bool>(nullable: true),
                    F_IsPublic = table.Column<bool>(nullable: true),
                    F_AllowEdit = table.Column<bool>(nullable: true),
                    F_AllowDelete = table.Column<bool>(nullable: true),
                    F_SortCode = table.Column<int>(nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_Description = table.Column<string>(maxLength: 500, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Module", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_ModuleButton",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_ModuleId = table.Column<string>(maxLength: 50, nullable: true),
                    F_ParentId = table.Column<string>(maxLength: 50, nullable: true),
                    F_Layers = table.Column<int>(nullable: true),
                    F_EnCode = table.Column<string>(maxLength: 50, nullable: true),
                    F_FullName = table.Column<string>(maxLength: 50, nullable: true),
                    F_Icon = table.Column<string>(maxLength: 50, nullable: true),
                    F_Location = table.Column<int>(nullable: true),
                    F_JsEvent = table.Column<string>(maxLength: 50, nullable: true),
                    F_UrlAddress = table.Column<string>(maxLength: 50, nullable: true),
                    F_Split = table.Column<bool>(nullable: true),
                    F_IsPublic = table.Column<bool>(nullable: true),
                    F_AllowEdit = table.Column<bool>(nullable: true),
                    F_AllowDelete = table.Column<bool>(nullable: true),
                    F_SortCode = table.Column<int>(nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_Description = table.Column<string>(maxLength: 500, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_ModuleButton", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_MonthlySummary",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_Month = table.Column<string>(maxLength: 20, nullable: false),
                    F_HDTimes = table.Column<int>(nullable: true),
                    F_HDDialyzerType = table.Column<string>(maxLength: 50, nullable: true),
                    F_HFTimes = table.Column<int>(nullable: true),
                    F_HFDialyzerType = table.Column<string>(maxLength: 50, nullable: true),
                    F_HDFTimes = table.Column<int>(nullable: true),
                    F_HDFDialyzerType = table.Column<string>(maxLength: 50, nullable: true),
                    F_HDHPTimes = table.Column<int>(nullable: true),
                    F_HDHPDialyzerType = table.Column<string>(maxLength: 50, nullable: true),
                    F_TotalCount = table.Column<int>(nullable: true),
                    F_IdeaWeight = table.Column<float>(nullable: true),
                    F_UrineVolume = table.Column<float>(nullable: true),
                    F_VascularAccess = table.Column<string>(maxLength: 30, nullable: true),
                    F_AccessName = table.Column<string>(maxLength: 30, nullable: true),
                    F_HeparinType = table.Column<string>(maxLength: 50, nullable: true),
                    F_HeparinAmount = table.Column<float>(nullable: true),
                    F_HeparinUnit = table.Column<string>(maxLength: 20, nullable: true),
                    F_BloodSpeed = table.Column<float>(nullable: true),
                    F_TXYCa = table.Column<float>(nullable: true),
                    F_TXYK = table.Column<float>(nullable: true),
                    F_TXYHco3 = table.Column<float>(nullable: true),
                    F_EstimateHours = table.Column<float>(nullable: true),
                    F_WeekTimes = table.Column<int>(nullable: true),
                    F_Complication = table.Column<string>(maxLength: 300, nullable: true),
                    F_Hb = table.Column<float>(nullable: true),
                    F_RBC = table.Column<float>(nullable: true),
                    F_HCT = table.Column<float>(nullable: true),
                    F_WBC = table.Column<float>(nullable: true),
                    F_PLT = table.Column<float>(nullable: true),
                    F_Scr = table.Column<float>(nullable: true),
                    F_Urea = table.Column<float>(nullable: true),
                    F_UA = table.Column<float>(nullable: true),
                    F_K = table.Column<float>(nullable: true),
                    F_Na = table.Column<float>(nullable: true),
                    F_Cl = table.Column<float>(nullable: true),
                    F_HCO3 = table.Column<float>(nullable: true),
                    F_Ca = table.Column<float>(nullable: true),
                    F_P = table.Column<float>(nullable: true),
                    F_Scr1 = table.Column<float>(nullable: true),
                    F_Urea1 = table.Column<float>(nullable: true),
                    F_UA1 = table.Column<float>(nullable: true),
                    F_K1 = table.Column<float>(nullable: true),
                    F_Na1 = table.Column<float>(nullable: true),
                    F_Cl1 = table.Column<float>(nullable: true),
                    F_HCO31 = table.Column<float>(nullable: true),
                    F_Ca1 = table.Column<float>(nullable: true),
                    F_P1 = table.Column<float>(nullable: true),
                    F_URR = table.Column<float>(nullable: true),
                    F_spKtV = table.Column<float>(nullable: true),
                    F_FER = table.Column<float>(nullable: true),
                    F_TS = table.Column<float>(nullable: true),
                    F_iPTH = table.Column<float>(nullable: true),
                    F_CRP = table.Column<float>(nullable: true),
                    F_ALT = table.Column<float>(nullable: true),
                    F_AST = table.Column<float>(nullable: true),
                    F_TP = table.Column<float>(nullable: true),
                    F_ALB = table.Column<float>(nullable: true),
                    F_Glu = table.Column<float>(nullable: true),
                    F_HCV = table.Column<bool>(nullable: true),
                    F_HIV = table.Column<bool>(nullable: true),
                    F_RPR = table.Column<bool>(nullable: true),
                    F_HBsAg = table.Column<bool>(nullable: true),
                    F_HBsAb = table.Column<bool>(nullable: true),
                    F_HBeAg = table.Column<bool>(nullable: true),
                    F_HBeAb = table.Column<bool>(nullable: true),
                    F_HBcAb = table.Column<bool>(nullable: true),
                    F_ExamXP = table.Column<string>(maxLength: 300, nullable: true),
                    F_ExamXDT = table.Column<string>(maxLength: 300, nullable: true),
                    F_ExamCC = table.Column<string>(maxLength: 300, nullable: true),
                    F_DrugSummary = table.Column<string>(maxLength: 1000, nullable: true),
                    F_Content = table.Column<string>(maxLength: 500, nullable: true),
                    F_Suggest = table.Column<string>(maxLength: 500, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_MonthlySummary", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_NutritionDietary",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Type = table.Column<string>(maxLength: 30, nullable: false),
                    F_Name = table.Column<string>(maxLength: 50, nullable: true),
                    F_Py = table.Column<string>(maxLength: 50, nullable: true),
                    F_Col1 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Col2 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Col3 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Col4 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Col5 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Col6 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Col7 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Col8 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Col9 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Col10 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Col11 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Memo = table.Column<string>(maxLength: 200, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_NutritionDietary", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Orders",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_ParentId = table.Column<string>(maxLength: 50, nullable: true),
                    F_SubIndex = table.Column<int>(nullable: true),
                    F_DialysisNo = table.Column<int>(nullable: false),
                    F_RecordNo = table.Column<string>(maxLength: 20, nullable: false),
                    F_VisitDate = table.Column<DateTime>(nullable: true),
                    F_VisitNo = table.Column<int>(nullable: true),
                    F_OrderType = table.Column<string>(maxLength: 30, nullable: true),
                    F_OrderStartTime = table.Column<DateTime>(nullable: true),
                    F_OrderStopTime = table.Column<DateTime>(nullable: true),
                    F_OrderCode = table.Column<string>(maxLength: 50, nullable: false),
                    F_OrderText = table.Column<string>(maxLength: 50, nullable: true),
                    F_OrderSpec = table.Column<string>(maxLength: 30, nullable: true),
                    F_OrderUnitAmount = table.Column<string>(maxLength: 20, nullable: true),
                    F_OrderUnitSpec = table.Column<string>(maxLength: 30, nullable: true),
                    F_OrderAmount = table.Column<float>(nullable: true),
                    F_OrderFrequency = table.Column<string>(maxLength: 20, nullable: true),
                    F_OrderAdministration = table.Column<string>(maxLength: 30, nullable: true),
                    F_IsTemporary = table.Column<bool>(nullable: true),
                    F_Doctor = table.Column<string>(maxLength: 20, nullable: true),
                    F_DoctorOrderTime = table.Column<DateTime>(nullable: true),
                    F_DoctorAuditTime = table.Column<DateTime>(nullable: true),
                    F_OrderStatus = table.Column<int>(nullable: true),
                    F_Nurse = table.Column<string>(maxLength: 20, nullable: true),
                    F_NurseOperatorTime = table.Column<DateTime>(nullable: true),
                    F_Memo = table.Column<string>(maxLength: 150, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Orders", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_OrdersExecLog",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Oid = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_VisitDate = table.Column<DateTime>(nullable: true),
                    F_VisitNo = table.Column<int>(nullable: true),
                    F_OrderType = table.Column<string>(maxLength: 30, nullable: true),
                    F_OrderStartTime = table.Column<DateTime>(nullable: true),
                    F_OrderStopTime = table.Column<DateTime>(nullable: true),
                    F_OrderCode = table.Column<string>(maxLength: 50, nullable: false),
                    F_OrderText = table.Column<string>(maxLength: 50, nullable: true),
                    F_OrderSpec = table.Column<string>(maxLength: 30, nullable: true),
                    F_OrderUnitAmount = table.Column<string>(maxLength: 20, nullable: true),
                    F_OrderUnitSpec = table.Column<string>(maxLength: 30, nullable: true),
                    F_OrderAmount = table.Column<float>(nullable: true),
                    F_OrderFrequency = table.Column<string>(maxLength: 20, nullable: true),
                    F_OrderAdministration = table.Column<string>(maxLength: 30, nullable: true),
                    F_IsTemporary = table.Column<bool>(nullable: true),
                    F_Doctor = table.Column<string>(maxLength: 20, nullable: true),
                    F_DoctorOrderTime = table.Column<DateTime>(nullable: true),
                    F_DoctorAuditTime = table.Column<DateTime>(nullable: true),
                    F_Nurse = table.Column<string>(maxLength: 20, nullable: true),
                    F_NurseId = table.Column<string>(maxLength: 50, nullable: true),
                    F_NurseOperatorTime = table.Column<DateTime>(nullable: true),
                    F_Memo = table.Column<string>(maxLength: 150, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_OrdersExecLog", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Organize",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_ParentId = table.Column<string>(maxLength: 50, nullable: true),
                    F_Layers = table.Column<int>(nullable: true),
                    F_EnCode = table.Column<string>(maxLength: 50, nullable: true),
                    F_FullName = table.Column<string>(maxLength: 50, nullable: true),
                    F_ShortName = table.Column<string>(maxLength: 50, nullable: true),
                    F_CategoryId = table.Column<string>(maxLength: 50, nullable: true),
                    F_ManagerId = table.Column<string>(maxLength: 50, nullable: true),
                    F_TelePhone = table.Column<string>(maxLength: 20, nullable: true),
                    F_MobilePhone = table.Column<string>(maxLength: 20, nullable: true),
                    F_WeChat = table.Column<string>(maxLength: 50, nullable: true),
                    F_Fax = table.Column<string>(maxLength: 20, nullable: true),
                    F_Email = table.Column<string>(maxLength: 50, nullable: true),
                    F_AreaId = table.Column<string>(maxLength: 50, nullable: true),
                    F_Address = table.Column<string>(maxLength: 500, nullable: true),
                    F_AllowEdit = table.Column<bool>(nullable: true),
                    F_AllowDelete = table.Column<bool>(nullable: true),
                    F_SortCode = table.Column<int>(nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_Description = table.Column<string>(maxLength: 200, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Organize", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Patient",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Name = table.Column<string>(maxLength: 20, nullable: true),
                    F_DialysisNo = table.Column<int>(nullable: false),
                    F_RecordNo = table.Column<string>(maxLength: 20, nullable: true),
                    F_PatientNo = table.Column<string>(maxLength: 20, nullable: true),
                    F_Gender = table.Column<string>(maxLength: 8, nullable: true),
                    F_BirthDay = table.Column<DateTime>(nullable: true),
                    F_Charges = table.Column<float>(nullable: true),
                    F_InsuranceNo = table.Column<string>(maxLength: 20, nullable: true),
                    F_IdNo = table.Column<string>(maxLength: 20, nullable: true),
                    F_MaritalStatus = table.Column<string>(maxLength: 8, nullable: true),
                    F_IdealWeight = table.Column<float>(nullable: true),
                    F_Height = table.Column<float>(nullable: true),
                    F_DialysisStartTime = table.Column<DateTime>(nullable: true),
                    F_PrimaryDisease = table.Column<string>(maxLength: 80, nullable: true),
                    F_Diagnosis = table.Column<string>(maxLength: 80, nullable: true),
                    F_Address = table.Column<string>(maxLength: 80, nullable: true),
                    F_InsuranceType = table.Column<string>(maxLength: 30, nullable: true),
                    F_Contacts = table.Column<string>(maxLength: 20, nullable: true),
                    F_PhoneNo = table.Column<string>(maxLength: 20, nullable: true),
                    F_Contacts2 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PhoneNo2 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Trasfer = table.Column<string>(maxLength: 20, nullable: true),
                    F_BloodAbo = table.Column<string>(maxLength: 6, nullable: true),
                    F_BloodRh = table.Column<string>(maxLength: 6, nullable: true),
                    F_Tp = table.Column<string>(maxLength: 6, nullable: true),
                    F_Hiv = table.Column<string>(maxLength: 6, nullable: true),
                    F_HBsAg = table.Column<string>(maxLength: 6, nullable: true),
                    F_HBsAb = table.Column<string>(maxLength: 6, nullable: true),
                    F_HBcAb = table.Column<string>(maxLength: 6, nullable: true),
                    F_HBeAg = table.Column<string>(maxLength: 6, nullable: true),
                    F_HBeAb = table.Column<string>(maxLength: 6, nullable: true),
                    F_HCVAb = table.Column<string>(maxLength: 6, nullable: true),
                    F_MedicalHistory = table.Column<string>(maxLength: 300, nullable: true),
                    F_CardNo = table.Column<string>(maxLength: 20, nullable: true),
                    F_PY = table.Column<string>(maxLength: 20, nullable: true),
                    F_HeadIcon = table.Column<string>(maxLength: 100, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Patient", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_PatVisit",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_Name = table.Column<string>(maxLength: 20, nullable: true),
                    F_Gender = table.Column<string>(maxLength: 8, nullable: true),
                    F_DialysisNo = table.Column<int>(nullable: true),
                    F_RecordNo = table.Column<string>(maxLength: 20, nullable: true),
                    F_BirthDay = table.Column<DateTime>(nullable: true),
                    F_VisitDate = table.Column<DateTime>(nullable: true),
                    F_VisitNo = table.Column<int>(nullable: true),
                    F_PatientSourse = table.Column<string>(maxLength: 20, nullable: true),
                    F_BedNo = table.Column<string>(maxLength: 10, nullable: true),
                    F_InpNo = table.Column<string>(maxLength: 20, nullable: true),
                    F_GroupName = table.Column<string>(maxLength: 20, nullable: true),
                    F_DialysisBedNo = table.Column<string>(maxLength: 10, nullable: true),
                    F_WeightSXTH = table.Column<float>(nullable: true),
                    F_FirstWeightTime = table.Column<DateTime>(nullable: true),
                    F_LastWeightValue = table.Column<float>(nullable: true),
                    F_WeightTQ = table.Column<float>(nullable: true),
                    F_WeightYT = table.Column<float>(nullable: true),
                    F_WeightTH = table.Column<float>(nullable: true),
                    F_WeightST = table.Column<float>(nullable: true),
                    F_DialysisType = table.Column<string>(maxLength: 30, nullable: true),
                    F_ExchangeAmount = table.Column<float>(nullable: true),
                    F_ExchangeSpeed = table.Column<float>(nullable: true),
                    F_Machine = table.Column<string>(maxLength: 30, nullable: true),
                    F_DisinfectTime = table.Column<DateTime>(nullable: true),
                    F_DisinfectPerson = table.Column<string>(maxLength: 10, nullable: true),
                    F_DialyzerType1 = table.Column<string>(maxLength: 50, nullable: true),
                    F_DialyzerType2 = table.Column<string>(maxLength: 50, nullable: true),
                    F_EstimateHours = table.Column<float>(nullable: true),
                    F_VascularAccess = table.Column<string>(maxLength: 30, nullable: true),
                    F_AccessName = table.Column<string>(maxLength: 30, nullable: true),
                    F_DialysisStartTime = table.Column<DateTime>(nullable: true),
                    F_DialysisEndTime = table.Column<DateTime>(nullable: true),
                    F_DialysisHours = table.Column<string>(maxLength: 10, nullable: true),
                    F_PuncturePerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_StartPerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_CheckPerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_EndPerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_HeparinType = table.Column<string>(maxLength: 50, nullable: true),
                    F_HeparinAmount = table.Column<float>(nullable: true),
                    F_HeparinUnit = table.Column<string>(maxLength: 15, nullable: true),
                    F_HeparinAddAmount = table.Column<float>(nullable: true),
                    F_HeparinAddSpeedUnit = table.Column<string>(maxLength: 20, nullable: true),
                    F_LowCa = table.Column<bool>(nullable: true),
                    F_Ca = table.Column<float>(nullable: true),
                    F_K = table.Column<float>(nullable: true),
                    F_Na = table.Column<float>(nullable: true),
                    F_Hco3 = table.Column<float>(nullable: true),
                    F_BloodSpeed = table.Column<float>(nullable: true),
                    F_DialysateTemperature = table.Column<float>(nullable: true),
                    F_Nurse = table.Column<string>(maxLength: 50, nullable: true),
                    F_Doctor = table.Column<string>(maxLength: 50, nullable: true),
                    F_IsAcct = table.Column<bool>(nullable: true),
                    F_IsCritical = table.Column<bool>(nullable: true),
                    F_IsPrint = table.Column<bool>(nullable: true),
                    F_MachineShowAmount = table.Column<float>(nullable: true),
                    F_RealExchangeAmount = table.Column<float>(nullable: true),
                    F_DJMH = table.Column<string>(maxLength: 50, nullable: true),
                    F_DialyzerStatus = table.Column<string>(maxLength: 50, nullable: true),
                    F_FistulaStatus = table.Column<string>(maxLength: 50, nullable: true),
                    F_DuctOpeningStatus = table.Column<string>(maxLength: 50, nullable: true),
                    F_PatientStatus = table.Column<string>(maxLength: 200, nullable: true),
                    F_Reason = table.Column<string>(maxLength: 255, nullable: true),
                    F_SystolicPressure = table.Column<float>(nullable: true),
                    F_DiastolicPressure = table.Column<float>(nullable: true),
                    F_Pulse = table.Column<float>(nullable: true),
                    F_Temperature = table.Column<float>(nullable: true),
                    F_RecordDoctor = table.Column<string>(maxLength: 50, nullable: true),
                    F_DilutionType = table.Column<string>(maxLength: 10, nullable: true),
                    F_Memo = table.Column<string>(nullable: true),
                    F_PGwzsz = table.Column<string>(maxLength: 50, nullable: true),
                    F_PGxftz1 = table.Column<string>(maxLength: 50, nullable: true),
                    F_PGxftz2 = table.Column<string>(maxLength: 50, nullable: true),
                    F_PGxftz3 = table.Column<string>(maxLength: 50, nullable: true),
                    F_PGwzcx1 = table.Column<string>(maxLength: 50, nullable: true),
                    F_PGwzcx2 = table.Column<string>(maxLength: 50, nullable: true),
                    F_PGwzcx3 = table.Column<string>(maxLength: 50, nullable: true),
                    F_PGother = table.Column<string>(maxLength: 150, nullable: true),
                    F_IsArchive = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_PatVisit", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_ProcessFlow",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialylisNo = table.Column<int>(nullable: true),
                    F_PName = table.Column<string>(maxLength: 20, nullable: true),
                    F_PGender = table.Column<string>(maxLength: 10, nullable: true),
                    F_Vid = table.Column<string>(maxLength: 50, nullable: false),
                    F_VisitDate = table.Column<DateTime>(nullable: true),
                    F_VisitNo = table.Column<int>(nullable: true),
                    F_TotalHours = table.Column<float>(nullable: true),
                    F_PreUrea = table.Column<float>(nullable: true),
                    F_PostUrea = table.Column<float>(nullable: true),
                    F_PreWeight = table.Column<float>(nullable: true),
                    F_PostWeight = table.Column<float>(nullable: true),
                    F_Result = table.Column<float>(nullable: true),
                    F_Step_1 = table.Column<bool>(nullable: true),
                    F_Step_2 = table.Column<bool>(nullable: true),
                    F_Step_3 = table.Column<bool>(nullable: true),
                    F_Step_4 = table.Column<bool>(nullable: true),
                    F_Step_5 = table.Column<bool>(nullable: true),
                    F_Step_1_Reason1 = table.Column<bool>(nullable: true),
                    F_Step_1_Reason2 = table.Column<bool>(nullable: true),
                    F_Step_1_Reason3 = table.Column<bool>(nullable: true),
                    F_Step_1_Measures = table.Column<string>(maxLength: 500, nullable: true),
                    F_Step_2_Reason1 = table.Column<bool>(nullable: true),
                    F_Step_2_Reason2 = table.Column<bool>(nullable: true),
                    F_Step_2_Reason3 = table.Column<bool>(nullable: true),
                    F_Step_2_Reason4 = table.Column<bool>(nullable: true),
                    F_Step_2_Measures = table.Column<string>(maxLength: 500, nullable: true),
                    F_Step_3_Measures = table.Column<string>(maxLength: 500, nullable: true),
                    F_Step_4_Measures = table.Column<string>(maxLength: 500, nullable: true),
                    F_Step_5_Measures = table.Column<string>(maxLength: 500, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_ProcessFlow", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Puncture",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_Nurse = table.Column<string>(maxLength: 50, nullable: true),
                    F_OperateTime = table.Column<DateTime>(nullable: true),
                    F_Point1 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Point2 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Distance1 = table.Column<float>(nullable: true),
                    F_Distance2 = table.Column<float>(nullable: true),
                    F_IsSuccess = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_Memo = table.Column<string>(maxLength: 250, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Puncture", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_QualityItem",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_ItemType = table.Column<string>(maxLength: 20, nullable: true),
                    F_OrderNo = table.Column<int>(nullable: true),
                    F_ItemCode = table.Column<string>(maxLength: 20, nullable: true),
                    F_ItemName = table.Column<string>(maxLength: 50, nullable: true),
                    F_HisItemCode = table.Column<string>(maxLength: 20, nullable: true),
                    F_Unit = table.Column<string>(maxLength: 20, nullable: true),
                    F_Result = table.Column<string>(maxLength: 20, nullable: true),
                    F_LowerValue = table.Column<float>(nullable: true),
                    F_UpperValue = table.Column<float>(nullable: true),
                    F_LowerCriticalValue = table.Column<float>(nullable: true),
                    F_UpperCriticalValue = table.Column<float>(nullable: true),
                    F_ResultType = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_Memo = table.Column<string>(maxLength: 500, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_QualityItem", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_QualityItemPartition",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_ParentId = table.Column<string>(maxLength: 50, nullable: true),
                    F_OrderNo = table.Column<int>(nullable: false),
                    F_LowerCheck = table.Column<bool>(nullable: false),
                    F_LowerValue = table.Column<float>(nullable: true),
                    F_UpperCheck = table.Column<bool>(nullable: false),
                    F_UpperValue = table.Column<float>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_QualityItemPartition", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_QualityResult",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_ItemId = table.Column<string>(maxLength: 50, nullable: false),
                    F_ItemType = table.Column<string>(maxLength: 20, nullable: true),
                    F_ReportTime = table.Column<DateTime>(nullable: false),
                    F_ItemCode = table.Column<string>(maxLength: 20, nullable: true),
                    F_ItemName = table.Column<string>(maxLength: 20, nullable: true),
                    F_Unit = table.Column<string>(maxLength: 20, nullable: true),
                    F_Result = table.Column<string>(maxLength: 20, nullable: true),
                    F_Flag = table.Column<string>(maxLength: 20, nullable: true),
                    F_LowerValue = table.Column<float>(nullable: true),
                    F_UpperValue = table.Column<float>(nullable: true),
                    F_LowerCriticalValue = table.Column<float>(nullable: true),
                    F_UpperCriticalValue = table.Column<float>(nullable: true),
                    F_ResultType = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_Memo = table.Column<string>(maxLength: 500, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_QualityResult", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_RecordTemplate",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Title = table.Column<string>(maxLength: 50, nullable: false),
                    F_Content = table.Column<string>(maxLength: 1000, nullable: false),
                    F_IsPrivate = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_RecordTemplate", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Regulation",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_FileIndex = table.Column<string>(maxLength: 50, nullable: false),
                    F_FileType = table.Column<string>(maxLength: 10, nullable: true),
                    F_RegulationName = table.Column<string>(maxLength: 200, nullable: false),
                    F_FilePath = table.Column<string>(maxLength: 200, nullable: true),
                    F_FileSize = table.Column<string>(maxLength: 30, nullable: true),
                    F_UploadPerson = table.Column<string>(maxLength: 20, nullable: true),
                    F_UploadDate = table.Column<DateTime>(nullable: false),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Regulation", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Role",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_OrganizeId = table.Column<string>(maxLength: 50, nullable: true),
                    F_Category = table.Column<int>(nullable: true),
                    F_EnCode = table.Column<string>(maxLength: 50, nullable: true),
                    F_FullName = table.Column<string>(maxLength: 50, nullable: true),
                    F_Type = table.Column<string>(maxLength: 50, nullable: true),
                    F_AllowEdit = table.Column<bool>(nullable: true),
                    F_AllowDelete = table.Column<bool>(nullable: true),
                    F_SortCode = table.Column<int>(nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_Description = table.Column<string>(maxLength: 500, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Role", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_RoleAuthorize",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_ItemType = table.Column<int>(nullable: true),
                    F_ItemId = table.Column<string>(maxLength: 50, nullable: true),
                    F_ObjectType = table.Column<int>(nullable: true),
                    F_ObjectId = table.Column<string>(maxLength: 50, nullable: true),
                    F_SortCode = table.Column<int>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_RoleAuthorize", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Schedule",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_StartDate = table.Column<DateTime>(nullable: true),
                    F_EndDate = table.Column<DateTime>(nullable: true),
                    F_VisitNo = table.Column<int>(nullable: true),
                    F_GroupName = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Schedule", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_ScheduleWeek",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_CurrentDate = table.Column<DateTime>(nullable: true),
                    F_BId = table.Column<string>(maxLength: 50, nullable: false),
                    F_GroupName = table.Column<string>(maxLength: 20, nullable: true),
                    F_DialysisBedNo = table.Column<string>(maxLength: 10, nullable: true),
                    F_Monday1 = table.Column<string>(maxLength: 10, nullable: true),
                    F_Monday2 = table.Column<string>(maxLength: 10, nullable: true),
                    F_Monday3 = table.Column<string>(maxLength: 10, nullable: true),
                    F_Tuesday1 = table.Column<string>(maxLength: 10, nullable: true),
                    F_Tuesday2 = table.Column<string>(maxLength: 10, nullable: true),
                    F_Tuesday3 = table.Column<string>(maxLength: 10, nullable: true),
                    F_Wednesday1 = table.Column<string>(nullable: true),
                    F_Wednesday2 = table.Column<string>(maxLength: 10, nullable: true),
                    F_Wednesday3 = table.Column<string>(maxLength: 10, nullable: true),
                    F_Thursday1 = table.Column<string>(nullable: true),
                    F_Thursday2 = table.Column<string>(maxLength: 10, nullable: true),
                    F_Thursday3 = table.Column<string>(maxLength: 10, nullable: true),
                    F_Friday1 = table.Column<string>(nullable: true),
                    F_Friday2 = table.Column<string>(maxLength: 10, nullable: true),
                    F_Friday3 = table.Column<string>(maxLength: 10, nullable: true),
                    F_Saturday1 = table.Column<string>(nullable: true),
                    F_Saturday2 = table.Column<string>(maxLength: 10, nullable: true),
                    F_Saturday3 = table.Column<string>(maxLength: 10, nullable: true),
                    F_Sunday1 = table.Column<string>(nullable: true),
                    F_Sunday2 = table.Column<string>(maxLength: 10, nullable: true),
                    F_Sunday3 = table.Column<string>(maxLength: 10, nullable: true),
                    F_PId1 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType1 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId2 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType2 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId3 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType3 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId4 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType4 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId5 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType5 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId6 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType6 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId7 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType7 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId8 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType8 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId9 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType9 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId10 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType10 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId11 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType11 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId12 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType12 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId13 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType13 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId14 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType14 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId15 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType15 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId16 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType16 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId17 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType17 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId18 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType18 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId19 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType19 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId20 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType20 = table.Column<string>(maxLength: 20, nullable: true),
                    F_PId21 = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType21 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Sort = table.Column<int>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_ScheduleWeek", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Setting",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType = table.Column<string>(maxLength: 30, nullable: true),
                    F_IsDefault = table.Column<bool>(nullable: false),
                    F_DilutionType = table.Column<string>(maxLength: 30, nullable: true),
                    F_ExchangeAmount = table.Column<float>(nullable: true),
                    F_ExchangeSpeed = table.Column<float>(nullable: true),
                    F_DialyzerType1 = table.Column<string>(maxLength: 50, nullable: true),
                    F_DialyzerType2 = table.Column<string>(maxLength: 50, nullable: true),
                    F_EstimateHours = table.Column<float>(nullable: true),
                    F_VascularAccess = table.Column<string>(maxLength: 30, nullable: true),
                    F_AccessName = table.Column<string>(maxLength: 30, nullable: true),
                    F_HeparinType = table.Column<string>(maxLength: 50, nullable: true),
                    F_HeparinAmount = table.Column<float>(nullable: true),
                    F_HeparinUnit = table.Column<string>(maxLength: 15, nullable: true),
                    F_HeparinAddAmount = table.Column<float>(nullable: true),
                    F_HeparinAddSpeedUnit = table.Column<string>(maxLength: 20, nullable: true),
                    F_LowCa = table.Column<bool>(nullable: true),
                    F_Ca = table.Column<float>(nullable: true),
                    F_K = table.Column<float>(nullable: true),
                    F_Na = table.Column<float>(nullable: true),
                    F_Hco3 = table.Column<float>(nullable: true),
                    F_BloodSpeed = table.Column<float>(nullable: true),
                    F_DialysateTemperature = table.Column<float>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Setting", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_SettingModel",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_DialysisType = table.Column<string>(maxLength: 30, nullable: false),
                    F_DilutionType = table.Column<string>(maxLength: 30, nullable: true),
                    F_ExchangeAmount = table.Column<float>(nullable: true),
                    F_ExchangeSpeed = table.Column<float>(nullable: true),
                    F_DialyzerType1 = table.Column<string>(maxLength: 50, nullable: true),
                    F_DialyzerType2 = table.Column<string>(maxLength: 50, nullable: true),
                    F_EstimateHours = table.Column<float>(nullable: true),
                    F_VascularAccess = table.Column<string>(maxLength: 30, nullable: true),
                    F_AccessName = table.Column<string>(maxLength: 30, nullable: true),
                    F_HeparinType = table.Column<string>(maxLength: 50, nullable: true),
                    F_HeparinAmount = table.Column<float>(nullable: true),
                    F_HeparinUnit = table.Column<string>(maxLength: 15, nullable: true),
                    F_HeparinAddAmount = table.Column<float>(nullable: true),
                    F_HeparinAddSpeedUnit = table.Column<string>(maxLength: 20, nullable: true),
                    F_LowCa = table.Column<bool>(nullable: true),
                    F_Ca = table.Column<float>(nullable: true),
                    F_K = table.Column<float>(nullable: true),
                    F_Na = table.Column<float>(nullable: true),
                    F_Hco3 = table.Column<float>(nullable: true),
                    F_BloodSpeed = table.Column<float>(nullable: true),
                    F_DialysateTemperature = table.Column<float>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_SettingModel", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Storage",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_ItemId = table.Column<string>(maxLength: 50, nullable: false),
                    F_ImpClass = table.Column<string>(maxLength: 30, nullable: true),
                    F_Storage = table.Column<string>(maxLength: 50, nullable: true),
                    F_Code = table.Column<string>(maxLength: 20, nullable: true),
                    F_Name = table.Column<string>(maxLength: 50, nullable: true),
                    F_Spec = table.Column<string>(nullable: true),
                    F_Unit = table.Column<string>(nullable: true),
                    F_Charges = table.Column<float>(nullable: true),
                    F_Amount = table.Column<int>(nullable: true),
                    F_RealAmount = table.Column<int>(nullable: true),
                    F_CheckTime = table.Column<DateTime>(nullable: true),
                    F_Memo = table.Column<string>(nullable: true),
                    F_Py = table.Column<string>(nullable: true),
                    F_MinAmount = table.Column<int>(nullable: true),
                    F_MaxAmount = table.Column<int>(nullable: true),
                    F_TotalCharges = table.Column<float>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Storage", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_StorageLog",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_ItemId = table.Column<string>(maxLength: 50, nullable: false),
                    F_ImpClass = table.Column<string>(maxLength: 30, nullable: true),
                    F_Storage = table.Column<string>(maxLength: 50, nullable: true),
                    F_Code = table.Column<string>(maxLength: 20, nullable: true),
                    F_Name = table.Column<string>(maxLength: 50, nullable: true),
                    F_Spec = table.Column<string>(maxLength: 80, nullable: true),
                    F_Unit = table.Column<string>(maxLength: 50, nullable: true),
                    F_Charges = table.Column<float>(nullable: true),
                    F_CheckType = table.Column<string>(maxLength: 20, nullable: true),
                    F_Amount = table.Column<int>(nullable: true),
                    F_RealAmount = table.Column<int>(nullable: true),
                    F_TotalCharges = table.Column<float>(nullable: true),
                    F_CheckTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_StorageLog", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_TestSampleNo",
                columns: table => new
                {
                    sn = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InstrumentId = table.Column<string>(nullable: false),
                    TestDate = table.Column<DateTime>(nullable: false),
                    SampleNo = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    F_Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_TestSampleNo", x => x.sn);
                });

            migrationBuilder.CreateTable(
                name: "Sys_TransferLog",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_TransferDate = table.Column<DateTime>(nullable: false),
                    F_Status = table.Column<string>(maxLength: 20, nullable: true),
                    F_LocationOut = table.Column<string>(maxLength: 80, nullable: true),
                    F_TransferReason = table.Column<string>(maxLength: 100, nullable: true),
                    F_ExitType = table.Column<string>(maxLength: 20, nullable: true),
                    F_ExitReason = table.Column<string>(maxLength: 150, nullable: true),
                    F_Memo = table.Column<string>(maxLength: 150, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_TransferLog", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Treatment",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_TreatmentCode = table.Column<string>(maxLength: 15, nullable: true),
                    F_TreatmentName = table.Column<string>(maxLength: 40, nullable: true),
                    F_TreatmentSpec = table.Column<string>(maxLength: 30, nullable: true),
                    F_TreatmentUnit = table.Column<string>(maxLength: 20, nullable: true),
                    F_Charges = table.Column<float>(nullable: true),
                    F_TreatmentSpell = table.Column<string>(maxLength: 15, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Treatment", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_UserEntities",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Account = table.Column<string>(maxLength: 50, nullable: true),
                    F_RealName = table.Column<string>(maxLength: 50, nullable: true),
                    F_NickName = table.Column<string>(maxLength: 50, nullable: true),
                    F_Password = table.Column<string>(maxLength: 500, nullable: true),
                    F_HeadIcon = table.Column<string>(maxLength: 50, nullable: true),
                    F_Gender = table.Column<bool>(nullable: true),
                    F_Birthday = table.Column<DateTime>(nullable: true),
                    F_MobilePhone = table.Column<string>(maxLength: 20, nullable: true),
                    F_Email = table.Column<string>(maxLength: 50, nullable: true),
                    F_WeChat = table.Column<string>(maxLength: 50, nullable: true),
                    F_ManagerId = table.Column<string>(maxLength: 50, nullable: true),
                    F_SecurityLevel = table.Column<int>(nullable: true),
                    F_Signature = table.Column<string>(maxLength: 500, nullable: true),
                    F_OrganizeId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DepartmentId = table.Column<string>(maxLength: 500, nullable: true),
                    F_RoleId = table.Column<string>(maxLength: 500, nullable: true),
                    F_DutyId = table.Column<string>(maxLength: 500, nullable: true),
                    F_IsAdministrator = table.Column<bool>(nullable: true),
                    F_SortCode = table.Column<int>(nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_LastLoggedIn = table.Column<DateTimeOffset>(nullable: true),
                    F_SerialNumber = table.Column<string>(nullable: true),
                    F_IsActive = table.Column<bool>(nullable: false),
                    F_Description = table.Column<string>(maxLength: 500, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_UserEntities", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_UserLogOn",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_UserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_UserPassword = table.Column<string>(maxLength: 50, nullable: true),
                    F_UserSecretkey = table.Column<string>(maxLength: 50, nullable: true),
                    F_AllowStartTime = table.Column<DateTime>(nullable: true),
                    F_AllowEndTime = table.Column<DateTime>(nullable: true),
                    F_LockStartDate = table.Column<DateTime>(nullable: true),
                    F_LockEndDate = table.Column<DateTime>(nullable: true),
                    F_FirstVisitTime = table.Column<DateTime>(nullable: true),
                    F_PreviousVisitTime = table.Column<DateTime>(nullable: true),
                    F_LastVisitTime = table.Column<DateTime>(nullable: true),
                    F_ChangePasswordDate = table.Column<DateTime>(nullable: true),
                    F_MultiUserLogin = table.Column<bool>(nullable: true),
                    F_LogOnCount = table.Column<int>(nullable: true),
                    F_UserOnLine = table.Column<bool>(nullable: true),
                    F_Question = table.Column<string>(maxLength: 50, nullable: true),
                    F_AnswerQuestion = table.Column<string>(maxLength: 500, nullable: true),
                    F_CheckIPAddress = table.Column<bool>(nullable: true),
                    F_Language = table.Column<string>(maxLength: 50, nullable: true),
                    F_Theme = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_UserLogOn", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_VascularAccess",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Pid = table.Column<string>(maxLength: 50, nullable: false),
                    F_Type = table.Column<string>(maxLength: 30, nullable: true),
                    F_VascularAccess = table.Column<string>(maxLength: 30, nullable: true),
                    F_OperateTime = table.Column<DateTime>(nullable: false),
                    F_BloodSpeed_Idea = table.Column<float>(nullable: true),
                    F_BloodSpeed = table.Column<float>(nullable: true),
                    F_FirstUseTime = table.Column<DateTime>(nullable: true),
                    F_DiscardTime = table.Column<DateTime>(nullable: true),
                    F_BodyPart = table.Column<string>(maxLength: 30, nullable: true),
                    F_BodyPosition = table.Column<string>(maxLength: 30, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_DisabledRemark = table.Column<string>(maxLength: 200, nullable: true),
                    F_Memo = table.Column<string>(maxLength: 500, nullable: true),
                    F_Complication = table.Column<string>(maxLength: 500, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true),
                    F_PicPath = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_VascularAccess", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_WaterMBrief",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_RecordDate = table.Column<DateTime>(nullable: false),
                    F_Value1 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value2 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value3 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value4 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value5 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value6 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value7 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value8 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value9 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value10 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value11 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value12 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value13 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value14 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value15 = table.Column<bool>(nullable: true),
                    F_Value16 = table.Column<bool>(nullable: true),
                    F_Value17 = table.Column<bool>(nullable: true),
                    F_Value18 = table.Column<bool>(nullable: true),
                    F_Memo = table.Column<string>(maxLength: 200, nullable: true),
                    F_OperatePerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_OperatePersonName = table.Column<string>(maxLength: 30, nullable: true),
                    F_CheckPerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_CheckPersonName = table.Column<string>(maxLength: 30, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_WaterMBrief", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_WaterMDisinfect",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_DisinfectDate = table.Column<DateTime>(nullable: false),
                    F_DisinfectantName = table.Column<string>(maxLength: 60, nullable: true),
                    F_DisinfectantVolume = table.Column<float>(nullable: true),
                    F_DisinfectantUnit = table.Column<string>(maxLength: 20, nullable: true),
                    F_DisinfectType = table.Column<string>(maxLength: 60, nullable: true),
                    F_Option1 = table.Column<bool>(nullable: true),
                    F_RecyclingStartTime = table.Column<DateTime>(nullable: true),
                    F_RecyclingEndTime = table.Column<DateTime>(nullable: true),
                    F_RecyclingMinutes = table.Column<float>(nullable: true),
                    F_SoakStartTime = table.Column<DateTime>(nullable: true),
                    F_SoakEndTime = table.Column<DateTime>(nullable: true),
                    F_SoakMinutes = table.Column<float>(nullable: true),
                    F_RinseStartTime = table.Column<DateTime>(nullable: true),
                    F_RinseEndTime = table.Column<DateTime>(nullable: true),
                    F_RinseMinutes = table.Column<float>(nullable: true),
                    F_Option2 = table.Column<bool>(nullable: true),
                    F_Option3 = table.Column<bool>(nullable: true),
                    F_OperatePerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_CheckPerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_WaterMDisinfect", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_WaterMLog",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_LogDate = table.Column<DateTime>(nullable: false),
                    F_Value1 = table.Column<float>(nullable: true),
                    F_Value2 = table.Column<float>(nullable: true),
                    F_Value3 = table.Column<float>(nullable: true),
                    F_Value4 = table.Column<float>(nullable: true),
                    F_Value5 = table.Column<float>(nullable: true),
                    F_Value6 = table.Column<float>(nullable: true),
                    F_Value7 = table.Column<float>(nullable: true),
                    F_Value8 = table.Column<float>(nullable: true),
                    F_Value9 = table.Column<float>(nullable: true),
                    F_Option1 = table.Column<bool>(nullable: true),
                    F_Option2 = table.Column<bool>(nullable: true),
                    F_Option3 = table.Column<bool>(nullable: true),
                    F_Option4 = table.Column<bool>(nullable: true),
                    F_Option5 = table.Column<bool>(nullable: true),
                    F_Option6 = table.Column<bool>(nullable: true),
                    F_Memo = table.Column<string>(maxLength: 200, nullable: true),
                    F_OperatePerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_CheckPerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_LastModifyTime = table.Column<DateTime>(nullable: true),
                    F_LastModifyUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteTime = table.Column<DateTime>(nullable: true),
                    F_DeleteUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_DeleteMark = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_WaterMLog", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_WaterMObservation",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_RecordDate = table.Column<DateTime>(nullable: false),
                    F_Value1 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value2 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value3 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value4 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value5 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value6 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value7 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value8 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value9 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value10 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value11 = table.Column<bool>(nullable: true),
                    F_Value12 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value13 = table.Column<string>(maxLength: 20, nullable: true),
                    F_Value14 = table.Column<bool>(nullable: true),
                    F_Memo = table.Column<string>(maxLength: 200, nullable: true),
                    F_OperatePerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_OperatePersonName = table.Column<string>(maxLength: 30, nullable: true),
                    F_CheckPerson = table.Column<string>(maxLength: 50, nullable: true),
                    F_CheckPersonName = table.Column<string>(maxLength: 30, nullable: true),
                    F_EnabledMark = table.Column<bool>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_WaterMObservation", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_WeightLog",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Vid = table.Column<string>(maxLength: 50, nullable: false),
                    F_Value = table.Column<float>(nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_WeightLog", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_YTWeightLog",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_Vid = table.Column<string>(maxLength: 50, nullable: false),
                    F_Value = table.Column<float>(nullable: true),
                    F_CreatorUserId = table.Column<string>(maxLength: 50, nullable: true),
                    F_CreatorTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_YTWeightLog", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_UserToken",
                columns: table => new
                {
                    F_Id = table.Column<string>(maxLength: 50, nullable: false),
                    F_AccessTokenHash = table.Column<string>(nullable: true),
                    F_AccessTokenExpiresDateTime = table.Column<DateTimeOffset>(nullable: false),
                    F_RefreshTokenIdHash = table.Column<string>(nullable: true),
                    F_RefreshTokenIdHashSource = table.Column<string>(nullable: true),
                    F_RefreshTokenExpiresDateTime = table.Column<DateTimeOffset>(nullable: false),
                    F_UserId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_UserToken", x => x.F_Id);
                    table.ForeignKey(
                        name: "FK_Sys_UserToken_Sys_UserEntities_F_UserId",
                        column: x => x.F_UserId,
                        principalTable: "Sys_UserEntities",
                        principalColumn: "F_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sys_UserToken_F_UserId",
                table: "Sys_UserToken",
                column: "F_UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_Area");

            migrationBuilder.DropTable(
                name: "Sys_AuditLog");

            migrationBuilder.DropTable(
                name: "Sys_Barcode");

            migrationBuilder.DropTable(
                name: "Sys_Billing");

            migrationBuilder.DropTable(
                name: "Sys_BillingModel");

            migrationBuilder.DropTable(
                name: "Sys_Book");

            migrationBuilder.DropTable(
                name: "Sys_ConclusionTemplate");

            migrationBuilder.DropTable(
                name: "Sys_CriticalValue");

            migrationBuilder.DropTable(
                name: "Sys_DataReportJS");

            migrationBuilder.DropTable(
                name: "Sys_DATestReport");

            migrationBuilder.DropTable(
                name: "Sys_DbBackup");

            migrationBuilder.DropTable(
                name: "Sys_DialysisMachine");

            migrationBuilder.DropTable(
                name: "Sys_DialysisObservation");

            migrationBuilder.DropTable(
                name: "Sys_DialysisSchedule");

            migrationBuilder.DropTable(
                name: "Sys_DoseGuide");

            migrationBuilder.DropTable(
                name: "Sys_Drugs");

            migrationBuilder.DropTable(
                name: "Sys_Evaluation");

            migrationBuilder.DropTable(
                name: "Sys_FileIndex");

            migrationBuilder.DropTable(
                name: "Sys_FilterIP");

            migrationBuilder.DropTable(
                name: "Sys_IdeaWeight");

            migrationBuilder.DropTable(
                name: "Sys_ImportDetail");

            migrationBuilder.DropTable(
                name: "Sys_ImportMaster");

            migrationBuilder.DropTable(
                name: "Sys_Infection");

            migrationBuilder.DropTable(
                name: "Sys_Instruction");

            migrationBuilder.DropTable(
                name: "Sys_Items");

            migrationBuilder.DropTable(
                name: "Sys_ItemsDetail");

            migrationBuilder.DropTable(
                name: "Sys_LabDecodeEngine");

            migrationBuilder.DropTable(
                name: "Sys_LabInstrument");

            migrationBuilder.DropTable(
                name: "Sys_LabInstrumentItem");

            migrationBuilder.DropTable(
                name: "Sys_LabItem");

            migrationBuilder.DropTable(
                name: "Sys_LabOriginalMessage");

            migrationBuilder.DropTable(
                name: "Sys_LabReport");

            migrationBuilder.DropTable(
                name: "Sys_LabSheet");

            migrationBuilder.DropTable(
                name: "Sys_LabSheetItems");

            migrationBuilder.DropTable(
                name: "Sys_LabTest");

            migrationBuilder.DropTable(
                name: "Sys_LabTestDuplexItem");

            migrationBuilder.DropTable(
                name: "Sys_Log");

            migrationBuilder.DropTable(
                name: "Sys_MachineDisinfection");

            migrationBuilder.DropTable(
                name: "Sys_MachineProcess");

            migrationBuilder.DropTable(
                name: "Sys_Material");

            migrationBuilder.DropTable(
                name: "Sys_MedicalRecord");

            migrationBuilder.DropTable(
                name: "Sys_Module");

            migrationBuilder.DropTable(
                name: "Sys_ModuleButton");

            migrationBuilder.DropTable(
                name: "Sys_MonthlySummary");

            migrationBuilder.DropTable(
                name: "Sys_NutritionDietary");

            migrationBuilder.DropTable(
                name: "Sys_Orders");

            migrationBuilder.DropTable(
                name: "Sys_OrdersExecLog");

            migrationBuilder.DropTable(
                name: "Sys_Organize");

            migrationBuilder.DropTable(
                name: "Sys_Patient");

            migrationBuilder.DropTable(
                name: "Sys_PatVisit");

            migrationBuilder.DropTable(
                name: "Sys_ProcessFlow");

            migrationBuilder.DropTable(
                name: "Sys_Puncture");

            migrationBuilder.DropTable(
                name: "Sys_QualityItem");

            migrationBuilder.DropTable(
                name: "Sys_QualityItemPartition");

            migrationBuilder.DropTable(
                name: "Sys_QualityResult");

            migrationBuilder.DropTable(
                name: "Sys_RecordTemplate");

            migrationBuilder.DropTable(
                name: "Sys_Regulation");

            migrationBuilder.DropTable(
                name: "Sys_Role");

            migrationBuilder.DropTable(
                name: "Sys_RoleAuthorize");

            migrationBuilder.DropTable(
                name: "Sys_Schedule");

            migrationBuilder.DropTable(
                name: "Sys_ScheduleWeek");

            migrationBuilder.DropTable(
                name: "Sys_Setting");

            migrationBuilder.DropTable(
                name: "Sys_SettingModel");

            migrationBuilder.DropTable(
                name: "Sys_Storage");

            migrationBuilder.DropTable(
                name: "Sys_StorageLog");

            migrationBuilder.DropTable(
                name: "Sys_TestSampleNo");

            migrationBuilder.DropTable(
                name: "Sys_TransferLog");

            migrationBuilder.DropTable(
                name: "Sys_Treatment");

            migrationBuilder.DropTable(
                name: "Sys_UserLogOn");

            migrationBuilder.DropTable(
                name: "Sys_UserToken");

            migrationBuilder.DropTable(
                name: "Sys_VascularAccess");

            migrationBuilder.DropTable(
                name: "Sys_WaterMBrief");

            migrationBuilder.DropTable(
                name: "Sys_WaterMDisinfect");

            migrationBuilder.DropTable(
                name: "Sys_WaterMLog");

            migrationBuilder.DropTable(
                name: "Sys_WaterMObservation");

            migrationBuilder.DropTable(
                name: "Sys_WeightLog");

            migrationBuilder.DropTable(
                name: "Sys_YTWeightLog");

            migrationBuilder.DropTable(
                name: "Sys_UserEntities");
        }
    }
}
