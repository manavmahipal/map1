<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="website_curr_ECR.aspx.cs" Inherits="merit.website_curr_ECR" %>

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

   

</head>
<body>
    <form id="form1" runat="server">
        <div style="font-size:larger;">
          

            <script type="text/javascript">  
                $(function () {
                    $('#container').highcharts({                        
                        chart: {
                            marginBottom: 90,
                            zoomType: 'x'
                        },title: {
                            enabled: false,
                            text: ''
                        },
                        subtitle: {
                            enabled: false,
                            text: ''
                        },
                        xAxis: {
                            categories: <%= chartData1%>,
                            labels: {
                                style: {
                                    fontSize: '8px'
                                },
                                rotation: 310,
                                overflow: false
                            }
                        },
                     yAxis: {
                         allowDecimals: false,
                         min: 0,
                         title: {
                             text: 'Rs / KWh'
                         },
                         stackLabels: {
                             enabled: true,
                             textAlign: 'center',
                             style: {
                                 textAlign: 'center',
                                 color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                             }
                         }
                     },
                     tooltip: {
                         headerFormat: '<b>{point.key}</b><br>',
                         pointFormat: '<span style="color:{series.color}">\u25CF</span> {series.name}: {point.y} <br/>',
                         shared: true,
                         split: false,
                         enabled: true
                     },
                        series: [{
                            type: 'line',
                         name: 'NTPC Station current ECR (Rs/KWh)',
                         data: <%= chartData2%> ,
                         showInLegend: false,
                         dataLabels: {
                             align: 'left',
                             style: {
                                 fontSize: '8px'
                             },
                             enabled: true,
                             rotation: 270,
                             x: 2,
                             y: -10
                         }
                     }],
                     credits: {
                         enabled: false
                     }
                 });
             });
            </script>  



         <div id="container" style="width:100%;min-width: 400px; height: 90vh; margin: 0 auto"></div>  
             <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            <table style="margin-left: auto; margin-right: auto;">
        <tr>
            <td style="width:40px;"><a href="website_curr_ECR.aspx"><img src="dot.png" width="20px" /></a></td>
            <td style="width:40px;"><img src="dot.png" width="20px" /></td>
            <td style="width:40px;"><img src="dot.png" width="20px" /></td>
            <td style="width:40px;"><img src="dot.png" width="20px" /></td>
        </tr>
    </table>
        </div>
    </form>
</body>
</html>
