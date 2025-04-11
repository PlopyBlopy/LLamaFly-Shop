﻿namespace Core.Contracts.Messages
{
    public record UserCreateMessage(Guid Id, string Role, string Login, string Email, string PhoneNumber, DateTime CreatedAt, DateTime UpdatedAt);
}