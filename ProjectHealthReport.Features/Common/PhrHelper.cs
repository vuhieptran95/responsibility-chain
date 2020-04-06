namespace ProjectHealthReport.Features.Common
{
    public static class PhrHelper
    {
        public const string MissedTimesStatus = "missed_times";
        public const string MissedStatus = "missed";
        public const string OnTimeStatus = "ontime";
        public const string FilledStatus = "filled";
        public const string NotFilledStatus = "notfill";

        public static string CreateMissedTimesStatus(int times)
        {
            return MissedTimesStatus + ":" + times;
        }

        public const decimal DoDMaxValue = 1000000000;
    }
}