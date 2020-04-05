using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ProjectHealthReport.Domains.Helpers;

namespace ProjectHealthReport.Domains.Domains
{
    public class Metric
    {
        public Metric()
        {
            Thresholds = new HashSet<Threshold>();
            DoDReports = new HashSet<DoDReport>();
        }

        public Metric(int id, string name, string valueType, string unit, string tool, string selectValues, int order,
            int toolOrder) : this()
        {
            Id = id;
            Name = name;
            ValueType = valueType;
            Unit = unit;
            Tool = tool;
            SelectValues = selectValues;
            Order = order;
            ToolOrder = toolOrder;

            ValueTypeMustBeValid();
            ValueTypeIsSelect_SelectValueMustHaveValue();
        }

        public Metric(int id, string name, string valueType, string unit, string tool, string selectValues, int order,
            int toolOrder, ICollection<Threshold> thresholds, ICollection<DoDReport> doDReports) : this(id, name,
            valueType, unit, tool, selectValues, order, toolOrder)
        {
            Thresholds = thresholds ?? Thresholds;
            DoDReports = doDReports ?? DoDReports;

            ValidateThresholds();
        }

        public int Id { get; private set; }
        [Required] public string Name { get; private set; }
        [Required] public string ValueType { get; private set; }
        public string Unit { get; private set; }
        [Required] public string Tool { get; private set; }
        public string SelectValues { get; private set; }
        public int Order { get; private set; }
        public int ToolOrder { get; private set; }
        public ICollection<Threshold> Thresholds { get; private set; }
        public ICollection<DoDReport> DoDReports { get; private set; }

        public void AddThresholds(List<Threshold> thresholds)
        {
            foreach (var threshold in thresholds)
            {
                threshold.SetMetricId(Id);
                Thresholds.Add(threshold);
            }

            ValidateThresholds();
        }

        public void ValueTypeMustBeValid()
        {
            var validValueTypes = new[]
                {DoDHelper.ValueTypeText, DoDHelper.ValueTypeNumber, DoDHelper.ValueTypeSelect};
            if (!validValueTypes.Contains(ValueType))
            {
                throw new ValidationException(
                    $"Metric value types must be: {DoDHelper.ValueTypeText}, {DoDHelper.ValueTypeNumber} or {DoDHelper.ValueTypeSelect} - metric name: {Name} - tool: {Tool}");
            }
        }

        public void ValueTypeIsSelect_SelectValueMustHaveValue()
        {
            if (ValueType == DoDHelper.ValueTypeSelect && string.IsNullOrEmpty(SelectValues))
            {
                throw new ValidationException(
                    $"If Metric Value Type is Select, Select Values must have values - metric name: {Name} - tool: {Tool}");
            }
        }

        public void ValidateThresholds()
        {
            if (Thresholds.Count > 3)
            {
                throw new ValidationException("Can be maximum 3 thresholds");
            }

            foreach (var threshold in Thresholds)
            {
                switch (ValueType, threshold.IsRange)
                {
                    case (DoDHelper.ValueTypeNumber, false):
                        throw new ValidationException("If metric value type is Number, threshold must be in range");
                    
                    case (DoDHelper.ValueTypeText, true):
                        throw new ValidationException("If metric value type is Text, threshold cannot be in range");
                    
                    case (DoDHelper.ValueTypeSelect, true):
                        throw new ValidationException("If metric value type is Select, threshold cannot be in range");
                }
            }
        }
    }

    public class MetricStatus
    {
        public MetricStatus()
        {
            Thresholds = new HashSet<Threshold>();
        }

        public int Id { get; set; }
        [Required] public string Name { get; set; }
        public ICollection<Threshold> Thresholds { get; set; }
    }
}