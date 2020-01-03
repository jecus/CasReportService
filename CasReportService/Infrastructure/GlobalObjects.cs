using System.Collections.Generic;
using CASReports.Models;

namespace CasReportService.Helpers
{
	public static class GlobalObject
	{
		private static Dictionary<int, Aircraft> _aircrafts;
		private static Dictionary<int, Operator> _operators;

		public static Dictionary<int, Aircraft> Aircrafts
		{
			get => _aircrafts ?? (_aircrafts = new Dictionary<int, Aircraft>());
			set => _aircrafts = value;
		}

		public static Dictionary<int, Operator> Operators
		{
			get => _operators ?? (_operators = new Dictionary<int, Operator>());
			set => _operators = value;
		}
	}
}