using System.Collections.Generic;
using CASReports.Datasets;
using CASReports.Models;
using CASReports.ReportTemplates;

namespace CASReports.Builders
{
	/// <summary>
	/// ����������� ������ Release To Service 
	/// </summary>
	public class InitialOrderReportBuilder : AbstractReportBuilder
	{

		#region Fields

		private Operator _operator;
		private readonly List<InitialOrderRecord> _orderRecords;
		private readonly InitialOrder _order;

		#endregion

		#region Properties

		#region public Operator Operator
		public Operator Operator
		{
			set { _operator = value; }
		}
		#endregion

		public byte[] AuthorSign { get; set; }
		public byte[] PublishSign { get; set; }

		#endregion

		#region Constructor
		/// <summary>
		/// ��������� ������ �����������
		/// </summary>
		public InitialOrderReportBuilder()
		{
		}

		/// <summary>
		/// ��������� ����������� ������ Release To Service 
		/// </summary>
		/// <param name="op"></param>
		/// <param name="items"></param>
		public InitialOrderReportBuilder(Operator op, List<InitialOrderRecord> orderRecords, InitialOrder order)
		{
			_operator = op;
			_orderRecords = orderRecords;
			_order = order;
		}

		#endregion

		#region Methods

		#region public override object GenerateReport()

		/// <summary>
		/// �������������� ����� �� ������, ����������� � ������� ������
		/// </summary>
		/// <returns>����������� �����</returns>
		public override object GenerateReport()
		{
			var report = new InitialOrderReport();
			report.SetDataSource(GenerateDataSet());
			return report;
		}

		#endregion

		#region private SupplierListDataSet GenerateDataSet()

		private InitialRecordDataSet GenerateDataSet()
		{
			var dataSet = new InitialRecordDataSet();
			AddOperatorInformationToDataSet(dataSet);
			AddInitialOrderToDataSet(dataSet);
			AddInitialOrderRecordsToDataSet(dataSet);
			return dataSet;
		}

		private void AddInitialOrderRecordsToDataSet(InitialRecordDataSet dataSet)
		{
			int i = 1;
			

			foreach (var record in _orderRecords)
			{
				var destination = record.DestinationObject is Aircraft
					? ((Aircraft) record.DestinationObject).ToString()
					: "";
				var model = record.DestinationObject is Aircraft
					? ((Aircraft)record.DestinationObject)?.Model?.ShortName
					: "";

				dataSet.InitialOrderRecord.AddInitialOrderRecordRow(i.ToString(), record.AirportCode?.ToString(),
					model, destination, record.AccessoryDescription, record.Product.PartNumber, record.Product.GoodsClass.ToString(),
					record.Quantity.ToString("F1"), record.Priority.ToString(), record.Reference,
					record.Remarks);

				i++;
			}
		}

		private void AddInitialOrderToDataSet(InitialRecordDataSet dataSet)
		{
			dataSet.InitialOrder.AddInitialOrderRow(_order.Number,
				_order.Author,
				SmartCore.Auxiliary.Convert.GetDateFormat(_order.OpeningDate),
				SmartCore.Auxiliary.Convert.GetDateFormat(_order.PublishingDate),
				_order.PublishedByUser, _order.Remarks);
		}

		#endregion

		#region private void AddOperatorInformationToDataSet(SupplierListDataSet destinationDataSet)

		private void AddOperatorInformationToDataSet(InitialRecordDataSet destinationDataSet)
		{
			byte[] operatorLogotype = _operator.LogotypeReportVeryLarge;
			string operatorName = _operator.Name;
			string operatorAddress = _operator.Address;
			destinationDataSet.HeaderTable.AddHeaderTableRow(operatorLogotype, operatorName, operatorAddress, AuthorSign, PublishSign);
		}

		#endregion

		#endregion
	}
}
