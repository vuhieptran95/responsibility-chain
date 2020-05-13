using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjectHealthReport.Domains.Exceptions;
using ProjectHealthReport.Domains.Helpers;

namespace ProjectHealthReport.Domains.Domains
{
    public class DoDReport : IWeeklyReport
    {
        protected int _projectId;
        protected string _reportFileName;
        protected string _linkToReport;
        protected Metric _metric;
        protected Project _project;
        protected string _value;
        protected int _yearWeek;
        protected int _metricId;

        public DoDReport()
        {
            
        }
        public DoDReport(int projectId, int metricId, int yearWeek, string value, string linkToReport,
            string reportFileName, Project project, Metric metric)
        {
            _projectId = projectId;
            _metricId = metricId;
            YearWeek = yearWeek;
            _value = value;
            _linkToReport = linkToReport;
            _reportFileName = reportFileName;
            _project = project;
            _metric = metric;

            Validate();
        }

        public DoDReport(int projectId, string reportFileName, string linkToReport, string value, int yearWeek, int metricId)
        {
            _projectId = projectId;
            _reportFileName = reportFileName;
            _linkToReport = linkToReport;
            _value = value;
            _yearWeek = yearWeek;
            _metricId = metricId;
        }

        public int ProjectId => _projectId;

        public int MetricId => _metricId;

        public int YearWeek
        {
            get => _yearWeek;
            set => _yearWeek = value;
        }

        [Required]
        public string Value => _value;

        public Project Project => _project;

        public Metric Metric => _metric;

        public string LinkToReport => _linkToReport;

        public string ReportFileName => _reportFileName;

        public void SetReportFile(string name, string link)
        {
            _linkToReport = link;
            _reportFileName = name;

            ReportFileMustHaveBothLinkAndFileName();
        }

        public void Validate()
        {
            ValidateMetric();
            // ValidateProject();
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
            if (Metric == null)
            {
                DomainExceptionCode.Throw(DomainError.D034, this, Metric);
            }
            
            var eval = (Metric.ValueType, double.TryParse(Value, out var n));
            switch (eval)
            {
                case (DoDHelper.ValueTypeNumber, false): DomainExceptionCode.Throw(DomainError.D018, this, Metric);
                    break;
                case (DoDHelper.ValueTypeSelect, _):
                    if (string.IsNullOrEmpty(Value) || !Metric.SelectValues.Contains(Value))
                    {
                        DomainExceptionCode.Throw(DomainError.D019, this, Metric);
                    }

                    break;
            }
        }

        public void ValidateProject()
        {
            if (Project == null)
            {
                DomainExceptionCode.Throw(DomainError.D035, this, Project);
            }
            
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