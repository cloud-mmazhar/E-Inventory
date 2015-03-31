<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AcceptPurchaseOrder.aspx.cs" Inherits="IMS.AcceptPurchaseOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
          
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <%--<script src="Scripts/jquery.js"  type="text/javascript"></script>
          <script src="Scripts/jquery-ui.js" type="text/javascript"></script>
          <link rel="stylesheet" href="Style/jquery-ui.css" />
          <script>
              $(function () { $("#<%= txtExpDate.ClientID %>").datepicker(); });

          </script>--%>
      <h3> Purchase Order Details</h3> 
    <table>
        <tr>
            <td>
               <b>Receipt Number :</b>
            </td>
            <td>
                <div class="col-md-10">
                    <asp:Label runat="server" ID="ReceiptNum" CssClass="col-md-2 control-label"/>
                </div>

            </td>
            </tr>
  
    </table>
     <br />
    <div class="form-horizontal">
    <div class="form-group">
        <asp:GridView ID="StockDisplayGrid" CssClass="table table-striped table-bordered table-condensed"  Visible="true" runat="server" AllowPaging="True" PageSize="10" 
                AutoGenerateColumns="false" OnPageIndexChanging="StockDisplayGrid_PageIndexChanging" OnRowCancelingEdit="StockDisplayGrid_RowCancelingEdit" OnRowCommand="StockDisplayGrid_RowCommand" OnRowDataBound="StockDisplayGrid_RowDataBound" OnRowEditing="StockDisplayGrid_RowEditing">
                 <Columns>

                     <asp:TemplateField HeaderText="Product Name" HeaderStyle-Width ="280px">
                        <ItemTemplate>
                            <asp:Label ID="ProductName"  runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  Width="280px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Ordered Quantity" HeaderStyle-Width ="150px">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity"  runat="server" Text='<%# Eval("OrderedQuantity") %>' ></asp:Label>
                        </ItemTemplate>
                          <ItemStyle  Width="150px" HorizontalAlign="Left"/>
                    </asp:TemplateField>
                    
                      <asp:TemplateField Visible="false" HeaderText="Delivery Date" HeaderStyle-Width ="150px">
                        <ItemTemplate>
                            <asp:Label ID="lblDDate"  runat="server" Text='<%# Eval("SendDate") %>' ></asp:Label>
                        </ItemTemplate>
                          <ItemStyle  Width="150px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Received Quantity" HeaderStyle-Width ="110px">
                        <ItemTemplate>
                            <asp:Label ID="lblSenQuan" runat="server" Text=' <%#Eval("ReceivedQuantity")==DBNull.Value?0:int.Parse( Eval("ReceivedQuantity").ToString())  %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="110px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Accepted Quantity" HeaderStyle-Width ="110px">
                        <ItemTemplate>
                            <asp:Label ID="lblRecQuan" runat="server" Text=' <%#Eval("ReceivedQuantity")==DBNull.Value?0:int.Parse( Eval("ReceivedQuantity").ToString())  %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="RecQuanVal"  runat="server" Text=' <%#Eval("ReceivedQuantity")==DBNull.Value?0:int.Parse( Eval("ReceivedQuantity").ToString())  %>'></asp:TextBox>
                         </EditItemTemplate>
                        <ItemStyle  Width="110px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Expired Quantity" HeaderStyle-Width ="110px">
                        <ItemTemplate>
                            <asp:Label ID="lblExpQuan" runat="server" Text=' <%#Eval("ExpiredQuantity")==DBNull.Value?0:int.Parse( Eval("ExpiredQuantity").ToString())  %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="ExpQuanVal"  runat="server" Text=' <%#Eval("ExpiredQuantity")==DBNull.Value?0:int.Parse( Eval("ExpiredQuantity").ToString())  %>'></asp:TextBox>
                         </EditItemTemplate>
                        <ItemStyle  Width="110px" HorizontalAlign="Left"/>
                    </asp:TemplateField>
                     
                      <asp:TemplateField HeaderText="Defected Quantity" HeaderStyle-Width ="110px">
                        <ItemTemplate>
                            <asp:Label ID="lblDefQuan" runat="server" Text=' <%#Eval("DefectedQuantity")==DBNull.Value?0:int.Parse( Eval("DefectedQuantity").ToString())  %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="defQuanVal"  runat="server" Text=' <%#Eval("DefectedQuantity")==DBNull.Value?0:int.Parse( Eval("DefectedQuantity").ToString())  %>'></asp:TextBox>
                         </EditItemTemplate>
                        <ItemStyle  Width="110px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Returned Quantity" HeaderStyle-Width ="110px">
                        <ItemTemplate>
                            <asp:Label ID="lblRetQuan" runat="server" Text=' <%#Eval("ReturnedQuantity")==DBNull.Value?0:int.Parse( Eval("ReturnedQuantity").ToString())  %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:TextBox ID="retQuanVal"  runat="server" Text=' <%#Eval("ReturnedQuantity")==DBNull.Value?0:int.Parse( Eval("ReturnedQuantity").ToString())  %>'></asp:TextBox>
                         </EditItemTemplate>
                        <ItemStyle  Width="110px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Cost Price" HeaderStyle-Width ="110px">
                        <ItemTemplate>
                            <asp:Label ID="lblCP"  runat="server" Text='<%# Eval("UnitCost") %>'></asp:Label>
                        </ItemTemplate>
                         <EditItemTemplate>
                             <asp:TextBox ID="retCP"  runat="server" Text=' <%#Eval("UnitCost")==DBNull.Value?0:float.Parse( Eval("UnitCost").ToString())  %>'></asp:TextBox>
                         </EditItemTemplate>
                         <ItemStyle  Width="110px" HorizontalAlign="Left"/>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Sale Price" HeaderStyle-Width ="110px">
                        <ItemTemplate>
                            <asp:Label ID="lblSP"  runat="server" Text='<%# Eval("UnitSale") %>'></asp:Label>
                        </ItemTemplate>
                          <EditItemTemplate>
                             <asp:TextBox ID="retSP"  runat="server" Text=' <%#Eval("UnitSale")==DBNull.Value?0:float.Parse( Eval("UnitSale").ToString())  %>'></asp:TextBox>
                         </EditItemTemplate>
                         <ItemStyle  Width="110px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Expiry Date" HeaderStyle-Width ="110px">
                        <ItemTemplate>
                            <asp:Label ID="lblExpDate" runat="server" Text='<%# Eval("Expiry") %>'></asp:Label>
                        </ItemTemplate>
                          <EditItemTemplate>
                             <asp:TextBox ID="txtExpDate"  runat="server" Text=' <%#Eval("ExpiredQuantity")==DBNull.Value?0:int.Parse( Eval("ExpiredQuantity").ToString())  %>'></asp:TextBox>
                         </EditItemTemplate>
                        <ItemStyle  Width="110px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="BarCode" Visible="false" HeaderStyle-Width ="110px">
                        <ItemTemplate>
                            <asp:Label ID="lblbarCode" runat="server" Text='<%# Eval("BarCode") %>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  Width="110px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Action" HeaderStyle-Width ="200px">
                        <ItemTemplate>
                            <asp:Button CssClass="btn btn-default" ID="btnEdit" Text="Accept" runat="server" CommandName="Edit"/>
                             <%--CommandArgument='<%# Container.DisplayIndex  %>'--%>
                        </ItemTemplate>
                        <EditItemTemplate>
                           <asp:LinkButton ID="btnUpdate" Text="Update" runat="server" CommandName="UpdateStock" />
                            <br />
                            <asp:LinkButton  ID="btnCancel" Text="Cancel" runat="server" CommandName="Cancel" />
                        </EditItemTemplate>
                         
                    </asp:TemplateField>
                     <%-- Hidden Fields --%>
                     <asp:TemplateField HeaderText="Order Detail ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdDet_id" runat="server" Text='<%# Eval("OrderDetailID") %>'></asp:Label>
                            </ItemTemplate>
                      </asp:TemplateField>
                      
                     <asp:TemplateField HeaderText="Product ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblProd_id" runat="server" Text='<%# Eval("ProductID") %>'></asp:Label>
                            </ItemTemplate>
                      </asp:TemplateField>
                     <asp:TemplateField HeaderText="Order Master ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdMs_id" runat="server" Text='<%# Eval("OrderNO") %>'></asp:Label>
                            </ItemTemplate>
                      </asp:TemplateField>
                     <asp:TemplateField HeaderText="Barcode Serial" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblBrSerial" runat="server" Text='<%# Eval("barcodeSerial") %>'></asp:Label>
                            </ItemTemplate>
                      </asp:TemplateField>
                 </Columns>
             </asp:GridView>
             <br />
            
             <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="BACK" CssClass="btn btn-large" Visible="true" />
    </div>

    <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
               
            </div>
        </div>
    </div>
</asp:Content>
