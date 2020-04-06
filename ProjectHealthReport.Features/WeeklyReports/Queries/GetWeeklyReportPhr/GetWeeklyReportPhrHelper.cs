using System.Collections.Generic;
using System.Linq;

namespace ProjectHealthReport.Features.WeeklyReports.Queries.GetWeeklyReportPhr
{
    public static class GetWeeklyReportPhrHelper
    {
        public static void CalculateBacklogItemsRemaining(
            List<GetWeeklyReportPhrQuery.Dto.BacklogItemDto> listBacklogItemDtoReadOnly)
        {
            if (listBacklogItemDtoReadOnly.Count > 0)
            {
                for (var i = 0; i < listBacklogItemDtoReadOnly.Count; i++)
                {
                    if (i == 0)
                    {
                        listBacklogItemDtoReadOnly[i].ItemsRemaining = listBacklogItemDtoReadOnly[i].ItemsAdded;
                        listBacklogItemDtoReadOnly[i].StoryPointsRemaining =
                            listBacklogItemDtoReadOnly[i].StoryPointsAdded;
                    }
                    else
                    {
                        listBacklogItemDtoReadOnly[i].ItemsRemaining =
                            listBacklogItemDtoReadOnly[i - 1].ItemsRemaining +
                            listBacklogItemDtoReadOnly[i].ItemsAdded - listBacklogItemDtoReadOnly[i].ItemsDone;

                        listBacklogItemDtoReadOnly[i].StoryPointsRemaining =
                            listBacklogItemDtoReadOnly[i - 1].StoryPointsRemaining +
                            listBacklogItemDtoReadOnly[i].StoryPointsAdded -
                            listBacklogItemDtoReadOnly[i].StoryPointsDone;
                    }
                }
            }
        }

        public static void CalculateNewAndRemainingBugs(
            List<GetWeeklyReportPhrQuery.Dto.QualityReportDto> listQualityReportDtoReadOnly)
        {
            for (var i = 0; i < listQualityReportDtoReadOnly.Count; i++)
            {
                listQualityReportDtoReadOnly[i].NewBugs = listQualityReportDtoReadOnly[i].CriticalBugs +
                                                          listQualityReportDtoReadOnly[i].MajorBugs +
                                                          listQualityReportDtoReadOnly[i].MinorBugs;
                if (i == 0)
                {
                    listQualityReportDtoReadOnly[i].RemainingBugs =
                        listQualityReportDtoReadOnly[i].NewBugs - listQualityReportDtoReadOnly[i].DoneBugs;
                }
                else
                {
                    listQualityReportDtoReadOnly[i].RemainingBugs =
                        listQualityReportDtoReadOnly[i - 1].RemainingBugs + listQualityReportDtoReadOnly[i].NewBugs -
                        listQualityReportDtoReadOnly[i].DoneBugs;
                }
            }
        }

        public static List<GetWeeklyReportPhrQuery.Dto.AdditionalInfoDto> TakeAdditionalInfoItemsInLastXWeek(
            List<GetWeeklyReportPhrQuery.Dto.AdditionalInfoDto> listAdditionalInfoReadOnly, int numberOfWeek)
        {
            var distinctYearWeeksToTake = listAdditionalInfoReadOnly.Select(a => a.YearWeek).Distinct().ToArray()
                .TakeLast(numberOfWeek).ToList();

            return listAdditionalInfoReadOnly.Where(a => distinctYearWeeksToTake.Contains(a.YearWeek)).ToList();
        }

        public static List<GetWeeklyReportPhrQuery.Dto.AdditionalInfoDto> RemoveClosedAdditionalInfoItemsOlderThanXWeek(
            List<GetWeeklyReportPhrQuery.Dto.AdditionalInfoDto> listAdditionalInfoReadOnly, int numberOfWeek)
        {
            var distinctYearWeeks = listAdditionalInfoReadOnly.Select(a => a.YearWeek).Distinct().ToArray();

            if (distinctYearWeeks.Count() <= numberOfWeek)
            {
                return listAdditionalInfoReadOnly;
            }

            var closedItemYearWeeks = distinctYearWeeks.Take(distinctYearWeeks.Count() - numberOfWeek);
            var closedItemsToRemove =
                listAdditionalInfoReadOnly.Where(a => a.Status == "Closed" && closedItemYearWeeks.Contains(a.YearWeek));

            return listAdditionalInfoReadOnly.Except(closedItemsToRemove).ToList();
        }

        public static void PopulateAdditionalInfoItemsOfThisWeekBasedOnLastWeek(GetWeeklyReportPhrQuery.Dto dto)
        {
            var listAdditionalInfoIssue = dto.AdditionalInfos.Select(a => a.IssueId);
            var listAdditionalInfoIssueEditableStatus = dto.AdditionalInfoListWithEditableStatus.Select(a => a.IssueId);

            var issueIdDifference = listAdditionalInfoIssueEditableStatus.Except(listAdditionalInfoIssue);

            dto.AdditionalInfos.AddRange(
                dto.AdditionalInfoListWithEditableStatus.Where(a => issueIdDifference.Contains(a.IssueId)));

            dto.AdditionalInfos = dto.AdditionalInfos.OrderBy(a => a.YearWeek).ToList();
        }
    }
}