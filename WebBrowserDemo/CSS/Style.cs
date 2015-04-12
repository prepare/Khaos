using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Reflection;

namespace CascadingStyleSheets
{

    public class Style
    {
        public string selector = "";
        public bool bImportant = false;
        public List<string> media = new List<string>();

        protected PhoenixOI.Request request;
        protected Style parent = null;

        /// <summary>
        /// Used for root only
        /// </summary>
        public Style(PhoenixOI.Request request)
            : this()
        {
            this.request = request;
        }

        /// <summary>
        /// Used for root only
        /// </summary>
        public Style()
        {
            this.parent = null;

            // Setup Default Style Here....
            SetDefaults();

            LoadProperties();
            LoadWebProperties();
        }

        // Add Inherit lookup to properties.
        public Style(Style parent)
        {
            SetDefaults();

            this.request = parent.request;
            this.parent = parent;

            LoadProperties();
            LoadWebProperties();

            //// :active, :hover, :link, :visited
           
        }

        public Style Clone()
        {
            Style s = new Style();
            s.request = this.request;
            //s.parent = this.parent;
            s.selector = this.selector;
            s.media = this.media;

            foreach (KeyValuePair<string, PropertyInfo> kvp in properties)
            {
                if (kvp.Value.CanRead && kvp.Value.CanWrite)
                {
                    if (propertiesSet[NameConverter(kvp.Key)])
                    {
                        string v = this[kvp.Key];
                        if (v != null)
                            s[kvp.Key] = v;
                    }
                }
            }

            return s;
        }

        public Style CascadeFrom(Style s)
        {
            this.request = s.request;
            this.parent = s.parent;

            foreach (KeyValuePair<string, PropertyInfo> kvp in s.properties)
            {
                if (kvp.Value.CanRead && kvp.Value.CanWrite)
                {
                    if (s.propertiesSet[NameConverter(kvp.Key)])
                    {
                        string v = s[kvp.Key];
                        if (v != null)
                            this[kvp.Key] = v;
                    }
                }
            }

            return s;
        }

        ////////CHANGES/////////

        private void SetDefaults()
        {
            backImage = new WebImage(this.request);

            //bwLeft.RealValue = 50;
            //bwRight.RealValue = 50;
            //bwTop.RealValue = 50;
            //bwBottom.RealValue = 50;

            paddingTop.RealValue = new Unit(0, UnitType.Pixel);
            paddingBottom.RealValue = new Unit(0, UnitType.Pixel);
            paddingLeft.RealValue = new Unit(0, UnitType.Pixel);
            paddingRight.RealValue = new Unit(0, UnitType.Pixel);

            marginTop.RealValue = new Unit(0, UnitType.Pixel);
            marginBottom.RealValue = new Unit(0, UnitType.Pixel);
            marginLeft.RealValue = new Unit(0, UnitType.Pixel);
            marginRight.RealValue = new Unit(0, UnitType.Pixel);

            WebColor.IsInherit = true; // RealValue = new SolidColorBrush(Colors.Black);

            WebCursor.RealValue = CursorEnum.Inherit;
            WebOverflow.RealValue = OverflowEnum.Hidden;
            WebPosition.RealValue = PositionEnum.Static;
            lineBreak.RealValue = LineBreakEnum.None;

            WebBorderBottomColor.RealValue = new SolidColorBrush(Colors.Black);
            WebBorderLeftColor.RealValue = new SolidColorBrush(Colors.Black);
            WebBorderRightColor.RealValue = new SolidColorBrush(Colors.Black);
            WebBorderTopColor.RealValue = new SolidColorBrush(Colors.Black);

            WebZIndex = -1;
        }

        Dictionary<string, PropertyInfo> properties;
        Dictionary<string, bool> propertiesSet;
        private void LoadProperties()
        {
            properties = new Dictionary<string, PropertyInfo>(54); //54
            propertiesSet = new Dictionary<string, bool>(54);

            Type type = this.GetType();
            foreach (var item in type.GetProperties())
            {
                string name = item.Name.ToLower();

                if (!(name.StartsWith("web") || name == "item"))
                {
                    properties.Add(name, item);
                    propertiesSet.Add(name, false);
                }
            }
        }

        private string NameConverter(string property)
        {
            property = property.ToLower();
            property = property.Replace("-", "");

            return property;
        }

        public string this[string propertyName]
        {
            get
            {
                return (string)properties[NameConverter(propertyName)].GetValue(this, null);
            }
            set
            {
                string key = NameConverter(propertyName);
                if (properties.ContainsKey(key))
                {
                    propertiesSet[key] = true;
                    properties[key].SetValue(this, value, null);
                }
            }
        }

        List<PropertyInfo> propertyList;
        private void LoadWebProperties()
        {
            propertyList = new List<PropertyInfo>(54); //54

            Type type = this.GetType();
            foreach (var item in type.GetProperties())
            {
                string name = item.Name.ToLower();

                if (name.StartsWith("web"))
                    propertyList.Add(item);
            }
        }
        public object this[int propertyIndex]
        {
            get
            {
                PropertyInfo info = propertyList[propertyIndex];
                return info.GetValue(this, null);
            }
        }
        public int WebPropertyCount
        {
            get { return propertyList.Count; }
        }

        ///////////END OF CHANGES/////////////

        public void InheritFrom(Style parent)
        {
            this.request = parent.request;
            this.parent = parent;
        }

        // Standard
        public string Title { get; set; }

        // Background // *
        private WebColor backColor = new WebColor();
        private WebImage backImage;
        private WebEnumInheriting<BackgroundRepeatEnum> backRepeat =
            new WebEnumInheriting<BackgroundRepeatEnum>(BackgroundRepeatEnum.Inherit);
        private WebEnumInheriting<BackgroundAttachmentEnum> backAttach =
            new WebEnumInheriting<BackgroundAttachmentEnum>(BackgroundAttachmentEnum.Inherit);
        private WebEnumInheriting<BackgroundPositionEnum> backPosition =
            new WebEnumInheriting<BackgroundPositionEnum>(BackgroundPositionEnum.Inherit);

        public WebColor WebBackgroundColor
        {
            get { return backColor.IsInherit && parent != null ? parent.WebBackgroundColor : backColor; }
            set { backColor = value; }
        }
        public WebImage WebBackgroundImage
        {
            get { return backImage.IsInherit && parent != null ? parent.WebBackgroundImage : backImage; }
            set { backImage = value; }
        }
        public WebEnumInheriting<BackgroundRepeatEnum> WebBackgroundRepeat
        {
            get { return backRepeat.IsInherit && parent != null ? parent.WebBackgroundRepeat : backRepeat; }
            set { backRepeat = value; }
        }
        public WebEnumInheriting<BackgroundAttachmentEnum> WebBackgroundAttachment
        {
            get { return backAttach.IsInherit && parent != null ? parent.WebBackgroundAttachment : backAttach; }
            set { backAttach = value; }
        }
        public WebEnumInheriting<BackgroundPositionEnum> WebBackgroundPosition
        {
            get { return backPosition.IsInherit && parent != null ? parent.WebBackgroundPosition : backPosition; }
            set { backPosition = value; }
        }

        public string Background
        {
            set
            {
                string[] values = value.Split( new char[] {' '} ,
                    StringSplitOptions.RemoveEmptyEntries);

                if (values.Length > 4)
                    backPosition.StyleValue = values[4];

                if (values.Length > 3)
                    backAttach.StyleValue = values[3];

                if (values.Length > 2)
                    backRepeat.StyleValue = values[2];

                if (values.Length > 1)
                    backImage.StyleValue = values[1];

                if (values.Length > 0)
                    backImage.StyleValue = values[0];
            }
        }
        public string BackgroundColor 
        {
            get { return backColor.StyleValue; }
            set { backColor.StyleValue = value; }
        }
        public string BackgroundImage
        {
            get { return backImage.StyleValue; }
            set { backImage.StyleValue = value; }
        }
        public string BackgroundRepeat 
        {
            get { return backRepeat.StyleValue; }
            set { backRepeat.StyleValue = value; }
        }
        public string BackgroundAttachment
        {
            get { return backAttach.StyleValue; }
            set { backAttach.StyleValue = value; }
        }
        public string BackgroundPosition
        {
            get { return backPosition.StyleValue; }
            set { backPosition.StyleValue = value; }
        }

        // Border  // *
        private WebColor bcLeft = new WebColor();
        private WebColor bcRight = new WebColor();
        private WebColor bcTop = new WebColor();
        private WebColor bcBottom = new WebColor();

        private WebEnum<BorderStyleEnum> bsLeft = new WebEnum<BorderStyleEnum>();
        private WebEnum<BorderStyleEnum> bsRight = new WebEnum<BorderStyleEnum>();
        private WebEnum<BorderStyleEnum> bsTop = new WebEnum<BorderStyleEnum>();
        private WebEnum<BorderStyleEnum> bsBottom = new WebEnum<BorderStyleEnum>();

        private WebBorderWidth bwLeft = new WebBorderWidth();
        private WebBorderWidth bwRight = new WebBorderWidth();
        private WebBorderWidth bwTop = new WebBorderWidth();
        private WebBorderWidth bwBottom = new WebBorderWidth();

        public WebColor WebBorderLeftColor
        {
            get { return bcLeft.IsInherit && parent != null ? parent.WebBorderLeftColor : bcLeft; }
            set { bcLeft = value; }
        }
        public WebColor WebBorderTopColor
        {
            get { return bcTop.IsInherit && parent != null ? parent.WebBorderTopColor : bcTop; }
            set { bcTop = value; }
        }
        public WebColor WebBorderBottomColor
        {
            get { return bcBottom.IsInherit && parent != null ? parent.WebBorderBottomColor : bcBottom; }
            set { bcBottom = value; }
        }
        public WebColor WebBorderRightColor
        {
            get { return bcRight.IsInherit && parent != null ? parent.WebBorderRightColor : bcRight; }
            set { bcRight = value; }
        }

        public WebEnum<BorderStyleEnum> WebBorderLeftStyle
        {
            get { return bsLeft; }
            set { bsLeft = value; }
        }
        public WebEnum<BorderStyleEnum> WebBorderTopStyle
        {
            get { return bsTop; }
            set { bsTop = value; }
        }
        public WebEnum<BorderStyleEnum> WebBorderBottomStyle
        {
            get { return bsBottom; }
            set { bsBottom = value; }
        }
        public WebEnum<BorderStyleEnum> WebBorderRightStyle
        {
            get { return bsRight; }
            set { bsRight = value; }
        }

        public WebBorderWidth WebBorderLeftWidth
        {
            get { return bwLeft; }
            set { bwLeft = value; }
        }
        public WebBorderWidth WebBorderTopWidth
        {
            get { return bwTop; }
            set { bwTop = value; }
        }
        public WebBorderWidth WebBorderBottomWidth
        {
            get { return bwBottom; }
            set { bwBottom = value; }
        }
        public WebBorderWidth WebBorderRightWidth
        {
            get { return bwRight; }
            set { bwRight = value; }
        }

        public string BorderTop
        {
            set 
            {
                string[] values = value.Split(new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries);

                if (values.Length > 2)
                    bcTop.StyleValue = values[2];

                if (values.Length > 1)
                    bsTop.StyleValue = values[1];

                if (values.Length > 0)
                    bwTop.StyleValue = values[0];
            }
        }
        public string BorderBottom
        {
            set
            {
                string[] values = value.Split(new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries);

                if (values.Length > 2)
                    bcBottom.StyleValue = values[2];

                if (values.Length > 1)
                    bsBottom.StyleValue = values[1];

                if (values.Length > 0)
                    bwBottom.StyleValue = values[0];
            }
        }
        public string BorderLeft
        {
            set
            {
                string[] values = value.Split(new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries);

                if (values.Length > 2)
                    bcLeft.StyleValue = values[2];

                if (values.Length > 1)
                    bsLeft.StyleValue = values[1];

                if (values.Length > 0)
                    bwLeft.StyleValue = values[0];
            }
        }
        public string BorderRight
        {
            set
            {
                string[] values = value.Split(new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries);

                if (values.Length > 2)
                    bcRight.StyleValue = values[2];

                if (values.Length > 1)
                    bsRight.StyleValue = values[1];

                if (values.Length > 0)
                    bwRight.StyleValue = values[0];
            }
        }
        public string Border
        {
            set
            {
                string[] values = value.Split(new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < values.Length; i++)
                    if (values[i] == "none")
                        values[i] = "0px";

                if (values.Length > 2)
                {
                    bcTop.StyleValue = values[2];
                    bcBottom.RealValue =
                    bcLeft.RealValue =
                    bcRight.RealValue =
                    bcTop.RealValue;
                }

                if (values.Length > 1)
                {
                    bsTop.StyleValue = values[1];
                    bsBottom.RealValue =
                    bsLeft.RealValue =
                    bsRight.RealValue =
                    bsTop.RealValue;
                }

                if (values.Length > 0)
                {
                    bwTop.StyleValue = values[0];
                    bwBottom.RealValue =
                    bwLeft.RealValue =
                    bwRight.RealValue =
                    bwTop.RealValue;
                }
            }
        }

        public string BorderColor 
        { 
            set 
            {
                bcLeft.StyleValue = value;

                bcBottom.RealValue =
                bcRight.RealValue =
                bcTop.RealValue =
                bcLeft.RealValue;
             }  
        }
        public string BorderLeftColor 
        {
            get { return bcLeft.StyleValue; }
            set { bcLeft.StyleValue = value; }
        }
        public string BorderTopColor
        {
            get { return bcTop.StyleValue; }
            set { bcTop.StyleValue = value; }
        }
        public string BorderBottomtColor
        {
            get { return bcBottom.StyleValue; }
            set { bcBottom.StyleValue = value; }
        }
        public string BorderRightColor
        {
            get { return bcRight.StyleValue; }
            set { bcRight.StyleValue = value; }
        }

        public string BorderStyle
        {
            set
            {
                bsRight.StyleValue = value;

                bsLeft.RealValue =
                bsTop.RealValue =
                bsBottom.RealValue =
                bsRight.RealValue;
            }
        }
        public string BorderLeftStyle
        {
            get { return bsLeft.StyleValue; }
            set { bsLeft.StyleValue = value; }
        }
        public string BorderTopStyle
        {
            get { return bsTop.StyleValue; }
            set { bsTop.StyleValue = value; }
        }
        public string BorderBottomStyle
        {
            get { return bsBottom.StyleValue; }
            set { bsBottom.StyleValue = value; }
        }
        public string BorderRightStyle
        {
            get { return bsRight.StyleValue; }
            set { bsRight.StyleValue = value; }
        }

        public string BorderWidth
        {
            set
            {
                bwTop.StyleValue = value;
                bwLeft.StyleValue = value;
                bwRight.StyleValue = value;
                bwBottom.StyleValue = value;
            }
        }
        public string BorderLeftWidth
        {
            get { return bwRight.StyleValue; }
            set { bwRight.StyleValue = value; }
        }
        public string BorderTopWidth
        {
            get { return bwRight.StyleValue; }
            set { bwRight.StyleValue = value; }
        }
        public string BorderBottomWidth
        {
            get { return bwRight.StyleValue; }
            set { bwRight.StyleValue = value; }
        }
        public string BorderRightWidth
        {
            get { return bwRight.StyleValue; }
            set { bwRight.StyleValue = value; }
        }

        // Margin and Padding //
        private WebLength paddingTop = new WebLength();
        private WebLength paddingBottom = new WebLength();
        private WebLength paddingLeft = new WebLength();
        private WebLength paddingRight = new WebLength();

        private WebLength marginTop = new WebLength();
        private WebLength marginBottom = new WebLength();
        private WebLength marginLeft = new WebLength();
        private WebLength marginRight = new WebLength();

        public string Padding
        {
            set
            {
                string[] values = value.Split(new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries);

                // Top Right Bottom Left

                if (values.Length > 3)
                {
                    paddingLeft.StyleValue = values[3];
                }

                if (values.Length > 2)
                {
                    paddingBottom.StyleValue = values[2];
                }

                if (values.Length > 1)
                {
                    paddingRight.StyleValue = values[1];
                }

                if (values.Length > 0)
                {
                    paddingTop.StyleValue = values[0];

                }
            }
        }
        public string PaddingTop
        {
            get { return paddingTop.StyleValue; }
            set { paddingTop.StyleValue = value; }
        }
        public string PaddingBottom
        {
            get { return paddingBottom.StyleValue; }
            set { paddingBottom.StyleValue = value; }
        }
        public string PaddingLeft
        {
            get { return paddingLeft.StyleValue; }
            set { paddingLeft.StyleValue = value; }
        }
        public string PaddingRight
        {
            get { return paddingRight.StyleValue; }
            set { paddingRight.StyleValue = value; }
        }

        public string Margin
        {
            set
            {
                string[] values = value.Split(new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries);

                // Top Right Bottom Left

                if (values.Length > 3)
                {
                    marginLeft.StyleValue = values[3];
                }

                if (values.Length > 2)
                {
                    marginBottom.StyleValue = values[2];
                }

                if (values.Length > 1)
                {
                    marginRight.StyleValue = values[1];
                }

                if (values.Length > 0)
                {
                    marginTop.StyleValue = values[0];

                }
            }
        }
        public string MarginTop
        {
            get { return marginTop.StyleValue; }
            set { marginTop.StyleValue = value; }
        }
        public string MarginBottom
        {
            get { return marginBottom.StyleValue; }
            set { marginBottom.StyleValue = value; }
        }
        public string MarginLeft
        {
            get { return marginLeft.StyleValue; }
            set { marginLeft.StyleValue = value; }
        }
        public string MarginRight
        {
            get { return marginRight.StyleValue; }
            set { marginRight.StyleValue = value; }
        }

        public WebLength WebPaddingTop
        {
            get { return paddingTop; }
            set { paddingTop = value; }
        }
        public WebLength WebPaddingBottom
        {
            get { return paddingBottom; }
            set { paddingBottom = value; }
        }
        public WebLength WebPaddingLeft
        {
            get { return paddingLeft; }
            set { paddingLeft = value; }
        }
        public WebLength WebPaddingRight
        {
            get { return paddingRight; }
            set { paddingRight = value; }
        }

        public WebLength WebMarginTop
        {
            get { return marginTop; }
            set { marginTop = value; }
        }
        public WebLength WebMarginBottom
        {
            get { return marginBottom; }
            set { marginBottom = value; }
        }
        public WebLength WebMarginLeft
        {
            get { return marginLeft; }
            set { marginLeft = value; }
        }
        public WebLength WebMarginRight
        {
            get { return marginRight; }
            set { marginRight = value; }
        }

        // Dimension  // *
        private WebLength width = new WebLength();
        private WebLength height = new WebLength();

        public WebLength WebWidth
        {
            get { return width; }
            set { width = value; }
        }
        public WebLength WebHeight
        {
            get { return height; }
            set { height = value; }
        }

        public string Width 
        {
            get { return width.StyleValue; }
            set { width.StyleValue = value; }
        }
        public string Height
        {
            get { return height.StyleValue; }
            set { height.StyleValue = value; }
        }

        private WebLength minWidth = new WebLength();
        private WebLength maxWidth = new WebLength();
        private WebLength minHeight = new WebLength();
        private WebLength maxHeight = new WebLength();

        public WebLength WebMinWidth
        {
            get { return minWidth; }
            set { minWidth = value; }
        }
        public WebLength WebMaxWidth
        {
            get { return maxWidth; }
            set { maxWidth = value; }
        }
        public WebLength WebMinHeight
        {
            get { return minHeight; }
            set { minHeight = value; }
        }
        public WebLength WebMaxHeight
        {
            get { return maxHeight; }
            set { maxHeight = value; }
        }

        public string MinWidth
        {
            get { return minWidth.StyleValue; }
            set { minWidth.StyleValue = value; }
        }
        public string MaxWidth
        {
            get { return maxWidth.StyleValue; }
            set { maxWidth.StyleValue = value; }
        }
        public string MinHeight
        {
            get { return minHeight.StyleValue; }
            set { minHeight.StyleValue = value; }
        }
        public string MaxHeight
        {
            get { return maxHeight.StyleValue; }
            set { maxHeight.StyleValue = value; }
        }

        private int pixelHeight;
        private int pixelWidth;
        private WebUnit lineHeight = new WebUnit(); // Incorrect.. Fix in later version. <FIX>

        public int WebPixelHeight
        {
            get { return pixelHeight; }
            set { pixelHeight = value; }
        }
        public int WebPixelWidth
        {
            get { return pixelWidth; }
            set { pixelWidth = value; }
        }
        public WebUnit WebLineHeight
        {
            get { return lineHeight; }
            set { lineHeight = value; }
        }

        public string PixelHeight
        {
            get { return pixelHeight.ToString(); }
            set { int.TryParse(value, out pixelHeight); }
        }
        public string PixelWidth
        {
            get { return pixelWidth.ToString(); }
            set { int.TryParse(value, out pixelWidth); }
        }
        public string LineHeight
        {
            get { return lineHeight.StyleValue; }
            set { lineHeight.StyleValue = value; }
        }

        // Classification // *
        private WebEnum<PositionEnum> position = new WebEnum<PositionEnum>();
        private WebEnum<VAlignEnum> vAlign = new WebEnum<VAlignEnum>();
        private WebEnumInheriting<FloatEnum> cssFloat = new WebEnumInheriting<FloatEnum>(FloatEnum.Inherit);
        private WebEnumInheriting<ClearingEnum> clear = new WebEnumInheriting<ClearingEnum>(ClearingEnum.Inherit);
        private WebCursor cursor = new WebCursor();
        private WebEnum<DisplayEnum> display = new WebEnum<DisplayEnum>();
        private WebEnum<VisibilityEnum> visibility = new WebEnum<VisibilityEnum>();

        public WebEnum<PositionEnum> WebPosition
        {
            get { return position; }
            set { position = value; }
        }
        public WebEnum<VAlignEnum> WebVerticalAlign
        {
            get { return vAlign; }
            set { vAlign = value; }
        }
        public WebEnumInheriting<FloatEnum> WebFloat
        {
            get { return cssFloat.IsInherit && parent != null ? parent.WebFloat : cssFloat; }
            set { cssFloat = value; }
        }
        public WebEnumInheriting<ClearingEnum> WebClear
        {
            get { return clear.IsInherit && parent != null ? parent.WebClear : clear; }
            set { clear = value; }
        }
        public WebCursor WebCursor
        {
            get { return cursor.IsInherit && parent != null ? parent.WebCursor : cursor; }
            set { cursor = value; }
        }
        public WebEnum<DisplayEnum> WebDisplay
        {
            get { return display; }
            set { 
                display = value; 
            }
        }
        public WebEnum<VisibilityEnum> WebVisibility
        {
            get { return visibility; }
            set { visibility = value; }
        }

        public string Position
        {
            get { return position.StyleValue; }
            set { position.StyleValue = value; }
        }
        public string VerticalAlign
        {
            get { return vAlign.StyleValue; }
            set { vAlign.StyleValue = value; }
        }
        public string Float
        {
            get { return cssFloat.StyleValue; }
            set { cssFloat.StyleValue = value; }
        }
        public string Clear
        {
            get { return clear.StyleValue; }
            set { clear.StyleValue = value; }
        }
        public string Cursor
        {
            get { return cursor.StyleValue; }
            set { cursor.StyleValue = value; }
        }
        public string Display
        {
            get { return display.StyleValue; }
            set { display.StyleValue = value; }
        }
        public string Visibility
        {
            get { return visibility.StyleValue; }
            set { visibility.StyleValue = value; }
        }

        // Position //
        private WebLength botton = new WebLength();
        private WebLength left = new WebLength();
        private WebLength right = new WebLength();
        private WebLength top = new WebLength();
        private int zIndex;

        public WebLength WebBottom
        {
            get { return botton; }
            set { botton = value; }
        }
        public WebLength WebLeft
        {
            get { return left; }
            set { left = value; }
        }
        public WebLength WebRight
        {
            get { return right; }
            set { right = value; }
        }
        public WebLength WebTop
        {
            get { return top; }
            set { top = value; }
        }
        public int WebZIndex
        {
            get { return zIndex; }
            set { zIndex = value; }
        }

        public string Bottom
        {
            get { return botton.StyleValue; }
            set { botton.StyleValue = value; }
        }
        public string Left
        {
            get { return left.StyleValue; }
            set { left.StyleValue = value; }
        }
        public string Right
        {
            get { return right.StyleValue; }
            set { right.StyleValue = value; }
        }
        public string Top
        {
            get { return top.StyleValue; }
            set { top.StyleValue = value; }
        }
        public string ZIndex
        {
            get { return zIndex.ToString(); }
            set { int.TryParse(value, out zIndex); }
        }

        private WebEnum<OverflowEnum> overflow = new WebEnum<OverflowEnum>();

        public WebEnum<OverflowEnum> WebOverflow
        {
            get { return overflow; }
            set { overflow = value; }
        }

        public string Overflow
        {
            get { return overflow.StyleValue; }
            set { overflow.StyleValue = value; }
        }
        //public RectangleGeometry Clip { get; set; } // Removed: too hard <Fix>

        // Opacity //
        private byte opacity = (byte)255;
        public byte WebOpacity
        {
            get
            {
                return opacity;
            }
            set
            {
                opacity = value;
            }
        }
        public string Opacity
        {
            get 
            { 
                return ((float)opacity / 255f).ToString(); 
            }
            set
            {
                float percent = 0f;
                if (float.TryParse(value, out percent))
                {
                    float clamped = (percent > 1) ? 1 : ((percent < 0) ? 0 : percent);
                    opacity = (byte)(clamped * 255);
                }
            } 
        }

        // Text //
        private WebColor color = new WebColor();
        public WebColor WebColor
        {
            get { return color.IsInherit && parent != null ? parent.WebColor : color; }
            set { color = value; }
        }
        public string Color
        {
            get { return color.StyleValue; }
            set { color.StyleValue = value; }
        }

        private WebEnumInheriting<TextDecorationEnum> textDecoration = 
            new WebEnumInheriting<TextDecorationEnum>(TextDecorationEnum.Inherit);
        public WebEnumInheriting<TextDecorationEnum> WebTextDecoration
        {
            get { return textDecoration.IsInherit && parent != null ? parent.WebTextDecoration : textDecoration; }
            set { textDecoration = value; }
        }
        public string TextDecoration
        {
            get { return textDecoration.StyleValue; }
            set { textDecoration.StyleValue = value; }
        }

        private WebEnum<TextAlignEnum> textAlign = new WebEnum<TextAlignEnum>();
        public WebEnum<TextAlignEnum> WebTextAlign
        {
            get { return textAlign; }
            set { textAlign = value; }
        }
        public string TextAlign
        {
            get { return textAlign.StyleValue; }
            set { textAlign.StyleValue = value; }
        }

        // Font //
        public string Font
        {
            set
            {
                string[] values = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (values.Length > 4)
                    fontFamily.StyleValue = values[4]; 
                if (values.Length > 3)
                    FontSize = values[3];
                if (values.Length > 2)
                    fontWeight.StyleValue = values[2]; 
                if (values.Length > 1)
                    FontVariant = values[1];
                if (values.Length > 0)
                    fontStyle.StyleValue = values[0];
                
            }
        }

        private WebFontFamily fontFamily = new WebFontFamily();
        private WebUnit fontSize = new WebUnit();
        private WebFontStyle fontStyle = new WebFontStyle();
        private WebFontWeight fontWeight = new WebFontWeight();
        private WebFontStretch fontStretch = new WebFontStretch();

        public WebFontFamily WebFontFamily
        {
            get { return fontFamily; }
            set { fontFamily = value; }
        }
        public Unit WebFontSize
        {
            get 
            {
                if (fontSize.RealValue.Value < 4)
                    return new Unit(12, UnitType.Pixel);
                return fontSize.RealValue; 
            }
            set { fontSize.RealValue = value; }
        }
        public WebFontStyle WebFontStyle
        {
            get { return fontStyle; }
            set { fontStyle = value; }
        }
        public WebFontWeight WebFontWeight
        {
            get { return fontWeight; }
            set { fontWeight = value; }
        }
        public WebFontStretch WebFontStretch
        {
            get { return fontStretch; }
            set { fontStretch = value; }
        }

        public string FontFamily
        {
            get { return fontFamily.StyleValue; }
            set { fontFamily.StyleValue = value; }
        }
        public string FontSize
        {
            get { return fontSize.StyleValue; }
            set
            {
                if (value == "xx-small")
                    value = "4px";
                if (value == "x-small")
                    value = "8px";
                if (value == "small")
                    value = "10px";
                if (value == "medium")
                    value = "12px";
                if (value == "large")
                    value = "14px";
                if (value == "x-large")
                    value = "16px";
                if (value == "xx-large")
                    value = "18px";
                if (value == "smaller")
                    value = "75%";
                if (value == "larger")
                    value = "150%";

                fontSize.StyleValue = value; 
            }
        }
        public string FontStyle
        {
            get { return fontStyle.StyleValue; }
            set { fontStyle.StyleValue = value; }
        }
        public string FontWeight
        {
            get { return fontWeight.StyleValue; }
            set { fontWeight.StyleValue = value; }
        }
        public string FontStretch
        {
            get { return fontStretch.StyleValue; }
            set { fontStretch.StyleValue = value; }
        }

        public string FontVariant { get; set; } // FontVariants (Not supported by render engine)

        // Custom
        private WebEnum<LineBreakEnum> lineBreak = new WebEnum<LineBreakEnum>();
        public WebEnum<LineBreakEnum> WebLineBreak 
        {
            get { return lineBreak; }
            set { lineBreak = value; }
        }

        public string LineBreak
        {
            get { return WebLineBreak.StyleValue; }
            set { WebLineBreak.StyleValue = value; }
        }
    }
}
