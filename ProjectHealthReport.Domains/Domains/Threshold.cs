using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjectHealthReport.Domains.Helpers;

namespace ProjectHealthReport.Domains.Domains
{
    public class Threshold
    {
        private int _metricStatusId;
        private MetricStatus _metricStatus;
        private Metric _metric;
        private string _value;
        private bool _isRange;
        private string _lowerBoundOperator;
        private string _upperBoundOperator;
        private decimal? _lowerBound;
        private decimal? _upperBound;
        private int _metricId;

        public Threshold()
        {
        }

        public Threshold(int metricStatusId, int metricId, decimal? upperBound, decimal? lowerBound,
            string upperBoundOperator, string lowerBoundOperator, bool isRange, string value, Metric metric,
            MetricStatus metricStatus)
        {
            _metricStatusId = metricStatusId;
            _metricId = metricId;
            _upperBound = upperBound;
            _lowerBound = lowerBound;
            _upperBoundOperator = upperBoundOperator;
            _lowerBoundOperator = lowerBoundOperator;
            _isRange = isRange;
            _value = value;
            _metric = metric;
            _metricStatus = metricStatus;

            ValidateIsRange();
            ValidateOperators();
        }

        public Threshold(int metricStatusId, int metricId, decimal? upperBound, decimal? lowerBound,
            string upperBoundOperator, string lowerBoundOperator, bool isRange, string value)
        {
            _metricStatusId = metricStatusId;
            _metricId = metricId;
            _upperBound = upperBound;
            _lowerBound = lowerBound;
            _upperBoundOperator = upperBoundOperator;
            _lowerBoundOperator = lowerBoundOperator;
            _isRange = isRange;
            _value = value;
        }

        public int MetricStatusId => _metricStatusId;

        public int MetricId => _metricId;

        public decimal? UpperBound => _upperBound;

        public decimal? LowerBound => _lowerBound;

        public string UpperBoundOperator => _upperBoundOperator;

        public string LowerBoundOperator => _lowerBoundOperator;

        public bool IsRange => _isRange;

        public string Value => _value;

        public Metric Metric => _metric;

        public MetricStatus MetricStatus => _metricStatus;

        public void UpdateValue(int metricStatusId, int metricId, decimal? upperBound, decimal? lowerBound,
            string upperBoundOperator, string lowerBoundOperator, bool isRange, string value)
        {
            _metricStatusId = metricStatusId;
            _metricId = metricId;
            _upperBound = upperBound;
            _lowerBound = lowerBound;
            _upperBoundOperator = upperBoundOperator;
            _lowerBoundOperator = lowerBoundOperator;
            _isRange = isRange;
            _value = value;
        }

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