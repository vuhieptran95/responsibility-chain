using System;
using System.Collections.Generic;
using FluentAssertions;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Exceptions;
using Xunit;

namespace TestProject1
{
    public class ThresholdTest : Threshold
    {
        [Theory]
        [InlineData("test value")]
        [InlineData("   test    ")]
        public void IsRange_ValueIsNotNullOrEmpty_ThrowD030(string value)
        {
            _value = value;
            _isRange = true;

            Action action = ValidateIsRange;

            action.Should().Throw<DomainException>().And.Code.Should().Be(DomainError.D030.ToString());
        }

        [Theory]
        [InlineData(null, null, null, null)]
        [InlineData(null, "", "", null)]
        public void IsRange_BoundsAndOperatorsNotFullySpecified_ThrowD032(decimal? lowerBound, string lowerBoundOperator,
            string upperBoundOperator, decimal? upperBound)
        {
            _isRange = true;
            
            _lowerBound = lowerBound;
            _lowerBoundOperator = lowerBoundOperator;
            _upperBoundOperator = upperBoundOperator;
            _upperBound = upperBound;

            Action action = ValidateIsRange;
            
            action.Should().Throw<DomainException>().And.Code.Should().Be(DomainError.D032.ToString());
        }

        [Theory]
        [InlineData("","")]
        [InlineData(null,"")]
        [InlineData("",null)]
        [InlineData("<",">")]
        [InlineData("<=",">=")]
        [InlineData(">","<")]
        [InlineData(">=","<=")]
        public void InvalidOperators_ThrowD033(string lowerBoundOperator, string upperBoundOperator)
        {
            _isRange = true;
            _lowerBoundOperator = lowerBoundOperator;
            _upperBoundOperator = upperBoundOperator;

            Action action = ValidateOperators;
            
            action.Should().Throw<DomainException>().And.Code.Should().Be(DomainError.D033.ToString());
        }
    }
}