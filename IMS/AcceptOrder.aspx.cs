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
using IMSCommon.Util;

namespace IMS
{
    public partial class AcceptOrder : System.Web.UI.Page
    {
        public static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IMSConnectionString"].ToString());
        public static DataSet ProductSet;
        public static DataSet systemSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        public void LoadData()
        {
            ReceiptNum.Text = Session["RequestedNO"].ToString();
           // RequestFrom.Text = Session["RequestedFrom"].ToString();
            //RequestDate.Text = Session["RequestedDate"].ToString();
            #region Display Products
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Sp_GetStoreOrderDetails_ByID", connection);
                command.CommandType = CommandType.StoredProcedure;
                int OrderNumber = 0;
                DataSet ds = new DataSet();

                if (int.TryParse(Session["RequestedNO"].ToString(), out OrderNumber))
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
        protected void StockDisplayGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            StockDisplayGrid.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void StockDisplayGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReceiveStock.aspx");
        }

       

        protected void StockDisplayGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            StockDisplayGrid.EditIndex= e.NewEditIndex;
            LoadData();
        }

        protected void StockDisplayGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("UpdateStock"))
            {
                   
                    GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    int RowIndex = gvr.RowIndex;
                    try
                    {
                        int sentQuan = int.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblSenQuan")).Text);
                        int recQuan = int.Parse(((TextBox)StockDisplayGrid.Rows[RowIndex].FindControl("RecQuanVal")).Text);
                        int expQuan= int.Parse(((TextBox)StockDisplayGrid.Rows[RowIndex].FindControl("ExpQuanVal")).Text);
                        int defQuan= int.Parse(((TextBox)StockDisplayGrid.Rows[RowIndex].FindControl("defQuanVal")).Text);
                        int retQuan = int.Parse(((TextBox)StockDisplayGrid.Rows[RowIndex].FindControl("retQuanVal")).Text);
                        int expQuanOrg = int.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblExpQuanOrg")).Text);
                        int defQuanOrg = int.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblDefQuanOrg")).Text);
                        int retQuanOrg = int.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblretQuanOrg")).Text);
                        int remQuan = int.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblRemainQuan")).Text);
                        string expDate = ((TextBox)StockDisplayGrid.Rows[RowIndex].FindControl("txtExpDate")).Text;
                        string status = ((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblStatus")).Text;
                        int orderedQuantity = int.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblQuantity")).Text);
                        string batch = ((TextBox)StockDisplayGrid.Rows[RowIndex].FindControl("txtBatch")).Text;
                        DateTime expiryDate= new DateTime();
                     
                        if(!DateTime.TryParse(expDate, out expiryDate))
                        {
                            WebMessageBoxUtil.Show("Expiry Date is in incorrect Format");
                            StockDisplayGrid.EditIndex = -1;
                            LoadData();
                            return;
                        }
                        if (recQuan < 0 || expQuan < 0 || defQuan < 0) 
                        {
                            WebMessageBoxUtil.Show("Entered value cannot be negative");
                            StockDisplayGrid.EditIndex = -1;
                            LoadData();
                            return;
                        }
                        if (sentQuan > (recQuan + expQuan + defQuan + retQuan)) 
                        {
                            WebMessageBoxUtil.Show("Mismatch in received and accepted quantity. Kindly correctly fill expired,defected or returned quantities");
                            StockDisplayGrid.EditIndex = -1;
                            LoadData();
                            return;
                        }
                        if (status.Equals("Partial"))
                        {
                            if (recQuan > remQuan)
                            {
                                WebMessageBoxUtil.Show("Your remaining quantity cannot be larger than " + remQuan);
                                StockDisplayGrid.EditIndex = -1;
                                LoadData();
                                return;
                            }
                            else
                            {
                                int val = 0;
                                if (retQuan != retQuanOrg ) 
                                {
                                    val += retQuan;
                                }
                                if (expQuan != expQuanOrg) 
                                {
                                    val += expQuan;
                                }
                                if (defQuan != defQuanOrg) 
                                {
                                    val += defQuan;
                                }
                                remQuan = remQuan - (val + recQuan);
                            }
                        }
                        else
                        {
                            remQuan = remQuan - (recQuan + expQuan + expQuan);
                        }
                        if (orderedQuantity >= (recQuan + expQuan + defQuan+ retQuan))
                        {
                            
                            if (retQuan > 0)
                            {
                                if (status.Equals("Partial"))
                                {
                                    if (retQuan != retQuanOrg)
                                    {
                                        UpdateStock(int.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblOrdDet_id")).Text), retQuan);
                                    }
                                }
                                else
                                {
                                    //update returned quantity
                                    UpdateStock(int.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblOrdDet_id")).Text), retQuan);
                                }
                            }
                            connection.Open();
                            SqlCommand command = new SqlCommand("Sp_StockReceiving", connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@p_OrderDetailID", int.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblOrdDet_id")).Text));
                            command.Parameters.AddWithValue("@p_ReceivedQuantity", recQuan);
                            command.Parameters.AddWithValue("@p_ExpiredQuantity", expQuan);
                            command.Parameters.AddWithValue("@p_RemainingQuantity", remQuan);
                            command.Parameters.AddWithValue("@p_DefectedQuantity", defQuan);
                            command.Parameters.AddWithValue("@p_ReturnedQuantity", retQuan);
                            command.Parameters.AddWithValue("@p_SystemType", Session["RequestDesRole"].ToString());
                            command.Parameters.AddWithValue("@p_StoreID", Session["UserSys"]);

                            command.Parameters.AddWithValue("@p_ProductID", int.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblProd_id")).Text));
                            command.Parameters.AddWithValue("@p_BarCode", ((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblbarCode")).Text);
                            command.Parameters.AddWithValue("@p_Expiry", expiryDate);
                            command.Parameters.AddWithValue("@p_DiscountPercentage", 0);
                            command.Parameters.AddWithValue("@p_Bonus", 0);
                            command.Parameters.AddWithValue("@p_BatchNumber", batch);
                            command.Parameters.AddWithValue("@p_Cost", float.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblCP")).Text));
                            command.Parameters.AddWithValue("@p_Sales", float.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblSP")).Text));
                            command.Parameters.AddWithValue("@p_orderMasterID", int.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblOrdMs_id")).Text));
                            command.Parameters.AddWithValue("@p_isInternal", "TRUE");
                            command.Parameters.AddWithValue("@p_isPO", "FALSE");
                            if (int.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblQuantity")).Text) > recQuan)
                            {
                                if (Session["RequestDesRole"].ToString().ToLower().Equals("warehouse"))
                                {
                                    command.Parameters.AddWithValue("@p_comments", "Sent to warehouse");
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@p_comments", "Sent to store");
                                }
                            }
                            else 
                            {
                                command.Parameters.AddWithValue("@p_comments", "Completed");
                            }
                            command.ExecuteNonQuery();
                            WebMessageBoxUtil.Show("Stock Successfully Added");
                        }
                        else 
                        {
                            WebMessageBoxUtil.Show("The entered value is larger than the requested value");
                            StockDisplayGrid.EditIndex = -1;
                            //LoadData();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        connection.Close();
                        StockDisplayGrid.EditIndex = -1;
                        LoadData();
                    }
         
                }
           
        }

        private void UpdateStock(int orderDetailID, int quantity)
        {
            try
            {
                int requesteeID = int.Parse(Session["RequestDesID"].ToString());
                DataSet stockDet;
                connection.Open();
                SqlCommand command = new SqlCommand("Sp_GetStockBy_OrderDetID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@p_OrderDetailID", orderDetailID);
                command.Parameters.AddWithValue("@p_StoredAt", requesteeID);

                DataSet ds = new DataSet();
                SqlDataAdapter dA = new SqlDataAdapter(command);
                dA.Fill(ds);
                stockDet = ds;
                Dictionary<int, int> stockSet = new Dictionary<int, int>();

                foreach (DataRow row in stockDet.Tables[0].Rows)
                {
                    stockSet.Add(int.Parse(row["StockID"].ToString()), quantity);//only set to the first stock
                    break;
                }


                foreach (int id in stockSet.Keys)
                {
                    command = new SqlCommand("Sp_UpdateStockBy_StockID", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@p_StockID", id);
                    command.Parameters.AddWithValue("@p_quantity", stockSet[id]);
                    command.Parameters.AddWithValue("@p_Action", "Add");
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception exp) { }
            finally
            {
                connection.Close();
                //Response.Redirect("Warehouse_StoreRequests.aspx");
            }
        }
        protected Boolean IsStatusNotComplete(String status)
        {
            if(status.Equals("Complete") ||status.Equals("Initiated"))
            {
                return false;
            }
            else
            return true;
        }
        protected void StockDisplayGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            StockDisplayGrid.EditIndex = -1;
            LoadData();
        }
    }
}