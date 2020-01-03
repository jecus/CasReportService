using System.Collections.Generic;
using System.ComponentModel;

namespace Vendors.Helpers
{
	public class Filter
	{
		#region public Filter(string filterProperty, FilterType filterType, object value)

		public Filter(string filterProperty, FilterType filterType, object value)
		{
			FilterProperty = filterProperty;
			FilterType = filterType;
			Value = value;
		}

		#endregion

		#region public Filter(string filterProperty, FilterType filterType, IEnumerable<int> values)

		public Filter(string filterProperty, IEnumerable<int> values)
		{
			FilterProperty = filterProperty;
			FilterType = FilterType.Equal;
			Values = values;
		}

		#endregion

		#region public Filter(string filterProperty, object value) : this(filterProperty, FilterType.Equal, value )

		public Filter(string filterProperty, object value) : this(filterProperty, FilterType.Equal, value)
		{
		}

		public Filter()
		{

		}

		#endregion



		public string FilterProperty { get; set; }


		public FilterType FilterType { get; set; }


		public object Value { get; set; }


		public IEnumerable<int> Values { get; set; }
	}

	public enum FilterType
	{
		/// <summary>
		/// Находится в определенном множестве
		/// </summary>
		[Description("In")]
		
		In = 1,
		/// <summary>
		/// Меньше 
		/// </summary>
		[Description("<")]
		
		Less = 10,
		/// <summary>
		/// Меньше или равно
		/// </summary>
		[Description("<=")]
		
		LessOrEqual = 11,
		/// <summary>
		/// Эквивалентно (равно)
		/// </summary>
		[Description("=")]
		
		Equal = 12,
		/// <summary>
		/// Больше или равно
		/// </summary>
		[Description(">=")]
		
		GratherOrEqual = 13,
		/// <summary>
		/// Больше
		/// </summary>
		[Description(">")]
		
		Grather = 14,
		/// <summary>
		/// Не эквивалентно (не равно)
		/// </summary>
		[Description("!=")]
		
		NotEqual = 15,
		/// <summary>
		/// Между 2-мя значениями
		/// </summary>
		[Description("Between")]
		
		Between = 20,
		// <summary>
		/// Между 2-мя значениями
		/// </summary>
		[Description("Between")]
		
		Contains = 21
	}
}