using System;
using System.Collections.Generic;
using LTR.Core;
using LTR.Core.Types.Aircrafts.Parts;
using LTR.Core.Types.ReportFilters;
using LTR.Settings;
using LTRReports.Datasets;
using LTRReports.ReprotTemplates;

namespace LTRReports
{
    /// <summary>
    /// 
    /// </summary>
    public class DetailListReportBuilder
    {
        #region Fields
        /// <summary>
        /// ����������� ������ ���������� � ���������
        /// </summary>
        LifelengthFormatter lifelengthFormatter = new LifelengthFormatter();
        /// <summary>
        /// �������� ��
        /// </summary>
        private Operator reportedOperator;
        /// <summary>
        /// �� ��� �������� ������������ �����
        /// </summary>
        private Aircraft reportedAircraft;
        /// <summary>
        /// ������ ��������� ��� ������� ������������ �����
        /// </summary>
        private List<Detail> reportedDetails = new List<Detail>();

        private string dateAsOfData = "Print date";

        AllDetailFilter defaultFilter = new AllDetailFilter();
        private string model = "";

        #endregion

        #region Constructors

        #region public DetailListReportBuilder(Aircraft reportedAircraft , Detail[] details, string dateAsOf)

        /// <summary>
        /// ��������� ������ ��� �������� ������� ��������� ��� ��������� ��
        /// </summary>
        /// <param name="reportedAircraft">������� ��� �������� ��������� �����</param>
        /// <param name="details">������ ��������� ��� �����������</param>
        /// <param name="dateAsOf">���� �������� ������</param>
        public DetailListReportBuilder(Aircraft reportedAircraft , Detail[] details, string dateAsOf)
        {
            dateAsOfData = dateAsOf;
            Add(reportedAircraft.Operator);
            Add(reportedAircraft);
            Add(details);
        }

        #endregion

        #region public DetailListReportBuilder()

        /// <summary>
        /// ��������� ������ ��� �������� �������. ���������� ������
        /// </summary>
        public DetailListReportBuilder()
        {
        }

        #endregion

        #endregion

        #region Properties

        #region public Operator ReportedOperator

        /// <summary>
        /// �������� ��
        /// </summary>
        public Operator ReportedOperator
        {
            get { return reportedOperator; }
        }

        #endregion

        #region public Aircraft ReportedAircraft

        /// <summary>
        /// �� ��� �������� ������������ �����
        /// </summary>
        public Aircraft ReportedAircraft
        {
            get { return reportedAircraft; }
        }

        #endregion

        #region public string DateAsOf

        /// <summary>
        /// ����� ���� DateAsOf
        /// </summary>
        public string DateAsOfData
        {
            get { return dateAsOfData; }
            set { dateAsOfData = value; }
        }

        #endregion

        #region public virtual string ReportTitle

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public virtual string ReportTitle
        {
            get { return "Component Status list report"; }
        }

        #endregion

        #region public virtual DetailFilter DefaultFilter

        /// <summary>
        /// ������ �� ���������
        /// </summary>
        public virtual DetailFilter DefaultFilter
        {
            get
            {
                return defaultFilter;
            }
        }

        #endregion

        #region public string Model

        /// <summary>
        /// ����� - ������ ������������� �������� � �������������� ���������
        /// </summary>
        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        #endregion

        #endregion
        
        #region Methods

        #region public void AddResources(Aircraft aircraft, Detail[] details, string dateAsOf)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aircraft">������� ��� �������� ��������� �����</param>
        /// <param name="details">������ ��������� ��� �����������</param>
        /// <param name="dateAsOf">���� �������� ������</param>
        public void AddResources(Aircraft aircraft, Detail[] details, string dateAsOf)
        {
            dateAsOfData = dateAsOf;
            Add(aircraft.Operator);
            Add(aircraft);
            Add(details);
        }
        #endregion

        #region public void Add(Aircraft aircraft)
        /// <summary>
        /// ���������� ���������� �����
        /// </summary>
        /// <param name="aircraft">��</param>
        public void Add(Aircraft aircraft)
        {
            if (aircraft == null) return;
            if (reportedAircraft == aircraft) return;
            reportedAircraft = aircraft;
        }

        #endregion

        #region public void Add(Operator _operator)
        /// <summary>
        /// ���������� ���������
        /// </summary>
        /// <param name="_operator">��������</param>
        public void Add(Operator _operator)
        {
            if (_operator == null) return;
            if (reportedOperator == _operator) return;
            reportedOperator = _operator;
        }
        #endregion

        #region public void Add(Detail detail)
        /// <summary>
        /// ���������� �������� � ������
        /// </summary>
        /// <param name="detail">�������</param>
        public void Add(Detail detail)
        {
            if (detail == null) return;
            if (reportedDetails.Contains(detail)) return;
            reportedDetails.Add(detail);
        }
        #endregion

        #region public void Add(Detail[] details)
        /// <summary>
        /// ���������� �������
        /// </summary>
        /// <param name="details">������ �������</param>
        public void Add(Detail[] details)
        {
            foreach (Detail detail in details)
            {
                Add(detail);
            }
        }
        #endregion

        #region public ComponentStatusReport GenerateReport()

        /// <summary>
        /// �������������� ����� �� ������, ����������� � ������� ������
        /// </summary>
        /// <returns>����������� �����</returns>
        public ComponentStatusReport GenerateReport()
        {
            ComponentStatusReport report = new ComponentStatusReport();
            report.SetDataSource(GenerateDataSet());
                return report;
        }

        #endregion

        #region public DetailListDataSet GenerateDataSet()

        /// <summary>
        /// ��������� �������� ������ (DataSet) ��� ������ � �����
        /// </summary>
        /// <returns></returns>
        public DetailListDataSet GenerateDataSet()
        {
            DetailListDataSet dataset = new DetailListDataSet();
            AddOperatorToDataset(dataset);
            AddAircraftToDataset(dataset);
            AddDetailsToDataset(dataset);
            AddAdditionalInformation(dataset);
            return dataset;
        }

        #endregion

        #region protected void AddAdditionalInformation(DetailListDataSet destinationDateSet)
        /// <summary>
        /// ���������� �������������� ���������� 
        /// </summary>
        /// <param name="destinationDateSet"></param>
        protected void AddAdditionalInformation(DetailListDataSet destinationDateSet)
        {
            destinationDateSet.AdditionalDataTable.AddAdditionalDataTableRow(0, ReportTitle, dateAsOfData,
                                                                             ProgramSettings.ReportFooter,
                                                                             ProgramSettings.ReportFooterPrepared,
                                                                             ProgramSettings.WebSite);
        }
        #endregion

        #region protected void AddOperatorToDataset(DetailListDataSet destinationDataSet)

        /// <summary>
        /// ����������� ��������� � ������� ������
        /// </summary>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        protected virtual void AddOperatorToDataset(DetailListDataSet destinationDataSet)
        {
            destinationDataSet.OperatorInfomationTable.AddOperatorInfomationTableRow(reportedOperator.ID, reportedOperator.Name, reportedOperator.ICAOCode);
        }

        #endregion

        #region protected virtual void AddAircraftToDataset(DetailListDataSet destinationDataSet)

        /// <summary>
        /// ����������� ������� � ������� ������
        /// </summary>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        protected virtual void AddAircraftToDataset(DetailListDataSet destinationDataSet)
        {
            int operatorId = destinationDataSet.OperatorInfomationTable.FindByOperatorId(reportedAircraft.Parent.ID).OperatorId;
            string registrationNumber = reportedAircraft.RegistrationNumber;
            string serialNumber = reportedAircraft.SerialNumber;
            string manufactureDate = reportedAircraft.ManufactureDate.ToString("MMM dd, yyyy");
            string lineNumber = "";
            string variableNumber = "";
            string SinceNewHours = lifelengthFormatter.GetHoursData(reportedAircraft.Limitation.ResourceSinceNew.Hours).Trim();
            int sinceNewCycles = reportedAircraft.Limitation.ResourceSinceNew.Cycles;
            string SinceOverhaulHours = lifelengthFormatter.GetHoursData(reportedAircraft.Limitation.ResourceSinceOverhaul.Hours).Trim();
            int sinceOverhaulCycles = reportedAircraft.Limitation.ResourceSinceOverhaul.Cycles;
            if (reportedAircraft is WestAircraft)
            {
                lineNumber = ((WestAircraft)reportedAircraft).LineNumber;
                variableNumber = ((WestAircraft)reportedAircraft).VariableNumber;
            }
            string model = reportedAircraft.Model;
            destinationDataSet.AircraftInformationTable.AddAircraftInformationTableRow(reportedAircraft.ID, operatorId, registrationNumber, serialNumber,
                                                                     manufactureDate, lineNumber, variableNumber, SinceNewHours,  sinceNewCycles,  SinceOverhaulHours, sinceOverhaulCycles, model);
        }

        #endregion

        #region protected virtual void AddDetailToDataset(Detail detail, DetailListDataSet destinationDataSet)

        /// <summary>
        /// ����������� ������� � ������� ������
        /// </summary>
        /// <param name="detail">����������� �������</param>
        /// <param name="number">���������� ����� ��������</param>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        protected virtual void AddDetailToDataset(Detail detail,int number,DetailListDataSet destinationDataSet)
        {
            if (!DefaultFilter.Acceptable(detail))
                return;
            int aircraftId = ReportedAircraft.ID;
            string atachapter = detail.AtaChapter.ShortName;
            string componentNumber = number.ToString();
            string atachapterfull = detail.AtaChapter.FullName;
            string partNumber = detail.PartNumber;
            string description = detail.Description;
            string serialNumber = detail.SerialNumber;
            string positionNumber = detail.PositionNumber;
            string maintanceType = detail.MaintenanceType.ShortName;
            string instalationDate = detail.InstallationDate.ToString("MMM dd, yyyy");
            string complianceTSN = "";
            string complianceDate = "";
            string complianceWorkType = "";
            if (detail.Limitation.LastPerformance != null)
            {
                complianceTSN = lifelengthFormatter.GetData(detail.Limitation.LastPerformance.Lifelength, "h\r\n", "cyc\r\n", "");
                DateTime tempDateTime = new DateTime(detail.Limitation.LastPerformance.Lifelength.Calendar.Ticks);
                complianceDate = tempDateTime.ToString("MMM dd, yyyy");
                complianceWorkType = detail.Limitation.LastPerformance.DetailRecordType.ShortName;
            }
            string nextTSN = "";
            string nextDate = "";
            string nextRemains = "";
            string nextWorkType = "";
            if (detail.Limitation.NextPerformance !=null)
            {
                
            }
            string condition = detail.LimitationCondition.GetHashCode().ToString();
            

            destinationDataSet.ItemsTable.AddItemsTableRow(detail.ID,aircraftId, componentNumber,
                                                           atachapter, atachapterfull, partNumber, description,
                                                           serialNumber, positionNumber,
                                                           maintanceType,
                                                           instalationDate,
                                                           complianceTSN,
                                                           complianceDate,
                                                           complianceWorkType, nextTSN, 
                                                               nextDate,
                                                               nextRemains,
                                                               nextWorkType,condition);
        }

        #endregion

        #region protected virtual void AddDetailsToDataset(DetailListDataSet dataset)

        /// <summary>
        /// ���������� ������� ��������� � ������� ������
        /// </summary>
        /// <param name="dataset">�������, � ������� ����������� ������</param>
        protected virtual void AddDetailsToDataset(DetailListDataSet dataset)
        {
            for (int i = 0; i < reportedDetails.Count; i++)
            {
                AddDetailToDataset(reportedDetails[i] ,i+1, dataset);
            }
        }

        #endregion

        #region public void Clear()

        /// <summary>
        /// ������� ������������ ���������
        /// </summary>
        public void Clear()
        {        
            reportedDetails.Clear();
        }

        #endregion
        
        
        #endregion
        
    }
}
