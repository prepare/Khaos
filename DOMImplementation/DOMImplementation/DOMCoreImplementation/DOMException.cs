using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    /// <summary>
    /// An integer indicating the type of error generated.
    /// </summary>
    public enum ExceptionCode
    {
        // NodeTypeconst
        INDEX_SIZE_ERR = 1,             //If index or size is negative, or greater than the allowed value
        DOMSTRING_SIZE_ERR = 2,         //If the specified range of text does not fit into a DOMString
        HIERARCHY_REQUEST_ERR = 3,      //If any node is inserted somewhere it doesn't belong
        WRONG_DOCUMENT_ERR = 4,         //If a node is used in a different document than the one that created it (that doesn't support it)
        INVALID_CHARACTER_ERR = 5,      //If an invalid character is specified, such as in a name.
        NO_DATA_ALLOWED_ERR = 6,        //If data is specified for a node which does not support data
        NO_MODIFICATION_ALLOWED_ERR = 7,//If an attempt is made to modify an object where modifications are not allowed
        NOT_FOUND_ERR = 8,              //If an attempt was made to reference a node in a context where it does not exist
        NOT_SUPPORTED_ERR = 9,          //If the implementation does not support the type of object requested
        INUSE_ATTRIBUTE_ERR = 10        //If an attempt is made to add an attribute that is already inuse elsewhere
    }

    public class DOMException : Exception
    {
        /// <summary>
        /// An integer indicating the type of error generated.
        /// </summary>
        public ExceptionCode code;

        public DOMException(ExceptionCode code)
        {
            this.code = code;
        }

        public DOMException(ExceptionCode code, string message) : base(message)
        {
            this.code = code;
        }
    }
}
