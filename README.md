# Color_Format_Handler

There are two options in this project:
Option 1. External JS, consider, a user wants to see a javascript file (example.js)
Oprion 2. Internal JS, consider, a user wants to see javascript code <script></script> which is inside 
an asp.net page (Default.aspx)

following these steps to run ColorFormatHandler.dll file:

1. create "javascript" folder in "wwwroot" folder which is in "inetpub" folder, create a "bin" folder inside 
the "javascript" folder.

2. paste the ColorFormatHandler.dll file in "bin" folder.

3. go to IIS

4. right click on the "javascript" folder, choose "Convert to Application", so first we have to convert the folder
to application and then add a handler.

5. on the "javascript" folder, click on the "Handler Mappings"

6. click on Add Managed Handler...

7. for the "Request path" *.js - this is for javascript file (example.js)

8. for the "Type:" ColorFormatHandler.ColorFormat

9. after clicking OK , the configuration file will be created in the "javascript" folder 

10. before adding *.aspx, you need to REMOVE all paths which start with *.aspx  <-----------

11. after removing them, click on Add Managed Handler

12. for the "Request path" *.aspx - this is for <script></script> (Default.aspx),  "Type:" ColorFormatHandler.ColorFormat


OPTION 1: Please place JS file in inetpub->wwwroot->"javascript" folder
"http://localhost/javascript/example.js"

IN OPTION 2: Please place asp.net file ("Default.aspx") in inetpub->wwwroot->"javascript" folder
"http://localhost/javascript/Default.aspx"

Removed all *.aspx and add my own *.aspx in IIS
my configuration file:

--<?xml version="1.0" encoding="UTF-8"?>
<configuration>
    <system.webServer>
        <handlers>
            <remove name="PageHandlerFactory-ISAPI-4.0_64bit" />
            <remove name="PageHandlerFactory-ISAPI-4.0_32bit" />
            <remove name="PageHandlerFactory-ISAPI-2.0-64" />
            <remove name="PageHandlerFactory-ISAPI-2.0" />
            <remove name="PageHandlerFactory-Integrated-4.0" />
            <remove name="PageHandlerFactory-Integrated" />
            <add name="javaScript" path="*.aspx" verb="*" type="ColorFormatHandler.ColorFormat" resourceType="Unspecified" preCondition="integratedMode" />
            <add name="javaScriptFile" path="*.js" verb="*" type="ColorFormatHandler.ColorFormat" resourceType="Unspecified" preCondition="integratedMode" />
        </handlers>
    </system.webServer>
</configuration>

------
please don't forget to enable Directory Browsing by going to IIS, click on the project, choose Directory Browsing,
and click enable
