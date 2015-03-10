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
using System.Globalization;

namespace IMS
{
    public partial class AddStock : System.Web.UI.Page
    {
        public static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IMSConnectionString"].ToString());
        public static DataSet ProductSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProductSet = new DataSet();
                #region Populating Product List
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Select TOP 200 * From tbl_ProductMaster Where tbl_ProductMaster.Product_Id_Org LIKE '444%' AND Status = 1", connection);
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

                #region Populating System Types
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Select * From tbl_System", connection); // needs to be completed
                    DataSet ds = new DataSet();
                    SqlDataAdapter sA = new SqlDataAdapter(command);
                    sA.Fill(ds);
                    StockAt.DataSource = ds.Tables[0];
                    StockAt.DataTextField = "SystemName";
                    StockAt.DataValueField = "SystemID";
                    StockAt.DataBind();
                    if (StockAt != null)
                    {
                        StockAt.Items.Insert(0, "Select System");
                        StockAt.SelectedIndex = 0;
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
        }

        protected void btnCreateProduct_Click(object sender, EventArgs e)
        {
            #region BarCode Generation

            DateTime dateValue = (Convert.ToDateTime(DateTextBox.Text.ToString()));
           
            string p1;
            long BarCode=0;
            String mm = dateValue.Month.ToString();
            String yy = dateValue.ToString("yy", DateTimeFormatInfo.InvariantInfo);
            p1 = BarCodeSerial.Text + mm+yy;
                
            if (long.TryParse(p1, out BarCode))
            {
            }
            else
            {
               //post error message 
            }
            
         
            #endregion

            #region Adding Stock
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_AddStock", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@p_ProductID", Int32.Parse(SelectProduct.SelectedValue.ToString()));
                command.Parameters.AddWithValue("@p_Quantity", Decimal.Parse(Quantity.Text.ToString()));
                command.Parameters.AddWithValue("@p_Status", "1");
                command.Parameters.AddWithValue("@p_UserRoleID", Int32.Parse(StockAt.SelectedValue.ToString()));
                command.Parameters.AddWithValue("@p_BarCode", BarCode);
                command.Parameters.AddWithValue("@p_Expiry", DateTextBox.Text); // Calender Date or DateTime Picker Date
                command.Parameters.AddWithValue("@p_Cost", Decimal.Parse(ProductCost.Text.ToString()));
                command.Parameters.AddWithValue("@p_Sales", Decimal.Parse(ProductSale.Text.ToString()));
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
        }

        protected void btnDeleteProduct_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelProduct_Click(object sender, EventArgs e)
        {
            SelectProduct.SelectedIndex = 0;
            BarCodeSerial.Text = string.Empty;
            ProductName.Text = string.Empty;
            Quantity.Text = string.Empty;
            DateTextBox.Text = string.Empty;
            StockAt.Items.Clear();
            ProductCost.Text = string.Empty;
            ProductSale.Text = string.Empty;
            btnCreateProduct.Enabled = false;
        }

        protected void SelectProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            #region Getting Product Details
            try
            {
                DataView dv = new DataView();
                dv = ProductSet.Tables[0].DefaultView;
                dv.RowFilter = "ProductID = '" + SelectProduct.SelectedValue.ToString() + "'";
                dt = dv.ToTable();

                BarCodeSerial.Text = dt.Rows[0]["Product_Id_Org"].ToString();
                ProductName.Text = dt.Rows[0]["Product_Name"].ToString();
                ProductCost.Text = dt.Rows[0]["UnitCost"].ToString();
                ProductSale.Text = dt.Rows[0]["SP"].ToString();
                btnCreateProduct.Enabled = true;
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageStocks.aspx", false);
        }
    }
}