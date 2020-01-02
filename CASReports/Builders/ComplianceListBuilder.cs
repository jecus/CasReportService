using CASReports.Datasets;
using CASReports.ReportTemplates;

namespace CASReports.Builders
{
    public class ComplianceListBuilder : AbstractReportBuilder
    {
        #region Fields

        private readonly object [] _directiveRecords;
        private string _dateAsOf = "";
        private string _reportName = "";

        #endregion

        #region Constructor

        public ComplianceListBuilder(object [] directiveRecords)
        {
            _directiveRecords = directiveRecords;    
        }

        #endregion

        #region Properties

        #region public string DateAsOf

        /// <summary>
        /// ���������� ��� ������������� ����, �� ������� ���������� ������
        /// </summary>
        public string DateAsOf
        {
            get
            {
                return _dateAsOf;
            }
            set
            {
                _dateAsOf = value;
            }
        }

        #endregion

        #region public string ReportName

        /// <summary>
        /// ���������� ��� ������������� �������� ������
        /// </summary>
        public string ReportName
        {
            get
            {
                return _reportName;
            }
            set
            {
                _reportName = value;
            }
        }

        #endregion

        #endregion

        #region Methods

        #region public override object GenerateReport()

        /// <summary>
        /// ������� ����� �� ������, ����������� � ������� �������� ������ (DataSet)
        /// </summary>
        /// <returns>����������� �����</returns>
        public override object GenerateReport()
        {
            ComplianceListReport report = new ComplianceListReport();
            report.SetDataSource(GenerateDataSet());
            return report;
        }

        #endregion

        #region public ComplianceListDataSet GenerateDataSet()

        /// <summary>
        /// ������� �������� ������ (DataSet) ��� ������ � �����
        /// </summary>
        /// <returns>�������� ������</returns>
        public ComplianceListDataSet GenerateDataSet()
        {
            var dataset = new ComplianceListDataSet();
            for (int i = 0; i < _directiveRecords.Length; i++)
            {
	            if (_directiveRecords[i] is TransferRecord)
	            {
		            var transferRecord = (TransferRecord)_directiveRecords[i];
					dataset.ComplianceList.AddComplianceListRow(i, transferRecord.TransferDate.ToString(new GlobalTermsProvider()["DateFormat"].ToString()),
																"Transfer Record",
																transferRecord.OnLifelength.ToString(),
																transferRecord.Remarks);
				}
	            if (_directiveRecords[i] is ActualStateRecord)
	            {
		            var actualStateRec = (ActualStateRecord)_directiveRecords[i];
					dataset.ComplianceList.AddComplianceListRow(i, actualStateRec.RecordDate.Date.ToString(new GlobalTermsProvider()["DateFormat"].ToString()),
																"Actual State Record",
																actualStateRec.OnLifelength.ToString(),
																actualStateRec.Remarks);
				}
	            if (_directiveRecords[i] is DirectiveRecord)
	            {
		            var directiveRecord = (DirectiveRecord)_directiveRecords[i];
					dataset.ComplianceList.AddComplianceListRow(i, directiveRecord.RecordDate.ToString(new GlobalTermsProvider()["DateFormat"].ToString()),
																directiveRecord.RecordType.ToString(),
																directiveRecord.OnLifelength.ToString(),
																directiveRecord.Remarks);
				}
            }
            dataset.AdditionalTable.AddAdditionalTableRow(0, _dateAsOf, _reportName, (string)new GlobalTermsProvider()["ReportFooterPrepared"], (string)new GlobalTermsProvider()["ReportFooterLink"]);
            

            return dataset;
        }

        #endregion

        #endregion
    }
}