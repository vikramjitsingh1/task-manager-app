using TmModule.Infrastructure;
using TmModule.Modules.Users.Models;
using TmModule.Modules.Users.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TmModule.Modules.Users.Services
{
    public class UsersService
    {
        // Dependency injection of the repository
        private readonly IRepositoryUsers _repository;
        public UsersService(IRepositoryUsers repository)
        {
            _repository = repository;
        }

        
        // Method to get all users
        public Task<List<UsersTable>> GetAllUsersAsync()
        {
            return _repository.GetAllUsersAsync();
        }

        // Method to get a user by ID
        public Task<UsersTable?> GetUserByIdAsync(int userId)
        {
            return _repository.GetUserByIdAsync(userId);
        }

        // Method to add a new user
        public async Task AddUserAsync(UsersTable user)
        {
            ArgumentNullException.ThrowIfNull(user);

            await _repository.AddAsync(user);
        }

        // Method to save changes (if needed)
        public Task SaveChangesAsync()
        {
            return _repository.SaveChangesAsync();
        }
    }
}
