namespace Core.Interfaces.Constraints
{
    public interface IImageLengthConstraint : IImageConstraints
    {
        const long MAX_LENGTH = 5242880;
        const string MAX_LENGTH_UNIT = "5 MB";
    }
}