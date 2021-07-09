<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="station_VC1.aspx.cs" Inherits="merit.station_VC1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta http-equiv="Content-type" content="text/html; charset=utf-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no">
    <title>DataTables example - Base style - hover</title>
    <link rel="shortcut icon" type="image/png" href="/media/images/favicon.png">
    <link rel="alternate" type="application/rss+xml" title="RSS 2.0" href="http://www.datatables.net/rss.xml">
    <link rel="stylesheet" type="text/css" href="/media/css/site-examples.css?_=0602f7ec58abe00302963423bf7a8d5a">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.24/css/jquery.dataTables.min.css">
    <style type="text/css" class="init">
    </style>
    <script type="text/javascript" src="/media/js/site.js?_=30648b1410332bada11fa3ff8050ff88"></script>
    <script type="text/javascript" src="/media/js/dynamic.php?comments-page=examples%2Fstyling%2Fhover.html" async></script>
    <script type="text/javascript" language="javascript" src="https://code.jquery.com/jquery-3.5.1.js"></script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/1.10.24/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" language="javascript" src="../resources/demo.js"></script>
    <link href="Content/css/select2.min.css" rel="stylesheet" />
    <script src="scripts/select2.min.js"></script>
    <script type="text/javascript" class="init">

        $(document).ready(function () {
            $('#testgrid').DataTable();
        });

    </script>

    <script src="./Highcharts-9.1.0/code/highcharts.js"></script>
<script src="./Highcharts-9.1.0/code/highcharts-more.js"></script>
<script src="./Highcharts-9.1.0/code/modules/solid-gauge.js"></script>
<script src="./Highcharts-9.1.0/code/modules/exporting.js"></script>
<script src="./Highcharts-9.1.0/code/modules/export-data.js"></script>
<script src="./Highcharts-9.1.0/code/modules/accessibility.js"></script>

</head>
<body>
    <script>
        $(function () {
            $("#<%=DDL_for_station.ClientID%>").select2();
            $("#<%=DDL_for_VC_filter.ClientID%>").select2();
    })
    </script>
    <form id="form1" runat="server">
    <!-- <div style="width:100%;height: 50px; background-color: deepskyblue; background-image: radial-gradient( #c6e9f7,#05adf0 );font-family:'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size:30px;text-align:center;vertical-align:middle;"><b>Merit Order Dashboard</b></div>
       <br/><br/> -->
        <div style="font-size:larger;">
            Select State : &nbsp;&nbsp; <asp:DropDownList ID="DDL_for_VC_filter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="VC_filter_DDL_change" DataTextField="StateName" DataValueField="SrNo" EnableIncrementalSearch="true" Width="300px" ></asp:DropDownList>  
        
       </div><br/><div style="font-size:larger;">
            Select Station : &nbsp;&nbsp; <asp:DropDownList ID="DDL_for_station" runat="server" AutoPostBack="true" OnSelectedIndexChanged="station_DDL_change" DataTextField="StationName" DataValueField="SrNo" EnableIncrementalSearch="true" ></asp:DropDownList>  
        
       </div><br/>
            <asp:Literal ID = "ltrChart" runat = "server" > </asp:Literal>  


    <div id="container" style="margin-left: 0px;"></div>



   
        <div> <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            <asp:GridView ID="testgrid" runat="server" CssClass="hover" OnRowDataBound="grd_RowDataBound" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"> 
                <AlternatingRowStyle BackColor="White" />
               <Columns>
							<asp:TemplateField HeaderText="display_name" HeaderStyle-HorizontalAlign="Center">  
           <ItemTemplate>  
               <%#Eval("display_name")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  	
                   <asp:TemplateField HeaderText="date_from" HeaderStyle-HorizontalAlign="Center">  
           <ItemTemplate>  
               <%#Eval("date_from")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
                   <asp:TemplateField HeaderText="VC" HeaderStyle-HorizontalAlign="Center">  
           <ItemTemplate>  
               <%#Eval("VC")%>  
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
        </div>
    </form>
</body>
</html>