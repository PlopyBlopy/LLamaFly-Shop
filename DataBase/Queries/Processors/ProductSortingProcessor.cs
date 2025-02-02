using Core.Entities;
using System.Linq.Expressions;

namespace DataBase.Queries.Processors
{
    public class ProductSortingProcessor
    {
        public static IQueryable<ProductEntity> Process(IQueryable<ProductEntity> query, string sortProp, string sortOrder)
        {
            if (!string.IsNullOrEmpty(sortProp))
            {
                Expression<Func<ProductEntity, object>> selectorProp = GetSelectorProp(sortProp.ToLower());

                query = sortOrder == "desc"
                    ? query.OrderByDescending(selectorProp)
                    : query.OrderBy(selectorProp);
            }

            return query;
        }

        private static Expression<Func<ProductEntity, object>> GetSelectorProp(string sortProp)
        {
            switch (sortProp)
            {
                case "title":
                    return product => product.Title;

                case "price":
                    return product => product.Price;

                case "rating":
                    return product => product.Rating;

                case "createAt":
                    return product => product.CreateAt;

                default:
                    return product => product.Id;
            }
        }
    }
}