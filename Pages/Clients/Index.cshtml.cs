using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient; 

namespace crud.Pages.Clients
{
    public class IndexModel : PageModel
    {
        // Initialize the list of clients properly
        public List<ClientInfo> ClientsList = new List<ClientInfo>();

        public void OnGet()
        {
            try
            {
                string conn = "Data Source=.;Initial Catalog=crudb;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader()) 
                        {
                            while (reader.Read()) 
                            {
                                ClientInfo clientInfo = new ClientInfo();

                                // line (0), (1), (2), (3) .......

                                clientInfo.ID = "" + reader.GetInt32(0);
                                clientInfo.Name = reader.GetString(1);
                                clientInfo.Email = reader.GetString(2);
                                clientInfo.Phone = reader.GetString(3);
                                clientInfo.Address = reader.GetString(4);
                                clientInfo.CreatedAt = reader.GetDateTime(5).ToString();

                                ClientsList.Add(clientInfo);
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Not Connected! Error: {ex.Message}");
            }
        }
    }

    public class ClientInfo
    {
        // Use PascalCase for public properties
        public string ID = string.Empty;
        public string Email = string.Empty;
        public string Name = string.Empty;
        public string Phone = string.Empty;
        public string Address = string.Empty;
        public string CreatedAt = string.Empty;
    }
}
