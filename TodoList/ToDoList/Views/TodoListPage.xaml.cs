﻿using System;
using System.Collections.Generic;
using ToDoList.Actions;
using ToDoList.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ToDoList.Constants;

namespace ToDoList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TodoListPage : ContentPage
	{
		public ToDoListModelView viewModel { get; set; }
		public TodoListPage()
		{
			viewModel = new ToDoListModelView(this);
			BindingContext = viewModel;
			Appearing += TodoListPage_Appearing;
			InitializeComponent();
			viewModel.ChangeSortedType("3");
		}

        private void TodoListPage_Appearing(object sender, EventArgs e)
        {
			App.TodoStore.Dispatch(new UpdateTodoListAction());
        }

        async void OnAddItemClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new TodoItemPage(true)
			{
				BindingContext = new ToDoItemModelView() { IsNew = true, ToDoListModelView =viewModel }
			});

		}

		void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			//listView.SelectedItem = null;
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
			viewModel.AdminFormEnable = false;
        }
		private void TapGestureRecognizer_Tapped3(object sender, EventArgs e)
		{
			if (!viewModel.IsAdmin)
				viewModel.AdminFormEnable = true;
			else
				App.TodoStore.Dispatch(new UnlogingAction());
		}
		private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
			//listView.SelectedItem = null;
			if (viewModel.IsAdmin)
            {
				var item = ((Grid)sender).BindingContext as ToDoItemModelView;
				item.IsNew = false;
				await Navigation.PushAsync(new TodoItemPage
				{
					BindingContext = item
				});
			}
			else
				viewModel.AdminFormEnable = true;
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
			await App.TodoManager.GetTasksAsync(1, Constants.SortedField.username, Constants.SortDirection.asc);
        }
    }
}