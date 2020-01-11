namespace CASReports.Models
{
	public class Specialist : BaseModel
	{
		public bool IsDeleted { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string ShortName { get; set; }
		public string Email { get; set; }
		public string PhoneMobile { get; set; }
		public string Phone { get; set; }
		public string Additional { get; set; }

		#region public override string ToString()

		public override string ToString()
		{
			if (IsDeleted)
				return LastName + " " + LastName + " is deleted.";
			return LastName + " " + FirstName;
		}

		#endregion
    }
}