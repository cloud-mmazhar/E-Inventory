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
using System.Data.OleDb;
using System.IO;

namespace IMS
{
    public partial class HAADPopulation : System.Web.UI.Page
    {
        public static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IMSConnectionString"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
        }
        public  void HaadLIST()
        {
            StreamReader srW = new StreamReader(@"G:\HaadDrug.csv");
            String[] Values = new String[10];
            while (srW.ReadLine() != null)
            {
                String TextLine = srW.ReadLine();
                Values = TextLine.Split(',');

                String GreenRain = Values[0];
                String ProductName = Values[2];
                String GenericName = Values[3];
                String Strength = Values[4];
                String Form = Values[5];
                String PackSize = Values[6];
                String UnitSalePrice = Values[9];
                String UnitCostPrice = Values[10];
                String Status = Values[11];
                String Manufacturer = Values[15];
                String BarCodeSerial = "";

                #region Populating BarCode Serial
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Select Count(*) From tbl_ProductMaster Where DrugType = 'MEDICINE(HAAD)' AND ItemCode IS NOT NULL", connection);
                    DataSet ds = new DataSet();
                    SqlDataAdapter sA = new SqlDataAdapter(command);
                    sA.Fill(ds);

                    if (ds.Tables[0].Rows[0][0].ToString().Length.Equals(7))
                    {
                        BarCodeSerial = "1" + (Int32.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1).ToString();
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString().Length.Equals(6))
                    {
                        BarCodeSerial = "10" + (Int32.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1).ToString();
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString().Length.Equals(5))
                    {
                        BarCodeSerial = "100" + (Int32.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1).ToString();
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString().Length.Equals(4))
                    {
                        BarCodeSerial = "1000" + (Int32.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1).ToString();
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString().Length.Equals(3))
                    {
                        BarCodeSerial = "10000" + (Int32.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1).ToString();
                    }

                    else if (ds.Tables[0].Rows[0][0].ToString().Length.Equals(2))
                    {
                        BarCodeSerial = "100000" + (Int32.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1).ToString();
                    }

                    else if (ds.Tables[0].Rows[0][0].ToString().Length.Equals(1))
                    {
                        BarCodeSerial = "1000000" + (Int32.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1).ToString();
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString().Length < 1)
                    {
                        BarCodeSerial = "1000000" + (Int32.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1).ToString();
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

                #region Creation Product
                string errorMessage = "";
                int x = 0;
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("sp_InsertProduct", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@p_BarCodeSerial", BarCodeSerial.ToString());
                    command.Parameters.AddWithValue("@p_ProductCode", GreenRain.ToString());
                    command.Parameters.AddWithValue("@p_ProductName", ProductName.ToString());
                    command.Parameters.AddWithValue("@p_Description", ProductName.ToString());
                    command.Parameters.AddWithValue("@p_BrandName", GenericName.ToString());
                    command.Parameters.AddWithValue("@p_ProductType", "MEDICINE(HAAD)");
                    int res1, res4;
                    float res2, res3, res5;

                    command.Parameters.AddWithValue("@p_SubCategoryID", 0);

                    if (float.TryParse(UnitCostPrice.ToString(), out res2))
                    {
                        command.Parameters.AddWithValue("@p_UnitCost", res2);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@p_UnitCost", 0);
                    }

                    if (float.TryParse(UnitSalePrice.ToString(), out res3))
                    {
                        command.Parameters.AddWithValue("@p_SP", res3);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@p_SP", 0);
                    }

                    command.Parameters.AddWithValue("@p_MaxiMumDiscount", 0);

                    command.Parameters.AddWithValue("@p_AWT", 0);



                    command.Parameters.AddWithValue("@p_form", Form.ToString());
                    command.Parameters.AddWithValue("@p_strength", Strength.ToString());
                    command.Parameters.AddWithValue("@p_packtype", "0");
                    command.Parameters.AddWithValue("@p_packsize", PackSize.ToString());

                    command.Parameters.AddWithValue("@p_shelf", "0");
                    command.Parameters.AddWithValue("@p_rack", "0");
                    command.Parameters.AddWithValue("@p_bin", "0");

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

                if (x == 1)
                {
                    WebMessageBoxUtil.Show("Record Inserted Successfully");
                }
                else
                {
                    WebMessageBoxUtil.Show(errorMessage);
                }
                #endregion

            }

            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HaadLIST();
        }
    }
}