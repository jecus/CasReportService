using LTR.Core;
using LTR.Core.Types.Aircrafts.Parts;
using LTR.Core.Types.Directives;
using LTR.Core.Types.ReportFilters;
using LTRReports.Datasets;

namespace LTRReports
{
    /// <summary>
    /// ���������� ������ SB status
    /// </summary>
    public class SBReportBuilder:DirectiveListReportBuilder
    {
        SBStatusFilter defaultFilter = new SBStatusFilter();
        #region Constructors

        #region public SBReportBuilder()

        /// <summary>
        /// ��������� ������ ��� �������� �������. ���������� ������
        /// </summary>
        public SBReportBuilder()
        {
        }

        #endregion

        #region public SBReportBuilder(BaseDetailDirective[] directives) : base(directives)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ��������
        /// </summary>
        /// <param name="directives">�������� ��� ������� ��������� �����</param>
        public SBReportBuilder(BaseDetailDirective[] directives) : base(directives)
        {
        }

        #endregion

        #region public SBReportBuilder(Operator reportedOperator) : base(reportedOperator)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� ���������, ��� �� � ������� ��������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedOperator">������� ��� �������� ��������� �����</param>
        public SBReportBuilder(Operator reportedOperator) : base(reportedOperator)
        {
        }

        #endregion

        #region public SBReportBuilder(Operator[] reportedOperators) : base(reportedOperators)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ����������, �� �� ��� ������� ��������� � ���� ��������
        /// </summary>
        /// <param name="reportedOperators">�������� ��� ������� ��������� �����</param>
        public SBReportBuilder(Operator[] reportedOperators) : base(reportedOperators)
        {
        }

        #endregion

        #region public SBReportBuilder(Aircraft[] reportedAircrafts) : base(reportedAircrafts)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ���������, �� ������� ��������� � ��������, ������������ � ���
        /// </summary>
        /// <param name="reportedAircrafts">�������� ��� ������� ��������� �����</param>
        public SBReportBuilder(Aircraft[] reportedAircrafts) : base(reportedAircrafts)
        {
        }

        #endregion

        #region public SBReportBuilder(Aircraft reportedAircraft) : base(reportedAircraft)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� ��, ��� ������� ��������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedAircraft">������� ��� �������� ��������� �����</param>
        public SBReportBuilder(Aircraft reportedAircraft) : base(reportedAircraft)
        {
        }

        #endregion

        #region public SBReportBuilder(BaseDetail[] reportedBaseDetails) : base(reportedBaseDetails)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ������� ��������� � ���� ��������
        /// </summary>
        /// <param name="reportedBaseDetails">�������� ��� ������� ��������� �����</param>
        public SBReportBuilder(BaseDetail[] reportedBaseDetails) : base(reportedBaseDetails)
        {
        }

        #endregion

        #region public SBReportBuilder(BaseDetail reportedBaseDetail) : base(reportedBaseDetail)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� �������� �������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedBaseDetail">������� ��� �������� ��������� �����</param>
        public SBReportBuilder(BaseDetail reportedBaseDetail) : base(reportedBaseDetail)
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
                return "SB Status Compliance List";
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
