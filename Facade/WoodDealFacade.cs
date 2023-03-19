using A2_test_console.Entity;
using A2_test_console.Factory.Entity;
using A2_test_console.Model;
using A2_test_console.Repository;
using A2_test_console.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_test_console.Facade
{
    public class WoodDealFacade
    {

        public async Task ParseAllFromApi()
        {
            WoodDealModelService woodDealModelService = new WoodDealModelService();
            int woodDealCount = await woodDealModelService.CountFromApi();
            System.Diagnostics.Debug.WriteLine("woodDealCount: " + woodDealCount);

            WoodDealRepository woodDealRepository = new WoodDealRepository();
            WoodDealBuyerRepository woodDealBuyerRepository = new WoodDealBuyerRepository();
            WoodDealBuyerFactory woodDealBuyerFactory = new WoodDealBuyerFactory();
            WoodDealBuyer woodDealBuyer = null;
            WoodDealSellerRepository woodDealSellerRepository = new WoodDealSellerRepository();
            WoodDealSellerFactory woodDealSellerFactory = new WoodDealSellerFactory();
            WoodDealSeller woodDealSeller = null;
            WoodDealFactory woodDealFactory = new WoodDealFactory();

            int countOfItems = 10000;
            int outOfCount = woodDealCount + countOfItems;
            for (int page = 0; (page + 1) * countOfItems <= outOfCount; page++)
            {
                System.Diagnostics.Debug.WriteLine("page: {0} allCountOfItems {1}", page, (page + 1) * countOfItems);

                List<WoodDealModel> woodDealModels = await woodDealModelService.SearchFromApi(page, countOfItems);
                foreach(WoodDealModel woodDealModel in woodDealModels)
                {
                    //System.Diagnostics.Debug.WriteLine("Работам с " + woodDealModel.dealNumber);
                    if(woodDealRepository.FindByDealNumber(woodDealModel.dealNumber) == null)
                    {
                        woodDealBuyer = woodDealBuyerRepository.FindByInn(woodDealModel.buyerInn);
                        if(woodDealBuyer == null)
                        {
                            woodDealBuyer = woodDealBuyerFactory.CreateFromApiModel(woodDealModel);
                            woodDealBuyerRepository.Add(woodDealBuyer);
                            woodDealBuyer = woodDealBuyerRepository.FindByInn(woodDealModel.buyerInn);
                        }

                        woodDealSeller = woodDealSellerRepository.FindByInn(woodDealModel.sellerInn);
                        if (woodDealSeller == null)
                        {
                            woodDealSeller = woodDealSellerFactory.CreateFromApiModel(woodDealModel);
                            woodDealSellerRepository.Add(woodDealSeller);
                            woodDealSeller = woodDealSellerRepository.FindByInn(woodDealModel.sellerInn);
                        }

                        woodDealRepository.Add(woodDealFactory.CreateFromApiModel(woodDealModel, woodDealBuyer, woodDealSeller));
                    }
                }

                if (page > 0) break;
            }

        }

    }
}
