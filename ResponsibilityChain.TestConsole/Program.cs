using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ResponsibilityChain.TestConsole
{
    public class A
    {
        public string Name { get; set; }
    }

    public class B
    {
        public string Name { get; set; }
    }
    class Program
    {
        
        static async Task Main(string[] args)
        {
            var test = (new A(), new B());
            var result = await DoSomeThing(test);
            Console.WriteLine("Hello World!");
            
            async Task<B> DoSomeThing((A, B) param)
            {
                param.Item2 = new B(){Name = "Hiepdeptrai"};
                return param.Item2;
            }
        }
    }
}