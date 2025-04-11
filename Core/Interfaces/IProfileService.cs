namespace Core.Interfaces
{
    public interface IProfileService : IService
    {
        Task<bool> IsSellerExist(Guid id, CancellationToken ct);
    }
}