using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aspDb
{
    public partial class disconnect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=EBussines;User ID=sa;Password=123");
                SqlCommand cmd = new SqlCommand("select City.*,Country.Name as CountryName from City,Country where FK_CountryId=Country.Id",con);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                gv_city.DataSource = dt;
                gv_city.DataBind();
                ViewState.Add("mydt", dt);
            }
        }

        protected void gv_city_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gv_city_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_city.EditIndex = e.NewEditIndex;
            gv_city.DataSource = (DataTable)ViewState["mydt"];
            gv_city.DataBind();
        }

        protected void gv_city_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }

        protected void gv_city_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string newCity = ((TextBox)gv_city.Rows[e.RowIndex].FindControl("txt_city")).Text;
            ((DataTable)ViewState["mydt"]).Rows[e.RowIndex]["Name"] = newCity;
            gv_city.EditIndex = -1;
            gv_city.DataSource = ViewState["mydt"];
            gv_city.DataBind();
            gv_city.DataBind();

        }

        protected void gv_city_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_city.EditIndex = -1;
            gv_city.DataSource = (DataTable)ViewState["mydt"];
            gv_city.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = EBussines; User ID = sa; Password = 123");
            SqlCommand UpdateCmd = new SqlCommand("update City set Name=@Name,FK_CountryId=@FK_CountryId where Id=@Id",con);
            UpdateCmd.Parameters.Add("@Id", SqlDbType.Int, 4,"Id");
            UpdateCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");
            UpdateCmd.Parameters.Add("@FK_CountryId", SqlDbType.Int, 4, "Fk_CountryId");

            SqlCommand DeleteCmd = new SqlCommand("delete from city where Id=@Id",con);
            DeleteCmd.Parameters.Add("@Id", SqlDbType.Int, 4, "Id");
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.UpdateCommand = UpdateCmd;
            dataAdapter.DeleteCommand = DeleteCmd;
            DataTable dt=(DataTable)ViewState["mydt"];
            dataAdapter.Update(dt);
        }
    }
}