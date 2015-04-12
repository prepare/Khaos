using System;
using System.Collections.Generic;
using System.Text;
using DOMCore.DOMParserRules;
using PhoenixOI;

namespace DOMCore
{
    public class DOMParser
    {
        //public List<TagInfo> documentStream = new List<TagInfo>();
        public DocumentStream documentStream;
        public List<string> errormsgs = new List<string>();

        public Dictionary<string, DOMParserRule> rules = new Dictionary<string, DOMParserRule>();

        public HTMLDocument doc;

        private Stack<Node> parentlist = new Stack<Node>();

        private Request request;

        public Request Request
        {
            get { return request; }
        }

        public DOMParser(List<TagInfo> lst, Request request)
        {
            this.request = request;
            documentStream = new DocumentStream(lst);
            InitRules();
        }

        /// <summary>
        /// Add all supported parsing rules to parser.
        /// </summary>
        public void InitRules()
        {
            rules.Add(DPRGeneric_Tag.Name, new DPRGeneric_Tag(this));
            rules.Add(DPRText.Name, new DPRText(this));
            rules.Add(DPRA_Tag.Name, new DPRA_Tag(this));
            rules.Add(DPRBody_Tag.Name, new DPRBody_Tag(this));
            rules.Add(DPRBr_Tag.Name, new DPRBr_Tag(this));
            rules.Add(DPRDiv_Tag.Name, new DPRDiv_Tag(this));
            rules.Add(DPRDl_Tag.Name, new DPRDl_Tag(this));
            rules.Add(DPRFont_Tag.Name, new DPRFont_Tag(this));
            rules.Add(DPRForm_Tag.Name, new DPRForm_Tag(this));
            rules.Add(DPRH1_Tag.Name, new DPRH1_Tag(this));
            rules.Add(DPRH2_Tag.Name, new DPRH2_Tag(this));
            rules.Add(DPRH3_Tag.Name, new DPRH3_Tag(this));
            rules.Add(DPRH4_Tag.Name, new DPRH4_Tag(this));
            rules.Add(DPRH5_Tag.Name, new DPRH5_Tag(this));
            rules.Add(DPRH6_Tag.Name, new DPRH6_Tag(this));
            rules.Add(DPRHead_Tag.Name, new DPRHead_Tag(this));
            rules.Add(DPRHr_Tag.Name, new DPRHr_Tag(this));
            rules.Add(DPRHtml_Tag.Name, new DPRHtml_Tag(this));
            rules.Add(DPRImg_Tag.Name, new DPRImg_Tag(this));
            rules.Add(DPRInput_Tag.Name, new DPRInput_Tag(this));
            rules.Add(DPRLabel_Tag.Name, new DPRLabel_Tag(this));
            rules.Add(DPRLi_Tag.Name, new DPRLi_Tag(this));
            rules.Add(DPRLink_Tag.Name, new DPRLink_Tag(this));
            rules.Add(DPRMeta_Tag.Name, new DPRMeta_Tag(this));
            rules.Add(DPROl_Tag.Name, new DPROl_Tag(this));
            rules.Add(DPROptgroup_Tag.Name, new DPROptgroup_Tag(this));
            rules.Add(DPROption_Tag.Name, new DPROption_Tag(this));
            rules.Add(DPRP_Tag.Name, new DPRP_Tag(this));
            rules.Add(DPRStyle_Tag.Name, new DPRStyle_Tag(this));
            rules.Add(DPRTable_Tag.Name, new DPRTable_Tag(this));
            rules.Add(DPRTd_Tag.Name, new DPRTd_Tag(this));
            rules.Add(DPRTextArea_Tag.Name, new DPRTextArea_Tag(this));
            rules.Add(DPRTitle_Tag.Name, new DPRTitle_Tag(this));
            rules.Add(DPRTr_Tag.Name, new DPRTr_Tag(this));
        }

        /// <summary>
        /// Checks if first object in documentStream is a tag.
        /// </summary>
        /// <returns>True if first object in documentStream is a tag. False otherwise.</returns>
        public bool IsTag()
        {
            return documentStream.PeekTag().Name != "#text";
        }

        /// <summary>
        /// Checks if first object in documentStream is a tag with given name.
        /// </summary>
        /// <param name="tagname">The name the first tag needs to match.</param>
        /// <returns>True if first object in document stream is an tag with given name. False otherwise.</returns>
        public bool IsTag(string tagname)
        {
            if (IsTag())
                return (documentStream.PeekTag().Name == tagname);
            else
                return false;
        }

        /// <summary>
        /// If first object in documentStream is a string.
        /// </summary>
        /// <returns>True if first object in documentStream is a string. False otherwise.</returns>
        public bool IsText()
        {
            return documentStream.PeekTag().Name == "#text";
        }

        /// <summary>
        /// Try to match given tagname to current tag in document stream.
        /// </summary>
        /// <param name="tagname">The tag to try matching the object to.</param>
        /// <param name="bParentTag">Whether the tag can have children.</param>
        /// <returns>True if the object was a tag of the given type. False otherwise.</returns>
        public TagInfo MatchTag(string tagname)
        {
            //if there is an object in stream and it is a tag of the given name
            if (!documentStream.EOF() && tagname.EndsWith("/"))
            {
                string tname = tagname.Replace("/", "");
                if (IsTag(tname))
                {
                    TagInfo ti = documentStream.GetTag();
                    ti.TagCategory = TagCategories.InlineTag;
                    return ti;
                }
            }
            if (!documentStream.EOF() && IsTag(tagname))
            {
                return documentStream.GetTag();
            }
            return null;
        }

        public TagInfo MatchTag()
        {
            //if there is an object in stream and it is a tag
            if (!documentStream.EOF() && IsTag())
            {
                return documentStream.GetTag();
            }
            return null;
        }

        //Regular characters in the document character encoding, along with character entities denoted by the ampersand character.
        public string MatchPlain_Text()
        {
            //if there is an object in stream and it is a string
            if (!documentStream.EOF() && IsText())
            {
                return documentStream.GetTag().Value;
            }
            else
                return null;
        }

        //Text is interpreted exactly as specified; no character entities or style tags are recognized.
        public string MatchLiteral_Text()
        {
            if (!documentStream.EOF() && IsText())
            {
                return documentStream.GetTag().Value;
            }
            else
                return null;
        }

        public void HandleTag(Node newNode, bool bClosingTag, bool bParentTag)
        {
            if (newNode is HTMLElement)
            {
                (newNode as HTMLElement).Request = request;
            }

            if (!bClosingTag && newNode != null)
            {
                AppendNode(newNode);
            }
            //if element can have children add it to stack of parent nodes
            if (bParentTag)
            {
                parentlist.Push(newNode);
            }
            //if tag is a closing tag
            if (bClosingTag)
            {
                //remove top parent from parent stack
                if (parentlist.Count > 0)
                {
                    Node p = parentlist.Pop();
                }
            }
        }

        /// <summary>
        /// Insert an node into the HTMLDocument.
        /// </summary>
        /// <param name="al">The Node to insert.</param>
        public void AppendNode(Node n)
        {
            //insert element into document
            if (parentlist.Count > 0)
            {
                parentlist.Peek().appendChild(n);
            }
        }

        /// <summary>
        /// Remove HTML comments from document stream to avoid problems with tag matching.
        /// </summary>
        /// <param name="documentStream">DocumentStream to remove comments from.</param>
        public void RemoveComments(DocumentStream documentStream)
        {
            while (!documentStream.EOF())
            {
                if (documentStream.PeekTag().Name.StartsWith("!"))
                    documentStream.RemoveTag();
                else
                    documentStream.Advance();
            }
            documentStream.Seek(0);
        }

        public HTMLDocument ParseDom()
        {
            doc = new HTMLDocument();
            doc.cssMgr = new CSS.CSSManager(doc, request);
            parentlist.Push(doc);

            //remove comments from stream
            RemoveComments(documentStream);

            while (!documentStream.EOF())
            {
                TagInfo ti = documentStream.PeekTag();
                if (Rule("<" + ti.Name + ">") > 0)
                {
                    //System.Windows.Forms.MessageBox.Show("Specific:" + ti.Name);
                }
                else if (Rule("<" + ti.Name + "/>") > 0)
                {
                    //System.Windows.Forms.MessageBox.Show("Specific:" + ti.Name);
                }
                else if (Rule("generic_tag") > 0)
                {
                    //System.Windows.Forms.MessageBox.Show("General:" + ti.Name);
                }
                else
                {
                    Rule("text");
                }
            }
            return doc;
        }

        /// <summary>
        /// Return number of tags/objects matched with given rule.
        /// </summary>
        /// <param name="name">Name of the rule to use for matching.</param>
        /// <returns>Number of tags/objects matched with given rule.</returns>
        public int Rule(string name)
        {
            if (rules.ContainsKey(name))
                return rules[name].ExecuteRule();
            else
                return 0;
        }
    }
}
