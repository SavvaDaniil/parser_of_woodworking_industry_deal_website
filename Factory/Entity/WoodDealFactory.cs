using A2_test_console.Entity;
using A2_test_console.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_test_console.Factory.Entity
{
    public class WoodDealFactory
    {
        public WoodDeal CreateFromApiModel(WoodDealModel woodDealModel, WoodDealBuyer woodDealBuyer, WoodDealSeller woodDealSeller)
        {
            if (woodDealModel == null || woodDealBuyer == null || woodDealSeller == null) throw new NullReferenceException();
            WoodDeal woodDeal = new WoodDeal();

            woodDeal.WoodDealBuyerId = woodDealBuyer.Id;
            woodDeal.WoodDealSellerId = woodDealSeller.Id;

            if (woodDealModel.dealDate != null)
            {
                DateTime dealDate;
                if(DateTime.TryParse(woodDealModel.dealDate, out dealDate))
                {
                    woodDeal.DealDate = dealDate;
                }
            }

            ..as.

            return woodDeal;
        }
    }
}
