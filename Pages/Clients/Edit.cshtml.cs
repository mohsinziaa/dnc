using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Net;

namespace crud.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string ID = Request.Query["id"];

            try
            {
                string conn = "Data Source=.;Initial Catalog=crudb;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clientInfo.ID = "" + reader.GetInt32(0);
                                clientInfo.Name = reader.GetString(1);
                                clientInfo.Email = reader.GetString(2);
                                clientInfo.Phone = reader.GetString(3);
                                clientInfo.Address = reader.GetString(4);

                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            clientInfo.ID = Request.Form["id"];
            clientInfo.Name = Request.Form["Name"];
            clientInfo.Email = Request.Form["Email"];
            clientInfo.Phone = Request.Form["Phone"];
            clientInfo.Address = Request.Form["Address"];

            if (clientInfo.Name.Length == 0 || clientInfo.Email.Length == 0
                || clientInfo.Phone.Length == 0 || clientInfo.Address.Length == 0
                || clientInfo.ID.Length == 0)
            {
                errorMessage = "All Fields Are Required!";
                return;
            }

            //Save client into DB.

            try
            {
                string conn = "Data Source=.;Initial Catalog=crudb;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    string sql = "UPDATE clients " +
                                 "SET name=@name, email=@email, phone=@phone, address=@address " +
                                 "WHERE id=@id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.Name);       // Assign 'name'
                        command.Parameters.AddWithValue("@email", clientInfo.Email);     // Assign 'email'
                        command.Parameters.AddWithValue("@phone", clientInfo.Phone);     // Assign 'phone'
                        command.Parameters.AddWithValue("@address", clientInfo.Address); // Assign 'address'
                        command.Parameters.AddWithValue("@id", clientInfo.ID); // Assign 'ID'

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Clients/Index");
        }

    }
}
