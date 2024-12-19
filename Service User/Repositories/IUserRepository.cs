using FluentResults;
using Service_User.Models;

namespace Service_User.Repositories
{
    public interface IUserRepository
    {
        Task<Result<User>> CreateUserAsync(User user);
        Task<Result<User>> GetUserByIdAsync(Guid userId);
        Task<Result<User>> UpdateUserAsync(Guid userId, User updatedUser);
        Task<Result> DeleteUserAsync(Guid userId);
        Task<Result<IQueryable<User>>> GetAllUsersAsync();
        Task<Result<User>> GetUserByGitIdAsync(long gitId);
    }
}