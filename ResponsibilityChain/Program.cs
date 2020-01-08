using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Flurl.Util;

namespace ResponsibilityChain
{
    public interface IHandler<TRequest, TResponse>
    {
        Response Handle(TRequest request);
        Task<Response> HandleAsync(TRequest request);
        IHandler<TRequest, TResponse> Next { get; set; }
        IHandler<TRequest, TResponse> AddHandler(IHandler<TRequest, TResponse> handler);
    }

    public class Handler<TRequest, TResponse> : IHandler<TRequest, TResponse>
    {
        public virtual Response Handle(TRequest request)
        {
            return Next?.Handle(request);
        }

        public virtual Task<Response> HandleAsync(TRequest request)
        {
            return Next?.HandleAsync(request);
        }

        public IHandler<TRequest, TResponse> AddHandler(IHandler<TRequest, TResponse> handler)
        {
            if (Next == null)
            {
                Next = handler;
            }
            else
            {
                Next.Next = Next.AddHandler(handler);
            }

            return Next;
        }

        public IHandler<TRequest, TResponse> Next { get; set; }
    }

    public class Handler1 : Handler<Request, Response>
    {
        public override Response Handle(Request request)
        {
            Console.WriteLine("Doing sth with handler 1");
            return base.Handle(request);
        }

        public override Task<Response> HandleAsync(Request request)
        {
            Console.WriteLine("Doing sth with handler async 1");
            return base.HandleAsync(request);
        }
        
        
    }
    
    public class Handler2 : Handler<Request, Response>
    {
        public override Response Handle(Request request)
        {
            Console.WriteLine("Doing sth with handler 1");
            return base.Handle(request);
        }
        
        public override Task<Response> HandleAsync(Request request)
        {
            Console.WriteLine("Doing sth with handler async 2");
            return base.HandleAsync(request);
        }
    }
    
    public class Handler21 : Handler<Request, Response>
    {
        public override Response Handle(Request request)
        {
            Console.WriteLine("Doing sth with handler 1");
            return base.Handle(request);
        }
        public override Task<Response> HandleAsync(Request request)
        {
            Console.WriteLine("Doing sth with handler async 21");
            var result = base.HandleAsync(request);
            Console.WriteLine("Logging sth");    
            
            return result;
        }
    }
    
    public class Handler22 : Handler<Request, Response>
    {
        public override Response Handle(Request request)
        {
            Console.WriteLine("Doing sth with handler 1");
            return base.Handle(request);
        }
        public override Task<Response> HandleAsync(Request request)
        {
            Console.WriteLine("Doing sth with handler async 22");
            return base.HandleAsync(request);
        }
    }
    
    public class Handler3 : Handler<Request, Response>
    {
        public override Response Handle(Request request)
        {
            Console.WriteLine("Doing sth with handler 1");
            return new Response();
        }
        public override async Task<Response> HandleAsync(Request request)
        {
            var todo = await "https://jsonplaceholder.typicode.com/posts".GetStringAsync();;
            var response = new Response {ResponseTodo = todo}; 
                
            return response;
        }
    }
    
    
    class Program
    {
        static async Task Main(string[] args)
        {
            var handler1 = new Handler1();
            var handler2 = new Handler2();
            var handler21 = new Handler21();
            var handler22 = new Handler22();
            var handler3 = new Handler3();

            handler2.AddHandler(handler21);
            handler2.AddHandler(handler22);
            
            handler1.AddHandler(handler2);
            handler1.AddHandler(handler3);

            var resultAsync = await handler1.HandleAsync(new Request());
            
            var result = handler1.Handle(new Request());
            
            Console.WriteLine("Hello World!");
        }
    }
}