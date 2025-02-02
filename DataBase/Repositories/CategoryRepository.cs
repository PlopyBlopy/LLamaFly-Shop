using AutoMapper;
using Core.Configs;
using Core.Contracts.Dto;
using Core.Entities;
using Core.Interfaces;
using DataBase.Interfaces;
using Microsoft.EntityFrameworkCore;
using DataBase.Utilities;

namespace DataBase.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IProductServiceDbContext _context;
        private readonly IMapper _mapper;
        private readonly CategoryEntityToDtoRecursionConverter _converter;

        public CategoryRepository(IProductServiceDbContext context, IMapper mapper, CategoryEntityToDtoRecursionConverter converter)
        {
            _context = context;
            _mapper = mapper;
            _converter = converter;
        }

        public async Task<(bool, string)> Add(CategoryEntity entity, CancellationToken ct)
        {
            CategoryEntity? parentEntity = await GetOnlyParentById(entity.ParentCategoryId, ct);

            if (parentEntity == null)
            {
                return (false, "The ID of the parent category was not found.");
            }

            parentEntity.Subcategories.Add(entity);
            entity.ParentCategory = parentEntity;

            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync(ct);

            return (true, "The category has been added.");
        }

        public async Task<CategoryDto?> GetById(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();

            //var rootCategory = await _context.Categories
            //    .AsNoTracking()
            //    .Include(e => e.Subcategories) // Включаем подкатегории
            //    .FirstOrDefaultAsync(de => de.Id == id, ct);

            //CategoryDto dto = MapToDto(rootCategory);
            //return dto;

            //    return await _context.Categories
            //            .AsNoTracking()
            //            .Include(c => c.ParentCategory)
            //            .Include(c => c.Subcategories)
            //            .Where(c => c.Id == id)
            //            .Select(c => new CategoryDto(
            //                c.Id,
            //                c.Title,
            //                c.ParentCategoryId,
            //                c.Subcategories.Select(sc => new CategoryDto(
            //                    sc.Id,
            //                    sc.Title,
            //                    sc.ParentCategoryId,
            //                    null! // Рекурсивно загружаем подкатегории
            //                )).ToList()
            //            ))
            //            .FirstOrDefaultAsync(ct);
        }

        public async Task<CategoryEntity?> GetOnlyParentById(Guid parentCategoryId, CancellationToken ct)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(e => e.Id == parentCategoryId, ct);
        }

        public async Task<IEnumerable<CategoryDto>?> GetAll(CancellationToken ct)
        {
            Guid defaultCategory = CategoryGuidConfig.DEFAULT_CATEGORY_GUID;

            var rootCategory = await _context.Categories
                .AsNoTracking()
                .Include(e => e.Subcategories)
                .ThenInclude(e => e.Subcategories)
                .ThenInclude(e => e.Subcategories)
                .FirstOrDefaultAsync(de => de.Id == defaultCategory, ct);

            CategoryDto rootCategoryDto = _converter.Convert(rootCategory);

            return rootCategoryDto.Subcategories;
        }

        public async Task Update(CategoryEntity entity, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}