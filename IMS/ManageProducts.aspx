<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageProducts.aspx.cs" Inherits="IMS.ManageProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <div class="row">
        <h3>Product Management</h3>
         <asp:Button ID="btnAddProduct" runat="server" CssClass="btn btn-primary btn-large" Text="Add Product" OnClick="btnAddProduct_Click"/>
         <asp:Button ID="btnDeleteProduct" runat="server" CssClass="btn btn-primary btn-large" Text="Delete Product" OnClick="btnDeleteProduct_Click"/>
         <asp:Button ID="btnEditProduct" runat="server" CssClass="btn btn-primary btn-large" Text="Edit Product" OnClick="btnEditProduct_Click"/>
         <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary btn-large" Text="Go Back" OnClick="btnBack_Click"/>
     </div>
</asp:Content>
