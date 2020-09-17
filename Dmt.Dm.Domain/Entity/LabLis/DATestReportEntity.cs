using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.LabLis
{
    public class DATestReportEntity : IEntity<DATestReportEntity>, ICreationAudited
    {
        /// <summary>
        /// 迪安条形码
        /// </summary>
        [StringLength(20)]
        public string BARCODE { get; set; }
        /// <summary>
        /// 送检单位
        /// </summary>
        [StringLength(50)]
        public string SAMPLEFROM { get; set; }
        /// <summary>
        /// 样本类型 血清 
        /// </summary>
        [StringLength(20)]
        public string SAMPLETYPE { get; set; }
        /// <summary>
        /// 采样时间
        /// </summary>
        public DateTime? COLLECTDDATE { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime? SUBMITDATE { get; set; }
        /// <summary>
        /// 测试编码
        /// </summary>
        [StringLength(20)]
        public string TESTCODE { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? APPRDATE { get; set; }
        /// <summary>
        /// 所属子公司 北京迪安
        /// </summary>
        [StringLength(50)]
        public string DEPT { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        [StringLength(50)]
        public string SERVGRP { get; set; }
        /// <summary>
        /// 检测人员 ID
        /// </summary>
        [StringLength(20)]
        public string USRNAMID { get; set; }
        /// <summary>
        /// 检测人
        /// </summary>
        [StringLength(20)]
        public string USRNAM { get; set; }
        /// <summary>
        /// 审核人员 ID
        /// </summary>
        [StringLength(20)]
        public string APPRVEDBYID { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        [StringLength(20)]
        public string APPRVEDBY { get; set; }
        /// <summary>
        /// 病人姓名
        /// </summary>
        [StringLength(20)]
        public string PATIENTNAME { get; set; }
        /// <summary>
        /// 病人类型（门诊/体检/住院）
        /// </summary>
        [StringLength(20)]
        public string PATIENTCATEGORY { get; set; } = "门诊";
        /// <summary>
        /// 送检医生
        /// </summary>
        [StringLength(20)]
        public string DOCTOR { get; set; }
        /// <summary>
        /// 性别（男/女）
        /// </summary>
        [StringLength(10)]
        public string SEX { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public float? AGE { get; set; }
        /// <summary>
        /// 年龄单位（岁/月）
        /// </summary>
        [StringLength(10)]
        public string AGEUNIT { get; set; }
        /// <summary>
        /// 项目名称 糖类抗原 125
        /// </summary>
        [StringLength(50)]
        public string SINONYM { get; set; }
        /// <summary>
        /// 项目简称 CA125
        /// </summary>
        [StringLength(20)]
        public string SHORTNAME { get; set; }
        /// <summary>
        /// 单位 kU/L
        /// </summary>
        [StringLength(30)]
        public string UNITS { get; set; }
        /// <summary>
        /// 结果 23.10 
        /// </summary>
        [StringLength(40)]
        public string FINAL { get; set; }
        /// <summary>
        /// 分析项目 糖类抗原 125[CA125] 
        /// </summary>
        [StringLength(100)]
        public string ANALYTE { get; set; }
        /// <summary>
        /// 参考范围 ≤35.00
        /// </summary>
        [StringLength(80)]
        public string DISPLOWHIGH { get; set; }
        /// <summary>
        /// 参考范围 女 ≤35.00
        /// </summary>
        [StringLength(80)]
        public string DISPLOWHIGH_F { get; set; }
        /// <summary>
        /// 参考范围 男 ≤35.00
        /// </summary>
        [StringLength(80)]
        public string DISPLOWHIGH_M { get; set; }
        /// <summary>
        /// 结果异常标志位（↑↓） L
        /// </summary>
        [StringLength(6)]
        public string RN10 { get; set; }
        /// <summary>
        /// 对接唯一标识字段 213-9706 
        /// </summary>
        [StringLength(20)]
        public string S { get; set; }
        /// <summary>
        /// 结果异常标记位（补充）
        /// </summary>
        public string RN20 { get; set; }
        /// <summary>
        /// 项目名称英文 CA125(Carbohydrate Antigen 125)
        /// </summary>
        [StringLength(80)]
        public string SYNONIM_EN { get; set; }
        /// <summary>
        /// 建议与解释或实验室对此样本信 息的注释说明
        /// </summary>
        [StringLength(500)]
        public string COMMENTS { get; set; }
        /// <summary>
        /// 临床诊断
        /// </summary>
        [StringLength(80)]
        public string DIAGNOSIS { get; set; }
        /// <summary>
        ///  乙肝耐药检测突变比例
        /// </summary>
        [StringLength(20)]
        public string GENE_MUTATIONS_RATIO { get; set; }
        /// <summary>
        ///  参考值下限(根据病人性别年龄 匹配到的实际参考区间)
        /// </summary>
        [StringLength(20)]
        public string LOWB { get; set; }
        /// <summary>
        ///  参考值上限(根据病人性别年龄 匹配到的实际参考区间)
        /// </summary>
        [StringLength(20)]
        public string HIGHB { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        [StringLength(40)]
        public string CLINICNAME { get; set; }
        /// <summary>
        /// 危急值标志（有危急值时为 Y, 否则为 N）
        /// </summary>
        [StringLength(6)]
        public string RANGE_FLG { get; set; } = "N";
        /// <summary>
        /// 危急值描述（RANGE_FLG 为 Y 时有内容，否则为空） 
        /// </summary>
        [StringLength(500)]
        public string RANGE_DESC { get; set; }
        /// <summary>
        /// 病人电话
        /// </summary>
        [StringLength(20)]
        public string PATIENTPHONE { get; set; }
        /// <summary>
        ///  项目排序
        /// </summary>
        public int? SORTER { get; set; }
        /// <summary>
        /// 结果值为"Other"时，实际结果内 容保存字段 
        /// </summary>
        [StringLength(500)]
        public string LONGTXT { get; set; }
        /// <summary>
        /// 检验仪器
        /// </summary>
        [StringLength(40)]
        public string EQUIPMENT { get; set; }
        /// <summary>
        /// 方法学
        /// </summary>
        [StringLength(40)]
        public string METHODNAME { get; set; }
        /// <summary>
        /// 送检医生电话
        /// </summary>
        [StringLength(20)]
        public string DOCTORPHONE { get; set; }
        /// <summary>
        /// 分析项编码
        /// </summary>
        [StringLength(30)]
        public string ANALYTE_ORIGREC { get; set; }
        /// <remarks/>
        [StringLength(40)]
        public string SAMPLESTATUS { get; set; }
        /// <remarks/>
        public DateTime? TESTDATE { get; set; }
        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
    }
}
