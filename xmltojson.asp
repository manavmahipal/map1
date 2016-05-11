<%
Set objXML = Server.CreateObject("Microsoft.XMLDOM")

objXML.async = False
objXML.Load ("http://www.indianpowerstations.org/datafiles/appview.xml")

 Set objname = Server.CreateObject("Microsoft.XMLDOM")

 Set objdc = Server.CreateObject("Microsoft.XMLDOM")

 Set objsg = Server.CreateObject("Microsoft.XMLDOM")

 Set objtime= Server.CreateObject("Microsoft.XMLDOM")

Set objcap= Server.CreateObject("Microsoft.XMLDOM")

Set objcapcomml= Server.CreateObject("Microsoft.XMLDOM")

Set objavl= Server.CreateObject("Microsoft.XMLDOM")

Set objbar= Server.CreateObject("Microsoft.XMLDOM")

Set objtot= Server.CreateObject("Microsoft.XMLDOM")

Set objtotgen= Server.CreateObject("Microsoft.XMLDOM")

Set objblk= Server.CreateObject("Microsoft.XMLDOM")

Set objavgfreq= Server.CreateObject("Microsoft.XMLDOM")

Set objfreq= Server.CreateObject("Microsoft.XMLDOM")

Set objstat= Server.CreateObject("Microsoft.XMLDOM")

Set objrgn= Server.CreateObject("Microsoft.XMLDOM")
if objXML.readyState=4 then

Set objtime = objXML.getElementsByTagName("TIME")

Set objblk = objXML.getElementsByTagName("BLK")

Set objrgn = objXML.getElementsByTagName("REGION")

Set objtyp = objXML.getElementsByTagName("TYPE")

Set objname = objXML.getElementsByTagName("NAME")

Set objdc = objXML.getElementsByTagName("DC")

Set objsg = objXML.getElementsByTagName("SG")

Set objcap = objXML.getElementsByTagName("STNCAP")
outp = ""
c = chr(34)

For i = 0 to (objname.length-1)
if ((trim(objtyp.item(i).text)<>"S") and (trim(objtyp.item(i).text)<>"J")) then
	if outp<>"" then
	outp=outp &","
	end if
		outp = outp & "{" & c & "Name" & c & ":" & c & objname.item(i).text & c & ","
		outp = outp & c & "Type" & c & ":" & c & objtyp.item(i).text & c & ","
		outp = outp & c & "Cap" & c & ":" & c & objcap.item(i).text & c & ","
		outp = outp & c & "DC" & c & ":" & c & objdc.item(i).text & c & ","
		outp = outp & c & "SG" & c & ":" & c & objsg.item(i).text & c & "}"

end if
next
end if

outp ="{" & c & "records" & c & ":[" & outp & "]}"
Response.Write outp
%>