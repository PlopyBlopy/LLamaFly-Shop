namespace Core.Interfaces.Constraints
{
    public interface IProductPriceConstraint : IProductConstraints
    {
        const decimal MIN_PRICE = 50;
        const decimal MAX_PRICE = 10000000;
    }
}