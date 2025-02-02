namespace Core.Entities
{
    public class CategoryEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Guid ParentCategoryId { get; set; }
        public CategoryEntity? ParentCategory { get; set; }
        public ICollection<CategoryEntity> Subcategories { get; set; } = new List<CategoryEntity>();
    }
}