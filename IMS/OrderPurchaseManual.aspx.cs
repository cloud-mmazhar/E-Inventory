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
    public partial class OrderPurchaseManual : System.Web.UI.Page
    {
        public static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IMSConnectionString"].ToString());
        public static DataSet ProductSet;
        public static DataSet systemSet;
        public static bool FirstOrder;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FirstOrder = false;
                systemSet = new DataSet();
                ProductSet = new DataSet();
                LoadData();

            }
        }
        private void LoadData()
        {
            #region Getting Vendors
            try
            {
                connection.Open();
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand("Sp_GetVendor", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dA = new SqlDataAdapter(command);
                dA.Fill(ds);

                RequestTo.DataSource = ds.Tables[0];
                RequestTo.DataTextField = "SupName";
                RequestTo.DataValueField = "SuppID";
                RequestTo.DataBind();
                if (RequestTo != null)
                {
                    RequestTo.Items.Insert(0, "Select Vendor");
                    RequestTo.SelectedIndex = 0;
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

            #region Populating Product List
            
            //try
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand("Select  Top 200 Product_Name,ProductID From tbl_ProductMaster Where tbl_ProductMaster.Product_Id_Org LIKE '444%' AND Status = 1", connection);
            //    DataSet ds = new DataSet();
            //    SqlDataAdapter sA = new SqlDataAdapter(command);
            //    sA.Fill(ds);
            //    ProductSet = ds;
            //    SelectProduct.DataSource = ds.Tables[0];
            //    SelectProduct.DataTextField = "Product_Name";
            //    SelectProduct.DataValueField = "ProductID";
            //    SelectProduct.DataBind();
            //    if (SelectProduct != null)
            //    {
            //        SelectProduct.Items.Insert(0, "Select Product");
            //        SelectProduct.SelectedIndex = 0;
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
            //    connection.Close();
            //}
            #endregion
        }
        protected void btnAccept_Click(object sender, EventArgs e)
        {
            

            Response.Redirect("PO_GENERATE.aspx");
        }

        protected void btnDecline_Click(object sender, EventArgs e)
        {
            RequestTo.Enabled = true;
            StockDisplayGrid.DataSource = null;
            StockDisplayGrid.DataBind();
            SelectQuantity.Text = "";
            SelectProduct.SelectedIndex = -1;
            RequestTo.SelectedIndex = -1;
            btnAccept.Visible = false;
            btnDecline.Visible = false;
        }

        protected void StockDisplayGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            StockDisplayGrid.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void StockDisplayGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            StockDisplayGrid.EditIndex = -1;
            BindGrid();
        }

        protected void StockDisplayGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("UpdateStock"))
                {
                    int quan = int.Parse(((TextBox)StockDisplayGrid.Rows[StockDisplayGrid.EditIndex].FindControl("txtQuantity")).Text);
                    int orderDetID = int.Parse(((Label)StockDisplayGrid.Rows[StockDisplayGrid.EditIndex].FindControl("OrderDetailNo")).Text);
                    connection.Open();  
                    SqlCommand command = new SqlCommand("sp_UpdateOrderDetailsQuantity", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@p_OrderDetailID", orderDetID);
                    command.Parameters.AddWithValue("@p_Qauntity", quan);
                    
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exp) { }
            finally
            {
                connection.Close();
                StockDisplayGrid.EditIndex = -1;
                BindGrid();
            }
        }

       

        protected void StockDisplayGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int orderDetID = int.Parse(((Label)StockDisplayGrid.Rows[e.RowIndex].FindControl("OrderDetailNo")).Text);
                connection.Open();
                SqlCommand command = new SqlCommand("sp_DeleteOrderDetailsbyID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@p_OrderDetailID", orderDetID);
                

                command.ExecuteNonQuery();
            }
            catch (Exception exp) { }
            finally
            {
                connection.Close();
                StockDisplayGrid.EditIndex = -1;
                BindGrid();
            }
        }

        protected void StockDisplayGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            StockDisplayGrid.EditIndex = e.NewEditIndex;
            LoadData();
        }

        protected void btnCreateOrder_Click(object sender, EventArgs e)
        {

            btnAccept.Visible = true;
            btnDecline.Visible = true;
            if (FirstOrder.Equals(false))
            {
                #region Creating Order
                
                int pRequestFrom = 0;
                int pRequestTo = 0;
                String OrderMode = "";
                int OrderType = 3;//incase of vendor this should be 3

                if (RequestTo.SelectedItem.ToString().Contains("store")) // neeed to check it, because name doesn't always contains Store
                {
                    OrderMode = "Store";
                }
                else if (RequestTo.SelectedItem.ToString().Contains("warehouse"))
                {
                    OrderMode = "Warehouse";
                }
                else
                {
                    OrderMode = "Vendor";
                }

                String Invoice = "";
                String Vendor = "True";


                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_CreateOrder", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    //sets vendor
                    if (int.TryParse(RequestTo.SelectedValue.ToString(), out pRequestTo))
                    {
                        command.Parameters.AddWithValue("@p_RequestTO", pRequestTo);
                    }
                    //sets warehouse/store
                    if (int.TryParse(Session["UserSys"].ToString(), out pRequestFrom))
                    {
                        command.Parameters.AddWithValue("@p_RequestFrom", pRequestFrom);
                    }

                    command.Parameters.AddWithValue("@p_OrderType", OrderType);
                    command.Parameters.AddWithValue("@p_Invoice", Invoice);
                    command.Parameters.AddWithValue("@p_OrderMode", OrderMode);
                    command.Parameters.AddWithValue("@p_Vendor", Vendor);
                    command.Parameters.AddWithValue("@p_orderStatus", "Pending");
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

                #region Linking to Order Detail table

                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_InserOrderDetail_ByStore", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    int OrderNumber, ProductNumber, Quantity = 0;

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

                   
                    command.Parameters.AddWithValue("@p_status", "Pending");
                    command.Parameters.AddWithValue("@p_comments", "Generated to Vendor");
                    
                    command.ExecuteNonQuery();
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
            else
            {
                #region Product Existing in the Current Order
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_GetOrderbyVendor", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    int OrderNumber = 0;


                    if (int.TryParse(Session["OrderNumber"].ToString(), out OrderNumber))
                    {
                        command.Parameters.AddWithValue("@p_OrderID", OrderNumber);
                    }
                    SqlDataAdapter sA = new SqlDataAdapter(command);
                    sA.Fill(ds);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }

                int ProductNO = 0;
                bool ProductPresent = false;
                if (int.TryParse(SelectProduct.SelectedValue.ToString(), out ProductNO))
                {
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[i]["ProductID"]).Equals(ProductNO))
                    {
                        ProductPresent = true;
                        break;
                    }
                }
                #endregion

                if (ProductPresent.Equals(false))
                {
                    #region Linking to Order Detail table

                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("sp_InserOrderDetail_ByStore", connection);
                        command.CommandType = CommandType.StoredProcedure;

                        int OrderNumber, ProductNumber, Quantity = 0;

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

                        
                        command.Parameters.AddWithValue("@p_status", "Pending");
                        command.Parameters.AddWithValue("@p_comments", "Generated to Vendor");
                       
                        command.ExecuteNonQuery();
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
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record can not be inserted, because it is already present')", true);
                }
            }

            BindGrid();
        }


        private void BindGrid() 
        {
            #region Display Products
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_GetOrderbyVendor", connection);
                command.CommandType = CommandType.StoredProcedure;
                int OrderNumber = 0;
                DataSet ds = new DataSet();

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
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            #endregion
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            RequestTo.Enabled = true;
            StockDisplayGrid.DataSource = null;
            StockDisplayGrid.DataBind();
            SelectQuantity.Text = "";
            SelectProduct.SelectedIndex = -1;
            RequestTo.SelectedIndex = -1;
            btnAccept.Visible = false;
            btnDecline.Visible = false;
        }

        protected void btnCancelOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("PlaceOrder.aspx");
        }

        protected void btnSearchProduct_Click(object sender, ImageClickEventArgs e)
        {
            if (txtProduct.Text.Length >= 3)
            {
                PopulateDropDown(txtProduct.Text);
                SelectProduct.Visible = true;
            }
        }

        public void PopulateDropDown(String Text)
        {
            #region Populating Product Name Dropdown

            try
            {
                connection.Open();

                Text = Text + "%";
                SqlCommand command = new SqlCommand("SELECT * From tbl_ProductMaster Where tbl_ProductMaster.Product_Name LIKE '" + Text + "' AND Status = 1", connection);
                DataSet ds = new DataSet();
                SqlDataAdapter sA = new SqlDataAdapter(command);
                sA.Fill(ds);
                if (SelectProduct.DataSource != null)
                {
                    SelectProduct.DataSource = null;
                }

                ProductSet = null;
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

        protected void SelectProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void RequestTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RequestTo.Enabled = false;
        }
    }
}