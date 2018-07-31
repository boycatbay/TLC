<%@ Page Title="" Language="C#" MasterPageFile="~/Setting.Master" AutoEventWireup="true"
    CodeBehind="conversion.aspx.cs" Inherits="Toollife.conversion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div align="center">
            <h2>
                Unit Conversion</h2>
        </div>
        <div id="dataShow" align="center">
            <asp:GridView ID="unicorn" runat="server" AllowPaging="True" OnPageIndexChanging="unicorn_PageIndexChanging"
                AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None"
                BorderWidth="1px" CellPadding="3" GridLines="Vertical" OnRowCommand="unicorn_RowCommand"
                DataKeyNames="PS_CODE,PKG_GROUP_SK,PKG_CODE,UNIT_STRIP,UNIT_TUBE,UNIT_TRAY,UNIT_HIT,MULTIPLIER">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="PS_DESC" HeaderText="Process Step" />
                    <asp:BoundField DataField="PKG_CODE" HeaderText="PKG code" />
                    <asp:BoundField DataField="UNIT_STRIP" HeaderText="Units / Strip" />
                    <asp:BoundField DataField="UNIT_TUBE" HeaderText="Units / Tube" />
                    <asp:BoundField DataField="UNIT_TRAY" HeaderText="Unit / Tray" />
                    <asp:BoundField DataField="UNIT_HIT" HeaderText="Units / Hit " />
                    <asp:BoundField DataField="MULTIPLIER" HeaderText="Multiplier" />
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
                <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>
                <SortedAscendingHeaderStyle BackColor="#0000A9"></SortedAscendingHeaderStyle>
                <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>
                <SortedDescendingHeaderStyle BackColor="#000065"></SortedDescendingHeaderStyle>
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
                    <label for="partno"><b>
                        Process Step</b></label>
                </div>
                <div class="col-75">
                    <asp:DropDownList ID="selectPS" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp;</div>
            </div>
            <div class="row">
                <div class="col-25">
                    <label for="partno"><b>
                        Package Group</b></label>
                </div>
                <div class="col-75">
                    <asp:DropDownList ID="selectPG" runat="server" OnSelectedIndexChanged="selectPG_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp;</div>
            </div>
            <div class="row">
                <div class="col-25"><b>
                    Package Code</b>
                </div>
                <div class="col-75">
                    <asp:ListBox ID="selectPC" runat="server" AutoPostBack="True"><b></b></asp:ListBox>
                    &nbsp;</div>
            </div>
            <div class="row">
            <h3 align="center">Unit Conversion</h3>
                <div class="col-25"><b>
                    Unit Per Strip</b>
                </div>
                <div class="col-75">
                    <asp:TextBox ID="unipesi" runat="server"><b>0</b></asp:TextBox></div>
                <div class="col-25"><b>
                    Unit Per Tube</b>
                </div>
                <div class="col-75">
                    <asp:TextBox ID="unipetu" runat="server"><b>0</b></asp:TextBox></div>
                <div class="col-25"><b>
                    Unit Per Tray</b>
                </div>
                <div class="col-75">
                    <asp:TextBox ID="unipeta" runat="server"><b>0</b></asp:TextBox></div>
                <div class="col-25"><b>
                    Unit Per Hit</b>
                </div>
                <div class="col-75">
                    <asp:TextBox ID="unipehi" runat="server"><b>0</b></asp:TextBox></div>
            </div>
            <div class="row">
                <div class="col-25"><b>
                    Multiplier</b>
                </div>
                <div class="col-75">
                    <asp:TextBox ID="multi" runat="server"><b>1</b></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <asp:Button ID="editButton" runat="server" OnClick="edit_Click" Text="SAVE" Width="180px" />
                <asp:Button ID="addButton" runat="server" OnClick="addButton_Click" Text="SAVE" Width="180px" />
            </div>
            </form>
        </div>
    </div>
</asp:Content>
