namespace Dmt.DM.Mapper.ValueObject
{
    /// <summary>
    /// 缓存对象结构 患者列表
    /// </summary>
    public class PatientSelectOptions
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int RecordNo { get; set; }
        public string Py { get; set; }
        public float? IdealWeight { get; set; }
        public bool BeInfected { get; set; }
    }
}
