using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Exceptions;
using ProjectHealthReport.Domains.Helpers;
using TestProject1.Dummies;
using Xunit;

namespace TestProject1
{
    public class MetricTest : Metric
    {
        public new class ValueType : MetricTest
        {
            [Theory]
            [InlineData("")]
            [InlineData(null)]
            [InlineData("randomvalue")]
            public void IsNotNumberOrTextOrSelect_ThrowD024(string valueType)
            {
                _valueType = valueType;

                Action action = ValueTypeMustBeValid;

                action.Should().ThrowExactly<DomainException>().And.Code.Should().Be(DomainError.D024.ToString());
            }
            
            [Theory]
            [InlineData(DoDHelper.ValueTypeNumber)]
            [InlineData(DoDHelper.ValueTypeSelect)]
            [InlineData(DoDHelper.ValueTypeText)]
            public void IsValid_PassTest(string valueType)
            {
                _valueType = valueType;

                Action action = ValueTypeMustBeValid;

                action.Should().NotThrow();
            }

            [Fact]
            public void IsNumber_ThresholdIsRange_PassTest()
            {
                _valueType = DoDHelper.ValueTypeNumber;
                var threshold = new ThresholdDummy();
                threshold.SetIsRange(true);
                
                _thresholds = new List<Threshold>(){threshold};

                Action action = ValidateThresholds;
                
                action.Should().ThrowExactly<DomainException>().And.Code.Should().Be(DomainError.D026.ToString());
            }
        }
        
        public new class Thresholds: MetricTest
        {
            [Fact]
            public void HaveMoreThan3_ThrowD025()
            {
                _thresholds = new List<Threshold>(){new Threshold(), new Threshold(), new Threshold(), new Threshold()};
            
                Action action = ValidateThresholds;
            
                action.Should().ThrowExactly<DomainException>().And.Code.Should().Be(DomainError.D025.ToString());
            }

            [Fact]
            public void HaveSameMetricStatus_ThrowD023()
            {
                var threshold1 = new ThresholdDummy();
                threshold1.SetMetricStatusId(1);
                var threshold2 = new ThresholdDummy();
                threshold2.SetMetricStatusId(1);
                var threshold3 = new ThresholdDummy();
                threshold3.SetMetricStatusId(2);
            
                _thresholds = new List<Threshold>(){threshold1, threshold2, threshold3};

                Action action = ThresholdMustHaveDifferentMetricStatus;
            
                action.Should().ThrowExactly<DomainException>().And.Code.Should().Be(DomainError.D023.ToString());
            }
        }
        
        

    }
}