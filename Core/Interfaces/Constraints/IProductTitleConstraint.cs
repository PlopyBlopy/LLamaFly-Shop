namespace Core.Interfaces.Constraints
{
    public interface IProductTitleConstraint : IProductConstraints
    {
        const int MIN_TITLE_LENGTH = 5;
        const int MAX_TITLE_LENGTH = 100;
    }
}