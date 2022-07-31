using AuthenticationService.Application.Common.Mappings;
using AuthenticationService.Domain.Entities;

namespace AuthenticationService.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
