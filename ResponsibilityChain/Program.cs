using System;
using System.Collections.Generic;

namespace ResponsibilityChain
{
    public interface IHandler<TRequest, TResponse>
    {
        TResponse Handle(TRequest request);
        IHandler<TRequest, TResponse> Next { get; set; }
        IHandler<TRequest, TResponse> AddHandler(IHandler<TRequest, TResponse> handler);
    }

    public class Handler<TRequest, TResponse> : IHandler<TRequest, TResponse>
    {
        public virtual TResponse Handle(TRequest request)
        {
            if (Next == null)
            {
                return default;
            }
            return Next.Handle(request);
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
    }
    
    public class Handler2 : Handler<Request, Response>
    {
        public override Response Handle(Request request)
        {
            Console.WriteLine("Doing sth with handler 2");
            return base.Handle(request);
        }
    }
    
    public class Handler21 : Handler<Request, Response>
    {
        public override Response Handle(Request request)
        {
            Console.WriteLine("Doing sth with handler 21");
            base.Handle(request);
            Console.WriteLine("after 21");
            return null;
        }
    }
    
    public class Handler22 : Handler<Request, Response>
    {
        public override Response Handle(Request request)
        {
            Console.WriteLine("Doing sth with handler 22");
            base.Handle(request);
            Console.WriteLine("after 22");
            return null;
        }
    }
    
    public class Handler3 : Handler<Request, Response>
    {
        public override Response Handle(Request request)
        {
            Console.WriteLine("Doing sth with handler 3");
            return new Response();
        }
    }
    
    
    class Program
    {
        static void Main(string[] args)
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

            var result = handler1.Handle(new Request());
            
            Console.WriteLine("Hello World!");
        }
    }
}