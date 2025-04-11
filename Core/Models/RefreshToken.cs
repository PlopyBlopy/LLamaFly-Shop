namespace Core.Models
{
    public class RefreshToken
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public string Token { get; }
        public DateTime Expires { get; }
        public bool IsRevoked { get; }
        public DateTime CreatedAt { get; }

        public RefreshToken(Guid id, Guid userId, string token, DateTime expires, bool isRevoked, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            Token = token;
            Expires = expires;
            IsRevoked = isRevoked;
            CreatedAt = createdAt;
        }
    }
}