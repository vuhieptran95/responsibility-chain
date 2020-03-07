using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Xunit;

namespace ResponsibilityChain.Test
{
    public class HandlerTest
    {
        public class SequenceHandlers
        {
            private readonly Handler<Request, Response> _handler2;
            public SequenceHandlers()
            {
                var handler3 = new Handler3();
                _handler2 = new Handler<Request, Response>();
                _handler2.AddHandler(handler3);
            }

            [Fact]
            public void AddNewHandlerToHandlerWithNextNull_NextBecomeNewHandler()
            {
                var handler = new Handler<Request,Response>();
                var handler3 = new Handler3();
                
                handler.AddHandler(handler3);

                handler.Next.Should().BeEquivalentTo(handler3);
            }
            
            [Fact]
            public void AddNewHandlerToHandlerWithNextNotNull_NewHandlerAddedToTail()
            {
                var handler = new Handler<Request,Response>();
                var handler21 = new Handler21();
                var handler3 = new Handler3();
                
                handler.AddHandler(handler21);
                handler.AddHandler(handler3);

                handler.Next.Next.Should().BeEquivalentTo(handler3);
            }

            [Fact]
            public void WithHandlerNull_ThrowNullHandlerException()
            {
                var handler1 = new Handler<Request, Response>();

                Func<Task> action = async() => await handler1.HandleAsync(new Request());

                action.Should().ThrowExactly<NullHandlerException>();
            }

            [Fact]
            public async Task WithCorrectHandlers_RunCorrectly()
            {
                var response = await _handler2.HandleAsync(new Request {Role = "COO"});

                var expectedResponse = new Response {Role = "Test", IsCorrect = true};
                
                response.Should().BeEquivalentTo(expectedResponse);
            }
        }

        public class BranchHandlers
        {
            private readonly Handler1 _handler1;

            public BranchHandlers()
            {
                var handlerBranch = new Handler<Request, Response>();
                var handlerBranch1 = new HandlerBranch1();
                var handlerBranch2 = new HandlerBranch2();
                var handler3 = new Handler3();
                
                handlerBranch.AddHandler(handlerBranch1);
                handlerBranch.AddHandler(handlerBranch2);
                
                _handler1 = new Handler1(handlerBranch);
                _handler1.AddHandler(handler3);
                
                
            }

            [Fact]
            public async Task WithCorrectBranchHandlers_RunCorrectly()
            {
                var request = new Request {Role = "COO"};
                await _handler1.HandleAsync(request);

                var expectedRequest = new Request() {Role = "COO", IsCorrect = true};
                request.Should().BeEquivalentTo(expectedRequest);
            }
            
            [Fact]
            public void WithInCorrectBranchHandlers_ThrowUnAuthorizedException()
            {
                var handlerBranch1 = new HandlerBranch1();
                var handlerBranch2 = new HandlerBranch2();
                var handlerBranchException = new HandlerBranchException();
                var handlerBranchThrowException = new Handler<Request,Response>();
                handlerBranchThrowException.AddHandler(handlerBranch1);
                handlerBranchThrowException.AddHandler(handlerBranch2);
                handlerBranchThrowException.AddHandler(handlerBranchException);

                var handler3 = new Handler3();
                var handler1 = new Handler1(handlerBranchThrowException);
                handler1.AddHandler(handler3);
                
                Func<Task> action = async () => await handler1.HandleAsync(new Request());

                action.Should().ThrowExactly<UnauthorizedAccessException>();
            }
        }

        public class HandlerBranch1 : Handler<Request, Response>
        {
            public override Task<Response> HandleAsync(Request request)
            {
                if (request.Role == "COO")
                {
                    request.IsCorrect = true;
                    var response = new Response() {IsCorrect = true, Role = request.Role};
                    return Task.FromResult<Response>(response);
                }

                return base.HandleAsync(request);
            }
        }

        public class HandlerBranch2 : Handler<Request, Response>
        {
            public override Task<Response> HandleAsync(Request request)
            {
                if (request.Role == "PMO")
                {
                    request.IsCorrect = true;
                    var response = new Response() {IsCorrect = true, Role = request.Role};
                    return Task.FromResult<Response>(response);
                }

                return base.HandleAsync(request);
            }
        }

        public class HandlerBranch3 : Handler<Request, Response>
        {
            public override Task<Response> HandleAsync(Request request)
            {
                if (request.Role == "DM")
                {
                    request.IsCorrect = true;
                    var response = new Response() {IsCorrect = true, Role = request.Role};
                    return Task.FromResult<Response>(response);
                }

                return base.HandleAsync(request);
            }
        }

        public class HandlerBranchException : Handler<Request, Response>
        {
            public override Task<Response> HandleAsync(Request request)
            {
                throw new UnauthorizedAccessException("Unauthorized roles");
            }
        }

        public class Handler1 : Handler<Request, Response>
        {
            private readonly Handler<Request, Response> _branchHandler;

            public Handler1(Handler<Request, Response> branchHandler)
            {
                _branchHandler = branchHandler;
            }

            public override async Task<Response> HandleAsync(Request request)
            {
                await _branchHandler.HandleAsync(request);

                return await base.HandleAsync(request);
            }
        }


        public class Handler21 : Handler<Request, Response>
        {
            public override Task<Response> HandleAsync(Request request)
            {
                var result = base.HandleAsync(request);

                return result;
            }
        }

        public class Handler3 : Handler<Request, Response>
        {
            public override async Task<Response> HandleAsync(Request request)
            {
                var response = new Response{Role = "Test", IsCorrect = true};

                return response;
            }
        }
    }


    public class Request
    {
        public string Role { get; set; }
        public bool IsCorrect { get; set; } = false;
    }

    public class Response
    {
        public string Role { get; set; }
        public bool IsCorrect { get; set; } = false;
    }
}