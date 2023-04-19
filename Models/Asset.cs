namespace hitachiv1.Models
{
    public class Asset: BaseEntity
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime CapitalizedOn { get; set; } = DateTime.Now;
        public string PhysicalCheckNumber { get; set; } = string.Empty;
        public string InventoryNumber { get; set; } = string.Empty;
        public Category? Category { get; set; }
        public AssetClass? AssetClass { get; set; }
        public Area? Area { get; set; }
        public Movement? Movement { get; set; }
    }
}