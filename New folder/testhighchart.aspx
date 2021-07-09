<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testhighchart.aspx.cs" Inherits="merit.testhighchart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     
    <script src="Highcharts-9.1.0/code/modules/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="Highcharts-9.1.0/code/highcharts.js"></script>
    <script src="Highcharts-9.1.0/code/highcharts-3d.js"></script>
    <script src="Highcharts-9.1.0/code/modules/exporting.js"></script>
</head>
<body>
     <form id="form1" runat="server">
         <script type="text/javascript">  
             $(function () {
                 $('#container').highcharts({
                     chart: {
                         options3d: {
                             enabled: true,
                             alpha: 5,
                             beta: 5,
                             viewDistance: 500,
                             depth: 15
                         }
                     },
                     title: {
                         enabled: false,
                     },
                     subtitle: {
                         enabled:false
                     },
                     xAxis: {
                         categories: <%= chartData3%>
        },
          yAxis: [{
              allowDecimals: false,
              min: 0,
              title: {
                  text: 'VC (Rs/KWh)'
              },
              stackLabels: {
                  enabled: true,
                  textAlign: 'center',
                  style: {
                      fontWeight: 'bold',
                      textAlign: 'center',
                      color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                  }
              }
          }, {
              opposite:true,
                  allowDecimals: false,
                  min: 0,
                  title: {
                      text: 'Cumm. Allocation (MW)'
                  },
                  stackLabels: {
                      enabled: true,
                      textAlign: 'center',
                      style: {
                          fontWeight: 'bold',
                          textAlign: 'center',
                          color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                      }
                  }
              }],
          tooltip: {
              headerFormat: '<b>{point.key}</b><br>',
              pointFormat: '<span style="color:{series.color}">\u25CF</span> {series.name}: {point.y} / {point.stackTotal}'
          },
                     series: [{
              type:'line',
              name: 'Curr VC',
              data: <%= chartData1%> 
                     }, {
            type:'column',
                name: 'Cumm. Allocation',
                         data: <%= chartData5%> ,
                         yAxis:1
                         }],
                     credits: {
                         text: '&copy; Commercial Dept, NTPC Ltd.',
                         href: 'mywebsite.com',
                         position: {
                             align: 'right',
                             verticalAlign: 'bottom'
                         }
                     }
      });
  });
         </script>  


<asp:Literal ID="Literal1" runat="server"></asp:Literal>
         <div id="container" style="min-width: 400px; height: 400px; margin: 0 auto"></div>  
          <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
