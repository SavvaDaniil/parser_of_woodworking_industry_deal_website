using A2_test_console.ExceptionCustom.Service.WoodDealModelService;
using A2_test_console.Model;
using A2_test_console.Model.WoodDeal;
using MySql.Data.MySqlClient.Memcached;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace A2_test_console.Service
{

    public class WoodDealModelService
    {

        public async Task GetTest()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:95.0) Gecko/20100101 Firefox/95.0");
                    var response = await httpClient.GetAsync("...");
                    //System.Diagnostics.Debug.WriteLine("response.StatusCode: " + response.StatusCode);
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new WoodDealSearchFromApiException("StatusCode not OK: " + response.StatusCode);
                    }
                }
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw new WoodDealSearchFromApiException(ex.Message);
            }
        }

        public async Task<int> CountFromApi()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:95.0) Gecko/20100101 Firefox/95.0");
                    httpClient.DefaultRequestHeaders.Date = DateTime.Now;

                    var request = new HttpRequestMessage(HttpMethod.Post, "...");
                    request.Content = new StringContent("...", Encoding.UTF8, "application/json");
                    var response = await httpClient.SendAsync(request);
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new WoodDealSearchFromApiException("StatusCode not OK: " + response.StatusCode);
                    }

                    string content = await response.Content.ReadAsStringAsync();
                    //System.Diagnostics.Debug.WriteLine(content);
                    JObject contentJson = JObject.Parse(content);

                    JsonSerializer serializer = new JsonSerializer();
                    WoodDealSearchCountModel woodDealSearchCountModel =
                        (WoodDealSearchCountModel)serializer.Deserialize(new JTokenReader(contentJson), typeof(WoodDealSearchCountModel));
                    if (woodDealSearchCountModel == null) throw new WoodDealTryParseJsonException("no woodDealSearchCountModel");
                    if (woodDealSearchCountModel.data == null) throw new WoodDealTryParseJsonException("no data");
                    if (woodDealSearchCountModel.data.searchReportWoodDeal != null)
                    {
                        return woodDealSearchCountModel.data.searchReportWoodDeal.total;
                    }
                }
                catch (HttpRequestException ex)
                {
                    throw new WoodDealSearchFromApiException("HttpRequestException: " + ex.ToString());
                }

                return 0;
            }
        }

        public async Task<List<WoodDealModel>> SearchFromApi(int page, int count, bool isDebug = false)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:95.0) Gecko/20100101 Firefox/95.0");
                    httpClient.DefaultRequestHeaders.Date = DateTime.Now;

                    if(isDebug) System.Diagnostics.Debug.WriteLine("httpClient: " + httpClient.DefaultRequestHeaders);

                    var request = new HttpRequestMessage(HttpMethod.Post, "...");
                    request.Content = new StringContent("...", Encoding.UTF8, "application/json");
                    var response = await httpClient.SendAsync(request);
                    //System.Diagnostics.Debug.WriteLine(response.RequestMessage.ToString());
                    //System.Diagnostics.Debug.WriteLine(response.RequestMessage.Content.ToString());
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new WoodDealSearchFromApiException("StatusCode not OK: " + response.StatusCode);
                    }

                    string content = await response.Content.ReadAsStringAsync();
                    if (isDebug) System.Diagnostics.Debug.WriteLine(content);
                    JObject contentJson = JObject.Parse(content);

                    JsonSerializer serializer = new JsonSerializer();
                    WoodDealSearchModel woodDealSearchModel =
                        (WoodDealSearchModel)serializer.Deserialize(new JTokenReader(contentJson), typeof(WoodDealSearchModel));
                    if (woodDealSearchModel == null) throw new WoodDealTryParseJsonException("no woodDealSearchModel");
                    if (woodDealSearchModel.data == null) throw new WoodDealTryParseJsonException("no data");
                    if (woodDealSearchModel.data.searchReportWoodDeal == null) throw new WoodDealTryParseJsonException("no searchReportWoodDeal");
                    if (woodDealSearchModel.data.searchReportWoodDeal.content != null)
                    {
                        return woodDealSearchModel.data.searchReportWoodDeal.content;
                    }
                }
                catch (HttpRequestException ex)
                {
                    throw new WoodDealSearchFromApiException("HttpRequestException: " + ex.ToString());
                }

                return null;
            }
        }
    }
}
