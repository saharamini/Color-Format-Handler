using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ColorFormatHandler
{
    public class ColorFormat : IHttpHandler
    {
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse Response = context.Response;
            HttpRequest Request = context.Request;

            // grab the file's contents
            string output = string.Empty;
            StreamReader sr = File.OpenText(Request.PhysicalPath);
            string contents = sr.ReadToEnd();
            sr.Close();

            // determine how to format the file based on its extension
            string extension = Path.GetExtension(Request.PhysicalPath).ToLower();

            SyntaxHighlighter highlighter;

            // e.g click.js
            if (extension == ".js")
            {
                highlighter = SyntaxHighlighter.GetHighlighter(SyntaxHighlighter.SyntaxType.JavaScript);
                output = highlighter.Highlight(contents);

            }

            // javascript in HTML page <script></script>
            else if (extension == ".aspx")
            {

                highlighter = SyntaxHighlighter.GetHighlighter(SyntaxHighlighter.SyntaxType.JavaScriptInHtml);
                output = highlighter.Highlight(contents);
               
            }

            // unknown extention
            else
            {
                output = contents;
            }

            // output the formatted contents 
            Response.Write("<html><body><pre>");
            Response.Write(output);
            Response.Write("</pre></body></html>");
          
        }
    }
}
