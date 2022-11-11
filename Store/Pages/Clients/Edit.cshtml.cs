using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Store.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientinfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=LAPTOP-0AEC5LSU;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientinfo.id = "" + reader.GetInt32(0);
                                clientinfo.name = reader.GetString(1);
                                clientinfo.email = reader.GetString(2);
                                clientinfo.password = reader.GetString(3);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            clientinfo.id = Request.Form["id"];
            clientinfo.name = Request.Form["name"];
            clientinfo.email = Request.Form["email"];
            clientinfo.password = Request.Form["password"];

            if (clientinfo.id.Length == 0 || clientinfo.name.Length == 0 ||
                clientinfo.email.Length == 0 || clientinfo.password.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=LAPTOP-0AEC5LSU;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clients " +
                                 "SET name=@name, email=@email, password=@password " +
                                 "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientinfo.name);
                        command.Parameters.AddWithValue("@email", clientinfo.email);
                        command.Parameters.AddWithValue("@password", clientinfo.password);
                        command.Parameters.AddWithValue("@id", clientinfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage=ex.Message;
                return;
            }

            Response.Redirect("/Clients/Index");
        }
    }
}
