using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TodoList.Core.Models;

namespace TodoList.Infrastructure.Data.Config;

public class ToDoConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(t => t.FirstName)
            .IsRequired();
        builder.Property(t => t.LastName)
          .IsRequired();
    }
}