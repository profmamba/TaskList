using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ProfMamba.TaskList.Objects;

namespace ProfMamba.TaskList.Context.Mappings
{
	public class TaskMap : EntityTypeConfiguration<Task>
	{
		//Constructors

        public TaskMap()
        {
            // Primary Key
            this.HasKey(t => t.TaskId);

			//Custom Mappings
			this.Property(t => t.TaskType)
				.HasColumnName("TaskTypeId");
        }
	}
}