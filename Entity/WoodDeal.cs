using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_test_console.Entity
{
    public class WoodDeal
    {
        public int Id { get; set; }
        public int WoodDealBuyerId { get; set; }
        public int WoodDealSellerId { get; set; }
        ...

        public DateTime? DealDate { get; set; }
        public string DealNumber { get; set; }
        ...

    }
}
