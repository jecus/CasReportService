using System.Collections.Generic;

namespace CASReports.Models
{
	public class RequestForQuotation : BaseModel
	{
		public List<Product> Products { get; set; }
	}
}