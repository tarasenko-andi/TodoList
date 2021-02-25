using Redux;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Actions
{
    internal class RemoveTodoAction : IAction
    {
        public int id { get; internal set; }
    }
}
