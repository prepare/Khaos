using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    public class ProcessingInstruction : Node, IProcessingInstruction
    {
        public string data;

        public ProcessingInstruction()
        {
            nodeName = "";
            data = "";
            nodeType = NodeType.PROCESSING_INSTRUCTION_NODE;
        }
        
        public ProcessingInstruction(string target, string data)
        {
            nodeName = target;
            this.data = data;
            nodeType = NodeType.PROCESSING_INSTRUCTION_NODE;
        }

        /// <summary>
        /// The target of this processing instruction. XML defines this as being the first token following the markup that begins the processing instruction.
        /// </summary>
        public string Target
        {
            get
            {
                return NodeName;
            }
        }

        /// <summary>
        /// The content of this processing instruction. This is from the first non white space character after the target to the character immediately preceding the ?>.
        /// </summary>
        public string Data
        {
            get
            {
                return data;
            }
            set
            {
            }
        }
        // raises(DOMException) on setting
    }
}
