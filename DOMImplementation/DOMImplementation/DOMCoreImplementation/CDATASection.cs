using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    public class CDATASection : Text, ICDATASection
    {
        public CDATASection() : base() 
        {
            nodeName = "#cdata-section";
            nodeType = NodeType.CDATA_SECTION_NODE;
        }
        public CDATASection(string data)
            : base(data)
        {
            nodeName = "#cdata-section";
            nodeType = NodeType.CDATA_SECTION_NODE;
        }
    }
}
