<%@ Page Title="" Language="C#" MasterPageFile="~/Setting.Master" AutoEventWireup="true" CodeBehind="Machine_Set.aspx.cs" Inherits="Toollife.Machine_Set" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script language="javascript" type="text/javascript">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">
    <div align="center">
        <h2>Machine</h2>
    </div>
   
    <div id="dataShow" align="center">
     
        <asp:GridView id="macData" runat="server" AllowPaging="True" 
            onpageindexchanging="macData_PageIndexChanging" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" 
            onrowcommand="macData_RowCommand" 
            DataKeyNames="M_ID,PS_CODE,PKG_GROUP_SK,M_STATUS,MODEL" 
             >
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="M_ID" HeaderText="Machine NO." 
                    SortExpression="M_ID" />
              
                <asp:BoundField DataField="PS_DESC" HeaderText="Process Step" 
                    SortExpression="PS_DESC" />
               
                <asp:BoundField DataField="PKG_GROUP_DESC" HeaderText="Package Group" 
                    SortExpression="PKG_GROUP_DESC" />
                <asp:BoundField DataField="MODEL" HeaderText="Model" 
                    SortExpression="MODEL" />
                
                <asp:BoundField DataField="M_DESC" HeaderText="Machine Status" 
                    SortExpression="M_DESC" />
                
                <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Edit" 
                    ShowHeader="True" Text="Edit" />
                
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerSettings FirstPageText="First" LastPageText="Last" PageButtonCount="5" 
                Mode="NumericFirstLast" />
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
    
    <h3 align="center">Add/Edit</h3>
        <div id="add">
         <form id="addpart">
            <div class="row">
                <div class="col-25">
                     <label for="partno">Process Step</label>
                </div>
                <div class="col-75">
                    
          <asp:DropDownList ID="selectPS" runat="server" >
         
                    </asp:DropDownList>
&nbsp;</div>
            </div>
             <div class="row">
                <div class="col-25">
                     <label for="desc">Machine NO.</label>
                </div>
                <div class="col-75">
                     <asp:TextBox ID="macNO" runat="server"></asp:TextBox>
&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-25">
                     <label for="qty">Package Group</label>
                </div>
                <div class="col-75">
                  
                    <asp:DropDownList ID="pkgGp" runat="server">
                    </asp:DropDownList>
&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-25">
                     <label for="alert">Model</label>
                </div>
                <div class="col-75">
                     &nbsp;<asp:TextBox ID="mod" runat="server" ></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-25">
                     <label for="max">Machine Status</label>
                </div>
                <div class="col-75">
                     &nbsp;
                    <asp:DropDownList ID="macSts" runat="server">
                    </asp:DropDownList>
                </div>
            </div><br />
            <div class="row">
                   <asp:Button ID="editButton" runat="server" onclick="edit_Click" 
                    Text="SAVE" Width="180px"  /> 
           
           

                    <asp:Button ID="addButton" runat="server"
                    Text="SAVE" Width="180px" onclick="addButton_Click" />
            </div>
            </form>
        </div>
     </div>
     
     
</asp:Content>