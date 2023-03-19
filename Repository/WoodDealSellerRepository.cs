using A2_test_console.Entity;
using A2_test_console.ExceptionCustom.WoodDeal;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2_test_console.Repository
{
    public class WoodDealSellerRepository
    {
        public WoodDealSeller FindByInn(string inn, bool isDebug = false)
        {
            WoodDealSeller woodDealSeller = null;

            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            if (isDebug) System.Diagnostics.Debug.WriteLine("connectionString: " + connectionString);
            using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ...", sqlConnection);
                cmd.Parameters.Add("inn", MySqlDbType.VarString).Value = inn;
                sqlConnection.Open();

                MySqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    if (isDebug) System.Diagnostics.Debug.WriteLine("sqlDataReader.Read()");
                    woodDealSeller = new WoodDealSeller();
                    woodDealSeller.Id = Convert.ToInt32(sqlDataReader["id"]);
                    woodDealSeller.Inn = sqlDataReader["inn"] != null ? sqlDataReader["inn"].ToString() : null;
                    ...


                    woodDealSeller.DateOfAdd = dateOfAdd;
                }

            }

            return woodDealSeller;
        }

        public void Add(WoodDealSeller woodDealSeller, bool isDebug = false)
        {
            if (woodDealSeller == null) throw new NullReferenceException();
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
            {
                string sqlQuery = "INSERT INTO ...";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, sqlConnection);
                cmd.Parameters.Add("@inn", MySqlDbType.VarChar);
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

        public List<WoodDealSeller> ListAll()
        {
            List<WoodDealSeller> woodDealSellers = new List<WoodDealSeller>();
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ...", sqlConnection);
                cmd.CommandType = CommandType.Text;
                sqlConnection.Open();

                MySqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    WoodDealSeller woodDealSeller = new WoodDealSeller();

                    woodDealSeller.Id = Convert.ToInt32(sqlDataReader["id"]);
                    woodDealSeller.Inn = sqlDataReader["inn"] != null ? sqlDataReader["inn"].ToString() : null;
                    ...

                    woodDealSellers.Add(woodDealSeller);
                }
            }

            return woodDealSellers;
        }
    }
}
