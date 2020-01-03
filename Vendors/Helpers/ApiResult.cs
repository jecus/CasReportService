using System.Net;

namespace Vendors.Helpers
{
	public class ApiResult<TView> : ApiResult
	{
		/// <summary>
		/// Данные
		/// </summary>
		public TView Data { get; set; }
	}

	public class ApiResult
	{
		/// <summary>
		/// Флаг успешности
		/// </summary>
		public bool IsSuccessful { get; set; }

		/// <summary>
		/// Код ошибки
		/// </summary>
		public HttpStatusCode StatusCode { get; set; }

		/// <summary>
		/// Сообщение об ошибке
		/// </summary>
		public string Error { get; set; }
	}
}