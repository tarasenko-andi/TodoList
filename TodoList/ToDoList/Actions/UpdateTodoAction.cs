using Redux;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Actions
{
    internal class UpdateTodoAction : IAction
    {
        public string Text { get; internal set; }
        public int id { get; internal set; }
    }
}
