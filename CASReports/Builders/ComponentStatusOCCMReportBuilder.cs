
using System.ComponentModel;
using CASReports.Datasets;
using CASReports.ReportTemplates;

namespace CASReports.Builders
{
    /// <summary>
    /// ����������� ������ ��� ComponentStatus OCCM
    /// </summary>
    public abstract class ComponentStatusOCCMReportBuilder : DetailListReportBuilder
    {

        #region Constructors

        #region public ComponentStatusOCCMReportBuilder()

        public ComponentStatusOCCMReportBuilder()
        {
            ReportTitle = "OC/CM Component Status";
        }

        #endregion


        #endregion

        #region Methods

        #region public override object GenerateReport()

        /// <summary>
        /// �������������� ����� �� ������, ����������� � ������� ������
        /// </summary>
        /// <returns>����������� �����</returns>
        public override object GenerateReport()
        {
            ComponentStatusOCCMReport report = new ComponentStatusOCCMReport();
            report.SetDataSource(GenerateDataSet());
            return report;
        }

        #endregion

        #region protected override void AddDetailToDataset(Detail detail, ref int previousNumber, DetailListDataSet destinationDataSet)

        /// <summary>
        /// ����������� ������� � ������� ������
        /// </summary>
        /// <param name="component���������� �������</param>
        /// <param name="previousNumber">���������� ����� ��������</param>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        protected override void AddDetailToDataset(Component component, ref int previousNumber, DetailListDataSet destinationDataSet)
        {
            var ata =component.ATAChapter;
            var atachapter = ata.ShortName;
            var componentNumber = (previousNumber++).ToString();
            var atachapterfull = ata.FullName;
            var partNumber = component.PartNumber;
            var description = component.Description;
            var serialNumber = component.SerialNumber;
	        var lastTransferRecord = component.TransferRecords.GetLast();
            var positionNumber = lastTransferRecord.Position;
            var instalationDate = UsefulMethods.NormalizeDate(lastTransferRecord.TransferDate);
			var installationLifelength = lastTransferRecord.OnLifelength;
			var remarks = component.Remarks;
            GlobalObjects.PerformanceCalculator.GetNextPerformance(component);
            var current = GlobalObjects.CasEnvironment.Calculator.GetCurrentFlightLifelength(component);
            var installationTsncsn = LifelengthFormatter.GetHoursData(installationLifelength, " hrs\r\n") +
                                     LifelengthFormatter.GetCyclesData(installationLifelength, " cyc\r\n");
            var currentTsncsn = LifelengthFormatter.GetHoursData(current, " hrs\r\n") +
                                LifelengthFormatter.GetCyclesData(current, " cyc\r\n");
            var condition = component.Condition.GetHashCode().ToString();
            var lifelengthAircraftTime = current;
            lifelengthAircraftTime.Substract(installationLifelength);
            var aircraftTime = LifelengthFormatter.GetHoursData(lifelengthAircraftTime, " hrs\r\n") +
                               LifelengthFormatter.GetCyclesData(lifelengthAircraftTime, " cyc\r\n");

            destinationDataSet.ItemsTable.AddItemsTableRow(componentNumber, atachapter, atachapterfull, partNumber, description, serialNumber, positionNumber, "",
                                                           instalationDate, "", "", "", "", "",
                                                           "", "", condition, aircraftTime, "", "", "", "", "", "",
                                                           installationTsncsn, remarks, currentTsncsn);
            

        }

        #endregion

        #endregion

    }
}