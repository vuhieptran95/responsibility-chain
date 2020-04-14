using System;
using System.Collections.Generic;

namespace ProjectHealthReport.Domains.Domains
{
    public class Issue
    {
        private int _id;
        private ICollection<AdditionalInfoIssues> _additionalInfoIssues;
        private int _openedYearWeek;
        private string _action;
        private string _impact;
        private string _item;

        public Issue()
        {
            _additionalInfoIssues = new HashSet<AdditionalInfoIssues>();
        }

        public Issue(int id, string item, string impact, string action, int openedYearWeek,
            ICollection<AdditionalInfoIssues> additionalInfoIssues) : this()
        {
            _id = id;
            _item = item;
            _impact = impact;
            _action = action;
            _openedYearWeek = openedYearWeek;
            _additionalInfoIssues = additionalInfoIssues;
        }

        public Issue(int id, int openedYearWeek, string action, string impact, string item) : this()
        {
            _id = id;
            _openedYearWeek = openedYearWeek;
            _action = action;
            _impact = impact;
            _item = item;
        }

        public int Id => _id;

        public string Item => _item;

        public string Impact => _impact;

        public string Action => _action;

        public int OpenedYearWeek => _openedYearWeek;

        public IEnumerable<AdditionalInfoIssues> AdditionalInfoIssues => _additionalInfoIssues;
    }
}