using System.Collections.Generic;
using LTR.Core;
using LTR.Core.Types.Aircrafts.Parts;
using LTR.Core.Types.Directives;
using LTR.Core.Types.ReportFilters;
using LTR.Settings;
using LTRReports.Datasets;
using LTRReports.ReprotTemplates;

namespace LTRReports
{
    /// <summary>
    /// 
    /// </summary>
    public class DirectiveListReportBuilder
    {
        #region Fields
        /// <summary>
        /// ����������� ������ ���������� � ���������
        /// </summary>
        LifelengthFormatter lifelengthFormatter = new LifelengthFormatter();
        /// <summary>
        /// ��������� ���������� � �����
        /// </summary>
        private List<Operator> reportedOperators = new List<Operator>();
        /// <summary>
        /// �� ���������� � �����
        /// </summary>
        private List<Aircraft> reportedAircrafts = new List<Aircraft>();
        /// <summary>
        /// ������� �������� ���������� � �����
        /// </summary>
        private List<BaseDetail> reportedBaseDetails = new List<BaseDetail>();
        /// <summary>
        /// ��������� ���������� � �����
        /// </summary>
        private List<BaseDetailDirective> reportedDirectives = new List<BaseDetailDirective>();

        private string dateAsOf = "Print date";

        AllDirectiveFilter defaultFilter = new AllDirectiveFilter();
        private string model = "";

        #endregion

        #region Constructors

        #region public DirectiveListReportBuilder(BaseDetail reportedBaseDetail)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� �������� �������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedBaseDetail">������� ��� �������� ��������� �����</param>
        public DirectiveListReportBuilder(BaseDetail reportedBaseDetail)
        {
            Add(reportedBaseDetail);
        }

        #endregion

        #region public DirectiveListReportBuilder(BaseDetail[] reportedBaseDetails)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ������� ��������� � ���� ��������
        /// </summary>
        /// <param name="reportedBaseDetails">�������� ��� ������� ��������� �����</param>
        public DirectiveListReportBuilder(BaseDetail[] reportedBaseDetails)
        {
            Add(reportedBaseDetails);
        }

        #endregion

        #region public DirectiveListReportBuilder(Aircraft reportedAircraft)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� ��, ��� ������� ��������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedAircraft">������� ��� �������� ��������� �����</param>
        public DirectiveListReportBuilder(Aircraft reportedAircraft)
        {
            Add(reportedAircraft);
        }

        #endregion

        #region public DirectiveListReportBuilder(Aircraft[] reportedAircrafts)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ���������, �� ������� ��������� � ��������, ������������ � ���
        /// </summary>
        /// <param name="reportedAircrafts">�������� ��� ������� ��������� �����</param>
        public DirectiveListReportBuilder(Aircraft[] reportedAircrafts)
        {
            Add(reportedAircrafts);
        }

        #endregion

        #region public DirectiveListReportBuilder(Operator[] reportedOperators)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ����������, �� �� ��� ������� ��������� � ���� ��������
        /// </summary>
        /// <param name="reportedOperators">�������� ��� ������� ��������� �����</param>
        public DirectiveListReportBuilder(Operator[] reportedOperators)
        {
            Add(reportedOperators);
        }

        #endregion

        #region public DirectiveListReportBuilder(Operator reportedOperator)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� ���������, ��� �� � ������� ��������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedOperator">������� ��� �������� ��������� �����</param>
        public DirectiveListReportBuilder(Operator reportedOperator)
        {
            Add(reportedOperator);
        }

        #endregion

        #region public DirectiveListReportBuilder(BaseDetailDirective[] directives)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ��������
        /// </summary>
        /// <param name="directives">�������� ��� ������� ��������� �����</param>
        public DirectiveListReportBuilder(BaseDetailDirective[] directives)
        {
            Add(directives);
        }

        #endregion

        #region public DirectiveListReportBuilder()

        /// <summary>
        /// ��������� ������ ��� �������� �������. ���������� ������
        /// </summary>
        public DirectiveListReportBuilder()
        {
        }

        #endregion

        #endregion

        #region Properties

        #region public Operator[] ReportedOperators

        /// <summary>
        /// ��������� ���������� � �����
        /// </summary>
        public Operator[] ReportedOperators
        {
            get { return reportedOperators.ToArray(); }
        }

        #endregion

        #region public Aircraft[] ReportedAircrafts

        /// <summary>
        /// �� ���������� � �����
        /// </summary>
        public Aircraft[] ReportedAircrafts
        {
            get { return reportedAircrafts.ToArray(); }
        }

        #endregion

        #region public BaseDetail[] ReportedBaseDetails

        /// <summary>
        /// ������� �������� ���������� � �����
        /// </summary>
        public BaseDetail[] ReportedBaseDetails
        {
            get { return reportedBaseDetails.ToArray(); }
        }

        #endregion

        #region public Directive[] ReportedDirectives

        /// <summary>
        /// ��������� ���������� � �����
        /// </summary>
        public Directive[] ReportedDirectives
        {
            get { return reportedDirectives.ToArray(); }
        }

        #endregion

        #region public string DateAsOf

        /// <summary>
        /// ����� ���� DateAsOf
        /// </summary>
        public string DateAsOf
        {
            get { return dateAsOf; }
            set { dateAsOf = value; }
        }

        #endregion

        #region public virtual string ReportTitle

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public virtual string ReportTitle
        {
            get { return "Directive list report"; }
            set { }
        }

        #endregion

        #region public virtual DirectiveFilter DefaultFilter

        /// <summary>
        /// ������ �� ���������
        /// </summary>
        public virtual DirectiveFilter DefaultFilter
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

        #region public void Add(BaseDetail[] addedBaseDetails)
        /// <summary>
        /// ���������� ������ ��������� � �����
        /// </summary>
        /// <param name="addedBaseDetails">����������� ������� ��������</param>
        public void Add(BaseDetail[] addedBaseDetails)
        {
            for (int i = 0; i < addedBaseDetails.Length; i++)
            {
                Add(addedBaseDetails[i]);
            }
        }

        #endregion

        #region public void Add(Aircraft[] addedAircrafts)

        /// <summary>
        /// ���������� ������ ��������� � �����
        /// </summary>
        /// <param name="addedAircrafts">����������� ��</param>
        public void Add(Aircraft[] addedAircrafts)
        {
            for (int i = 0; i < addedAircrafts.Length; i++)
            {
                Add(addedAircrafts[i]);
            }
        }

        #endregion

        #region public void Add(Operator[] addedOperators)

        /// <summary>
        /// ���������� ������ ��������� � �����
        /// </summary>
        /// <param name="addedOperators">����������� ���������</param>
        public void Add(Operator[] addedOperators)
        {
            for (int i = 0; i < addedOperators.Length; i++)
            {
                Add(addedOperators[i]);
            }
        }

        #endregion

        #region public void Add(BaseDetailDirective[] addedDirectives)

        /// <summary>
        /// ���������� ������ ��������� � �����
        /// </summary>
        /// <param name="addedDirectives">����������� ���������</param>
        public void Add(BaseDetailDirective[] addedDirectives)
        {
            for (int i = 0; i < addedDirectives.Length; i++)
            {
                Add(addedDirectives[i]);
            }
        }

        #endregion

        #region public void Add(BaseDetail detail)

        /// <summary>
        /// ���������� �������� �������� � ���� ������������ � ��� ��������
        /// </summary>
        /// <param name="detail">����������� �������</param>
        public void Add(BaseDetail detail)
        {
            if (detail == null)
                return;
            if (reportedBaseDetails.Contains(detail))
                return;
            AddParent(detail);
            for (int i = 0; i < detail.Directives.Length; i++)
            {
                Add(detail.Directives[i] as BaseDetailDirective);
            }            
        }

        #endregion

        #region public void Add(Operator _operator)

        /// <summary>
        /// ���������� ��������� � ���� ��� ��, ������� ��������� � ��������
        /// </summary>
        /// <param name="_operator">����������� �������</param>
        public void Add(Operator _operator)
        {
            if (_operator == null)
                return;
            if (reportedOperators.Contains(_operator))
                return;
            AddParent(_operator);
            for (int i = 0; i < _operator.AircraftCollection.Count; i++)
            {
                Add(_operator.AircraftCollection[i]);
            }  
        }

        #endregion

        #region public void Add(Aircraft aircraft)

        /// <summary>
        /// ���������� �� � ���� ��� ������� ��������� � ��������
        /// </summary>
        /// <param name="aircraft">����������� �������</param>
        public void Add(Aircraft aircraft)
        {
            if (aircraft == null)
                return;
            if (reportedAircrafts.Contains(aircraft))
                return;
            AddParent(aircraft);
            for (int i = 0; i < aircraft.Directives.Length; i++)
            {
                if (aircraft.Directives[i] is BaseDetailDirective)
                    Add((BaseDetailDirective)aircraft.Directives[i]);
            }  
        }

        #endregion

        #region public void Add(BaseDetailDirective directive)

        /// <summary>
        /// ���������� ���������
        /// </summary>
        /// <param name="directive">����������� �������</param>
        public void Add(BaseDetailDirective directive)
        {
            if (directive == null)
                return;
            if (reportedDirectives.Contains(directive))
                return;
            AddParent(directive);
        }

        #endregion

        #region public void AddParent(BaseDetailDirective directive)

        /// <summary>
        /// ���������� ��������� � ����� � ���� �� �������
        /// </summary>
        /// <param name="directive">����������� �������</param>
        public void AddParent(BaseDetailDirective directive)
        {
            if (directive == null)
                return;
            if (reportedDirectives.Contains(directive))
                return;
            reportedDirectives.Add(directive);
            AddParent(directive.Parent as BaseDetail);
        }

        #endregion

        #region public void AddParent(BaseDetail baseDetail)

        /// <summary>
        /// ���������� ������������ ���������
        /// </summary>
        /// <param name="baseDetail">����������� ������� �������</param>
        public void AddParent(BaseDetail baseDetail)
        {
            if (baseDetail == null)
                return;
            if (reportedBaseDetails.Contains(baseDetail))
                return;
            reportedBaseDetails.Add(baseDetail);
            AddParent(baseDetail.ParentAircraft);
        }

        #endregion

        #region public void AddParent(Aircraft aircraft)

        /// <summary>
        /// ���������� ������������ ���������
        /// </summary>
        /// <param name="aircraft">����������� �������</param>
        public void AddParent(Aircraft aircraft)
        {
            if (aircraft == null)
                return;
            if (reportedAircrafts.Contains(aircraft))
                return;
            reportedAircrafts.Add(aircraft);
            AddParent(aircraft.Operator);
        }

        #endregion

        #region public void AddParent(Operator _operator)

        /// <summary>
        /// ���������� ������������ ���������
        /// </summary>
        /// <param name="_operator">����������� ��������</param>
        public void AddParent(Operator _operator)
        {
            if (_operator == null)
                return;
            if (reportedOperators.Contains(_operator))
                return;
            reportedOperators.Add(_operator);
        }

        #endregion

        #region public ADReport GenerateReport()

        /// <summary>
        /// �������������� ����� �� ������, ����������� � ������� ������
        /// </summary>
        /// <returns>����������� �����</returns>
        public ADReport GenerateReport()
        {
            ADReport report = new ADReport();
            report.SetDataSource(GenerateDataSet());
                return report;
        }

        #endregion

        #region public DirectiveListReportDataSet GenerateDataSet()

        /// <summary>
        /// ��������� �������� ������ (DataSet) ��� ������ � �����
        /// </summary>
        /// <returns></returns>
        public DirectiveListReportDataSet GenerateDataSet()
        {
            DirectiveListReportDataSet dataset = new DirectiveListReportDataSet();
            AddOperatorsToDataset(dataset);
            AddAircraftsToDataset(dataset);
            AddBaseDetailsToDataset(dataset);
            AddDirectivesToDataSet(dataset);
            dataset.AdditionalDataTAble.AddAdditionalDataTAbleRow(1, ReportTitle, DateAsOf, Model, ProgramSettings.ReportFooter, ProgramSettings.ReportFooterPrepared, ProgramSettings.WebSite);
            return dataset;
        }

        #endregion

        #region protected virtual void AddOperatorsToDataset(DirectiveListReportDataSet dataset)

        /// <summary>
        /// ���������� ���������� � ������� ������
        /// </summary>
        /// <param name="dataset">�������, � ������� ����������� ������</param>
        protected virtual void AddOperatorsToDataset(DirectiveListReportDataSet dataset)
        {
            for (int i = 0; i < reportedOperators.Count; i++)
            {
                AddOperatorToDataset(reportedOperators[i], dataset);
            }
        }

        #endregion

        #region protected virtual void AddAircraftsToDataset(DirectiveListReportDataSet dataset)

        /// <summary>
        /// ���������� �� � ������� ������
        /// </summary>
        /// <param name="dataset">�������, � ������� ����������� ������</param>
        protected virtual void AddAircraftsToDataset(DirectiveListReportDataSet dataset)
        {
            for (int i = 0; i < reportedAircrafts.Count; i++)
            {
                AddAircraftToDataset(reportedAircrafts[i], dataset);
            }
        }

        #endregion

        #region protected virtual void AddBaseDetailsToDataset(DirectiveListReportDataSet dataset)

        /// <summary>
        /// ���������� ������� ��������� � ������� ������
        /// </summary>
        /// <param name="dataset">�������, � ������� ����������� ������</param>
        protected virtual void AddBaseDetailsToDataset(DirectiveListReportDataSet dataset)
        {
            for (int i = 0; i < reportedBaseDetails.Count; i++)
            {
                if (!(reportedBaseDetails[i] is AircraftContainer)) AddBaseDetailToDataset(reportedBaseDetails[i], dataset);
            }
        }

        #endregion

        #region protected virtual void AddDirectivesToDataSet(DirectiveListReportDataSet dataset)

        /// <summary>
        /// ���������� �������� � ������� ������
        /// </summary>
        /// <param name="dataset">�������, � ������� ����������� ������</param>
        protected virtual void AddDirectivesToDataSet(DirectiveListReportDataSet dataset)
        {
            for (int i = 0; i < reportedDirectives.Count; i++)
            {
                AddDirectiveToDataset(reportedDirectives[i], dataset);
            }
        }

        #endregion

        #region public void AddLifelengthIncrementToDataset(Operator _operator, DirectiveListReportDataSet destinationDataSet)
         
        /// <summary>
        /// ����������� ������� � ������� ������
        /// </summary>
        /// <param name="_operator">���������� ��������</param>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        public virtual void AddOperatorToDataset(Operator _operator, DirectiveListReportDataSet destinationDataSet)
        {
            destinationDataSet.OperatorTable.AddOperatorTableRow(_operator.ID, _operator.Name, _operator.ICAOCode);
        }

        #endregion

        #region public void AddAircraftToDataset(Aircraft aircraft, DirectiveListReportDataSet destinationDataSet)

        /// <summary>
        /// ����������� ������� � ������� ������
        /// </summary>
        /// <param name="aircraft">���������� ��</param>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        public virtual void AddAircraftToDataset(Aircraft aircraft, DirectiveListReportDataSet destinationDataSet)
        {
            DirectiveListReportDataSet.OperatorTableRow operatorId = destinationDataSet.OperatorTable.FindByOperatorID(aircraft.Parent.ID);
            string model = aircraft.Model;
            string serialNumber = aircraft.SerialNumber;
            string manufactureDate = aircraft.ManufactureDate.ToString("MMM dd, yyyy");
            string SinceNewHours = lifelengthFormatter.GetHoursData(aircraft.Limitation.ResourceSinceNew.Hours).Trim();
            string sinceNewCycles = aircraft.Limitation.ResourceSinceNew.Cycles.ToString().Trim();
            string sinceNewCalendar = lifelengthFormatter.GetCalendarData(aircraft.Limitation.ResourceSinceNew.Calendar).Trim();
            string SinceOverhaulHours = lifelengthFormatter.GetHoursData(aircraft.Limitation.ResourceSinceOverhaul.Hours).Trim();
            string sinceOverhaulCycles = aircraft.Limitation.ResourceSinceOverhaul.Cycles.ToString().Trim();
            string sinceOverhaulCalendar = lifelengthFormatter.GetCalendarData(aircraft.Limitation.ResourceSinceOverhaul.Calendar).Trim();
            string registrationNumber = aircraft.RegistrationNumber;
            string lineNumber = "-";
            string variableNumber = "-";
            if (aircraft is WestAircraft)
            {
                lineNumber = ((WestAircraft)aircraft).LineNumber;
                variableNumber = ((WestAircraft)aircraft).VariableNumber;
            }
            destinationDataSet.BaseDetailTable.AddBaseDetailTableRow(operatorId, aircraft.ID, serialNumber,
                                                                     manufactureDate, model, SinceNewHours, sinceNewCycles, sinceNewCalendar, SinceOverhaulHours, sinceOverhaulCycles, sinceOverhaulCalendar, registrationNumber, lineNumber, variableNumber);
        }

        #endregion

        #region public void AddBaseDetailToDataset(BaseDetail baseDetail, DirectiveListReportDataSet destinationDataSet)

        /// <summary>
        /// ����������� ������� � ������� ������
        /// </summary>
        /// <param name="baseDetail">���������� ������� �������</param>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        public virtual void AddBaseDetailToDataset(BaseDetail baseDetail, DirectiveListReportDataSet destinationDataSet)
        {
            DirectiveListReportDataSet.OperatorTableRow parentOperator = destinationDataSet.OperatorTable.FindByOperatorID(baseDetail.ParentAircraft.Parent.ID);
            string serialNumber = baseDetail.SerialNumber;
            string manufactureDate = baseDetail.ManufactureDate.ToString("MMM dd, yyyy");
            string model = baseDetail.Model;
            string sinceNewHours = ((int) baseDetail.Lifelength.Hours.TotalHours).ToString().Trim();
            string sinceNewCycles = baseDetail.Lifelength.Cycles.ToString().Trim();
            string sinceNewCalendar = baseDetail.Lifelength.Calendar.ToString().Trim();
            string sinceOverhaulHours = ((int)baseDetail.Limitation.ResourceSinceOverhaul.Hours.TotalHours).ToString().Trim();
            string sinceOverhaulCycles = baseDetail.Limitation.ResourceSinceOverhaul.Cycles.ToString().Trim();
            string sinceOverhaulCalendar = (baseDetail.Limitation.ResourceSinceOverhaul.Calendar).ToString().Trim();
            destinationDataSet.BaseDetailTable.AddBaseDetailTableRow(
                parentOperator, baseDetail.ID,
                serialNumber, manufactureDate, model, sinceNewHours,
                sinceNewCycles, sinceNewCalendar,
                sinceOverhaulHours,
                sinceOverhaulCycles,
                sinceOverhaulCalendar, "", "", "");
        }

        #endregion

        #region public void AddDirectiveToDataset(Directive directive, DirectiveListReportDataSet destinationDataSet)

        /// <summary>
        /// ����������� ������� � ������� ������
        /// </summary>
        /// <param name="directive">���������� ���������</param>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        public virtual void AddDirectiveToDataset(BaseDetailDirective directive, DirectiveListReportDataSet destinationDataSet)
        {
            if (!DefaultFilter.Acceptable(directive))
                return;
            string applicability = directive.Applicability;
            //if (applicability == null || string.IsNullOrEmpty(applicability)) applicability = "-";
            string remarks = directive.Remarks;
            string description = directive.Description;
            string title = directive.Title;
            string references = directive.References;
            string condition = directive.Condition.ToString();
            condition = condition.Substring(0, 5);
            if (directive.Closed) condition = "Close";
            string effectivityDate = directive.EffectivityDate.ToString("MMM dd, yyyy");
            string ThresholdSinceNew =
                lifelengthFormatter.GetData(directive.FirstPerformSinceNew, "h\r\n", "cyc\r\n", "");
            string thresholdSInceEffectivityDate =
                lifelengthFormatter.GetData(directive.SinceEffectivityDatePerformanceLifelength, "h\r\n", "cyc\r\n", "");
            string repearIntervals = lifelengthFormatter.GetData(directive.RepeatPerform, "h\r\n", "cyc\r\n", "");
            string compliance = lifelengthFormatter.GetData(directive.LastPerformanceLifelength, "h\r\n", "cyc\r\n", "");
            string complianceDate = "-";
            if (directive.LastPerformance != null) complianceDate = directive.LastPerformance.RecordsAddDate.ToString("MMM dd, yyyy");
            string nextLifelength = lifelengthFormatter.GetData(directive.NextPerformance, "h\r\n", "cyc\r\n", "");
            string nextDate = "-"; //todo
            string leftTillNext = lifelengthFormatter.GetData(directive.LeftTillNextPerformance, "h\r\n", "cyc\r\n", "");
            int parentID = directive.Parent.ID;
            if (directive.Parent is AircraftContainer)
                parentID = directive.Parent.Parent.ID;
            destinationDataSet.ItemsTable.AddItemsTableRow(directive.ID,
                                                           destinationDataSet.BaseDetailTable.FindByBaseDetailID(
                                                               parentID), applicability,
                                                           remarks, description, title,
                                                           references, condition,
                                                           effectivityDate,
                                                           ThresholdSinceNew,
                                                           thresholdSInceEffectivityDate,
                                                           repearIntervals,
                                                           compliance, complianceDate, 
                                                               nextLifelength,
                                                               nextDate,
                                                               leftTillNext);
        }

        #endregion

        #region public void Clear()

        /// <summary>
        /// ������� ������������ ���������
        /// </summary>
        public void Clear()
        {
            reportedDirectives.Clear();
            reportedAircrafts.Clear();
            reportedOperators.Clear();
            reportedBaseDetails.Clear();
        }

        #endregion


        #endregion

    }
}
