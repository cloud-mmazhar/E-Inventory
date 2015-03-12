﻿using IMSCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS
{
    public partial class ManageStore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddWH_Click(object sender, EventArgs e)
        {
            Session["Action"] = "Add";
            Session["SysToAdd"] = RoleNames.store;
            Response.Redirect("AddSystem.aspx", false);
        }

        protected void btnViewWareHouse_Click(object sender, EventArgs e)
        {
            SelWH.Visible = true;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("HeadOfficeMain.aspx", false);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("StoreMain.aspx", false);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Session["Action"] = "Edit";
            Session["SysToAdd"] = RoleNames.store;
            Response.Redirect("AddSystem.aspx", false);
        }
    }
}