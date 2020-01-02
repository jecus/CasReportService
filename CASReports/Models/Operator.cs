namespace CASReports.Models
{
	public class Operator : BaseModel
	{
		#region Fields

		private byte[] _logoType;
		private byte[] _logoTypeWhite;
		private byte[] _logotypeReportLarge;
		private byte[] _logotypeReportVeryLarge;

		#endregion

		public string Name { get; set; }

		public string ICAOCode { get; set; }

		public string Address { get; set; }

		public string Phone { get; set; }

		public string Fax { get; set; }

		public byte[] LogoType
		{
			get => _logoType ?? (_logoType = new byte[0]);
			set => _logoType = value;
		}

		public byte[] LogoTypeWhite
		{
			get => _logoTypeWhite ?? (_logoTypeWhite = new byte[0]);
			set => _logoTypeWhite = value;
		}

		public byte[] LogotypeReportLarge
		{
			get => _logotypeReportLarge ?? (_logotypeReportLarge = new byte[0]);
			set => _logotypeReportLarge = value;
		}

		public byte[] LogotypeReportVeryLarge
		{
			get => _logotypeReportVeryLarge ?? (_logotypeReportVeryLarge = new byte[0]);
			set => _logotypeReportVeryLarge = value;
		}
	}
}