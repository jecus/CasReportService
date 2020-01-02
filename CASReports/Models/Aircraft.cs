using System;

namespace CASReports.Models
{
	public class Aircraft : BaseModel
	{
		public string RegistrationNumber { get; set; }
		public string SerialNumber { get; set; }
		public DateTime ManufactureDate { get; set; }
		public string LineNumber { get; set; }
		public string VariableNumber { get; set; }
		public DateTime DeliveryDate { get; set; }
		public DateTime AcceptanceDate { get; set; }
	}
}