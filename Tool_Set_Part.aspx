<%@ Page Title="" Language="C#" MasterPageFile="~/Setting.Master" AutoEventWireup="true"
    CodeBehind="Tool_Set_Part.aspx.cs" Inherits="Toollife.Tool_Set_Part" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div align="center">
            <h1>
                Tool Set - Part Set </h1>
        </div>
        <div id="selectTS" align="center">
      
            <h2 align="center">Tool Set</h2>
            <asp:GridView ID="tsData" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                CellPadding="3" GridLines="Vertical" 
                DataKeyNames="PKG_CODE,TOOL_SK,TOOL_ID" 
                onpageindexchanging="partNO_PageIndexChanging" 
                onrowcommand="tsData_RowCommand" Font-Bold="True" >
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="TOOL_ID" HeaderText="Tool ID" SortExpression="TOOL_ID" />
                    <asp:BoundField DataField="PKG_CODE" HeaderText="Package Code" SortExpression="PKG_CODE" />
                    <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Select" ShowHeader="True"
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
        <div id="addpartALL">
            <div id="selectPart" align="center" runat="server">
                <h2 align="center">
                    Part Number</h2>
                <asp:GridView ID="partNO" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                    CellPadding="3" GridLines="Vertical" DataKeyNames="PART_NO,PART_DESC" 
                    onpageindexchanging="partNO_PageIndexChanging" Font-Bold="True" >
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="PKG_CODE" HeaderText="PKG" SortExpression="PART_CODE" />
                        <asp:BoundField DataField="PART_NO" HeaderText="Part NO." SortExpression="PART_NO" />
                        <asp:BoundField DataField="PART_DESC" HeaderText="Discription" SortExpression="PART_DESC" />
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkPart" runat="server" AutoPostBack="True" />
                            </ItemTemplate>
                        </asp:TemplateField>
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
                <br />
                <asp:Button  ID="chkPartSelected" runat="server" 
                    Text="Select the Checked item(s)" Width="230px" 
                    onclick="chkPartSelected_click"  />

            </div>
            <br />
            <br />
            <div id="addPartSet" runat="server">
                <h2 align="center">
                    Add/Edit</h2>
                <div class="row"><b>
                    <div class="col-25">
                        Tool ID
                    </div>
                    <div class="col-75">
                        <asp:TextBox ID="tIDIN" runat="server" Enabled="False"></asp:TextBox>
                        &nbsp;<asp:TextBox ID="Toolsk" Visible="false" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-25">
                        Package Code
                    </div>
                    <div class="col-75">
                        <asp:TextBox ID="pkgCoIN" runat="server" Enabled="False"></asp:TextBox>
                        &nbsp;</div>
                </div>
                <div class="row">
                    <div class="col-25">
                        Part Contain
                    </div>
                    <div class="col-75">
                        &nbsp;<asp:ListBox ID="selectPC" runat="server"></asp:ListBox>
                        
                        <br />
                    </div>
                </div></b>
                <div class="row" id="adEdBut" runat="server">
                    <asp:Button ID="add" runat="server" Text="SAVE" Width="180px" 
                        onclick="add_Click" />
                    <asp:Button ID="edit" runat="server" Text="SAVE" Width="180px" 
                        onclick="edit_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
