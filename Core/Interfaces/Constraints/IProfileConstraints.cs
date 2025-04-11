namespace Core.Interfaces.Constraints
{
    public interface IProfileConstraints : IConstraints
    {
        const int MIN_NAME_LENGTH = 1;
        const int MAX_NAME_LENGTH = 50;

        const int MIN_SURNAME_LENGTH = 1;
        const int MAX_SURNAME_LENGTH = 50;

        const int MIN_PATRONYMIC_LENGTH = 1;
        const int MAX_PATRONYMIC_LENGTH = 50;
    }
}