using CASReports.ReportTemplates;

namespace CASReports.Builders
{
    public class MaintenanceDirectivesReportBuilder : MaintenanceDirectivesFullReportBuilder
    {

        public MaintenanceDirectivesReportBuilder()
        {
            ReportTitle = "MAINTENANCE PROGRAM";
        }

        #region Methods

        #region public override object GenerateReport()

        /// <summary>
        /// ������������� ����� �� ������, ����������� � ������� ������
        /// </summary>
        /// <returns>����������� �����</returns>
        public override object GenerateReport()
        {
            MaintenanceDirectiveReport report = new MaintenanceDirectiveReport();
            report.SetDataSource(GenerateDataSet());
            return report;
        }

        #endregion

        #endregion

    }
}