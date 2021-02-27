using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.State;
using static ToDoList.Constants;

namespace ToDoList.Data
{
    public class TodoItemManager
	{
		IRestService restService;

		public TodoItemManager(IRestService service)
		{
			restService = service;
		}

		public Task<(int pagesCount, List<TodoItem>)> GetTasksAsync(int page, SortedField sortedField, SortDirection sortDirection)
		{
			return restService.RefreshDataAsync(page, sortedField, sortDirection);
		}

		public Task<bool> SaveTaskAsync(TodoItem item, bool isNewItem = false)
		{
			return restService.SaveTodoItemAsync(item, isNewItem);
		}

		public Task<bool> Logining(string login, string password)
        {
			return restService.Logining(login, password);
        }
		public Task<bool> UpdateTodoItemAsync(TodoItem item, Status itemStatus)
        {
			return restService.UpdateTodoItemAsync(item, itemStatus);
        }
	}
}
