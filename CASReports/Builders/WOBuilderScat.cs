using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CASReports.Datasets;
using CASReports.ReportTemplates;

namespace CASReports.Builders
{
    /// <summary>
    /// ����������� ������ Release To Service 
    /// </summary>
    public class WOBuilderScat : AbstractReportBuilder
	{

        #region Fields

        private WorkPackage _currentWorkPackage;
        private readonly int _count;
        private readonly List<string[]> _summarySheetItems;

        private readonly bool _isScatReport;

		#endregion

		#region Properties

		#region public WorkPackage WorkPackage
		/// <summary>
		/// ���������� ������� �����
		/// </summary>
		public WorkPackage WorkPackage
        {
            set { _currentWorkPackage = value; }
        }

		public ICommonCollection<BaseEntityObject> Items { get; set; }

		#endregion

        #endregion

        #region Constructor

        /// <summary>
        /// ��������� ����������� ������ Release To Service 
        /// </summary>
        /// <param name="workPackage">������� �����</param>
        /// <param name="count"></param>
        /// <param name="summarySheetItems"></param>
        /// <param name="items"></param>
        public WOBuilderScat(WorkPackage workPackage, int count, List<string[]> summarySheetItems)
        {
	        _currentWorkPackage = workPackage;
	        _count = count;
	        _summarySheetItems = summarySheetItems;
        }

		#endregion

		#region Methods

		#region public object GenerateReport()

		/// <summary>
		/// �������������� ����� �� ������, ����������� � ������� ������
		/// </summary>
		/// <returns>����������� �����</returns>
		public override object GenerateReport()
		{
			var report = new WOScat();
			report.SetDataSource(GenerateDataSet());
			return report;
		}

		#endregion

		#region private WorkPackageTitlePageDataSet GenerateDataSet()

		private WorkPackageTitlePageDataSet GenerateDataSet()
		{
			var dataSet = new WorkPackageTitlePageDataSet();
			AddReleaseToServiceInformationToDataSet(dataSet);
			AddItemsToDataSet(dataSet);
			return dataSet;
		}

		#endregion

		private void AddItemsToDataSet(WorkPackageTitlePageDataSet destinationDataSet)
		{
			int count = 1;

			foreach (var item in _summarySheetItems)
			{
				destinationDataSet.WPItemsTable.AddWPItemsTableRow(item[0],
					0, item[1], count.ToString(), item[4]);
				count++;
			}
		}


		#region private void AddReleaseToServiceInformationToDataSet(WorkPackageTitlePageDataSet destinationDataSet)

		private void AddReleaseToServiceInformationToDataSet(WorkPackageTitlePageDataSet destinationDataSet)
		{
			var termsProvider = new GlobalTermsProvider();
			var aircraft = _currentWorkPackage.Aircraft;
			var totalFlight = GlobalObjects.CasEnvironment.Calculator.GetCurrentFlightLifelength(aircraft);
			var op = GlobalObjects.CasEnvironment.Operators.First(o => o.ItemId == aircraft.OperatorId);
			var airportName = op.Name + Environment.NewLine + "The Seychelles National Airport";
			var manufacturer = GlobalObjects.ComponentCore.GetBaseComponentById(aircraft.AircraftFrameId).Manufacturer;
			var registrationMark = aircraft.RegistrationNumber;
			var model = aircraft.Model.ToString();
			var serialNumber = aircraft.SerialNumber;
			var totalCycles = totalFlight.Cycles.ToString();
			var totalFlightHours = totalFlight.Hours.ToString();
			var operatorLogotype = op.LogotypeReportVeryLarge;
			var operatorName = op.Name;
			var operatorAddress = op.Address;
			var workPerformedStartDate = "";
			if (_currentWorkPackage.Status == WorkPackageStatus.Published || _currentWorkPackage.Status == WorkPackageStatus.Closed)
				workPerformedStartDate = _currentWorkPackage.PublishingDate.ToString(termsProvider["DateFormat"].ToString());
			var workPerformedEndDate = "";
			if (_currentWorkPackage.Status == WorkPackageStatus.Closed)
				workPerformedEndDate = _currentWorkPackage.ClosingDate.ToString(termsProvider["DateFormat"].ToString());
			var workPerformedStation = _currentWorkPackage.Station;
			var workPerformedWorkOrderNo = _currentWorkPackage.Number;
			var wpTitle = _currentWorkPackage.Title;
			var wpCreatedBy = _currentWorkPackage.Author;
			var wpPublishedBy = _currentWorkPackage.PublishedBy;
			var accomplich = _count.ToString();
			var createDate = _currentWorkPackage.CreateDate;


			destinationDataSet.MainDataTable.AddMainDataTableRow(GlobalObjects.ComponentCore.GetBaseComponentById(aircraft.AircraftFrameId).SerialNumber,
																 manufacturer,
																 registrationMark, model, serialNumber,
																 totalCycles, totalFlightHours,
																 operatorLogotype,
																 operatorName, operatorAddress,
																 workPerformedStartDate,
																 workPerformedEndDate,
																 workPerformedStation,
																 workPerformedWorkOrderNo,
																 wpTitle, wpCreatedBy, wpPublishedBy, accomplich, createDate);
		}

		#endregion

		private string GetAccomplich(IEnumerable<WorkPackageRecord> workPakageRecords)
		{
			var groups = new List<string>();

			foreach (var workPackageRecord in workPakageRecords)
			{
				IBaseEntityObject parent = workPackageRecord.Task;

				if (parent is Directive)
				{
					var directive = (Directive) parent;
					if (directive is DeferredItem)
						groups.Add("Deffred");
					else if (directive is DamageItem)
						groups.Add("Damage");
					else if (directive.DirectiveType == DirectiveType.OutOfPhase)
						groups.Add("Out of phase");
					else
					{
						if (directive.DirectiveType.ItemId == DirectiveType.AirworthenessDirectives.ItemId)
							groups.Add("AD");
						else if (directive.DirectiveType.ItemId == DirectiveType.EngineeringOrders.ItemId)
							groups.Add("EO");
						else if (directive.DirectiveType.ItemId == DirectiveType.SB.ItemId)
							groups.Add("SB");
					}
				}
				else if (parent is BaseComponent)
					groups.Add("Base Component");
				else if (parent is Component)
					groups.Add("Component");
				else if (parent is ComponentDirective)
					groups.Add("Component directive");
				else if (parent is MaintenanceCheck)
					groups.Add("MC");
				else if (parent is MaintenanceDirective)
					groups.Add("MPD");
				else if (parent is NonRoutineJob)
					groups.Add("NRJ");
			}
			return string.Join("+" , groups.Distinct().ToArray());
		}


		#endregion
	}
}
