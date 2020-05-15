﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectHealthReport.Domains.Domains
{
    public partial class Project
    {
        public void AddEditStatus(Status status)
        {
            // TODO Validate status data related to projects....

            if (status.Id == 0)
            {
                _statuses.Add(status);
            }
            else
            {
                var statusInDb = _statuses.First(s => s.Id == status.Id);
                statusInDb.UpdateValue(status);
            }
        }

        public void AddEditBacklogItem(BacklogItem item)
        {
            // TODO Validate BacklogItem data

            if (item.Id == 0)
            {
                _backlogItems.Add(item);
            }
            else
            {
                var itemInDb = _backlogItems.First(b => b.Id == item.Id);
                itemInDb.UpdateValue(item);
            }
        }

        public void AddEditQualityReport(QualityReport item)
        {
            // TODO Validate Quality Report data

            if (item.Id == 0)
            {
                _qualityReports.Add(item);
            }
            else
            {
                var itemInDb = _qualityReports.First(b => b.Id == item.Id);
                itemInDb.UpdateValue(item);
            }
        }

        public void ReplaceProjectAccess(List<ProjectAccess> accesses)
        {
            _projectAccesses = accesses;
        }

        public void AddEditDivisionStatus(DivisionProjectStatus status)
        {
            // TODO Validate Division Project Status data

            if (status.Id == 0)
            {
                _divisionProjectStatuses.Add(status);
            }
            else
            {
                var statusInDb = _divisionProjectStatuses.First(s => s.Id == status.Id);
                statusInDb.UpdateValue(status);
            }
        }

        public void EditNonMasterData(string platformVersion, string jiraLink, string sourceCodeLink, string note,
            DateTime? projectEndDate, BacklogItem item, List<ProjectAccess> projectAccesses)
        {
            _platformVersion = platformVersion;
            _jiraLink = jiraLink;
            _sourceCodeLink = sourceCodeLink;
            _note = note;
            _projectEndDate = projectEndDate;
            
            AddEditBacklogItem(item);
            
            ReplaceProjectAccess(projectAccesses);
            
            ValidateLinkProperties();
            ValidateProjectEndDate();
        }
    }
}