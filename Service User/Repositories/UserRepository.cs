using FluentResults;
using Microsoft.EntityFrameworkCore;
using Service_User.Context;
using Service_User.Models;

namespace Service_User.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task<Result<User>> CreateUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Result.Ok(user);  // Correct use of FluentResults.Result.Ok()
            }
            catch (Exception ex)
            {
                return Result.Fail<User>($"Error creating user: {ex.Message}");  // Correct use of FluentResults.Result.Fail()
            }
        }

        // Read (by id)
        public async Task<Result<User>> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.id == userId);
                
                if (user == null)
                    return Result.Fail<User>("User not found.");  // Correct use of FluentResults.Result.Fail()
                
                return Result.Ok(user);  // Correct use of FluentResults.Result.Ok()
            }
            catch (Exception ex)
            {
                return Result.Fail<User>($"Error retrieving user: {ex.Message}");  // Correct use of FluentResults.Result.Fail()
            }
        }

        // Update
        public async Task<Result<User>> UpdateUserAsync(Guid userId, User updatedUser)
        {
            try
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.id == userId);
                
                if (existingUser == null)
                    return Result.Fail<User>("User not found.");  // Correct use of FluentResults.Result.Fail()

                // Update properties
                existingUser.username = updatedUser.username;
                existingUser.gitId = updatedUser.gitId;
                existingUser.avatar = updatedUser.avatar;

                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();

                return Result.Ok(existingUser);  // Correct use of FluentResults.Result.Ok()
            }
            catch (Exception ex)
            {
                return Result.Fail<User>($"Error updating user: {ex.Message}");  // Correct use of FluentResults.Result.Fail()
            }
        }

        // Delete
        public async Task<Result> DeleteUserAsync(Guid userId)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.id == userId);
                
                if (user == null)
                    return Result.Fail("User not found.");  // Correct use of FluentResults.Result.Fail()

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Result.Ok();  // Correct use of FluentResults.Result.Ok()
            }
            catch (Exception ex)
            {
                return Result.Fail($"Error deleting user: {ex.Message}");  // Correct use of FluentResults.Result.Fail()
            }
        }

        // Get all users (optional)
        public async Task<Result<IQueryable<User>>> GetAllUsersAsync()
        {
            try
            {
                var users = _context.Users.AsQueryable();
                return Result.Ok(users);  // Correct use of FluentResults.Result.Ok()
            }
            catch (Exception ex)
            {
                return Result.Fail<IQueryable<User>>($"Error retrieving users: {ex.Message}");  // Correct use of FluentResults.Result.Fail()
            }
        }
        
        public async Task<Result<User>> GetUserByGitIdAsync(long gitId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.gitId == gitId);
                return Result.Ok(user);
            }
            catch (Exception ex)
            {
                return Result.Fail(new List<string> { ex.Message });
            }
        }
    }
}
