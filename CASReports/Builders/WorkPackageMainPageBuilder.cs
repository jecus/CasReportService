using System;
using System.Collections.Generic;
using System.Linq;
using CASReports.Datasets;
using CASReports.Helpers;
using CASReports.Models;
using CASReports.ReportTemplates;

namespace CASReports.Builders
{
	/// <summary>
	/// ����������� ������ Release To Service 
	/// </summary>
	public class WorkPackageMainPageBuilder
	{

		#region Fields

		private WorkPackage _currentWorkPackage;
		private readonly bool _isScatReport;

		/// <summary>
		/// ��������� ���������� � �����
		/// </summary>
		private List<KeyValuePair<string, string>> Items { get; set; }

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

		#endregion

		#endregion

		#region Constructor
		/// <summary>
		/// ��������� ����������� ������ Release To Service 
		/// </summary>
		/// <param name="workPackage">������� �����</param>
		/// <param name="items"></param>
		public WorkPackageMainPageBuilder(WorkPackage workPackage, List<KeyValuePair<string, string>> items, bool isScatReport = false)
		{
			_currentWorkPackage = workPackage;
			_isScatReport = isScatReport;
			Items = items;
		}

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
				var report = new WPMainPagePerortScat();
				report.SetDataSource(GenerateDataSet());
				return report;
			}
			else
			{
				var report = new WPMainPagePerort();
				report.SetDataSource(GenerateDataSet());
				return report;
			}
			
		}

		#endregion

		#region private WorkPackageMainPageDataSet GenerateDataSet()

		private WorkPackageMainPageDataSet GenerateDataSet()
		{
			WorkPackageMainPageDataSet dataSet = new WorkPackageMainPageDataSet();
			AddReleaseToServiceInformationToDataSet(dataSet);
			AddItemsToDataSet(dataSet);
			return dataSet;
		}

		#endregion

		#region private void AddItemsToDataSet(WorkPackageMainPageDataSet dataset)

		/// <summary>
		/// ���������� �������� � ������� ������
		/// </summary>
		/// <param name="dataset">�������, � ������� ����������� ������</param>
		private void AddItemsToDataSet(WorkPackageMainPageDataSet dataset)
		{
			if (_isScatReport)
			{
				AddItemDataset(new KeyValuePair<string, string>("Certificate of Release to Service", "1"), dataset);
				AddItemDataset(new KeyValuePair<string, string>("Component Change Report ", ""), dataset);
				AddItemDataset(new KeyValuePair<string, string>("Certificates of Component and Materials ", ""), dataset);
				AddItemDataset(new KeyValuePair<string, string>("Work Summary Sheet", Items.FirstOrDefault(i => i.Key == "Summary Sheet").Value ?? ""), dataset);
				AddItemDataset(new KeyValuePair<string, string>("NRC ", ""), dataset);
				AddItemDataset(new KeyValuePair<string, string>("", ""), dataset);
				AddItemDataset(new KeyValuePair<string, string>("", ""), dataset);
				AddItemDataset(new KeyValuePair<string, string>("", ""), dataset);
				AddItemDataset(new KeyValuePair<string, string>("", ""), dataset);
				AddItemDataset(new KeyValuePair<string, string>("", ""), dataset);
				AddItemDataset(new KeyValuePair<string, string>("", ""), dataset);
				AddItemDataset(new KeyValuePair<string, string>("", ""), dataset);
				
			}
			else
			{
				foreach (KeyValuePair<string, string> keyValuePair in Items)
					AddItemDataset(keyValuePair, dataset);
			}
			
		}

		#endregion

		#region private void AddItemDataset(object reportedDirective, WorkPackageMainPageDataSet destinationDataSet)
		/// <summary>
		/// ����������� ������� � ������� ������
		/// </summary>
		/// <param name="keyValuePair">���������� ���������</param>
		/// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
		private void AddItemDataset(KeyValuePair<string, string> keyValuePair, WorkPackageMainPageDataSet destinationDataSet)
		{

			destinationDataSet.WPItemsTable.AddWPItemsTableRow(keyValuePair.Key, keyValuePair.Value);
		}

		#endregion

		#region private void AddReleaseToServiceInformationToDataSet(WorkPackageMainPageDataSet destinationDataSet)

		private void AddReleaseToServiceInformationToDataSet(WorkPackageMainPageDataSet destinationDataSet)
		{
			var termsProvider = GlobalTermsProvider.Terms;
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
			var operatorLogotype = op.LogotypeReportLarge;
			var operatorName = op.Name;
			var operatorAddress = op.Address;
			var workPerformedStartDate = _currentWorkPackage.OpeningDate.ToString(termsProvider["DateFormat"].ToString());
			if (_currentWorkPackage.Status == WorkPackageStatus.Published || _currentWorkPackage.Status == WorkPackageStatus.Closed)
				workPerformedStartDate = _currentWorkPackage.PublishingDate.ToString(termsProvider["DateFormat"].ToString());
			var workPerformedEndDate = "";
			if (_currentWorkPackage.Status == WorkPackageStatus.Closed)
				workPerformedEndDate = _currentWorkPackage.ClosingDate.ToString(termsProvider["DateFormat"].ToString());

			var workPerformedStation = "";
			var task = _currentWorkPackage.WorkPakageRecords.FirstOrDefault(i => i.Task is MaintenanceDirective)?.Task;
			if (task != null)
			{
				var mpd = task as MaintenanceDirective;
				workPerformedStation =
					$"{mpd.ScheduleRef} R{mpd.ScheduleRevisionNum} {mpd.ScheduleRevisionDate:dd.MM.yyyy}";
			}


			
			var workPerformedWorkOrderNo = _currentWorkPackage.Number;
			var wpTitle= _currentWorkPackage.Title;
			destinationDataSet.MainDataTable.AddMainDataTableRow(airportName,
																 manufacturer,
																 registrationMark, model, serialNumber,
																 totalCycles, totalFlightHours,
																 operatorLogotype,
																 operatorName, operatorAddress,
																 workPerformedStartDate,
																 workPerformedEndDate,
																 workPerformedStation,
																 workPerformedWorkOrderNo,
																 wpTitle);
		}

		#endregion

		#endregion
	}
}
