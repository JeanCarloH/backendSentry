﻿namespace TaskManagementAPI.Services
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role);
    }
}
