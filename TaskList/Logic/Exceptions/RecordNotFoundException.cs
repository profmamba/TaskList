using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfMamba.TaskList.Logic
{
	public class RecordNotFoundException<T> : Exception
	{
		public RecordNotFoundException(int id)
			:base (string.Format("Could not find a {0} with Id {1}", typeof(T).Name, id))
		{}
	}
}
