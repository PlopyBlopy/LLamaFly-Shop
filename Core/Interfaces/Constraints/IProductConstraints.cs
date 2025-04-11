namespace Core.Interfaces.Constraints
{
    public interface IProductConstraints : IConstraints
    {
        const int MIN_TITLE_LENGTH = 5;
        const int MAX_TITLE_LENGTH = 200;

        const int MIN_Description_LENGTH = 0;
        const int MAX_Description_LENGTH = 6000;

        const decimal MIN_PRICE = 50;
        const decimal MAX_PRICE = 10000000;

        public const double MIN_RATING = 0.0;
        public const double MAX_RATING = 5.0;
    }
}