<%@ Page Title="" Language="C#" MasterPageFile="~/Setting.Master" AutoEventWireup="true"
    CodeBehind="Part_Set.aspx.cs" Inherits="Toollife.Part_Set" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div align="center">
            <h2>
                Part Setting</h2>
        </div>
        <div id="dataShow" align="center">
            <asp:GridView ID="partData" runat="server" AllowPaging="True" OnPageIndexChanging="partData_PageIndexChanging"
                AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None"
                BorderWidth="1px" CellPadding="3" GridLines="Vertical" OnRowCommand="partData_RowCommand"
                DataKeyNames="PART_NO">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="PART_NO" HeaderText="Part NO." SortExpression="PART_NO" />
                    <asp:BoundField DataField="PART_DESC" HeaderText="Description" SortExpression="PART_DESC" />
                    <asp:BoundField DataField="QTY_USE" HeaderText="Quantity of Part" SortExpression="QTY_USE" />
                    <asp:BoundField DataField="ALERT_COUNT" HeaderText="Alert Limit" SortExpression="ALERT_COUNT" />
                    <asp:BoundField DataField="MAX_COUNT" HeaderText="Maximum Limit" SortExpression="MAX_COUNT" />
                    <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Edit" ShowHeader="True"
                        Text="Edit" />
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <PagerSettings FirstPageText="First" LastPageText="Last" PageButtonCount="5" Mode="NumericFirstLast" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
        </div>
        <br />
        <br />
        <h3 align="center">
            Add/Edit Part</h3>
        <div id="add">
            <form id="addpart">
            <div class="row">
                <div class="col-25">
                    <label for="partno">
                        Part Number</label>
                </div>
                <div class="col-75">
                    <asp:TextBox ID="partNoIN" runat="server"></asp:TextBox>
                    &nbsp;</div>
            </div>
            <div class="row">
                <div class="col-25">
                    <label for="desc">
                        Description</label>
                </div>
                <div class="col-75">
                    <asp:TextBox ID="descIN" runat="server"></asp:TextBox>
                    &nbsp;</div>
            </div>
            <div class="row">
                <div class="col-25">
                    <label for="qty">
                        Quantity of Part</label>
                </div>
                <div class="col-75">
                    <asp:TextBox ID="qtyIN" runat="server"></asp:TextBox>
                    &nbsp;</div>
            </div>
            <div class="row">
                <div class="col-25">
                    <label for="alert">
                        Alert Limit</label>
                </div>
                <div class="col-75">
                    &nbsp;<asp:TextBox ID="alertIN" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-25">
                    <label for="max">
                        Maximum Limit</label>
                </div>
                <div class="col-75">
                    &nbsp;<asp:TextBox ID="maxIN" runat="server"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <asp:Button ID="editButton" runat="server" OnClick="edit_Click" Text="SAVE" Width="180px" />
                <asp:Button ID="addButton" runat="server" OnClick="addButton_Click" Text="SAVE" Width="180px" />
            </div>
            </form>
        </div>
    </div>
</asp:Content>
