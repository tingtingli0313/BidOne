using Ardalis.Result;
using TodoList.Core.Models;

namespace TodoList.Core.Interfaces;

public interface IUserService
{
    Task<Result<User>> AddAsync(User newUser);
    Task<Result<List<User>>> GetAllUsersAsync();
    Task<Result<User>> GetUserByIdAsyc(Guid id);
    Task<Result<User>> UpdateAsync(User value);
}