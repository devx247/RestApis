using MiniApi.Models;

namespace MiniApi.Services;

public class UserRepositoryService : IUserRepositoryService
{
    private readonly List<UserDto> _users = new List<UserDto>()
    {
        new("erich.brunner@live.at", "devzone$1"),
        new("helga.brunner@live.at", "chefin$1")
    };

    public UserDto? GetUser(UserModel userModel)
    {
        return _users.FirstOrDefault(
            u => u.Username.Equals(userModel.UserName) && u.Password.Equals(userModel.Password));
    }
}