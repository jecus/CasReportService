using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASReports.Models
{
	public class MaintenanceCheck : BaseModel
	{
		public Lifelength Interval { get; set; }
		public MaintenanceCheckType CheckType { get; set; }
		public String Name { get; set; }
	}
}
