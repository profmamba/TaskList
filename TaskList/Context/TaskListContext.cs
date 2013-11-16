using ProfMamba.TaskList.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using ProfMamba.TaskList.Objects;

namespace ProfMamba.TaskList.Context
{
	public class TaskListContext : DbContext, ITaskListContext
	{
		//Constructors

		static TaskListContext()
        {
            Database.SetInitializer<TaskListContext>(null);
        }

		public TaskListContext()
            : base(string.Format("Name=TaskList"))
        {
			this.Configuration.ProxyCreationEnabled = false;
        }

		//Properties

        public IDbSet<Task> Tasks { get; set; }
        

		//Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Mappings.TaskMap());
        }
	}
}
