namespace Domain.Models
{
    public class Product : BaseModel
    {

        public string Name { get; set; } = string.Empty;

        public int Price { get; set; }

        public string Description { get; set; } = string.Empty;

        public string LongDescription { get; set; } = string.Empty;

        public string Picture { get; set; } = string.Empty;

        public Guid CategoryId { get; set; }

    }
}
