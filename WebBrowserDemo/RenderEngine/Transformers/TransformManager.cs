using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOMCore;
using PhoenixOI;
using System.Windows.Threading;
using System.Diagnostics;

namespace RenderEngine.Transformers
{
    public class TransformManager
    {
        private static List<ITransformable> supportedTransforms;
        
        private Trans_html htmlTransformer;
        private static Trans_text textTransformer;

        static TransformManager()
        {
            supportedTransforms = new List<ITransformable>();
            textTransformer = new Trans_text();

            supportedTransforms.Add(new Trans_a());
            supportedTransforms.Add(new Trans_br());
            supportedTransforms.Add(new Trans_center());
            supportedTransforms.Add(new Trans_lable());
            supportedTransforms.Add(new Trans_input());
            supportedTransforms.Add(new Trans_br());
            supportedTransforms.Add(new Trans_img());
            supportedTransforms.Add(new Trans_form());
            supportedTransforms.Add(new Trans_noscript());
            supportedTransforms.Add(new Trans_noframes());

            supportedTransforms.Add(new Trans_table());
            supportedTransforms.Add(new Trans_tr());
            supportedTransforms.Add(new Trans_td());

            supportedTransforms.Add(new Trans_default());
        }

        private IHTMLDocument htmlDocument = null;
        private VisualDocument visualDocument = null;

        public IHTMLDocument HtmlDocument
        {
            set
            {
                htmlDocument = value;
            }
        }

        public VisualDocument VisDocument
        {
            get
            {
                return visualDocument;
            }
        }

        public void Transform(Request webRequest, IPageHost pageHost, Dispatcher mainDispatcher)
        {
            htmlTransformer = new Trans_html();
            htmlTransformer.PageHost = pageHost;
            htmlTransformer.WebRequest = webRequest;

            // The stack for the current dom and render elements
            //Stack<IElement> domStack = new Stack<IElement>();
            Stack<INodeList> domStack = new Stack<INodeList>();
            Stack<VisualNode> renderStack = new Stack<VisualNode>();

            if (htmlDocument.DocumentElement == null)
            {
                visualDocument = VisualDocument.CreateVisualRoot(pageHost, webRequest);
                visualDocument.SetDefaultStyle();
            }
            else
            {

                //mainDispatcher.Invoke( (Action)delegate {
                visualDocument = (htmlTransformer.TransformElement(htmlDocument.DocumentElement, null) as VisualDocument);
                //});
            }

            // Set the initial stack elements
            renderStack.Push(visualDocument);
            //domStack.Push(htmlDocument.DocumentElement);
            domStack.Push(htmlDocument.ChildNodes);

            // While there are still elements to evaluate
            while (domStack.Count > 0)
            {
                // get the next element to evaluate
                //IElement item = domStack.Pop();
                INodeList itemList = domStack.Pop();
                VisualNode parent = renderStack.Pop();

                VisualNode nextNode = null;

                if (itemList != null)
                {

                    // For each child the element has
                    for (int cIndex = 0; cIndex < itemList.Length; cIndex++)
                    {
                        object unidentifiedObject = itemList[cIndex];

                        if (unidentifiedObject is IElement) //IText
                        {
                            var child = unidentifiedObject as IElement;

                            // Transform from DOM to Visual Element
                            nextNode = this.TransformElement(child, parent, mainDispatcher);

                            if (nextNode != null)
                            {
                                // Build Visual Tree
                                //parent.Add(nextNode);

                                // Add Child to process stack
                                renderStack.Push(nextNode);
                                domStack.Push(child.ChildNodes);
                            }
                            else
                            {
                                Debug.WriteLine(child.TagName);
                            }

                        }
                        else if (unidentifiedObject is IText)
                        {
                        //    mainDispatcher.Invoke( (Action)delegate {
                                textTransformer.TransformText(unidentifiedObject as IText, parent);
                        //    });
                        }
                        else
                        {
                            throw new Exception("Sanity Check");
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("Connect to the internet and try again");
                }
            }
        }

        private VisualNode TransformElement(IElement element, VisualNode parent, Dispatcher mainDispatcher)
        {
            foreach (var item in supportedTransforms)
            {
                string tag = element.TagName;
                if (item.SupportsTag(tag))
                {
                    VisualNode result = null;
                    //mainDispatcher.Invoke((Action)delegate {
                          result = item.TransformElement(element, parent);
                    //  });
                    return result;
                }
            }
            return null;
        }

    }
}
