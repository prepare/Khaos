using System;
using System.Collections.Generic;
using System.Text;
using CSS;
using CascadingStyleSheets;

namespace DOMCore
{
    public interface IHTMLElement : IElement
    {
        /// <summary>
        /// The element'coreRule identifier. See the id attribute definition  in HTML 4.0.
        /// </summary>
        string Id
        {
            get;
            set;
        }

        /// <summary>
        /// The element'coreRule advisory title. See the title attribute definition  in HTML 4.0.
        /// </summary>
        string Title
        {
            get;
            set;
        }
        
        /// <summary>
        /// Language code defined in RFC 1766. See the lang attribute definition  in HTML 4.0.
        /// </summary>
        string Lang
        {
            get;
            set;
        }

        /// <summary>
        /// Specifies the base direction of directionally neutral text and the directionality of tables. See the dir attribute definition  in HTML 4.0.
        /// </summary>
        string Dir
        {
            get;
            set;
        }

        /// <summary>
        /// The class attribute of the element. This attribute has been renamed due to conflicts with the "class" keyword exposed by many languages. See the class attribute definition  in HTML 4.0.
        /// </summary>
        string ClassName
        {
            get;
            set;
        }

        Style CSSStyle
        {
            get;
        }

    }
}
