<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PackingListGeneration.aspx.cs" Inherits="IMS.PackingListGeneration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <h2>Packing List</h2>
    </div>
    <br />
    <br />

     <div class="form-horizontal">
          <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="StockAt" CssClass="col-md-2 control-label"> Dispatched To : </asp:Label>
            <div class="col-md-10">
                <asp:Label runat="server" ID="StockAt" CssClass="col-md-2 control-label"/>
                <br/>
            </div>
    </div>   
     </div>
     <div class="form-horizontal">
    <div class="form-group">
        <br />
        <asp:GridView ID="StockDisplayGrid" CssClass="table table-striped table-bordered table-condensed"  Visible="true" runat="server" AllowPaging="false" PageSize="10" 
                AutoGenerateColumns="false" >
                 <Columns>
                    <asp:TemplateField HeaderText="Request #" HeaderStyle-Width ="70px">
                        <ItemTemplate>
                            <asp:Label ID="RequestNo" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("OrderID") %>' Width="50px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="70px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="BarCode" HeaderStyle-Width ="110px">
                        <ItemTemplate>
                            <asp:Label ID="BarCode" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("BarCode") %>'  Width="110px" ></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  Width="110px" HorizontalAlign="Left"/>
                    </asp:TemplateField>
                     
                    <asp:TemplateField HeaderText="Product Name" HeaderStyle-Width ="270px">
                        <ItemTemplate>
                            <asp:Label ID="ProductName" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("Product_Name") %>'  Width="270px" ></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  Width="270px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                     
                    <asp:TemplateField HeaderText="Expiry Date" HeaderStyle-Width ="200px">
                        <ItemTemplate>
                            <asp:Label ID="Expiry" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("ExpiryDate") %>'  Width="200px" ></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  Width="200px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width ="110px">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("SendQuantity") %>' ></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  Width="110px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Send Date" HeaderStyle-Width ="200px">
                        <ItemTemplate>
                            <asp:Label ID="SendDate" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("SendDate") %>'  Width="200px" ></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  Width="200px" HorizontalAlign="Left"/>
                    </asp:TemplateField>
                    
                 </Columns>
             </asp:GridView>
        <br />
         <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="PRINT LIST" CssClass="btn btn-large"/>
         <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="CANCEL LIST" CssClass="btn btn-large" />
    </div>
    <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
               
            </div>
        </div>
    </div>
</asp:Content>
