namespace ProjectHealthReport.Features.WeeklyReports.Commands.AddEditStatus
{
    public class AddEditStatusDto
    {
        public int Id { get; set; }
        public string StatusColor { get; set; }
        public string RetrospectiveFeedBack { get; set; }
        public string ProjectStatus { get; set; }
        public int YearWeek { get; set; }
        public int Year { get; set; }
        public int Week { get; set; }
    }
}
