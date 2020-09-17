using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dmt.DM.Data;

namespace Dmt.DM.Domain.Entity.LabLis
{
    public class BarcodeEntity : EntityBase<long>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long sn { get; set; }
        [Required]
        public DateTime BarcodeDate { get; set; }
        [Required]
        public int Barcode { get; set; }
        public long? RequestId { get; set; }
        public DateTime? BarcodeCreateTime { get; set; }
    }
}
