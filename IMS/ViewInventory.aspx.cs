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
    public partial class ViewInventory : System.Web.UI.Page
    {
        public static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IMSConnectionString"].ToString());
        public static DataSet ProductSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            #region Getting Product Details
            try
            {
                int id;
                if (int.TryParse(Session["UserSys"].ToString(), out id))
                {
                    String Query = "Select tblStock_Detail.ProductID AS ProductID ,tbl_ProductMaster.Product_Name AS ProductName, tblStock_Detail.BarCode AS BarCode, tblStock_Detail.Quantity AS Qauntity, tblStock_Detail.ExpiryDate As Expiry, tblStock_Detail.UCostPrice AS CostPrice, tblStock_Detail.USalePrice AS SalePrice, tbl_System.SystemName AS Location From  tblStock_Detail INNER JOIN tbl_ProductMaster ON tblStock_Detail.ProductID = tbl_ProductMaster.ProductID INNER JOIN tbl_System ON tblStock_Detail.StoredAt = tbl_System.SystemID AND tblStock_Detail.StoredAt = '" + id.ToString() + "'";

                    connection.Open();
                    SqlCommand command = new SqlCommand(Query, connection);
                    SqlDataAdapter SA = new SqlDataAdapter(command);
                    SA.Fill(ds);
                    StockDisplayGrid.DataSource = ds;
                    StockDisplayGrid.DataBind();
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

        protected void StockDisplayGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            StockDisplayGrid.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void StockDisplayGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Print"))
                {
                    Image BarcodeImage=new Image();
                    //int 
                    //Label Barcode = (Label)StockDisplayGrid.Rows[StockDisplayGrid.].FindControl("BarCode");
                  //  Label Quantity = (Label)StockDisplayGrid.Rows[StockDisplayGrid.EditIndex].FindControl("lblQuantity");

                    long ProductBarCode = long.Parse(e.CommandArgument.ToString());
                   // int PrintQuantity = Int32.Parse(Quantity.Text.ToString());

                    #region barcode creation
                    System.Drawing.Image barcodeImage = null;
                    BarcodeLib.Barcode b = new BarcodeLib.Barcode();
                    b.IncludeLabel = true;
                    barcodeImage = b.Encode(BarcodeLib.TYPE.EAN13, ProductBarCode.ToString(), System.Drawing.ColorTranslator.FromHtml("#" + "000000"), System.Drawing.ColorTranslator.FromHtml("#" + "FFFFFF"), 300, 150);
                    //string strImageURL = "generateBarCode.aspx?d=" + ProductBarCode + "&h=" + 300 + "&w=" + 150;
                    //BarcodeImage.ImageUrl = strImageURL;
                    //BarcodeImage.Width = 300;
                    //BarcodeImage.Height = 150;

                    BarcodeUtility bUtil = new BarcodeUtility();
                    bUtil.Print(barcodeImage);
                    #endregion
                    
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageStocks.aspx", false);
        }

        protected void StockDisplayGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}