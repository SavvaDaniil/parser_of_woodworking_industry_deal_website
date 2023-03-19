using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_test_console.ExceptionCustom.Service.WoodDealModelService
{
    public class WoodDealSearchFromApiException : Exception
    {
        public WoodDealSearchFromApiException(string message) : base("Error when try post request to api: " + message)
        {
        }
    }
}
