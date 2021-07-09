<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="All_India_MO.aspx.cs" Inherits="merit.All_India_MO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> 
  <meta charset="utf-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <title>jQuery UI Datepicker - Default functionality</title>
  
<script src="./JS/jquery-1.4.4.min.js" type="text/javascript"></script>
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


     

 <link type="text/css" rel="Stylesheet" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/themes/redmond/jquery-ui.css" /> 

    
    <script src="./Highcharts-9.1.0/code/highcharts.js"></script>
<script src="./Highcharts-9.1.0/code/highcharts-more.js"></script>
<script src="./Highcharts-9.1.0/code/modules/solid-gauge.js"></script>
<script src="./Highcharts-9.1.0/code/modules/exporting.js"></script>
<script src="./Highcharts-9.1.0/code/modules/export-data.js"></script>
<script src="./Highcharts-9.1.0/code/modules/accessibility.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <!-- <div style="width:100%;height: 50px; background-color: deepskyblue; background-image: radial-gradient( #c6e9f7,#05adf0 );font-family:'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size:30px;text-align:center;vertical-align:middle;"><b>Merit Order Dashboard</b></div>
       <br/><br/> --><div style="font-size:larger;">
            Select Date : &nbsp;&nbsp;     <asp:Textbox ID="Date_Input" AutoPostBack="false"   runat="server" Width="150px" class="Calender" ></asp:Textbox>  
           <asp:Button ID="date_filter" runat="server" OnClick="date_DDL_change" Text="  Click to filter data  " />
                     </div><br/><br/>
            <asp:Literal ID = "ltrChart" runat = "server" > </asp:Literal>  

         <asp:Literal ID = "Literal1" runat = "server" > </asp:Literal> 
    
         <asp:GridView ID="testgrid" runat="server" CssClass="hover" OnRowDataBound="grd_RowDataBound" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"> 
                <AlternatingRowStyle BackColor="White" />
               <Columns>
							<asp:TemplateField HeaderText="display_name" HeaderStyle-HorizontalAlign="Center">  
           <ItemTemplate>  
               <%#Eval("display_name")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField>  
                   <asp:TemplateField HeaderText="VC" HeaderStyle-HorizontalAlign="Center">  
           <ItemTemplate>  
               <%#Eval("VC")%>  
           </ItemTemplate>  
                                <HeaderStyle HorizontalAlign="Center" />
       </asp:TemplateField> 
                   <asp:TemplateField HeaderText="Installed Cap (MW)" HeaderStyle-HorizontalAlign="Center">  
           <ItemTemplate>  
               <%#Eval("Installed_cap_MW")%>  
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

    </form>
</body>
</html>