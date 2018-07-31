<%@ Page Title="" Language="C#" MasterPageFile="~/Setting.Master" AutoEventWireup="true"
    CodeBehind="Adjust.aspx.cs" Inherits="Toollife.Adjust" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div align="center">
                <h2>
                    Adjust Life Time
                </h2>
            </div>
        </div>
        <div class="row">
            <div class="col-25">
                <label for="macaid"><b>
                    MC ID :</b></label>
                <asp:Label ID="macid" runat="server"><b></b></asp:Label>
            </div>
            <br />
        </div>
        <div class="row">
            <div class="col-25">
                <label for="toolid"><b>
                    TOOL ID :</b></label>
            </div>
            <div class="col-50">
                <asp:DropDownList ID="tool" runat="server" Height="20%" AutoPostBack="true" OnSelectedIndexChanged="tool_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-25"><b>
                Tool</b></div>
            <div class="col-50">
                <asp:TextBox ID="tid" runat="server"><b></b></asp:TextBox>
                &nbsp;</div>
        </div>
        <div class="row">
            <div class="col-25"><b>
                Life Time</b></div>
            <div class="col-50">
                <asp:TextBox ID="lifet" runat="server"><b></b></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-25">
                <label for="qlity"><b>
                    Comment</b></label>
            </div>
            <div class="col-50">
                <asp:TextBox ID="comment" Width="100%" runat="server"><b></b></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row">
            <asp:Button ID="saveButton" runat="server" OnClick="save_Click" Text="SAVE" />
            <asp:Button ID="backButton" runat="server" Text="Back" />
        </div>
    </div>
</asp:Content>
