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
            public void HasNoFileNameAndOrReportLink_ThrowD016(string fileName, string link)
            {
                this._reportFileName = fileName;
                this._linkToReport = link;

                Action action = this.ReportFileMustHaveBothLinkAndFileName;

                action.Should().ThrowExactly<DomainException>().And.Code.Should().Be(DomainError.D016.ToString());
            }
            
            [Theory]
            [InlineData("test", "test")]
            [InlineData(null, null)]
            public void LinkAndNameBothNullOrBothExist_PassTest(string fileName, string link)
            {
                this._reportFileName = fileName;
                this._linkToReport = link;

                Action action = this.ReportFileMustHaveBothLinkAndFileName;

                action.Should().NotThrow();
            }

            [Theory]
            [InlineData("")]
            [InlineData("fake url")]
            [InlineData("google.com")]
            public void LinkExist_And_NotAnUrl_ThrowD017(string link)
            {
                this._linkToReport = link;

                Action action = this.ValidateLink;

                action.Should().ThrowExactly<DomainException>().And.Code.Should().Be(DomainError.D017.ToString());
            }
            
            [Theory]
            [InlineData("http://google.com")]
            public void LinkExist_And_CorrectUrl_PassTest(string link)
            {
                this._linkToReport = link;

                Action action = this.ValidateLink;

                action.Should().NotThrow();
            }
            
            [Theory]
            [InlineData(null)]
            public void LinkNotExist_PassTest(string link)
            {
                this._linkToReport = link;

                Action action = this.ValidateLink;

                action.Should().NotThrow();
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
                [InlineData("")]
                [InlineData("0")]
                [InlineData("1")]
                [InlineData("12")]
                public void IsNumber_And_ValueIsNumber_PassTest(string value)
                {
                    _value = value;

                    var metric = new MetricDummy();
                    metric.SetValueType(DoDHelper.ValueTypeNumber);
                    _metric = metric;

                    Action action = ValidateMetric;

                    action.Should().NotThrow();
                }

                [Theory]
                [InlineData("abc")]
                [InlineData(null)]
                public void IsNumber_And_ValueIsNotNumber_ThrowD018(string value)
                {
                    _value = value;

                    var metric = new MetricDummy();
                    metric.SetValueType(DoDHelper.ValueTypeNumber);
                    _metric = metric;

                    Action action = ValidateMetric;

                    action.Should().Throw<DomainException>().And.Code.Should().Be(DomainError.D018.ToString());
                }

                [Theory]
                [InlineData("")]
                [InlineData("yes")]
                [InlineData("no")]
                [InlineData("maybe")]
                public void IsSelect_And_ValueIsInSelectValues_PassTest(string value)
                {
                    _value = value;

                    var metric = new MetricDummy();
                    metric.SetValueType(DoDHelper.ValueTypeSelect);
                    metric.SetSelectValues("yes;no;maybe");
                    _metric = metric;

                    Action action = ValidateMetric;

                    action.Should().NotThrow();
                }

                [Theory]
                [InlineData(null)]
                [InlineData("abc")]
                public void IsSelect_And_ValueIsNotInSelectValues_ThrowD019(string value)
                {
                    _value = value;

                    var metric = new MetricDummy();
                    metric.SetValueType(DoDHelper.ValueTypeSelect);
                    metric.SetSelectValues("yes;no;maybe");
                    _metric = metric;

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
                var project = new ProjectDummy();
                project.SetDoDRequired(false);
                _project = project;

                Action action = ValidateProject;

                action.Should().Throw<DomainException>().And.Code.Should().Be(DomainError.D020.ToString());
            }
        }
    }
}