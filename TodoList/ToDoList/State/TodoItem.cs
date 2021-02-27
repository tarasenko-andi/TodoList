using System;

namespace ToDoList.State
{
    public class TodoItem
	{
		public int id { get; set; }

		public string username { get; set; }

		public string email { get; set; }

		public string text { get; set; }

		public Status status { get; set; }
        public static Status ChangeStatus(TodoItem item, bool editAdmin, bool execute)
        {
            if (!editAdmin && !execute)
                return Status.NoExecute;
            else if (editAdmin && !execute)
                return Status.NoExecuteAndAdminCheck;
            else if (!editAdmin && (execute || item.status == Status.Execute))
                return Status.Execute;
            else if (editAdmin && (execute || item.status == Status.ExecuteAndAdminCheck))
                return Status.ExecuteAndAdminCheck;
            else
                return Status.NoExecute;
        }
    }
	public enum Status { NoExecute, NoExecuteAndAdminCheck, Execute = 10, ExecuteAndAdminCheck = 11 };
}
