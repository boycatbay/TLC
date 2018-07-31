<%@ Page Title="" Language="C#" MasterPageFile="~/Setting.Master" AutoEventWireup="true" CodeBehind="newform.aspx.cs" Inherits="Toollife.newform" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="left">
        <h2>New</h2>
    </div>

        <div id="add"><b>
            <form id="addextent">
                <div class="row">
                    <div class="col-25">
                        <label for="macid">MC ID :</label>
                        <asp:label ID="macid" runat="server"></asp:label>
                    </div>
                    <div class="col-25">
                        <label for="toolid">Tool ID :</label>
                        <asp:Label ID="toolid" runat="server"></asp:label>
                    </div>
                    <div class="col-25">
                        <label for="pno">Part NO :</label>
                        <asp:Label ID="pno" runat="server"></asp:label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-25">
                        <label for="qlity">Comment</label>
                    </div>
                    <div class="col-25">
                        <asp:TextBox ID="comment" width="100%" runat="server"></asp:TextBox>
                    </div>
                </div></b>
                            <div class="row">
                <asp:Button ID="saveButton" onclick="savebtn_Click" runat="server" Text="Save" />
                <asp:Button ID="backButton" runat="server" Text="Back" 
                    PostBackUrl="~/Part_Status.aspx" />
            </div>
            </form>
        </div>

</asp:Content>
