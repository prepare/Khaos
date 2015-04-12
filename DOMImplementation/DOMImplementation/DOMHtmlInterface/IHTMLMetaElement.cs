﻿using System;
namespace DOMCore
{
    public interface IHTMLMetaElement : IHTMLElement
    {
        /**
         * Associated information. See the content attribute definition in Html 
         * 4.01.
         */
        string Content { get; set; }

        /**
         * HTTP response header name [<a href='http://www.ietf.org/rfc/rfc2616.txt'>IETF RFC 2616</a>]. See the http-equiv attribute definition in 
         * Html 4.01.
         */
        string HttpEquiv { get; set; }

        /**
         * Meta information name. See the name attribute definition in Html 4.01.
         */
        string Name { get; set; }

        /**
         * Select form of content. See the scheme attribute definition in Html 
         * 4.01.
         */
        string Scheme { get; set; }
    }
}
