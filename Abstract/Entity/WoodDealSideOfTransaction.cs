using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_test_console.Abstract.Entity
{
    public abstract class WoodDealSideOfTransaction
    {
        public int Id { get; set; }
        public string Inn { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfAdd { get; set; }
    }
}
