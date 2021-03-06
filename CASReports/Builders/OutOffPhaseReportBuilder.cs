using CASReports.Builders;
using CASReports.ReportTemplates;
using System;
using CASReports.ReportTemplates;
using CASReports.Datasets;
using System;
using Auxiliary;
using CASTerms;
using SmartCore;
using SmartCore.Calculations;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General;

namespace CASReports.Builders
{
    /// <summary>
    /// Построение отчета AD Status
    /// </summary>
    public class OutOffPhaseReportBuilder : DirectiveListReportBuilder
    {

       /* #region Fileds

        //private readonly OutOffPhaseFilter defaultFilter = new OutOffPhaseFilter();

        #endregion
        
        #region Constructor

        /// <summary>
        /// Создается объект для создания отчетов. Изначально пустой
        /// </summary>
        public OutOffPhaseReportBuilder()
        {
            ReportTitle = DirectiveType.OutOfPhase.CommonName;
            //ReportTitle = "Out of Phase Requirements";
        }

        #endregion
        
        #region Properties
        
    /*    /// <summary>
        /// Фильтр по умолчанию
        /// </summary>
        public override DirectiveFilter DefaultFilter
        {
            get
            {
                return defaultFilter;
            }
        }♥1♥
        #endregion

        #region Methods

        #region public override object GenerateReport()

        /// <summary>
        /// Сгенерируовать отчет по данным, добавленным в текущий объект
        /// </summary>
        /// <returns>Построенный отчет</returns>
        public override object GenerateReport()
        {
            OutOfPhaseRequirmentsReport report = new OutOfPhaseRequirmentsReport();
            report.SetDataSource(GenerateDataSet());
            return report;
        }

        #endregion

        #region public override void AddDirectiveToDataset(Directive directive, DirectiveListReportDataSet destinationDataSet)

        /// <summary>
        /// Добавляется элемент в таблицу данных
        /// </summary>
        /// <param name="directive">Добавлямая директива</param>
        /// <param name="destinationDataSet">Таблица, в которую добавляется элемент</param>
        public override void AddDirectiveToDataset(OutOfPhaseItem directive, DirectiveListReportDataSet destinationDataSet)
        {
            //if (!DefaultFilter.Acceptable(directive))
              //  return;
            string applicability = directive.Applicability;
            string remarks = directive.Remarks;
            if (directive.LastPerformance != null && directive.LastPerformance.Description != "")
            {
                if (remarks != "")
                    remarks += "." + Environment.NewLine + directive.GetLastPerformance().Description;
                else
                    remarks += directive.GetLastPerformance().Description;
            }
            string description = directive.Description;
            string title = directive.Title;
            string references = directive.References;
            string status = "Open";
           
                if (directive.IsClosed)
                    status = "Closed";
                else if (directive.Threshold.PerformRepeatedly && directive.Threshold.PerformSinceNew)
                    status = "Repeat";
            }
            string effectivityDate = UsefulMethods.NormalizeDate(directive.Threshold.EffectiveDate);
            string thresholdSinceNew = "";
            if (directive.PerformSinceNew)
                thresholdSinceNew = lifelengthFormatter.GetData(directive.FirstPerformSinceNew, " hrs\r\n", " cyc\r\n", " day");
            string thresholdSInceEffectivityDate = "";
            if (directive.PerformSinceEffectivityDate)
                thresholdSInceEffectivityDate = lifelengthFormatter.GetData(directive.SinceEffectivityDatePerformanceLifelength, " hrs\r\n", " cyc\r\n", " day");
            string repeatIntervals = "";
            if (!directive.Closed && directive.RepeatedlyPerform)
                repeatIntervals = lifelengthFormatter.GetData(directive.RepeatPerform, " hrs\r\n", " cyc\r\n");
            string compliance = lifelengthFormatter.GetData(directive.LastPerformanceLifelength, " hrs\r\n", " cyc\r\n", "");
            string complianceDate = "";
            if (directive.LastPerformance != null)
                complianceDate = UsefulMethods.NormalizeDate(directive.LastPerformance.RecordDate);
            Lifelength nextPerfomance = directive.NextPerformance;
            string nextDate = directive.ApproximateDate.ToString(new GlobalTermsProvider()["DateFormat"].ToString());
            string next = LifelengthFormatter.GetData(nextPerfomance, " hrs\r\n", " cyc\r\n", "");
            string remains = LifelengthFormatter.GetData(directive.LeftTillNextPerformance, " hrs\r\n", " cyc\r\n",
                                                         " day");
            if (!nextPerfomance.Applicable || nextPerfomance == null)
            {
                next = "";
                nextDate = "";
                remains = "";
            }
            //string conditionState = UsefulMethods.GetDirectiveColorName(directive);
            string adType = "";
            adType = directive.ADType == ADType.Airframe ? "AF" : "AP";
            destinationDataSet.ItemsTable.AddItemsTableRow(directive.ID, applicability, remarks, description, title,
                                                           references, (status == "Open" || status == "Repeat") ? "Open" : "Closed",
                                                           effectivityDate,
                                                           (status == "Open" || status == "Repeat") ? thresholdSinceNew : "",
                                                           (status == "Open" || status == "Repeat") ? thresholdSInceEffectivityDate : "",
                                                           (status == "Open" || status == "Repeat") ? repeatIntervals : "",
                                                           compliance, complianceDate, next, nextDate, remains,
                //conditionState, "", directive.AtaChapter.ShortName, directive.AtaChapter.FullName);
                                                           //conditionState, "", "N/A", "N/A", adType,"");
                                                           "", "", "N/A", "N/A", adType, "");
        }

        #endregion


        #endregion*/
    }
}