
namespace DOMCore
{

    /**
     * The document title. See the TITLE element definition in Html 4.01.
     * <p>See also the <a href='http://www.w3.org/TR/2003/REC-DOM-Level-2-Html-20030109'>Document Object Model (DOM) Level 2 Html Specification</a>.
     */
    public class HTMLTitleElement : HTMLElement, IHTMLTitleElement
    {
        public HTMLTitleElement(string tag, IDocument doc)
            : base(tag, doc)
        { }

        public HTMLTitleElement(TagInfo tag, IDocument doc)
            : base(tag, doc)
        { }

        /**
         * The specified title as a string. 
         */
        public string Text 
        { 
            get 
            { 
                if (hasChildNodes() && FirstChild.NodeType == NodeType.TEXT_NODE)
                    return FirstChild.NodeValue;
                return "";
            } 
            set
            {
                Text t = ownerDocument.createTextNode(value);
                insertBefore(t, FirstChild);
            } 
        }


    }

}