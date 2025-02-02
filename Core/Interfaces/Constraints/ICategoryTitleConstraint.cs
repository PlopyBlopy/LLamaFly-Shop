namespace Core.Interfaces.Constraints
{
    public interface ICategoryTitleConstraint : ICategoryConstraints
    {
        const int MIN_TITLE_LENGTH = 5;
        const int MAX_TITLE_LENGTH = 60;
    }
}