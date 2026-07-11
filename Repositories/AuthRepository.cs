using Microsoft.EntityFrameworkCore;
using User_Auth.Data;
using User_Auth.DTOs;
using User_Auth.Models;

namespace User_Auth.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserAuthContext _context;

    public AuthRepository(UserAuthContext context)
    {
        _context = context;
    }

    public async Task<User> RegisterAsync(RegisterDto dto)
    {
        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            Username = dto.Username,
            //Password = dto.Password
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> SignInAsync(LoginDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (user == null)
        {
            return null;
        }

        bool isValidPassword = BCrypt.Net.BCrypt.Verify(
            dto.Password,
            user.Password);

        if (!isValidPassword)
        {
            return null;
        }

        return user;
    }
}