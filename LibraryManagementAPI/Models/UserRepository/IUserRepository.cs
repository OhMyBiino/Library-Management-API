using LibraryManagementModels;

namespace LibraryManagementAPI.Models.UserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetUsersAsync();
        Task<UserModel> GetUserByIdAsync(int Id);
        Task<IEnumerable<UserModel>> SearchAsync(string? Query);
        Task<UserModel> CreateUserAsync(UserModel newUser);
        Task<UserModel> UpdateUserAsync(UserModel user);
        Task<UserModel> DeleteUserAsync(int Id);
    }
}