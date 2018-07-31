<%@ Page Title="" Language="C#" MasterPageFile="~/Setting.Master" AutoEventWireup="true" CodeBehind="Part_Status.aspx.cs" Inherits="Toollife.Part_Status" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align ="center">
    <h2>Part Status</h2>
</div>
<div id="dataShow" align ="center">
    <div class="col-25"><b>
        <label for="macid">MC ID :</label> 
            <asp:label id="macid" runat="server"></asp:label>
    </div>
    <div class="col-25">
        <label for="toolid">Tool ID :</label>
            <asp:label id="toolid" runat="server"></asp:label>
    </div><br /></b>

    <asp:GridView id="partData" runat="server" AllowPaging="true" PageSize="15"
            AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Vertical" DataKeyNames="M_ID,TOOL_SK,PART_NO" 
        onrowcommand="partData_RowCommand" onrowdatabound="partData_RowDataBound" 
        onpageindexchanging="partData_PageIndexChanging">
    <AlternatingRowStyle BackColor="White" Font-Bold="True" />
            <Columns>
                <asp:BoundField DataField="PART_NO" HeaderText="Part NO." 
                    SortExpression="PART_NO" />
                <asp:BoundField DataField="PART_DESC" HeaderText="Description" 
                    SortExpression="PART_DESC" />
                <asp:BoundField DataField="QTY_USE" HeaderText="Quantity of Part" 
                    SortExpression="QTY_USE" />
                <asp:BoundField DataField="LIFE_TIME" HeaderText="Count" 
                    SortExpression="LIFE_TIME" />
                <asp:BoundField DataField="ALERT_COUNT" HeaderText="Alert Limit" 
                    SortExpression="ALERT_COUNT" />
                <asp:BoundField DataField="MAX_COUNT" HeaderText="Maximum Limit" 
                    SortExpression="MAX_COUNT" />
                <asp:BoundField HeaderText="STATUS" />

                <asp:ButtonField ButtonType="Button" CommandName="New" HeaderText="" 
                    ShowHeader="True" Text="New" />
                <asp:ButtonField ButtonType="Button" CommandName="Extend" HeaderText="" 
                    ShowHeader="True" Text="Extend" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerSettings FirstPageText="First" LastPageText="Last" PageButtonCount="5" 
                Mode="NumericFirstLast" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Bold="True" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />


    </asp:GridView>
</div>
</asp:Content>
