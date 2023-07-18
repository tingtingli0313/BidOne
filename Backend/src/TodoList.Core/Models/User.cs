using Ardalis.GuardClauses;
using System.Xml.Linq;
using TodoList.Core.Shared;
using TodoList.Core.Shared.Interfaces;

namespace TodoList.Core.Models;

public class User : EntityBase, IAggregateRoot
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public User(string firstName, string lastName)
    {
        FirstName = Guard.Against.NullOrEmpty(firstName, nameof(FirstName));
        LastName = Guard.Against.NullOrEmpty(lastName, nameof(LastName));
    }

    public override string ToString()
    {
        return $"{Id}: Name: {FirstName} - {LastName}";
    }
}