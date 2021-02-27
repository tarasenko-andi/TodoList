using System;
using System.Text.RegularExpressions;
using ToDoList.Actions;
using ToDoList.State;
using ToDoList.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TodoItemPage : ContentPage
	{
		bool isNewItem;
		ToDoItemModelView startedItem { get; set; }
		public TodoItemPage(bool isNew = false)
		{
			InitializeComponent();
			isNewItem = isNew;
            BindingContextChanged += TodoItemPage_BindingContextChanged;
		}
		bool updateContext;
        private void TodoItemPage_BindingContextChanged(object sender, EventArgs e)
        {
           if(!updateContext && BindingContext != null)
            {
				updateContext = true;
				var item = ((ToDoItemModelView)BindingContext).TodoItem;
				startedItem = new ToDoItemModelView(new TodoItem { email = item.email, id = item.id, status = item.status, text = item.text, username=item.username }, ((ToDoItemModelView)BindingContext).ToDoListModelView);
			}
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
		{
			var todoItem = (ToDoItemModelView)BindingContext;
			if (ValidEntry(todoItem, out string str))
            {
				await DisplayAlert("Ошибка ввода", str, "OK");
				return;
            }
            if (todoItem.IsNew)
            {
				var adTodo = new AddTodoAction(todoItem.UserName, todoItem.Email, todoItem.Text, todoItem.Status, todoItem.IsNew);
				App.TodoStore.Dispatch(adTodo);
				if (adTodo.Result)
				{
					await DisplayAlert("Успешно", "Сохранение данных прошло успешно", "ОК");
				}
                else
                {
					await DisplayAlert("Ошибка", "Ошибка сохранения данных", "ОК");
					return;
				}
            }
            else
            {
				bool adminEdit = startedItem.Text != todoItem.Text;
				var adTodo = new UpdateTodoAction(todoItem.TodoItem, adminEdit, IsExecute_Switch.IsToggled);
				App.TodoStore.Dispatch(adTodo);
				if (adTodo.Result)
				{
					await DisplayAlert("Успешно", "Сохранение данных прошло успешно", "ОК");
				}
				else
				{
					await DisplayAlert("Ошибка", "Ошибка сохранения данных", "ОК");
					return;
				}

			}
			await Navigation.PopAsync();
		}

        private bool ValidEntry(ToDoItemModelView item, out string str)
        {
			str = "";
			if(item.UserName == null || item.UserName.Length == 0)
            {
				str += "Поле имья не заполнено";
            }
			if(item.Email == null || !Regex.IsMatch(item.Email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
			{
				str += "Поле Email не соответсвует патерну";
			}
			if(item.Text == null || item.Text.Length == 0)
            {
				str += "Поле Задачи не заполнено";
			}
			return str != "";
        }

        async void OnCancelButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}
    }
}