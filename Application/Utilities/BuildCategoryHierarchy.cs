using Core.Contracts.Dtos;

namespace Application.Utilities
{
    public class BuildCategoryHierarchy
    {
        public List<CategoryDto> BuildHierarchy(IEnumerable<CategoryFlatDto> flatList)
        {
            var allCategories = flatList.Select(c => new CategoryDto(c.Id, c.Title, c.ParentCategoryId, c.Level)).ToList();

            var lookup = allCategories.ToLookup(c => c.ParentCategoryId);

            var rootCategories = lookup[null].ToList();

            foreach (var category in allCategories)
            {
                category.Subcategories = lookup[category.Id].ToList();
            }
            return rootCategories;
        }
    }
}