using System;
using TodoList.Core.Models;

namespace TodoList.Api.UnitTests;


public class ToDoItemBuilder
{
    private User _todo = new User("test", "John");

    public ToDoItemBuilder Id(Guid id)
    {
        _todo.Id = id;
        return this;
    }

    public ToDoItemBuilder Description(string firstName)
    {
        _todo.FirstName = firstName;
        return this;
    }

    public ToDoItemBuilder WithDefaultValues()
    {
        _todo = new User("firstName", "lastName") { Id = new Guid() };

        return this;
    }

    public User Build() => _todo;
}
