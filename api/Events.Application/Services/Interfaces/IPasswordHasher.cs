﻿namespace Events.Application.Services.Interfaces;

public interface IPasswordHasher
{
    string GenerateHash(string password);

    bool VerifyPassword(string password, string passwordHash);
}
