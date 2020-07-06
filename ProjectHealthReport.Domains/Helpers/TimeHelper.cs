using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NodaTime;
using NodaTime.Calendars;

namespace ProjectHealthReport.Domains.Helpers
{
    public static class TimeHelper
    {
        public static DateTime GetFirstWorkingDateOfWeek(int year, int weekOfYear)
        {
            var jan1 = new DateTime(year, 1, 1);
            var daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            var firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            var firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }

        public static DayOfWeek GetDayOfWeek(string day)
        {
            Enum.TryParse<DayOfWeek>(day, out var result);
            return result;
        }

        public static IsoDayOfWeek GetIsoDayOfWeek(string day)
        {
            Enum.TryParse<IsoDayOfWeek>(day, out var result);
            return result;
        }

        public static IEnumerable<int> GetNumberOfWeekNotShowClosedItems(int numberOfWeek = 10)
        {
            return Enumerable.Range(1, numberOfWeek).ToList();
        }

        public static IEnumerable<int> GetNumberOfWeekToDisplay(int numberOfWeek = 50)
        {
            return Enumerable.Range(1, numberOfWeek).ToList();
        }

        public static DateTime GetLastWorkingDateOfWeek(DateTime firstWorkingDate)
        {
            var lastWorkingDate = firstWorkingDate.AddDays(4);

            return lastWorkingDate;
        }

        public static List<int> GetListYears()
        {
            return new List<int> {2018, 2019, 2020, 2021, 2022};
        }

        public static int GetCurrentWeekIso()
        {
            var current = DateTime.Now;

            var rule = WeekYearRules.Iso;

            return rule.GetWeekOfWeekYear(new LocalDate(current.Year, current.Month, current.Day));
        }

        public static int GetCurrentYearIso()
        {
            var current = DateTime.Now;

            var rule = WeekYearRules.Iso;

            return rule.GetWeekYear(new LocalDate(current.Year, current.Month, current.Day));
        }

        public static int GetYearWeek(DateTime dateTime)
        {
            var rule = WeekYearRules.Iso;
            var localDate = new LocalDate(dateTime.Year, dateTime.Month, dateTime.Day);

            var week = rule.GetWeekOfWeekYear(localDate);
            var year = rule.GetWeekYear(localDate);

            return CalculateYearWeek(year, week);
        }

        public static List<int> GetNumberOfWeekInAYear(int year)
        {
            var rule = WeekYearRules.Iso;

            var weekNumber = rule.GetWeeksInWeekYear(year);

            return Enumerable.Range(1, weekNumber).ToList();
        }

        public static int CalculateYearWeek(int year, int week)
        {
            return year * 100 + week;
        }

        public static int CalculateYear(int yearWeek)
        {
            if (yearWeek == 0)
            {
                return 0;
            }

            return Convert.ToInt32(string.Concat(yearWeek.ToString().Take(4)));
        }

        public static int CalculateWeek(int yearWeek)
        {
            if (yearWeek == 0)
            {
                return 0;
            }

            return Convert.ToInt32(string.Concat(yearWeek.ToString().Skip(4)));
        }

        public static DateTime GetDateTimeOfXHourYDayNextZWeekFollowingYear(int xHour, IsoDayOfWeek yDay, int nextZWeek,
            int selectedYear, int selectedWeek)
        {
            var expectedWeek = selectedWeek;
            var expectedYear = selectedYear;

            var dayDifferent = yDay - IsoDayOfWeek.Monday;

            var maxNumberOfWeek = GetNumberOfWeekInAYear(selectedYear).Count;

            if (nextZWeek >= maxNumberOfWeek || xHour >= 24)
            {
                throw new ArgumentOutOfRangeException("nextZWeek must be smaller than the max number of week in " +
                                                      selectedYear);
            }

            if (selectedWeek <= maxNumberOfWeek - nextZWeek)
            {
                expectedWeek = selectedWeek + nextZWeek;
            }
            else
            {
                expectedWeek = selectedWeek - maxNumberOfWeek + nextZWeek;
                expectedYear = selectedYear + 1;
            }

            var firstWorkingDayOfExpectedWeek = GetFirstWorkingDateOfWeek(expectedYear, expectedWeek);
            firstWorkingDayOfExpectedWeek = firstWorkingDayOfExpectedWeek.AddDays(dayDifferent);
            firstWorkingDayOfExpectedWeek = firstWorkingDayOfExpectedWeek.AddHours(xHour);

            return firstWorkingDayOfExpectedWeek;
        }

        public static List<int> GetYearWeeksOfXRecentWeeksStartFrom(int selectedYear, int selectedWeek,
            int numberOfRecentWeeks)
        {
            var listYearWeek = new List<int>();
            var selectedYearWeek = CalculateYearWeek(selectedYear, selectedWeek);

            if (numberOfRecentWeeks < selectedWeek)
            {
                for (int i = 1; i <= numberOfRecentWeeks; i++)
                {
                    listYearWeek.Add(selectedYearWeek - i);
                }
            }
            else
            {
                var lastYear = selectedYear - 1;
                var listLastYearWeek = GetNumberOfWeekInAYear(lastYear);

                if (listLastYearWeek.Count + selectedWeek <= numberOfRecentWeeks)
                {
                    const string errorMessage =
                        "numberOfRecentWeeks must be smaller than sum of selectedWeek and last year max week";

                    throw new ArgumentOutOfRangeException(nameof(numberOfRecentWeeks), errorMessage);
                }

                for (var i = 1; i < selectedWeek; i++)
                {
                    var yearWeekToAdd = CalculateYearWeek(selectedYear, i);
                    listYearWeek.Add(yearWeekToAdd);
                }

                var differentInWeek = numberOfRecentWeeks - selectedWeek;
                differentInWeek += 1;
                listLastYearWeek
                    .OrderByDescending(w => w)
                    .Take(differentInWeek)
                    .ToList()
                    .ForEach(w =>
                    {
                        var yearWeekToAdd = CalculateYearWeek(lastYear, w);
                        listYearWeek.Add(yearWeekToAdd);
                    });
            }

            return listYearWeek.OrderBy(yw => yw).ToList();
        }

        public static int GetLastYearWeek(int yearWeek)
        {
            var year = CalculateYear(yearWeek);
            var week = CalculateWeek(yearWeek);
            var lastYearWeek = TimeHelper.GetYearWeeksOfXRecentWeeksStartFrom(year, week, 1).First();

            return lastYearWeek;
        }

        public static int GetNextYearWeek(int yearWeek)
        {
            var futureDateTime = GetDateTimeOfXHourYDayNextZWeekFollowingYear(0, IsoDayOfWeek.Thursday, 1,
                CalculateYear(yearWeek),
                CalculateWeek(yearWeek));

            return GetYearWeek(futureDateTime);
        }

        public static int GetNextXYearWeek(int yearWeek, int nextNumberOfWeek)
        {
            var futureDateTime = GetDateTimeOfXHourYDayNextZWeekFollowingYear(0, IsoDayOfWeek.Thursday,
                nextNumberOfWeek, CalculateYear(yearWeek),
                CalculateWeek(yearWeek));

            return GetYearWeek(futureDateTime);
        }

        public static List<int> GetListYearWeekFrom(int fromYearWeek, int toYearWeek)
        {
            if (fromYearWeek > toYearWeek)
            {
                throw new InvalidOperationException("FromYearWeek must be smaller than ToYearWeek");
            }

            var fromYear = CalculateYear(fromYearWeek);
            var toYear = CalculateYear(toYearWeek);
            var listInt = new List<int>();

            // if (toYear - fromYear > 1)
            // {
            //     throw new ArgumentOutOfRangeException("fromYearWeek - toYearWeek", "FromYearWeek and ToYearWeek cannot be further than 1 year apart");
            // }

            if (fromYear == toYear)
            {
                for (var i = (fromYearWeek + 1); i < toYearWeek; i++)
                {
                    listInt.Add(i);
                }

                return listInt;
            }

            var biggestWeekFromYear = GetNumberOfWeekInAYear(fromYear).OrderByDescending(w => w).First();
            var fromWeek = CalculateWeek(fromYearWeek);
            for (var i = (fromWeek + 1); i <= biggestWeekFromYear; i++)
            {
                listInt.Add(CalculateYearWeek(fromYear, i));
            }

            for (int i = fromYear + 1; i < toYear; i++)
            {
                var listWeeks = TimeHelper.GetNumberOfWeekInAYear(i).Select(w => CalculateYearWeek(i, w));
                listInt.AddRange(listWeeks);
            }

            var toWeek = CalculateWeek(toYearWeek);
            for (var i = 1; i < toWeek; i++)
            {
                listInt.Add(CalculateYearWeek(toYear, i));
            }

            return listInt;
        }

        public static bool IsDateTimeBetweenDays(DateTime dateTime, IsoDayOfWeek fromDay, int fromHour,
            int fromYearWeek,
            IsoDayOfWeek toDay, int toHour, int toYearWeek)
        {
            var rule = WeekYearRules.Iso;

            var fromYear = CalculateYear(fromYearWeek);
            var fromWeek = CalculateWeek(fromYearWeek);
            var fromLocalDate = rule.GetLocalDate(fromYear, fromWeek, fromDay);
            var fromDate = new DateTime(fromLocalDate.Year, fromLocalDate.Month, fromLocalDate.Day, fromHour, 0, 0);

            var toYear = CalculateYear(toYearWeek);
            var toWeek = CalculateWeek(toYearWeek);
            var toLocalDate = rule.GetLocalDate(toYear, toWeek, toDay);
            var toDate = new DateTime(toLocalDate.Year, toLocalDate.Month, toLocalDate.Day, toHour, 0, 0);

            return dateTime < toDate && dateTime > fromDate;
        }

        public static DateTime GetDate(IsoDayOfWeek dayOfWeek, int isoYearWeek)
        {
            var year = CalculateYear(isoYearWeek);
            var week = CalculateWeek(isoYearWeek);

            return GetDate(dayOfWeek, year, week);
        }

        public static DateTime GetDate(IsoDayOfWeek dayOfWeek, int isoYearWeek, int isoWeekOfYearWeek)
        {
            var rule = WeekYearRules.Iso;
            var localDate = rule.GetLocalDate(isoYearWeek, isoWeekOfYearWeek, dayOfWeek);

            return new DateTime(localDate.Year, localDate.Month, localDate.Day);
        }
    }
}