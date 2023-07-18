using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;

namespace TodoList.Core.Services;
public class UserService : IUserService
{
    private readonly IToDoContext _dbContext;
    
    public UserService(IToDoContext toDoContext)
    {
        _dbContext = toDoContext;
    }

    public async Task<Result<User>> AddAsync(User newItem)
    {
        if(_dbContext.GetItems().Any(x => string.Equals(x.FirstName.ToLowerInvariant(), newItem.FirstName.ToLowerInvariant())
                                       && string.Equals(x.LastName.ToLowerInvariant(), newItem.LastName.ToLowerInvariant())))
        {
            return Result<User>.Conflict($"user name {newItem.ToString()} is already exist.");
        }

        _dbContext.GetItems().Add(newItem);
        await _dbContext.SaveChangesAsync();

        return new Result<User>(newItem);
    }

    public async Task<Result<List<User>>> GetAllUsersAsync()
    {
        var result = new Result<List<User>>(new List<User>());
        var items = await _dbContext.GetItems().ToListAsync();

        if (items.Any())
        {
            result = new Result<List<User>>(items);
        }

        return result;
    }

    public async Task<Result<User>> GetUserByIdAsyc(Guid id)
    {
        var item = await _dbContext.GetItems().FindAsync(id);

        if (item is null)
        {
            return Result<User>.NotFound();
        }

        return new Result<User>(item);
    }

    public async Task<Result<User>> UpdateAsync(User toUpdate)
    {
        var updateItem = await _dbContext.GetItems().FindAsync(toUpdate.Id);
        if (updateItem is null) return Result<User>.NotFound();

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return Result<User>.Error(new[] { ex.Message });
        }
       
        return new Result<User>(updateItem);
    }
}
