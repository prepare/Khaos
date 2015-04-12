using System;
using System.Collections;
using System.Collections.Specialized;

namespace DOMCore
{

    public enum TagCategories
    {
        TagStart,
        InlineTag,
        TagEnd,
        Special
    }

    public enum TagTypes
    {
        Element,
        Unkown,
        Comment = 6,
        Doctype = 7,
        Error = 8,
        EndOfFile = 9
    }

    public class TagInfo
    {
        private string name = "";
        public string Value = "";
        private TagTypes tagType;
        private TagCategories tagCategory;

        private NameValueCollection attributes = new NameValueCollection();

        public TagInfo()
        {
        }

        public TagInfo(string tagName)
        {
            Name = tagName;
        }

        public TagTypes TagType
        {
            get
            {
                return tagType;
            }
            set
            {
                tagType = value;
            }
        }

        public TagCategories TagCategory
        {
            get
            {
                return tagCategory;
            }
            set
            {
                tagCategory = value;
            }
        }

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

        public NameValueCollection Attributes
        {
            get
            {
                return attributes;
            }
            set
            {
                attributes = value;
            }
        }

        public bool IsParentTag
        {
            get
            {
                return !(TagCategory == TagCategories.TagEnd || TagCategory == TagCategories.InlineTag);
            }
        }
    }

    /// <summary>
    /// The TagInfo class is used to store list of
    /// Attribute classes.
    /// This source code may be used freely under the
    /// Limited GNU Public License(LGPL).
    ///
    /// Written by Jeff Heaton (http://www.jeffheaton.com)
    /// </summary>
    ///
    /*public class TagInfo : Attribute
    {
        /// <summary>
        /// An internally used Vector.  This vector contains
        /// the entire list of attributes.
        /// </summary>
        protected ArrayList list;
        /// <summary>
        /// Make an exact copy of this object using the cloneable
        /// interface.
        /// </summary>
        /// <returns>A new object that is a clone of the specified
        /// object.</returns>
        public override Object Clone()
        {
            TagInfo rtn = new TagInfo();

            for (int i = 0; i < list.Count; i++)
                rtn.Add((Attribute)this[i].Clone());

            return rtn;
        }

        /// <summary>
        /// Create a new, empty, attribute list.
        /// </summary>
        public TagInfo()
            : base("", "")
        {
            list = new ArrayList();
        }


        /// <summary>
        /// Add the specified attribute to the list of attributes.
        /// </summary>
        /// <param name="a">An attribute to add to this
        /// TagInfo.</paramv
        public void Add(Attribute a)
        {
            list.Add(a);
        }


        /// <summary>
        /// Clear all attributes from this TagInfo and return
        /// it to a empty state.
        /// </summary>
        public void Clear()
        {
            list.Clear();
        }

        /// <summary>
        /// Returns true of this TagInfo is empty, with no
        /// attributes.
        /// </summary>
        /// <returns>True if this TagInfo is empty, false
        /// otherwise.</returns>
        public bool IsEmpty()
        {
            return (list.Count <= 0);
        }

        /// <summary>
        /// If there is already an attribute with the specified name,
        /// it will have its value changed to match the specified
        /// value. If there is no Attribute with the specified name,
        /// one will be created. This method is case-insensitive.
        /// </summary>
        /// <param name="name">The name of the Attribute to edit or
        /// create.  Case-insensitive.</param>
        /// <param name="value">The value to be held in this
        /// attribute.</param>
        public void Set(string name, string value)
        {
            if (name == null)
                return;
            if (value == null)
                value = "";

            Attribute a = this[name];

            if (a == null)
            {
                a = new Attribute(name, value);
                Add(a);
            }

            else
                a.Value = value;
        }

        /// <summary>
        /// How many attributes are in this TagInfo?
        /// </summary>
        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        /// <summary>
        /// A list of the attributes in this TagInfo
        /// </summary>
        public ArrayList List
        {
            get
            {
                return list;
            }
        }

        /// <summary>
        /// Access the individual attributes
        /// </summary>
        public Attribute this[int index]
        {
            get
            {
                if (index < list.Count)
                    return (Attribute)list[index];
                else
                    return null;
            }
        }

        /// <summary>
        /// Access the individual attributes by name.
        /// </summary>
        public Attribute this[string index]
        {
            get
            {
                int i = 0;

                while (this[i] != null)
                {
                    if (this[i].Name.ToLower().Equals((index.ToLower())))
                        return this[i];
                    i++;
                }

                return null;

            }
        }

        public bool IsClosingTag
        {
            get
            {
                return Name.StartsWith("/");
            }
        }

        public bool IsParentTag
        {
            get
            {
                return !(IsClosingTag || Name.EndsWith("/"));
            }
        }
    }*/
}
