<%@ Page Title="Inventory" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewInventory.aspx.cs" Inherits="IMS.ViewInventory" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
    <div class="form-group">
            <h1>CURRENT STOCK</h1>
        </div>
    </div>

     <div class="form-horizontal">

         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ProductDept" CssClass="col-md-2 control-label">Product Department</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="ProductDept" CssClass="form-control" Width="29%" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ProductDept_SelectedIndexChanged"/>
                <br />
            </div>
        </div>  

         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ProductCat" CssClass="col-md-2 control-label">Product Category</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="ProductCat" CssClass="form-control" Width="29%" AutoPostBack="True" OnSelectedIndexChanged="ProductCat_SelectedIndexChanged" />
                <br />
            </div>
        </div>

         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ProductSubCat" CssClass="col-md-2 control-label"> Product SubCategory </asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="ProductSubCat" CssClass="form-control" Width="29%" OnSelectedIndexChanged="ProductSubCat_SelectedIndexChanged"/>
                <br />
            </div>
        </div>

         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ProductType" CssClass="col-md-2 control-label">Product Type</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="ProductType" OnSelectedIndexChanged="ProductType_SelectedIndexChanged" CssClass="form-control" Width="29%"/>
                <br />
            </div>
        </div>

         <div class="form-group">
             <asp:Label runat="server" AssociatedControlID="Productname" CssClass="col-md-2 control-label"> Product Name </asp:Label>
             <div class="col-md-10">
                <asp:TextBox runat="server" ID="Productname" CssClass="form-control" Enabled="True" />
                 <cc1:AutoCompleteExtender runat="server" ID="ProductAutoExtender" TargetControlID="Productname" ></cc1:AutoCompleteExtender>
             </div>
         </div>

         <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Enabled="false" Text="SEARCH" CssClass="btn btn-default"/>
                <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Enabled="false" Text="REFRESH" CssClass="btn btn-default"/>
            </div>
        </div>
     </div>

    <div class="form-horizontal">
    <div class="form-group">
        <asp:GridView ID="StockDisplayGrid" runat="server" CssClass="table table-striped table-bordered table-condensed" AllowPaging="True" PageSize="10" 
                AutoGenerateColumns="false" OnPageIndexChanging="StockDisplayGrid_PageIndexChanging" 
            onrowcommand="StockDisplayGrid_RowCommand" OnRowDataBound="StockDisplayGrid_RowDataBound">
                 <Columns>
                     <asp:TemplateField HeaderText="BarCode">
                        <ItemTemplate>
                            <asp:Label ID="BarCode" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("BarCode") %>' Width="100px"></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  Width="110px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="ProductName" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("ProductName") %>' Width="310px"></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  Width="320px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("Qauntity") %>' Width="40px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="50px" HorizontalAlign="Left"/>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Expiry">
                        <ItemTemplate>
                            <asp:Label ID="lblExpiry" CssClass="col-md-2 control-label"  runat="server" Text='<%# Eval("Expiry") %>' Width="190px"></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  Width="200px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Unit Cost">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitCostPrice" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("CostPrice") %>' Width="50px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="60px" HorizontalAlign="Left"/>
                       
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Unit Sale">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitSalePrice" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("SalePrice") %>' Width="50px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="60px" HorizontalAlign="Left"/>
                       
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton CssClass="btn btn-default" ID="btnEdit" Text="Print BarCode" runat="server" CommandName="Print" CommandArgument='<%# Eval("BarCode") %>' />
                            <br />
                        </ItemTemplate>
                         <ItemStyle  Width="70px" HorizontalAlign="Left"/>
                    </asp:TemplateField>
                 </Columns>
             </asp:GridView>
        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary btn-large" Text="Go Back" OnClick="btnBack_Click"/>
    </div>
    </div>
</asp:Content>
