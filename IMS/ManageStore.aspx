<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageStore.aspx.cs" Inherits="IMS.ManageStore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <h2>Manage Store</h2>
         <br />
         <br />
         <asp:Button ID="btnAddWH" runat="server" CssClass="btn btn-primary btn-large" Text="Add Store" OnClick="btnAddWH_Click"/>
         <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary btn-large" Text="Edit Store" OnClick="btnEdit_Click"/>
         <asp:Button ID="btnViewWareHouse" runat="server" CssClass="btn btn-primary btn-large" Text="View Store" OnClick="btnViewWareHouse_Click"/>
         <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary btn-large" Text="Go Back" OnClick="btnBack_Click"/>
         <br/>
          <table id="SelWH" runat="server" visible="False">
               <tr>
                    <td>Select WareHouse</td>
                     <td>
                        <div id="container">


                            <div class="side-by-side clearfix">

                                <div>

                                    <asp:DropDownList data-placeholder="select a warehouse..." ID="WHList" runat="server" class="chzn-select" >
                                        <asp:ListItem Text="" Value=""></asp:ListItem>
                                        <asp:ListItem Value=''> ------------------- Select ------------------ </asp:ListItem>

                                    </asp:DropDownList>

                                </div>
                            </div>

                        </div>
                        <script src="Scripts/jquery.min.js" type="text/javascript"></script>
                        <script src="Scripts/chosen.jquery.js" type="text/javascript"></script>
                        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>


                    </td>
                    
                </tr>

                <tr><td colspan="2">

                    <asp:Button ID="btnSubmit" runat="server" Text="Search" OnClick="btnSubmit_Click"/>
                    </td></tr>
               
            </table>
</asp:Content>
