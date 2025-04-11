namespace Core.Extensions.Errors
{
    public static class ProductErrors
    {
        public static readonly string NotFoundProducts = $"Does not found products.";

        public static string NotNullProduct()
            => $"The product does not exist.";

        public static string NotFoundProduct(Guid id)
            => $"The product with the id {id} was not found.";

        //public static ValidationError ProductCreateRequestValidatorError() => new ValidationError($"Product.ProductCreateRequestValidator.ProductCreateRequest");
    }
}