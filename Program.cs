using A2_test_console.Facade;
using A2_test_console.Model;
using A2_test_console.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace A2_test_console
{
    internal class Program
    {
         static void Main(string[] args)
        {
            while (true)
            {
                Task.Run(async () =>
                {
                    WoodDealFacade woodDealFacade = new WoodDealFacade();
                    await woodDealFacade.ParseAllFromApi();

                }).GetAwaiter().GetResult();

                Thread.Sleep(60000 * 10);
            }
        }
    }
}
