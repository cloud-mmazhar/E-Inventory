<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageProducts.aspx.cs" Inherits="IMS.ManageProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <div class="row">
        <h3>Product Management</h3>
         <asp:Button ID="btnAddProduct" runat="server" CssClass="btn btn-primary btn-large" Text="Add Product" OnClick="btnAddProduct_Click"/>
        <asp:Button ID="btnGoBack" runat="server" CssClass="btn btn-primary btn-large" Text="Go Back" OnClick="btnBack_Click"/>
         <asp:Button ID="btnDeleteProduct" runat="server" CssClass="btn btn-primary btn-large" Text="Delete Product" OnClick="btnDeleteProduct_Click" Visible="False"/>
         <asp:Button ID="btnEditProduct" runat="server" CssClass="btn btn-primary btn-large" Text="Edit Product" OnClick="btnEditProduct_Click" Visible="False"/>
     </div>
    <div class="form-horizontal">
    <div class="form-group">
        <asp:GridView ID="StockDisplayGrid" CssClass="table table-striped table-bordered table-condensed" runat="server" AllowPaging="True" PageSize="10" 
                AutoGenerateColumns="false" OnPageIndexChanging="StockDisplayGrid_PageIndexChanging"   onrowcancelingedit="StockDisplayGrid_RowCancelingEdit" 
            onrowcommand="StockDisplayGrid_RowCommand" OnRowDataBound="StockDisplayGrid_RowDataBound" onrowdeleting="StockDisplayGrid_RowDeleting" 
            onrowediting="StockDisplayGrid_RowEditing" >
                 <Columns>
                     <asp:TemplateField HeaderText="UPC">
                        <ItemTemplate>
                            <asp:Label ID="UPC" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("Product_Id_Org") %>' Width="110px"></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  Width="120px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="GreenRain Code">
                        <ItemTemplate>
                            <asp:Label ID="GreenRain" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("ItemCode") %>' Width="130px"></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  Width="140px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Product Name">
                        <ItemTemplate>
                            <asp:Label ID="ProductName" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("Product_Name") %>' Width="330px"></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  Width="330px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Product Type">
                        <ItemTemplate>
                            <asp:Label ID="Type" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("DrugType") %>' Width="130px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="130px" HorizontalAlign="Left"/>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Unit Cost Price">
                        <ItemTemplate>
                            <asp:Label ID="UnitCost" CssClass="col-md-2 control-label"  runat="server" Text='<%# Eval("UnitCost") %>' Width="100px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="110px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Unit Sale Price">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitSalePrice" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("SP") %>' Width="100px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="110px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton CssClass="btn btn-default" ID="btnEdit" Text="Edit" runat="server" CommandName="Edit" CommandArgument='<%# Container.DisplayIndex  %>'/>
                            <span onclick="return confirm('Are you sure you want to delete this record?')">
                                <asp:LinkButton CssClass="btn btn-default" ID="btnDelete" Text="Delete" runat="server" CommandName="Delete" CommandArgument='<%# Container.DisplayIndex  %>'/>
                            </span>
                        </ItemTemplate>
                         <ItemStyle  Width="180px" HorizontalAlign="Left"/>
                    </asp:TemplateField>
                 </Columns>
             </asp:GridView>
        
    </div>
    </div>
</asp:Content>
