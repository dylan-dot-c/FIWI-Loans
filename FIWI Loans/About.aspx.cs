using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FIWI_Loans
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string connString = WebConfigurationManager.ConnectionStrings["localDatabase"].ToString();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    string query = $"SELECT ID FROM login WHERE username = @Username AND password = @Password";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read(); // Move to the first row

                        Session["Username"] = txtUsername.Text;
                        Session["ID"] = reader["ID"].ToString();
                        Response.Redirect("~/Default.aspx");
                    }
                    else
                    {
                        lblError.Text = "Incorrect Login Info";
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
            }
        }
    
    }
}