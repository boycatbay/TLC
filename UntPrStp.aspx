<%@ Page Title="" Language="C#" MasterPageFile="~/Setting.Master" AutoEventWireup="true" CodeBehind="UntPrStp.aspx.cs" Inherits="Toollife.UntPrStp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">
        <div align="center">
            <h2>
                Unit Per Strip Setting</h2>
        </div>
        <div id="dataShow" align="center">
            <asp:GridView ID="upsData" runat="server" AllowPaging="True" OnPageIndexChanging="upsData_PageIndexChanging"
                AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None"
                BorderWidth="1px" CellPadding="3" GridLines="Vertical" OnRowCommand="upsData_RowCommand"
                DataKeyNames="FRAME_NO,DEVICE_NO,PKG_CODE,UNIT_PER_STRP,PKG_GROUP_SK">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="FRAME_NO" HeaderText="Frame NO." />
                    <asp:BoundField DataField="DEVICE_NO" HeaderText="Device NO." />
                    <asp:BoundField DataField="PKG_CODE" HeaderText="Package Code" />
                    <asp:BoundField DataField="UNIT_PER_STRP" HeaderText="Unit/Strip" />
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
            Add/Edit Unit Per Strip</h3>
        <div id="add">
            <form id="addpart">
            <div class="row">
                <div class="col-25">
                    <label for="partno">
                       Frame NO.</label>
                </div>
                <div class="col-75">
                    &nbsp;<asp:TextBox ID="frameIn" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-25">
                    Device NO.
                </div>
                <div class="col-75">
                    <asp:TextBox ID="deviceIn" runat="server"></asp:TextBox>
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
            <div class="row">
                <div class="col-25">
                    Unit/Strip
                </div>
                <div class="col-75">
                    <asp:TextBox ID="unitIn" runat="server"></asp:TextBox>
                    &nbsp;</div>
            </div>
            <br />
            <div class="row">
                <asp:Button ID="editButton" runat="server" OnClick="edit_onclick" Text="SAVE" 
                    Width="180px" />
                <asp:Button ID="addButton" runat="server" OnClick="add_Click" Text="SAVE" 
                    Width="180px" />
            </div>
            </form>
        </div>
    </div>
</asp:Content>
