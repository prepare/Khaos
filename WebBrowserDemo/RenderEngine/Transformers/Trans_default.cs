using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;

namespace RenderEngine.Transformers
{
    public class Trans_default : ITransformable
    {
        public static HashSet<string> exculdedTags = new HashSet<string>();

        static Trans_default()
        {
            exculdedTags.Add("head");
            exculdedTags.Add("meta");
            exculdedTags.Add("script");
            exculdedTags.Add("heading");
            exculdedTags.Add("style");
            exculdedTags.Add("link");
        }

        #region ITransformable Members

        public bool SupportsTag(string tagName)
        {
            return true;
        }

        public VisualNode TransformElement(DOMCore.IElement element, VisualNode parent)
        {
            if (!exculdedTags.Contains(element.TagName))
            {
                IHTMLElement pElement = element as IHTMLElement;

                VisualNode node = null;
                if (pElement != null)
                {
                    node = new VisualNode(parent);
                    //TransformManager.LoadStyle(element, node);
                    node.Element = pElement;

                }
                return node;
            }
            return null;
        }

        #endregion
    }
}
