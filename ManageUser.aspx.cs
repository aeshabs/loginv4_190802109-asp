using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString1"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    { 
        Print();
    }
    public void Print()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("SELECT [id], [fullname], [email], [password] FROM [users]", con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            Button btn = (Button)sender;
            SqlCommand cmd = new SqlCommand("DELETE FROM [users] WHERE [id] = @id", con);
            cmd.Parameters.AddWithValue("@id", btn.CommandArgument);
            con.Open();
            int s = cmd.ExecuteNonQuery();
            con.Close();
            if (s == 1)
            {
                Literal1.Text = "User deleted successfully!";
                Print();
            }
            else
            {
                Literal1.Text = "Error!";
            }
        }
    }
 
}