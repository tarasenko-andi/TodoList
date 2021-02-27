using Redux;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.State;

namespace ToDoList.Actions
{
    internal class UpdateTodoAction : IAction
    {
        public UpdateTodoAction(TodoItem todoItem, bool adminEdit, bool isExecute)
        {
            TodoItem = todoItem;
            AdminEdit = adminEdit;
            IsExecute = isExecute;
        }
        public TodoItem TodoItem { get; set; }
        public bool AdminEdit { get; set; }
        public bool IsExecute { get; set; }
        public bool Result { get; set; }
    }
}
