using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ProjectHealthReport.Domains.Exceptions;
using ProjectHealthReport.Domains.Helpers;

namespace ProjectHealthReport.Domains.Domains
{
    public class Metric
    {
        private int _id;
        private string _name;
        private string _valueType;
        private string _unit;
        private string _tool;
        private string _selectValues;
        private int _order;
        private int _toolOrder;
        private ICollection<Threshold> _thresholds;
        private ICollection<DoDReport> _doDReports;

        public Metric()
        {
            _thresholds = new HashSet<Threshold>();
            _doDReports = new HashSet<DoDReport>();
        }

        public Metric(int id, string name, string valueType, string unit, string tool, string selectValues, int order,
            int toolOrder) : this()
        {
            _id = id;
            _name = name;
            _valueType = valueType;
            _unit = unit;
            _tool = tool;
            _selectValues = selectValues;
            _order = order;
            _toolOrder = toolOrder;

            ValueTypeMustBeValid();
            ValueTypeIsSelect_SelectValueMustHaveValue();
        }

        public Metric(int id, string name, string valueType, string unit, string tool, string selectValues, int order,
            int toolOrder, ICollection<Threshold> thresholds, ICollection<DoDReport> doDReports) : this(id, name,
            valueType, unit, tool, selectValues, order, toolOrder)
        {
            _thresholds = thresholds;
            _doDReports = doDReports;

            ValidateThresholds();
        }

        public int Id => _id;

        public string Name => _name;

        public string ValueType => _valueType;

        public string Unit => _unit;

        public string Tool => _tool;

        public string SelectValues => _selectValues;

        public int Order => _order;

        public int ToolOrder => _toolOrder;

        public IEnumerable<Threshold> Thresholds => _thresholds;

        public IEnumerable<DoDReport> DoDReports => _doDReports;

        public void UpdateValue(int id, string name, string valueType, string unit, string tool, string selectValues,
            int order,
            int toolOrder)
        {
            _id = id;
            _name = name;
            _valueType = valueType;
            _unit = unit;
            _tool = tool;
            _selectValues = selectValues;
            _order = order;
            _toolOrder = toolOrder;

            ValueTypeMustBeValid();
            ValueTypeIsSelect_SelectValueMustHaveValue();
        }

        public void ReplaceThresholds(List<Threshold> thresholds)
        {
            var listMetricStatusIds = thresholds.Select(t => t.MetricStatusId).ToList();
            var listDistinctIds = listMetricStatusIds.Distinct().ToList();
            if (!listDistinctIds.SequenceEqual(listMetricStatusIds))
            {
                DomainExceptionCode.Throw(DomainError.D023, this, Thresholds);
            }
            
            if (thresholds.Any(t => t.MetricId > 0 && t.MetricId != Id))
            {
                DomainExceptionCode.Throw(DomainError.D022, this, thresholds);
            }

            var listRemove = Thresholds.Except(thresholds, Threshold.ThresholdComparer).ToList();
            var listAddNew = thresholds.Except(Thresholds, Threshold.ThresholdComparer).ToList();
            var listUpdate = thresholds.Intersect(Thresholds, Threshold.ThresholdComparer).ToList();

            listRemove.ForEach(i => _thresholds.Remove(i));

            listAddNew.ForEach(i => _thresholds.Add(i));

            listUpdate.ForEach(i =>
            {
                var item = Thresholds.First(t => t.MetricId == i.MetricId && t.MetricStatusId == i.MetricStatusId);
                item.UpdateValue(item.MetricStatusId, item.MetricId, i.UpperBound, i.LowerBound, i.UpperBoundOperator,
                    i.LowerBoundOperator, i.IsRange, i.Value);
            });

            ValidateThresholds();
        }

        public void ValueTypeMustBeValid()
        {
            var validValueTypes = new[]
                {DoDHelper.ValueTypeText, DoDHelper.ValueTypeNumber, DoDHelper.ValueTypeSelect};
            if (!validValueTypes.Contains(ValueType))
            {
                DomainExceptionCode.Throw(DomainError.D024, this);
            }
        }

        public void ValueTypeIsSelect_SelectValueMustHaveValue()
        {
            if (ValueType == DoDHelper.ValueTypeSelect && string.IsNullOrEmpty(SelectValues))
            {
                DomainExceptionCode.Throw(DomainError.D029, this);
            }
        }

        public void ValidateThresholds()
        {
            if (Thresholds.Count() > 3)
            {
                DomainExceptionCode.Throw(DomainError.D025, this, Thresholds);
            }

            foreach (var threshold in Thresholds)
            {
                switch (ValueType, threshold.IsRange)
                {
                    case (DoDHelper.ValueTypeNumber, false):
                        DomainExceptionCode.Throw(DomainError.D026, this, Thresholds);
                        break;

                    case (DoDHelper.ValueTypeText, true):
                        DomainExceptionCode.Throw(DomainError.D027, this, Thresholds);
                        break;

                    case (DoDHelper.ValueTypeSelect, true):
                        DomainExceptionCode.Throw(DomainError.D028, this, Thresholds);
                        break;
                }
            }
        }
    }
}