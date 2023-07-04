using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
namespace RazorPagesMovie.Pages.Clients
{
    public class Edit : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            try{
                string connectionString = "server=localhost;database=CrudDb;uid=root;password=vishalanshuman@123;";
                using(MySqlConnection connection = new MySqlConnection(connectionString)){
                    connection.Open();
                    string id = Request.Query["id"];
                string sql = "SELECT * FROM client WHERE id=@id";
                using(MySqlCommand command = new MySqlCommand(sql,connection)){
                    command.Parameters.AddWithValue("@id",id);
                    using (MySqlDataReader reader = command.ExecuteReader()){
                        if(reader.Read()){
                            clientInfo.id = ""+reader.GetInt32(0);
                            clientInfo.name = reader.GetString(1);
                            clientInfo.email = reader.GetString(2);
                            clientInfo.address = reader.GetString(3);
                        }
                    }
                }
                }
                
                

            }catch(Exception ex){
                errorMessage = ex.Message;
            }
            

        
        }
        public void OnPost()
        {
            clientInfo.id = Request.Form["id"];
            clientInfo.name  = Request.Form["name"];
            clientInfo.email  = Request.Form["email"];
            clientInfo.address  = Request.Form["address"];

            if(clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.address.Length == 0 || clientInfo.id.Length == 0){
                errorMessage  = " All fields are required";
            }

            try{
                string connectionString = "server=localhost;database=CrudDb;uid=root;password=vishalanshuman@123;";
                using(MySqlConnection connection = new MySqlConnection(connectionString)){
                    connection.Open();
                    string sql = "UPDATE client SET name=@name, email=@email, address=@address WHERE id=@id";

                    using(MySqlCommand command = new MySqlCommand(sql, connection)){
                        command.Parameters.AddWithValue("@id",clientInfo.id);
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@address", clientInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            }catch(Exception ex){
                errorMessage =ex.Message;
            }
            Response.Redirect("/Clients/index");
        }
    }
} 