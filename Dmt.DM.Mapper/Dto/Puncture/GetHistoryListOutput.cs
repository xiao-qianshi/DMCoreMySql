using System;
using System.Collections.Generic;

namespace Dmt.Dm.Domain.Dto.Puncture
{
    public class GetHistoryListOutput
    {
        public string imagePath { get; set; }
        //public PunctureImage punctureImage { get; set; }
        public List<PunctureItem> punctureItems { get; set; }
        public GetHistoryListOutput()
        {
            punctureItems = new List<PunctureItem>();
        }
    }

    //public class PunctureImage
    //{
    //    public string imagePath { get; set; }
    //    public string fileName { get; set; }
    //    public string fileExt { get; set; }
    //}

    public class PunctureItem
    {
        public string id { get; set; }
        public string nurseId { get; set; }
        public string nurseName { get; set; }
        public DateTime? operateTime { get; set; }
        public string point1 { get; set; }
        public string point2 { get; set; }
        public bool isSucess { get; set; } = true;
        public string memo { get; set; }
    }
}