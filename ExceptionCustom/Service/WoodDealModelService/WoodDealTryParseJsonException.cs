using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_test_console.ExceptionCustom.Service.WoodDealModelService
{
    public class WoodDealTryParseJsonException : Exception
    {
        public WoodDealTryParseJsonException(string message) : base("Error when try parse json from search WoodDeal from api: " + message)
        {
        }
    }
}
