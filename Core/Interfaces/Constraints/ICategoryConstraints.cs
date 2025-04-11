namespace Core.Interfaces.Constraints
{
    public interface ICategoryConstraints : IConstraints
    {
        const int MIN_TITLE_LENGTH = 4;
        const int MAX_TITLE_LENGTH = 60;
    }
}