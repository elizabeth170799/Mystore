using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Store.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {


            try
            {
                String connectionString = "Data Source=LAPTOP-0AEC5LSU;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientinfo = new ClientInfo();
                                clientinfo.id = "" + reader.GetInt32(0);
                                clientinfo.name = reader.GetString(1);
                                clientinfo.email = reader.GetString(2);
                                clientinfo.password = reader.GetString(3);
                                clientinfo.created_at = reader.GetDateTime(4).ToString();

                                listClients.Add(clientinfo);
                            }
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


    public class ClientInfo
    {
        public String? id;
        public String? name;
        public String? email;
        public String? password;
        public String? created_at;
    }
}


