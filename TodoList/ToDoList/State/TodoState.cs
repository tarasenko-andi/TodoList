using System.Collections.Generic;
using static ToDoList.Constants;

namespace ToDoList.State
{
    public class TodoState
    {
        public bool IsAdmin { get; set; }
        public List<TodoItem> Todos { get; set; }
        public SortedField CurrentSortedField { get; set; }
        public SortDirection CurrentSortDirection { get; set; }
        public int CountPages { get; set; }
        public int SelectedPage { get; set; }
        public static TodoState InitialState = new TodoState
        {
            Todos = new List<TodoItem>()
        };
    }
}
