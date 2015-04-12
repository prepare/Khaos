using System;
using System.Collections.Generic;
using System.Text;

namespace DOMCore
{
    public class DocumentStream
    {
        public List<TagInfo> stream;
        public int position = 0;

        public DocumentStream()
        {
            stream = new List<TagInfo>();
        }

        public DocumentStream(List<TagInfo> lst)
        {
            stream = lst;
        }

        public int Length
        {
            get
            {
                return stream.Count;
            }
        }

        /// <summary>
        /// If document stream is empty.
        /// </summary>
        /// <returns>True if document stream is empty. False otherwise.</returns>
        public bool EOF()
        {
            return !(position < Length);
        }

        /// <summary>
        /// Retrieve tag at current position in document stream.
        /// </summary>
        /// <returns>Returns the tag at current position in document stream.</returns>
        public TagInfo PeekTag()
        {
            if (!EOF())
                return stream[position];

            return null;
        }

        /// <summary>
        /// Retrieve tag at current position in document stream and advance position.
        /// </summary>
        /// <returns>Returns the tag at current position in document stream.</returns>
        public TagInfo GetTag()
        {
            if (!EOF())
                return stream[position++];

            return null;
        }

        /// <summary>
        /// Remove tag at current position in document stream.
        /// </summary>
        /// <returns>Returns the tag removed from current position in document stream.</returns>
        public TagInfo RemoveTag()
        {
            if (!EOF())
            {
                TagInfo ti = stream[position];
                stream.RemoveAt(position);
                return ti;
            }

            return null;
        }

        public void InsertTag(TagInfo ti)
        {
            stream.Insert(position++, ti);
        }

        /// <summary>
        /// Advance to next object in document stream.
        /// </summary>
        public void Advance()
        {
            if (!EOF())
                position++;
        }

        public void Seek(int newPos)
        {
            if (newPos < Length)
                position = newPos;
        }
    }
}
