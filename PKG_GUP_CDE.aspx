<%@ Page Title="" Language="C#" MasterPageFile="~/Setting.Master" AutoEventWireup="true"
    CodeBehind="PKG_GUP_CDE.aspx.cs" Inherits="Toollife.PKG_GUP_CDE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div align="center">
            <h2>
                Package</h2>
        </div>
        <div id="dataShow" align="center">
            <asp:GridView ID="pkgData" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                CellPadding="3" GridLines="Vertical" 
                DataKeyNames="PKG_GROUP_SK,PKG_CODE,PKG_GROUP_DESC" 
                onpageindexchanging="pkgData_PageIndexChanging" 
                onrowcommand="pkgData_RowCommand" Font-Bold="True"
                
                >
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="PKG_GROUP_DESC" HeaderText="Package Group" SortExpression="PKG_GROUP_DESC" />
                    <asp:BoundField DataField="PKG_CODE" HeaderText="Package Code" SortExpression="PKG_CODE" />
                    <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Edit" 
                        ShowHeader="True" Text="Select" />
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
        <div id="select">
            <h2 align="center">
                Choose the action</h2><b>
                <asp:RadioButtonList ID="selectAct" runat="server" align="center" 
                AutoPostBack="True" onselectedindexchanged="selectAct_SelectedIndexChanged">
                    <asp:ListItem Text="Add/Edit Package Group" Value= "0" ></asp:ListItem>
                    <asp:ListItem Text="Add/Edit Package Code" Value="1"></asp:ListItem>
                </asp:RadioButtonList></b>
              
        </div>
        <br />
        <br />
        <div id="addPKG">
            <asp:MultiView ID="MultiView1" runat="server"><b>
                <asp:View ID="View1" runat="server">
                    <div id="addPkgC" runat="server">
                        <h3 align="center">
                            Add/Edit - Package Code</h3>
                        <div class="row">
                            <div class="col-25">
                                <label for="partno">
                                    Package Group</label>
                            </div>
                            <div class="col-75">
                                <asp:DropDownList ID="selectPG" runat="server">
                                </asp:DropDownList>
                                &nbsp;</div>
                        </div>
                        <div class="row">
                            <div class="col-25">
                                Package Code
                            </div>
                            <div class="col-75">
                                <asp:TextBox ID="pkgCoIn" runat="server"></asp:TextBox>
                                &nbsp;</div>
                        </div>
                        <br />
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div id="Div1" runat="server">
                        <h3 align="center">
                            Add/Edit - Package Group</h3>
                        <div class="row">
                            <div class="col-25">
                                Package Group
                            </div>
                            <div class="col-75">
                                <asp:TextBox ID="pkgGIn" runat="server"></asp:TextBox>
                                &nbsp;</div>
                            <asp:TextBox ID="pkgSK" runat="server" Visible="False"></asp:TextBox>
                        </div>
                        </div>
                </asp:View></b>
            </asp:MultiView>
            <div class="row" id="adEdBut" runat="server">
                <asp:Button ID="add" runat="server" Text="SAVE" Width="180px" onclick="add_Click" 
                    />
                <asp:Button ID="edit" runat="server" Text="SAVE" Width="180px" 
                    onclick="edit_Click" />
            </div>
        </div>
    </div>
</asp:Content>
