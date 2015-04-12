using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace CSS
{

    #region enums
    public enum Floats
    {
        Left,
        Right,
        None,
        Inherit
    }

    public enum Clearing
    {
        Left,
        Right,
        None,
        Both,
        Inherit
    }

    public enum Visibility
    {
        Visiblie,
        Hidden,
        Inherit
    }

    public enum Overflow
    {
        Visible,
        Hidden,
        Scroll,
        Auto,
        Inherit
    }
    #endregion

    public class CSSStyle
    {
        public Dictionary<string, List<string>> attributes = new Dictionary<string, List<string>>();

        public Floats CssFloat 
        {
            get
            {
                return Floats.None;
            }
            set
            {
            }
        }

        public Unit Width 
        {
            get
            {
                if (attributes.ContainsKey("width") && attributes["width"].Count > 0)
                    return Unit.Parse(attributes["width"][0].Replace('.',','));
                return new Unit();
            }
            set
            {
            }
        }

        public Unit Height 
        {
            get
            {
                if (attributes.ContainsKey("height") && attributes["width"].Count > 0)
                    return Unit.Parse(attributes["height"][0].Replace('.', ','));
                return new Unit();
            }
            set
            {
            }
        }

        public Unit MinWidth 
        {
            get
            {
                if (attributes.ContainsKey("min-width") && attributes["min-width"].Count > 0)
                    return Unit.Parse(attributes["min-width"][0].Replace('.', ','));
                return new Unit();
            }
            set
            {
            }
        }

        public Unit MaxWidth 
        {
            get
            {
                if (attributes.ContainsKey("max-width") && attributes["max-width"].Count > 0)
                    return Unit.Parse(attributes["max-width"][0].Replace('.', ','));
                return new Unit();
            }
            set
            {
            }
        }

        public Unit MinHeight 
        {
            get
            {
                if (attributes.ContainsKey("min-height") && attributes["min-height"].Count > 0)
                    return Unit.Parse(attributes["min-height"][0].Replace('.', ','));
                return new Unit();
            }
            set
            {
            }
        }

        public Unit MaxHeight 
        {
            get
            {
                if (attributes.ContainsKey("max-height") && attributes["max-height"].Count > 0)
                    return Unit.Parse(attributes["max-height"][0].Replace('.', ','));
                return new Unit();
            }
            set
            {
            }
        }

        public Distancing Padding 
        {
            get
            {
                Distancing d = new Distancing();
                if (attributes.ContainsKey("padding") && attributes["padding"].Count > 0)
                {
                    if (attributes["padding"].Count == 1)
                    {
                    }
                }
                return null;
            }
            set
            {
            }
        }

        public Distancing Border 
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public Distancing Margin 
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public Distancing Position 
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        // border-style, border-spacing
        //text-indent, line-height, word-spacing
        //vertical-align

        public string Src 
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        public Overflow Overflow 
        {
            get
            {
                return Overflow.Hidden;
            }
            set
            {
            }
        }

        public FontFamily FontFaimily 
        {
            get
            {
                return null;
            }
            set
            {
            }
        } //font-family

        public FontSize FontSize 
        {
            get
            {
                return FontSize.Medium;
            }
            set
            {
            }
        } //font-size

        public FontStyle FontStyle 
        {
            get
            {
                return new FontStyle();
            }
            set
            {
            }
        } //font-style


        public Color Color 
        {
            get
            {
                return new Color();
            }
            set
            {
            }
        }

        public Color BackgroundColor 
        {
            get
            {
                return new Color();
            }
            set
            {
            }
        }


        public string BackgroundImage 
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        public int ZIndex 
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }

        public Visibility Visibility 
        {
            get
            {
                return new Visibility();
            }
            set
            {
            }
        }

        public Clearing Clear 
        {
            get
            {
                return new Clearing();
            }
            set
            {
            }
        }

        public Geometry Clip { get; set; }

        public void InheritFrom(CSSStyle parentStyle)
        {
            foreach (KeyValuePair<string, List<string>> kvp in parentStyle.attributes)
            {
                if (!attributes.ContainsKey(kvp.Key))
                {
                    attributes.Add(kvp.Key, kvp.Value);
                }
            }
        }
    }
}
