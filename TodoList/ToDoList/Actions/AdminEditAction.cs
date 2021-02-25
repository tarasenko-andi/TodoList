using Redux;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.State;

namespace ToDoList.Actions
{
    public class AdminEditAction : IAction
    {
        public TodoItem TodoItem { get; }
        public AdminEditAction(int id, string text, bool completed, TodoItem todoItem)
        {
            Id = id;
            Text = text;
            Completed = completed;
            TodoItem = todoItem;
        }
        public int Id { get; }
        public string Text { get; }
        public bool Completed { get; }
    }
}
