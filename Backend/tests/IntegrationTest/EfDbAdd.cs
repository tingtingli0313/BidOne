using TodoList.Core.Models;

namespace IntegrationTest;

public class EfDbAdd : BaseEfRepoTestFixture
{
    [Fact]
    public async Task AddsProjectAndSetsId()
    {
        var testFirstName = "testItem";
        var testLastName = "testItem";
        var todoItem = new User(testFirstName, testLastName);

        await _dbContext.AddAsync(todoItem);
        await _dbContext.SaveChangesAsync();

        var result = (_dbContext.GetItems())
                        .FirstOrDefault();

        Assert.Equal(testFirstName, result?.FirstName);
        Assert.NotNull(result?.Id);
    }
}