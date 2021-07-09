<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="meritindia_display.aspx.cs" Inherits="merit.meritindia_display" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no"/>
    <title></title>
    <link rel="shortcut icon" type="image/png" href="/media/images/favicon.png"/>
    <link rel="alternate" type="application/rss+xml" title="RSS 2.0" href="http://www.datatables.net/rss.xml"/>
    <link rel="stylesheet" type="text/css" href="/media/css/site-examples.css?_=0602f7ec58abe00302963423bf7a8d5a"/>
    <link rel="stylesheet" type="text/css" href="./DataTables/DataTables-1.10.24/css/jquery.dataTables.min.css"/>
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
            $('#testgrid1').DataTable({ "lengthMenu": [[100, 250, 500, -1], [100, 250, 500, "All"]] });
            $("#myDIV").hide();
            $("#myDIV1").hide();
        });

    </script>

    <script src="./Highcharts-9.1.0/code/highcharts.js"></script>
<script src="./Highcharts-9.1.0/code/highcharts-more.js"></script>
<script src="./Highcharts-9.1.0/code/modules/solid-gauge.js"></script>
<script src="./Highcharts-9.1.0/code/modules/exporting.js"></script>
<script src="./Highcharts-9.1.0/code/modules/export-data.js"></script>
<script src="./Highcharts-9.1.0/code/modules/accessibility.js"></script>


    
<script src="./JS/jquery.dynDateTime.js" type="text/javascript"></script>
<script src="./JS/calendar-en.js" type="text/javascript"></script>
<link href="./JS/calendar-blue.css" rel="stylesheet" type="text/css" />
<script type = "text/javascript">
    $(document).ready(function () {
        $(".Calender").dynDateTime({
            showsTime: false,
            ifFormat: "%Y-%m-%d",
            daFormat: "%Y-%m-%d",
            singleClick: true,
            weekNumbers: false,
        });
    });
</script>

    <style>
        .docblock {
            /* just for showing that background doesn't need to be solid */
            background: linear-gradient(to right, #fff 0%, #FFF 50%, #fff 100%);
            padding: 5px;
            font-size:medium;
            display: inline-block;
            width:25%;
            height: 300px;
        }

        .docgrounded-radiants {
            position: relative;
            border: 4px solid transparent;
            border-radius: 16px;
            background: linear-gradient(#effdff,#e2ffed);
            background-clip: padding-box;
            padding: 5px;
            /* just to show box-shadow still works fine */
            box-shadow: 0 2px 5px black, inset 0 0 5px white;
        }

            .docgrounded-radiants::after {
                position: absolute;
                top: -4px;
                bottom: -4px;
                left: -4px;
                right: -4px;
                background: linear-gradient(#ffffff, #ffffff);
                content: '';
                z-index: -1;
                border-radius: 16px;
            }
    </style>
     <style>
        .docitem {
            /* just for showing that background doesn't need to be solid */
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
       <div> <div style="font-size:larger;background-color:#3dd1eb;">Select Date : &nbsp;&nbsp;     <asp:Textbox ID="Date_Input" AutoPostBack="false"   runat="server" Width="150px" class="Calender" ></asp:Textbox>  
           <asp:Button ID="date_filter" runat="server" OnClick="Date_DDL_change" Text="  Click to filter data  " /></div>
           <br/><br/>

        <script type="text/javascript">  
            $(function () {
                $('#container').highcharts({
                    chart: {
                        zoomType: 'xy'
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
                        categories: <%= Data_blk%>,
                        crosshair: true,
                        index: 2,
                    },
                    yAxis: {
                        allowDecimals: false,
                        min: 0,
                        title: {
                            text: 'MW'
                        },
                        Labels: {
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
                        pointFormat: '<span style="color:{series.color}">\u25CF</span> {series.name}: {point.y:.f} <br/>',
                        shared: true,
                        split: false,
                        enabled: true
                    },
                    plotOptions: {
                        area: {
                            stacking: 'normal',
                            lineColor: '#666666',
                            lineWidth: 1,
                            marker: {
                                lineWidth: 1,
                                lineColor: '#666666'
                            }
                        },
                        series: {
                            marker: {
                                enabled: false
                            }
                        }
                    },
                    series: [{
                        name: 'Coal gen',
                        type: 'area',
                        color: '#c1dde0',
                        data: <%= Data_T_gen%> 
                     }, {
                                name: 'Gas gen',
                                type: 'area',
                                color: '#fbc99f',
                                data: <%= Data_G_gen%> 
                     }, {
                                name: 'Nuclear gen',
                                type: 'area',
                                color: '#ffd689',
                                data: <%= Data_N_gen%> 
                     }, {
                                name: 'hydro gen',
                                type: 'area',
                                color: '#afe8f9',
                                data: <%= Data_H_gen%> 
                     }, {
                                name: 'RE gen',
                                type: 'area',
                                color: '#80ffb6',
                                data: <%= Data_R_gen%> 
                     }, {
                                name: 'Demand_met',
                                type: 'spline',
                                color: '#081414',
                                data: <%= Data_Dem_met%> 
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

        <div id="container" runat="server" style="min-width: 400px; height: 300px; margin: 0 auto;display: inline-block;width:70%; "></div>
             <div class="docblock">

            <div class="docgrounded-radiants">
        <asp:GridView ID="testgrid3" runat="server" CssClass="hover" OnRowDataBound="grd_RowDataBound" AutoGenerateColumns="False" Width="100%" BackColor="Transparent" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Transparent" GridLines="Vertical"> 
                <AlternatingRowStyle BackColor="Transparent" />    

						<FooterStyle BackColor="Transparent" />
                <PagerStyle BackColor="Transparent" ForeColor="Transparent" HorizontalAlign="Right" />
                <RowStyle BackColor="Transparent" />
                <SelectedRowStyle BackColor="Transparent" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="Transparent" />
                <SortedAscendingHeaderStyle BackColor="Transparent" />
                <SortedDescendingCellStyle BackColor="Transparent" />
                <SortedDescendingHeaderStyle BackColor="Transparent" />
               <Columns>
                   <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="docitem" ItemStyle-HorizontalAlign="Right" ItemStyle-ForeColor="#eef2cc" ItemStyle-BackColor="#125478" HeaderStyle-BackColor="Transparent" HeaderStyle-ForeColor="Transparent" >  
           <ItemTemplate>  
               <%#Eval("param")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>   
                   <asp:TemplateField HeaderText="MAX" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"  ItemStyle-ForeColor="#125478" HeaderStyle-BackColor="#125478" HeaderStyle-ForeColor="#eef2cc" >  
           <ItemTemplate>  
               <%#Eval("max")%>  <span style="color:red;font-size:10px;">&nbsp;(<%#Eval("time1")%>)&nbsp;</span>
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="MIN" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-ForeColor="#125478" HeaderStyle-BackColor="#125478" HeaderStyle-ForeColor="#eef2cc" >  
           <ItemTemplate>  
               <%#Eval("min")%>  <span style="color:red;font-size:10px;">&nbsp;(<%#Eval("time2")%>)&nbsp;</span>
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
                   <asp:TemplateField HeaderText="AVG" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-ForeColor="#125478" HeaderStyle-BackColor="#125478" HeaderStyle-ForeColor="#eef2cc" >  
           <ItemTemplate>  
               <%#Eval("avg")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
						</Columns>
						
            </asp:GridView>  
                 <asp:GridView ID="testgrid4" runat="server" CssClass="hover" OnRowDataBound="grd_RowDataBound" AutoGenerateColumns="False" Width="100%" BackColor="Transparent" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Transparent" GridLines="Vertical"> 
                <AlternatingRowStyle BackColor="Transparent" />    

						<FooterStyle BackColor="Transparent" />
                <PagerStyle BackColor="Transparent" ForeColor="Transparent" HorizontalAlign="Right" />
                <RowStyle BackColor="Transparent" />
                <SelectedRowStyle BackColor="Transparent" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="Transparent" />
                <SortedAscendingHeaderStyle BackColor="Transparent" />
                <SortedDescendingCellStyle BackColor="Transparent" />
                <SortedDescendingHeaderStyle BackColor="Transparent" />
               <Columns>
                   <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="docitem" ItemStyle-HorizontalAlign="Right" ItemStyle-ForeColor="#eef2cc" ItemStyle-BackColor="#125478" HeaderStyle-BackColor="Transparent" HeaderStyle-ForeColor="Transparent" >  
           <ItemTemplate>  
              <%#Eval("param")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>   
                   <asp:TemplateField HeaderText="Dem_met" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"  ItemStyle-ForeColor="#dbfffc" ItemStyle-BackColor="#88A9BB" HeaderStyle-BackColor="#125478" HeaderStyle-ForeColor="#eef2cc" >  
           <ItemTemplate>  
               <%#Eval("Dem_met")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="Coal" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-ForeColor="#125478" HeaderStyle-BackColor="#125478" HeaderStyle-ForeColor="#eef2cc" >  
           <ItemTemplate>  
               <%#Eval("Coal")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
                   <asp:TemplateField HeaderText="Gas" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-ForeColor="#125478" HeaderStyle-BackColor="#125478" HeaderStyle-ForeColor="#eef2cc" >  
           <ItemTemplate>  
               <%#Eval("Gas")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="Nuclear" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-ForeColor="#125478" HeaderStyle-BackColor="#125478" HeaderStyle-ForeColor="#eef2cc" >  
           <ItemTemplate>  
               <%#Eval("Nuclear")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="Hydro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-ForeColor="#125478" HeaderStyle-BackColor="#125478" HeaderStyle-ForeColor="#eef2cc" >  
           <ItemTemplate>  
               <%#Eval("Hydro")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="RE" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-ForeColor="#125478" HeaderStyle-BackColor="#125478" HeaderStyle-ForeColor="#eef2cc" >  
           <ItemTemplate>  
               <%#Eval("RE")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
						</Columns>
						
            </asp:GridView>  
                
            <asp:Literal ID="Literal3" runat="server"></asp:Literal>
            </div>

        </div>
    
     <style>
.buttonnn {
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

.buttonnn2 {
  background-color: white; 
  color: black; 
  border: 2px solid #008CBA;
}

.buttonnn2:hover {
  background-color: #008CBA;
  color: white;
}
</style>

  

<br />
  <div id="target" runat="server" class="buttonnn buttonnn2">
  Show / hide tables
</div>
             <script>
                 $("#target").click(function () {
                     $("#myDIV").toggle("slow");
                 });
             </script>
             <div id="myDIV">
        <asp:GridView ID="testgrid" runat="server" CssClass="hover" OnRowDataBound="grd_RowDataBound" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"> 
                <AlternatingRowStyle BackColor="White" />
               <Columns>
                   <asp:TemplateField HeaderText="Block" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("Blk")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>    
                   <asp:TemplateField HeaderText="Dem_met" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("Dem_met")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="Coal_gen" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("T_gen")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="G_gen" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("G_gen")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="N_gen" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("N_gen")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
                   <asp:TemplateField HeaderText="H_gen" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("H_gen")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
                   <asp:TemplateField HeaderText="R_gen" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("R_gen")%>  
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
</div><br /><br />
 <script>
     $(function () {
         $("#<%=DDL_for_state.ClientID%>").select2();
                });
 </script></div>
            <div style="font-size:larger;background-color:#3dd1eb;">
            Select State : &nbsp;&nbsp; <asp:DropDownList ID="DDL_for_state" runat="server" AutoPostBack="true" OnSelectedIndexChanged="State_DDL_change" DataTextField="StateName" DataValueField="StateCode" Width="300px" ></asp:DropDownList>  
        </div><br />
        <script type="text/javascript">  
            $(function () {
                $('#container1').highcharts({
                    chart: {
                        zoomType: 'xy'
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
                        categories: <%= Data_blk1%>,
                        crosshair: true,
                        index: 2,
                    },
                    yAxis: {
                        allowDecimals: false,
                        min: 0,
                        title: {
                            text: 'MW'
                        },
                        Labels: {
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
                        pointFormat: '<span style="color:{series.color}">\u25CF</span> {series.name}: {point.y:.f} <br/>',
                        shared: true,
                        split: false,
                        enabled: true
                    },
                    plotOptions: {
                        area: {
                            stacking: 'normal',
                            lineColor: '#666666',
                            lineWidth: 1,
                            marker: {
                                lineWidth: 1,
                                lineColor: '#666666'
                            }
                        },
                        series: {
                            marker: {
                                enabled: false
                            }
                        }
                    },
                    series: [ {
                        name: 'Own gen',
                            type: 'area',
                            color: '#afe8f9',
                            data: <%= Data_isgs_st%> 
                     }, {
                                name: 'Import',
                            type: 'area',
                            color: '#80ffb6',
                            data: <%= Data_import_st%> 
                     }, {
                                name: 'Demand',
                            type: 'spline',
                            color: '#081414',
                                data: <%= Data_demand_st%> 
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

        <div id="container1" runat="server" style="min-width: 400px; height: 400px; margin: 0 auto;border-right:1px solid green;display: inline-block; width:100%; "></div>
        <br />
        <div id="target1" runat="server" class="buttonnn buttonnn2">
  Show / hide tables
</div>
             <script>
                 $("#target1").click(function () {
                     $("#myDIV1").toggle("slow");
                 });
             </script>
             <div id="myDIV1">
        <asp:GridView ID="testgrid1" runat="server" CssClass="hover" OnRowDataBound="grd_RowDataBound" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"> 
                <AlternatingRowStyle BackColor="White" />
               <Columns>
                   <asp:TemplateField HeaderText="Block" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("Blk")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>    
                   <asp:TemplateField HeaderText="Demand" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("Demand")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="Own Generation" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("ISGS")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="Import" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">  
           <ItemTemplate>  
               <%#Eval("Import_Q")%>  
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
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            <asp:Literal ID="Literal2" runat="server"></asp:Literal>

       
         

    </form>
</body>
</html>
