using MiniApi.Models;

namespace MiniApi.Services;

public interface IUserRepositoryService
{
    UserDto? GetUser(UserModel userModel);
}