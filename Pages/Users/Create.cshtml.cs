using System.Dynamic;
using System.Net.Mime;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RazorPagesMovie.Pages.Users
{
    public  class Create : PageModel
    {
        public class UserInfo
        {
            public String id;
            public byte[] image;
            public String name;
            public string phone;
            public String email;
            public String address;
        }
        public UserInfo userInfo = new UserInfo();
        public string errorMessage = "";
        public string successMessage  = "";
            public void OnGet()
        {
            // Handle GET request if needed
        }

        public async Task<IActionResult> OnPostAsync(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                // Get the file extension
                string fileExtension = Path.GetExtension(image.FileName);

                // Generate a unique file name
                string uniqueFileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + fileExtension;

                // Combine the unique file name with the path where the image will be stored
                string filePath = Path.Combine("wwwroot/images", uniqueFileName);

                // Save the image to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                try{
                    string connectionString = "server=localhost;database=CrudDb;uid=root;password=vishalanshuman@123;";
                    MySqlConnection connection = new MySqlConnection(connectionString);

                    connection.Open();
                    string sql = "INSERT INTO users "+
                                "(image,name, phone,email, address) VALUES "+
                                "(@image,@name,@phone,@email,@address)";
                    using (MySqlCommand command = new MySqlCommand(sql, connection)){
                        command.Parameters.AddWithValue("@image",userInfo.image);
                        command.Parameters.AddWithValue("@name",userInfo.name);
                        command.Parameters.AddWithValue("@phone",userInfo.phone);
                        command.Parameters.AddWithValue("@email",userInfo.email);
                        command.Parameters.AddWithValue("@address",userInfo.address);// HR@REPUTABLEteCHNOLOGY.COM

                        command.ExecuteNonQuery();

                        successMessage = "User Created.";
                        Response.Redirect("/");
                    }


                }catch(Exception ex){
                    errorMessage = ex.Message;
                }
                
                    


                // Handle the successful upload (e.g., save the file path to the database)
                // ...

            }

            // Handle validation errors or missing file
            ModelState.AddModelError(string.Empty, "Please select an image to upload.");
            return Page();
        }
    }
    
    
}
