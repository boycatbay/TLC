<%@ Page Title="" Language="C#" MasterPageFile="~/Setting.Master" AutoEventWireup="true" CodeBehind="Current_Mac.aspx.cs" Inherits="Toollife.Current_Mac" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
    <h2>Current Machine</h2>
    </div>
    <div id="dataShow" align="center">
     <label for="area"><b>AREA : </b></label>
     <asp:DropDownList ID="marea" runat="server" width="20%" 
            DataField="PS_DESC" AutoPostBack = "true" OnSelectedIndexChanged="marea_SelectedIndexChanged">
        </asp:DropDownList> &nbsp;
         
     <br /><br />
    <asp:GridView id="partData" runat="server" AllowPaging="true" PageSize="15"
             
            AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" 
            onrowcommand="partData_RowCommand" onrowdatabound="partData_RowDataBound" 
            DataKeyNames="M_ID,TOOL_ID,STATUS,TOOL_SK,PKG_CODE" 
            onpageindexchanging="partData_PageIndexChanging" >
     <AlternatingRowStyle BackColor="#F0F0F0" Font-Bold="True" />
        <Columns>
            <asp:BoundField DataField="M_ID" HeaderText="Machine ID" 
                    SortExpression="M_ID" />
            <asp:BoundField DataField="TOOL_ID" HeaderText="Tool ID" 
                    SortExpression="TOOL_ID" />
            <asp:BoundField DataField="PKG_GROUP_DESC" HeaderText="PKG Group" 
                    SortExpression="PKG_GROUP_DESC" />
            <asp:BoundField DataField="QUAT_CNT" HeaderText="Quarter Count" 
                    SortExpression="QUAT_CNT" />
            <asp:BoundField DataField="WEKLY_CNT" HeaderText="Weekly Count" 
                    SortExpression="WEKLY_CNT" />
            <asp:BoundField DataField="PARTCONTROL" HeaderText="Part Control" 
                    SortExpression="PARTCONTROL" />
            <asp:BoundField DataField="PARTALERT" HeaderText="Part ALert" 
                    SortExpression="PARTALERT" />
            <asp:BoundField DataField="PARTEND" HeaderText="Part End" 
                    SortExpression="PARTEND" />
            <asp:BoundField DataField="PARTEXTENT" HeaderText="Part Extend" 
                    SortExpression="PARTEXTENT" />

            <asp:BoundField HeaderText="STATUS"  />
     
            <asp:ButtonField ButtonType="Button" CommandName="More" HeaderText="Detail" 
                    ShowHeader="True" Text="More" />
            <asp:ButtonField ButtonType="Button" CommandName="Adjust" HeaderText="Adjust" 
                    ShowHeader="True" Text="Adjust" />        
       </Columns>

            <EditRowStyle BackColor="White" />

            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerSettings FirstPageText="First" LastPageText="Last" PageButtonCount="5" 
                Mode="NumericFirstLast" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="Black" Font-Bold="True" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>
    </div>
   
</asp:Content>
