using Xamarin.Forms;
using Redux;
using ToDoList.Actions;
using ToDoList.Data;
using ToDoList.Reducers;
using ToDoList.State;
using ToDoList.Views;
using Xamarin.Essentials;

namespace ToDoList
{
    public partial class App : Application
    {
        public static Store<TodoState> TodoStore { get; private set; }
        public delegate void UpdateIsAdminEventHandler(bool state);
        public static event UpdateIsAdminEventHandler UpdateIsAdmin;
        public static TodoItemManager TodoManager { get; private set; }
        public App()
        {
            TodoStore = new Store<TodoState>(TodoReducer.Execute, TodoState.InitialState);
            TodoManager = new TodoItemManager(new RestService());
            MainPage = new NavigationPage(new TodoListPage());
            string username = Preferences.Get("username", "noUser");
            string password = Preferences.Get("password", "noPassword");
            if (username != "noUser" && password != "noPassword")
                TodoStore.Dispatch(new LoginAsAdmin(username, password));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
