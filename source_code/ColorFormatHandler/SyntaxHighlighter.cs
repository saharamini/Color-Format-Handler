using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorFormatHandler
{
    public class SyntaxHighlighter
    {
        public virtual string Highlight(string contents)
        {
            return "";
        }

        public static SyntaxHighlighter GetHighlighter(SyntaxType synType)
        {
            if (synType.Equals(SyntaxType.JavaScript))
            {
                return new JavaScriptHighlighter();
            }
            else if (synType.Equals(SyntaxType.JavaScriptInHtml))
            {
                return new JavaScriptInHtmlHighlighter();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public enum SyntaxType
        {
            JavaScript = 1,
            JavaScriptInHtml =2
        }
    }

}
