using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    public class Text : CharacterData, IText
    {
        public Text()
            : base()
        {
            nodeName = "#text";
            nodeType = NodeType.TEXT_NODE;

            if (htmlCharacters == null)
                LoadHtmlCharacters();
        }
        public Text(string data)
            : base(data)
        {
            nodeName = "#text";
            nodeType = NodeType.TEXT_NODE;

            if (htmlCharacters == null)
                LoadHtmlCharacters();
        }

        /// <summary>
        /// Breaks this Text node into two Text nodes at the specified offset, keeping both in the tree as siblings. This node then only contains all the content up to the offset point. And a new Text node, which is inserted as the next sibling of this node, contains all the content at and after the offset point.
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public Text splitText(int offset)
        {
            return null;
        }
        //raises(DOMException);

        public override string Data
        {
            get
            {
                return ParseChars(nodeValue);
            }
            set
            {
                nodeValue = value;
            }
        }

        private string ParseChars(string s)
        {
            if (s.Contains("&"))
            {
                foreach (KeyValuePair<string,char> kvp in htmlCharacters)
                {
                    string res = "&" + kvp.Key + ";";
                    if (s.Contains(res))
                    {
                        s = s.Replace(res, kvp.Value.ToString());
                    }
                }
            }
            return s;
        }

        protected static Dictionary<string, char> htmlCharacters;

        private static void LoadHtmlCharacters()
        {
            htmlCharacters = new Dictionary<string, char>();

            htmlCharacters.Add("amp", '&');
            htmlCharacters.Add("lt", '<');
            htmlCharacters.Add("gt", '>');
            htmlCharacters.Add("quot", '"');
            htmlCharacters.Add("nbsp", (char)(160));

            htmlCharacters.Add("lsquo", '\u2018');
            htmlCharacters.Add("rsquo", '\u2019');

            htmlCharacters.Add("frasl", (char)(47));
            htmlCharacters.Add("ndash", (char)(8211));
            htmlCharacters.Add("mdash", (char)(8212));
            htmlCharacters.Add("iexcl", (char)(161));
            htmlCharacters.Add("cent", (char)(162));
            htmlCharacters.Add("pound", (char)(163));
            htmlCharacters.Add("curren", (char)(164));
            htmlCharacters.Add("yen", (char)(165));
            htmlCharacters.Add("brvbar", (char)(166));
            htmlCharacters.Add("brkbar", (char)(166));
            htmlCharacters.Add("sect", (char)(167));
            htmlCharacters.Add("uml", (char)(168));
            htmlCharacters.Add("die", (char)(168));
            htmlCharacters.Add("copy", (char)(169));
            htmlCharacters.Add("ordf", (char)(170));
            htmlCharacters.Add("laquo", (char)(171));
            htmlCharacters.Add("not", (char)(172));
            htmlCharacters.Add("shy", (char)(173));
            htmlCharacters.Add("reg", (char)(174));
            htmlCharacters.Add("macr", (char)(175));
            htmlCharacters.Add("hibar", (char)(175));
            htmlCharacters.Add("deg", (char)(176));
            htmlCharacters.Add("plusmn", (char)(177));
            htmlCharacters.Add("sup2", (char)(178));
            htmlCharacters.Add("sup3", (char)(179));
            htmlCharacters.Add("acute", (char)(180));
            htmlCharacters.Add("micro", (char)(181));
            htmlCharacters.Add("para", (char)(182));
            htmlCharacters.Add("middot", (char)(183));
            htmlCharacters.Add("cedil", (char)(184));
            htmlCharacters.Add("sup1", (char)(185));
            htmlCharacters.Add("ordm", (char)(186));
            htmlCharacters.Add("raquo", (char)(187));
            htmlCharacters.Add("frac14", (char)(188));
            htmlCharacters.Add("frac12", (char)(189));
            htmlCharacters.Add("frac34", (char)(190));
            htmlCharacters.Add("iquest", (char)(191));
            htmlCharacters.Add("Agrave", (char)(192));
            htmlCharacters.Add("Aacute", (char)(193));
            htmlCharacters.Add("Acirc", (char)(194));
            htmlCharacters.Add("Atilde", (char)(195));
            htmlCharacters.Add("Auml", (char)(196));
            htmlCharacters.Add("Aring", (char)(197));
            htmlCharacters.Add("AElig", (char)(198));
            htmlCharacters.Add("Ccedil", (char)(199));
            htmlCharacters.Add("Egrave", (char)(200));
            htmlCharacters.Add("Eacute", (char)(201));
            htmlCharacters.Add("Ecirc", (char)(202));
            htmlCharacters.Add("Euml", (char)(203));
            htmlCharacters.Add("Igrave", (char)(204));
            htmlCharacters.Add("Iacute", (char)(205));
            htmlCharacters.Add("Icirc", (char)(206));
            htmlCharacters.Add("Iuml", (char)(207));
            htmlCharacters.Add("ETH", (char)(208));
            htmlCharacters.Add("Ntilde", (char)(209));
            htmlCharacters.Add("Ograve", (char)(210));
            htmlCharacters.Add("Oacute", (char)(211));
            htmlCharacters.Add("Ocirc", (char)(212));
            htmlCharacters.Add("Otilde", (char)(213));
            htmlCharacters.Add("Ouml", (char)(214));
            htmlCharacters.Add("times", (char)(215));
            htmlCharacters.Add("Oslash", (char)(216));
            htmlCharacters.Add("Ugrave", (char)(217));
            htmlCharacters.Add("Uacute", (char)(218));
            htmlCharacters.Add("Ucirc", (char)(219));
            htmlCharacters.Add("Uuml", (char)(220));
            htmlCharacters.Add("Yacute", (char)(221));
            htmlCharacters.Add("THORN", (char)(222));
            htmlCharacters.Add("szlig", (char)(223));
            htmlCharacters.Add("agrave", (char)(224));
            htmlCharacters.Add("aacute", (char)(225));
            htmlCharacters.Add("acirc", (char)(226));
            htmlCharacters.Add("atilde", (char)(227));
            htmlCharacters.Add("auml", (char)(228));
            htmlCharacters.Add("aring", (char)(229));
            htmlCharacters.Add("aelig", (char)(230));
            htmlCharacters.Add("ccedil", (char)(231));
            htmlCharacters.Add("egrave", (char)(232));
            htmlCharacters.Add("eacute", (char)(233));
            htmlCharacters.Add("ecirc", (char)(234));
            htmlCharacters.Add("euml", (char)(235));
            htmlCharacters.Add("igrave", (char)(236));
            htmlCharacters.Add("iacute", (char)(237));
            htmlCharacters.Add("icirc", (char)(238));
            htmlCharacters.Add("iuml", (char)(239));
            htmlCharacters.Add("eth", (char)(240));
            htmlCharacters.Add("ntilde", (char)(241));
            htmlCharacters.Add("ograve", (char)(242));
            htmlCharacters.Add("oacute", (char)(243));
            htmlCharacters.Add("ocirc", (char)(244));
            htmlCharacters.Add("otilde", (char)(245));
            htmlCharacters.Add("ouml", (char)(246));
            htmlCharacters.Add("divide", (char)(247));
            htmlCharacters.Add("oslash", (char)(248));
            htmlCharacters.Add("ugrave", (char)(249));
            htmlCharacters.Add("uacute", (char)(250));
            htmlCharacters.Add("ucirc", (char)(251));
            htmlCharacters.Add("uuml", (char)(252));
            htmlCharacters.Add("yacute", (char)(253));
            htmlCharacters.Add("thorn", (char)(254));
            htmlCharacters.Add("yuml", (char)(255));

            //symbols from http://de.selfhtml.org/html/referenz/zeichen.htm

            //greek letters
            htmlCharacters.Add("Alpha", (char)(913));
            htmlCharacters.Add("Beta", (char)(914));
            htmlCharacters.Add("Gamma", (char)(915));
            htmlCharacters.Add("Delta", (char)(916));
            htmlCharacters.Add("Epsilon", (char)(917));
            htmlCharacters.Add("Zeta", (char)(918));
            htmlCharacters.Add("Eta", (char)(919));
            htmlCharacters.Add("Theta", (char)(920));
            htmlCharacters.Add("Iota", (char)(921));
            htmlCharacters.Add("Kappa", (char)(922));
            htmlCharacters.Add("Lambda", (char)(923));
            htmlCharacters.Add("Mu", (char)(924));
            htmlCharacters.Add("Nu", (char)(925));
            htmlCharacters.Add("Xi", (char)(926));
            htmlCharacters.Add("Omicron", (char)(927));
            htmlCharacters.Add("Pi", (char)(928));
            htmlCharacters.Add("Rho", (char)(929));
            htmlCharacters.Add("Sigma", (char)(930));
            htmlCharacters.Add("Sigmaf", (char)(931));
            htmlCharacters.Add("Tau", (char)(932));
            htmlCharacters.Add("Upsilon", (char)(933));
            htmlCharacters.Add("Phi", (char)(934));
            htmlCharacters.Add("Chi", (char)(935));
            htmlCharacters.Add("Psi", (char)(936));
            htmlCharacters.Add("Omega", (char)(937));

            htmlCharacters.Add("alpha", (char)(945));
            htmlCharacters.Add("beta", (char)(946));
            htmlCharacters.Add("gamma", (char)(947));
            htmlCharacters.Add("delta", (char)(948));
            htmlCharacters.Add("epsilon", (char)(949));
            htmlCharacters.Add("zeta", (char)(950));
            htmlCharacters.Add("eta", (char)(951));
            htmlCharacters.Add("theta", (char)(952));
            htmlCharacters.Add("iota", (char)(953));
            htmlCharacters.Add("kappa", (char)(954));
            htmlCharacters.Add("lambda", (char)(955));
            htmlCharacters.Add("mu", (char)(956));
            htmlCharacters.Add("nu", (char)(957));
            htmlCharacters.Add("xi", (char)(958));
            htmlCharacters.Add("omicron", (char)(959));
            htmlCharacters.Add("pi", (char)(960));
            htmlCharacters.Add("rho", (char)(961));
            htmlCharacters.Add("sigma", (char)(962));
            htmlCharacters.Add("sigmaf", (char)(963));
            htmlCharacters.Add("tau", (char)(964));
            htmlCharacters.Add("upsilon", (char)(965));
            htmlCharacters.Add("phi", (char)(966));
            htmlCharacters.Add("chi", (char)(967));
            htmlCharacters.Add("psi", (char)(968));
            htmlCharacters.Add("omega", (char)(969));
            htmlCharacters.Add("thetasym", (char)(977));
            htmlCharacters.Add("upsih", (char)(978));
            htmlCharacters.Add("piv", (char)(982));

            //math symbols
            htmlCharacters.Add("forall", (char)(8704));
            htmlCharacters.Add("part", (char)(8706));
            htmlCharacters.Add("exist", (char)(8707));
            htmlCharacters.Add("empty", (char)(8709));
            htmlCharacters.Add("nabla", (char)(8711));
            htmlCharacters.Add("isin", (char)(8712));
            htmlCharacters.Add("notin", (char)(8713));
            htmlCharacters.Add("ni", (char)(8715));
            htmlCharacters.Add("prod", (char)(8719));
            htmlCharacters.Add("sum", (char)(8721));
            htmlCharacters.Add("minus", (char)(8722));
            htmlCharacters.Add("lowast", (char)(8727));
            htmlCharacters.Add("radic", (char)(8730));
            htmlCharacters.Add("prop", (char)(8733));
            htmlCharacters.Add("infin", (char)(8734));
            htmlCharacters.Add("ang", (char)(8736));
            htmlCharacters.Add("and", (char)(8743));
            htmlCharacters.Add("or", (char)(8744));
            htmlCharacters.Add("cap", (char)(8745));
            htmlCharacters.Add("cup", (char)(8746));
            htmlCharacters.Add("int", (char)(8747));
            htmlCharacters.Add("there4", (char)(8756));
            htmlCharacters.Add("sim", (char)(8764));
            htmlCharacters.Add("cong", (char)(8773));
            htmlCharacters.Add("asymp", (char)(8776));
            htmlCharacters.Add("ne", (char)(8800));
            htmlCharacters.Add("equiv", (char)(8801));
            htmlCharacters.Add("le", (char)(8804));
            htmlCharacters.Add("ge", (char)(8805));
            htmlCharacters.Add("sub", (char)(8834));
            htmlCharacters.Add("sup", (char)(8835));
            htmlCharacters.Add("nsub", (char)(8836));
            htmlCharacters.Add("sube", (char)(8838));
            htmlCharacters.Add("supe", (char)(8839));
            htmlCharacters.Add("oplus", (char)(8853));
            htmlCharacters.Add("otimes", (char)(8855));
            htmlCharacters.Add("perp", (char)(8869));
            htmlCharacters.Add("sdot", (char)(8901));
            htmlCharacters.Add("loz", (char)(9674));

            //technical symbols
            htmlCharacters.Add("lceil", (char)(8968));
            htmlCharacters.Add("rceil", (char)(8969));
            htmlCharacters.Add("lfloor", (char)(8970));
            htmlCharacters.Add("rfloor", (char)(8971));
            htmlCharacters.Add("lang", (char)(9001));
            htmlCharacters.Add("rang", (char)(9002));

            //arrow symbols
            htmlCharacters.Add("larr", (char)(8592));
            htmlCharacters.Add("uarr", (char)(8593));
            htmlCharacters.Add("rarr", (char)(8594));
            htmlCharacters.Add("darr", (char)(8595));
            htmlCharacters.Add("harr", (char)(8596));
            htmlCharacters.Add("crarr", (char)(8629));
            htmlCharacters.Add("lArr", (char)(8656));
            htmlCharacters.Add("uArr", (char)(8657));
            htmlCharacters.Add("rArr", (char)(8658));
            htmlCharacters.Add("dArr", (char)(8659));
            htmlCharacters.Add("hArr", (char)(8960));

            //divers symbols
            htmlCharacters.Add("bull", (char)(8226));
            htmlCharacters.Add("prime", (char)(8242));
            htmlCharacters.Add("Prime", (char)(8243));
            htmlCharacters.Add("oline", (char)(8254));
            htmlCharacters.Add("weierp", (char)(8472));
            htmlCharacters.Add("image", (char)(8465));
            htmlCharacters.Add("real", (char)(8476));
            htmlCharacters.Add("trade", (char)(8482));
            htmlCharacters.Add("euro", (char)(8364));
            htmlCharacters.Add("alefsym", (char)(8501));
            htmlCharacters.Add("spades", (char)(9824));
            htmlCharacters.Add("clubs", (char)(9827));
            htmlCharacters.Add("hearts", (char)(9829));
            htmlCharacters.Add("diams", (char)(9830));

            //ext lat symbols
            htmlCharacters.Add("OElig", (char)(338));
            htmlCharacters.Add("oelig", (char)(339));
            htmlCharacters.Add("Scaron", (char)(352));
            htmlCharacters.Add("scaron", (char)(353));
            htmlCharacters.Add("fnof", (char)(402));

            //interpunction
            htmlCharacters.Add("ensp", (char)(8194));
            htmlCharacters.Add("emsp", (char)(8195));
            htmlCharacters.Add("thinsp", (char)(8201));
            htmlCharacters.Add("zwnj", (char)(8204));
            htmlCharacters.Add("zwj", (char)(8205));
            htmlCharacters.Add("lrm", (char)(8206));
            htmlCharacters.Add("rlm", (char)(8207));

            htmlCharacters.Add("sbquo", (char)(8218));
            htmlCharacters.Add("ldquo", (char)(8220));
            htmlCharacters.Add("rdquo", (char)(8221));
            htmlCharacters.Add("bdquo", (char)(8222));
            htmlCharacters.Add("dagger", (char)(8224));
            htmlCharacters.Add("Dagger", (char)(8225));
            htmlCharacters.Add("hellip", (char)(8230));
            htmlCharacters.Add("permil", (char)(8240));
            htmlCharacters.Add("lsaquo", (char)(8249));
            htmlCharacters.Add("rsaquo", (char)(8250));

            //diacrit symb
            htmlCharacters.Add("circ", (char)(710));
            htmlCharacters.Add("tilde", (char)(732));
        }
    }
}
