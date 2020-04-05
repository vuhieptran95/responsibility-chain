using System.ComponentModel.DataAnnotations;

namespace ProjectHealthReport.Domains.Domains
{
    public class DoDReport : IWeeklyReport
    {
        public DoDReport()
        {
        }

        public DoDReport(int projectId, int metricId, int yearWeek, string value, string linkToReport,
            string reportFileName)
        {
            ProjectId = projectId;
            MetricId = metricId;
            YearWeek = yearWeek;
            Value = value;
            LinkToReport = linkToReport;
            ReportFileName = reportFileName;

            ReportFileMustHaveBothLinkAndFileName();
        }

        public int ProjectId { get; private set; }
        public int MetricId { get; private set; }
        public int YearWeek { get; set; }
        [Required] public string Value { get; private set; }
        public Project Project { get; set; }
        public Metric Metric { get; set; }
        public string LinkToReport { get; private set; }
        public string ReportFileName { get; private set; }

        public void SetReportFile(string name, string link)
        {
            LinkToReport = link;
            ReportFileName = name;
            
            ReportFileMustHaveBothLinkAndFileName();
        }

        public void ReportFileMustHaveBothLinkAndFileName()
        {
            if (!string.IsNullOrEmpty(LinkToReport) && !string.IsNullOrEmpty(ReportFileName))
            {
                return;
            }

            throw new ValidationException("Report file must have both Link and File Name");
        }
    }
}