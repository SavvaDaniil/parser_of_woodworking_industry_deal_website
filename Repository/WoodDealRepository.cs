using A2_test_console.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A2_test_console.ExceptionCustom.WoodDeal;
using MySql.Data.MySqlClient;

namespace A2_test_console.Repository
{
    public class WoodDealRepository
    {
        public WoodDeal FindByDealNumber(string dealNumber, bool isDebug = false)
        {
            WoodDeal woodDeal = null;

            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            if(isDebug) System.Diagnostics.Debug.WriteLine("connectionString: " + connectionString);
            using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
            {
                string strQuery = string.Format("SELECT * FROM ...", dealNumber);
                MySqlCommand cmd = new MySqlCommand(strQuery, sqlConnection);
                cmd.CommandType = CommandType.Text;
                sqlConnection.Open();
                
                MySqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    if (isDebug) System.Diagnostics.Debug.WriteLine("sqlDataReader.Read()");
                    woodDeal = new WoodDeal();
                    woodDeal.Id = Convert.ToInt32(sqlDataReader["id"]);
                    woodDeal.WoodDealBuyerId = Convert.ToInt32(sqlDataReader["wood_deal_buyer_id"]);
                    woodDeal.WoodDealSellerId = Convert.ToInt32(sqlDataReader["wood_deal_seller_id"]);

                    DateTime dealDate;
                    DateTime.TryParse(sqlDataReader["deal_date"].ToString(), out dealDate);

                    woodDeal.DealDate = dealDate;
                    woodDeal.DealNumber = sqlDataReader["deal_number"] != null ? sqlDataReader["deal_number"].ToString() : null;
                    woodDeal.WoodVolumeBuyer = Convert.ToInt32(sqlDataReader["wood_volume_buyer"]);
                    woodDeal.WoodVolumeSeller = Convert.ToInt32(sqlDataReader["wood_volume_seller"]);
                }
                
            }

            return woodDeal;
        }

        public void Add(WoodDeal woodDeal, bool isDebug = false)
        {
            if (woodDeal == null) throw new NullReferenceException();
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
            {
                string sqlQuery = "INSERT INTO ...;";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, sqlConnection);
                cmd.Parameters.Add("@wood_deal_buyer_id", MySqlDbType.Int32);
                ...

                cmd.Parameters["@wood_deal_buyer_id"].Value = woodDeal.WoodDealBuyerId;
                ...

                try
                {
                    sqlConnection.Open();
                    int number = cmd.ExecuteNonQuery();
                    if (isDebug) Console.WriteLine("Добавлено объектов: {0}", number);
                }
                catch (Exception ex)
                {
                    throw new WoodDealAddFailedException(ex.ToString());
                }
            }
        }

        public List<WoodDeal> ListAll()
        {
            List<WoodDeal> woodDeals = new List<WoodDeal>();
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ...", sqlConnection);
                cmd.CommandType = CommandType.Text;
                sqlConnection.Open();

                MySqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    WoodDeal woodDeal = new WoodDeal();

                    woodDeal.Id = Convert.ToInt32(sqlDataReader["id"]);
                    woodDeal.WoodDealBuyerId = Convert.ToInt32(sqlDataReader["wood_deal_buyer_id"]);
                    ...


                    woodDeals.Add(woodDeal);
                }
            }

            return woodDeals;
        }
    }
}
