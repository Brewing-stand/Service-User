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

        // Read (by id) - Ensure the user can only get their own data
        public async Task<Result<User>> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.id == userId);

                if (user == null)
                    return Result.Fail<User>("User not found.");

                return Result.Ok(user);
            }
            catch (Exception ex)
            {
                return Result.Fail<User>($"Error retrieving user: {ex.Message}");
            }
        }

        // Update - Ensure the user can only update their own data
        public async Task<Result<User>> UpdateUserAsync(Guid userId, User updatedUser)
        {
            try
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.id == userId);

                if (existingUser == null)
                    return Result.Fail<User>("User not found.");

                // Update properties
                existingUser.username = updatedUser.username;
                existingUser.avatar = updatedUser.avatar;

                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();

                return Result.Ok(existingUser);
            }
            catch (Exception ex)
            {
                return Result.Fail<User>($"Error updating user: {ex.Message}");
            }
        }

        // Delete - Ensure the user can only delete their own data
        public async Task<Result> DeleteUserAsync(Guid userId)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.id == userId);

                if (user == null)
                    return Result.Fail("User not found.");

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail($"Error deleting user: {ex.Message}");
            }
        }
    }
}
