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
    public partial class ManageProducts : System.Web.UI.Page
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
                    String Query = "Select * FROM tbl_ProductMaster";

                    connection.Open();
                    SqlCommand command = new SqlCommand(Query, connection);
                    SqlDataAdapter SA = new SqlDataAdapter(command);
                    ProductSet = null;
                    SA.Fill(ds);
                    ProductSet = ds;
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
        protected void btnManageProducts_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            Session["MODE"] = "ADD";
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

        protected void StockDisplayGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            StockDisplayGrid.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void StockDisplayGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void StockDisplayGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Edit"))
                {
                    #region Updating Product
                    Label ItemNo = (Label)StockDisplayGrid.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("Product_Id_Org");
                    Label ItemName = (Label)StockDisplayGrid.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("Product_Name");
                    Label ItemType = (Label)StockDisplayGrid.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("DrugType");
                    Label GreenRainCode = (Label)StockDisplayGrid.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("ItemCode");
                    Label UnitSale = (Label)StockDisplayGrid.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("SP");
                    Label UnitCost = (Label)StockDisplayGrid.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("UnitCost");


                    

                    Session["PageMasterProduct"] = "true";
                    Session["MODE"] = "EDIT";
                   
                    Session["MS_ItemNo"] = ItemNo.Text.ToString();
                    Session["MS_ItemName"] = ItemName.Text.ToString();
                    Session["MS_ItemType"] = ItemType.Text.ToString();
                    
                    DataView dv = ProductSet.Tables[0].DefaultView;
                    dv.RowFilter = "Product_Id_Org = " + ItemNo.Text;
                    DataTable dt = dv.ToTable();

                    Session["MS_GenericName"] = dt.Rows[0]["GName"].ToString();
                    Session["MS_Control"] = dt.Rows[0]["Control"].ToString();
                    Session["MS_BinNumber"] = dt.Rows[0]["binNumber"].ToString();
                    Session["MS_GreenRainCode"] = GreenRainCode.Text.ToString();
                    Session["MS_BrandName"] = dt.Rows[0]["Brand_Name"].ToString();
                    Session["MS_MaxiMumDiscount"] = dt.Rows[0]["MaxiMumDiscount"].ToString();
                    Session["MS_LineID"] = dt.Rows[0]["LineID"].ToString();
                    Session["MS_UnitSale"] = UnitSale.Text.ToString();
                    Session["MS_UnitCost"] = UnitCost.Text.ToString();
                    Session["MS_itemAWT"] = dt.Rows[0]["itemAWT"].ToString();
                    Session["MS_itemForm"] = dt.Rows[0]["itemForm"].ToString();
                    Session["MS_itemStrength"] = dt.Rows[0]["itemStrength"].ToString();
                    Session["MS_itemPackType"] = dt.Rows[0]["itemPackType"].ToString();
                    Session["MS_itemPackSize"] = dt.Rows[0]["itemPackSize"].ToString();

                    Response.Redirect("Addproduct.aspx");
                    #endregion

                }
                else if (e.CommandName.Equals("Delete"))
                {

                    #region Getting Product Details
                    try
                    {
                            Label ItemNo = (Label)StockDisplayGrid.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("Product_Id_Org");
                            connection.Open();
                            SqlCommand command = new SqlCommand("sp_DeleteProduct", connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@p_ItemNo", Convert.ToInt32(ItemNo.Text));
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
            }
            catch(Exception ex)
            {

            }
            finally
            {

            }

        }

        protected void StockDisplayGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void StockDisplayGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void StockDisplayGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            StockDisplayGrid.EditIndex = e.NewEditIndex;
            BindGrid();
        }
    }
}