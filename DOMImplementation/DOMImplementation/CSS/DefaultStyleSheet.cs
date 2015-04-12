using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CascadingStyleSheets
{
    public class DefaultStyleSheet
    {
        private static string page2 = @"
            /* rendered CSS1-addressable elements and all applicable non-inherited
            properties set to initial values and default display types */

            A, ABBR, ACRONYM, ADDRESS, BDO, BLOCKQUOTE, BODY, BUTTON, CITE, CODE, DD, DEL,
            DFN, DIV, DL, DT, EM, FIELDSET, FORM, H1, H2, H3, H4, H5, H6, HTML, IFRAME, IMG, INS,
            KBD, LABEL, LI, OBJECT, OL, P, Q, SAMP, SPAN, STRONG, SUB, SUP, UL, VAR, 
            APPLET, B, BIG, CENTER, DIR, FONT, HR, I, MENU, PRE, S, SMALL, STRIKE, TT, U	{
	            background: transparent;
	            width: auto;
	            height: auto;
	            text-decoration: none;
	            margin: 0;
	            padding: 0;
	            border: 0;
	            float: none;
	            clear: none;
	            vertical-align: baseline;
	            list-style-image: none;
	            list-style-type: disc;
	            list-style-position: outside;
	            }	 

            ADDRESS, BLOCKQUOTE, BODY, DD, DIV, DL, DT, FIELDSET, FORM, H1, H2, H3, H4, H5,
            H6, OL, P, UL, CENTER, DIR, HR, MENU, PRE	{
	            display: block;
	            }

            A, ABBR, ACRONYM, APPLET, BDO, BUTTON, CITE, CODE, DEL, DFN, EM, IFRAME, IMG,
            INS, KBD, LABEL, OBJECT, Q,
            SAMP, SPAN, STRONG, SUB, SUP, VAR, B, BIG, FONT, I, S, SMALL, STRIKE, TT, U	{

	            display: inline;
	            }

            LI	{
	            display: list-item;
	            }

            /* Begin tree of inherited properties and cascades. */

            /* Describes the default type, color, and link decoration specs of
            Mosaic-derivative browsers to the extent and degree of granularity that users
            may typically override. Uncomment for factory settings.*/

            HTML	{
	            font-family: 'Times New Roman', Times;
	            font-size: medium; 
	            color: black;
	            background-color: #BFBFBF;
	            }

            PRE, TT, CODE, KBD, SAMP	{
	            font-family: 'Courier New', Courier;
	            }

            A:link, A:visited, A:active	{
	            text-decoration: underline;
	            }

            A:link	{
	            color: #0000FF;
	            }
            	
            A:visited	{
	            color: #7F007F;
	            }

            A:active	{
	            color: #0000FF;
	            }

            HTML	{
	            line-height: 1.12;
	            word-spacing: normal;
	            letter-spacing: normal;
	            text-transform: none;
	            text-align: left;
	            text-indent: 0;
	            white-space: normal;
	            }

            BODY	{
	            padding: 8px;
	            }

            H1	{
	            font-size: 2em;
	            margin: .67em 0; 
	            }

            H2	{ 
	            font-size: 1.5em;
	            margin: .75em 0; 
	            }

            H3	{ 
	            font-size: 1.17em;
	            margin: .83em 0; 
	            }

            H4, P, BLOCKQUOTE, FIELDSET, FORM, UL, OL, DL, DIR, MENU	{ 
	            margin: 1.12em 0; 
	            }

            H5	{
	            font-size: .83em; /* varies with pixels-per-em at document root */
	            margin: 1.5em 0; 
	            }

            H6	{
	            font-size: .6em; /* varies with pixels-per-em at document root */
	            margin: 1.67em 0; 
	            }

            H1, H2, H3, H4, H5, H6, B, STRONG	{ 
	            font-weight: bolder;
	            }
            	
            BLOCKQUOTE	{ 
	            margin-left: 40px;
	            margin-right: 40px;
	            }

            I, CITE, EM, VAR, ADDRESS	{ 
	            font-style: italic;
	            }

            PRE, TT, CODE, KBD, SAMP	{ 
	            font-family: monospace;
	            }

            PRE	{
	            white-space: pre;
	            }

            BIG	{ 
	            font-size: larger;
	            }
            	
            SMALL, SUB, SUP	{
	            font-size: smaller;
	            }

            SUB	{
	            vertical-align: sub;
	            }

            SUP	{
	            vertical-align: super;
	            }

            S, STRIKE, DEL	{
	            text-decoration: line-through;
	            }

            HR	{
	            border: 1px inset; /* questionable */
	            }

            OL, UL, DIR, MENU, DD	{
	            padding-left: 40px; 
	            }
            	
            OL LI	{
	            list-style-type: decimal;
	            }
            	
            UL LI	{
	            list-style-type: disc;
	            }

            UL UL, UL OL, UL MENU, UL DIR, MENU UL, MENU OL, MENU MENU, MENU DIR, DIR UL,
            DIR OL, DIR MENU, DIR DIR, OL UL, OL OL, OL MENU, OL DIR	{
	            margin-top: 0;
	            margin-bottom: 0;
	            }

            OL UL, UL UL, MENU UL, DIR UL, OL MENU, UL MENU, MENU MENU, DIR MENU, OL DIR, UL
            DIR, MENU DIR, DIR DIR 	{
               list-style-type: circle;
	            }

            U, INS	{
	            text-decoration: underline;
	            }

            CENTER	{
	            text-align: center;
	            }

            CAPTION, COL, COLGROUP, LEGEND, TABLE, TBODY, TD, TFOOT, TH, THEAD, TR	{
	            background: transparent;
	            text-decoration: none;
	            margin: 1px;
	            padding: 1px;
	            border: none;
	            float: none;
	            clear: none;
	            }

            TABLE, TBODY, TFOOT, THEAD, TR	{
	            display: block;
	            background-position: top left;
	            width: auto;
	            height: auto;
	            }

            CAPTION, LEGEND, TD, TH	{ 
	            display: inline;
	            vertical-align: baseline;
	            font-size: 1em;
	            line-height: 1.33em;
	            color: black;
	            word-spacing: normal;
	            letter-spacing: normal;
	            text-transform: none;
	            text-align: left;
	            text-indent: 0;
	            white-space: normal;
	            }

            TH	{
	            font-weight: bolder;
	            text-align: center;
	            }

            CAPTION	{
	            text-align: center;
	            }

            br {
                    line-break: normal; 
                    width: 1px;
               }
            
            html, address,
            blockquote,
            body, dd, div,
            dl, dt, fieldset, form,
            frame, frameset,
            h1, h2, h3, h4,
            h5, h6, noframes,
            ol, p, ul, center,
            dir, hr, menu, pre   { display: block }
            li              { display: list-item }
            head            { display: none }
            table           { display: table }
            tr              { display: table-row }
            thead           { display: table-header-group }
            tbody           { display: table-row-group }
            tfoot           { display: table-footer-group }
            col             { display: table-column }
            colgroup        { display: table-column-group }
            td, th          { display: table-cell }
            caption         { display: table-caption }
            th              { font-weight: bolder; text-align: center }
            caption         { text-align: center }
            body            { margin: 8px }
            h1              { font-size: 2em; margin: .67em 0 }
            h2              { font-size: 1.5em; margin: .75em 0 }
            h3              { font-size: 1.17em; margin: .83em 0 }
            h4, p,
            blockquote, ul,
            fieldset, form,
            ol, dl, dir,
            menu            { margin: 1.12em 0 }
            h5              { font-size: .83em; margin: 1.5em 0 }
            h6              { font-size: .75em; margin: 1.67em 0 }
            h1, h2, h3, h4,
            h5, h6, b,
            strong          { font-weight: bolder }
            blockquote      { margin-left: 40px; margin-right: 40px }
            i, cite, em,
            var, address    { font-style: italic }
            pre, tt, code,
            kbd, samp       { font-family: monospace }
            pre             { white-space: pre }
            button, textarea,
            input, select   { display: inline-block }
            big             { font-size: 1.17em }
            small, sub, sup { font-size: .83em }
            sub             { vertical-align: sub }
            sup             { vertical-align: super }
            table           { border-spacing: 2px; }
            thead, tbody,
            tfoot           { vertical-align: middle }
            td, th, tr      { vertical-align: inherit }
            s, strike, del  { text-decoration: line-through }
            hr              { border: 1px inset }
            ol, ul, dir,
            menu, dd        { margin-left: 40px }
            ol              { list-style-type: decimal }
            ol ul, ul ol,
            ul ul, ol ol    { margin-top: 0; margin-bottom: 0 }
            u, ins          { text-decoration: underline }
            center          { text-align: center }
            :link, :visited { text-decoration: underline }
            :focus          { outline: thin dotted invert }

            @media print {
              h1            { page-break-before: always }
              h1, h2, h3,
              h4, h5, h6    { page-break-after: avoid }
              ul, ol, dl    { page-break-before: avoid }
            }";
        public static string styletext
        {
            get
            {
                return page2;
            }
        }

    }
}
