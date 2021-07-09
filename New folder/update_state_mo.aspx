<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="update_state_mo.aspx.cs" Inherits="merit.update_state_mo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta http-equiv="Content-type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no"/>
    <title>DataTables example - Base style - hover</title>
    <link rel="shortcut icon" type="image/png" href="/media/images/favicon.png"/>
    <link rel="alternate" type="application/rss+xml" title="RSS 2.0" href="http://www.datatables.net/rss.xml"/>
    <link rel="stylesheet" type="text/css" href="/media/css/site-examples.css?_=0602f7ec58abe00302963423bf7a8d5a"/>
    <link rel="stylesheet" type="text/css" href="./DataTables/DataTables-1.10.24/css/jquery.dataTables.min.css"/>
    
    <script type="text/javascript" src="/media/js/site.js?_=30648b1410332bada11fa3ff8050ff88"></script>
    <script type="text/javascript" src="/media/js/dynamic.php?comments-page=examples%2Fstyling%2Fhover.html" async></script>
    <script type="text/javascript" language="javascript" src="jquery-3.5.1.js"></script>
    <script type="text/javascript" language="javascript" src="DataTables/DataTables-1.10.24/js/jquery.dataTables.min.js"></script>
    
   <script type="text/javascript" class="init">

       $(document).ready(function () {
           $('#testgrid').DataTable({
               "paging": false,
               "dom": 'lrtip',
               "ordering": true,
               "autoWidth": true,
               "info": false
           });
       });

   </script>
    <link href="JS/jquery-ui.css" rel="stylesheet" />
<script src="packages/jQuery.UI.Combined.1.12.1/Content/Scripts/jquery-ui-1.12.1.min.js"></script>
    <script src="JS/jquery-ui.js"></script>
    <script>  
        $(function () {
            $('#date_from').datepicker(
                {
                    dateFormat: 'yy-mm-dd',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: '1950:2100'
                });
        })
    </script>  
    <script>  
        function isFloatNumber(e, t) {
            var n;
            var r;
            if (navigator.appName == "Microsoft Internet Explorer" || navigator.appName == "Netscape") {
                n = t.keyCode;
                r = 1;
                if (navigator.appName == "Netscape") {
                    n = t.charCode;
                    r = 0
                }
            } else {
                n = t.charCode;
                r = 0
            }
            if (r == 1) {
                if (!(n >= 48 && n <= 57 || n == 46)) {
                    t.returnValue = false
                }
            } else {
                if (!(n >= 48 && n <= 57 || n == 0 || n == 46)) {
                    t.preventDefault()
                }
            }
        }
    </script> 


     <style type="text/css">
        .VC_font   
        { 
            padding:0;
            margin:0;
           font: 15pt Verdana; 
           font-weight:200;
           color:darkblue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
            Select State : <asp:DropDownList ID="DDL_for_state" runat="server" AutoPostBack="true" OnSelectedIndexChanged="state_DDL_change" DataTextField="StateName" DataValueField="SrNo" ></asp:DropDownList>  
       
           <br /><br />Merit Order is valid from : <asp:TextBox runat="server" ID="date_from" AutoPostBack="false"></asp:TextBox>
            
            <asp:GridView ID="testgrid" runat="server" CssClass="hover"  OnRowDataBound="grd_RowDataBound" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" Width="60%" BorderStyle="None" BorderWidth="1px" CellSpacing="0" CellPadding="0" ForeColor="Black" GridLines="Vertical"> 
                <AlternatingRowStyle BackColor="White" />
               <Columns>
							<asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="no-sort" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">  
           <ItemTemplate>  
              <asp:Label ID="SrNo" runat="server" Text='<%#Eval("Sr_No")%>'></asp:Label>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
                   <asp:TemplateField HeaderText="Station" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" >  
           <ItemTemplate>  
               <asp:Label ID="StationName" runat="server" Text='<%#Eval("display_name")%>'></asp:Label>  
               <asp:Label ID="StationID" runat="server" Text='<%#Eval("StationID")%>' Visible="false"></asp:Label>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="VC" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="150px" >  
           <ItemTemplate>   
               <asp:Label ID="VC_old" runat="server" Text='<%#Eval("VC")%>' Visible="false"></asp:Label>
               <asp:TextBox ID="VC_text" runat="server" Text='<%# Eval("VC") %>' ClientIDMode="AutoID" CssClass="VC_font"  onkeypress="return isFloatNumber(this,event);"></asp:TextBox> 
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
						</Columns>
						<HeaderStyle BackColor="#6B696B" HorizontalAlign="Center" Font-Bold="True" ForeColor="White"></HeaderStyle>

						<FooterStyle BackColor="#CCCC99" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView> 
            
        <asp:GridView ID="GridView1" runat="server" > 
            </asp:GridView> 
        <asp:Button runat="server" Text="Update Data" OnClick="update_mo" />
        </div>
    </form>
</body>
</html>
