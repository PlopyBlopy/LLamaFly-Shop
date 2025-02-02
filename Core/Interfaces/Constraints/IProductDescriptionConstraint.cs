namespace Core.Interfaces.Constraints
{
    public interface IProductDescriptionConstraint : IProductConstraints
    {
        const int MIN_Description_LENGTH = 0;
        const int MAX_Description_LENGTH = 700;
    }
}