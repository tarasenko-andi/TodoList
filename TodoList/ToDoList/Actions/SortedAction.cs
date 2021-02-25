using Redux;
using System;
using System.Collections.Generic;
using System.Text;
using static ToDoList.Constants;

namespace ToDoList.Actions
{
    public class SortedAction : IAction
    {
        public SortedAction(int page, SortedField sortedField, SortDirection sortDirection)
        {

            SortedField = sortedField;
            SortDirection = sortDirection;
            Page = page;
        }
        public SortedField SortedField { get; }
        public SortDirection SortDirection { get; }
        public int Page { get; }
    }
}
