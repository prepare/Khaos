using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    public interface IProcessingInstruction : INode
    {
        /// <summary>
        /// The target of this processing instruction. XML defines this as being the first token following the markup that begins the processing instruction.
        /// </summary>
        string Target
        {
            get;
        }

        /// <summary>
        /// The content of this processing instruction. This is from the first non white space character after the target to the character immediately preceding the ?>.
        /// </summary>
        string Data
        {
            get;
            set;
        }
                                      // raises(DOMException) on setting
    }
}
