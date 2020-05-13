using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Exceptions;
using ProjectHealthReport.Domains.Helpers;
using Xunit;

namespace TestProject1
{
    public class ProjectAccessTest : ProjectAccess
    {
        protected List<(string Email, string Role)> UserRoleList;

        public ProjectAccessTest()
        {
            UserRoleList = AuthorizationHelper.UserRoleList;
        }

        
        [Theory]
        [InlineData("fake email")]
        [InlineData(null)]
        [InlineData("")]
        public void Email_MustBeCorrectOrganizationInfo(string email)
        {
            this._email = email;

            Action action = () => this.Validate(UserRoleList);

            action.Should().ThrowExactly<DomainException>().And.Code.Should().Be(DomainError.D014.ToString());
        }

        [Theory]
        [InlineData("fake role")]
        [InlineData(null)]
        [InlineData("")]
        public void Role_MustBePic(string role)
        {
            this._role = role;
            this._email = UserRoleList.First().Email;

            Action action = () => this.Validate(UserRoleList);

            action.Should().ThrowExactly<DomainException>().And.Code.Should().Be(DomainError.D015.ToString());
        }
    }
}