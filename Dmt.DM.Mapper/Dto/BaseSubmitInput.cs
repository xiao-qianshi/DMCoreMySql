using Dmt.DM.Domain;

namespace Dmt.DM.Mapper.Dto
{
    public class BaseSubmitInput<T>where T : class
    {
        public T Entity { get; set; }
        public string KeyValue { get; set; }
    }
}
