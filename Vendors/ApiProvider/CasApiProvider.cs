using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using CASReports.Models;
using Vendors.Helpers;

namespace Vendors.ApiProvider
{
	public class CasApiProvider
	{
		private HttpClient _client;

		public CasApiProvider(string uri)
		{ 
			_client = new HttpClient
			{
				BaseAddress = new Uri(uri),
			};
		}


		public async Task<List<Aircraft>> GetAllAircrafts()
		{
			var param = HttpUtility.ParseQueryString(string.Empty);
			param.Add(new NameValueCollection()
			{
				["loadChild"] = true.ToString(),
				["getDeleted"] = false.ToString(),
			});
			var res = await _client.SendJsonAsync<IEnumerable<Filter>, List<Aircraft>>(HttpMethod.Post, $"aircraft/getlistall?{param}", new List<Filter>());
			return res?.Data;
		}

		public async Task<List<Operator>> GetAllOperators()
		{
			var param = HttpUtility.ParseQueryString(string.Empty);
			param.Add(new NameValueCollection()
			{
				["loadChild"] = true.ToString(),
				["getDeleted"] = false.ToString(),
			});
			var res = await _client.SendJsonAsync<IEnumerable<Filter>, List<Operator>>(HttpMethod.Post, $"aircraft/getlistall?{param}", new List<Filter>());
			return res?.Data;
		}
	}
}