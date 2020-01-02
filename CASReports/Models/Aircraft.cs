using System;

namespace CASReports.Models
{
	public class Aircraft : BaseModel
	{
		public string ELTIdHexCode;
		public string RegistrationNumber { get; set; }
		public string SerialNumber { get; set; }
		public DateTime ManufactureDate { get; set; }
		public string LineNumber { get; set; }
		public string VariableNumber { get; set; }
		public DateTime DeliveryDate { get; set; }
		public DateTime AcceptanceDate { get; set; }
		public double MaxTakeOffCrossWeight { get; set; }
		public double MaxTaxiWeight { get; set; }
		public double MaxZeroFuelWeight { get; set; }
		public double MaxLandingWeight { get; set; }
		public double BasicEmptyWeight { get; set; }
		public double OperationalEmptyWeight { get; set; }
		public string FuelCapacity { get; set; }
		public string MaxCruiseAltitude { get; set; }
		public string CruiseFuelFlow { get; set; }
		public string CargoCapacityContainer { get; set; }
		public string AircraftAddress24Bit { get; set; }
		public string CockpitSeating { get; set; }
		public string Galleys { get; set; }
		public string Lavatory { get; set; }
		public string Oven { get; set; }
		public string Boiler { get; set; }
		public string AirStairsDoors { get; set; }
	}
}