using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using CascadingStyleSheets;
using PhoenixOI;

namespace CSS
{
    public static class CSSParser
    {

        public static StyleSheet Parse(string str, Request request)
        {
            MemoryStream memStream = new MemoryStream();
            byte[] data = Encoding.ASCII.GetBytes(str);
            memStream.Write(data, 0, data.Length);
            memStream.Position = 0;
            StreamReader sReader = new StreamReader(memStream);

            return Parse(sReader, request);
        }

        public static StyleSheet Parse(StreamReader reader, Request request)
        {
            StyleSheet sheet = new StyleSheet();
            List<string> defaultMedia = new List<string>();
            defaultMedia.Add("all");
            List<string> currentMedia = defaultMedia;


            while (!reader.EndOfStream)
            {
                int intChar;
                char realChar;

                if ((intChar = reader.Peek()) != -1)
                {
                    realChar = (char)intChar;

                    //consume whitespace
                    if (Char.IsWhiteSpace(realChar))
                    {
                        reader.Read();
                        continue;
                    }
                    else if (realChar == '@')
                    {
                        string w = ParseWord(reader);

                        if (w == "@media")
                        {
                            List<string> elements = ParseElements(reader);
                            currentMedia = elements;
                        }

                        if (w == "@input")
                        {
                            //not implemented!
                        }
                        continue;
                    }
                    else if (realChar == '}')
                    {
                        currentMedia = defaultMedia;
                        reader.Read();
                        continue;
                    }
                    //comments, not secure yet!!!
                    else if (realChar == '/')
                    {
                        reader.Read();
                        reader.Read();
                        ParseComment(reader);
                        continue;
                    }

                    //read style
                    else if (Char.IsLetterOrDigit(realChar) || realChar == '#' || realChar == '.' || realChar == '*')
                    {
                        List<string> elements = ParseElements(reader);
                        Style csss = ParseStyleBody(reader, request);
                        csss.media = currentMedia;

                        foreach (string s in elements)
                        {
                            csss.selector = s;
                            sheet[s] = csss.Clone();
                        }
                    }
                    else
                    {
                        reader.Read();
                    }
                }
                else
                {
                    break;
                }
            }
            return sheet;
        }

        public static string ParseWord(StreamReader reader)
        {
            int intChar;
            char realChar;
            string buffer = "";

            while (!reader.EndOfStream)
            {
                if ((intChar = reader.Read()) != -1)
                {
                    realChar = (char)intChar;

                    //new selector
                    if (Char.IsWhiteSpace(realChar))
                    {
                        break;
                    }

                    buffer += realChar;
                }
            }
            return buffer;
        }

        public static List<string> ParseElements(StreamReader reader)
        {
            int intChar;
            char realChar;
            string buffer = "";
            List<string> lst = new List<string>();

            while (!reader.EndOfStream)
            {
                if ((intChar = reader.Read()) != -1)
                {
                    realChar = (char)intChar;

                    //possible beginning of comment section
                    if (realChar == '/')
                    {
                        int p = reader.Peek();
                        char peekChar = (char)p;
                        if (peekChar == '*')
                        {
                            ParseComment(reader);
                        }
                    }

                    //beginning of style body
                    if (realChar == '{')
                    {
                        lst.Add(buffer.Trim().ToLower());
                        buffer = "";
                        break;
                    }

                    //new selector
                    if (realChar == ',')
                    {
                        lst.Add(buffer.Trim().ToLower());
                        buffer = "";
                        continue;
                    }

                    buffer += realChar;
                }
            }
            return lst;
        }

        private static string ReadString(StreamReader reader, char stringDelim)
        {
            StringBuilder stringBuilder = new StringBuilder();

            int intChar;
            char realChar = ' ';

            while ((intChar = reader.Read()) != -1)
            {
                realChar = (char)intChar;

                if (realChar == stringDelim)
                    break;

                stringBuilder.Append(realChar);
            }
            return stringBuilder.ToString();
        }

        public static string ParseComment(StreamReader reader)
        {
            int intChar;
            char realChar;
            string buffer = "";

            while (!reader.EndOfStream)
            {
                if ((intChar = reader.Read()) != -1)
                {
                    realChar = (char)intChar;

                    //possible beginning of comment section
                    if (realChar == '*')
                    {
                        int p = reader.Peek();
                        char peekChar = (char)p;
                        if (peekChar == '/')
                        {
                            reader.Read();
                            break;
                        }
                    }

                    buffer += realChar;
                }
            }
            return buffer;
        }

        //currently just skip media definitions to avoid problems
        /*public static void ParseMedia(StreamReader reader)
        {
            int intChar;
            char realChar;
            int lvl = 1;

            while (!reader.EndOfStream)
            {
                if ((intChar = reader.Read()) != -1)
                {
                    realChar = (char)intChar;

                    if (realChar == '{')
                    {
                        lvl++;
                    }

                    if (realChar == '}')
                    {
                        lvl--;
                        if (lvl == 0)
                            break;
                    }
                }
            }
        }*/

        public static Style ParseStyleBody(string inlineStyle, Request request)
        {
            MemoryStream memStream = new MemoryStream();
            byte[] data = Encoding.ASCII.GetBytes(inlineStyle);
            memStream.Write(data, 0, data.Length);
            memStream.Position = 0;
            StreamReader sReader = new StreamReader(memStream);

            return ParseStyleBody(sReader, request);
        }

        public static Style ParseStyleBody(StreamReader reader, Request request)
        {
            string buffer = "";
            int intChar;
            char realChar;
            string name = "";
            List<string> values = new List<string>();
            Style sty = new Style(request);

            while (!reader.EndOfStream)
            {
                if ((intChar = reader.Read()) != -1)
                {
                    realChar = (char)intChar;

                    if (realChar == '/')
                    {
                        int p = reader.Peek();
                        char peekChar = (char)p;
                        if (peekChar == '*')
                        {
                            ParseComment(reader);
                        }
                        continue;
                    }

                    if (realChar == '}')
                    {
                        if (buffer != "")
                            values.Add(buffer);
                        if (values.Contains("!important"))
                        {
                            sty.bImportant = true;
                            values.Remove("!important");
                        }
                        buffer = "";
                        string x = "";
                        for (int i = 0; i < values.Count; i++)
                        {
                            if (i > 0)
                                x += " ";
                            x += values[i];
                        }
                        //Console.WriteLine(name + ":" + x);
                        sty[name] = x;
                        name = "";
                        values.Clear();
                        break;
                    }

                    if (realChar == ';')
                    {
                        if (buffer != "")
                            values.Add(buffer);
                        if (values.Contains("!important"))
                        {
                            sty.bImportant = true;
                            values.Remove("!important");
                        }
                        buffer = "";
                        string x = "";
                        for (int i = 0; i < values.Count; i++)
                        {
                            if (i > 0)
                                x += " ";
                            x += values[i];
                        }
                        //Console.WriteLine(name + ":" + x);
                        sty[name] = x;
                        name = "";
                        values.Clear();
                        continue;
                    }

                    if (realChar == ':')
                    {
                        name = buffer;
                        buffer = "";
                        continue;
                    }

                    if (realChar == '"')
                    {
                        buffer += ReadString(reader, '"');
                    }

                    if (realChar == '\'')
                    {
                        buffer += ReadString(reader, '\'');
                    }

                    if (!Char.IsWhiteSpace(realChar))
                    {
                        buffer += realChar;
                    }
                    else
                    {
                        if (buffer != "")
                            values.Add(buffer);
                        buffer = "";
                    }
                }
            }
            return sty;
        }
    }
}
