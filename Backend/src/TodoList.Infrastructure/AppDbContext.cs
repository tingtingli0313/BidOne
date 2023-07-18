using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;

namespace TodoList.Infrastructure.Data;
public class TodoContext : DbContext, IToDoContext
{

    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { set; get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return result;
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }

    public DbSet<User> GetItems()
    {
        return Users;
    }
}