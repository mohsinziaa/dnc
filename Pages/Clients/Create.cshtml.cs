using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace crud.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public string errorMessage = "";
        public string successMessage = "";
        public ClientInfo newClient = new ClientInfo();

        public void OnGet()
        {

        }
        public void OnPost()
        {
            newClient.Name = Request.Form["Name"];
            newClient.Email = Request.Form["Email"];
            newClient.Phone = Request.Form["Phone"];
            newClient.Address = Request.Form["Address"];

            if (newClient.Name.Length == 0 || newClient.Email.Length == 0
                || newClient.Phone.Length == 0 || newClient.Address.Length == 0)
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
                    string sql = "INSERT INTO clients " + 
                                 "(name, email, phone, address) VALUES" +
                                 "(@name, @email, @phone, @address);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", newClient.Name);       // Assign 'name'
                        command.Parameters.AddWithValue("@email", newClient.Email);     // Assign 'email'
                        command.Parameters.AddWithValue("@phone", newClient.Phone);     // Assign 'phone'
                        command.Parameters.AddWithValue("@address", newClient.Address); // Assign 'address'

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            newClient.Name = "";
            newClient.Email = "";
            newClient.Address = "";
            newClient.Phone = "";

            successMessage = "Client Added Successfully!";

            Response.Redirect("/Clients/Index");
        }

    }
}
