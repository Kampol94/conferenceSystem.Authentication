using AuthenticationService.Application.TodoLists.Queries.ExportTodos;

namespace AuthenticationService.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
