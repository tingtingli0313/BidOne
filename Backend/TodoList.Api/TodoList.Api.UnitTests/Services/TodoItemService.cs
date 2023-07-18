using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Core.Interfaces;
using TodoList.Core.Models;
using TodoList.Core.Services;
using Xunit;
using static TodoList.Api.UnitTests.Helper.EfHelper;

namespace TodoList.Api.UnitTests.Services
{
    public class TodoItemService
    {
        private readonly Mock<IToDoContext> _mockRepo = new Mock<IToDoContext>();
        private readonly UserService _service;

        public TodoItemService()
        {
            _service = new UserService(_mockRepo.Object);
        }

        [Fact]
        public async Task SetDefaultDataGetItem_Success_Async()
        {
            var item1 = new User("test1", "Alex");
            var item2 = new User("test2", "Alex");
            var item3 = new User("test3", "Alex");

            var mockDbSet = new Mock<DbSet<User>>();

            // Set up the mock DbSet to return specific data
            var items = new List<User> { item1, item2, item3 }.AsQueryable();
            mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(items.Provider);
            mockDbSet.As<IAsyncEnumerable<User>>()
                  .Setup(m => m.GetAsyncEnumerator(System.Threading.CancellationToken.None))
                  .Returns(new TestAsyncEnumerator<User>(items.GetEnumerator()));

            mockDbSet.As<IQueryable<User>>()
                     .Setup(m => m.Provider)
                     .Returns(new TestAsyncQueryProvider<User>(items.Provider));

            _mockRepo.Setup(s => s.GetItems()).Returns(mockDbSet.Object);
              
            //mock service
            var result = await _service.GetAllUsersAsync();

            Assert.Equal(Ardalis.Result.ResultStatus.Ok, result.Status);
        }


        [Fact]
        public async Task SetDefaultData_GetItemById_Success_Async()
        {
            var item1 = new User("test1", "Alex") { Id = new Guid() };
            var item2 = new User("test2", "Alex");
            var item3 = new User("test3", "Alex");

            var mockDbSet = new Mock<DbSet<User>>();

            // Set up the mock DbSet to return specific data
            var items = new List<User> { item1, item2, item3 }.AsQueryable();
            mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(items.Provider);
            mockDbSet.As<IAsyncEnumerable<User>>()
                  .Setup(m => m.GetAsyncEnumerator(System.Threading.CancellationToken.None))
                  .Returns(new TestAsyncEnumerator<User>(items.GetEnumerator()));

            mockDbSet.As<IQueryable<User>>()
                     .Setup(m => m.Provider)
                     .Returns(new TestAsyncQueryProvider<User>(items.Provider));

            _mockRepo.Setup(s => s.GetItems()).Returns(mockDbSet.Object);
            var result = await _service.GetUserByIdAsyc(item1.Id);

            Assert.Equal(Ardalis.Result.ResultStatus.Ok, result.Status);
        }


        [Fact]
        public async Task GivenInvalidaId_WithDefaultData_GetItemById_NotFound()
        {
            var item1 = new User("test1", "Alex");
            var item2 = new User("test2", "Alex");
            var item3 = new User("test3", "Alex");

            var mockDbSet = new Mock<DbSet<User>>();

            // Set up the mock DbSet to return specific data
            var items = new List<User> { item1, item2, item3 }.AsQueryable();
            mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(items.Provider);
            mockDbSet.As<IAsyncEnumerable<User>>()
                  .Setup(m => m.GetAsyncEnumerator(System.Threading.CancellationToken.None))
                  .Returns(new TestAsyncEnumerator<User>(items.GetEnumerator()));

            mockDbSet.As<IQueryable<User>>()
                     .Setup(m => m.Provider)
                     .Returns(new TestAsyncQueryProvider<User>(items.Provider));

            _mockRepo.Setup(s => s.GetItems()).Returns(mockDbSet.Object);

            //mock service
            var result = await _service.GetUserByIdAsyc(new Guid());
            Assert.Equal(Ardalis.Result.ResultStatus.NotFound, result.Status);
        }
    }
}
