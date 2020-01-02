using System;
using System.Collections.Generic;
using System.Linq;
using CASReports.Datasets;
using CASReports.ReportTemplates;
using CASTerms;
using SmartCore.Calculations;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General;
using SmartCore.Entities.General.WorkPackage;

namespace CASReports.Builders
{
    /// <summary>
    /// ����������� ������ Release To Service 
    /// </summary>
    public class WorkPackageMainPageBuilder
    {

        #region Fields

        private WorkPackage _currentWorkPackage;
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
        public WorkPackageMainPageBuilder(WorkPackage workPackage, List<KeyValuePair<string, string>> items)
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
            WPMainPagePerort report = new WPMainPagePerort();
            report.SetDataSource(GenerateDataSet());
            return report;
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
            foreach (KeyValuePair<string, string> keyValuePair in Items)
            {
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
            var operatorLogotype = op.LogotypeReportLarge;
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
