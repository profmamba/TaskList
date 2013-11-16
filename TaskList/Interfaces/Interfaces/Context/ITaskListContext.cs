using ProfMamba.TaskList.Objects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace ProfMamba.TaskList.Interfaces
{
	public interface ITaskListContext : IDisposable
	{
		IDbSet<Task> Tasks { get; set; }

		int SaveChanges();

		DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
	}
}
