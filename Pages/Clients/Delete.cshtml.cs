using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;


namespace crud.Pages.Clients
{
    public class DeleteModel : PageModel
    {

        public ClientInfo clientInfo = new ClientInfo();
        public void OnGet()
        {
            string ID = Request.Query["id"];
            string sql = "";

            try
            {
                string conn = "Data Source=.;Initial Catalog=crudb;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
 
                    if (string.IsNullOrEmpty(ID))
                    {
                        sql = "DELETE FROM clients";
                    }
                    else
                    {
                        sql = "DELETE FROM clients WHERE id=@id";
                    }

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Add parameter only if we're deleting a specific client
                        if (!string.IsNullOrEmpty(ID))
                        {
                            command.Parameters.AddWithValue("@id", ID);
                        }

                        command.ExecuteNonQuery();
                    }
                    Response.Redirect("/Clients/Index");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

    }
}
