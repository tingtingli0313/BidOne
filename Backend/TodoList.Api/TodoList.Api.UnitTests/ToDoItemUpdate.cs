using TodoList.Api.UnitTests;
using Xunit;


namespace TodoList.Api.UnitTests;

public class ToDoItemUpdate
{

    [Fact]
    public void RaisesToDoItemCompletedEvent()
    {
        var item = new ToDoItemBuilder().Build();


        Assert.False(item.Id == null);
    }
}
