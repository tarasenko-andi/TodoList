using Redux;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Actions
{
    public class LoginAsAdmin : IAction
    {
        public LoginAsAdmin(string login, string password)
        {
            Login = login;
            Password = password;
        }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
