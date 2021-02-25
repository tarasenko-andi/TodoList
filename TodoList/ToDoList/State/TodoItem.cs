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
        public static void ChangeStatus(TodoItem item, bool editAdmin, bool execute)
        {
            if (!editAdmin && !execute)
                item.status = Status.NoExecute;
            else if (editAdmin && !execute)
                item.status = Status.NoExecuteAndAdminCheck;
            else if (!editAdmin && (execute || item.status == Status.Execute))
                item.status = Status.Execute;
            else if (editAdmin && (execute || item.status == Status.ExecuteAndAdminCheck))
                item.status = Status.ExecuteAndAdminCheck;
        }
    }
	public enum Status { NoExecute, NoExecuteAndAdminCheck, Execute = 10, ExecuteAndAdminCheck = 11 };
}
