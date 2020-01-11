using System.Collections.Generic;
using CASReports.Datasets;
using CASReports.Models;
using CASReports.ReportTemplates;

namespace CASReports.Builders
{
	/// <summary>
	/// ����������� ������ Release To Service 
	/// </summary>
	public class WorkPackageSummarySheetBuilder
	{

		#region Fields

		private readonly WorkPackage _currentWorkPackage;
		/// <summary>
		/// ��������� ���������� � �����
		/// </summary>
		private List<string[]> Items { get; set; }

		#endregion

		#region Properties

		#endregion

		#region Constructor
		/// <summary>
		/// ��������� ����������� ������ Release To Service 
		/// </summary>
		/// <param name="workPackage">������� �����</param>
		/// <param name="items"></param>
		public WorkPackageSummarySheetBuilder(WorkPackage workPackage, List<string[]> items)
		{
			_currentWorkPackage = workPackage;
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
			WorkPackageSummarySheetReport report = new WorkPackageSummarySheetReport();
			report.SetDataSource(GenerateDataSet());
			return report;
		}

		#endregion

		#region private MaintenanceJobCardDataSet GenerateDataSet()

		private WorkPackageSummarySheetDataSet GenerateDataSet()
		{
			WorkPackageSummarySheetDataSet dataSet = new WorkPackageSummarySheetDataSet();
			AddReleaseToServiceInformationToDataSet(dataSet);
			AddItemsToDataSet(dataSet);
			return dataSet;
		}

		#endregion

		#region private void AddItemsToDataSet(WorkPackageSummarySheetDataSet dataset)

		/// <summary>
		/// ���������� �������� � ������� ������
		/// </summary>
		/// <param name="dataset">�������, � ������� ����������� ������</param>
		private void AddItemsToDataSet(WorkPackageSummarySheetDataSet dataset)
		{
			//int count = 0;
			foreach (string[] itemData in Items)
			{
				//itemData[0] = count.ToString();
				AddItemDataset(itemData, dataset);
			}
		}

		#endregion

		#region private void AddItemDataset(string[]itemData, WorkPackageSummarySheetDataSet destinationDataSet)
		/// <summary>
		/// ����������� ������� � ������� ������
		/// </summary>
		/// <param name="itemData">���������� ���������</param>
		/// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
		private void AddItemDataset(string[]itemData, WorkPackageSummarySheetDataSet destinationDataSet)
		{
			destinationDataSet.ItemsTable.AddItemsTableRow(itemData[0],itemData[1],itemData[2],
														   itemData[3],itemData[4],itemData[5], "", "");
		}

		#endregion

		#region private void AddReleaseToServiceInformationToDataSet(WorkPackageSummarySheetDataSet destinationDataSet)

		private void AddReleaseToServiceInformationToDataSet(WorkPackageSummarySheetDataSet destinationDataSet)
		{
			var aircraft = _currentWorkPackage.Aircraft;
			var totalFlight = GlobalObjects.CasEnvironment.Calculator.GetCurrentFlightLifelength(aircraft);
			var manufacturer = GlobalObjects.ComponentCore.GetBaseComponentById(aircraft.AircraftFrameId).Manufacturer;
			var registrationMark = aircraft.RegistrationNumber;
			var model = aircraft.Model.ToString();
			var serialNumber = aircraft.SerialNumber;
			var totalCycles = totalFlight.Cycles.ToString();
			var totalFlightHours = totalFlight.Hours.ToString();
			var op = GlobalObjects.CasEnvironment.Operators.First(o => o.ItemId == aircraft.OperatorId);
			var operatorLogotype = op.LogotypeReportLarge;
			var operatorName = op.Name;
			var operatorAddress = op.Address;
			var workPerformedStation = _currentWorkPackage.Station;
			var workPerformedWorkOrderNo = _currentWorkPackage.Number;
			destinationDataSet.HeaderTable.AddHeaderTableRow(manufacturer,
																 registrationMark, model, serialNumber,
																 totalCycles, totalFlightHours,
																 operatorLogotype,
																 operatorName, operatorAddress,
																 workPerformedStation,
																 workPerformedWorkOrderNo, "", _currentWorkPackage.Title);
		}

		#endregion

		#endregion
	}
}
