<%@ Page Title="" Language="C#" MasterPageFile="~/Setting.Master" AutoEventWireup="true"
    CodeBehind="Alert_Mail.aspx.cs" Inherits="Toollife.Alert_Mail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div align="center">
            <h2>
                Alert E-MAIL </h2>
        </div>
        <div id="dataShow" align="center">
            <asp:GridView ID="alermail" runat="server" AllowPaging="True" OnPageIndexChanging="alermail_PageIndexChanging"
                AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None"
                BorderWidth="1px" CellPadding="3" GridLines="Vertical" OnRowCommand="alermail_RowCommand"
                DataKeyNames="PS_CODE,PS_DESC,PS_SUPPT_EMAIL">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="PS_CODE" HeaderText="Process Step" />
                    <asp:BoundField DataField="PS_SUPPT_EMAIL" HeaderText="E-Mail" />
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
            Add/Edit</h3>
        <div id="add">
            <form id="addpart">
            <div class="row">
                <div class="col-25">
                    <label for="partno">
                        Process Step</label>
                </div>
                <div class="col-75">
                    <asp:DropDownList ID="selectPS" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp;<asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-25">
                    E-MAIL
                </div>
                <div class="col-75">
                    <asp:TextBox ID="emailIN" runat="server"></asp:TextBox>
                    &nbsp;<asp:TextBox ID="TextBox2" runat="server" Visible="False"></asp:TextBox>
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
