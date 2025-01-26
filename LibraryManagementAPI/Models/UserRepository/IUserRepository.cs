using LibraryManagementModels;

namespace LibraryManagementAPI.Models.UserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetUsersAsync();
        Task<UserModel> GetUserByIdAsync(Guid Id);
        Task<UserModel> GetUserByEmail(string email);
        Task<UserModel> GetUserByUsername(string username);
        Task<IEnumerable<UserModel>> SearchAsync(string Query);
        Task<UserModel> CreateUserAsync(UserModel newUser);
        Task<UserModel> UpdateUserAsync(UserModel user);
        Task<UserModel> DeleteUserAsync(Guid Id);
    }
}