using System.Collections.Generic;

namespace Dmt.DM.Mapper.Dto.Patient
{
    public class GetPattientHeadIconOutput
    {
        public List<PattientHeadIcon> Rows { get; set; }
        public GetPattientHeadIconOutput()
        {
            Rows = new List<PattientHeadIcon>();
        }
    }

    public class PattientHeadIcon
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FileBase64Str { get; set; }
        public string FileExtension { get; set; }
    }
}