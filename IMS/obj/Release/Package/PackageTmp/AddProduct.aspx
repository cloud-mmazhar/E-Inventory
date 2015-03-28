<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="IMS.AddProduct" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="form-horizontal">
     <br />
             
     </div>
     <div class="form-horizontal">
     <asp:Label runat="server" AssociatedControlID="txtProduct" CssClass="col-md-2 control-label">Search Product </asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtProduct" CssClass="form-control product"/>
                <asp:Button runat="server" ID="btnMasterSearch" CssClass ="btn btn-default" Text="Master Search" OnClick="btnMasterSearch_Click"/>
                <br />
            </div>
     </div>
     <br />
     <br />
     <div class="form-horizontal">
         <br />
        <h4>Add Product</h4>
        <hr />
        
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="BarCodeSerial" CssClass="col-md-2 control-label">BarCode Serial</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="BarCodeSerial" CssClass="form-control" Enabled="false" />
                <br />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="GreenRainCode" CssClass="col-md-2 control-label">GreenRain Code</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="GreenRainCode" CssClass="form-control" />
                </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ProductName" CssClass="col-md-2 control-label">Product Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ProductName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ProductName" CssClass="text-danger" ErrorMessage="The product name field is required." ValidationGroup="exSave"/>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ProdcutDesc" CssClass="col-md-2 control-label">Product Description</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ProdcutDesc" CssClass="form-control" />
                <br />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ProdcutBrand" CssClass="col-md-2 control-label">Product Brand</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ProdcutBrand" CssClass="form-control" />
                <br />
            </div>
        </div>
        
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ProductType" CssClass="col-md-2 control-label">Product Type</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="ProductType" CssClass="form-control" Width="29%"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ProductType" CssClass="text-danger" ErrorMessage="The product type field is required." ValidationGroup="exSave"/>
            </div>
        </div>    

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
            <asp:Label runat="server" AssociatedControlID="ProductSubCat" CssClass="col-md-2 control-label">Product SubCategory</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="ProductSubCat" CssClass="form-control" Width="29%"/>
                <br />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ProductCost" CssClass="col-md-2 control-label">Unit Cost Price</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ProductCost" CssClass="form-control" />
                <br />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ProductSale" CssClass="col-md-2 control-label">Unit Sale Price</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ProductSale" CssClass="form-control" />
                <br />
            </div>
        </div>
        
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="WholeSalePrice" CssClass="col-md-2 control-label">WholeSale Price</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="WholeSalePrice" CssClass="form-control" />
                <br />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ProductDiscount" CssClass="col-md-2 control-label">Maximum Discount</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ProductDiscount" CssClass="form-control" />
                <br />
            </div>
        </div>
        
         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ItemForm" CssClass="col-md-2 control-label">Product Form</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ItemForm" CssClass="form-control" />
                <br />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ItemStrength" CssClass="col-md-2 control-label">Product Strength</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ItemStrength" CssClass="form-control" />
                <br />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="PackType" CssClass="col-md-2 control-label">Pack Type</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="PackType" CssClass="form-control" />
                <br />
            </div>
        </div>
        
         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="PackSize" CssClass="col-md-2 control-label">Pack Size</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="PackSize" CssClass="form-control" />
                <br />
            </div>
        </div>
        
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="shelfNumber" CssClass="col-md-2 control-label">Shelf Number</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="shelfNumber" CssClass="form-control" />
                <br />
            </div>
        </div> 

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="rackNumber" CssClass="col-md-2 control-label">Rack Number</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="rackNumber" CssClass="form-control" />
                <br />
            </div>
        </div> 

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="binNumber" CssClass="col-md-2 control-label">Bin Number</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="binNumber" CssClass="form-control" />
                <br />
            </div>
        </div> 
             
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="btnCreateProduct" runat="server" OnClick="btnCreateProduct_Click"  Text="ADD" CssClass="btn btn-default" ValidationGroup="exSave"/>
                <asp:Button ID="btnCancelProduct" runat="server" OnClick="btnCancelProduct_Click" Text="CANCEL" CssClass="btn btn-default" />
                <asp:Button ID="btnGoBack" runat="server" CssClass="btn btn-primary btn-large" Text="Go Back" OnClick="btnGoBack_Click"/>
            </div>
        </div>
    </div>
</asp:Content>
