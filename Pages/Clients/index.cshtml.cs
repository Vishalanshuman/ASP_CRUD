using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace RazorPagesMovie.Pages.Clients
{
        public class ClientInfo
        {
            public String id;
            public String name;
            public String email;
            public String address;
        }
    public class IndexModel : PageModel

    {

        public List<ClientInfo> clientList = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "server=localhost;database=CrudDb;uid=root;password=vishalanshuman@123;";
                MySqlConnection connection = new MySqlConnection(connectionString);

                connection.Open();
                string sql = "SELECT * FROM client";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClientInfo clientInfo = new ClientInfo();
                            clientInfo.id = "" + reader.GetInt32(0);
                            clientInfo.name = reader.GetString(1);
                            clientInfo.email = reader.GetString(2);
                            clientInfo.address = reader.GetString(3);

                            clientList.Add(clientInfo);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());

            }
        }
    }



}
