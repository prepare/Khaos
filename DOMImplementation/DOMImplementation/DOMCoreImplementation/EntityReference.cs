using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    public class EntityReference : Node, IEntityReference
    {
        public EntityReference()
        {
            nodeType = NodeType.ENTITY_REFERENCE_NODE;
        }
    }
}
