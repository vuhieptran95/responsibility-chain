﻿using System;

 namespace ProjectHealthReport.Domains.Entities
{
    public class Milestone
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public DateTime? Date { get; set; }
        public string Target { get; set; }
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public Project Project { get; set; }
    }
}