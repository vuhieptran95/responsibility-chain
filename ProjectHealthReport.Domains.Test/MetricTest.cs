using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Exceptions;
using ProjectHealthReport.Domains.Helpers;
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
                
            }
            
        }
    }
}