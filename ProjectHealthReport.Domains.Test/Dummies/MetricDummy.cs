﻿using ProjectHealthReport.Domains.Domains;

 namespace TestProject1.Dummies
{
    public class MetricDummy : Metric
    {
        public void SetValueType(string valueType)
        {
            _valueType = valueType;
        }

        public void SetSelectValues(string selectValues)
        {
            _selectValues = selectValues;
        }
    }

    public class ProjectDummy : Project
    {
        public void SetDoDRequired(bool dodRequired)
        {
            _dodRequired = dodRequired;
        }
    }
}