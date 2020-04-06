using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjectHealthReport.Domains.Exceptions;
using ProjectHealthReport.Domains.Helpers;

namespace ProjectHealthReport.Domains.Domains
{
    public class DoDReport : IWeeklyReport
    {
        public DoDReport()
        {
            
        }
        public DoDReport(int projectId, int metricId, int yearWeek, string value, string linkToReport,
            string reportFileName, Project project, Metric metric)
        {
            ProjectId = projectId;
            MetricId = metricId;
            YearWeek = yearWeek;
            Value = value;
            LinkToReport = linkToReport;
            ReportFileName = reportFileName;
            Project = project;
            Metric = metric;

            Validate();
        }

        public int ProjectId { get; private set; }
        public int MetricId { get; private set; }
        public int YearWeek { get; set; }
        [Required] public string Value { get; private set; }
        public Project Project { get; private set; }
        public Metric Metric { get; private set; }
        public string LinkToReport { get; private set; }
        public string ReportFileName { get; private set; }

        public void SetReportFile(string name, string link)
        {
            LinkToReport = link;
            ReportFileName = name;

            ReportFileMustHaveBothLinkAndFileName();
        }

        public void Validate()
        {
            ValidateMetric();
            ValidateProject();
            ValidateLink();
            ReportFileMustHaveBothLinkAndFileName();
        }

        public void ReportFileMustHaveBothLinkAndFileName()
        {
            if (!string.IsNullOrEmpty(LinkToReport) && !string.IsNullOrEmpty(ReportFileName))
            {
                return;
            }

            DomainExceptionCode.Throw(DomainError.D016, this);
        }

        public void ValidateLink()
        {
            if (!MiscHelper.ValidateLink(LinkToReport))
            {
                DomainExceptionCode.Throw(DomainError.D017, this);
            }
        }

        public void ValidateMetric()
        {
            var eval = (Metric.ValueType, double.TryParse(Value, out var n));
            switch (eval)
            {
                case (DoDHelper.ValueTypeNumber, false): DomainExceptionCode.Throw(DomainError.D018, this, Metric);
                    break;
                case (DoDHelper.ValueTypeSelect, _):
                    if (!Metric.SelectValues.Contains(Value))
                    {
                        DomainExceptionCode.Throw(DomainError.D019, this, Metric);
                    }

                    break;
            }
        }

        public void ValidateProject()
        {
            if (!Project.DodRequired)
            {
                DomainExceptionCode.Throw(DomainError.D020, this, Project);
            }
        }

        private sealed class ProjectIdMetricIdYearWeekEqualityComparer : IEqualityComparer<DoDReport>
        {
            public bool Equals(DoDReport x, DoDReport y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.ProjectId == y.ProjectId && x.MetricId == y.MetricId && x.YearWeek == y.YearWeek;
            }

            public int GetHashCode(DoDReport obj)
            {
                return HashCode.Combine(obj.ProjectId, obj.MetricId, obj.YearWeek);
            }
        }

        public static IEqualityComparer<DoDReport> DoDComparer { get; } = new ProjectIdMetricIdYearWeekEqualityComparer();
    }
}