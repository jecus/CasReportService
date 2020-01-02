using System;
using System.Collections.Generic;
using CASReports.Datasets;
using CASReports.Models;
using CASReports.ReportTemplates;

namespace CASReports.Builders
{
    /// <summary>
    /// ����������� ������� ��� ������ ��������
    /// </summary>
    public class WorkPackageListReportBuilder : AbstractReportBuilder 
    {

        #region Fields

        private readonly LifelengthFormatter _lifelengthFormatter = new LifelengthFormatter();
        private Aircraft _reportedAircraft;
        private Operator _reportedOperator;
        private List<WorkPackage> _reportedDirectives = new List<WorkPackage>();
        private string _dateAsOf = "";
        private string _reportTitle = "WorkPackage list report";
        private bool _isFiltered;
        private byte[] _operatorLogotype;

        private Lifelength _lifelengthAircraftSinceNew;
        private Lifelength _lifelengthAircraftSinceOverhaul;

        #endregion

        #region Properties

        #region public Aircraft ReportedAircraft

        /// <summary>
        /// �� ����������� � �����
        /// </summary>
        public Aircraft ReportedAircraft
        {
            get
            {
                return _reportedAircraft;
            }
            set
            {
                _reportedAircraft = value;
                OperatorLogotype = GlobalObjects.CasEnvironment.Operators.First(o => o.ItemId == _reportedAircraft.OperatorId).LogoTypeWhite;
                _reportedDirectives.Clear();
            }
        }

        #endregion

        #region public Aircraft ReportedAircraft

        /// <summary>
        /// �� ����������� � �����
        /// </summary>
        public Operator ReportedOperator
        {
            get
            {
                return _reportedOperator;
            }
            set
            {
                _reportedOperator = value;
                OperatorLogotype = _reportedOperator.LogoTypeWhite;
                _reportedDirectives.Clear();
            }
        }

        #endregion
       
        #region public List<Directive> ReportedDirectives

        /// <summary>
        /// ��������� ���������� � �����
        /// </summary>
        public List<WorkPackage> ReportedDirectives
        {
            get
            {
                return _reportedDirectives;
            }
            set
            {
                _reportedDirectives = value;
            }
        }

        #endregion

        #region public string DateAsOf

        /// <summary>
        /// ����� ���� DateAsOf
        /// </summary>
        public string DateAsOf
        {
            get { return _dateAsOf; }
            set { _dateAsOf = value; }
        }

        #endregion

        #region public string ReportTitle

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public string ReportTitle
        {
            get { return _reportTitle; }
            set { _reportTitle = value; }
        }

        #endregion

        #region public LifelengthFormatter LifelengthFormatter

        /// <summary>
        /// ����������� ������ ���������� � ���������
        /// </summary>
        public LifelengthFormatter LifelengthFormatter
        {
            get { return _lifelengthFormatter; }
        }

        #endregion

        #region public bool IsFiltered
        /// <summary>
        /// ������������ �� ����� � ������
        /// </summary>
        public bool IsFiltered
        {
            get { return _isFiltered; }
            set { _isFiltered = value; }
        }
        #endregion

        #region public Image OperatorLogotype

        /// <summary>
        /// ���������� ��� ������������� ������ ������������
        /// </summary>
        public byte[] OperatorLogotype
        {
            get
            {
                return _operatorLogotype;
            }
            set
            {
                _operatorLogotype = value;
            }
        }

        #endregion

        #region public Lifelength LifelengthAircraftSinceNew

        /// <summary>
        /// ��������� �� SinceNew
        /// </summary>
        public Lifelength LifelengthAircraftSinceNew
        {
            get { return _lifelengthAircraftSinceNew; }
            set { _lifelengthAircraftSinceNew = value; }
        }

        #endregion

        #region public Lifelength LifelengthAircraftSinceOverhaul

        /// <summary>
        /// ��������� �� SinceOverhaul
        /// </summary>
        public Lifelength LifelengthAircraftSinceOverhaul
        {
            get { return _lifelengthAircraftSinceOverhaul; }
            set { _lifelengthAircraftSinceOverhaul = value; }
        }

        #endregion

        #endregion

        #region Methods

        #region public void AddDirectives(CpcpItem[] directives)

        public void AddDirectives(WorkPackage[] directives)
        {
            _reportedDirectives.Clear();
            _reportedDirectives.AddRange(directives);//9.11 My
            if (_reportedDirectives.Count == 0)
                return;
        }

        #endregion

        #region public override object GenerateReport()

        /// <summary>
        /// �������������� ����� �� ������, ����������� � ������� ������
        /// </summary>
        /// <returns>����������� �����</returns>
        public override object GenerateReport()
        {
            WorkPackageListReport report = new WorkPackageListReport();
            report.SetDataSource(GenerateDataSet());
            return report;
        }

        #endregion

        #region public virtual DirectiveListReportDataSet GenerateDataSet()

        /// <summary>
        /// ��������� �������� ������ (DataSet) ��� ������ � �����
        /// </summary>
        /// <returns></returns>
        public virtual WorkPackagesDataSet GenerateDataSet()
        {
            WorkPackagesDataSet dataset = new WorkPackagesDataSet();
            AddAircraftToDataset(dataset);
            AddDirectivesToDataSet(dataset);
            AddAdditionalDataToDataSet(dataset);
     
            return dataset;
        }

        #endregion

        #region protected virtual void AddDirectivesToDataSet(DirectiveListReportDataSet dataset)

        /// <summary>
        /// ���������� �������� � ������� ������
        /// </summary>
        /// <param name="dataset">�������, � ������� ����������� ������</param>
        protected virtual void AddDirectivesToDataSet(WorkPackagesDataSet dataset)
        {
            foreach (var workPackage in _reportedDirectives)
            {
                AddDirectiveToDataset(workPackage, dataset);
            }
        }

        #endregion

        #region public void AddAircraftToDataset(Aircraft aircraft, DirectiveListReportDataSet destinationDataSet)

        /// <summary>
        /// ����������� ������� � ������� ������
        /// </summary>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        protected virtual void AddAircraftToDataset(WorkPackagesDataSet destinationDataSet)
        {
            if (ReportedAircraft == null)
                return;
            string registrationNumber = ReportedAircraft.RegistrationNumber;
            string serialNumber = ReportedAircraft.SerialNumber;
            string manufactureDate = SmartCore.Auxiliary.Convert.GetDateFormat(ReportedAircraft.ManufactureDate);
            Lifelength lifelength = GlobalObjects.CasEnvironment.Calculator.GetCurrentFlightLifelength(_reportedAircraft);
            string sinceNewHours = lifelength.Hours.ToString().Trim();
            string sinceNewCycles = lifelength.Cycles.ToString().Trim();
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
                                                   lineNumberCaption, variableNumberCaption, lineNumber,
                                                   variableNumber, sinceNewHours, sinceNewCycles);
        }

        #endregion

        #region public override void AddDirectiveToDataset(BaseDetailDirective directive, DirectiveListReportDataSet destinationDataSet)

        /// <summary>
        /// ����������� ������� � ������� ������
        /// </summary>
        /// <param name="directive">���������� ���������</param>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        private void AddDirectiveToDataset(WorkPackage directive, WorkPackagesDataSet destinationDataSet)
        {
            string date;

            if(directive.Status == WorkPackageStatus.Opened)
                date = SmartCore.Auxiliary.Convert.GetDateFormat(directive.OpeningDate);
            else if (directive.Status == WorkPackageStatus.Closed)
                date = SmartCore.Auxiliary.Convert.GetDateFormat(directive.ClosingDate);
            else
                date = SmartCore.Auxiliary.Convert.GetDateFormat(directive.PublishingDate);

            destinationDataSet.
                WorkPackagesTable.
                    AddWorkPackagesTableRow(directive.Title, directive.Description, directive.Status.ToString(),
                                            date, directive.Author, directive.Remarks, directive.Number);

        }

        #endregion

        #region protected void AddAdditionalDataToDataSet(WorkPackagesDataSet destinationDateSet, bool addRegistrationNumber)

        /// <summary>
        /// ���������� �������������� ���������� 
        /// </summary>
        /// <param name="destinationDateSet"></param>
        private void AddAdditionalDataToDataSet(WorkPackagesDataSet destinationDateSet)
        {
            DateAsOf = SmartCore.Auxiliary.Convert.GetDateFormat(DateTime.Today);

            string reportHeader;
            string model;

            if(_reportedAircraft != null)
            {
                reportHeader = ReportedAircraft.RegistrationNumber + ". " + ReportTitle;
                model = ReportedAircraft.Model.ToString();   
            }
            else
            {
                reportHeader = ReportTitle;
                model = "";
            }
            
            if (_isFiltered)
                reportHeader += ". Filtered";
            string reportFooter = new GlobalTermsProvider()["ReportFooter"].ToString();
            string reportFooterPrepared = new GlobalTermsProvider()["ReportFooterPrepared"].ToString();
            string reportFooterLink = new GlobalTermsProvider()["ProductWebsite"].ToString();
        
            destinationDateSet.
                AdditionalDataTable.
                    AddAdditionalDataTableRow(_operatorLogotype, 
                                              reportHeader, model, DateAsOf, 
                                              reportFooter,reportFooterPrepared, reportFooterLink);

        }

        #endregion

        #endregion

    }
}