using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class ViewModel
    {
        public IEnumerable<Drink> Drinks { get; set; }
        public IEnumerable<Food> Foods { get; set; }
        public IEnumerable<Table> Tables { get; set; }
    }
}
