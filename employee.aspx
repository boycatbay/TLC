<%@ Page Title="" Language="C#" MasterPageFile="~/Setting.Master" AutoEventWireup="true"
    CodeBehind="employee.aspx.cs" Inherits="Toollife.employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div align="center">
            <h2>
                Employee</h2>
        </div>
        <div id="dataShow" align="center">
            <asp:GridView ID="employData" runat="server" AllowPaging="True" OnPageIndexChanging="employData_PageIndexChanging"
                AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None"
                BorderWidth="1px" CellPadding="3" GridLines="Vertical" OnRowCommand="employData_RowCommand"
                DataKeyNames="BADGE_NO,NT_ACC,AUTHORIZE_LEVEL,E_MAIL">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="BADGE_NO" HeaderText="Badge NO." />
                    <asp:BoundField DataField="NT_ACC" HeaderText="NT ACC" />
                    <asp:BoundField DataField="authorize_desc" HeaderText="Authorize level" />
                    <asp:BoundField DataField="E_MAIL" HeaderText="E-Mail" />
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
                <div class="col-25"><b>
                    Badge NO</b>
                </div>
                <div class="col-75">
                    &nbsp;<asp:TextBox ID="bNoin" runat="server"><b></b></asp:TextBox><br />
                </div>
            </div>
            <div class="row">
                <div class="col-25"><b>
                    NT ACC</b>
                </div>
                <div class="col-75">
                <asp:TextBox ID="ntIn" runat="server"><b></b></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-25"><b>
                Auth. Level</div></b>
            <div class="col-75">
                <asp:DropDownList ID="authSe" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;</div>
        </div>
        <br />
        <div class="row" id="process" runat="server">
            <div class="col-25"><b>
                Process Step</div></b>
            <div class="col-75">
                <asp:DropDownList ID="psSel" runat="server" OnSelectedIndexChanged="psSel_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
                <br />
                <asp:ListBox ID="psSeltd" runat="server"><b></b></asp:ListBox>
                &nbsp;<asp:Button ID="resetPS" runat="server" Text="Reset" 
                    onclick="resetPS_Click" />
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-25"><b>
                E-Mail</div></b>
            <div class="col-75">
                <asp:TextBox ID="emailIn" runat="server"><b></b></asp:TextBox>
                &nbsp;</div>
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
