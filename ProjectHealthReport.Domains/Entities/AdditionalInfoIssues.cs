﻿namespace ProjectHealthReport.Domains.Entities
{
    public class AdditionalInfoIssues
    {
        public int AdditionalInfoId { get; set; }
        public int IssueId { get; set; }
        public string Status { get; set; }
        public AdditionalInfo AdditionalInfo { get; set; }
        public Issue Issue { get; set; }
    }
}