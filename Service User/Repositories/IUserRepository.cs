using FluentResults;
using Service_User.Models;

namespace Service_User.Repositories
{
    public interface IUserRepository
    {
        Task<Result<User>> GetUserByIdAsync(Guid userId);
        Task<Result<User>> UpdateUserAsync(Guid userId, User updatedUser);
        Task<Result> DeleteUserAsync(Guid userId);
    }
}