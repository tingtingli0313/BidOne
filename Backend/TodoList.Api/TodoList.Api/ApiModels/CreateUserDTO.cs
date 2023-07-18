using System;

namespace TodoList.Api.ApiModels;

//public class UserDTO : CreateUserDTO
//{
//    public UserDTO(Guid id, string firstName, string lastName) : base(string firstName, string lastName)
//    {
//        Id = id;
//    }

//    public Guid Id { get; set; }
//}

public record CreateUserDTO(string FirstName, string LastName);
public class UserDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}