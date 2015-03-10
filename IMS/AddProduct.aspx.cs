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
    public partial class AddProduct : System.Web.UI.Page
    {
        public static SqlConnection connection= new SqlConnection(ConfigurationManager.ConnectionStrings["IMSConnectionString"].ToString());
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Populating BarCode Serial
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Select Count(*) From tbl_ProductMaster Where tbl_ProductMaster.Product_Id_Org LIKE '444%'", connection);
                    DataSet ds = new DataSet();
                    SqlDataAdapter sA = new SqlDataAdapter(command);
                    sA.Fill(ds);

                    if (ds.Tables[0].Rows[0][0].ToString().Length.Equals(5))
                    {
                        BarCodeSerial.Text = "444" + (Int32.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1).ToString();
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString().Length.Equals(4))
                    {
                        BarCodeSerial.Text = "4440" + (Int32.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1).ToString();
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString().Length.Equals(3))
                    {
                        BarCodeSerial.Text = "44400" + (Int32.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1).ToString();
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString().Length.Equals(2))
                    {
                        BarCodeSerial.Text = "444000" + (Int32.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1).ToString();
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString().Length.Equals(1))
                    {
                        BarCodeSerial.Text = "4440000" + (Int32.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1).ToString();
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString().Length < 1)
                    {
                        BarCodeSerial.Text = "4440000" + (Int32.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1).ToString();
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

                #region Populating Product Type DropDown
                ProductType.Items.Add("Medicine (HAAD)");
                ProductType.Items.Add("Medicine (Non HAAD)");
                ProductType.Items.Add("Non Medicine");
                #endregion

                #region Populating Product Department DropDown
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Select * From tblDepartment", connection);
                    DataSet ds = new DataSet();
                    SqlDataAdapter sA = new SqlDataAdapter(command);
                    sA.Fill(ds);
                    ProductDept.DataSource = ds.Tables[0];
                    ProductDept.DataTextField = "Name";
                    ProductDept.DataValueField = "DepId";
                    ProductDept.DataBind();
                    if (ProductDept != null) 
                    {
                        ProductDept.Items.Insert(0, "Select Department");
                        ProductDept.SelectedIndex = 0;
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

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {

        }

        protected void btnDeleteProduct_Click(object sender, EventArgs e)
        {

        }

        protected void btnEditProduct_Click(object sender, EventArgs e)
        {

        }

        protected void btnGoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ManageProducts.aspx", false);
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {

        }

        protected void AddProduct_Click(object sender, EventArgs e)
        {

        }

        protected void btnCreateProduct_Click(object sender, EventArgs e)
        {
            int x = 0;
            string errorMessage = "";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("sp_InsertProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@p_BarCodeSerial", BarCodeSerial.Text.ToString());
                command.Parameters.AddWithValue("@p_ProductCode", GreenRainCode.Text.ToString());
                command.Parameters.AddWithValue("@p_ProductName", ProductName.Text.ToString());
                command.Parameters.AddWithValue("@p_Description", ProdcutDesc.Text.ToString());
                command.Parameters.AddWithValue("@p_BrandName", ProdcutBrand.Text.ToString());
                command.Parameters.AddWithValue("@p_ProductType", ProductType.SelectedItem.ToString());

                int res1,res4;
                 float res2, res3;
                if (int.TryParse(ProductSubCat.SelectedValue.ToString(), out res1))
                {
                    command.Parameters.AddWithValue("@p_SubCategoryID", res1);
                }
                else 
                {
                    command.Parameters.AddWithValue("@p_SubCategoryID", 0);
                }

                if (float.TryParse(ProductCost.Text.ToString(), out res2))
                {
                    command.Parameters.AddWithValue("@p_UnitCost", res2);
                }
                else
                {
                    command.Parameters.AddWithValue("@p_UnitCost", 0);
                }

                if (float.TryParse(ProductSale.Text.ToString(), out res3))
                {
                    command.Parameters.AddWithValue("@p_SP", res3);
                }
                else
                {
                    command.Parameters.AddWithValue("@p_SP", 0);
                }

                if (int.TryParse(ProductDiscount.Text.ToString(), out res4))
                {
                    command.Parameters.AddWithValue("@p_MaxiMumDiscount", res4);
                }
                else
                {
                    command.Parameters.AddWithValue("@p_MaxiMumDiscount", 0);
                }


                command.Parameters.AddWithValue("@p_shelf", shelfNumber.Text.ToString());
                command.Parameters.AddWithValue("@p_rack", rackNumber.Text.ToString());
                command.Parameters.AddWithValue("@p_bin", binNumber.Text.ToString());

                x = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            if(x==1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                BarCodeSerial.Text ="";
                GreenRainCode.Text = "";
                ProductName.Text = "";
                ProdcutDesc.Text = "";
                ProdcutBrand.Text = "";
                ProductType.SelectedIndex = -1;
                ProductSubCat.SelectedIndex = -1;
                ProductDept.SelectedIndex = -1;
                ProductCat.SelectedIndex = -1;
                ProductCost.Text = "";
                ProductSale.Text = "";
                ProductDiscount.Text = "";
                rackNumber.Text = "";
                shelfNumber.Text = "";
                binNumber.Text = "";
                
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(''" + errorMessage + "'')", true);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelProduct_Click(object sender, EventArgs e)
        {
                
                GreenRainCode.Text=string.Empty;
                ProductName.Text=string.Empty;
                ProdcutDesc.Text = string.Empty;
                ProdcutBrand.Text = string.Empty;
                ProductType.SelectedIndex = -1;
                ProductDept.SelectedIndex=0;
                ProductCat.Items.Clear();
                ProductSubCat.Items.Clear();
                ProductCost.Text = string.Empty;
                ProductSale.Text = string.Empty;
                ProductDiscount.Text = string.Empty;
        }

        protected void ProductDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Populating Category Dropdown
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * From tblCategory Where DepartmentID = '"+ ProductDept.SelectedValue.ToString() +"'", connection);
                DataSet ds = new DataSet();
                SqlDataAdapter sA = new SqlDataAdapter(command);
                sA.Fill(ds);
                ProductCat.DataSource = ds.Tables[0];
                ProductCat.DataTextField = "Name";
                ProductCat.DataValueField = "CategoryID";
                ProductCat.DataBind();
                if (ProductCat != null) 
                {
                    ProductCat.Items.Insert(0, "Select Category");
                    ProductCat.SelectedIndex = 0;
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

        protected void ProductCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Populating SubCategory Dropdown
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * From tblSub_Category WHERE CategoryID ='" + ProductCat.SelectedValue.ToString() + "'", connection);
                DataSet ds = new DataSet();
                SqlDataAdapter sA = new SqlDataAdapter(command);
                sA.Fill(ds);
                ProductSubCat.DataSource = ds.Tables[0];
                ProductSubCat.DataTextField = "Name";
                ProductSubCat.DataValueField = "Sub_CatID";
                ProductSubCat.DataBind();

                if (ProductSubCat != null)
                {
                    ProductSubCat.Items.Insert(0, "Select Sub Category");
                    ProductSubCat.SelectedIndex = 0;
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
}