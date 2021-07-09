<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Mkt_Q_data.aspx.cs" Inherits="merit.Mkt_Q_data" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><meta http-equiv="Content-type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no"/>
    <title>DataTables example - Base style - hover</title>
    <link rel="shortcut icon" type="image/png" href="/media/images/favicon.png"/>
    <link rel="alternate" type="application/rss+xml" title="RSS 2.0" href="http://www.datatables.net/rss.xml"/>
    <link rel="stylesheet" type="text/css" href="/media/css/site-examples.css?_=0602f7ec58abe00302963423bf7a8d5a"/>
    <link rel="stylesheet" type="text/css" href="./DataTables/DataTables-1.10.24/css/jquery.dataTables.min.css"/>
    <style type="text/css" class="init">
    </style>
    <script type="text/javascript" src="/media/js/site.js?_=30648b1410332bada11fa3ff8050ff88"></script>
    <script type="text/javascript" src="/media/js/dynamic.php?comments-page=examples%2Fstyling%2Fhover.html" async></script>
    <script type="text/javascript" language="javascript" src="jquery-3.5.1.js"></script>
    <script type="text/javascript" language="javascript" src="./DataTables/DataTables-1.10.24/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" language="javascript" src="../resources/demo.js"></script>
    <link href="Content/css/select2.min.css" rel="stylesheet" />
    <script src="scripts/select2.min.js"></script>
    <script type="text/javascript" class="init">

        $(window).on("load", function () {
            $('#testgrid').DataTable({ "lengthMenu": [[100, 250, 500, -1], [100, 250, 500, "All"]] });
            $('#testgrid2').DataTable({ "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]] });
            //$('#testgrid3').DataTable({ "lengthMenu": [[100, 250, 500, -1], [100, 250, 500, "All"]] });
            //$('#testgrid4').DataTable({ "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]] });
            $("#myDIV").hide();
            $("#myDIV1").hide();
            $("#myDIV2").hide();
            $("#myDIV3").hide();
        });

    </script>

    <script src="./Highcharts-9.1.0/code/highcharts.js"></script>
<script src="./Highcharts-9.1.0/code/highcharts-more.js"></script>
<script src="./Highcharts-9.1.0/code/modules/solid-gauge.js"></script>
<script src="./Highcharts-9.1.0/code/modules/exporting.js"></script>
<script src="./Highcharts-9.1.0/code/modules/export-data.js"></script>
<script src="./Highcharts-9.1.0/code/modules/accessibility.js"></script>
    
    <style>
.button {
  background-color: #4CAF50; /* Green */
  border: none;
  color: white;
  padding: 10px 20px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 16px;
  margin: 4px 2px;
  transition-duration: 0.4s;
  cursor: pointer;
}

.button2 {
  background-color: white; 
  color: black; 
  border: 2px solid #008CBA;
}

.button2:hover {
  background-color: #008CBA;
  color: white;
}
</style>

    <style>
        .wrapper{
            margin:0;
  display: inline-flex;
  background: #fff;
  height: 40px;
  align-items: center;
  justify-content: space-evenly;
  border-radius: 5px;
  padding: 10px 15px;
  box-shadow: 5px 5px 20px rgba(0,0,0,0.2);
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="width:100%; border-bottom:3px solid #9439e4;font-size:30px;font-weight:400;font-family:fantasy;">
                Quarter-wise
                </div><br />
            <asp:RadioButtonList ID="Qtr_list" runat="server" CssClass="wrapper" RepeatDirection="Horizontal" OnSelectedIndexChanged="Qtr_change" AutoPostBack="true" >
            <asp:ListItem Text="Quarter -I" Value="1" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 10px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;" >Quarter -I</asp:ListItem>
            <asp:ListItem Text="Quarter -II" Value="2" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 10px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;">Quarter -II</asp:ListItem>
            <asp:ListItem Text="Quarter -III" Value="3" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 10px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;">Quarter -III</asp:ListItem>
            <asp:ListItem Text="Quarter -IV" Value="4" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 10px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;">Quarter -IV</asp:ListItem>
        </asp:RadioButtonList>


                    <script type="text/javascript">
                $(function () {
                    $('#container').highcharts({
                        chart: {
                            options3d: {
                                enabled: true,
                                alpha: 5,
                                beta: 5,
                                viewDistance: 500,
                                depth: 15,
                                margin: 0
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
                            minPadding: 0,
                            maxPadding: 0,
                            labels: {
                                style: {
                                    fontSize: '8px'
                                }
                            },
                            categories: <%= Data_Q_Station_st%>
                                },
                        yAxis: [{
                            allowDecimals: false,
                            min: 0,
                            title: {
                                text: 'MUs'
                            },
                            minPadding: 0,
                            maxPadding: 0,
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
                            pointFormat: '<span style="color:{series.color}">\u25CF</span> {series.name}: {point.y:.f} <br/>',
                            shared: true,
                            split: false,
                            enabled: true
                        },
                        series: [{
                            type: 'column',
                            name: '<%= Data_Q_fy%>',
                            data: <%= Data_Q_Curr_st%> 
                     }, {
                                type: 'column',
                            name: '<%= Data_Q_pq%>',
                                data: <%= Data_Q_Prev_st%> 
                         }, {
                                type: 'column',
                            name: '<%= Data_Q_py%>',
                                data: <%= Data_Q_Prev_yr_st%> 
                     }],
                        credits: {
                            text: '&copy; Commercial Dept, NTPC Ltd.',
                            href: 'mywebsite.com',
                            position: {
                                align: 'right',
                                verticalAlign: 'bottom'
                            }
                        }
                    }).reflow();
                });
            </script>  





         
            <script type="text/javascript">  
                $(function () {
                    $('#container1').highcharts({
                        chart: {
                            spacingLeft: 0,
                            spacingRight: 0
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
                            margin: 0,
                            categories: <%= Data_Q_Month_m%>
        },
                        yAxis: {
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
                        },
                        tooltip: {
                            headerFormat: '<b>{point.key}</b><br>',
                            pointFormat: '<span style="color:{series.color}">\u25CF</span> {series.name}: {point.y} <br/>',
                            shared: true,
                            split: false,
                            enabled: true
                        },
                        series: [{
                            type: 'column',
                            name: '<%= Data_Q_fy%>',
                            data: <%= Data_Q_Curr_m%> 
                     }, {
                                type: 'column',
                            name: '<%= Data_Q_py%>',
                                data: <%= Data_Q_Prev_m%> 
                     }],
                        credits: {
                            text: '&copy; Commercial Dept, NTPC Ltd.',
                            href: 'mywebsite.com',
                            position: {
                                align: 'right',
                                verticalAlign: 'bottom'
                            }
                        }
                    }).reflow();
                });
            </script>  


<br/>
<br/>
<div id="container" style="min-width: 400px; height: 400px; margin: 0 auto;border-right:1px solid green;display: inline-block; width:80%; "></div><div id="container1" style="min-width: 300px; height: 400px; margin: 0 auto;border-left:1px solid green;display: inline-block;  width:19%;"></div> 

         
          <asp:Label ID="Label5" runat="server" Text=""></asp:Label>

            <div id="target" class="button button2">
  Show / hide tables
</div>
             <script>
                 $("#target").click(function () {
                     $("#myDIV").toggle("slow");
                     $("#myDIV1").toggle("slow");
                 });
             </script>


            <div id="myDIV">
            <asp:GridView ID="testgrid" runat="server" CssClass="hover" OnRowDataBound="grd_RowDataBound" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"> 
                <AlternatingRowStyle BackColor="White" />
               <Columns>
							<asp:TemplateField HeaderText="station" HeaderStyle-HorizontalAlign="Center">  
           <ItemTemplate>  
               <%#Eval("display_name")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="Inj. Qtm. Selected Qtr (MUs)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("Inj_Q1")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
                   <asp:TemplateField HeaderText="Inj. Qtm. Same Qtr Prev yr (MUs)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("Inj_Q2")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
                   <asp:TemplateField HeaderText="Inj. Qtm. Prev Qtr (MUs)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("Inj_Q3")%>&nbsp; %
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
            
            <div id="myDIV1">
            <asp:GridView ID="testgrid2" runat="server" CssClass="hover" OnRowDataBound="grd_RowDataBound" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"> 
                <AlternatingRowStyle BackColor="White" />
               <Columns>
							<asp:TemplateField HeaderText="Month" HeaderStyle-HorizontalAlign="Center">  
           <ItemTemplate>  
               <%#Eval("Del_month")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="Inj. Qtm. Selected Qtr (MUs)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("Q1")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
                   <asp:TemplateField HeaderText="Inj. Qtm. Same Qtr Prev yr (MUs)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("Q2")%>&nbsp; %
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
                   <asp:TemplateField HeaderText="FY1" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("FY1")%>&nbsp; %
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
                   <asp:TemplateField HeaderText="FY2" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("FY2")%>&nbsp; %
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

            <asp:Label ID="Label1" runat="server"></asp:Label>
          <br /><br />  <div style="width:100%; border-bottom:3px solid #9439e4;font-size:30px;font-weight:400;font-family:fantasy;">
                Month-wise
                </div><br />
            <asp:RadioButtonList ID="Month_list" runat="server" CssClass="wrapper" RepeatDirection="Horizontal" OnSelectedIndexChanged="Month_change" AutoPostBack="true" >
            <asp:ListItem Text="Jan" Value="1" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
            <asp:ListItem Text="feb" Value="2" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
            <asp:ListItem Text="Mar" Value="3" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
            <asp:ListItem Text="Apr" Value="4" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
            <asp:ListItem Text="May" Value="5" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
            <asp:ListItem Text="Jun" Value="6" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
            <asp:ListItem Text="Jul" Value="7" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
            <asp:ListItem Text="Aug" Value="8" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
            <asp:ListItem Text="Sep" Value="9" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
            <asp:ListItem Text="Oct" Value="10" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
            <asp:ListItem Text="Nov" Value="11" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
            <asp:ListItem Text="Dec" Value="12" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
        </asp:RadioButtonList>
             
           <script type="text/javascript">  
               $(function () {
                   $('#container2').highcharts({
                       chart: {
                           options3d: {
                               enabled: true,
                               alpha: 5,
                               beta: 5,
                               viewDistance: 500,
                               depth: 15,
                               margin: 0
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
                           minPadding: 0,
                           maxPadding: 0,
                           labels: {
                               style: {
                                   fontSize: '8px'
                               }
                           },
                           categories: <%= Data_m_Month_m%>
                                },
                       yAxis: [{
                           allowDecimals: false,
                           min: 0,
                           title: {
                               text: 'MUs'
                           },
                           minPadding: 0,
                           maxPadding: 0,
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
                       series: [{
                           type: 'column',
                           name: '<%= Data_m_sel_m_fy%>',
                           data: <%= Data_m_Curr_m%> 
                     }, {
                                type: 'column',
                                name: '<%= Data_m_sel_m_py%>',
                                data: <%= Data_m_Prev_yr_m%> 
                     }],
                        credits: {
                            text: '&copy; Commercial Dept, NTPC Ltd.',
                            href: 'mywebsite.com',
                            position: {
                                align: 'right',
                                verticalAlign: 'bottom'
                            }
                        }
                    }).reflow();
                });
           </script>  





         
            <script type="text/javascript">  
                $(function () {
                    $('#container3').highcharts({
                        chart: {
                            spacingLeft: 0,
                            spacingRight: 0
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
                            margin: 0,
                            categories: <%= Data_m_fy_fy%>
                        },
                        yAxis: {
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
                        },
                        tooltip: {
                            headerFormat: '<b>{point.key}</b><br>',
                            pointFormat: '<span style="color:{series.color}">\u25CF</span> {series.name}: {point.y} <br/>',
                            shared: true,
                            split: false,
                            enabled: true
                        },
                        series: [{
                            type: 'column',
                            name: '<%= Data_m_sel_m%>',
                            data: <%= Data_m_Qtm_fy%> 
                     }],
                        credits: {
                            text: '&copy; Commercial Dept, NTPC Ltd.',
                            href: 'mywebsite.com',
                            position: {
                                align: 'right',
                                verticalAlign: 'bottom'
                            }
                        }
                    }).reflow();
                });
            </script>  


<br/>
<br/>
<div id="container2" style="min-width: 400px; height: 400px; margin: 0 auto;border-right:1px solid green;display: inline-block; width:80%; "></div><div id="container3" style="min-width: 300px; height: 400px; margin: 0 auto;border-left:1px solid green;display: inline-block;  width:19%;"></div> 

         
          <asp:Label ID="Label4" runat="server" Text=""></asp:Label>

            <div id="target1" class="button button2">
  Show / hide tables
</div>
             <script>
                 $("#target1").click(function () {
                     $("#myDIV2").toggle("slow");
                     $("#myDIV3").toggle("slow");
                 });
             </script>


            <div id="myDIV2">
            <asp:GridView ID="testgrid3" runat="server" CssClass="hover" OnRowDataBound="grd_RowDataBound" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"> 
                <AlternatingRowStyle BackColor="White" />
               <Columns>
							<asp:TemplateField HeaderText="station" HeaderStyle-HorizontalAlign="Center">  
           <ItemTemplate>  
               <%#Eval("display_name")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="Inj. Qtm. Selected month (MUs)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("Q1")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="Inj. Qtm. Same month Prev yr (MUs)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("Q2")%>&nbsp; %
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
            
            <div id="myDIV3">
            <asp:GridView ID="testgrid4" runat="server" CssClass="hover" OnRowDataBound="grd_RowDataBound" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"> 
                <AlternatingRowStyle BackColor="White" />
               <Columns>
							<asp:TemplateField HeaderText="FY" HeaderStyle-HorizontalAlign="Center">  
           <ItemTemplate>  
               <%#Eval("FY")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="Inj. Qtm. Selected month (MUs)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("Q1")%>  
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
            
            














            <div style="width:100%; border-bottom:3px solid #9439e4;font-size:30px;font-weight:400;font-family:fantasy;">
                Year-wise
                </div><br />
            <asp:RadioButtonList ID="Year_list" runat="server" CssClass="wrapper" RepeatDirection="Horizontal" OnSelectedIndexChanged="Year_change" AutoPostBack="true" >
            <asp:ListItem Text="2021-22" Value="2021-22" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
            <asp:ListItem Text="2020-21" Value="2020-21" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
            <asp:ListItem Text="2019-20" Value="2019-20" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
            <asp:ListItem Text="2018-19" Value="2018-19" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
            <asp:ListItem Text="2017-18" Value="2017-18" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
            <asp:ListItem Text="2016-17" Value="2016-17" style="background: #fff;
  display: flex;
  height: 30px;
  align-items: center;
  justify-content: space-evenly;
  margin: 0 0px;
  border-radius: 5px;
  cursor: pointer;
  padding: 0 10px;
  border: 2px solid lightgrey;
  transition: all 0.3s ease;"></asp:ListItem>
        </asp:RadioButtonList>
            <asp:Label ID="Label3" runat="server"></asp:Label>
            <div style="width:100%; border-bottom:3px solid #9439e4;font-size:30px;font-weight:400;font-family:fantasy;">
                Station-wise
                </div><br />
             <script>
                 $(function () {
                     $("#<%=DDL_for_station.ClientID%>").select2();
                 });
             </script>
            <div style="font-size:larger;">
            Select Station : &nbsp;&nbsp; <asp:DropDownList ID="DDL_for_station" runat="server" AutoPostBack="true" OnSelectedIndexChanged="station_DDL_change" DataTextField="StationName" DataValueField="SrNo" Width="300px" ></asp:DropDownList>  
        </div><br/>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            <asp:Literal ID = "ltrChart" runat = "server" > </asp:Literal>  <asp:Literal ID = "Literal2" runat = "server" > </asp:Literal> 
            
        </div>
    </form>
</body>
</html>
