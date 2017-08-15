using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ColorFormatHandler
{
    class JavaScriptInHtmlHighlighter : SyntaxHighlighter
    {
        public string[] keywords = { "break","case","catch","class","const","continue","debugger","default","delete","else","export"," in ", "_Default","C#",
                                     "extends","finally","for ","function","if","import","instanceof","new","return","super","switch"," this ","throw","try","typeof",
                                     "var","void","while","with","yield", "enum", "implements", "interface", "let", "package", "private", "protected", "public", "static", "undefined","false","true"," null " };
        public string[] quotationmarks = { "\"", "\'" };
        public string[] htmltag = { "<", ">" };
        public string[] words = { "type","server","id","runat","value", "xmlns","html","script ","Path","ID","/script", "Language", "AutoEventWireup", "CodeFile", "Inherits" };
        public string[] lineTerminators = { "; ", "{", "}" };
        public string[] tag = { "&lt;", "&gt;"};

        public override string Highlight(string contents)
        {
            string replaceStr = contents;
            int indentLevelCount = 0;

            // change all tag to these ro be able to see the html code in browser
            foreach (string s in htmltag)
            {
                if (Regex.IsMatch(contents, s))
                {
                    if(s == "<")
                        replaceStr = Regex.Replace(replaceStr, s, "&lt;");
                    else
                        replaceStr = Regex.Replace(replaceStr, s, "&gt;");
                }
            }
            // change the color 
            foreach (string s in tag)
            {
                if (Regex.IsMatch(replaceStr, s))
                {
                    replaceStr = Regex.Replace(replaceStr, s, "<span style='color:blue'>" + s + "</span>");
                }
            }

            // script tag
            foreach (string s in words)
            {
                if (Regex.IsMatch(replaceStr, s, RegexOptions.IgnoreCase))
                {
                    replaceStr = Regex.Replace(replaceStr, s, "<span style='color:red'>" + s + "</span>");
                }
            }

            //
            // highlight keywords in blue
            //
            foreach (string s in keywords)
                {
                    if (Regex.IsMatch(contents, s, RegexOptions.IgnoreCase))
                    {
                        replaceStr = Regex.Replace(replaceStr, s, "<span style='color:blue'>" + s + "</span>");
                    }
                }

            //
            // find quotation marks and highlight them and their values in red
            //

            // find any string and any space which is in double quotes
            Regex regex = new Regex("([\"\'][a-zA-Z\r\n\t\f\v ]+[a-zA-Z]+[\"\'])");
            Match match = regex.Match(replaceStr);
                while (match.Success)
                {
                    replaceStr = Regex.Replace(replaceStr, match.Value, "<span style='color:darkred'>" + match.Value + "</span>");
                    match = match.NextMatch();
                }


                //
                // highlight comments in green and put them on new lines
                //
                StringBuilder sb = new StringBuilder(string.Empty);
                StringReader reader = new StringReader(replaceStr);
                string line = string.Empty;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("//") || line.Contains("/*"))
                    {
                        sb.Append("<span style='color:green'>" + line + "</span><br>");
                    }
                    else
                    {
                        sb.AppendLine(line);
                    }
                }
                replaceStr = sb.ToString();

                reader = null;
                sb = null;

                //
                // add indentation to enhance readability
                //
                // NOTE: better indentation would probably require
                // a style sheet. Are you up to giving this a shot
                // dear students?? :-)
                //
                reader = new StringReader(replaceStr);
                line = string.Empty;
                sb = new StringBuilder(string.Empty);
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("}"))
                    {
                        indentLevelCount--;
                        sb.AppendLine(line + "</BLOCKQUOTE>");
                    }
                    else
                    {
                        if (line.Contains("{"))
                        {
                            indentLevelCount++;
                            sb.Append("<BLOCKQUOTE>" + line);
                        }
                        else
                        {
                            sb.AppendLine(line);
                        }
                    }
                }
                replaceStr = sb.ToString();

                reader = null;
                sb = null;

                //
                // put curly braces on new lines
                //
                foreach (string s in lineTerminators)
                {
                    if (Regex.IsMatch(contents, s, RegexOptions.IgnoreCase))
                    {
                        if (s.Contains("{"))
                        {
                            replaceStr = Regex.Replace(replaceStr, s, "<br>" + s + "\n<br>");
                        }
                        else
                        {
                            replaceStr = Regex.Replace(replaceStr, s, s + "\n<br>");
                        }
                    }
                }


            return replaceStr;

        }

    }
}
