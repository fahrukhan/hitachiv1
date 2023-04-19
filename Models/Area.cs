namespace hitachiv1.Models
{
    public class Area: BaseEntity
    {
        public  int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}