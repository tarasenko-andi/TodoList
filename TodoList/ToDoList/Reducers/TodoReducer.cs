using Redux;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Actions;
using ToDoList.State;
using Xamarin.Essentials;

namespace ToDoList.Reducers
{
    public class TodoReducer
    {
        public static TodoState Execute(TodoState previousState, IAction action)
        {
            switch (action)
            {
                case AddTodoAction newTodo:
                    var todo = new TodoItem()
                    {
                        text = newTodo.text,
                        email = newTodo.email,
                        username = newTodo.username,
                        status = newTodo.status
                    };
                    previousState.Todos.Add(todo);
                    break;
                case SortedAction sortedAction:
                    previousState.CurrentSortDirection = sortedAction.SortDirection;
                    previousState.CurrentSortedField = sortedAction.SortedField;
                    previousState.SelectedPage = sortedAction.Page;
                    break;
                case LoginAsAdmin asAdmin:
                    previousState.IsAdmin = App.TodoManager.Logining(asAdmin.Login, asAdmin.Password).Result;
                    break;
                case AdminEditAction adminEditAction:
                    bool edit = adminEditAction.TodoItem.text != adminEditAction.Text;
                    TodoItem.ChangeStatus(adminEditAction.TodoItem, edit, adminEditAction.Completed);
                    App.TodoManager.UpdateTodoItemAsync(adminEditAction.TodoItem).Wait();
                    break;
                case UnlogingAction unloging:
                    previousState.IsAdmin = false;
                    Preferences.Remove("login");
                    Preferences.Remove("password");
                    Preferences.Remove("token");
                    Preferences.Set("isout", true);
                    Preferences.Remove("datetimetoken");
                    break;
                case UpdateTodoListAction updateTodoList:
                    var response1 = App.TodoManager.GetTasksAsync(previousState.SelectedPage, previousState.CurrentSortedField, previousState.CurrentSortDirection).Result;
                    previousState.Todos = response1.Item2;
                    previousState.CountPages = response1.pagesCount;
                    break;

                default:
                    break;
            }
            return previousState;
        }
    }
}
