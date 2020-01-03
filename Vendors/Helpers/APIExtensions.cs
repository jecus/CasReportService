using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vendors.Helpers
{
	public static class APIExtensions
	{
		public static async Task<ApiResult<TResult>> GetJsonAsync<TResult>(this HttpClient client, string requestUri)
		{
			var res = await client.GetAsync(requestUri);
			var content = await res.Content.ReadAsStringAsync();

			return new ApiResult<TResult>
			{
				IsSuccessful = res.IsSuccessStatusCode,
				StatusCode = res.StatusCode,
				Data = res.IsSuccessStatusCode ? JsonConvert.DeserializeObject<TResult>(content) : default(TResult),
				Error = res.IsSuccessStatusCode ? null : (content ?? res.ReasonPhrase)
			};
		}

		public static async Task<ApiResult> GetJsonAsync(this HttpClient client, string requestUri)
		{
			var res = await client.GetAsync(requestUri);
			var content = await res.Content.ReadAsStringAsync();

			return new ApiResult
			{
				IsSuccessful = res.IsSuccessStatusCode,
				StatusCode = res.StatusCode,
				Error = res.IsSuccessStatusCode ? null : (content ?? res.ReasonPhrase)
			};
		}

		public static async Task<ApiResult<TResult>> SendJsonAsync<TModel, TResult>(this HttpClient client, HttpMethod httpMethod, string requestUri, TModel model)
		{
			var json = "[{}]";
			if (model != null)
				json = JsonConvert.SerializeObject(model);
			var message = new HttpRequestMessage(httpMethod, requestUri)
			{
				Content = new StringContent(json, Encoding.UTF8, "application/json")
			};
			var res = await client.SendAsync(message);
			var content = await res.Content.ReadAsStringAsync();

			return new ApiResult<TResult>
			{
				IsSuccessful = res.IsSuccessStatusCode,
				StatusCode = res.StatusCode,
				Data = res.IsSuccessStatusCode
					? (string.IsNullOrWhiteSpace(content) ? default(TResult) : JsonConvert.DeserializeObject<TResult>(content, new JsonSerializerSettings()))
					: default(TResult),
				Error = res.IsSuccessStatusCode ? null : (content ?? res.ReasonPhrase)
			};
		}

		public static async Task<ApiResult> SendJsonAsync<TModel>(this HttpClient client, HttpMethod httpMethod, string requestUri, TModel model)
		{
			var json = "[{}]";
			if (model != null)
				json = JsonConvert.SerializeObject(model);
			var message = new HttpRequestMessage(httpMethod, requestUri)
			{
				Content = new StringContent(json, Encoding.UTF8, "application/json")
			};
			var res = await client.SendAsync(message);
			var content = await res.Content.ReadAsStringAsync();

			return new ApiResult
			{
				IsSuccessful = res.IsSuccessStatusCode,
				StatusCode = res.StatusCode,
				Error = res.IsSuccessStatusCode ? null : (content ?? res.ReasonPhrase)
			};
		}
	}
}