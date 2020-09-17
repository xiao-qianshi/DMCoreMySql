namespace Dmt.DM.Mapper.ValueObject
{
    public class DrugsSelectOptions
    {
        public string F_Id { get; set; }
        public string F_DrugCode { get; set; }
        public string F_DrugName { get; set; }
        public string F_DrugSpec { get; set; }
        public string F_DrugUnit { get; set; }
        public float? F_DrugMiniAmount { get; set; }
        public string F_DrugMiniSpec { get; set; }
        public int? F_DrugMiniPackage { get; set; }
        public string F_DrugAdministration { get; set; }
        public string F_DrugEfficacy { get; set; }
        public string F_DrugSupplier { get; set; }
        public float? F_Charges { get; set; }
        public string F_DrugSpell { get; set; }
        public bool? F_IsHeparin { get; set; }
    }
}
