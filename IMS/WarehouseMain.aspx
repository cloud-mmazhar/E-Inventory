﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WarehouseMain.aspx.cs" Inherits="IMS.WarehouseMain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
        <h2>IMS Warehouse</h2>
         <asp:Button ID="btnManageInventory" runat="server" CssClass="btn btn-primary btn-large" Text="Manage Inventory" OnClick="btnManageInventory_Click"/>
         <asp:Button ID="btnManageOrders" runat="server" CssClass="btn btn-primary btn-large" Text="Manage Orders" OnClick="btnManageOrders_Click"/>
         <asp:Button ID="btnManageVendor" runat="server" CssClass="btn btn-primary btn-large" Text="Manage Vendor" OnClick="btnManageVendor_Click"/>
         <asp:Button ID="btnRegisterUser" runat="server" CssClass="btn btn-primary btn-large" Text="Register Users" OnClick="btnRegisterUser_Click" Visible="false"/>
         <asp:Button ID="tbnStoreRequests" runat="server" CssClass="btn btn-primary btn-large" Text="Store Requests" OnClick="tbnStoreRequests_Click"/>
         <asp:Button ID="ButtonBack" runat="server" CssClass="btn btn-primary btn-large" Text="Go Back" Visible="false" OnClick="ButtonBack_Click"/>
</asp:Content>
