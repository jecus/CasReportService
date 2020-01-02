using CASReports.Datasets;
using CASReports.Helpers;
using CASReports.ReportTemplates;

namespace CASReports.Builders
{
    /// <summary>
    /// ����������� ������ WPIncomingInspectionBuilder
    /// </summary>
    public class WPIncomingInspectionBuilder
    {
        #region Fields

        private WorkPackage _currentWorkPackage;
        
        #endregion

        #region Constructor

        /// <summary>
        /// ��������� ����������� ������ WPIncomingInspectionReport 
        /// </summary>
        /// <param name="currentWorkPackage">������� �����</param>
        public WPIncomingInspectionBuilder(WorkPackage currentWorkPackage)
        {
            _currentWorkPackage = currentWorkPackage;
        }

        #endregion

        #region Properties
        
        #endregion

        #region Methods

        #region public object GenerateReport()
        /// <summary>
        /// �������������� ����� �� ������, ����������� � ������� ������
        /// </summary>
        /// <returns>����������� �����</returns>
        public object GenerateReport()
        {
            WPIncomingInspectionReport report = new WPIncomingInspectionReport();
            report.SetDataSource(GenerateDataSet());
            return report;
        }

        #endregion

        #region private WPIncomingInspectionDataSet GenerateDataSet()

        private WPIncomingInspectionDataSet GenerateDataSet()
        {
            WPIncomingInspectionDataSet dataSet = new WPIncomingInspectionDataSet();
            AddAdditionalDataToDataSet(dataSet);
            AddMainInformationToDataSet(dataSet);
            AddAircraftToDataset(dataSet);
            return dataSet;
        }

        #endregion

        #region private void AddMainInformationToDataSet(WPIncomingInspectionDataSet destinationDataSet)

        private void AddMainInformationToDataSet(WPIncomingInspectionDataSet destinationDataSet)
        {
            destinationDataSet.MainTable.AddMainTableRow("",
                                                         _currentWorkPackage.Title,
                                                         _currentWorkPackage.Station);
        }

        #endregion

        #region private void AddAdditionalDataToDataSet(WPIncomingInspectionDataSet destinationDateSet)

        /// <summary>
        /// ���������� �������������� ���������� 
        /// </summary>
        /// <param name="destinationDateSet"></param>
        private void AddAdditionalDataToDataSet(WPIncomingInspectionDataSet destinationDateSet)
        {
            var reportHeader = "Component Change Order";
            var reportFooter = GlobalTermsProvider.Terms["ReportFooter"].ToString();
            var reportFooterPrepared = GlobalTermsProvider.Terms["ReportFooterPrepared"].ToString();
            var reportFooterLink = GlobalTermsProvider.Terms["ProductWebsite"].ToString();
            destinationDateSet.AdditionalDataTAble.AddAdditionalDataTAbleRow(GlobalObjects.CasEnvironment.Operators.First(o => o.ItemId == _currentWorkPackage.Aircraft.OperatorId).LogotypeReportLarge, reportHeader, "", "", "", reportFooter, reportFooterPrepared, reportFooterLink);

        }

        #endregion

        #region private void AddAircraftToDataset(WPIncomingInspectionDataSet destinationDataSet)

        /// <summary>
        /// ����������� ������� � ������� ������
        /// </summary>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        private void AddAircraftToDataset(WPIncomingInspectionDataSet destinationDataSet)
        {
            if (_currentWorkPackage.Aircraft == null)
                return;
	        var aircraftLifelength =GlobalObjects.CasEnvironment.Calculator.GetCurrentFlightLifelength(_currentWorkPackage.Aircraft);
			string serialNumber = _currentWorkPackage.Aircraft.SerialNumber;
            string manufactureDate = _currentWorkPackage.Aircraft.ManufactureDate.ToString(GlobalTermsProvider.Terms["DateFormat"].ToString());
            string sinceNewHours = aircraftLifelength.Hours.ToString();
            string sinceNewCycles = aircraftLifelength.Cycles.ToString().Trim();
            string lineNumberCaption = "";
            string variableNumberCaption = "";
            string lineNumber = _currentWorkPackage.Aircraft.LineNumber;
            string variableNumber = _currentWorkPackage.Aircraft.VariableNumber;
            if (lineNumber != "")
                lineNumberCaption = "L/N:";
            if (variableNumber != "")
                variableNumberCaption = "V/N:";
            destinationDataSet.AircraftInformationTable.AddAircraftInformationTableRow(_currentWorkPackage.Aircraft.RegistrationNumber, serialNumber,
                                                                     manufactureDate, _currentWorkPackage.Aircraft.Model.ToString(),lineNumberCaption, variableNumberCaption, lineNumber,
                                                                     variableNumber, sinceNewHours, sinceNewCycles);
        }

        #endregion
       
        #endregion
    }
}
