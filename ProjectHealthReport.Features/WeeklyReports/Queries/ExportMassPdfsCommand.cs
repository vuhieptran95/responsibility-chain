using System.Collections.Generic;

namespace ProjectHealthReport.Features.WeeklyReports.Queries
{
    public class ExportMassPdfsCommand
    {
        public ViewSetting ViewSettings { get; set; }
        public IEnumerable<Report> Reports { get; set; }

        public class ViewSetting
        {
            public int NumberOfWeek { get; set; }
            public int NumberOfWeekNotShowClosedItem { get; set; }
        }

        public class Report
        {
            public int ProjectId { get; set; }
            public int YearWeek { get; set; }
        }
    }
}