using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS
{
    public partial class ManageProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnManageProducts_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {

            Response.Redirect("AddProduct.aspx");
        }

        protected void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            Session["Linkto"] = "DELETE";
            Response.Redirect("Edit_DeleteProduct.aspx");
        }

        protected void btnEditProduct_Click(object sender, EventArgs e)
        {
            Session["Linkto"] = "EDIT";
            Response.Redirect("Edit_DeleteProduct.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageInventory.aspx");
        }

        protected void btnStocks_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageStocks.aspx");
        }
    }
}