using LTR.Core;
using LTR.Core.Types.Aircrafts.Parts;
using LTR.Core.Types.Directives;
using LTR.Core.Types.ReportFilters;

namespace LTRReports
{
    /// <summary>
    /// ���������� ������ AD Status
    /// </summary>
    public class ModificationStatusReportBuilder : DirectiveListReportBuilder
    {
        private readonly ModificationFilter defaultFilter = new ModificationFilter();

        #region Constructors

        #region public ModificationStatusReportBuilder()

        /// <summary>
        /// ��������� ������ ��� �������� �������. ���������� ������
        /// </summary>
        public ModificationStatusReportBuilder()
        {
        }

        #endregion

        #region public ModificationStatusReportBuilder(BaseDetailDirective[] directives) : base(directives)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ��������
        /// </summary>
        /// <param name="directives">�������� ��� ������� ��������� �����</param>
        public ModificationStatusReportBuilder(BaseDetailDirective[] directives)
            : base(directives)
        {
        }

        #endregion

        #region public ModificationStatusReportBuilder(Operator reportedOperator) : base(reportedOperator)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� ���������, ��� �� � ������� ��������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedOperator">������� ��� �������� ��������� �����</param>
        public ModificationStatusReportBuilder(Operator reportedOperator)
            : base(reportedOperator)
        {
        }

        #endregion

        #region public ModificationStatusReportBuilder(Operator[] reportedOperators) : base(reportedOperators)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ����������, �� �� ��� ������� ��������� � ���� ��������
        /// </summary>
        /// <param name="reportedOperators">�������� ��� ������� ��������� �����</param>
        public ModificationStatusReportBuilder(Operator[] reportedOperators)
            : base(reportedOperators)
        {
        }

        #endregion

        #region public ModificationStatusReportBuilder(Aircraft[] reportedAircrafts) : base(reportedAircrafts)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ���������, �� ������� ��������� � ��������, ������������ � ���
        /// </summary>
        /// <param name="reportedAircrafts">�������� ��� ������� ��������� �����</param>
        public ModificationStatusReportBuilder(Aircraft[] reportedAircrafts)
            : base(reportedAircrafts)
        {
        }

        #endregion

        #region public ModificationStatusReportBuilder(Aircraft reportedAircraft) : base(reportedAircraft)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� ��, ��� ������� ��������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedAircraft">������� ��� �������� ��������� �����</param>
        public ModificationStatusReportBuilder(Aircraft reportedAircraft)
            : base(reportedAircraft)
        {
        }

        #endregion

        #region public ModificationStatusReportBuilder(BaseDetail[] reportedBaseDetails) : base(reportedBaseDetails)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� �������� ������� ��������� � ���� ��������
        /// </summary>
        /// <param name="reportedBaseDetails">�������� ��� ������� ��������� �����</param>
        public ModificationStatusReportBuilder(BaseDetail[] reportedBaseDetails)
            : base(reportedBaseDetails)
        {
        }

        #endregion

        #region public ModificationStatusReportBuilder(BaseDetail reportedBaseDetail) : base(reportedBaseDetail)

        /// <summary>
        /// ��������� ������ ��� �������� ������� �������� ��� ��������� �������� �������� � ���� ��� ��������
        /// </summary>
        /// <param name="reportedBaseDetail">������� ��� �������� ��������� �����</param>
        public ModificationStatusReportBuilder(BaseDetail reportedBaseDetail)
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
                return "Modification Status";
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
