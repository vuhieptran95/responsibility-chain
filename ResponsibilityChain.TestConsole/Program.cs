using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;

namespace ResponsibilityChain.TestConsole
{
    public class Des
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Src
    {
        private IEnumerable<Des> _dess;

        public Src()
        {
            Name = "sfd";
            
            _dess = new List<Des>();
        }

        public Src(int id, string name, IEnumerable<Des> dess): this()
        {
            Id = id;
            Name = name;
            _dess = dess;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Des> Dess
        {
            get => _dess;
        }
    }
    
    class Program
    {
        
        static async Task Main(string[] args)
        {
            var list1 = new List<int>(){1,2,3};
            var list2 = new List<int> {1,2,3,4};

            var list3 = list1.Except(list2);
            
            var src = new Src(1, "1", null);

        }
    }
}