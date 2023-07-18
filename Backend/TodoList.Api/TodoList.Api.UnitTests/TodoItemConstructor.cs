using TodoList.Core.Models;
using Xunit;

namespace TodoList.Api.UnitTests
{
    public class TodoItemConstructor
    {
        private string _testFirstName = "John";
        private string _testLastName = "Doe";

        private User? _testItem;

        private User CreateUser()
        {
            return new User(_testFirstName, _testLastName);
        }

        [Fact]
        public void InitializesName()
        {
            var _testUser = CreateUser();

            Assert.Equal(_testFirstName, _testUser.FirstName);
            Assert.Equal(_testLastName, _testUser.LastName);
        }
    }
}
