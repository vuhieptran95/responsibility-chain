using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectHealthReport.Domains.Domains
{
    public class MetricStatus
    {
        private readonly int _id;
        private readonly string _name;
        private readonly ICollection<Threshold> _thresholds;

        public MetricStatus()
        {
            _thresholds = new HashSet<Threshold>();
        }

        public MetricStatus(int id, string name) : this()
        {
            _id = id;
            _name = name;
        }

        public MetricStatus(int id, string name, ICollection<Threshold> thresholds): this()
        {
            _id = id;
            _name = name;
            _thresholds = thresholds;
        }

        public int Id => _id;

        [Required]
        public string Name => _name;

        public IEnumerable<Threshold> Thresholds => _thresholds;
    }
}