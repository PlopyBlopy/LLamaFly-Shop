namespace Core.Interfaces
{
    public interface IPasswordHasher
    {
        public string Hashing(string password);

        public bool Verify(string password, string passwordHash);
    }
}