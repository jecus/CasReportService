using LTR.Core;
using LTR.Core.Types.Aircrafts.Parts;
using LTR.Core.Types.Directives;
using LTR.Core.Types.ReportFilters;

namespace LTRReports
{
    /// <summary>
    /// ���������� ������ AD Status
    /// </summary>
    public class EngineeringOrdersReportBuilder : DirectiveListReportBuilder
    {
        private readonly EngeneeringOrderFilter defaultFilter = new EngeneeringOrderFilter();

        #region Constructors

        #region public EngineeringOrdersReportBuilder()

        /// <summary>
        /// ��������� ������ ��� �������� �������. ���������� ������
        /// </summary>
        public EngineeringOrdersReportBuilder()
        {
        }

        #endregion

        #region public EngineeringOrdersReportBuilder(BaseDetailDirective[] directives) : base(directives)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ��������
        /// </summary>
        /// <param name="directives">�������� ��� ������� ��������� �����</param>
        public EngineeringOrdersReportBuilder(BaseDetailDirective[] directives)
            : base(directives)
        {
        }

        #endregion

        #region public EngineeringOrdersReportBuilder(Operator reportedOperator) : base(reportedOperator)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� ���������, ��� �� � ������� ��������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedOperator">������� ��� �������� ��������� �����</param>
        public EngineeringOrdersReportBuilder(Operator reportedOperator)
            : base(reportedOperator)
        {
        }

        #endregion

        #region public EngineeringOrdersReportBuilder(Operator[] reportedOperators) : base(reportedOperators)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ����������, �� �� ��� ������� ��������� � ���� ��������
        /// </summary>
        /// <param name="reportedOperators">�������� ��� ������� ��������� �����</param>
        public EngineeringOrdersReportBuilder(Operator[] reportedOperators)
            : base(reportedOperators)
        {
        }

        #endregion

        #region public EngineeringOrdersReportBuilder(Aircraft[] reportedAircrafts) : base(reportedAircrafts)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ���������, �� ������� ��������� � ��������, ������������ � ���
        /// </summary>
        /// <param name="reportedAircrafts">�������� ��� ������� ��������� �����</param>
        public EngineeringOrdersReportBuilder(Aircraft[] reportedAircrafts)
            : base(reportedAircrafts)
        {
        }

        #endregion

        #region public EngineeringOrdersReportBuilder(Aircraft reportedAircraft) : base(reportedAircraft)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� ��, ��� ������� ��������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedAircraft">������� ��� �������� ��������� �����</param>
        public EngineeringOrdersReportBuilder(Aircraft reportedAircraft)
            : base(reportedAircraft)
        {
        }

        #endregion

        #region public EngineeringOrdersReportBuilder(BaseDetail[] reportedBaseDetails) : base(reportedBaseDetails)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ������� ��������� � ���� ��������
        /// </summary>
        /// <param name="reportedBaseDetails">�������� ��� ������� ��������� �����</param>
        public EngineeringOrdersReportBuilder(BaseDetail[] reportedBaseDetails)
            : base(reportedBaseDetails)
        {
        }

        #endregion

        #region public EngineeringOrdersReportBuilder(BaseDetail reportedBaseDetail) : base(reportedBaseDetail)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� �������� �������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedBaseDetail">������� ��� �������� ��������� �����</param>
        public EngineeringOrdersReportBuilder(BaseDetail reportedBaseDetail)
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
                return "Engeneering Orders";
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
