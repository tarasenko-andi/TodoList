using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Actions;
using ToDoList.State;
using Xamarin.Essentials;
using Xamarin.Forms;
using static ToDoList.Constants;

namespace ToDoList.ViewModels
{
    public class ToDoListModelView : INotifyPropertyChanged
    {
        public Page CallPage { get; set; }
        bool isAdmin;
        public bool IsAdmin { get => isAdmin;
            set
            {
                if(isAdmin != value)
                {
                    isAdmin = value;
                    OnPropertyChanged(nameof(IsAdmin));
                }
            }
        }
        public Command LoginingCommand { get; set; }
        ObservableCollection<ToDoItemModelView> items = new ObservableCollection<ToDoItemModelView>();
        public ObservableCollection<ToDoItemModelView> Items { get { return items; }
            set 
            {
                if(items!= value)
                {
                    items = value;
                    OnPropertyChanged(nameof(Items));
                }
            } 
        }
        public ToDoListModelView(Page page)
        {
            CallPage = page;
            LoginingCommand = new Command(async() => await Logining());
            ChangeSortedTypeCommand = new Command((object type) => ChangeSortedType(type));
            App.UpdateIsAdmin += App_UpdateIsAdmin;
            PropertyChanged += ToDoListModelView_PropertyChanged;
            App.TodoStore.Subscribe(state => 
            {
                UpdateItems(state.Todos);
                PagesCount = state.CountPages;
                IsAdmin = state.IsAdmin;
            });
        }
        void UpdateItems(List<TodoItem> todoItems)
        {
            Items.Clear();
            foreach (var item in todoItems)
            {
                Items.Add(new ToDoItemModelView(item, this));
            }
        }
        private void ToDoListModelView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CurrentPage) || e.PropertyName == nameof(CurrentSortedField) || e.PropertyName == nameof(CurrentSortDirection))
            {
                App.TodoStore.Dispatch(new SortedAction(CurrentPage, CurrentSortedField, CurrentSortDirection));
                App.TodoStore.Dispatch(new UpdateTodoListAction());
            }
        }

        private void App_UpdateIsAdmin(bool state)
        {
            IsAdmin = state;
        }

        string login;
        public string Login { get => login;
            set
            {
                if(login != value)
                {
                    login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }
        bool adminFormEnable;
        public bool AdminFormEnable 
        {
            get => adminFormEnable;
            set
            {
                if(adminFormEnable != value)
                {
                    adminFormEnable = value;
                    OnPropertyChanged(nameof(AdminFormEnable));
                }
            }
        }
        string password;
        public string Password { get => password; 
            set
            {
                if(password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        private async Task Logining()
        {
            string valid = ValidateModel();
            if(valid != "")
            {
                await CallPage.DisplayAlert("Ошибка ввода данных", valid, "OK");
                return;
            }
            App.TodoStore.Dispatch(new LoginAsAdmin(Login, Password));
            AdminFormEnable = false;
            if (IsAdmin)
                await CallPage.DisplayAlert("Успешно", "Вы АДМИН!!!", "ОК");
            else
                await CallPage.DisplayAlert("Ошибка", "Не удалось войти в аккаунт, проверьте интернет соединение и соответсвие логина и пароля", "ОК");
        }

        private string ValidateModel()
        {
            string valid = "";
            if(Login == null || Login.Length == 0)
            {
                valid += "Поле логин не заполнено\n";
            }
            if(Password == null || Password.Length == 0)
            {
                valid += "Поле пароль не заполнено";
            }
            return valid;
        }

        int pagesCount = 1;
        public int PagesCount { get => pagesCount;
            set
            {
                if(pagesCount != value)
                {
                    pagesCount = value;
                    OnPropertyChanged(nameof(PagesCount));
                    UpdateCheckedPageList();
                }
            }
        }
        private void UpdateCheckedPageList()
        {
            PagesCheckList.Clear();
            for (int i = 1; i <= PagesCount; i++)
            {
                PagesCheckList.Add(i);
            }
            OnPropertyChanged(nameof(CurrentPage));
        }
        public void ChangeSortedType(object type)
        {
            var sortedField = ((SortedField)Int32.Parse((string)type));
            if (sortedField != CurrentSortedField)
                CurrentSortedField = sortedField;
            else
            {
                CurrentSortDirection = CurrentSortDirection == SortDirection.asc ? SortDirection.desc : SortDirection.asc;
                if (CurrentSortDirection == SortDirection.asc)
                    CurrentPage = 1;
                else
                    CurrentPage = PagesCheckList.Count;
            }
            ChangeState = Int32.Parse(((int)CurrentSortedField).ToString() + ((int)CurrentSortDirection).ToString());
            App.TodoStore.Dispatch(new SortedAction(CurrentPage, CurrentSortedField, CurrentSortDirection));
            App.TodoStore.Dispatch(new UpdateTodoAction());
        }
        //int changeState;
        public int ChangeState { get => Preferences.Get("ChangeState", 0);
            set
            {
                if(ChangeState != value)
                {
                    Preferences.Set("ChangeState", value);
                    OnPropertyChanged(nameof(ChangeState));
                }
            }
        }
        public ICommand ChangeSortedTypeCommand { get; set; }
        public int CurrentPage
        {
            get => Preferences.Get("CurrentPage", 1);
            set
            {
                if(CurrentPage != value)
                {
                    Preferences.Set("CurrentPage", value);
                    OnPropertyChanged(nameof(CurrentPage));
                }
            }
        }
        SortedField sortedField = SortedField.username;
        public SortedField CurrentSortedField { get => sortedField;
            set
            {
                if(sortedField != value)
                {
                    sortedField = value;
                    OnPropertyChanged(nameof(this.CurrentSortedField));
                }
            }
        }
        SortDirection sortDirection;
        public SortDirection CurrentSortDirection
        {
            get => sortDirection;
            set
            {
                if (sortDirection != value)
                {
                    sortDirection = value;
                    OnPropertyChanged(nameof(this.CurrentSortDirection));
                }
            }
        }
        ObservableCollection<int> pagesCheckList = new ObservableCollection<int>();
        public ObservableCollection<int> PagesCheckList { get => pagesCheckList;
            set
            {
                if(pagesCheckList != value)
                {
                    pagesCheckList = value;
                    OnPropertyChanged(nameof(PagesCheckList));
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
