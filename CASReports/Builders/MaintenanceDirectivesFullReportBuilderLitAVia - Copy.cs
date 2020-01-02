using System;
using System.Collections.Generic;
using System.Data;
using CASReports.Datasets;
using CASReports.Models;
using CASReports.ReportTemplates;

namespace CASReports.Builders
{
    public class MaintenanceDirectivesFullReportBuilderLitAVia : AbstractReportBuilder
    {
	    private readonly bool _mpLimit;

	    #region Fields

        private string _reportTitle = "AMP TASKS STATUS";
        private string _filterSelection;
        private byte[] _operatorLogotype;
        private Aircraft _reportedAircraft;
        private BaseComponent _reportedBaseComponent;
        private List<MaintenanceDirective> _reportedDirectives = new List<MaintenanceDirective>();

        private readonly LifelengthFormatter _lifelengthFormatter = new LifelengthFormatter();
        private Forecast _forecast;
        private string _dateAsOf = "";
        private Lifelength _lifelengthAircraftSinceNew;
        private Lifelength _lifelengthAircraftSinceOverhaul;
        private Lifelength _current;
        private DateTime _manufactureDate;
        private Lifelength reportAircraftLifeLenght;

        #endregion

        #region Properties

        #region public Aircraft ReportedAircraft

        /// <summary>
        /// �� ����������� � �����
        /// </summary>
        public Aircraft ReportedAircraft
        {
            set
            {
                _reportedAircraft = value;
                if (value == null) return;
                _reportedBaseComponent = GlobalObjects.ComponentCore.GetBaseComponentById(value.AircraftFrameId);
                _manufactureDate = value.ManufactureDate;
                _current = GlobalObjects.CasEnvironment.Calculator.GetCurrentFlightLifelength(_reportedBaseComponent);
                _operatorLogotype = GlobalObjects.CasEnvironment.Operators.First(o => o.ItemId == _reportedAircraft.OperatorId).LogotypeReportLarge;
            }
        }

        #endregion

        #region public BaseDetail ReportedBaseDetail

        /// <summary>
        /// ������� �������, ���������� � �����
        /// </summary>
        public BaseComponent ReportedBaseComponent
        {
            set
            {
                if (value == null) return;
                _reportedBaseComponent = value;
                _manufactureDate = _reportedBaseComponent.ManufactureDate;
                _current = GlobalObjects.CasEnvironment.Calculator.GetCurrentFlightLifelength(_reportedBaseComponent);
                _operatorLogotype = GlobalObjects.CasEnvironment.Operators[0].LogotypeReportLarge;
                _reportedDirectives.Clear();
                //  reportedDirectives.AddRange(GlobalObjects.CasEnvironment.Loader.GetDefferedItems(reportedBaseDetail));
            }
        }

        #endregion

        #region public List<MaintenanceDirective> ReportedDirectives

        /// <summary>
        /// ��������� ���������� � �����
        /// </summary>
        public List<MaintenanceDirective> ReportedDirectives
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

        #region public Forecast Forecast

        public Forecast Forecast
        {
            set { _forecast = value; }
        }
        #endregion

        #region protected Lifelength Current

        protected Lifelength Current
        {
            get { return _current; }
        }
        #endregion

        #region public CommonFilterCollection ReportTitle

        /// <summary>
        /// ����� ������� ������
        /// </summary>
        public CommonFilterCollection FilterSelection
        {
            set { GetFilterSelection(value); }
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

        #region public Image OperatorLogotype

        /// <summary>
        /// ���������� ��� ������������� ������ ������������
        /// </summary>
        public byte[] OperatorLogotype
        {
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

        public MaintenanceDirectivesFullReportBuilderLitAVia(bool mpLimit = false)
        {
	        _mpLimit = mpLimit;
        }

        #region Methods

        #region protected virtual void GetFilterSelection(CommonFilterCollection filterCollection)
        protected virtual void GetFilterSelection(CommonFilterCollection filterCollection)
        {
            if (_reportedAircraft != null)
            {
                _filterSelection = "All";
                if (filterCollection == null || filterCollection.IsEmpty) 
                    return;
                _filterSelection = filterCollection.ToString();
            }
            else
            {
                if (_reportedBaseComponent != null)
                {
                    _filterSelection = "All";
                    if (filterCollection == null) return;
                    if (_reportedBaseComponent.BaseComponentType == BaseComponentType.LandingGear)
                        _filterSelection = _reportedBaseComponent.TransferRecords.GetLast().Position;
                    if (_reportedBaseComponent.BaseComponentType == BaseComponentType.Engine)
                        _filterSelection = BaseComponentType.Engine + " " + _reportedBaseComponent.TransferRecords.GetLast().Position;
                    if (_reportedBaseComponent.BaseComponentType == BaseComponentType.Apu)
                        _filterSelection = BaseComponentType.Apu.ToString();
                }
                else
                {
                    _filterSelection = "All";
                }
            }

        }
        #endregion

        #region public void AddDirectives(IEnumerable<MaintenanceDirective> directives)

        public void AddDirectives(IEnumerable<MaintenanceDirective> directives)
        {
            _reportedDirectives.Clear();
            _reportedDirectives.AddRange(directives);
            if (_reportedDirectives.Count == 0)
                return;

        }

        #endregion

        #region public override object GenerateReport()

        /// <summary>
        /// ������������� ����� �� ������, ����������� � ������� ������
        /// </summary>
        /// <returns>����������� �����</returns>
        public override object GenerateReport()
        {
            var report = new MaintenanceDirectiveReportLitAvia();
            report.SetDataSource(GenerateDataSet());
            return report;
        }

        #endregion

        #region protected virtual DataSet GenerateDataSet()

        /// <summary>
        /// ��������� �������� ������ (DataSet) ��� ������ � �����
        /// </summary>
        /// <returns></returns>
        protected virtual DataSet GenerateDataSet()
        {
            var dataset = new MaintenanceDirectivesDataSetLatAvia();
            AddAircraftToDataset(dataset);
            AddDirectivesToDataSet(dataset);
            AddAdditionalDataToDataSet(dataset);
            return dataset;
        }

        #endregion

        #region protected virtual void AddDirectivesToDataSet(MaintenanceDirectivesDataSet dataset)

        /// <summary>
        /// ���������� �������� � ������� ������
        /// </summary>
        /// <param name="dataset">�������, � ������� ����������� ������</param>
        protected virtual void AddDirectivesToDataSet(MaintenanceDirectivesDataSetLatAvia dataset)
        {
            /* List<String> colors = new List<string>();
            for (int i = 0; i < HighlightCollection.Instance.Count; i++ )
            {
                colors.Add(HighlightCollection.Instance[i].Color.R.ToString()+" "+
                            HighlightCollection.Instance[i].Color.G.ToString()+" "+
                            HighlightCollection.Instance[i].Color.B.ToString());
            }
            MessageBox.Show(string.Join("\r\n",colors.ToArray()));*/
            foreach (MaintenanceDirective t in _reportedDirectives)
            {
                AddDirectiveToDataset(t, dataset);
            }
        }

        #endregion

        #region private void AddAircraftToDataset(MaintenanceDirectivesDataSet destinationDataSet)

        /// <summary>
        /// ����������� ������� � ������� ������
        /// </summary>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        private void AddAircraftToDataset(MaintenanceDirectivesDataSetLatAvia destinationDataSet)
        {
            if (_reportedAircraft == null)
                return;

            reportAircraftLifeLenght = GlobalObjects.CasEnvironment.Calculator.GetCurrentFlightLifelength(_reportedAircraft);

	        var apu = GlobalObjects.ComponentCore.GetAicraftBaseComponents(_reportedAircraft.ItemId)
		        .FirstOrDefault(i => i.BaseComponentType == BaseComponentType.Apu);

	        var reportApuLifeLenght = GlobalObjects.CasEnvironment.Calculator.GetCurrentFlightLifelength(apu);

			var manufactureDate = _reportedAircraft.ManufactureDate.ToString(new GlobalTermsProvider()["DateFormat"].ToString());
            var serialNumber = _reportedAircraft.SerialNumber;
            var model = _reportedAircraft.Model.FullName;
	        var sinceNewCycles = reportAircraftLifeLenght.Cycles != null ? (int)reportAircraftLifeLenght.Cycles : 0;
            var registrationNumber = _reportedAircraft.RegistrationNumber;
            int averageUtilizationHours;
            int averageUtilizationCycles;
            string averageUtilizationType;
            if (_forecast == null)
            {
				var aircraftFrame = GlobalObjects.ComponentCore.GetBaseComponentById(_reportedAircraft.AircraftFrameId);
				var averageUtilization = GlobalObjects.AverageUtilizationCore.GetAverageUtillization(aircraftFrame);

				averageUtilizationHours = (int)averageUtilization.Hours;
                averageUtilizationCycles = (int)averageUtilization.Cycles;
                averageUtilizationType = averageUtilization.SelectedInterval == UtilizationInterval.Dayly ? "Day" : "Month";
            }
            else
            {
                averageUtilizationHours = (int)_forecast.ForecastDatas[0].AverageUtilization.Hours;
                averageUtilizationCycles = (int)_forecast.ForecastDatas[0].AverageUtilization.Cycles;
                averageUtilizationType =
                   _forecast.ForecastDatas[0].AverageUtilization.SelectedInterval == UtilizationInterval.Dayly ? "Day" : "Month";

            }

            string lineNumber = _reportedAircraft.LineNumber;
            string variableNumber = _reportedAircraft.VariableNumber;
            destinationDataSet.AircraftDataTable.AddAircraftDataTableRow(serialNumber,
                                                                         manufactureDate,
                                                                         reportAircraftLifeLenght.ToHoursMinutesFormat(""),
                                                                         sinceNewCycles,
                                                                         registrationNumber, model, lineNumber, variableNumber,
                                                                         averageUtilizationHours, averageUtilizationCycles, averageUtilizationType, reportApuLifeLenght.Hours.ToString(), reportApuLifeLenght.Cycles.ToString());
        }

        #endregion

        #region protected virtual void AddDirectiveToDataset(object directive, DefferedListDataSet destinationDataSet)

        /// <summary>
        /// ����������� ������� � ������� ������
        /// </summary>
        /// <param name="reportedDirective">���������� ���������</param>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        protected virtual void AddDirectiveToDataset(MaintenanceDirective reportedDirective, MaintenanceDirectivesDataSetLatAvia destinationDataSet)
        {
            if(reportedDirective == null)
                return;

            string status = "";
            Lifelength remain = Lifelength.Null;
            Lifelength used = Lifelength.Null;

            //string remarks = reportedDirective.LastPerformance != null ? reportedDirective.LastPerformance.Remarks : reportedDirective.Remarks;
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
	        string firstPerformanceString =
		        reportedDirective.Threshold.FirstPerformanceSinceNew.ToString();

            if (reportedDirective.LastPerformance != null)
            {
                used.Add(_current);
                used.Substract(reportedDirective.LastPerformance.OnLifelength);
                if(!reportedDirective.Threshold.RepeatInterval.IsNullOrZero())
                    used.Resemble(reportedDirective.Threshold.RepeatInterval);
                else if (!reportedDirective.Threshold.FirstPerformanceSinceNew.IsNullOrZero())
                    used.Resemble(reportedDirective.Threshold.FirstPerformanceSinceNew);

                if (reportedDirective.NextPerformanceSource != null && !reportedDirective.NextPerformanceSource.IsNullOrZero())
                {
                    remain.Add(reportedDirective.NextPerformanceSource);
                    remain.Substract(_current);
                    remain.Resemble(reportedDirective.Threshold.RepeatInterval);
                }
            }


            var remainCalc = Lifelength.Zero;
            NextPerformance next = null;

            try
            {
	            if (_mpLimit)
	            {
		            if (reportedDirective.LastPerformance != null)
		            {
			            if (!reportedDirective.Threshold.RepeatInterval.IsNullOrZero() && !reportedDirective.IsClosed)
			            {
				            next = reportedDirective.NextPerformance;

				            next.PerformanceSource = Lifelength.Zero;
				            next.PerformanceSource.Add(reportedDirective.LastPerformance.OnLifelength);
				            next.PerformanceSource.Add(reportedDirective.Threshold.RepeatInterval);
				            next.PerformanceSource.Resemble(reportedDirective.Threshold.RepeatInterval);

				            if (reportedDirective.Threshold.RepeatInterval.Days.HasValue)
					            next.PerformanceDate =
						            reportedDirective.LastPerformance.RecordDate.AddDays(reportedDirective.Threshold
							            .RepeatInterval.Days.Value);
				            else next.PerformanceDate = null;

				            remainCalc.Add(next.PerformanceSource);
				            remainCalc.Substract(_current);
				            remainCalc.Resemble(reportedDirective.Threshold.RepeatInterval);

				            if (next.PerformanceDate != null)
					            remainCalc.Days = DateTimeExtend.DifferenceDateTime(DateTime.Today, next.PerformanceDate.Value).Days;
				           
			            }
		            }
		            else if(reportedDirective.NextPerformanceSource != null && !reportedDirective.NextPerformanceSource.IsNullOrZero())
		            {
			            if (!reportedDirective.Threshold.RepeatInterval.IsNullOrZero())
			            {
				            remainCalc.Add(reportedDirective.NextPerformanceSource);
				            remainCalc.Substract(_current);
				            remainCalc.Resemble(reportedDirective.Threshold.RepeatInterval);
			            }
		            }

	            }
	            else
	            {
		            remainCalc = reportedDirective.Remains;
		            next = reportedDirective.NextPerformance;
	            }

	            destinationDataSet.ItemsTable.AddItemsTableRow(reportedDirective.TaskCardNumber, reportedDirective.TaskNumberCheck, reportedDirective.Description,
		            firstPerformanceString,
		            reportedDirective.Threshold.RepeatInterval != null ? reportedDirective.Threshold.RepeatInterval.Hours?.ToString() : "*",
		            reportedDirective.Threshold.RepeatInterval != null ? reportedDirective.Threshold.RepeatInterval.Cycles?.ToString() : "*",
		            reportedDirective.Threshold.RepeatInterval != null ? reportedDirective.Threshold.RepeatInterval.Days?.ToString() : "*",
		            reportedDirective.LastPerformance != null ? reportedDirective.LastPerformance.OnLifelength.Hours?.ToString() : "*",
		            reportedDirective.LastPerformance != null ? reportedDirective.LastPerformance.OnLifelength.Cycles?.ToString() : "*",
		            reportedDirective.LastPerformance != null ? reportedDirective.LastPerformance.RecordDate.Date.ToString("dd.MM.yyyy") : "*",
		            next != null ? next.PerformanceSource.Hours.ToString() : "*",
		            next != null ? next.PerformanceSource.Cycles.ToString() : "*",
		            next?.PerformanceDate != null  ? next.PerformanceDate.Value.ToString("dd.MM.yyyy") : "*" ,
		            remainCalc != null ? remainCalc.Hours.ToString() : "*",
		            remainCalc != null ? remainCalc.Cycles.ToString() : "*",
		            remainCalc != null ? remainCalc.Days.ToString() : "*", "", ""
	            );
            }
            catch (Exception e)
            {
	            Console.WriteLine(e);
	            throw;
            }
        }

        #endregion

        #region private void AddAdditionalDataToDataSet(MaintenanceDirectivesDataSet destinationDateSet)

        /// <summary>
        /// ���������� �������������� ���������� 
        /// </summary>
        /// <param name="destinationDateSet"></param>
        private void AddAdditionalDataToDataSet(MaintenanceDirectivesDataSetLatAvia destinationDateSet)
        {
            string firsttitle = "MPD Item";
            string discriptiontitle = "Description";
            string secondtitle = "Task Card �";

            string reportFooter = new GlobalTermsProvider()["ReportFooter"].ToString();
            string reportFooterPrepared = new GlobalTermsProvider()["ReportFooterPrepared"].ToString();
            string reportFooterLink = new GlobalTermsProvider()["ProductWebsite"].ToString();
            destinationDateSet.AdditionalDataTAble.AddAdditionalDataTAbleRow(_reportTitle, _operatorLogotype, _filterSelection, DateAsOf, firsttitle, secondtitle, discriptiontitle, reportFooter, reportFooterPrepared, reportFooterLink);

        }

        #endregion

        #endregion

    }
}