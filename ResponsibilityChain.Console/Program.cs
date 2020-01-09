using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Flurl.Util;

namespace ResponsibilityChain.Console
{
    public class HandlerBranch : Handler<Request, Response>
    {
        public override Task<Response> HandleAsync(Request request)
        {
            return base.HandleAsync(request);
        }
    }
    
    public class HandlerBranch1 : Handler<Request, Response>
    {
        public override Task<Response> HandleAsync(Request request)
        {
            if (request.Role == "COO")
            {
                System.Console.WriteLine("COO and that's all");
                return Task.FromResult<Response>(null);
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
                System.Console.WriteLine("PMO and that's all");
                return Task.FromResult<Response>(null);
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
                System.Console.WriteLine("DM and that's all");
                return Task.FromResult<Response>(null);
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
            System.Console.WriteLine("Doing sth with handler async 1");

            await  _branchHandler.HandleAsync(request);
            
            return await base.HandleAsync(request);
        }
    }
    
    public class Handler2 : Handler<Request, Response>
    {
        public override Task<Response> HandleAsync(Request request)
        {
            System.Console.WriteLine("Doing sth with handler async 2");
            return base.HandleAsync(request);
        }
    }
    
    public class Handler21 : Handler<Request, Response>
    {
        public override Task<Response> HandleAsync(Request request)
        {
            System.Console.WriteLine("Doing sth with handler async 21");
            var result = base.HandleAsync(request);
            System.Console.WriteLine("Logging sth");    
            
            return result;
        }
    }
    
    public class Handler22 : Handler<Request, Response>
    {
        public override Task<Response> HandleAsync(Request request)
        {
            System.Console.WriteLine("Doing sth with handler async 22");
            return base.HandleAsync(request);
        }
    }
    
    public class Handler3 : Handler<Request, Response>
    {
        public override async Task<Response> HandleAsync(Request request)
        {
            var todo = await "https://jsonplaceholder.typicode.com/posts/1".GetStringAsync();;
            var response = new Response {ResponseTodo = todo}; 
                
            return response;
        }
    }
    
    
    class Program
    {
        static async Task Main(string[] args)
        {
            var handlerBranch1 = new HandlerBranch1();
            var handlerBranch2 = new HandlerBranch2();
            var handlerBranch3 = new HandlerBranch3();
            var handlerBranchException = new HandlerBranchException();
            var handlerBranch = new HandlerBranch();
            handlerBranch.AddHandler(handlerBranch1);
            handlerBranch.AddHandler(handlerBranch2);
            handlerBranch.AddHandler(handlerBranch3);
            handlerBranch.AddHandler(handlerBranchException);
            
            var handler1 = new Handler1(handlerBranch);
            var handler2 = new Handler2();
            var handler21 = new Handler21();
            var handler22 = new Handler22();
            var handler3 = new Handler3();

            handler2.AddHandler(handler21);
            handler2.AddHandler(handler22);
            
            handler1.AddHandler(handler2);
            handler1.AddHandler(handler3);

            var resultAsync = await handler1.HandleAsync(new Request{Role = "DM"});
            
            System.Console.WriteLine(resultAsync.ResponseTodo);
            System.Console.WriteLine("Hello World!");
        }
    }
}