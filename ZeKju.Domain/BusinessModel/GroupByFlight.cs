using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeKju.Domain.BusinessModel
{
    public class GroupByFlight
    {
        public DateTime Key { get; set; }
        public List<Flight> Items { get; set; }
        public DateTime MinTime { get; set; }
        public DateTime MaxTime { get; set; }
    }
}
