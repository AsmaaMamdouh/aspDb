using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aspDb
{
    public partial class ADO_test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=EBussines;User ID=sa;Password=123");
                SqlCommand cmd = new SqlCommand("Select * from Country", con);
                SqlDataReader reader;
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListItem li = new ListItem(reader["Name"].ToString(), reader["Id"].ToString());
                    ddl_country.Items.Add(li);
                }

                con.Close();
            }

        }

        protected void ddl_country_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comment
                Console.Write("hi");
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=EBussines;User ID=sa;Password=123");
                SqlCommand cmd = new SqlCommand("Select * from City where FK_CountryId=" + ddl_country.SelectedValue, con);
                SqlDataReader reader;
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListItem li = new ListItem(reader["Name"].ToString(), reader["Id"].ToString());
                    ddl_city.Items.Add(li);
                }

                con.Close();

            
        }
    }
}