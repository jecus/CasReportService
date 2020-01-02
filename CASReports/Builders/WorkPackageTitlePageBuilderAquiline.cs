using System;
using System.Collections.Generic;
using System.Linq;
using CASReports.Datasets;
using CASReports.ReportTemplates;
using CASTerms;
using SmartCore.Calculations;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General;
using SmartCore.Entities.General.Accessory;
using SmartCore.Entities.General.WorkPackage;

namespace CASReports.Builders
{
    /// <summary>
    /// ����������� ������ Release To Service 
    /// </summary>
    public class WorkPackageTitlePageBuilderAquiline
	{

        #region Fields

        private WorkPackage _currentWorkPackage;

        /// <summary>
        /// ��������� ���������� � �����
        /// </summary>
        private readonly List<KeyValuePair<string, int>> _items;

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
        /// <param name="aircraftBaseDetails"></param>
        public WorkPackageTitlePageBuilderAquiline(WorkPackage workPackage, 
                                           List<KeyValuePair<string, int>> items)
        {
            _currentWorkPackage = workPackage;
            _items = items;
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
            var report = new WPTitlePageReportAquiline();
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

        #region private void AddItemsToDataSet(WorkPackageTitlePageDataSet dataset)

        /// <summary>
        /// ���������� �������� � ������� ������
        /// </summary>
        /// <param name="dataset">�������, � ������� ����������� ������</param>
        private void AddItemsToDataSet(WorkPackageTitlePageDataSet dataset)
        {
            _items.Add(new KeyValuePair<string, int>("Total:", _items.Sum(x => x.Value)));
            foreach (KeyValuePair<string, int> keyValuePair in _items)
            {
                AddItemDataset(keyValuePair, dataset);
            }
        }

        #endregion

        #region private void AddItemDataset(object reportedDirective, WorkPackageTitlePageDataSet destinationDataSet)
        /// <summary>
        /// ����������� ������� � ������� ������
        /// </summary>
        /// <param name="keyValuePair">���������� ���������</param>
        /// <param name="destinationDataSet">�������, � ������� ����������� �������</param>
        private void AddItemDataset(KeyValuePair<string, int> keyValuePair, WorkPackageTitlePageDataSet destinationDataSet)
        {
            destinationDataSet.WPItemsTable.AddWPItemsTableRow(keyValuePair.Key, keyValuePair.Value);
        }

        #endregion

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
