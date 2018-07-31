<%@ Page Title="" Language="C#" MasterPageFile="~/Setting.Master" AutoEventWireup="true"
    CodeBehind="MCxTS.aspx.cs" Inherits="Toollife.MCxTS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div align="center">
            <h2>
                Machine And Tool Set</h2>
        </div>
        <div id="dataShow" align="center">
            <asp:GridView ID="mxtsData" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                GridLines="Vertical" AllowPaging="True" 
                onpageindexchanging="getMxtInfo_PageIndexChanging" 
                
                DataKeyNames="M_ID,TOOL_SK,TOOL_ID,PKG_GROUP_SK,PKG_GROUP_DESC,PS_CODE,PS_DESC,QUAT_CNT,WEKLY_CNT" 
                onrowcommand="mxtsData_RowCommand">
                <AlternatingRowStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:BoundField DataField="M_ID" HeaderText="Machine ID" SortExpression="M_ID" />
                    <asp:BoundField DataField="PKG_GROUP_DESC" HeaderText="Package Group" 
                        SortExpression="PKG_GROUP_DESC" />
                    <asp:BoundField DataField="TOOL_ID" HeaderText="Tool ID" SortExpression="TOOL_ID" />
                    <asp:BoundField DataField="PS_DESC" HeaderText="Process Step" 
                        SortExpression="PS_DESC" />
                    <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Select" ShowHeader="True"
                        Text="Select" />
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
        </div>
                <div align="center">
            <h2>
                Tool Set Select</h2>
        </div>

        <div id="Div1" align="center">
        <asp:GridView ID="tsData" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                    CellPadding="3" GridLines="Vertical"  
                    onpageindexchanging="gettsData_PageIndexChanging" 
                DataKeyNames="TOOL_SK,TOOL_ID,PKG_CODE,PKG_GROUP_DESC" Font-Bold="True" >
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="TOOL_SK" HeaderText="TOOL_SK"  
                            SortExpression="TOOL_SK" ReadOnly="True" />
                        <asp:BoundField DataField="TOOL_ID" HeaderText="Tool ID" 
                            SortExpression="TOOL_ID" />
                        <asp:BoundField DataField="PKG_CODE" HeaderText="Package Code" 
                            SortExpression="PKG_CODE" />
                        <asp:BoundField DataField="PKG_GROUP_DESC" HeaderText="Package Group" 
                            SortExpression="PKG_GROUP_DESC" />
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkTS" runat="server" AutoPostBack="True" />
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
                <asp:Button  ID="tsSelected" runat="server" 
                    Text="Select the Checked item(s)" Width="230px" onclick="tsSelected_click" 
                    />
                </div>
        <h3 align="center">
            Add/Edit</h3>
        <div class="row">
            <div class="col-25"><b>
                    Process Step
            </div></b>
            <div class="col-75">
                <asp:DropDownList ID="selectPS" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="selectPS_SelectedIndexChanged">
                </asp:DropDownList>
           
            </div>
        </div>
        <div class="row">
            <div class="col-25"><b>
                Machine NO.
            </div></b>
            <div class="col-75">
                <asp:DropDownList ID="selectMac" runat="server" 
                   >
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-25"><b>
                Tool Set Contain
            
            </div>
            <div class="col-75">
                <asp:ListBox ID="selectPC" runat="server"></asp:ListBox>
                <asp:ListBox ID="oldTS" runat="server" Visible="False"></asp:ListBox>
            
            </div>
        </div></b>
        <br />
        <div class="row">
            <asp:Button ID="editButton" runat="server" Text="SAVE" Width="180px" 
                onclick="edit_click" />
            <asp:Button ID="addButton" runat="server" Text="SAVE" Width="180px" 
                onclick="add_Click" />
        </div>
    </div>
</asp:Content>
