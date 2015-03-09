using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS
{
    public partial class WarehouseMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnManageInventory_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageInventory.aspx",false);
        }

        protected void btnManageOrders_Click(object sender, EventArgs e)
        {

        }

        protected void btnRegisterUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterUsers.aspx", false);
        }

        protected void tbnStoreRequests_Click(object sender, EventArgs e)
        {
            Response.Redirect("Warehouse_StoreRequests.aspx", false);
        }
    }
}