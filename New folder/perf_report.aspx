<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="perf_report.aspx.cs" Inherits="merit.perf_report" %>
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
   
     
    <script type="text/javascript" language="javascript" src="../resources/demo.js"></script>
    
   <script type="text/javascript" class="init">

       $(document).ready(function () {
           $('#testgrid').DataTable({ "lengthMenu": [[100, 250, 500, -1], [100, 250, 500, "All"]] });
       });

   </script>
    
    
    <script src="Highcharts-9.1.0/code/highcharts.js"></script>
    <script src="Highcharts-9.1.0/code/highcharts-3d.js"></script>
    <script src="Highcharts-9.1.0/code/highcharts-more.js"></script>
    <script src="Highcharts-9.1.0/code/modules/exporting.js"></script>

     <link rel="stylesheet" href="cards.css" />

  
  
<style>

* {
  box-sizing: border-box;
}

.page-contain {
  display: flex;
  min-height: 100vh;
  align-items: center;
  justify-content: center;
  background: #E7F3F1;
  border: 0.75em solid white;
  padding: 2em;
  font-family: "Open Sans", sans-serif;
}

.data-card {
  display: flex;
  flex-direction: column;
  max-width: 20.75em;
  min-height: 20.75em;
  overflow: hidden;
  border-radius: 0.5em;
  text-decoration: none;
  background: white;
  margin: 1em;
  padding: 2.75em 2.5em;
  box-shadow: 0 1.5em 2.5em -0.5em rgba(0, 0, 0, 0.1);
  transition: transform 0.45s ease, background 0.45s ease;
}
.data-card h3 {
  color: #2E3C40;
  font-size: 3.5em;
  font-weight: 600;
  line-height: 1;
  padding-bottom: 0.5em;
  margin: 0 0 0.142857143em;
  border-bottom: 2px solid #753BBD;
  transition: color 0.45s ease, border 0.45s ease;
}
.data-card h4 {
  color: #627084;
  text-transform: uppercase;
  font-size: 0.725em;
  font-weight: 700;
  line-height: 1;
  letter-spacing: 0.1em;
  margin: 0 0 1.777777778em;
  transition: color 0.45s ease;
}
.data-card table {
  opacity: 1;
  color: #FFFFFF;
  font-weight: 100;
  margin: 0 0;
  transform: translateY(-1em);
  transition: opacity 0.45s ease, transform 0.5s ease;
}
.data-card .link-text {
  display: block;
  color: #753BBD;
  font-size: 1.125em;
  font-weight: 600;
  line-height: 1.2;
  margin: auto 0 0;
  transition: color 0.45s ease;
}
.data-card .link-text svg {
  margin-left: 0.5em;
  transition: transform 0.6s ease;
}
.data-card .link-text svg path {
  transition: fill 0.45s ease;
}
.data-card:hover {
  background: #753BBD;
  transform: scale(1.02);
}
.data-card:hover h3 {
  color: #FFFFFF;
  border-bottom-color: #A754C4;
}
.data-card:hover h4 {
  color: #FFFFFF;
}
.data-card:hover table {
  opacity: 1;
  transform: none;
}
.data-card:hover .link-text {
  color: #FFFFFF;
}
.data-card:hover .link-text svg {
  -webkit-animation: point 1.25s infinite alternate;
          animation: point 1.25s infinite alternate;
}
.data-card:hover .link-text svg path {
  fill: #FFFFFF;
}

@-webkit-keyframes point {
  0% {
    transform: translateX(0);
  }
  100% {
    transform: translateX(0.125em);
  }
}

@keyframes point {
  0% {
    transform: translateX(0);
  }
  100% {
    transform: translateX(0.125em);
  }
}
</style>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal><div style="font-size:larger;">
           Select FY : &nbsp;&nbsp; <asp:DropDownList ID="DDL_for_fy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="fy_DDL_change" DataTextField="FY" DataValueField="FY" >
                                        </asp:DropDownList>  
        <br/> <br/> Select Station : &nbsp;&nbsp; <asp:DropDownList ID="DDL_for_station" runat="server" AutoPostBack="true" OnSelectedIndexChanged="station_DDL_change" DataTextField="StationName" DataValueField="SrNo" ></asp:DropDownList>  
       
        </div><br/> <br/>
             <asp:Literal ID = "ltrChart" runat = "server" > </asp:Literal>  <asp:Literal ID = "Literal2" runat = "server" > </asp:Literal> 

               <asp:GridView ID="testgrid" runat="server" CssClass="hover" OnRowDataBound="grd_RowDataBound" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"> 
                <AlternatingRowStyle BackColor="White" />
               <Columns>
							
                   <asp:TemplateField HeaderText="station" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("display_name")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
                   <asp:TemplateField HeaderText="OP_HDP_avail_pc" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("OP_HDP_avail_pc")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
                   <asp:TemplateField HeaderText="OP_HDP_avail_pc" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("OP_HDP_plf_pc")%>&nbsp; %
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

<section class="page-contain">
  <a href="#" class="data-card">
    <h3>-4.01</h3>
    <h4>Marginal Contribution (P/KWhr)</h4>
    <table>
        <tr><td>Total gain/Loss due to Marginal Contribution : </td><td> -32.31 (in Rs.Cr.) </td></tr>
        <tr><td>Net gain/Loss due to Marginal Contribution (after sharing) : </td><td> -32.31 (in Rs.Cr.) </td></tr>
    </table>    
  </a>
  <a href="#" class="data-card">
    <h3>12,000</h3>
    <h4>Employees</h4>
    <p>Etiam porta sem malesuada.</p>
    <span class="link-text">
      View Information
      <svg width="25" height="16" viewBox="0 0 25 16" fill="none" xmlns="http://www.w3.org/2000/svg">
<path fill-rule="evenodd" clip-rule="evenodd" d="M17.8631 0.929124L24.2271 7.29308C24.6176 7.68361 24.6176 8.31677 24.2271 8.7073L17.8631 15.0713C17.4726 15.4618 16.8394 15.4618 16.4489 15.0713C16.0584 14.6807 16.0584 14.0476 16.4489 13.657L21.1058 9.00019H0.47998V7.00019H21.1058L16.4489 2.34334C16.0584 1.95281 16.0584 1.31965 16.4489 0.929124C16.8394 0.538599 17.4726 0.538599 17.8631 0.929124Z" fill="#753BBD"></path>
</svg>
    </span>
  </a>
</section>
  


        </div>
    </form>
</body>
</html>
