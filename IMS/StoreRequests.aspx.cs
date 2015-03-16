using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;

namespace IMS
{
    public partial class StoreRequests : System.Web.UI.Page
    {
        public static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IMSConnectionString"].ToString());
        public static DataSet ProductSet;
        public static DataSet systemSet;
        public static bool FirstOrder;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                FirstOrder = false;
                systemSet = new DataSet();
                ProductSet = new DataSet();
                LoadData();
                
            }
        }

        private void LoadData()
        {
            #region Getting Systems
            try
            {
                connection.Open();
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand("sp_GetSystemsForStore", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dA = new SqlDataAdapter(command);
                dA.Fill(ds);

                RequestTo.DataSource = ds.Tables[0];
                RequestTo.DataTextField = "SystemName";
                RequestTo.DataValueField = "SystemID";
                RequestTo.DataBind();
                if (RequestTo != null)
                {
                    RequestTo.Items.Insert(0, "Select System");
                    RequestTo.SelectedIndex = 0;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            #endregion

            #region Populating Product List
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select TOP 300 * From tbl_ProductMaster Where tbl_ProductMaster.Product_Id_Org LIKE '444%' AND Status = 1", connection);
                DataSet ds = new DataSet();
                SqlDataAdapter sA = new SqlDataAdapter(command);
                sA.Fill(ds);
                ProductSet = ds;
                SelectProduct.DataSource = ds.Tables[0];
                SelectProduct.DataTextField = "Product_Name";
                SelectProduct.DataValueField = "ProductID";
                SelectProduct.DataBind();
                if (SelectProduct != null)
                {
                    SelectProduct.Items.Insert(0, "Select Product");
                    SelectProduct.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            #endregion
        }
        protected void RequestTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RequestTo.Enabled = false;
        }

        protected void btnCreateOrder_Click(object sender, EventArgs e)
        {
            //InvNO.Enabled = false;
            btnAccept.Visible = true;
            btnDecline.Visible = true;
            if (FirstOrder.Equals(false))
            {
                #region Creating Order
                //Session["UserSys"] = 2;
                int pRequestFrom = 0;
                int pRequestTo = 0;
                String OrderMode = "";
                int OrderType = 2;

                if (RequestTo.SelectedItem.ToString().Contains("store")) // neeed to check it, because name doesn't always contains Store
                {
                    OrderMode = "Store";
                }
                else
                {
                    OrderMode = "Warehouse";
                }

                String Invoice = "";
                String Vendor = "";

                
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_CreateOrder", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    if (int.TryParse(RequestTo.SelectedValue.ToString(), out pRequestTo))
                    {
                        command.Parameters.AddWithValue("@p_RequestTO", pRequestTo);
                    }
                    if (int.TryParse(Session["UserSys"].ToString(), out pRequestFrom))
                    {
                        command.Parameters.AddWithValue("@p_RequestFrom", pRequestFrom);
                    }

                    command.Parameters.AddWithValue("@p_OrderType", OrderType);
                    command.Parameters.AddWithValue("@p_Invoice", Invoice);
                    command.Parameters.AddWithValue("@p_OrderMode", OrderMode);
                    command.Parameters.AddWithValue("@p_Vendor", Vendor);

                    DataTable dt = new DataTable();
                    SqlDataAdapter dA = new SqlDataAdapter(command);
                    dA.Fill(dt);
                    if (dt.Rows.Count != 0)
                    {
                        Session["OrderNumber"] = dt.Rows[0][0].ToString();
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
                #endregion

                FirstOrder = true;
            }

            #region Linking to Order Detail table

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_InserOrderDetail_ByStore", connection);
                command.CommandType = CommandType.StoredProcedure;

                int OrderNumber,ProductNumber,Quantity =0;
                
                if (int.TryParse(Session["OrderNumber"].ToString(), out OrderNumber))
                {
                    command.Parameters.AddWithValue("@p_OrderID", OrderNumber);
                }
                if (int.TryParse(SelectProduct.SelectedValue.ToString(), out ProductNumber))
                {
                    command.Parameters.AddWithValue("@p_ProductID", ProductNumber);
                }
                if (int.TryParse(SelectQuantity.Text.ToString(), out Quantity))
                {
                    command.Parameters.AddWithValue("@p_OrderQuantity", Quantity);
                }

                if (RequestTo.SelectedItem.ToString().Contains("store")) // neeed to check it, because name doesn't always contains Store
                {
                    command.Parameters.AddWithValue("@p_status", "Initiated");
                    command.Parameters.AddWithValue("@p_comments", "Generated to Store");
                }
                else
                {
                    command.Parameters.AddWithValue("@p_status", "Initiated");
                    command.Parameters.AddWithValue("@p_comments", "Generated to Warehouse");
                }
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            #endregion

            #region Display Products
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_GetStoreRequest_byOrderID", connection);
                command.CommandType = CommandType.StoredProcedure;
                int OrderNumber = 0;
                DataSet ds  = new DataSet();

                if (int.TryParse(Session["OrderNumber"].ToString(), out OrderNumber))
                {
                    command.Parameters.AddWithValue("@p_OrderID", OrderNumber);
                }

                SqlDataAdapter sA = new SqlDataAdapter(command);
                sA.Fill(ds);
                StockDisplayGrid.DataSource = null;
                StockDisplayGrid.DataSource = ds.Tables[0];
                StockDisplayGrid.DataBind();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            #endregion
        }

        protected void btnCancelOrder_Click(object sender, EventArgs e)
        {
            //should Delete all the rows, if final accept is not pressed.
            Response.Redirect("StoreRequestsMain.aspx");
        }

        protected void StockDisplayGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void StockDisplayGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void StockDisplayGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void StockDisplayGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void StockDisplayGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void StockDisplayGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            //should clean everything if final accept is not pressed
            //InvNO.Text = "";
            RequestTo.Enabled = true;
            StockDisplayGrid.DataSource = null;
            StockDisplayGrid.DataBind();
            SelectQuantity.Text = "";
            SelectProduct.SelectedIndex = -1;
            RequestTo.SelectedIndex = -1;
            btnAccept.Visible = false;
            btnDecline.Visible = false;
        }

        protected void SelectProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                int UserSys = 0;
                SqlCommand command = new SqlCommand();
                if (int.TryParse(Session["UserSys"].ToString(), out UserSys))
                {
                    command = new SqlCommand("Select SystemName from tbl_System Where SystemID = '" + UserSys + "'", connection);
           
                }

                DataTable dt = new DataTable();
                SqlDataAdapter sA = new SqlDataAdapter(command);
                sA.Fill(dt);

                Session["S_RequestFrom"] = dt.Rows[0][0].ToString();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            Session["S_RequestInvoice"] = "";
            Session["S_RequestTo"] = RequestTo.SelectedItem.ToString();

            Response.Redirect("StoreRequestsView.aspx");

        }

        protected void btnDecline_Click(object sender, EventArgs e)
        {
            //should clean everything if final accept is not pressed
          //  InvNO.Text = "";
            RequestTo.Enabled = true;
            StockDisplayGrid.DataSource = null;
            StockDisplayGrid.DataBind();
            SelectQuantity.Text = "";
            SelectProduct.SelectedIndex = -1;
            RequestTo.SelectedIndex = -1;
            btnAccept.Visible = false;
            btnDecline.Visible = false;
        }
    }
}