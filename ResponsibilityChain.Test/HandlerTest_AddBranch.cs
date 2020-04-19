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
        private readonly Handler<Request, Response> _handler;
        private readonly Handler<Request, Response> _handler2;
        private readonly Handler<Request, Response> _handler3;
        private readonly Handler<Request, Response> _default;
        private readonly Request _request;
        private readonly Response _response;

        public HandlerTest()
        {
            _handler = new Handler<Request, Response>();
            _handler2 = new Handler<Request, Response>();
            _handler3 = new Handler<Request, Response>();
            _default = new DefaultHandler<Request, Response>();
            _request = new Request();
            _response = new Response();
        }
        public class AddBranch: HandlerTest
        {
            public class GivenNullHandler : AddBranch
            {
                [Fact]
                public void ThenThrowNullHandlerEx()
                {
                    Action action = () => _handler.AddBranch(null);

                    action.Should().ThrowExactly<NullHandlerException>();
                }
            }
            
            public class WithNullBranch : AddBranch
            {
                public class Given1NodeHandler : WithNullBranch
                {
                    [Fact]
                    public void ThenNextIsNull()
                    {
                        _handler.AddBranch(_handler2);

                        _handler.Branch.Next.Should().BeNull();
                    }

                    [Fact]
                    public void ThenBranchIsHandler()
                    {
                        _handler.AddBranch(_handler2);

                        _handler.Branch.Should().Be(_handler2);
                    }
                }

                public class Given2NodesHandler : WithNullBranch
                {
                    [Fact]
                    public void ThenLastNextIsNull()
                    {
                        _handler2.Next = _handler3;
                        
                        _handler.AddBranch(_handler2);

                        _handler.Branch.Next.Next.Should().BeNull();
                    }

                    [Fact]
                    public void ThenBranchIsHandler()
                    {
                        _handler2.Next = _handler3;
                        _handler.AddBranch(_handler2);

                        _handler.Branch.Should().Be(_handler2);
                    }
                }
            }
            
            public class WithNotNullBranch : AddBranch
            {
                public class Given1NodeHandler : WithNotNullBranch
                {
                    [Fact]
                    public void HandlerAddedToTailOfBranch()
                    {
                        _handler.Branch = new Handler<Request, Response>();
                        
                        _handler.AddBranch(_handler2);

                        _handler.Branch.Next.Should().Be(_handler2);
                    }
                }
                
                public class Given2NodesHandler : WithNotNullBranch
                {
                    [Fact]
                    public void HandlerWith2NodesAddedToTailOfBranch()
                    {
                        _handler.Branch = new Handler<Request, Response>();
                        _handler2.Next = _handler3;
                        
                        _handler.AddBranch(_handler2);

                        _handler.Branch.Next.Should().Be(_handler2);
                        _handler.Branch.Next.Next.Should().Be(_handler3);
                    }
                }
            }
        } 
    }
}