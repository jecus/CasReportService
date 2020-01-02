﻿using System;

using System.Data;
using CASReports.Datasets;
using CASReports.Models;
using CASReports.ReportTemplates;

namespace CASReports.Builders
{
	public class ServisibleTagReportBuilder : AbstractReportBuilder
	{
		private Operator _reportedOperator;
		private  Component _component;

		#region Constructor

		public ServisibleTagReportBuilder(Operator @operator, Component component)
		{
			_reportedOperator = @operator;
			_component = component;
		}

		#endregion

		#region public override object GenerateReport()

		public override object GenerateReport()
		{
			var report = new ServisibleTagReport();
			report.SetDataSource(GenerateDataSet());
			return report;
		}

		#endregion

		#region protected virtual DataSet GenerateDataSet()

		protected virtual DataSet GenerateDataSet()
		{
			var dataset = new ServisibleDataSet();
			AddServisibleComponentDataToDataSet(dataset);
			return dataset;
		}

		#endregion

		private void AddServisibleComponentDataToDataSet(ServisibleDataSet dataset)
		{
			var transferRecord = _component.TransferRecords.GetLast();
			var owner = _reportedOperator.ToString();
			var aircraftString = "N/A";
			if (transferRecord != null && transferRecord.FromAircraftId > 0)
			{
				var aircraft = GlobalObjects.AircraftsCore.GetAircraftById(transferRecord.FromAircraftId);
				if (aircraft != null)
				{
					owner = aircraft.Owner;
					aircraftString = aircraft.ToString();
				}
			}
				

			var description = _component.Description;
			var serialNumber = string.IsNullOrEmpty(_component.SerialNumber) ? "N/A" : _component.SerialNumber;
			var partNumber = string.IsNullOrEmpty(_component.PartNumber) ? "N/A" : _component.PartNumber;
			var batchNumber = string.IsNullOrEmpty(_component.BatchNumber) ? "N/A" : _component.BatchNumber;
			var status = _component.ComponentStatus.ToString();
			var idNumber = _component.IdNumber;


			dataset.ServisibleDataTable.AddServisibleDataTableRow(owner, aircraftString, description, serialNumber, partNumber, 
				batchNumber, status, _component.QuantityIn.ToString(), DateTime.Today.ToString("dd MM yyyy"), _reportedOperator.Name , "", idNumber,
				"", "", "", "");
		}
	}
}