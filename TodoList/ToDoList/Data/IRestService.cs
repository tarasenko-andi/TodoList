using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.State;
using static ToDoList.Constants;

namespace ToDoList.Data
{
    public interface IRestService
	{
		Task<(int pagesCount, List<TodoItem>)> RefreshDataAsync(int page, SortedField sortedField, SortDirection sortDirection);

		Task<bool> SaveTodoItemAsync(TodoItem item, bool isNewItem);

		Task<bool> Logining(string login, string password);
		Task<bool> UpdateTodoItemAsync(TodoItem item, Status itemStatus);
	}
}
