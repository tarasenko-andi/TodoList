using Redux;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.State;

namespace ToDoList.Actions
{
    internal class AddTodoAction : IAction
    {
        public AddTodoAction(string userName, string eMail, string Text, Status Status, bool isNew)
        {
            username = userName;
            email = eMail;
            text = Text;
            status = Status;
            IsNew = isNew;
        }
        public string username { get; set; }

        public string email { get; set; }

        public string text { get; set; }

        public Status status { get; set; }
        public bool IsNew { get; set; }
        public bool Result { get; set; }
    }
}
