using System;
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
    }
}