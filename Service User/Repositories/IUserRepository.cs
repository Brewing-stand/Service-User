using FluentResults;
using Service_User.Models;

namespace Service_User.Repositories
{
    public interface IUserRepository
    {
        Task<Result<User>> GetUserByIdAsync(Guid userId, Guid requesterId);
        Task<Result<User>> UpdateUserAsync(Guid userId, Guid requesterId, User updatedUser);
        Task<Result> DeleteUserAsync(Guid userId, Guid requesterId);
    }
}