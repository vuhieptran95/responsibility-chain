using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ProjectHealthReport.Domains.Helpers
{
    // TODO use data annotation attribute
    public static class ValidationHelper
    {
        public const string NumberMustBeNonNegative = "Number must be non-negative if provided";

        public static void ValidateIntValue(PropertyInfo p, object validateObject)
        {
            if (p.PropertyType == typeof(int))
            {
                int intValue = (int)p.GetValue(validateObject);
                if (intValue < 0) throw new ValidationException("Integer values inputted must be greater or equal 0");
            }

            if (p.PropertyType == typeof(int?))
            {
                int? intValue = (int?)p.GetValue(validateObject);
                if (intValue.HasValue && intValue.Value < 0) throw new ValidationException("Integer values inputted must be greater or equal 0");
            }
        }

        // TODO this will cause problem when the last week of this year is the first week of next year
        public static void ValidateQueryTimeAgainstCurrentTime(int year, int week)
        {
            var queryYearWeek = TimeHelper.CalculateYearWeek(year, week);
            var currentYearWeek = TimeHelper.CalculateYearWeek(DateTime.Now.Year, TimeHelper.GetCurrentWeekIso());

            if (queryYearWeek > currentYearWeek)
            {
                throw new ArgumentOutOfRangeException(nameof(week), $"Cannot view future report of year {year} and week {week}");
            }
        }
    }
}