using MiniApi.Models;

namespace MiniApi.Services;

public interface ITokenService
{
    string BuildToken(UserDto user);
}