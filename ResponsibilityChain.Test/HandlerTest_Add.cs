using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Xunit;

namespace ResponsibilityChain.Test
{
    public partial class HandlerTest
    {
        public class Add : HandlerTest
        {
            public class GivenNullHandler : Add
            {
                [Fact]
                public void ThenThrowNullHandlerEx()
                {
                    Action action = () => _handler.Add(null);

                    action.Should().ThrowExactly<NullHandlerException>();
                }
            }
            
            public class GivenNotNullHandler : Add
            {
                [Fact]
                public void ThenNextIsHandler()
                {
                    _handler.Add(_handler2);

                    _handler.Next.Should().Be(_handler2);
                }
            }
        }
    }
    
    public partial class HandlerTest
    {
        public class DefaultHandler : HandlerTest
        {
            [Fact]
            public void GivenRequest_ReturnTaskCompleted()
            {
                var res =_default.HandleAsync(_request);

                res.Should().Be(Task.CompletedTask);
            }
        }
        
        public class HandleAsync: HandlerTest
        {
            [Fact]
            public void GivenNullRequest_ThrowNullReferenceEx()
            {
                Func<Task> action = async () => await _handler.HandleAsync(null);

                action.Should().ThrowExactly<NullReferenceException>();
            }
            
            [Fact]
            public void GivenARequestAndHandlerNextNull_ThrowNullReferenceEx()
            {
                Func<Task> action = async () => await _handler.HandleAsync(_request);

                action.Should().ThrowExactly<NullHandlerException>();
            }
            
            [Fact]
            public void GivenARequest_UseNextToHandleRequest()
            {
                _handler.Next = _default;
                Action action = () => _handler.HandleAsync(_request);

                action.Should().NotThrow();
            }
        }
        
        public class HandleBranchAsync: HandlerTest
        {
            [Fact]
            public void GivenNullRequest_ThrowNullReferenceEx()
            {
                Func<Task> action = async () => await _handler.HandleBranchAsync(null);

                action.Should().ThrowExactly<NullReferenceException>();
            }
            
            [Fact]
            public void GivenARequestAndBranchNull_ThrowNothing()
            {
                Func<Task> action = async () => await _handler.HandleBranchAsync(_request);

                action.Should().NotThrow();
            }
            
            [Fact]
            public void GivenARequest_UseBranchToHandleRequest()
            {
                _handler.Branch = _default;
                Func<Task> action = async () =>await _handler.HandleBranchAsync(_request);

                action.Should().NotThrow();
            }
        }
    }
}