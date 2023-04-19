namespace hitachiv1.Models
{
    public class Movement: BaseEntity
    {
        public  int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}