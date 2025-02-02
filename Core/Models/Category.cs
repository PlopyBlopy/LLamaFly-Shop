using Core.Interfaces.Constraints;

namespace Core.Models
{
    public class Category : ICategoryConstraints, ICategoryTitleConstraint
    {
        public Guid Id { get; }
        public string Title { get; }
        public Guid ParentCategoryId { get; }
        public ICollection<Guid> SubcategoriesIds { get; }

        public Category(Guid id, string title, Guid parentCategoryId, ICollection<Guid> subcategoriesIds)
        {
            Id = id;
            Title = title;
            ParentCategoryId = parentCategoryId;
            SubcategoriesIds = subcategoriesIds;
        }
    }
}