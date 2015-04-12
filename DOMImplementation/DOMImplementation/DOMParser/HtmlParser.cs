using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DOMCore
{
    /// <summary>
    /// Summary description for ParseHTML.
    /// </summary>

    public class HtmlParser //: Parser
    {
        private bool atTagStart = false;
        private bool inScript = false; //whether we are currently within a script tag
        private StringBuilder sourceBuilder;

        public string Source
        {
            get { return sourceBuilder.ToString(); }
        }

        public HtmlParser()
        {
        }

        public List<TagInfo> ParseHTML(StreamReader reader)
        {
            sourceBuilder = new StringBuilder(1000);
            List<TagInfo> objstream = new List<TagInfo>();

            //Source = page;

            //parse string into list of AttributeLists
            while (!reader.EndOfStream)
            {
                string sBuilder = null;

                if (inScript)
                    sBuilder = ReadScript(reader);
                else
                    sBuilder = ReadToNextTag(reader);

                if (sBuilder != null && sBuilder.Length != 0)
                {
                    TagInfo t = new TagInfo("#text");

                    //remove redundant whitespace from text
                    t.Value = sBuilder.ToString();//.Trim(); // <----------------
                    if (t.Value != "")
                        objstream.Add(t);
                }

                //found a tag
                if (atTagStart)
                {
                    //parse tag
                    TagInfo tag = ReadTag(reader);

                    //add tag to list
                    objstream.Add(tag);
                }
            }
            return objstream;
        }

        private string ReadToNextTag(StreamReader reader)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int intChar;
            char realChar;
            char lastChar = '\0';

            atTagStart = false;

            // Read until it hits a '<'.
            while ((intChar = reader.Read()) != -1)
            {
                realChar = (char)intChar;
                sourceBuilder.Append(realChar);

                if ((realChar == '\n') || (realChar == '\r') || (realChar == '\t'))
                {
                    continue;
                }

                /*if (inScript && (realChar == '\"' || realChar == '\''))
                {
                    stringBuilder.Append(realChar);
                    stringBuilder.Append(ReadString(reader, realChar));
                    stringBuilder.Append(realChar);
                }
                else */if (realChar == '<')
                {
                    atTagStart = true;
                    break;
                }
                else if (!(lastChar == ' ' && realChar == ' '))
                {
                    stringBuilder.Append(realChar);
                    lastChar = realChar;
                }
            }
            return stringBuilder.ToString();
        }

        private string ReadScript(StreamReader reader)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int intChar;
            char realChar;
            char lastChar = '\0';

            atTagStart = false;

            // Read until it hits a '<'.
            while ((intChar = reader.Read()) != -1)
            {
                realChar = (char)intChar;
                sourceBuilder.Append(realChar);

                if ((realChar == '\n') || (realChar == '\r') || (realChar == '\t'))
                {
                    continue;
                }

                if (inScript && (realChar == '\"' || realChar == '\''))
                {
                    stringBuilder.Append(realChar);
                    stringBuilder.Append(ReadString(reader, realChar));
                    stringBuilder.Append(realChar);
                }
                else if (realChar == '<')
                {
                    Char peekChar = (char)reader.Peek();
                    if (peekChar == '/')
                    {
                        atTagStart = true;
                        break;
                    }
                }
                else if (!(lastChar == ' ' && realChar == ' '))
                {
                    stringBuilder.Append(realChar);
                    lastChar = realChar;
                }
            }
            return stringBuilder.ToString();
        }

        private string ReadString(StreamReader reader, char stringDelim)
        {
            StringBuilder stringBuilder = new StringBuilder();

            int intChar;
            char realChar = ' ';

            while ((intChar = reader.Read()) != -1)
            {
                realChar = (char)intChar;
                sourceBuilder.Append(realChar);

                if (realChar == stringDelim)
                    break;

                stringBuilder.Append(realChar);
                //s += realChar;
            }
            return stringBuilder.ToString();
        }

        private TagInfo ReadTag(StreamReader reader)
        {
            string tagName = "";
            TagInfo result = new TagInfo();

            Char character = GetNextCharacter(reader);
            if (character != (char)0)
            {
                if (char.IsLetter(character))
                {
                    result.TagType = TagTypes.Element;
                    tagName = character + ReadWord(reader);

                    result.Name = tagName.ToLower();

                    // Read Attributes
                    if (ParseTagAttributes(reader, result))
                    {
                        result.TagCategory = TagCategories.InlineTag;
                    }
                    else
                    {
                        result.TagCategory = TagCategories.TagStart;
                    }

                    if (tagName == "script")
                        inScript = true;
                }
                else if (character == '!')
                {
                    string next = ReadWord(reader).ToLower();
                    switch (next)
                    {
                        case "--":

                            // remove "--" from the end of the string
                            string comment = ReadToTagEnd(reader);
                            char[] trims = { '-' };
                            comment = comment.TrimEnd(trims);
                            //comment = comment.Substring(0, comment.Length - 2);
                            comment.Trim();

                            result.Name = "!--";
                            result.Value = comment;
                            result.TagCategory = TagCategories.Special;
                            result.TagType = TagTypes.Comment;
                            break;
                        case "doctype":

                            ReadToTagEnd(reader);
                            result.Name = "!doctype";
                            result.TagCategory = TagCategories.Special;
                            result.TagType = TagTypes.Doctype;
                            break;
                        default:
                            result.Name = ReadToTagEnd(reader);
                            result.TagCategory = TagCategories.InlineTag;
                            result.TagType = TagTypes.Unkown;
                            break;
                    }

                }
                else if (character == '/')
                {
                    inScript = false;
                    result.TagType = TagTypes.Element;
                    result.TagCategory = TagCategories.TagEnd;
                    result.Name = '/' + ReadWord(reader).ToLower();

                    // Read to tag end
                    ReadToTagEnd(reader);
                }
                else
                {
                    // Read to tag end
                    ReadToTagEnd(reader);

                    /// <||| Fix> handle unkown types
                    result.Name = "UnkownElement";
                    result.TagCategory = TagCategories.InlineTag;
                    result.TagType = TagTypes.Unkown;
                }
            }
            else
            {
                // Error Occured
                result.Name = "FileEnd";
                result.TagCategory = TagCategories.Special;
                result.TagType = TagTypes.EndOfFile;
            }
            return result;
        }

        private char GetNextCharacter(StreamReader reader)
        {
            int intChar;
            char realChar = ' ';

            while ((intChar = reader.Read()) != -1)
            {
                realChar = (char)intChar;
                sourceBuilder.Append(realChar);

                if (!char.IsWhiteSpace(realChar))
                {
                    return realChar;
                }
            }
            return (char)0;
        }
        /// <summary>
        /// Reads from the currente character to the first whitespace character encountered
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private string ReadWord(StreamReader reader)
        {
            StringBuilder wordBuilder = new StringBuilder();

            int intChar;
            char realChar = ' ';

            while ((intChar = reader.Peek()) != -1)
            {
                realChar = (char)intChar;
                sourceBuilder.Append(realChar);

                /*if (realChar == '<')
                {
                    atTagStart = true;
                    break;
                }*/

                if (char.IsWhiteSpace(realChar) || realChar == '/' || realChar == '>')
                {
                    break;
                }

                wordBuilder.Append(realChar);
                //word += realChar;
                reader.Read();
            }

            return wordBuilder.ToString();
        }

        private string ReadToTagEnd(StreamReader reader)
        {
            StringBuilder tagTextBuilder = new StringBuilder();

            int intChar;
            char realChar = ' ';

            while ((intChar = reader.Read()) != -1)
            {
                realChar = (char)intChar;

                /*if (realChar == '<')
                {
                    atTagStart = true;
                    break;
                }*/

                if (realChar == '>')
                {
                    break;
                }

                sourceBuilder.Append(realChar);
                tagTextBuilder.Append(realChar);
                //tagText += realChar;
            }
            return tagTextBuilder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="info"></param>
        /// <returns>True, if element is inline</returns>
        private bool ParseTagAttributes(StreamReader reader, TagInfo info)
        {
            // Parsing
            int intChar;
            char realChar = ' ';

            string attributeName = "";
            string attributeValue = "";

            bool isInline = false;
            bool attribLeft = true;

            while ((intChar = reader.Read()) != -1)
            {
                realChar = (char)intChar;

                if (realChar != '>')
                {
                    sourceBuilder.Append(realChar);

                    if (realChar == '/' && attribLeft)
                    { 
                        isInline = true;
                    }
                    // Getting name or value?
                    else if (realChar == '=')
                    { 
                        attribLeft = false;
                        continue;
                    }

                    // Parse name
                    if (attribLeft)
                    {
                        if (!char.IsWhiteSpace(realChar))
                        {
                            attributeName += realChar;
                        }
                    }
                    // Parse value
                    else
                    {
                        // Only get text inside quotes
                        if (realChar == '\"' || realChar == '\'')
                        {
                            attributeValue += ReadString(reader, realChar);
                            info.Attributes.Add(attributeName.ToLower(), attributeValue);

                            attributeName = "";
                            attributeValue = "";
                            attribLeft = true;
                        }
                        else if (realChar == '>')
                        {
                            info.Attributes.Add(attributeName.ToLower(), attributeValue);

                            attributeName = "";
                            attributeValue = "";
                            attribLeft = true;
                        }
                        else if (char.IsWhiteSpace(realChar))
                        {
                            if (attributeValue.Trim() != "")
                            {
                                info.Attributes.Add(attributeName.ToLower(), attributeValue);

                                attributeName = "";
                                attributeValue = "";
                                attribLeft = true;
                            }
                        }
                        else
                        {
                            attributeValue += realChar;
                        }
                    }
                }
                else
                {
                    // End found
                    break;
                }
            }

            return isInline;
        }
    }
}