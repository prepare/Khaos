using System;

namespace DOMCore
{
    /// <summary>
    /// Attribute holds one attribute, as is normally stored in an
    /// HTML or XML file. This includes a name, value and delimiter.
    /// </summary>
    public class Attribute : ICloneable
    {
        /// <summary>
        /// The name of this attribute
        /// </summary>
        private string name = "";

        /// <summary>
        /// The value of this attribute
        /// </summary>
        private string value = "";

        /// <summary>
        /// The delimiter for the value of this attribute(i.newNode. " or ').
        /// </summary>
        private char delim;

        /// <summary>
        /// Construct a new Attribute.  The name, separator, and value
        /// properties can be specified here.
        /// </summary>
        /// <param name="name">The name of this attribute.</param>
        /// <param name="value">The value of this attribute.</param>
        /// <param name="separator">The delimiter character for the value.
        /// </param>
        public Attribute(string name, string value, char delim)
        {
            this.name = name;
            this.value = value;
            this.delim = delim;
        }


        /// <summary>
        /// The default constructor.  Construct a blank attribute.
        /// </summary>
        public Attribute()
            : this("", "", (char)0)
        {
        }

        /// <summary>
        /// Construct an attribute without a delimiter.
        /// </summary>
        /// <param name="name">The name of this attribute.</param>
        /// <param name="value">The value of this attribute.</param>
        public Attribute(String name, String value)
            : this(name, value,
                 (char)0)
        {
        }

        /// <summary>
        /// The delimiter for this attribute.
        /// </summary>
        public char Delim
        {
            get
            {
                return delim;
            }

            set
            {
                delim = value;
            }
        }

        /// <summary>
        /// The name for this attribute.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        /// <summary>
        /// The value for this attribute.
        /// </summary>
        public string Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        #region ICloneable Members
        public virtual object Clone()
        {
            return new Attribute(name, value, delim);
        }
        #endregion
    }
}