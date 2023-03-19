using A2_test_console.Entity;
using A2_test_console.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_test_console.Factory.Entity
{
    public class WoodDealSellerFactory
    {
        public WoodDealSeller CreateFromApiModel(WoodDealModel woodDealModel)
        {
            if (woodDealModel == null) throw new NullReferenceException();
            WoodDealSeller woodDealSeller = new WoodDealSeller();

            woodDealSeller.Inn = woodDealModel.sellerInn;
            ..as.

            return woodDealSeller;
        }
    }
}
