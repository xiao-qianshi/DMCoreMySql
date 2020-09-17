using System;
using System.ComponentModel.DataAnnotations;

namespace Dmt.DM.Domain.Entity.PatientManage
{
    public class EvaluationEntity : IEntity<EvaluationEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
   
        [Required]
        [StringLength(50)]
        public string F_Pid { get; set; }
        /// <summary>
        /// 关联透析记录
        /// </summary>
        public DateTime F_VisitDate { get; set; }
        public bool? Rsfs1 { get; set; }
        public bool? Rsfs2 { get; set; }
        public bool? Rsfs3 { get; set; }
        public bool? Xy1 { get; set; }
        public bool? Xy2 { get; set; }
        public bool? Xy3 { get; set; }
        public bool? Xy4 { get; set; }
        [StringLength(150)]
        public string Xyvalue1 { get; set; }
        [StringLength(150)]
        public string Xyvalue2 { get; set; }
        public bool? Xl1 { get; set; }
        public bool? Xl2 { get; set; }
        public bool? Xl3 { get; set; }
        public bool? Xl4 { get; set; }
        [StringLength(150)]
        public string Xlvalue1 { get; set; }
        [StringLength(150)]
        public string Xlvalue2 { get; set; }
        public bool? Hx1 { get; set; }
        public bool? Hx2 { get; set; }
        public bool? Hx3 { get; set; }
        public bool? Hx4 { get; set; }
        public bool? Hx5 { get; set; }
        public bool? Hx6 { get; set; }
        public bool? Tw1 { get; set; }
        public bool? Tw2 { get; set; }
        public bool? Tw3 { get; set; }
        [StringLength(150)]
        public string Twvalue1 { get; set; }
        public bool? Shzlnl1 { get; set; }
        public bool? Shzlnl2 { get; set; }
        public bool? Shzlnl3 { get; set; }
        public bool? Tl1 { get; set; }
        public bool? Tl2 { get; set; }
        public bool? Tl3 { get; set; }
        public bool? Ww1 { get; set; }
        public bool? Ww2 { get; set; }
        public bool? Ww3 { get; set; }
        public bool? Sy1 { get; set; }
        public bool? Sy2 { get; set; }
        public bool? Sy3 { get; set; }
        public bool? Yslkz1 { get; set; }
        public bool? Yslkz2 { get; set; }
        public bool? Yslkz3 { get; set; }
        public bool? Sm1 { get; set; }
        public bool? Sm2 { get; set; }
        public bool? Sm3 { get; set; }
        public bool? Nl1 { get; set; }
        public bool? Nl2 { get; set; }
        [StringLength(150)]
        public string Nlvalue1 { get; set; }
        public bool? Dd1 { get; set; }
        public bool? Dd2 { get; set; }
        [StringLength(150)]
        public string Ddvalue1 { get; set; }
        public bool? Cx1 { get; set; }
        public bool? Cx2 { get; set; }
        [StringLength(150)]
        public string Cxvalue1 { get; set; }
        public bool? Yyqk1 { get; set; }
        public bool? Yyqk2 { get; set; }
        public bool? Yyqk3 { get; set; }
        public bool? Yyqk4 { get; set; }
        [StringLength(150)]
        public string Yyqkvalue1 { get; set; }
        public bool? Qctxhqk1 { get; set; }
        public bool? Qctxhqk2 { get; set; }
        public bool? Qctxhqk3 { get; set; }
        public bool? Qctxhqk4 { get; set; }
        public bool? Qctxhqk5 { get; set; }
        public bool? Qctxhqk6 { get; set; }
        [StringLength(150)]
        public string Qctxhqkvalue1 { get; set; }
        public bool? Tsqk1 { get; set; }
        public bool? Tsqk2 { get; set; }
        public bool? Tsqk3 { get; set; }
        public bool? Tsqk4 { get; set; }
        [StringLength(150)]
        public string Tsqkvalue1 { get; set; }
        [StringLength(150)]
        public string Tsqkvalue2 { get; set; }
        public bool? Nlccdqk1 { get; set; }
        public bool? Nlccdqk2 { get; set; }
        public bool? Nlccdqk3 { get; set; }
        public bool? Nlccdqk4 { get; set; }
        public bool? Nlccdqk5 { get; set; }
        public bool? Nlccdqk6 { get; set; }
        public bool? Nlccdqk7 { get; set; }
        public bool? Sfsctx1 { get; set; }
        public bool? Sfsctx2 { get; set; }
        public DateTime? Sctxdate { get; set; }
        [StringLength(150)]
        public string Sfsctxvalue1 { get; set; }
        [StringLength(150)]
        public string Sfsctxvalue2 { get; set; }
        [StringLength(150)]
        public string Sfsctxvalue3 { get; set; }
        [StringLength(150)]
        public string Wytxcfvalue1 { get; set; }
        [StringLength(150)]
        public string Wytxcfvalue2 { get; set; }
        [StringLength(150)]
        public string Wytxcfvalue3 { get; set; }
        public bool? Wytxywbs1 { get; set; }
        public bool? Wytxywbs2 { get; set; }
        public bool? Wz1 { get; set; }
        public bool? Wz2 { get; set; }
        public bool? Wz3 { get; set; }
        public bool? Wz4 { get; set; }
        [StringLength(150)]
        public string Wzvalue1 { get; set; }
        public bool? Skwg1 { get; set; }
        public bool? Skwg2 { get; set; }
        public bool? Skwg3 { get; set; }
        public bool? Skwg4 { get; set; }
        public bool? Skwg5 { get; set; }
        public bool? Skwg6 { get; set; }
        [StringLength(150)]
        public string Skwgvalue1 { get; set; }
        public bool? Hy1 { get; set; }
        public bool? Hy2 { get; set; }
        public bool? Dgll1 { get; set; }
        public bool? Dgll2 { get; set; }
        public bool? Dgll3 { get; set; }
        public bool? Ywfr1 { get; set; }
        public bool? Ywfr2 { get; set; }
        [StringLength(150)]
        public string Ywfrvalue1 { get; set; }
        public bool? Place1 { get; set; }
        public bool? Place2 { get; set; }
        [StringLength(150)]
        public string Placevalue1 { get; set; }
        [StringLength(150)]
        public string Placevalue2 { get; set; }
        public bool? Sctxcczz1 { get; set; }
        public bool? Sctxcczz2 { get; set; }
        public bool? Sctxcczz3 { get; set; }
        public bool? Sctxcczz4 { get; set; }
        public bool? Sctxcczz5 { get; set; }
        public bool? Xgzy1 { get; set; }
        public bool? Xgzy2 { get; set; }
        public bool? Xgzy3 { get; set; }
        public bool? Nlcsxl1 { get; set; }
        public bool? Nlcsxl2 { get; set; }
        [StringLength(150)]
        public string Nlcsxlvalue1 { get; set; }
        public bool? Nlsynx1 { get; set; }
        public bool? Nlsynx2 { get; set; }
        public bool? Nlsynx3 { get; set; }
        public bool? Nlsynx4 { get; set; }
        public bool? Jkjyfs1 { get; set; }
        public bool? Jkjyfs2 { get; set; }
        public bool? Jkjyfs3 { get; set; }
        public bool? Jkjyfs4 { get; set; }
        public bool? Yszd1 { get; set; }
        public bool? Yszd2 { get; set; }
        public bool? Yszd3 { get; set; }
        public bool? Yszd4 { get; set; }
        public bool? Yszd5 { get; set; }
        public bool? Ydzd1 { get; set; }
        public bool? Ydzd2 { get; set; }
        public bool? Ydzd3 { get; set; }
        public bool? Xgtlzd1 { get; set; }
        public bool? Xgtlzd2 { get; set; }
        public bool? Xgtlzd3 { get; set; }
        public bool? Xgtlzd4 { get; set; }
        public bool? Tzgl1 { get; set; }
        public bool? Tzgl2 { get; set; }
        public bool? Tzgl3 { get; set; }
        public bool? Sjz1 { get; set; }
        public bool? Sjz2 { get; set; }
        [StringLength(150)]
        public string Sjzvalue1 { get; set; }
        [StringLength(50)]
        public string Checkperson { get; set; }
        [StringLength(50)]
        public string Checknurse { get; set; }
        public bool? F_EnabledMark { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        [StringLength(50)]
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        [StringLength(50)]
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        [StringLength(50)]
        public string F_DeleteUserId { get; set; }
        public bool? F_DeleteMark { get; set; }
    }
}
