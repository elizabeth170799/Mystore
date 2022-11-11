using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Store.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.password = Request.Form["password"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 ||
                clientInfo.password.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the new client into the database
            try
            {
                String connectionString = "Data Source=LAPTOP-0AEC5LSU;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients" +
                                 "(name, email, password) VALUES " +
                                 "(@name, @email, @password);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@password", clientInfo.password);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clientInfo.name = ""; clientInfo.email = ""; clientInfo.password = "";
            successMessage = "New client Added Correctly";

            Response.Redirect("/Clients/Index");
        }
    }
}
