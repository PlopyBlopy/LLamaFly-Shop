﻿using Core.Interfaces.Constraints;

namespace Core.Models
{
    public class Category : ModelBase, ICategoryConstraints, ICategoryTitleConstraint
    {
        public string Title { get; }
        public Guid? ParentCategoryId { get; }
        public ICollection<Category> SubcategoriesIds { get; }

        public Category(Guid id, string title, Guid? parentCategoryId, ICollection<Category> subcategoriesIds) : base(id)
        {
            Title = title;
            ParentCategoryId = parentCategoryId;
            SubcategoriesIds = subcategoriesIds;
        }
    }
}