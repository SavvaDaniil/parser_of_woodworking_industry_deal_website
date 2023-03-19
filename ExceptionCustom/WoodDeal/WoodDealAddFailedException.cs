using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_test_console.ExceptionCustom.WoodDeal
{
    public class WoodDealAddFailedException : Exception
    {
        public WoodDealAddFailedException(string message) : base("Fail add WoodDeal in database: " + message)
        {
        }
    }
}
