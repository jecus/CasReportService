using CASReports.Datasets;
using CASReports.Models;
using CASReports.ReportTemplates;

namespace CASReports.Builders
{
    public class MaintenanceDirectivesReportBuilderKAC : MaintenanceDirectivesFullReportBuilder
    {

        public MaintenanceDirectivesReportBuilderKAC()
        {
            ReportTitle = "AMP Tasks Status";
        }

        #region Methods

        #region public override object GenerateReport()

        /// <summary>
        /// ������������� ����� �� ������, ����������� � ������� ������
        /// </summary>
        /// <returns>����������� �����</returns>
        public override object GenerateReport()
        {
            MaintenanceDirectiveFullReportKAC report = new MaintenanceDirectiveFullReportKAC();
            report.SetDataSource(GenerateDataSet());
            return report;
        }

        #endregion

        #region protected override void AddDirectiveToDataset(object directive, DefferedListDataSet destinationDataSet)

        /// <summary>
        /// ����������� ������� � ������� ������
        /// </summary>
        /// <param name="reportedDirective">���������� ���������</param>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        protected override void AddDirectiveToDataset(MaintenanceDirective reportedDirective, MaintenanceDirectivesDataSet destinationDataSet)
        {
            if (reportedDirective == null)
                return;

            string status = "";
            Lifelength used = Lifelength.Null;

            //string remarks = reportedDirective.LastPerformance != null ? reportedDirective.LastPerformance.Remarks : reportedDirective.Remarks;
            string remarks = reportedDirective.Remarks;
            string directiveType = reportedDirective.WorkType.ShortName;
            double cost = reportedDirective.Cost;
            double mh = reportedDirective.ManHours;
            if (reportedDirective.Status == DirectiveStatus.Closed) status = "C";
            if (reportedDirective.Status == DirectiveStatus.Open) status = "O";
            if (reportedDirective.Status == DirectiveStatus.Repetative) status = "R";
            if (reportedDirective.Status == DirectiveStatus.NotApplicable) status = "N/A";

            string effectivityDate = UsefulMethods.NormalizeDate(reportedDirective.Threshold.EffectiveDate);
            string kits = "";
            int num = 1;
            foreach (AccessoryRequired kit in reportedDirective.Kits)
            {
                kits += num + ": " + kit.PartNumber + "\n";
                num++;
            }

            //������ ������� � ���� ������������ � � ����������� ����
            //������ ������� �� ���������� � ���� ������������
            string firstPerformanceString = "";
            string repeatPerformanceToString = reportedDirective.Threshold.RepeatPerformanceToStrings();

            if (reportedDirective.LastPerformance != null)
            {
                used.Add(Current);
                used.Substract(reportedDirective.LastPerformance.OnLifelength);
                if (!reportedDirective.Threshold.RepeatInterval.IsNullOrZero())
                    used.Resemble(reportedDirective.Threshold.RepeatInterval);
                else if (!reportedDirective.Threshold.FirstPerformanceSinceNew.IsNullOrZero())
                    used.Resemble(reportedDirective.Threshold.FirstPerformanceSinceNew);
            }
            else
            {
                firstPerformanceString = reportedDirective.Threshold.FirstPerformanceToStrings();    
            }

            destinationDataSet.ItemsTable.AddItemsTableRow(reportedDirective.Applicability,
                                                           remarks,
                                                           reportedDirective.HiddenRemarks,
                                                           reportedDirective.Description.Replace("\r\n", " "),
                                                           reportedDirective.TaskNumberCheck,
                                                           reportedDirective.Access,
                                                           directiveType,
                                                           status,
                                                           effectivityDate,
                                                           firstPerformanceString,
                                                           reportedDirective.LastPerformance != null ? reportedDirective.LastPerformance.ToStrings() : "",
                                                           reportedDirective.NextPerformance != null ? reportedDirective.NextPerformance.ToStrings() : "",
                                                           reportedDirective.Remains.ToStrings(),
                                                           reportedDirective.Condition.ToString(),
                                                           mh,
                                                           cost,
                                                           kits,
                                                           reportedDirective.Zone,
                                                           reportedDirective.ATAChapter != null ? reportedDirective.ATAChapter.ShortName : "",
                                                           reportedDirective.ATAChapter != null ? reportedDirective.ATAChapter.FullName : "",
                                                           reportedDirective.TaskCardNumber,
                                                           reportedDirective.Program.ToString(),
                                                           repeatPerformanceToString,
                                                           used.ToStrings(),
                                                           reportedDirective.MaintenanceCheck != null ? reportedDirective.MaintenanceCheck.ToString() : "N/A");
        }

        #endregion

        #endregion

    }
}