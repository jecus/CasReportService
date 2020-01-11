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
	public class WorkPackageReleaseToServiceBuilder : AbstractReportBuilder
	{

		#region Fields

		private readonly WorkPackage _currentWorkPackage;

		/// <summary>
		/// ��������� ���������� � �����
		/// </summary>
		private List<KeyValuePair<string, int>> Items { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// ��������� ����������� ������ Release To Service 
		/// </summary>
		/// <param name="workPackage">������� �����</param>
		/// <param name="items"></param>
		public WorkPackageReleaseToServiceBuilder(WorkPackage workPackage, List<KeyValuePair<string, int>> items)
		{
			_currentWorkPackage = workPackage;
			Items = items;
		}

		#endregion

		#region Properties
		
		#endregion

		#region Methods

		#region public override object GenerateReport()

		/// <summary>
		/// �������������� ����� �� ������, ����������� � ������� ������
		/// </summary>
		/// <returns>����������� �����</returns>
		public override object GenerateReport()
		{
			WorkPackageReleaseToServiceReport report = new WorkPackageReleaseToServiceReport();
			report.SetDataSource(GenerateDataSet());
			return report;
		}

		#endregion

		#region private MaintenanceJobCardDataSet GenerateDataSet()

		private WorkPackageReleaseToServiceDataSet GenerateDataSet()
		{
			WorkPackageReleaseToServiceDataSet dataSet = new WorkPackageReleaseToServiceDataSet();
			AddReleaseToServiceInformationToDataSet(dataSet);
			AddItemsToDataSet(dataSet);
			return dataSet;
		}

		#endregion

		#region private void AddItemsToDataSet(WorkPackageReleaseToServiceDataSet dataset)

		/// <summary>
		/// ���������� �������� � ������� ������
		/// </summary>
		/// <param name="dataset">�������, � ������� ����������� ������</param>
		private void AddItemsToDataSet(WorkPackageReleaseToServiceDataSet dataset)
		{
			foreach (KeyValuePair<string, int> keyValuePair in Items)
			{
				AddItemDataset(keyValuePair, dataset);
			}
		}

		#endregion

		#region private void AddItemDataset(object reportedDirective, WorkPackageReleaseToServiceDataSet destinationDataSet)
		/// <summary>
		/// ����������� ������� � ������� ������
		/// </summary>
		/// <param name="keyValuePair">���������� ���������</param>
		/// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
		private void AddItemDataset(KeyValuePair<string, int> keyValuePair, WorkPackageReleaseToServiceDataSet destinationDataSet)
		{
			destinationDataSet.WPItemsTable.AddWPItemsTableRow(keyValuePair.Key, keyValuePair.Value);
		}

		#endregion

		#region private void AddReleaseToServiceInformationToDataSet(WorkPackageReleaseToServiceDataSet destinationDataSet)

		private void AddReleaseToServiceInformationToDataSet(WorkPackageReleaseToServiceDataSet destinationDataSet)
		{
			var termsProvider = GlobalTermsProvider.Terms;
			var aircraft = _currentWorkPackage.Aircraft;
			var totalFlight = GlobalObjects.CasEnvironment.Calculator.GetCurrentFlightLifelength(aircraft);
			var op = GlobalObjects.CasEnvironment.Operators.First(o => o.ItemId == aircraft.ItemId);
			var airportName = op.Name + Environment.NewLine + "The Seychelles National Airport";
			var maintenanceReleaseCertificate = _currentWorkPackage.ReleaseCertificateNo;
			var nationality = "The Seychelles";
			var manufacturer = GlobalObjects.ComponentCore.GetBaseComponentById(aircraft.AircraftFrameId).Manufacturer;
			var registrationMark = aircraft.RegistrationNumber;
			var model = aircraft.Model.ToString();
			var serialNumber = aircraft.SerialNumber;
			var totalCycles = totalFlight.Cycles.ToString();
			var totalFlightHours = totalFlight.Hours.ToString();
			var operatorLogotype = op.LogotypeReportLarge;
			var operatorName = op.Name;
			var operatorAddress = op.Address;
			var pagesCount = Items != null ? Items.Sum(x => x.Value) : 0;
			
			var engines = new List<BaseComponent>(GlobalObjects.ComponentCore.GetAircraftEngines(aircraft.ItemId));
			var engine1Serial = engines.Count > 0 ? engines[0].SerialNumber : "";
			var engine2Serial = engines.Count > 1 ? engines[1].SerialNumber : "";

			var apu = GlobalObjects.ComponentCore.GetAircraftApu(aircraft.ItemId);
			var apuSerial = apu != null ? apu.SerialNumber : "";

			var workPerformedCheckType = _currentWorkPackage.CheckType;
			var workPerformedStartDate = "";
			if (_currentWorkPackage.Status == WorkPackageStatus.Published || _currentWorkPackage.Status == WorkPackageStatus.Closed)
				workPerformedStartDate = _currentWorkPackage.PublishingDate.ToString(termsProvider["DateFormat"].ToString());
			var workPerformedEndDate = "";
			if (_currentWorkPackage.Status == WorkPackageStatus.Closed)
				workPerformedEndDate = _currentWorkPackage.ClosingDate.ToString(termsProvider["DateFormat"].ToString());
			var workPerformedStation = _currentWorkPackage.Station;
			var workPerformedWorkOrderNo = _currentWorkPackage.Number;
			var wpTitle = _currentWorkPackage.Title;
			var workPerformedMaintenanceReportNo = _currentWorkPackage.MaintenanceRepairOrzanization;
			var remarks = _currentWorkPackage.Remarks;
			var additionalRemarks = termsProvider["CAARequirements"].ToString();
			var catchword = termsProvider["Revision"].ToString();
			var crsNumber = _currentWorkPackage.ReleaseCertificateNo;
			var revision = _currentWorkPackage.Revision;
			destinationDataSet.ReleaseToServiceTable.AddReleaseToServiceTableRow(airportName,
																				 maintenanceReleaseCertificate,
																				 nationality, manufacturer,
																				 registrationMark, model, serialNumber,
																				 totalCycles, totalFlightHours,
																				 operatorLogotype,
																				 operatorName, operatorAddress,
																				 workPerformedCheckType,
																				 workPerformedStartDate,
																				 workPerformedEndDate,
																				 workPerformedStation,
																				 workPerformedWorkOrderNo,
																				 wpTitle,
																				 workPerformedMaintenanceReportNo,
																				 remarks, additionalRemarks, catchword,
																				 crsNumber, engine1Serial, engine2Serial,
																				 apuSerial, revision, pagesCount);
		}

		#endregion

		#endregion
	}
}
