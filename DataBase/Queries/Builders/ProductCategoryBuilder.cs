using Core.Entities;
using DataBase.Interfaces;

namespace DataBase.Queries.Builders
{
    public class ProductCategoryBuilder
    {
        private const string DEFAULT_CATEGORY_ID = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
        private readonly IProductServiceDbContext _context;

        public ProductCategoryBuilder(IProductServiceDbContext context)
        {
            _context = context;
        }

        public static IQueryable<ProductEntity> Build(IQueryable<ProductEntity> query, Guid categoryId)
        {
            if (!IsCategory(categoryId))
            {
                categoryId = Guid.Parse(DEFAULT_CATEGORY_ID);
            }

            query.Where(e => e.CategoryId == categoryId);

            return query;
        }

        private static bool IsCategory(Guid categoryId)
        {
            //_context.Categories.First(e => e.Id == categoryId);
            return false;
        }
    }
}