namespace Core.Interfaces.Constraints
{
    public interface IImageOrderConstraint : IImageConstraints
    {
        const int MIN_ORDER = 0;
        const int MAX_ORDER = 9;
    }
}