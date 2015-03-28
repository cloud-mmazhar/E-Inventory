using IMSBusinessLogic;
using IMSCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS
{
    public partial class ManageVendor : System.Web.UI.Page
    {
        DataSet ds;
        public static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IMSConnectionString"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    BindGrid();
                   
                }
                catch (Exception exp) { }
            }
        }

        private void BindGrid() 
        {
            ds = VendorBLL.GetAllVendors(connection);
            gdvVendor.DataSource = ds;
            gdvVendor.DataBind();
            //drpVendor.DataSource = ds;
            //drpVendor.Items.Insert(0, new ListItem("Select Product", ""));
            //drpVendor.DataTextField = "SupName";
            //drpVendor.DataValueField = "Supp_ID";

            //drpVendor.DataBind();
        }

        protected void gdvVendor_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gdvVendor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gdvVendor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvVendor.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void gdvVendor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
        }

        protected void gdvVendor_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //gdvVendor.EditIndex = -1;
            //BindGrid();
        }

        protected void gdvVendor_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                VendorBLL _vendorBll = new VendorBLL();
                Label ID = (Label)gdvVendor.Rows[e.RowIndex].FindControl("lblSupID");
                int id = int.Parse(ID.Text);
                Vendor vendor = new Vendor();//= empid.Text;
                vendor.supp_ID = id;
               _vendorBll.Delete(vendor, connection);


            }
            catch (Exception exp) { }
            finally
            {
                gdvVendor.EditIndex = -1;
                BindGrid();
            }
        }

        protected void btnAddVendor_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEditVendor.aspx",false);
        }

        protected void btnGoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("WarehouseMain.aspx",false);
        }
    }
}