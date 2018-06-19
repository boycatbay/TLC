<%@ Page Title="" Language="C#" MasterPageFile="~/Setting.Master" AutoEventWireup="true"
    CodeBehind="TOOL_SET.aspx.cs" Inherits="Toollife.TOOL_SET" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div align="center">
            <h2>
                Tool Set</h2>
        </div>
        <div id="dataShow" align="center">
            <asp:GridView ID="tsData" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                CellPadding="3" GridLines="Vertical" OnPageIndexChanging="tsData_PageIndexChanging"
                DataKeyNames="TOOL_SK,TOOL_ID,PKG_CODE,STATUS_FLAG,PKG_GROUP_SK" OnRowCommand="tsData_RowCommand">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="TOOL_ID" HeaderText="Tool ID" SortExpression="TOOL_ID" />
                    <asp:BoundField DataField="PKG_CODE" HeaderText="Package Code" SortExpression="PKG_CODE" />
                    <asp:BoundField DataField="STATUS_FLAG" HeaderText="Status" SortExpression="STATUS_FLAG" />
                    <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Edit" ShowHeader="True"
                        Text="Select" />
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
        <div id="addTSK">
            <h2 align="center">
                Add/Edit</h2>
            <div id="addPkgC" runat="server">
                <div class="row">
                    <div class="col-25">
                        Tool ID
                    </div>
                    <div class="col-75">
                        <asp:TextBox ID="tIDIN" runat="server"></asp:TextBox>
                        &nbsp;</div>
                </div>
                <div class="row">
                    <div class="col-25">
                        <label for="partno">
                            Package Group</label>
                    </div>
                    <div class="col-75">
                        <asp:DropDownList ID="selectPG" runat="server" OnSelectedIndexChanged="selectPG_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                        &nbsp;</div>
                </div>
                <div class="row">
                    <div class="col-25">
                        Package Code
                    </div>
                    <div class="col-75">
                        <asp:ListBox ID="selectPC" runat="server"></asp:ListBox>
                        &nbsp;</div>
                </div>
                <br />
                <div class="row" id="statusFF" runat="server">
                    <div class="col-25">
                        STATUS
                    </div>
                    <div class="col-75">
                        <asp:TextBox ID="statusF" runat="server"></asp:TextBox>
                        &nbsp;<asp:TextBox Visible="false" ID="toolskin" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <br />
            <div class="row" id="adEdBut" runat="server">
                <asp:Button ID="add" runat="server" Text="SAVE" Width="180px" OnClick="add_Click" />
                <asp:Button ID="edit" runat="server" Text="SAVE" Width="180px" OnClick="edit_onclick" />
            </div>
        </div>
    </div>
</asp:Content>
