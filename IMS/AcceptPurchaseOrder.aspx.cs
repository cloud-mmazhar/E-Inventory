using IMSCommon.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS
{
    public partial class AcceptPurchaseOrder : System.Web.UI.Page
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
                SqlCommand command = new SqlCommand("Sp_GetPODetails_ByID", connection);
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label ProductStrength = (Label)e.Row.FindControl("ProductStrength2");
                Label Label1 = (Label)e.Row.FindControl("Label1");

                Label dosage = (Label)e.Row.FindControl("dosage2");
                Label Label2 = (Label)e.Row.FindControl("Label2");

                Label packSize = (Label)e.Row.FindControl("packSize2");
                Label Label3 = (Label)e.Row.FindControl("Label3");

                if (String.IsNullOrWhiteSpace(ProductStrength.Text))
                {
                    ProductStrength.Visible = false;
                    Label1.Visible = false;
                }
                else
                {
                    ProductStrength.Visible = true;
                    Label1.Visible = true;
                }

                if (String.IsNullOrWhiteSpace(dosage.Text))
                {
                    dosage.Visible = false;
                    Label2.Visible = false;
                }
                else
                {
                    dosage.Visible = true;
                    Label2.Visible = true;
                }

                if (String.IsNullOrWhiteSpace(packSize.Text))
                {
                    packSize.Visible = false;
                    Label3.Visible = false;
                }
                else
                {
                    packSize.Visible = true;
                    Label3.Visible = true;
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewPurchaseOrders.aspx");
        }



        protected void StockDisplayGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            StockDisplayGrid.EditIndex = e.NewEditIndex;
            LoadData();
        }

        protected Boolean IsStatusNotComplete(String status)
        {
            if (status.Equals("Complete") || status.Equals("Initiated"))
            {
                return false;
            }
            else
                return true;
        }

        protected void StockDisplayGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("UpdateStock"))
            {

                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;
                try
                {
                    int recQuan = int.Parse(((TextBox)StockDisplayGrid.Rows[RowIndex].FindControl("RecQuanVal")).Text);
                    int expQuan = int.Parse(((TextBox)StockDisplayGrid.Rows[RowIndex].FindControl("ExpQuanVal")).Text);
                    int defQuan = int.Parse(((TextBox)StockDisplayGrid.Rows[RowIndex].FindControl("defQuanVal")).Text);
                    int retQuan = int.Parse(((TextBox)StockDisplayGrid.Rows[RowIndex].FindControl("retQuanVal")).Text);
                    int remQuan = int.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblRemainQuan")).Text);
                    float txtCP = float.Parse(((TextBox)StockDisplayGrid.Rows[RowIndex].FindControl("retCP")).Text);
                    float txtSP = float.Parse(((TextBox)StockDisplayGrid.Rows[RowIndex].FindControl("retSP")).Text);
                    int orderedQuantity = int.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblQuantity")).Text);
                    string  barcode=((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblbarCode")).Text;
                    string expDate= ((TextBox)StockDisplayGrid.Rows[RowIndex].FindControl("txtExpDate")).Text;
                    string status = ((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblStatus")).Text;
                    int bonusOrg = int.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblBonusOrg")).Text);
                    string batch= ((TextBox)StockDisplayGrid.Rows[RowIndex].FindControl("txtBatch")).Text;
                    int bonusQuan = 0;
                    string bonusTxt=((TextBox)StockDisplayGrid.Rows[RowIndex].FindControl("txtBonus")).Text;
                    DateTime expiryDate = new DateTime();
                    DateTime.TryParse(expDate, out expiryDate);
                    //if (!)
                    //{
                    //    //WebMessageBoxUtil.Show("Expiry Date is in incorrect Format");
                    //    //StockDisplayGrid.EditIndex = -1;
                    //    //LoadData();
                    //    //return;
                    //}
                    if (!int.TryParse(bonusTxt, out bonusQuan))
                    {
                        WebMessageBoxUtil.Show("Invalid Format for Bonus");
                        StockDisplayGrid.EditIndex = -1;
                        LoadData();
                        return;
                    }

                    float txtDisc=0;
                    if (!float.TryParse(((TextBox)StockDisplayGrid.Rows[RowIndex].FindControl("txtDisc")).Text, out txtDisc))
                    {
                        WebMessageBoxUtil.Show("Invalid Format for Discount");
                        StockDisplayGrid.EditIndex = -1;
                        LoadData();
                        return;
                    }
                    long newBarcode = 0;
                    if(barcode.Equals("0"))
                    {
                        if (!string.IsNullOrEmpty(expDate))
                        {
                            DateTime dateValue = (Convert.ToDateTime(expDate));

                            string p1;
                            String mm;//= dateValue.Month.ToString();
                            if (dateValue.Month < 10)
                            {
                                mm = dateValue.Month.ToString().PadLeft(2, '0');

                            }
                            else
                            {
                                mm = dateValue.Month.ToString();
                            }
                            String yy = dateValue.ToString("yy", DateTimeFormatInfo.InvariantInfo);
                            p1 = ((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblBrSerial")).Text + mm + yy;

                            if (long.TryParse(p1, out newBarcode))
                            {
                            }
                            else
                            {
                                //post error message 
                            }
                        }
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
                            remQuan = remQuan - (recQuan + expQuan + expQuan);
                        }
                        if (bonusQuan == bonusOrg)
                        {
                            bonusQuan = 0;
                        }
                    }
                    else 
                    {
                        remQuan = remQuan - (recQuan + expQuan + expQuan);
                    }

                    if (txtCP < 0 || txtSP < 0) 
                    {
                        WebMessageBoxUtil.Show("Entered value cannot be negative");
                        StockDisplayGrid.EditIndex = -1;
                        LoadData();
                        return;
                    }

                    if (recQuan < 0 || expQuan < 0 || expQuan < 0)
                    {
                        WebMessageBoxUtil.Show("Entered value cannot be negative");
                        StockDisplayGrid.EditIndex = -1;
                        LoadData();
                        return;
                    }
                    if (orderedQuantity >= (recQuan + expQuan + defQuan + retQuan))
                    {
                        int requesteeID = int.Parse(Session["RequestDesID"].ToString());
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
                        command.Parameters.AddWithValue("@p_BarCode", newBarcode);
                        command.Parameters.AddWithValue("@p_DiscountPercentage", txtDisc);
                        command.Parameters.AddWithValue("@p_Bonus", bonusQuan);
                        command.Parameters.AddWithValue("@p_BatchNumber",  batch);
                        if (string.IsNullOrEmpty(expDate))
                        {
                            command.Parameters.AddWithValue("@p_Expiry", DBNull.Value);
                        }
                        else 
                        {
                            command.Parameters.AddWithValue("@p_Expiry", expiryDate);
                        }
                        command.Parameters.AddWithValue("@p_Cost", txtCP);
                        command.Parameters.AddWithValue("@p_Sales", txtSP);
                        command.Parameters.AddWithValue("@p_orderMasterID", int.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblOrdMs_id")).Text));
                        command.Parameters.AddWithValue("@p_isInternal", "TRUE");
                        command.Parameters.AddWithValue("@p_isPO", "TRUE");

                        if (int.Parse(((Label)StockDisplayGrid.Rows[RowIndex].FindControl("lblQuantity")).Text) > recQuan)
                        {
                            
                            command.Parameters.AddWithValue("@p_comments", "Sent to Vendor");
                            
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

        protected void StockDisplayGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            StockDisplayGrid.EditIndex = -1;
            LoadData();
        }
    }
}