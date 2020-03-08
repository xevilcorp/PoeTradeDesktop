using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoeTradeDesktop.Schemes.Filtering
{
    public class StatGroup
    {
        public string Type { get; set; }
        public PropertyMinMax Value { get; set; }
        public List<Stat> Filters { get; set; }
    }
}
