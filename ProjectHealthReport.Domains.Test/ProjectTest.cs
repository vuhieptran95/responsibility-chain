using System;
using System.Collections.Generic;
using ProjectHealthReport.Domains.Domains;
using FluentAssertions;
using ProjectHealthReport.Domains.Exceptions;
using Xunit;

namespace TestProject1
{
    public class ProjectTest : Project
    {
        private readonly Project _project;

        public ProjectTest()
        {
            _project = new Project();
        }

        public new class Code : ProjectTest
        {
            [Theory]
            [InlineData("ABC")]
            [InlineData("AB1")]
            [InlineData("123")]
            public void MustBeExact3Characters_UpperCase_OnlyLettersAndNumbers(string code)
            {
                _code = code;

                Action action = ValidateProjectCode;

                action.Should().NotThrow();
            }

            [Theory]
            [InlineData("AB 1")]
            [InlineData("AB.")]
            [InlineData("aBC")]
            [InlineData("12.")]
            [InlineData("12@")]
            [InlineData("AB@")]
            public void ElseThrowDomainEx_D001(string code)
            {
                _code = code;

                Action action = ValidateProjectCode;

                action.Should().ThrowExactly<DomainException>().And.Code.Should().Be(DomainError.D001.ToString());
            }
        }

        public new class ProjectEndDate : ProjectTest
        {
            [Fact]
            public void IfHasValue_MustBeGreaterThanProjectStartDate()
            {
                _projectEndDate = DateTime.Now.AddDays(1);
                _projectStartDate = DateTime.Now;

                Action action = ValidateProjectEndDate;
                
                action.Should().NotThrow();
            }
            
            [Fact]
            public void CanBeNull()
            {
                _projectEndDate = null;

                Action action = ValidateProjectEndDate;
                
                action.Should().NotThrow();
            }
            
            [Fact]
            public void ElseThrowDomainEx_D007()
            {
                _projectEndDate = new DateTime(2020,12,11);
                _projectStartDate = new DateTime(2020,12,12);

                Action action = ValidateProjectEndDate;
                
                action.Should().ThrowExactly<DomainException>().Which.Code.Should().Be(DomainError.D007.ToString());
            }
        }
        
        public new class PhrRequired: ProjectTest
        {
            [Fact]
            public void IfTrue_PhrRequiredFromHasNoValue_ThrowDomainEx_D008()
            {
                _phrRequired = true;
                _phrRequiredFrom = null;
                _deliveryResponsibleName = "Test";

                Action action = ValidatePhrRequired;
                
                action.Should().ThrowExactly<DomainException>().And.Code.Should()
                    .Be(DomainError.D008.ToString());
            }
            
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            public void IfTrue_DeliveryResponsibleNameHasNoValue_ThrowDomainEx_D013(string pic)
            {
                _phrRequired = true;
                _phrRequiredFrom = DateTime.Now;
                _deliveryResponsibleName = pic;

                Action action = ValidatePhrRequired;
                
                action.Should().ThrowExactly<DomainException>().And.Code.Should()
                    .Be(DomainError.D013.ToString());
            }

            [Fact]
            public void IfFalse_DoDRequiredIsTrue_ThrowDomainEx_D011()
            {
                _phrRequired = false;
                _dodRequired = true;
                _phrRequiredFrom = DateTime.Now;
                _deliveryResponsibleName = "Test";

                Action action = ValidatePhrRequired;
                action.Should().ThrowExactly<DomainException>().And.Code.Should()
                    .Be(DomainError.D011.ToString());
            }
            
            [Fact]
            public void IfFalse_DoDRequiredIsFalse_PassValidate()
            {
                _phrRequired = false;
                _dodRequired = false;

                Action action = ValidatePhrRequired;
                action.Should().NotThrow();
            }
        }
        
        public new class Links : ProjectTest
        {
            public class JiraLink: Links
            {
                [Fact]
                public void IsNull_PassValidate()
                {
                    _jiraLink = null;
                    _sourceCodeLink = null;

                    Action action = ValidateLinkProperties;
                    
                    action.Should().NotThrow();
                }

                [Theory]
                [InlineData("http://google.com")]
                [InlineData("http://google.com/abcd/abcd?asfd=12")]
                [InlineData("https://google.com")]
                public void HasValue_ValueMustBeALink(string link)
                {
                    _jiraLink = link;
                    _sourceCodeLink = null;

                    Action action = ValidateLinkProperties;
                    
                    action.Should().NotThrow();
                }

                [Theory]
                [InlineData("asf sda fsf ")]
                [InlineData("google.com")]
                [InlineData("google")]
                public void HasValue_ButNotALink_ThrowDomainEx_D005(string link)
                {
                    _jiraLink = link;
                    _sourceCodeLink = null;

                    Action action = ValidateLinkProperties;
                    
                    action.Should().ThrowExactly<DomainException>().And.Code.Should().Be(DomainError.D005.ToString());
                }
            }
            
            public new class SourceCodeLink: Links
            {
                [Fact]
                public void IsNull_PassValidate()
                {
                    _jiraLink = null;
                    _sourceCodeLink = null;

                    Action action = ValidateLinkProperties;
                    
                    action.Should().NotThrow();
                }

                [Theory]
                [InlineData("http://google.com")]
                [InlineData("http://google.com/abcd/abcd?asfd=12")]
                [InlineData("https://google.com")]
                public void HasValue_ValueMustBeALink(string link)
                {
                    _jiraLink = null;
                    _sourceCodeLink = link;

                    Action action = ValidateLinkProperties;
                    
                    action.Should().NotThrow();
                }

                [Theory]
                [InlineData("asf sda fsf ")]
                [InlineData("google.com")]
                [InlineData("google")]
                public void HasValue_ButNotALink_ThrowDomainEx_D006(string link)
                {
                    _jiraLink = null;
                    _sourceCodeLink = link;

                    Action action = ValidateLinkProperties;
                    
                    action.Should().ThrowExactly<DomainException>().And.Code.Should().Be(DomainError.D006.ToString());
                }
            }
        }

        public new class DmrRequired : ProjectTest
        {
            [Fact]
            public void IsFalse_PassValidate()
            {
                _dmrRequired = false;

                Action action = ValidateDmrRequired;
                
                action.Should().NotThrow();
            }
            [Fact]
            public void IsTrue_DmrRequiredFromMustHaveValue()
            {
                _dmrRequired = true;
                _dmrRequiredFrom = DateTime.Now;

                Action action = ValidateDmrRequired;
                
                action.Should().NotThrow();
            }
            
            [Fact]
            public void IsTrue_DmrRequiredFromNotHaveValue_ThrowDomainEx_D009()
            {
                _dmrRequired = true;
                _dmrRequiredFrom = null;

                Action action = ValidateDmrRequired;
                
                action.Should().ThrowExactly<DomainException>().And.Code.Should().Be(DomainError.D009.ToString());
            }
            
            [Fact]
            public void IsTrue_DmrRequiredFromHasValue_DmrRequiredToHasValue_FromGreaterThanTo_ThrowDomainEx_D010()
            {
                _dmrRequired = true;
                _dmrRequiredFrom = DateTime.Now.AddDays(1);
                _dmrRequiredTo = DateTime.Now;

                Action action = ValidateDmrRequired;
                
                action.Should().ThrowExactly<DomainException>().And.Code.Should().Be(DomainError.D010.ToString());
            }
        }
        
        public new class SetCollection : ProjectTest
        {
            [Fact]
            public void SetValidCollections_RespectivePropertiesHaveValidCollection()
            {
                var statuses = new List<Status>();
                var qualityReports = new List<QualityReport>();
                var backlogItems = new List<BacklogItem>();

                Action action = () => this.SetCollections(qualityReports, backlogItems, statuses);

                _qualityReports.Should().BeEquivalentTo(qualityReports);
                _backlogItems.Should().BeEquivalentTo(backlogItems);
                _statuses.Should().BeEquivalentTo(statuses);
            }
        }
    }
}