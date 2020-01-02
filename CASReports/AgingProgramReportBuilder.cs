using LTR.Core;
using LTR.Core.Types.Aircrafts.Parts;
using LTR.Core.Types.Directives;
using LTR.Core.Types.ReportFilters;

namespace LTRReports
{
    /// <summary>
    /// ���������� ������ AD Status
    /// </summary>
    public class AgingProgramReportBuilder : DirectiveListReportBuilder
    {
        private AgingProgramFilter defaultFilter = new AgingProgramFilter();

        #region Constructors

        #region public AgingProgramReportBuilder()

        /// <summary>
        /// ��������� ������ ��� �������� �������. ���������� ������
        /// </summary>
        public AgingProgramReportBuilder()
        {
        }

        #endregion

        #region public AgingProgramReportBuilder(BaseDetailDirective[] directives) : base(directives)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ��������
        /// </summary>
        /// <param name="directives">�������� ��� ������� ��������� �����</param>
        public AgingProgramReportBuilder(BaseDetailDirective[] directives)
            : base(directives)
        {
        }

        #endregion

        #region public AgingProgramReportBuilder(Operator reportedOperator) : base(reportedOperator)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� ���������, ��� �� � ������� ��������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedOperator">������� ��� �������� ��������� �����</param>
        public AgingProgramReportBuilder(Operator reportedOperator)
            : base(reportedOperator)
        {
        }

        #endregion

        #region public AgingProgramReportBuilder(Operator[] reportedOperators) : base(reportedOperators)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ����������, �� �� ��� ������� ��������� � ���� ��������
        /// </summary>
        /// <param name="reportedOperators">�������� ��� ������� ��������� �����</param>
        public AgingProgramReportBuilder(Operator[] reportedOperators)
            : base(reportedOperators)
        {
        }

        #endregion

        #region public AgingProgramReportBuilder(Aircraft[] reportedAircrafts) : base(reportedAircrafts)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ���������, �� ������� ��������� � ��������, ������������ � ���
        /// </summary>
        /// <param name="reportedAircrafts">�������� ��� ������� ��������� �����</param>
        public AgingProgramReportBuilder(Aircraft[] reportedAircrafts)
            : base(reportedAircrafts)
        {
        }

        #endregion

        #region public AgingProgramReportBuilder(Aircraft reportedAircraft) : base(reportedAircraft)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� ��, ��� ������� ��������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedAircraft">������� ��� �������� ��������� �����</param>
        public AgingProgramReportBuilder(Aircraft reportedAircraft)
            : base(reportedAircraft)
        {
        }

        #endregion

        #region public AgingProgramReportBuilder(BaseDetail[] reportedBaseDetails) : base(reportedBaseDetails)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ������� ��������� � ���� ��������
        /// </summary>
        /// <param name="reportedBaseDetails">�������� ��� ������� ��������� �����</param>
        public AgingProgramReportBuilder(BaseDetail[] reportedBaseDetails)
            : base(reportedBaseDetails)
        {
        }

        #endregion

        #region public AgingProgramReportBuilder(BaseDetail reportedBaseDetail) : base(reportedBaseDetail)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� �������� �������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedBaseDetail">������� ��� �������� ��������� �����</param>
        public AgingProgramReportBuilder(BaseDetail reportedBaseDetail)
            : base(reportedBaseDetail)
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
                return "Aging program directives List";
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
