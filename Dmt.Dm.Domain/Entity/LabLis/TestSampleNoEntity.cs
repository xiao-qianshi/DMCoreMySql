using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dmt.DM.Data;

namespace Dmt.DM.Domain.Entity.LabLis
{
    public class TestSampleNoEntity : EntityBase<long>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long sn { get; set; }
        [Required]
        public string InstrumentId { get; set; }
        [Required]
        public DateTime TestDate { get; set; }
        public int SampleNo { get; set; }
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        public override long F_Id { get; set; }
    }
}
