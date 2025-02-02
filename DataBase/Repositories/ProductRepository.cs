using Core.Contracts.Dto;
using Core.Entities;
using Core.Interfaces;
using Core.Models;
using DataBase.Interfaces;
using DataBase.Queries.Processors;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly IProductServiceDbContext _context;

    public ProductRepository(IProductServiceDbContext context)
    {
        _context = context;
    }

    public async Task<(bool, string)> Add(ProductEntity entity, CancellationToken ct)
    {
        await _context.Products.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);

        return (true, "The product has been added.");
    }

    public async Task<ProductDto?> GetById(Guid id, CancellationToken ct)
    {
        return await _context.Products
        .AsNoTracking()
        .Where(e => e.Id == id)
        .Select(e => new ProductDto(
            e.Id,
            e.Title,
            e.Description,
            e.Price,
            e.Rating,
            e.CreateAt,
            e.CategoryId,
            e.SellerId
        ))
        .FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<ProductDto>?> GetAll(CancellationToken ct)
    {
        return await _context.Products
        .AsNoTracking()
        .Select(e => new ProductDto(
            e.Id,
            e.Title,
            e.Description,
            e.Price,
            e.Rating,
            e.CreateAt,
            e.CategoryId,
            e.SellerId
        ))
        .ToListAsync(ct);
    }

    //0-----------------------

    private async Task<HashSet<Guid>> GetCategoryIdsWithChildrenAsync(Guid categoryId, CancellationToken ct)
    {
        var result = new HashSet<Guid>();

        // Рекурсивно собираем все дочерние категории
        await AddCategoryIdsRecursiveAsync(categoryId, result, ct);

        return result;
    }

    private async Task AddCategoryIdsRecursiveAsync(Guid categoryId, HashSet<Guid> result, CancellationToken ct)
    {
        // Добавляем текущую категорию
        result.Add(categoryId);

        // Получаем дочерние категории из базы данных
        var subcategories = await _context.Categories
            .Where(c => c.ParentCategoryId == categoryId)
            .Select(c => c.Id)
            .ToListAsync(ct);

        // Рекурсивно добавляем дочерние категории
        foreach (var subcategoryId in subcategories)
        {
            await AddCategoryIdsRecursiveAsync(subcategoryId, result, ct);
        }
    }

    //------------------

    public async Task<IEnumerable<ProductCardDto>?> GetCards(ProductFiltersDto dto, CancellationToken ct)
    {
        IQueryable<ProductEntity> query = _context.Products
            .AsNoTracking()
            .Where(e => string.IsNullOrWhiteSpace(dto.Search) || e.Title.ToLower().Contains(dto.Search.ToLower()));

        if (dto.CategoryId.HasValue)
        {
            // Получаем все идентификаторы категорий (включая дочерние)
            var categoryIds = await GetCategoryIdsWithChildrenAsync(dto.CategoryId.Value, ct);

            // Фильтруем товары по категориям
            query = query.Where(e => categoryIds.Contains(e.CategoryId));
        }

        query = ProductSortingProcessor.Process(query, dto.SortProp, dto.SortOrder);

        //CategoryEntity category = await _context.Categories.FirstAsync(e => e.Id == dto.CategoryId, ct);

        //if (category == null)
        //{
        //    query.Where(e => e.CategoryId == Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"));
        //}
        //else

        //CategoryEntity category = await _context.Categories.AsNoTracking().Include(e => e.Subcategories).FirstAsync(e => e.Id == dto.CategoryId, ct);

        //query = query.Where(e => e.CategoryId == dto.CategoryId);

        IEnumerable<ProductCardDto> dtos = await query
            .Select(e => new ProductCardDto(e.Id, e.Title, e.Price, e.Rating))
            .ToListAsync(ct);

        return dtos;
    }

    public async Task Update(ProductEntity entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}