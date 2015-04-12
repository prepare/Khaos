using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CascadingStyleSheets;
using DOMCore;

namespace CSS
{
    public class StyleSelector
    {
        /// <summary>
        /// Returns whether a style with given selector applies to a given tag.
        /// </summary>
        /// <param name="s">The selector of the style.</param>
        /// <param name="tag">The tag to check against.</param>
        /// <returns>True, if the selector applies to the given tag. False otherwise.</returns>
        public static bool StyleMatchesTag(Style s, IHTMLElement tag)
        {
            if (!s.media.Contains("all") && !s.media.Contains("screen"))
                return false;

            return SelectorMatchesTag(s.selector, tag);
        }

        public static bool SelectorMatchesTag(string selector, IHTMLElement tag)
        {
            //composite hierarchical selector
            if (selector.Contains(" "))
            {
                string[] parts = selector.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1)
                {
                    if (!SelectorMatchesTag(parts[parts.Length - 1], tag))
                    {
                        return false;
                    }
                    int i = parts.Length - 2;
                    INode parent;
                    parent = tag.ParentNode;
                    while (parent != tag.OwnerDocument)
                    {
                        if (parent is IHTMLElement)
                        {
                            if (SelectorMatchesTag(parts[i], (IHTMLElement)parent))
                            {
                                if (i == 0)
                                    return true;
                                else
                                    i--;
                            }
                        }
                        parent = parent.ParentNode;
                    }
                }
            }

            //composite selector with class
            if (selector.Contains(".") && !selector.StartsWith("."))
            {
                string[] parts = selector.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    return (SelectorMatchesTag(parts[0], tag) && SelectorMatchesTag("." + parts[1], tag));
                }
            }

            //composite selector with id
            if (selector.Contains("#") && !selector.StartsWith("#"))
            {
                string[] parts = selector.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    return (SelectorMatchesTag(parts[0], tag) && SelectorMatchesTag("#" + parts[1], tag));
                }
            }

            //universal selector
            if (selector == "*")
            {
                return true;
            }
            //tag name selector
            if (selector == tag.TagName)
            {
                return true;
            }
            //id selector
            if (selector == "#" + tag.Id)
            {
                return true;
            }
            //class selector
            if (selector == "." + tag.ClassName)
            {
                return true;
            }
            return false;
        }

        public static int GetSelectorPriority(string selector)
        {
            //id selector
            if (selector.StartsWith("#"))
                return 3;
            //class selector
            if (selector.StartsWith("."))
                return 2;
            //generic selector
            if (selector.StartsWith("*"))
                return 0;

            //tag name etc.
            return 1;
        }

        //sort styles by importance
        public static int CompareTo(Style t, Style s)
        {
            int myPriority;
            int sPriority;

            myPriority = GetSelectorPriority(t.selector);
            sPriority = GetSelectorPriority(s.selector);

            return sPriority - myPriority;
        }
    }
}
