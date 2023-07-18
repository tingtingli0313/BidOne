using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TodoList.Core.Models;
using TodoList.Infrastructure.Data;

namespace TodoList.Api;

public static class SeedData
{
    public static readonly User user1 = new User("tester1", "John");
    public static readonly User user2 = new User("tester2", "John");
    public static readonly User user3 = new User("tester3", "John");


    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var dbContext = new TodoContext(
            serviceProvider.GetRequiredService<DbContextOptions<TodoContext>>()))
        {
            // Look for any TODO items.
            if (dbContext.Users.Any())
            {
                return;  
            }

            PopulateTestData(dbContext);
        }
    }
    public static void PopulateTestData(TodoContext dbContext)
    {
        foreach (var item in dbContext.Users)
        {
            dbContext.Remove(item);
        }
        dbContext.Users.Add(user1);
        dbContext.Users.Add(user2);
        dbContext.Users.Add(user3);

        dbContext.SaveChanges();
    }
}

