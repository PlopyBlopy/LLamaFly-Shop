namespace Core.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public DateTime CreateAt { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SellerId { get; set; }
    }
}