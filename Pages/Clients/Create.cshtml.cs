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
    public class Create : PageModel

    {
        public class ClientInfo
        {
            public String id;
            public String name;
            public String email;
            public String address;
        }
        public string errorMessage = "";
        public string successMessage = "";
        public ClientInfo clientInfo = new ClientInfo();


        public void OnGet()
        {
        }
        public  void OnPost(){
            
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.address = Request.Form["address"];
            

            if (clientInfo.name.Length == 0|| clientInfo.email.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }
            try{
                string connectionString = "server=localhost;database=CrudDb;uid=root;password=vishalanshuman@123;";
                MySqlConnection connection = new MySqlConnection(connectionString);

                connection.Open();
                string sql = "INSERT INTO client "+
                            "(name, email, address) VALUES "+
                            "(@name,@email,@address)";
                using (MySqlCommand command = new MySqlCommand(sql, connection)){
                    command.Parameters.AddWithValue("@name",clientInfo.name);
                    command.Parameters.AddWithValue("@email",clientInfo.email);
                    command.Parameters.AddWithValue("@address",clientInfo.address);

                    command.ExecuteNonQuery();
                }

            }catch(Exception ex){
                errorMessage  =ex.Message;
                return;

            }
            clientInfo.name="";clientInfo.email="";clientInfo.address="";
            successMessage="New Client Added Successfully.";

            Response.Redirect("/Clients/Index");
        }
    }
}