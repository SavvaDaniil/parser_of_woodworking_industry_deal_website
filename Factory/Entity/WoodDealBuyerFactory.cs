using A2_test_console.Entity;
using A2_test_console.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_test_console.Factory.Entity
{
    public class WoodDealBuyerFactory
    {
        public WoodDealBuyer CreateFromApiModel(WoodDealModel woodDealModel)
        {
            if (woodDealModel == null) throw new NullReferenceException();
            WoodDealBuyer woodDealBuyer = new WoodDealBuyer();

            woodDealBuyer.Inn = woodDealModel.buyerInn;
            ...

            return woodDealBuyer;
        }
    }
}
