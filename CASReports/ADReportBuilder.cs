using LTR.Core;
using LTR.Core.Types.Aircrafts.Parts;
using LTR.Core.Types.Directives;
using LTR.Core.Types.ReportFilters;

namespace LTRReports
{
    /// <summary>
    /// ���������� ������ AD Status
    /// </summary>
    public class ADReportBuilder:DirectiveListReportBuilder
    {
        private readonly ADStatusFilter defaultFilter = new ADStatusFilter();

        #region Constructors

        #region public ADReportBuilder()

        /// <summary>
        /// ��������� ������ ��� �������� �������. ���������� ������
        /// </summary>
        public ADReportBuilder()
        {
        }

        #endregion

        #region public ADReportBuilder(BaseDetailDirective[] directives) : base(directives)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ��������
        /// </summary>
        /// <param name="directives">�������� ��� ������� ��������� �����</param>
        public ADReportBuilder(BaseDetailDirective[] directives) : base(directives)
        {
        }

        #endregion

        #region public ADReportBuilder(Operator reportedOperator) : base(reportedOperator)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� ���������, ��� �� � ������� ��������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedOperator">������� ��� �������� ��������� �����</param>
        public ADReportBuilder(Operator reportedOperator) : base(reportedOperator)
        {
        }

        #endregion

        #region public ADReportBuilder(Operator[] reportedOperators) : base(reportedOperators)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ����������, �� �� ��� ������� ��������� � ���� ��������
        /// </summary>
        /// <param name="reportedOperators">�������� ��� ������� ��������� �����</param>
        public ADReportBuilder(Operator[] reportedOperators) : base(reportedOperators)
        {
        }

        #endregion

        #region public ADReportBuilder(Aircraft[] reportedAircrafts) : base(reportedAircrafts)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ���������, �� ������� ��������� � ��������, ������������ � ���
        /// </summary>
        /// <param name="reportedAircrafts">�������� ��� ������� ��������� �����</param>
        public ADReportBuilder(Aircraft[] reportedAircrafts) : base(reportedAircrafts)
        {
        }

        #endregion

        #region public ADReportBuilder(Aircraft reportedAircraft) : base(reportedAircraft)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� ��, ��� ������� ��������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedAircraft">������� ��� �������� ��������� �����</param>
        public ADReportBuilder(Aircraft reportedAircraft) : base(reportedAircraft)
        {
        }

        #endregion

        #region public ADReportBuilder(BaseDetail[] reportedBaseDetails) : base(reportedBaseDetails)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ������� ��������� � ���� ��������
        /// </summary>
        /// <param name="reportedBaseDetails">�������� ��� ������� ��������� �����</param>
        public ADReportBuilder(BaseDetail[] reportedBaseDetails) : base(reportedBaseDetails)
        {
        }

        #endregion

        #region public ADReportBuilder(BaseDetail reportedBaseDetail) : base(reportedBaseDetail)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� �������� �������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedBaseDetail">������� ��� �������� ��������� �����</param>
        public ADReportBuilder(BaseDetail reportedBaseDetail) : base(reportedBaseDetail)
        {
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public override string ReportTitle
        {
            get
            {
                return "Airwortheness Directives Compliance List";
            }
            set
            {
            }
        }

        /// <summary>
        /// ������ �� ���������
        /// </summary>
        public override DirectiveFilter DefaultFilter
        {
            get
            {
                return defaultFilter;
            }
        }
        #endregion
    }
}
