using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    public abstract class DOMParserRule
    {
        public DOMParser parser;
        public static string Name = "";

        public DOMParserRule(DOMParser p)
        {
            parser = p;
        }

        /// <summary>
        /// Handles a given grammar rule of the form "Rule1 | Rule2 | ... | RuleN".
        /// </summary>
        /// <param name="name">The grammar rule to handle.</param>
        /// <returns>The number of objects in document stream matching the grammar rule.</returns>
        public int OrRule(string name)
        {
            string[] separator = { " | " };
            string[] rules = name.Split(separator, StringSplitOptions.None);

            //return the highest number of matches of all rules
            int max = Rule(rules[0]);
            for (int i = 1; i < rules.Length; i++)
            {
                max = Math.Max(max, Rule(rules[i]));
            }
            return max;
        }

        /// <summary>
        /// Handles a given grammar rule of the form "Rule1 Rule2 ... RuleN".
        /// </summary>
        /// <param name="name">The grammar rule to handle.</param>
        /// <returns>The number of objects in document stream matching the grammar rule.</returns>
        public int AndRule(string name)
        {
            char[] separator = { ' ' };
            string[] rules = name.Split(separator, StringSplitOptions.None);

            //return the total number of matches of all rules
            int total = 0;
            for (int i = 0; i < rules.Length; i++)
            {
                int minMatches = 1;
                int currentMatches = Rule(rules[i]);

                //catch optional match
                if (rules[i].StartsWith("[") && rules[i].EndsWith("]"))
                {
                    minMatches = 0;
                }
                //parse new minimum number of matches
                if (rules[i].StartsWith("{") && !rules[i].EndsWith("}"))
                {
                    int endPos = rules[i].LastIndexOf("}");
                    minMatches = int.Parse(rules[i].Remove(0, endPos + 1));
                }

                //actual number of matches less than required number of matches
                if (currentMatches < minMatches)
                {
                    return 0;
                }
                total += currentMatches;
            }
            return total;
        }

        /// <summary>
        /// Handles a given grammar rule of the form "{Rule1}N".
        /// </summary>
        /// <param name="name">The grammar rule to handle.</param>
        /// <returns>The number of objects in document stream matching the grammar rule.</returns>
        public int MinLoopRule(string name)
        {
            //parse new minimum number of matches
            int endPos = name.LastIndexOf("}");
            int minMatches = int.Parse(name.Remove(0, endPos + 1));

            int total = LoopRule(name.Substring(0, endPos + 1));

            //actual number of matches less than required number of matches
            if (total < minMatches)
                return 0;
            else
                return total;
        }

        /// <summary>
        /// Handles a given grammar rule of the form "{Rule1}".
        /// </summary>
        /// <param name="name">The grammar rule to handle.</param>
        /// <returns>The number of objects in document stream matching the grammar rule.</returns>
        public int LoopRule(string name)
        {
            int total = 0;
            string coreRule = name.Substring(1, name.Length - 2);

            int currentMatches = Rule(coreRule);
            while (currentMatches > 0)
            {
                total += currentMatches;
                currentMatches = Rule(coreRule);
            }
            return total;
        }

        /// <summary>
        /// Recursively handles a given grammar rule in disjunctive normal form.
        /// </summary>
        /// <param name="name">The grammar rule to handle.</param>
        /// <returns>The number of objects in document stream matching the grammar rule.</returns>
        public int Rule(string name)
        {
            //handle Or conditions
            if (name.Contains(" | "))
            {
                return OrRule(name);
            }
            //handle And conditions
            else if (name.Contains(" "))
            {
                return AndRule(name);
            }
            //handle single rules
            else
            {
                //handle tags
                if (name.StartsWith("<") && name.EndsWith(">"))
                    return Tag(name.Substring(1, name.Length - 2));
                //handle optional tags
                else if (name.StartsWith("[") && name.EndsWith("]"))
                        return Rule(name.Substring(1, name.Length - 2));
                //handle repetition matches with given minimum
                else if (name.StartsWith("{") && !name.EndsWith("}"))
                {
                    return MinLoopRule(name);
                }
                //handle repetition matches
                else if (name.StartsWith("{") && name.EndsWith("}"))
                {
                    return LoopRule(name);
                }
                //handle single non-tag rules
                else
                    return parser.Rule(name);
            }
        }

        /// <summary>
        /// Creates a HTMLElement for a given tag to add to the Document.
        /// </summary>
        /// <param name="tag">The given tag to create HTMLElement for.</param>
        /// <returns>The created HTMLElement.</returns>
        public virtual HTMLElement CreateElement(TagInfo tag)
        {
            //create a generic HTMLElement
            HTMLElement e = new HTMLElement(tag,parser.doc);
            //System.Windows.Forms.MessageBox.Show("General");

            return e;
        }

        /// <summary>
        /// Tries to match a given tagname to a tag in the document stream. 
        /// Also adds an HTMLElement for tag to DOM if matched successfully.
        /// </summary>
        /// <param name="name">The tagname to match.</param>
        /// <returns>The number of tags matched in the stream (always 0 or 1).</returns>
        public int Tag(string name)
        {
            TagInfo tag = parser.MatchTag(name);

            //found a matching tag in stream
            if (tag != null)
            {
                System.Console.WriteLine("Matched:" + tag.Name);
                //if tag needs to be added to DOM create an appropriate Element to add
                HTMLElement e = null;
                if (!(tag.TagCategory == TagCategories.TagEnd))
                {
                    e = CreateElement(tag);
                }
                //insert Element into the document
                parser.HandleTag(e, tag.TagCategory == TagCategories.TagEnd, tag.IsParentTag);
                return 1;
            }
            //found no matching tag in stream
            else
            {
                return 0;
            }
        }

        public int Tag()
        {
            TagInfo tag = parser.MatchTag();

            //found a matching tag in stream
            if (tag != null)
            {
                System.Console.WriteLine("Matched:" + tag.Name);
                //if tag needs to be added to DOM create an appropriate Element to add
                HTMLElement e = null;
                if (!(tag.TagCategory == TagCategories.TagEnd))
                {
                    e = CreateElement(tag);
                }
                //insert Element into the document
                parser.HandleTag(e, tag.TagCategory == TagCategories.TagEnd, tag.IsParentTag);
                return 1;
            }
            //found no matching tag in stream
            else
            {
                return 0;
            }
        }

        public int PlainText()
        {
            string text = parser.MatchPlain_Text();

            //found a matching tag in stream
            if (text != null)
            {
                System.Console.WriteLine("Matched text:" + text);
                //create a text node to add to the Document
                Text t = null;
                t = new Text(text);
                t.ownerDocument = parser.doc;

                //insert Node into the document and go to next object in stream
                parser.HandleTag(t, false, false);
                return 1;
            }
            //found no matching tag in stream
            else
            {
                return 0;
            }
        }

        public int LiteralText()
        {
            string text = parser.MatchLiteral_Text();

            //found a matching tag in stream
            if (text != null)
            {
                System.Console.WriteLine("Matched text:" + text);
                //create a text node to add to the Document
                Text t = null;
                t = new Text(text);
                t.ownerDocument = parser.doc;

                //insert Node into the document and go to next object in stream
                parser.HandleTag(t, false, false);
                return 1;
            }
            //found no matching tag in stream
            else
            {
                return 0;
            }
        }

        public abstract int ExecuteRule();
    }
}
