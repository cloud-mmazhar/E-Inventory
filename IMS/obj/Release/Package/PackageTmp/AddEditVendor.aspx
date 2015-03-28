<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEditVendor.aspx.cs" Inherits="IMS.AddEditVendor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <br />
     <div class="form-horizontal">
        <h4 id="regTitleWH" runat="server">Add Vendor</h4>
        <h4 id="EditTitleWH" visible="false" runat="server">Edit Vendor</h4>
        <hr />
        
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtVendorName" CssClass="col-md-2 control-label">Vendor Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtVendorName" CssClass="form-control" />
                <br />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" CssClass="text-danger" ErrorMessage="The product greenrain Code field is required."  ValidationGroup="exSave"/>--%>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtcity" CssClass="col-md-2 control-label">City</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtcity" CssClass="form-control" />
                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="ProductName" CssClass="text-danger" ErrorMessage="The product name field is required." ValidationGroup="exSave"/>--%>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtState" CssClass="col-md-2 control-label">State</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtState" CssClass="form-control" />
                <br />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtCounty" CssClass="col-md-2 control-label">Country</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtCounty" CssClass="form-control" />
                <br />
            </div>
        </div>
        
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtaddress" CssClass="col-md-2 control-label">Address</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtaddress" CssClass="form-control" />
                <br />
            </div>
        </div>    

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtphone" CssClass="col-md-2 control-label">Phone</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtphone" CssClass="form-control" />
                <br />
            </div>
        </div>  

         <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtfax" CssClass="col-md-2 control-label">Fax</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtfax" CssClass="form-control" />
                <br />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtmobile" CssClass="col-md-2 control-label">Mobile</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtmobile" CssClass="form-control" />
                <br />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtpager" CssClass="col-md-2 control-label">Pager</asp:Label>
           <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtpager" CssClass="form-control" />
                <br />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtpager" CssClass="col-md-2 control-label">Contact Person</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtConPerson" CssClass="form-control" />
                <br />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtDiscount" CssClass="col-md-2 control-label">Discount</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtDiscount" CssClass="form-control" />
                <br />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtCredit" CssClass="col-md-2 control-label">Credit</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtCredit" CssClass="form-control" />
                <br />
            </div>
        </div>
        
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtPincode" CssClass="col-md-2 control-label">Pincode</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtPincode" CssClass="form-control" />
                <br />
            </div>
        </div> 
          <div class="form-group" >
            <asp:Label runat="server" AssociatedControlID="txtID" Visible="false" CssClass="col-md-2 control-label">id</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtID" Visible="false" CssClass="form-control" />
                <br />
            </div>
        </div> 
      
             
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">

                <asp:Button ID="btnCreateVendor" runat="server" OnClick="btnCreateVendor_Click"  Text="ADD" CssClass="btn btn-default" ValidationGroup="exSave"/>
                 <asp:Button ID="btnUpdateVendor" runat="server" OnClick="btnUpdateVendor_Click"  Text="UPDATE" CssClass="btn btn-default" Visible="false"/>
                <asp:Button ID="btnCancelVendor" runat="server" OnClick="btnCancelVendor_Click" Text="CANCEL" CssClass="btn btn-default" />
                <asp:Button ID="btnGoBack" runat="server" CssClass="btn btn-primary btn-large" Text="Go Back" OnClick="btnGoBack_Click"/>
            </div>
        </div>
    </div>
</asp:Content>
