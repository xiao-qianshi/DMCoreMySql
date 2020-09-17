namespace Dmt.Dm.Domain.Dto.RecordTemplate
{
    public class AddRecordTemplateInput
    {
        public string title { get; set; }
        public string content { get; set; }
        public bool? isPrivate { get; set; } = false;
    }
}