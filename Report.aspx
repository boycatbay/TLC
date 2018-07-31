<%@ Page Title="" Language="C#" MasterPageFile="~/Setting.Master" AutoEventWireup="true"
    CodeBehind="Report.aspx.cs" Inherits="Toollife.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(function () {
            var startDate = $("#start");
            var endDate = $("#end");
            startDate.datetimepicker({
                addSliderAccess: true,
                sliderAccessArgs: { touchonly: false },
                onClose: function (dateText, inst) {
                    if (endDate.val() != '') {
                        var testStartDate = startDate.datetimepicker('getDate');
                        var testEndDate = endDate.datetimepicker('getDate');
                        if (testStartDate > testEndDate)
                            endDate.datetimepicker('setDate', testStartDate);
                    }
                    else {
                        endDate.val(dateText);
                    }
                },
                onSelect: function (selectedDateTime) {
                    endDate.datetimepicker('option', 'minDate', startDate.datetimepicker('getDate'));
                }
            });
            endDate.datetimepicker({
                addSliderAccess: true,
                sliderAccessArgs: { touchonly: false },
                onClose: function (dateText, inst) {
                    if (startDate.val() != '') {
                        var testStartDate = startDate.datetimepicker('getDate');
                        var testEndDate = endDate.datetimepicker('getDate');
                        if (testStartDate > testEndDate)
                            startDate.datetimepicker('setDate', testEndDate);
                    }
                    else {
                        startDate.val(dateText);
                    }
                },
                onSelect: function (selectedDateTime) {
                    startDate.datetimepicker('option', 'maxDate', endDate.datetimepicker('getDate'));
                }
            });
        });   
    </script>

    <div align="center">
        <h2>
            Inquiry report of Computerized Tool life System</h2>
    </div>
    <br />
    <div id="dataShow" align="center" runat="server">
        <div class="row" align="center"><b>
            <label for="start">
                Date from :
            </label>

            &nbsp;
            <asp:TextBox ID="start" runat="server" style="width:20%;text-align:center;"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
            <label for="end">
                To :
            </label>
            &nbsp;
            <asp:TextBox ID="end" runat="server" style="width:20%;text-align:center;"></asp:TextBox>
        </div>
        <br />
        <div class="row" align="center">
        <label for=a>Area : </label>
             <asp:DropDownList ID="marea" runat="server" width="20%" 
            DataField="PS_DESC" AutoPostBack = "true"  OnSelectedIndexChanged="marea_SelectedIndexChanged" >
        </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;
        <label for=b>Machine : </label>
                <asp:DropDownList ID="mac" runat="server" width="20%" 
            DataField="M_ID" AutoPostBack = "true" >
        </asp:DropDownList>&nbsp;&nbsp;&nbsp;
            <asp:Button ID = "selectButton" runat="server" OnClick="select_Click" Text="Select" />
        </div></b>
        <br />
        <asp:MultiView ID="Multiview1" runat="server">
            <asp:View ID="viewNormal" runat="server">
            <div align="center">
            <h2>Normal Status</h2>
            </div>
                <asp:GridView ID="nm" runat="server" AllowPaging="true" PageSize="15" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                    CellPadding="3" GridLines="Vertical" 
                    onpageindexchanging="nm_PageIndexChanging" Font-Bold="True" >
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="OCCUR_DATE" HeaderText="Date" SortExpression="OCCUR_DATE" />
                        <asp:BoundField DataField="OCCUR_TIME" HeaderText="Time" SortExpression="OCCUR_TIME" />
                        <asp:BoundField DataField="M_ID" HeaderText="Machine" SortExpression="M_ID" />
                        <asp:BoundField DataField="TOOL_ID" HeaderText="Tool" SortExpression="TOOL_ID" />
                        <asp:BoundField DataField="PKG_CODE" HeaderText="PKG" SortExpression="PKG_CODE" />
                        <asp:BoundField DataField="PART_NO" HeaderText="Part Id" SortExpression="PART_NO" />
                        <asp:BoundField DataField="PART_DESC" HeaderText="Part Name" SortExpression="PART_DESC" />
                        <asp:BoundField DataField="QTY_USE" HeaderText="Qty" SortExpression="QTY_USE" />
                        <asp:BoundField DataField="LIFE_TIME" HeaderText="Life use" SortExpression="LIFE_TIME" />
                        <asp:BoundField DataField="REMARKS" HeaderText="Remark" SortExpression="LIFE_TIME" />
                        <asp:BoundField DataField="BADGE_NO" HeaderText="Badge" SortExpression="BADGE_NO" />
                        <asp:BoundField DataField="NT_ACC" HeaderText="Initial" SortExpression="NT_ACC" />
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
            </asp:View>
            <asp:View ID="viewExtend" runat="server">
            <div align="center">
            <h2>Extend Status</h2>
            </div>
                <asp:GridView ID="et" runat="server" AllowPaging="true" PageSize="15" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                    CellPadding="3" GridLines="Vertical" 
                    onpageindexchanging="et_PageIndexChanging" Font-Bold="True" >
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="OCCUR_DATE" HeaderText="Date" SortExpression="OCCUR_DATE" />
                        <asp:BoundField DataField="OCCUR_TIME" HeaderText="Time" SortExpression="OCCUR_TIME" />
                        <asp:BoundField DataField="M_ID" HeaderText="Machine" SortExpression="M_ID" />
                        <asp:BoundField DataField="TOOL_ID" HeaderText="Tool" SortExpression="TOOL_ID" />
                        <asp:BoundField DataField="PKG_CODE" HeaderText="PKG" SortExpression="PKG_CODE" />
                        <asp:BoundField DataField="PART_NO" HeaderText="Part Id" SortExpression="PART_NO" />
                        <asp:BoundField DataField="PART_DESC" HeaderText="Part Name" SortExpression="PART_DESC" />
                        <asp:BoundField DataField="LIFE_TIME" HeaderText="Life use" SortExpression="LIFE_TIME" />
                        <asp:BoundField DataField="QC_DESC" HeaderText="Quality" SortExpression="QC_DESC" />
                        <asp:BoundField DataField="QC_FLAG" HeaderText="Result" SortExpression="QC_FLAG" />
                        <asp:BoundField DataField="ACTION" HeaderText="Action" SortExpression="ACTION" />
                        <asp:BoundField DataField="BADGE_NO" HeaderText="Badge" SortExpression="BADGE_NO" />
                        <asp:BoundField DataField="NT_ACC" HeaderText="Name" SortExpression="NT_ACC" />
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
            </asp:View>
            <asp:View ID="viewAdjust" runat="server">
            <div align="center">
            <h2>Adjust Count</h2>
            </div>
                <asp:GridView ID="aj" runat="server" AllowPaging="true" PageSize="15" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                    CellPadding="3" GridLines="Vertical" 
                    onpageindexchanging="aj_PageIndexChanging" Font-Bold="True" >
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="OCCUR_DATE" HeaderText="Date" SortExpression="OCCUR_DATE" />
                        <asp:BoundField DataField="OCCUR_TIME" HeaderText="Time" SortExpression="OCCUR_TIME" />
                        <asp:BoundField DataField="M_ID" HeaderText="Machine" SortExpression="M_ID" />
                        <asp:BoundField DataField="PKG_CODE" HeaderText="PKG" SortExpression="PKG_CODE" />
                        <asp:BoundField DataField="ADJUST_CNT" HeaderText="Adjust Count" SortExpression="ADJUST_CNT" />
                        <asp:BoundField DataField="REMARKS" HeaderText="Remark" SortExpression="REMARKS" />
                        <asp:BoundField DataField="BADGE_NO" HeaderText="Badge" SortExpression="BADGE_NO" />
                        <asp:BoundField DataField="NT_ACC" HeaderText="Name" SortExpression="NT_ACC" />
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
            </asp:View>
        </asp:MultiView><br />
        <asp:Button ID = "btnExport" runat="server" OnClick="btnExport_Click" Text="Export to Excel" />
    </div>
</asp:Content>
