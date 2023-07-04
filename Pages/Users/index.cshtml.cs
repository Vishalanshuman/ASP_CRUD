using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace RazorPagesMovie.Pages.Users
{
        public class UserInfo
        {
            public String id;
            public byte[] image;
            public String name;
            public String phone;
            public String email;
            public String address;
        }
    public class IndexModel : PageModel

    {

        public List<UserInfo> userList = new List<UserInfo>();
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
                            UserInfo userInfo= new UserInfo();
                            userInfo.id = "" + reader.GetInt32(0);
                            userInfo.image = (byte[])reader["image_data"];
                            userInfo.name = reader.GetString(1);
                            userInfo.email = reader.GetString(2);
                            userInfo.address = reader.GetString(3);

                            userList.Add(userInfo);

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
