<%@ Page Title="" Language="C#" MasterPageFile="~/Setting.Master" AutoEventWireup="true" CodeBehind="extentform.aspx.cs" Inherits="Toollife.extentform" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align ="center">
    <h2>Extend</h2>
</div>
<div id="dataShow" align ="center">
            <div class="row"><b>
                <div class="col-25">
                <label for="macid">MC ID :</label> 
                    <asp:label id="macid" runat="server"></asp:label>
                </div>
                <div class="col-25">
                <label for="toolid">Tool ID :</label>
                    <asp:label id="toolid" runat="server"></asp:label>
                </div>
                <div class="col-25">
                <label for="part">Part NO :</label> 
                    <asp:label id="part" runat="server"></asp:label>
                </div>
            </b></div><br />
    <div class="row">
    <asp:GridView id="ExtendF" runat="server" AllowPaging="true" PageSize="15"
            AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Vertical" 
            onrowcommand="ExtendF_RowCommand" DataKeyNames="QC_NO" 
            onpageindexchanging="ExtendF_PageIndexChanging" >
    <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                    <asp:BoundField DataField="QC_NO" HeaderText="Qualification" SortExpression="QC_NO" />
                    <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Select" ShowHeader="True"
                        Text="Select"/>
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
</div>
<br />
            <br />
            <div id="addPartSet" runat="server">
                <h2 align="center">
                    Add Extend</h2>
                <div class="row"><b>
                    <div class="col-25">
                        QC NO
                    </div>
                    <div class="col-25">
                        <asp:TextBox ID="qcno" runat="server" Enabled="False"></asp:TextBox>
                    </div>
                </div></b>
                <div class="row"><b>
                    <div class="col-25">
                        Result
                    </div>
                    <div class="col-25">
                        <asp:DropDownList ID="qcflag" runat="server" Enabled="False">
                        <asp:ListItem Text="Pass" Value="Y" />
                        <asp:ListItem Text="Not Pass" Value="N" />
                        </asp:DropDownList>
                        &nbsp;</div></b>
                </div>
                <div class="row"><b>
                    <div class="col-25">
                        Comment
                    </div>
                    <div class="col-25">
                        <asp:TextBox ID="qcaction" runat="server" Enabled="False"></asp:TextBox>
                    </div></b>
                </div>
               
            <div class="row">
                <asp:Button ID="saveButton" onclick="savebtn_Click" runat="server" Text="Save" />
                <asp:Button ID="backButton" runat="server" Text="Back" 
                    PostBackUrl="~/Part_Status.aspx" onclick="backButton_Click" />
            </div>
          </div>
            

</asp:Content>
