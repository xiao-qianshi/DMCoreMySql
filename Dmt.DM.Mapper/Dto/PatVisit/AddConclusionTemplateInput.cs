namespace Dmt.Dm.Domain.Dto.PatVisit
{
    public class AddConclusionTemplateInput
    {
        public string title { get; set; }
        public string content { get; set; }
        public bool? isPrivate { get; set; } = false;
    }
}