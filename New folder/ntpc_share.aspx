<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ntpc_share.aspx.cs" Inherits="merit.ntpc_share" %>

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
        <div><div style="font-size:larger;">
           Select Filter : &nbsp;&nbsp; <asp:DropDownList ID="DDL_for_filter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="filter_DDL_change" >               
                 <asp:ListItem Text="FY - wise" Value="0" Selected="True"></asp:ListItem>
				 <asp:ListItem Text="State - wise" Value="1"></asp:ListItem>
                                        </asp:DropDownList>  
        <br/> <br/> <asp:Label ID="Label2" runat="server" Text=""></asp:Label><asp:DropDownList ID="DDL_for_state" runat="server" AutoPostBack="true" OnSelectedIndexChanged="state_DDL_change" DataTextField="StateName" DataValueField="SrNo" ></asp:DropDownList>  
        <asp:DropDownList ID="DDL_for_fy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="fy_DDL_change" DataTextField="FY" DataValueField="FY" ></asp:DropDownList>  
        </div><br/> <br/>

             <asp:Literal ID = "ltrChart" runat = "server" > </asp:Literal>  <asp:Literal ID = "Literal2" runat = "server" > </asp:Literal> 

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
                            text: ''
                        },
                        subtitle: {
                            enabled: false,
                            text: ''
                        },
                        xAxis: {
                            categories: <%= chartData1%>
        },
                        yAxis: [{
                            allowDecimals: false,
                            min: 0,
                            title: {
                                text: 'MUs'
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
                            opposite: true,
                            allowDecimals: false,
                            min: 0,
                            title: {
                                text: 'NTPC share (%)'
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
                            pointFormat: '<span style="color:{series.color}">\u25CF</span> {series.name}: {point.y} <br/>',
                            shared: true,
                            split: false,
                            enabled: true
                        },
                        series: [ {
                                type: 'spline',
                                name: 'NTPC Share (FY-2) (%)',
                                data: <%= chartData6%> ,
                            yAxis: 1,
                            zIndex: 1
                     }, {
                                type: 'spline',
                                name: 'NTPC Share prev FY (%)',
                                data: <%= chartData5%> ,
                            yAxis: 1,
                            zIndex: 1
                     }, {
                                type: 'spline',
                                name: 'NTPC Share (%)',
                                data: <%= chartData2%> ,
                            yAxis: 1,
                            zIndex: 1
                            }, {
                                type: 'column',
                                name: 'Energy Consumption (CEA)',
                                data: <%= chartData3%>,
                            zIndex: 2
                         }, {
                             type: 'column',
                             name: 'NTPC Billed Energy',
                                data: <%= chartData4%> ,
                                zIndex: 2
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



         <div id="container" runat="server" style="min-width: 400px; height: 400px; margin: 0 auto"></div>  
            
            <script type="text/javascript">  
                $(function () {
                    $('#container1').highcharts({
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
                            text: ''
                        },
                        subtitle: {
                            enabled: false,
                            text: ''
                        },
                        xAxis: {
                            categories: <%= chartData1%>
                        },
                        yAxis: [{
                            allowDecimals: false,
                            min: 0,
                            title: {
                                text: 'MUs'
                            },
                            stackLabels: {
                                enabled: false,
                                textAlign: 'center',
                                style: {
                                    fontWeight: 'bold',
                                    textAlign: 'center',
                                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                                }
                            }
                        }, {
                            opposite: true,
                            allowDecimals: false,
                            min: 0,
                            title: {
                                text: 'NTPC share (%)'
                            },
                            stackLabels: {
                                enabled: false,
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
                            pointFormat: '<span style="color:{series.color}">\u25CF</span> {series.name}: {point.y} <br/>',
                            shared: true,
                            split: false,
                            enabled: true
                        },
                        series: [{
                                type: 'column',
                                name: 'Energy Consumption (CEA)',
                            data: <%= chartData3%> ,
                            zIndex: 2
                         }, {
                                type: 'column',
                                name: 'NTPC Billed Energy',
                            data: <%= chartData4%> ,
                            zIndex: 2
                         }, {
                                type: 'line',
                                name: 'NTPC Share (%)',
                                data: <%= chartData2%> ,
                            yAxis: 1,
                            zIndex: 1
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



         <div id="container1" runat="server" style="min-width: 400px; height: 400px; margin: 0 auto"></div> 
          <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

             <asp:GridView ID="testgrid" runat="server" CssClass="hover" OnRowDataBound="grd_RowDataBound" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"> 
                <AlternatingRowStyle BackColor="White" />
               <Columns>
							<asp:TemplateField HeaderText="state" HeaderStyle-HorizontalAlign="Center">  
           <ItemTemplate>  
               <%#Eval("state")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="Energy Consumption (CEA) (MUs)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("CEA")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
                   <asp:TemplateField HeaderText="NTPC Billed Energy (MUs)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("NTPC")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
                   <asp:TemplateField HeaderText="NTPC Share (%)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("share0")%>&nbsp; %
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
                   <asp:TemplateField HeaderText="FY" HeaderStyle-HorizontalAlign="Center">  
           <ItemTemplate>  
               <%#Eval("FY")%>  
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
            
             <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
