using System;
using System.ComponentModel;
using CASReports.Datasets;
using CASReports.ReportTemplates;

namespace CASReports.Builders
{
    /// <summary>
    /// ����������� ������� ��� ������ ��������
    /// </summary>
    public class WorkPackageReportBuilder : AbstractReportBuilder
    {

        #region Fields
        private Aircraft _reportedAircraft;
        private WorkPackage _reportedWorkPackage;
        private string _dateAsOf = "";
        private string _reportTitle = "Directive list report";

        #endregion

        #region Properties

        #region public Aircraft ReportedAircraft

        /// <summary>
        /// �� ����������� � �����
        /// </summary>
        public Aircraft ReportedAircraft
        {
            private get
            {
                return _reportedAircraft;
            }
            set
            {
                _reportedAircraft = value;
            }
        }

        #endregion
       
        #region public List<Directive> ReportedDirectives

        /// <summary>
        /// ��������� ���������� � �����
        /// </summary>
        public WorkPackage ReportedWorkPackage
        {
            set
            {
                _reportedWorkPackage = value;
            }
        }

        #endregion

        #region private string DateAsOf

        /// <summary>
        /// ����� ���� DateAsOf
        /// </summary>
        private string DateAsOf
        {
            get { return _dateAsOf; }
            set { _dateAsOf = value; }
        }

        #endregion

        #region private string ReportTitle
        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        private string ReportTitle
        {
            get { return _reportTitle; }
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
            WorkPackageReport report = new WorkPackageReport();
            report.SetDataSource(GenerateDataSet());
            return report;
        }

        #endregion

        #region public virtual DirectiveListReportDataSet GenerateDataSet()

        /// <summary>
        /// ��������� �������� ������ (DataSet) ��� ������ � �����
        /// </summary>
        /// <returns></returns>
        public virtual WorkPackageDataSet GenerateDataSet()
        {
            WorkPackageDataSet dataset = new WorkPackageDataSet();
            AddAircraftToDataset(dataset);
            AddItemsToDataSet(dataset);
            AddAdditionalDataToDataSet(dataset);
     
            return dataset;
        }

        #endregion

        #region protected virtual void AddDirectivesToDataSet(DirectiveListReportDataSet dataset)

        /// <summary>
        /// ���������� �������� � ������� ������
        /// </summary>
        /// <param name="dataset">�������, � ������� ����������� ������</param>
        protected virtual void AddItemsToDataSet(WorkPackageDataSet dataset)
        {
            int i;
            for (i = 0; i < _reportedWorkPackage.AdStatus.Count; i++)
                 AddADStatusItemToDataset(_reportedWorkPackage.AdStatus[i], dataset);
            for (i = 0; i < _reportedWorkPackage.Components.Count; i++)
                AddDetailItemToDataset(_reportedWorkPackage.Components[i], dataset);
        }

        #endregion

        #region protected void AddAircraftToDataset(Aircraft aircraft, DirectiveListReportDataSet destinationDataSet)

        /// <summary>
        /// ����������� ������� � ������� ������
        /// </summary>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        protected virtual void AddAircraftToDataset(WorkPackageDataSet destinationDataSet)
        {
    
            if (ReportedAircraft == null)
                return;
	        var aircraftLifelength = GlobalObjects.CasEnvironment.Calculator.GetCurrentFlightLifelength(_reportedAircraft);
			string registrationNumber = ReportedAircraft.RegistrationNumber;
            string model = _reportedAircraft.Model.ToString();
            string serialNumber = ReportedAircraft.SerialNumber;
            string manufactureDate = SmartCore.Auxiliary.Convert.GetDateFormat(ReportedAircraft.ManufactureDate);
            string sinceNewHours = aircraftLifelength.Hours.ToString().Trim();
            string sinceNewCycles = aircraftLifelength.Cycles.ToString().Trim();
            string lineNumberCaption = "";
            string variableNumberCaption = "";

            string lineNumber = (ReportedAircraft).LineNumber;
            string variableNumber = (ReportedAircraft).VariableNumber;
            if (lineNumber != "")
                lineNumberCaption = "L/N:";
            if (variableNumber != "")
                variableNumberCaption = "V/N:";
            destinationDataSet.
                AircraftInformationTable.
                    AddAircraftInformationTableRow(registrationNumber, serialNumber, manufactureDate,
                                                   model, lineNumberCaption, variableNumberCaption,
                                                   lineNumber,variableNumber,sinceNewHours, sinceNewCycles);
        }

        #endregion

        #region public private void AddADStatusItemToDataset(BaseDetailDirective directive, DirectiveListReportDataSet destinationDataSet)

        /// <summary>
        /// ����������� ������� � ������� ������
        /// </summary>
        /// <param name="adStatusItem">���������� ���������</param>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        private void AddADStatusItemToDataset(Directive adStatusItem, WorkPackageDataSet destinationDataSet)
        {
            GlobalObjects.PerformanceCalculator.GetNextPerformance(adStatusItem);

            string ataCapter = adStatusItem.ATAChapter.ToString();
            string reference = adStatusItem.Description;
            string description = adStatusItem.WorkType.ToString();
            string lastPerformance = (adStatusItem.LastPerformance == null ? "" : adStatusItem.LastPerformance.ToString());
            string mansHours = adStatusItem.ManHours.ToString();
            string overdue = (adStatusItem.Remains == null ? "" : adStatusItem.Remains.ToString());// ADStatusItem.ToString();
            string cost = adStatusItem.Cost.ToString();
            string approxDate = adStatusItem.NextPerformanceDate != null
                ? SmartCore.Auxiliary.Convert.GetDateFormat((DateTime)adStatusItem.NextPerformanceDate)
                : "";

            const string groupName = "ADStatusItem";
            destinationDataSet.
                Items.
                    AddItemsRow(ataCapter, reference, description, "Work Type",
                                mansHours, groupName, cost, overdue, approxDate, lastPerformance);

        }

        #endregion

        #region public override void AddDetailItemToDataset(Detail componentStatus, WorkPackageDataSet destinationDataSet)

        /// <summary>
        /// ����������� ������� ServiceBulletin � ������� ������
        /// </summary>
        /// <param name="componentStatus">���������� ���������</param>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        public void AddDetailItemToDataset(Component componentStatus, WorkPackageDataSet destinationDataSet)
        {

            string groupName = "Detail";
            destinationDataSet.
                Items.
                    AddItemsRow("MyAtaChapter", "MyRefNo", "MyDescription", "MyWorkType",
                                "MyManHours", groupName, "Cost", "Overdue", "apdate", "Complitance");

        }

        #endregion

        #region protected void AddAdditionalDataToDataSet(WorkPackagesDataSet destinationDateSet, bool addRegistrationNumber)

        /// <summary>
        /// ���������� �������������� ���������� 
        /// </summary>
        /// <param name="destinationDateSet"></param>
        protected void AddAdditionalDataToDataSet(WorkPackageDataSet destinationDateSet)
        {
            var reportHeader = ReportedAircraft.RegistrationNumber + ". " + ReportTitle;
            DateAsOf = SmartCore.Auxiliary.Convert.GetDateFormat(DateTime.Today);

            var reportFooter = new GlobalTermsProvider()["ReportFooter"].ToString();
            var reportFooterPrepared = new GlobalTermsProvider()["ReportFooterPrepared"].ToString();
            var reportFooterLink = new GlobalTermsProvider()["ProductWebsite"].ToString();
        
            destinationDateSet.
                AdditionalDataTAble.
                    AddAdditionalDataTAbleRow(GlobalObjects.CasEnvironment.Operators.First(o => o.ItemId == _reportedAircraft.OperatorId).LogoTypeWhite,
                                              reportHeader, "MyDate", DateAsOf, 
                                              "MyMansHours", reportFooter, reportFooterPrepared, 
                                              reportFooterLink);

        }

        #endregion

        #endregion

    }
}