using Core.Contracts.Dtos;
using Core.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface ICustomerController
    {
        Task<ActionResult<CustomerDto>> Get(CancellationToken ct);

        Task<IActionResult> Update(CustomerUpdateRequest request, CancellationToken ct);
    }
}