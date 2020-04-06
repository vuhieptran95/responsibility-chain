using System;
using System.Collections.Generic;

namespace ProjectHealthReport.Domains.Domains
{
    public class Issue
    {
        public Issue()
        {
            AdditionalInfoIssues = new HashSet<AdditionalInfoIssues>();
        }

        public Issue(int id, string item, string impact, string action, int openedYearWeek,
            ICollection<AdditionalInfoIssues> additionalInfoIssues) : this()
        {
            Id = id;
            Item = item;
            Impact = impact;
            Action = action;
            OpenedYearWeek = openedYearWeek;
            AdditionalInfoIssues = additionalInfoIssues ?? AdditionalInfoIssues;
        }

        public int Id { get; private set; }
        public string Item { get; private set; }
        public string Impact { get; private set; }
        public string Action { get; private set; }
        public int OpenedYearWeek { get; private set; }
        public ICollection<AdditionalInfoIssues> AdditionalInfoIssues { get; private set; }
    }
}