namespace Core.Interfaces.Constraints
{
    public interface IImageContentTypeConstraint : IImageConstraints
    {
        const string TYPE = "image";

        enum ContentType
        {
            png,
            jpg,
            jpeg
        }
    }
}