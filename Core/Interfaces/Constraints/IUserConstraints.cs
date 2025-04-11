namespace Core.Interfaces.Constraints
{
    public interface IUserConstraints : IConstraints
    {
        //TODO: Constraints для ролей и их валидация

        public enum UserRole
        {
            admin,
            seller,
            customer
        }

        //public static readonly Dictionary<UserRole, string> Roles = new Dictionary<UserRole, string>
        //{
        //    { UserRole.admin, "admin" },
        //    { UserRole.seller, "seller" },
        //    { UserRole.customer, "customer" }
        //};

        const int MIN_LOGIN_LENGTH = 1;
        const int MAX_LOGIN_LENGTH = 50;

        const int MIN_EMAIL_LENGTH = 1;
        const int MAX_EMAIL_LENGTH = 50;

        const int MIN_PHONENUMBER_LENGTH = 1;
        const int MAX_PHONENUMBER_LENGTH = 50;

        const int MIN_PASSWORD_LENGTH = 1;
        const int MAX_PASSWORD_LENGTH = 50;
    }
}