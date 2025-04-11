using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface ICategoryController
    {
        Task<ActionResult> AddCategory([FromBody] CategoryCreateRequest request, CancellationToken ct);

        Task<ActionResult> AddCategories([FromBody] IEnumerable<CategoryCreateRequest> request, CancellationToken ct);

        Task<ActionResult<CategoryHierarchyDto>> GetCategory(Guid id, CancellationToken ct);

        Task<ActionResult<IEnumerable<CategoryHierarchyDto>>> GetCategories(CancellationToken ct);

        Task<IActionResult> UpdateCategory(CategoryUpdateRequest request, CancellationToken ct);

        Task<IActionResult> DeleteCategory([FromQuery] Guid id, CancellationToken ct);

        Task<ActionResult<bool>> IsCategoryExists(Guid id, CancellationToken ct);
    }
}