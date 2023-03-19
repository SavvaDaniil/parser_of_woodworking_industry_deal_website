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
    public class WoodDealBuyerRepository
    {
        public WoodDealBuyer FindByInn(string inn, bool isDebug = false)
        {
            WoodDealBuyer woodDealBuyer = null;

            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            if (isDebug) System.Diagnostics.Debug.WriteLine("connectionString: " + connectionString);
            using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
            {

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ... WHERE XXXXXXXXXXX = @XXXXXXXXXXX ORDER BY id DESC LIMIT 1", sqlConnection);
                cmd.Parameters.Add("XXXXXXXXXXX", MySqlDbType.VarString).Value = inn;
                sqlConnection.Open();

                MySqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    if (isDebug) System.Diagnostics.Debug.WriteLine("sqlDataReader.Read()");
                    woodDealBuyer = new WoodDealBuyer();
                    woodDealBuyer.Id = Convert.ToInt32(sqlDataReader["id"]);
                    woodDealBuyer.Inn = sqlDataReader["inn"] != null ? sqlDataReader["inn"].ToString() : null;
                    .................

                    DateTime dateOfAdd;
                    DateTime.TryParse(sqlDataReader["date_of_add"].ToString(), out dateOfAdd);

                    woodDealBuyer.DateOfAdd = dateOfAdd;
                }

            }

            return woodDealBuyer;
        }

        public void Add(WoodDealBuyer woodDealBuyer, bool isDebug = false)
        {
            if (woodDealBuyer == null) throw new NullReferenceException();
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
            {
                string sqlQuery = "INSERT INTO XXXXXXXXXXXXXXXX (inn, ...) VALUES  " +
                    "(@inn, ...);";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, sqlConnection);
                cmd.Parameters.Add("@inn", MySqlDbType.VarChar);
                ...

                cmd.Parameters["@date_of_add"].Value = woodDealBuyer.DateOfAdd;

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

        public List<WoodDealBuyer> ListAll()
        {
            List<WoodDealBuyer> woodDealBuyers = new List<WoodDealBuyer>();
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ...", sqlConnection);
                cmd.CommandType = CommandType.Text;
                sqlConnection.Open();

                MySqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    WoodDealBuyer woodDealBuyer = new WoodDealBuyer();

                    woodDealBuyer.Id = Convert.ToInt32(sqlDataReader["id"]);
                    woodDealBuyer.Inn = sqlDataReader["inn"] != null ? sqlDataReader["inn"].ToString() : null;
                    ...

                    DateTime dateOfAdd;
                    DateTime.TryParse(sqlDataReader["date_of_add"].ToString(), out dateOfAdd);

                    woodDealBuyer.DateOfAdd = dateOfAdd;

                    woodDealBuyers.Add(woodDealBuyer);
                }
            }

            return woodDealBuyers;
        }
    }
}
