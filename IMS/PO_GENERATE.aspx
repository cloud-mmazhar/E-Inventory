﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PO_GENERATE.aspx.cs" Inherits="IMS.PO_GENERATE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 273px;
        }

        .auto-style2 {
            font-size: large;
        }

        .auto-style3 {
            width: 916px;
        }

        .auto-style4 {
            width: 758px;
        }

        @media print {
            .no-print, .no-print * {
                display: none !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="form-horizontal" runat="server" id="MAINDIV" visible="true">

        <table style="width: 100%;" align="center" border="0">
            <tr>
                <td class="auto-style2">
                    <h3><strong>PURCHASE ORDER</strong></h3>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
                <td></td>
                <td></td>
                <td></td>

            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label runat="server" ID="PO_Numberlbl" CssClass="col-md-2 control-label" Width="200px">P.O Number : </asp:Label></td>
                <td>
                    <asp:Label runat="server" ID="PO_Number" CssClass="col-md-2 control-label" Text="---" Width="300px"></asp:Label></td>

            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="auto-style1">
                    <asp:Label runat="server" ID="PO_Datelbl" CssClass="col-md-2 control-label" Width="200px">P.O Date : </asp:Label></td>
                <td>
                    <asp:Label runat="server" ID="PO_Date" CssClass="col-md-2 control-label" Text="---" Width="300px"></asp:Label></td>
            </tr>

            <tr>
                <td class="auto-style1">
                    <br />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td></td>
            </tr>

            <tr>
                <td class="text-left">
                    <h4>ORDER BY : </h4>
                    <asp:Label runat="server" ID="PO_FromName" CssClass="col-md-2 control-label" Text="---" Width="100%"></asp:Label></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="text-left">
                    <h4>ORDER FROM : </h4>
                    <asp:Label runat="server" ID="PO_ToName" CssClass="col-md-2 control-label" Text="---" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td class="text-left">
                    <asp:Label runat="server" ID="PO_FromAddress" CssClass="col-md-2 control-label" Text="---" Width="100%"></asp:Label></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="text-left">
                    <asp:Label runat="server" ID="PO_ToAddress" CssClass="col-md-2 control-label" Text="---" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label runat="server" ID="PO_FromPhone" CssClass="col-md-2 control-label" Text="---" Width="100%"></asp:Label></td>
                <td></td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label runat="server" ID="PO_ToPhone" CssClass="col-md-2 control-label" Text="---" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1"></td>
                <td></td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label runat="server" ID="PO_ToEmail" CssClass="col-md-2 control-label" Text="---" Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td></td>
                <td>&nbsp;</td>
                <td></td>
            </tr>
        </table>
        <br />
    </div>

    <div class="form-horizontal">
        <asp:GridView ID="StockDisplayGrid" CssClass="table table-striped table-bordered table-condensed" Visible="true" runat="server" AllowPaging="false" PageSize="10"
            AutoGenerateColumns="false" OnPageIndexChanging="StockDisplayGrid_PageIndexChanging">
            <Columns>

                <asp:TemplateField HeaderText="Ordered Product" HeaderStyle-Width="250px">
                    <ItemTemplate>
                        <asp:Label ID="ProductName" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("ProductName") %>' Width="330px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="330px" HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Ordered Quantity" HeaderStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Label ID="lblQuantity" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("Qauntity") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Unit Cost Price" HeaderStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCost" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("UnitCost") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Total Cost Price" HeaderStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalCost" CssClass="col-md-2 control-label" runat="server" Text='<%# Eval("totalCostPrice") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <table style="width: 100%;" id="TotalCostDiv" runat="server" visible="true">
            <tr>
                <td>
                    <br />
                    <br />
                </td>
                <td class="auto-style3">&nbsp;</td>
                <td>
                    <asp:Label ID="lblTotalCostALL" runat="server" Style="font-weight: 700"></asp:Label></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td><strong>Sign:</strong> ________________</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td><strong>Company Stamp:</strong> ____________</td>
            </tr>
        </table>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="btnPrint" runat="server" OnClientClick="window.print();" Text="PRINT" CssClass="btn btn-large no-print" Visible="true" />
                <asp:Button ID="btnFax" runat="server" OnClick="btnFax_Click" Text="FAX" CssClass="btn btn-large no-print" Visible="false" />
                <asp:Button ID="btnEmail" runat="server" OnClick="btnEmail_Click" Text="EMAIL" CssClass="btn btn-large no-print" Visible="true" />
            </div>
        </div>
    </div>


</asp:Content>
