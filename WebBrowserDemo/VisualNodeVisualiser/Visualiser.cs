using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System.Diagnostics;
using RenderEngine;
using VisualNodeVisualiser;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

 [assembly: DebuggerVisualizer(
     typeof(EngineX.VisualNodeVisualiser),
     typeof(EngineX.ElementTreeVisualizerObjectSource),
     Target = typeof(VisualNode),
     Description = "MetaData:MessageBox")]

namespace EngineX
{
    public class VisualNodeVisualiser : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            visualTreeNode node = objectProvider.GetObject() as visualTreeNode;

            VisForm form = new VisForm(node);

            windowService.ShowDialog(form);
        }
    }

    public class ElementTreeVisualizerObjectSource : VisualizerObjectSource
    {
        private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();

        public override void GetData(object target, Stream outgoingData)
        {
            VisualNode vNode = target as VisualNode;

            _binaryFormatter.Serialize(outgoingData, visualTreeNode.Createtree(vNode));
        }

        ///// <summary>
        ///// The visualizer calls this method when an element's property information
        ///// and snapshot is needed to be displayed in the UI.
        ///// </summary>
        //public override void TransferData(object target, Stream incomingData, Stream outgoingData)
        //{
        //    WpfElement element = (WpfElement)_binaryFormatter.Deserialize(incomingData);

        //    Debug.Assert(!element.IsPopulated, "Element should not be populated with data.");

        //    WpfElement populatedElement = this.CreatePopulatedElement(element);

        //    _binaryFormatter.Serialize(outgoingData, populatedElement);
        //}
    }
}
