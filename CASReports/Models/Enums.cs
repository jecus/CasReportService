using System;

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

	#region public enum WorkPackageStatus : short
	/// <summary>
	/// Статус (состояние) рабочего пакета
	/// </summary>
	public enum WorkPackageStatus : short
	{
		/// <summary>
		/// Все состояния (открыт, закрыт, на исполнении)
		/// </summary>
		All = 0,
		/// <summary>
		/// Рабочий пакет открыт
		/// </summary>
		Opened = 1,

		/// <summary>
		/// Рабочий пакет отправлен на выполнение
		/// </summary>
		Published = 2,

		/// <summary>
		/// Рабочий пакет закрыт
		/// </summary>
		Closed = 3,

	}
	#endregion

	#region public enum ComponentStatus : short

	/// <summary>
	/// Перечисления статуса сомпонента (Новый, после Кап. ремонта, после Ремонта, после ТО )
	/// </summary>
	[Flags]
	public enum ComponentStatus : short
	{
		Unknown = 0,

		New = 1,

		Serviceable = 4,

		Overhaul = 16,

		Repair = 64,
		Test = 65,
		Inspect = 66,
		Modification = 67


	}

	public enum Exchange : short
	{
		No = 1,
		Yes = 2
	}

	#endregion

	#region public enum AvionicsInventoryMarkType: short
	/// <summary>
	/// Типы отметки AvionicsInventory
	/// </summary>
	public enum AvionicsInventoryMarkType : short
	{
		/// <summary>
		/// Тип отметки AvionicsInventory - обязательная
		/// </summary>
		Required = 1,
		/// <summary>
		/// Тип отметки AvionicsInventory - опциональная
		/// </summary>
		Optional = 2,
		/// <summary>
		/// Тип отметки AvionicsInventory - неизвестная
		/// </summary>
		Unknown = 3,
		/// <summary>
		/// Не включено вовсе
		/// </summary>
		None = 0
	}
	#endregion

	#region public enum PayTerm

	public enum PayTerm
	{
		PrePay = 0,
		PostPay = 1
	}

	#endregion
}