namespace CASReports.Models
{
	#region public enum LifelengthSubResource
	/// <summary>
	/// Представляет часть наработки
	/// </summary>
	public enum LifelengthSubResource
	{
		/// <summary>
		/// минуты
		/// </summary>
		Minutes = 0,

		/// <summary>
		/// часы
		/// </summary>
		Hours = 1,

		/// <summary>
		/// Циклы
		/// </summary>
		Cycles = 2,

		/// <summary>
		/// Дни
		/// </summary>
		Calendar = 3,

	}
	#endregion

	#region public enum CalendarTypes
	/// <summary>
	/// Типы отображения календарной наработки
	/// </summary>
	public enum CalendarTypes
	{
		/// <summary>
		/// В днях
		/// </summary>
		Days = 0,
		/// <summary>
		/// В месяцах
		/// </summary>
		Months = 1,
		/// <summary>
		/// В годах
		/// </summary>
		Years = 2
	}
	#endregion

}