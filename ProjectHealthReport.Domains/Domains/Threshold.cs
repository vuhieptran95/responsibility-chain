using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjectHealthReport.Domains.Helpers;

namespace ProjectHealthReport.Domains.Domains
{
    public class Threshold
    {
        public Threshold()
        {
        }

        public Threshold(int metricStatusId, int metricId, decimal? upperBound, decimal? lowerBound,
            string upperBoundOperator, string lowerBoundOperator, bool isRange, string value, Metric metric,
            MetricStatus metricStatus)
        {
            MetricStatusId = metricStatusId;
            MetricId = metricId;
            UpperBound = upperBound;
            LowerBound = lowerBound;
            UpperBoundOperator = upperBoundOperator;
            LowerBoundOperator = lowerBoundOperator;
            IsRange = isRange;
            Value = value;
            Metric = metric;
            MetricStatus = metricStatus;

            ValidateIsRange();
            ValidateOperators();
        }

        public int MetricStatusId { get; private set; }
        public int MetricId { get; private set; }
        public decimal? UpperBound { get; private set; }
        public decimal? LowerBound { get; private set; }
        public string UpperBoundOperator { get; private set; }
        public string LowerBoundOperator { get; private set; }
        public bool IsRange { get; private set; }
        public string Value { get; private set; }
        public Metric Metric { get; private set; }
        public MetricStatus MetricStatus { get; private set; }

        public void SetMetricId(int metricId) => MetricId = metricId;

        public void ValidateIsRange()
        {
            if (IsRange)
            {
                if (!string.IsNullOrEmpty(Value))
                {
                    throw new ValidationException(
                        $"If threshold is in range, Value cannot be specified - metric Id: {MetricId} - metric status Id: {MetricStatusId}");
                }

                if (Metric != null && Metric.ValueType != DoDHelper.ValueTypeNumber)
                {
                    throw new ValidationException(
                        $"Threshold can only be in range when its metric's value type is Number - metric Id: {MetricId} - metric status Id: {MetricStatusId}");
                }

                if (LowerBound == null || UpperBound == null ||
                    LowerBoundOperator == null || UpperBoundOperator == null)
                {
                    throw new ValidationException(
                        $"Threshold bounds and operators must be specified - metric Id: {MetricId} - metric status Id: {MetricStatusId}");
                }
            }
        }

        public void ValidateOperators()
        {
            if (!IsRange)
            {
                return;
            }

            var operators = (LowerBoundOperator, UpperBoundOperator);

            switch (operators)
            {
                case ("<", "<"):
                case ("<", "<="):
                case ("<=", "<"):
                case ("<=", "<="):
                case (">", ">"):
                case (">=", ">"):
                case (">", ">="):
                case (">=", ">="): return;
                default:
                    throw new ValidationException(
                        $"Invalid operators - metric Id: {MetricId} - metric status Id: {MetricStatusId}");
            }
        }
        
        private sealed class ThresholdEqualityComparer : IEqualityComparer<Threshold>
        {
            public bool Equals(Threshold x, Threshold y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.MetricStatusId == y.MetricStatusId && x.MetricId == y.MetricId;
            }

            public int GetHashCode(Threshold obj)
            {
                return HashCode.Combine(obj.MetricStatusId, obj.MetricId);
            }
        }

        public static IEqualityComparer<Threshold> ThresholdComparer { get; } = new ThresholdEqualityComparer();
    }
}