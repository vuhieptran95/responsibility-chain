using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjectHealthReport.Domains.Exceptions;
using ProjectHealthReport.Domains.Helpers;

namespace ProjectHealthReport.Domains.Domains
{
    public class Threshold
    {
        protected int _metricStatusId;
        protected MetricStatus _metricStatus;
        protected Metric _metric;
        protected string _value;
        protected bool _isRange;
        protected string _lowerBoundOperator;
        protected string _upperBoundOperator;
        protected decimal? _lowerBound;
        protected decimal? _upperBound;
        protected int _metricId;

        public Threshold()
        {
        }


        public Threshold(int metricStatusId, int metricId, decimal? upperBound, decimal? lowerBound,
            string upperBoundOperator, string lowerBoundOperator, bool isRange, string value, MetricStatus metricStatus,
            Metric metric) : this(metricStatusId, metricId, upperBound, lowerBound, upperBoundOperator,
            lowerBoundOperator, isRange, value)
        {
            _metricStatus = metricStatus;
            _metric = metric;
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
            
            ValidateIsRange();
            ValidateOperators();
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
                    DomainExceptionCode.Throw(DomainError.D030, this, Metric);
                }

                if (LowerBound == null || UpperBound == null ||
                    LowerBoundOperator == null || UpperBoundOperator == null)
                {
                    DomainExceptionCode.Throw(DomainError.D032, this, Metric);
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
                    DomainExceptionCode.Throw(DomainError.D033, this);
                    break;
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