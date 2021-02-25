using Redux;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.State;

namespace ToDoList.Actions
{
    internal class AddTodoAction : IAction
    {
        public string username { get; set; }

        public string email { get; set; }

        public string text { get; set; }

        public Status status { get; set; }

    }
}
