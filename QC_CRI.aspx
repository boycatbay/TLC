<%@ Page Title="" Language="C#" MasterPageFile="~/Setting.Master" AutoEventWireup="true" CodeBehind="QC_CRI.aspx.cs" Inherits="Toollife.QC_CRI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">
        <div align="center">
            <h2>
                &nbsp;QC Discription</h2>
        </div>
        <div id="dataShow" align="center">
            <asp:GridView ID="qcdata" runat="server" AllowPaging="True" OnPageIndexChanging="qcdata_PageIndexChanging"
                AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None"
                BorderWidth="1px" CellPadding="3" GridLines="Vertical" OnRowCommand="qcdata_RowCommand"
                DataKeyNames="PART_NO,QC_NO,QC_DESC">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="PART_NO" HeaderText="Part NO." />
                    <asp:BoundField DataField="QC_NO" HeaderText="QC NO." />
                    <asp:BoundField DataField="QC_DESC" HeaderText="QC discription" />
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
            Add/Edit </h3>
        <div id="add">
            
            <div class="row">
                <div class="col-25">
                    
                        Part NO.
                </div>
                <div class="col-75">
                    <asp:DropDownList ID="selectPatNo" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp;
                </div>
            </div>
            <div class="row">
                <div class="col-25">
                   QC NO.
                </div>
                <div class="col-75">
                   <asp:ListBox ID="selectQC" runat="server" Visible="False" 
                        onselectedindexchanged="selectQC_SelectedIndexChanged" AutoPostBack="True"></asp:ListBox>
                        <asp:TextBox ID="oldQcEdit" runat="server" Visible="False"></asp:TextBox>
                        
                    &nbsp;<asp:TextBox ID="QCadd" runat="server" Visible="True"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-25">
                   Discription
                </div>
                <div class="col-75">
              <asp:TextBox ID="qcdescin" runat="server" ></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <asp:Button ID="editButton" runat="server"  Text="SAVE" Width="180px" 
                    onclick="edit_Click" />
                <asp:Button ID="addButton" runat="server" OnClick="addButton_Click" Text="SAVE" Width="180px" />
            </div>
        
        </div>
    </div>
</asp:Content>
