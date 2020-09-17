namespace Dmt.DM.Mapper.ValueObject
{
    /// <summary>
    /// 缓存对象结构 检验项目列表
    /// </summary>
    public class LabItemSelectOptions
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Container { get; set; }
        public int CuvetteNo { get; set; }
        public string EnName { get; set; }
        public bool IsExternal { get; set; }
        public bool? IsPeriodic { get; set; }
        public string Memo { get; set; }
        public string Name { get; set; }
        public string Py { get; set; }
        public string SampleType { get; set; }
        public string ShortName { get; set; }
        public int? Sorter { get; set; }
        public string ThirdPartyCode { get; set; }
        public float? TimeInterval { get; set; }
        public string Type { get; set; }
    }
}