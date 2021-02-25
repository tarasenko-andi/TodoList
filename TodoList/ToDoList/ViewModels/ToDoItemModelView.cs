﻿using System.ComponentModel;
using ToDoList.State;

namespace ToDoList.ViewModels
{
    public class ToDoItemModelView : INotifyPropertyChanged
    {
        public ToDoListModelView ToDoListModelView { get; set; }
        public bool IsAdmin { get => ToDoListModelView.IsAdmin; }
        bool isNew;
        public bool IsNew { get => isNew;
            set
            {
                if(isNew != value)
                {
                    isNew = value;
                    OnPropertyChanged(nameof(IsNew));
                }
            }
        }
        public TodoItem TodoItem { get; }
        public ToDoItemModelView()
        {
            TodoItem = new TodoItem();
        }
        public ToDoItemModelView(TodoItem todoItem, ToDoListModelView toDoListModelView)
        {
            ToDoListModelView = toDoListModelView;
            TodoItem = todoItem;
        }
        public int ID { get => TodoItem.id;
            set
            {
                if(TodoItem.id != value)
                {
                    TodoItem.id = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }
        public string UserName
        {
            get => TodoItem.username;
            set
            {
                if (TodoItem.username != value)
                {
                    TodoItem.username = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }
        public string Email
        {
            get => TodoItem.email;
            set
            {
                if (TodoItem.email != value)
                {
                    TodoItem.email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        public Status Status
        {
            get => TodoItem.status;
            set
            {
                if (TodoItem.status != value)
                {
                    TodoItem.status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }
        public string Text
        {
            get => TodoItem.text;
            set
            {
                if (TodoItem.text != value)
                {
                    TodoItem.text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
