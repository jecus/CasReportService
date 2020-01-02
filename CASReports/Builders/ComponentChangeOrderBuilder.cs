
using System.ComponentModel;
using CASReports.Datasets;
using CASReports.Helpers;
using CASReports.ReportTemplates;

namespace CASReports.Builders
{
    /// <summary>
    /// ����������� ������ Release To Service 
    /// </summary>
    public class ComponentChangeOrderBuilder
    {
        #region Fields

        private WorkPackage _currentWorkPackage;
        private Component _currentComponent;
        private TransferRecord _lastTransfer;
        private string _workType;
        private int _orderNum;
	    private readonly bool _isScatReport;

	    #endregion

        #region Constructor

        /// <summary>
        /// ��������� ����������� ������ Release To Service 
        /// </summary>
        /// <param name="currentWorkPackage">������� �����</param>
        /// <param name="currentDetail"></param>
        /// <param name="workType"></param>
        /// <param name="orderNum"></param>
        public ComponentChangeOrderBuilder(WorkPackage currentWorkPackage, object currentDetail, string workType, int orderNum, bool isScatReport = false )
        {
	        _currentWorkPackage = currentWorkPackage;
	        _workType = workType;
	        _orderNum = orderNum;
	        _isScatReport = isScatReport;
			if (!(currentDetail is Component)) return;
            _currentComponent = (Component) currentDetail;
            _lastTransfer = ((Component)currentDetail).TransferRecords.GetLast(); 
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
	        if (_isScatReport)
	        {
		        var report = new ComponentChangeOrderReportScat();
		        report.SetDataSource(GenerateDataSet());
		        return report;
			}
	        else
	        {
				var report = new ComponentChangeOrderReport();
		        report.SetDataSource(GenerateDataSet());
		        return report;
			}
            
        }

        #endregion

        #region private MaintenanceJobCardDataSet GenerateDataSet()

        private ComponentChangeOrderDataSet GenerateDataSet()
        {
            ComponentChangeOrderDataSet dataSet = new ComponentChangeOrderDataSet();
            AddAdditionalDataToDataSet(dataSet);
			AddMainInformationToDataSet(dataSet);
            AddAircraftToDataset(dataSet);
            return dataSet;
        }

        #endregion

        #region private void AddMainInformationToDataSet(ComponentChangeOrderDataSet destinationDataSet)

        private void AddMainInformationToDataSet(ComponentChangeOrderDataSet destinationDataSet)
        {
            destinationDataSet.MainTable.AddMainTableRow(_currentComponent?.ATAChapter.ToString(),
                                                         _workType,
                                                         _currentComponent?.Description,
                                                         _currentComponent?.Manufacturer,
                                                         _currentComponent?.PartNumber,
                                                         _currentComponent?.SerialNumber, 
                                                         _lastTransfer != null ? _lastTransfer.Position :"",
                                                         "",
                                                         "",
                                                         _currentWorkPackage.Number,
                                                         _currentWorkPackage.Station,
                                                         _orderNum.ToString());
        }

        #endregion

        #region private void AddAdditionalDataToDataSet(ComponentChangeOrderDataSet destinationDateSet)

        /// <summary>
        /// ���������� �������������� ���������� 
        /// </summary>
        /// <param name="destinationDateSet"></param>
        private void AddAdditionalDataToDataSet(ComponentChangeOrderDataSet destinationDateSet)
        {
            var reportHeader = "Component Change Order";
            var reportFooter = GlobalTermsProvider.Terms["ReportFooter"].ToString();
            var reportFooterPrepared = GlobalTermsProvider.Terms["ReportFooterPrepared"].ToString();
            var reportFooterLink = GlobalTermsProvider.Terms["ProductWebsite"].ToString();
            destinationDateSet.AdditionalDataTAble.AddAdditionalDataTAbleRow(GlobalObjects.CasEnvironment.Operators.First(o => o.ItemId == _currentWorkPackage.Aircraft.OperatorId).LogoTypeWhite, reportHeader, "", "", "", reportFooter, reportFooterPrepared, reportFooterLink);

        }

        #endregion

        #region private void AddAircraftToDataset(ComponentChangeOrderDataSet destinationDataSet)

        /// <summary>
        /// ����������� ������� � ������� ������
        /// </summary>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        private void AddAircraftToDataset(ComponentChangeOrderDataSet destinationDataSet)
        {
            if (_currentWorkPackage.Aircraft == null)
                return;
	        var airctaftLifelength = GlobalObjects.CasEnvironment.Calculator.GetCurrentFlightLifelength(_currentWorkPackage.Aircraft);
			string serialNumber = _currentWorkPackage.Aircraft.SerialNumber;
            string manufactureDate = _currentWorkPackage.Aircraft.ManufactureDate.ToString(GlobalTermsProvider.Terms["DateFormat"].ToString());
            string sinceNewHours = airctaftLifelength.Hours.ToString();
            string sinceNewCycles = airctaftLifelength.Cycles.ToString().Trim();
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
