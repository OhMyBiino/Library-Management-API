using LibraryManagementAPI.Database;
using LibraryManagementModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Models.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryContext _context;

        public UserRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }

        public async Task<UserModel> GetUserByIdAsync(Guid Id)
        {
            var user = await _context.Users.FindAsync(Id);

            return user;
        }

        public async Task<UserModel> GetUserByEmail(string email) 
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

            return existingUser;
        }

        public async Task<UserModel> GetUserByUsername(string username)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(user => user.UserName == username);

            return existingUser;
        }

        public async Task<IEnumerable<UserModel>> SearchAsync(string entity)
        {
            IQueryable<UserModel> query = _context.Users;

            if (!String.IsNullOrEmpty(entity))
            {
                query = query.Where(e => e.FirstName.Contains(entity) || e.LastName.Contains(entity)
                    || e.UserName.Contains(entity) || e.Email.Contains(entity) || e.PhoneNumber.Contains(entity));
            }

            var results = query.ToList();
            return results;
        }

        public async Task<UserModel> CreateUserAsync(UserModel newUser)
        {
            var addedUser = await _context.AddAsync(newUser);

            await _context.SaveChangesAsync();
            return addedUser.Entity;
        }

        public async Task<UserModel> UpdateUserAsync(UserModel updatedUser)
        {
            var userToUpdate = await _context.Users.FindAsync(updatedUser.UserId);

            if (userToUpdate != null)
            {
                userToUpdate.FirstName = updatedUser.FirstName;
                userToUpdate.LastName = updatedUser.LastName;
                userToUpdate.UserName = updatedUser.UserName;
                userToUpdate.PasswordHash = updatedUser.PasswordHash;
                userToUpdate.PhoneNumber = updatedUser.PhoneNumber;
            }

            await _context.SaveChangesAsync();
            return userToUpdate;
        }

        public async Task<UserModel> DeleteUserAsync(Guid Id)
        {
            var userToDelete = await _context.Users.FindAsync(Id);

            if (userToDelete != null)
            {
                var removedUSer = _context.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }

            return userToDelete;
        }

    }
}