namespace Dmt.DM.Mapper.Dto
{
    public class ApiResultModel
    {
        /// <summary>
        /// 状态代码
        /// </summary>
        public int StatusCode { get; set; }
       
        /// <summary>
        /// 返回的数据
        /// </summary>
        public object Data { get; set; }
     
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
       
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}