using System;
namespace Dmt.Dm.Domain.Dto.PatVisit
{
    public class GetSumCountInput
    {
        public DateTime? visitDate { get; set; } = DateTime.Today;
    }
}