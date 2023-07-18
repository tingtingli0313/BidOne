using Microsoft.EntityFrameworkCore;
using TodoList.Core.Models;

namespace TodoList.Core.Interfaces
{
    public interface IToDoContext
    {
        DbSet<User> GetItems();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
