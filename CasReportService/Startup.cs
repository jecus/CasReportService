using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CasReportService.Startup))]

namespace CasReportService
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			
		}
	}
}
