using System;
using System.Collections.Generic;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Features.WeeklyReports.Queries.GetWeeklyReportPhr
{
    public partial class GetWeeklyReportPhrQuery
    {
        public class Dto
        {
            public Dto()
            {
                BacklogItem = new BacklogItemDto();
                QualityReport = new QualityReportDto();
                BacklogItemListReadOnly = new List<BacklogItemDto>();
                QualityReportListReadOnly = new List<QualityReportDto>();
                Metrics = new List<MetricDto>();
                DodRecords = new List<DoDReportDto>();
            }

            public int ProjectId { get; set; }
            public string ProjectName { get; set; }
            public bool DodRequired { get; set; }
            public IEnumerable<int> Years => TimeHelper.GetListYears();
            public IEnumerable<int> Weeks => TimeHelper.GetNumberOfWeekInAYear(SelectedYear);
            public IEnumerable<int> NumberOfWeeks => TimeHelper.GetNumberOfWeekToDisplay();
            public IEnumerable<int> NumberOfWeekNotShowClosedItems => TimeHelper.GetNumberOfWeekNotShowClosedItems();
            public IEnumerable<string> Statuses => new List<string> {"GREEN", "YELLOW", "RED"};
            public IEnumerable<string> AdditionalInfoStatues => new List<string> {"Open", "Closed"};
            public DateTime FirstWorkingDateOfWeek => TimeHelper.GetFirstWorkingDateOfWeek(SelectedYear, SelectedWeek);

            public DateTime LastWorkingDateOfWeek =>
                TimeHelper.GetLastWorkingDateOfWeek(TimeHelper.GetFirstWorkingDateOfWeek(SelectedYear, SelectedWeek));

            public StatusDto Status { get; set; }
            public BacklogItemDto BacklogItem { get; set; }
            public QualityReportDto QualityReport { get; set; }
            public List<BacklogItemDto> BacklogItemListReadOnly { get; set; }
            public List<QualityReportDto> QualityReportListReadOnly { get; set; }
            public List<DoDReportDto> DodRecords { get; set; }
            public List<MetricDto> Metrics { get; set; }
            public int SelectedYear { get; set; }
            public int SelectedWeek { get; set; }
            public int NumberOfWeek { get; set; }
            public int NumberOfWeekNotShowClosedItem { get; set; }

            public class StatusDto : IMapFrom<Status>
            {
                public int Id { get; set; }
                public string StatusColor { get; set; }
                public string RetrospectiveFeedBack { get; set; }
                public string ProjectStatus { get; set; }
                public DateTime? MilestoneDate { get; set; }
                public string Milestone { get; set; }
                public int YearWeek { get; set; }
            }

            public class BacklogItemDto : IMapFrom<BacklogItem>
            {
                public int Id { get; set; }
                public int? Sprint { get; set; }
                public int ItemsAdded { get; set; }
                public int? StoryPointsAdded { get; set; }
                public int ItemsDone { get; set; }
                public int? StoryPointsDone { get; set; }
                public int ItemsRemaining { get; set; }
                public int? StoryPointsRemaining { get; set; }
                public int YearWeek { get; set; }
                public int Year  => TimeHelper.CalculateYear(YearWeek);
                public int Week => TimeHelper.CalculateWeek(YearWeek);
            }

            public class QualityReportDto : IMapFrom<QualityReport>
            {
                public int Id { get; set; }
                public int NewBugs { get; set; }
                public int CriticalBugs { get; set; }
                public int MajorBugs { get; set; }
                public int MinorBugs { get; set; }
                public int DoneBugs { get; set; }
                public int ReOpenBugs { get; set; }
                public int RemainingBugs { get; set; }
                public int YearWeek { get; set; }
                public int Year => TimeHelper.CalculateYear(YearWeek);
                public int Week => TimeHelper.CalculateWeek(YearWeek);
            }
            
            public class DoDReportDto : IMapFrom<DoDReport>
            {
                public int ProjectId { get; set; }
                public int MetricId { get; set; }
                public MetricDto Metric { get; set; }
                public string Value { get; set; }
                public int YearWeek { get; set; }
                public string LinkToReport { get; set; }
                public string ReportFileName { get; set; }
            }

            public class MetricDto
            {
                public int ProjectId { get; set; }
                public int Id { get; set; }
                public string Name { get; set; }
                public string Tool { get; set; }
                public int ToolOrder { get; set; }
                public int Order { get; set; }
                public int Count { get; set; }
                public string Unit { get; set; }
                public string ValueType { get; set; }
                public string SelectValues { get; set; }
                public string Value { get; set; }
                public int YearWeek { get; set; }
                public List<YearWeekValue> YearWeekValues { get; set; } = new List<YearWeekValue>();
            }

            public class YearWeekValue
            {
                public int YearWeek { get; set; }
                public string Value { get; set; }
            }
        }
    }
}