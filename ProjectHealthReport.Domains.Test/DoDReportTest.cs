﻿using System;
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
    public class DoDReportTest : DoDReport
    {
        public class ReportFile : DoDReportTest
        {
            [Theory]
            [InlineData("", "")]
            [InlineData("test", "")]
            [InlineData("", "test")]
            [InlineData("test", null)]
            [InlineData(null, "test")]
            public void IfExist_And_HasNoFileNameOrReportLink_ThrowD016(string fileName, string link)
            {
                this._reportFileName = fileName;
                this._linkToReport = link;

                Action action = this.ReportFileMustHaveBothLinkAndFileName;

                action.Should().ThrowExactly<DomainException>().And.Code.Should().Be(DomainError.D016.ToString());
            }

            [Theory]
            [InlineData("")]
            [InlineData(null)]
            [InlineData("fake url")]
            [InlineData("google.com")]
            public void LinkExist_And_NotAnUrl_ThrowD017(string link)
            {
                this._linkToReport = link;

                Action action = this.ValidateLink;

                action.Should().ThrowExactly<DomainException>().And.Code.Should().Be(DomainError.D017.ToString());
            }
        }

        public new class Metric : DoDReportTest
        {
            [Fact]
            public void NotExist_ThrowD034()
            {
                Action action = ValidateMetric;

                action.Should().Throw<DomainException>().And.Code.Should().Be(DomainError.D034.ToString());
            }

            public class ValueType : DoDReportTest
            {
                [Theory]
                [InlineData("0")]
                [InlineData("1")]
                [InlineData("12")]
                public void IsNumber_And_ValueIsNumber_PassTest(string value)
                {
                    _value = value;

                    _metric = new ProjectHealthReport.Domains.Domains.Metric();
                    _metric.GetType().GetField("_valueType", BindingFlags.NonPublic | BindingFlags.Instance)
                        .SetValue(_metric, DoDHelper.ValueTypeNumber);

                    Action action = ValidateMetric;

                    action.Should().NotThrow();
                }

                [Theory]
                [InlineData("")]
                [InlineData("abc")]
                [InlineData(null)]
                public void IsNumber_And_ValueIsNotNumber_ThrowD018(string value)
                {
                    _value = value;

                    _metric = new ProjectHealthReport.Domains.Domains.Metric();
                    _metric.GetType().GetField("_valueType", BindingFlags.NonPublic | BindingFlags.Instance)
                        .SetValue(_metric, DoDHelper.ValueTypeNumber);

                    Action action = ValidateMetric;

                    action.Should().Throw<DomainException>().And.Code.Should().Be(DomainError.D018.ToString());
                }

                [Theory]
                [InlineData("yes")]
                [InlineData("no")]
                [InlineData("maybe")]
                public void IsSelect_And_ValueIsInSelectValues_PassTest(string value)
                {
                    _value = value;

                    _metric = new ProjectHealthReport.Domains.Domains.Metric();
                    _metric.GetType().GetField("_valueType", BindingFlags.NonPublic | BindingFlags.Instance)
                        .SetValue(_metric, DoDHelper.ValueTypeSelect);
                    _metric.GetType().GetField("_selectValues", BindingFlags.NonPublic | BindingFlags.Instance)
                        .SetValue(_metric, "yes;no;maybe");

                    Action action = ValidateMetric;

                    action.Should().NotThrow();
                }

                [Theory]
                [InlineData("")]
                [InlineData(null)]
                [InlineData("abc")]
                public void IsSelect_And_ValueIsNotInSelectValues_ThrowD019(string value)
                {
                    _value = value;

                    _metric = new ProjectHealthReport.Domains.Domains.Metric();
                    _metric.GetType().GetField("_valueType", BindingFlags.NonPublic | BindingFlags.Instance)
                        .SetValue(_metric, DoDHelper.ValueTypeSelect);
                    _metric.GetType().GetField("_selectValues", BindingFlags.NonPublic | BindingFlags.Instance)
                        .SetValue(_metric, "yes;no;maybe");

                    Action action = ValidateMetric;

                    action.Should().Throw<DomainException>().And.Code.Should().Be(DomainError.D019.ToString());
                }
            }
        }

        public new class Project : DoDReportTest
        {
            [Fact]
            public void NotExist_ThrowD035()
            {
                Action action = ValidateProject;

                action.Should().Throw<DomainException>().And.Code.Should().Be(DomainError.D035.ToString());
            }

            [Fact]
            public void IsNotDoDRequired_CannotBeInserted_ThrowD020()
            {
                _project = new ProjectHealthReport.Domains.Domains.Project();
                _project.GetType().GetField("_dodRequired", BindingFlags.NonPublic | BindingFlags.Instance)
                    .SetValue(_project, false);

                Action action = ValidateProject;

                action.Should().Throw<DomainException>().And.Code.Should().Be(DomainError.D020.ToString());
            }
        }
    }
}