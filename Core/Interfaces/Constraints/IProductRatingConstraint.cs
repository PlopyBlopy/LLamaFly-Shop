namespace Core.Interfaces.Constraints
{
    public interface IProductRatingConstraint : IProductConstraints
    {
        public const double MIN_RATING = 0.0;
        public const double MAX_RATING = 5.0;
    }
}