using User_Auth.DTOs;
using User_Auth.Models;

namespace User_Auth.Repositories;

public interface IAuthRepository
{
    Task<User> RegisterAsync(RegisterDto dto);

    Task<User?> SignInAsync(LoginDto dto);
}