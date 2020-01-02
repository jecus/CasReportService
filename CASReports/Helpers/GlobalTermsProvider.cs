using System.Collections.Generic;

namespace CASReports.Helpers
{
	public static class GlobalTermsProvider
	{
		public static Dictionary<string, string> Terms = new Dictionary<string, string>
		{
            ["ProductWebsite"] = "",
			["SystemShortName"] = "CAS",
            ["SystemName"] = "Continuing Airworthiness System",
            ["DeleteQuestion"] = "Do you really want to delete current {0}?",
            ["DeleteQuestionSeveral"] = "Do you really want to delete selected {0}?",
            ["CutOffSubCheckQuestion"] = "Do you really want to cutoff selected subcheck?",
            ["ReportFooter"] = @"I hereby certify that the data specified above has been verified throughout. Authorize signature ________________________",
            ["ReportFooterPrepared"] = @"Produced by CAS ",
            ["ReportFooterLink"] = @"Visit www.avalonkg.com/cas for more information.",
            ["JobCardFooterPrepared"] =  @"This job card was prepared by Continuing Airworthiness System.",
            ["Copyright"] = " 2006-2019 Avalon Worldgroup Inc. All rights reserved",
            ["CopyrightMultiline"] = " 2006-2018\r\nAvalon  Worldgroup Inc.\r\nAll rights reserved",
            ["CompanyName"] = "Avalon Worldgroup Inc.",
            ["CompanyNameIO"] = "Avalon Worldgroup Inc",
            ["ProductVersion"] = "3.1",
            ["ProductBuild"] = "4800",//<<$SvnVersion$>>
            ["DateFormat"] = "dd.MM.yyyy",
            ["DateFormatShort"] = "dd.MM.yy",
            ["ReverseDateFormat"] = "yyyy.MM.dd",
            ["CAARequirements"] = "The work specified / recordered was carried out in accordance" +
                                        " with the requirements of the Republic of Seychelles - Civil Aviation" +
                                        " Authority (CAA) and EASA - 145 and, in that respect, the aircraft" +
                                        " / equipment considered to be fit for release to service under Seychelles Approval" +
                                        " - Air Operations Certificate (AOC) No. 001 (January 01, 2000)",
            ["Revision"] =  "PA EF 25 R0/January2000"
		};
	}
}