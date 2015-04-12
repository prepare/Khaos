using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RenderEngine;
using RenderEngine.Transformers;

namespace RenderDemo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        //protected override void OnMouseDown(MouseButtonEventArgs e)
        //{
        //    this.Name = "Cooler";
        //    base.OnMouseDown(e);
        //}

        public override void BeginInit()
        {
            base.BeginInit();

            ScrollViewer viewer = new ScrollViewer();
            //viewer.CanContentScroll = true;
            //viewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            viewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            VisualDocument page = VisualDocument.CreateVisualRoot(null, null);
            page.ItemName = "page";
            page.CssStyle.Display = "Block";

            //TextTests(page);
            
            //Google(page);
            DemoPage(page);
            //InlinesSmall(page);
            //InlinesLarge(page);

            //Blocks(page);
            //BlockAndInlines(page);

            //viewer.Content = page;
            //this.Content = viewer;

            this.Content = page;

        }

        private void TextTests(VisualNode parent)
        {
            VisualText text1 = new VisualText(parent);
            text1.Text = "An emotion is a mental and physiological state associated with a wide variety of feelings, thoughts, and behavior. ";
            VisualText text2 = new VisualText(parent);
            text2.Text = "Emotions are subjective experiences, or experienced from an individual point of view. ";
            VisualText text3 = new VisualText(parent);
            text3.Text = "Emotion is often associated with mood, temperament, personality, and disposition.";
        }

        private void Google(VisualNode parent)
        {
            VisualNode body = new VisualNode(parent);
            body.ItemName = "body";

            VisualNode div1 = new VisualNode(body);

            VisualNode menu = new VisualNode(div1);
                menu.CssStyle.BorderBottom = "1px solid lightblue";

                    VisualAnchor webLink = new VisualAnchor(menu);
                    VisualText textM1 = new VisualText(webLink);
                    textM1.Text = "Web ";
                    webLink.Href = "test";

                    //VisualText text1 = new VisualText(menu);
                    //text1.Text = " ";

                    VisualAnchor imageLink = new VisualAnchor(menu);
                    VisualText textM2 = new VisualText(imageLink);
                    textM2.Text = "Images";
                    imageLink.Href = "test";

                VisualCenter pageCenter = new VisualCenter(div1);
                pageCenter.ItemName = "pageCenter";

                    VisualBreak breaker1 = new VisualBreak(pageCenter);

                    VisualImage googleImage = new VisualImage(pageCenter);
                    googleImage.CssStyle.BackgroundImage = @"url( http://www.google.com.au/intl/en_au/images/logo.gif )";
                    googleImage.ItemName = "googleImage";

                    VisualBreak breaker2 = new VisualBreak(pageCenter);

                    VisualInput searchBox = new VisualInput(pageCenter);
                    searchBox.Type = VisualInput.InputType.text;
                    searchBox.Value = "test";
                    searchBox.ItemName = "searchBox";
                    //Input Textbox
                    //Input Button
                    //Input button
                    //Radio buttons

                    VisualBreak breaker3 = new VisualBreak(pageCenter);

                    VisualInput searchButton = new VisualInput(pageCenter);
                    searchButton.Type = VisualInput.InputType.submit;
                    searchButton.Value = "Search";
                    searchButton.ItemName = "searchButton";

                    VisualBreak breaker4 = new VisualBreak(pageCenter);

                    VisualAnchor bLink1 = new VisualAnchor(pageCenter);
                    bLink1.ItemName = "pageCenter";
                    VisualText textB1 = new VisualText(bLink1);
                    textB1.ItemName = "textB1";
                    textB1.Text = "Advertising Programmes ";
                    bLink1.Href = "test";

                    VisualAnchor bLink2 = new VisualAnchor(pageCenter);
                    VisualText textB2 = new VisualText(bLink2);
                    textB2.Text = "Business Solutions ";
                    bLink2.Href = "test";

                    VisualAnchor bLink3 = new VisualAnchor(pageCenter);
                    VisualText textB3 = new VisualText(bLink3);
                    textB3.Text = "About Google ";
                    bLink3.Href = "test";

                    VisualAnchor bLink4 = new VisualAnchor(pageCenter);
                    VisualText textB4 = new VisualText(bLink4);
                    textB4.Text = "Go to Google.com";
                    bLink4.Href = "test";

            //Advertising Programmes - Business Solutions - About Google - Go to Google.com

        }

        private void DemoPage(VisualNode parent)
        {
            parent.Background = new SolidColorBrush(Colors.LightBlue);

            VisualNode body, div1, div2, div3, center, span1, span2;
            VisualText text1, text2; 

            // <body style="padding: 5px 5px 5px 5px;">
            body = new VisualNode(parent);
            body.CssStyle.Padding = "5px 5px 5px 5px";
            body.ItemName = "body";

            //  <div style="background-color: gray;">
            div1 = new VisualNode(body);
            div1.CssStyle.BackgroundColor = "Gray";
            div1.ItemName = "div1";

            span1 = new VisualNode(div1);
            span1.CssStyle.BackgroundColor = "Green";
            span1.CssStyle.Height = "12px";
            span1.ItemName = "span1";

            //   <center>
            center = new VisualCenter(div1);
            center.CssStyle.BackgroundColor = "Pink";
            center.ItemName = "center";

            //    <div style="width: 80%; height: 300px; border-left: solid 5px green; border-right: solid 3px blue;">
            div2 = new VisualNode(center);
            div2.ItemName = "div2";
            div2.CssStyle.Width = "50%";
            div2.CssStyle.Height = "300px";
            div2.CssStyle.BorderLeft = "20px solid orange"; // Is his the right order?? <:FIX>
            div2.CssStyle.BorderRight = "5px solid orange";
            div2.CssStyle.BackgroundColor = "Blue";
            
            //     Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text. Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text .
            //<:CANNOT YET ADD TEXT>
            
            //     <div style="top: 5px; left: 5px; width: 100px; height: 100px">
            div3 = new VisualNode(div2);
            div3.CssStyle.Top = "50px";
            div3.CssStyle.Left = "50px";
            div3.CssStyle.Width = "100px";
            div3.CssStyle.Height = "100px";
            div3.CssStyle.BackgroundColor = "Red";
            div3.CssStyle.Display = "block";
            div3.ItemName = "div3";

            VisualButton button1 = new VisualButton(div3);
            button1.Text = "Hello World";
            button1.ItemName = "button1";
            button1.Name = "button1";

            ////      Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text. 
            //text1 = new VisualText(div3);
            //text1.Text = "Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text.";
            //text1.Name = "text1";

            //     </div>

            VisualLabel label1 = new VisualLabel(div2);
            label1.For = "button1";


            //      Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text. 
            text2 = new VisualText(label1);
            text2.Text = "Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text.";
            text2.ItemName = "text2";

            //    </div>
            //   </center>
            //   <span style="background-color: green;">

            //   </span>
            //   <span style="background-color: red; border-color: black; border-width: 3px; border-style: solid;">
            span2 = new VisualNode(div1);
            span2.CssStyle.BackgroundColor = "Red";
            span2.CssStyle.BorderColor = "Black";
            span2.CssStyle.BorderWidth = "3px";
            span2.CssStyle.BorderStyle = "solid";
            span2.CssStyle.Height = "12px";
            span2.ItemName = "span2";

            VisualImage image = new VisualImage(div1);
            image.CssStyle.BackgroundImage = @"url( http://www.google.com.au/intl/en_au/images/logo.gif )";
            image.ItemName = "image";

            //    Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text.
            //<:CANNOT YET ADD TEXT>

            //   </span>
            //  </div>
            // </body>

        }

        private void InlinesSmall(VisualNode parent)
        {

            VisualNode containter1 = new VisualNode(parent);
            containter1.ItemName = "containter1";
            containter1.Background = new SolidColorBrush(Colors.Orange);
            containter1.CssStyle.Margin = "5px 5px 5px 5px";
            containter1.CssStyle.Display = "Inline";

            VisualNode containter2 = new VisualNode(containter1);
            containter2.ItemName = "containter2";
            containter2.Background = new SolidColorBrush(Colors.Green);
            containter2.CssStyle.Margin = "5px 5px 5px 5px";
            containter2.CssStyle.Display = "Inline";

            VisualNode level0A = new VisualNode(containter2);
            level0A.ItemName = "level0A";
            level0A.Background = new SolidColorBrush(Colors.Blue);
            level0A.CssStyle.Margin = "5px 5px 5px 5px";
            level0A.CssStyle.Display = "Inline";

            VisualNode level1A = new VisualNode(level0A);
            level1A.ItemName = "level1A";
            level1A.Background = new SolidColorBrush(Colors.LightBlue);
            level1A.CssStyle.Margin = "5px 5px 5px 5px";
            level1A.CssStyle.Display = "Inline";

            VisualNode level2A = new VisualNode(level1A);
            level2A.ItemName = "level2A";
            level2A.Background = new SolidColorBrush(Color.FromRgb(20, 20, 20));
            level2A.CssStyle.Display = "Inline";
            level2A.CssStyle.Width = "200px";
            level2A.CssStyle.Height = "100px";

            VisualNode level2B = new VisualNode(level1A);
            level2B.ItemName = "level2B";
            level2B.Background = new SolidColorBrush(Color.FromRgb(40, 40, 40));
            level2B.CssStyle.Display = "Inline";
            level2B.CssStyle.Width = "200px";
            level2B.CssStyle.Height = "100px";

            VisualNode level2C = new VisualNode(level1A);
            level2C.ItemName = "level2C";
            level2C.CssStyle.Display = "Inline";
            level2C.Background = new SolidColorBrush(Color.FromRgb(60, 60, 60));
            level2C.CssStyle.Width = "200px";
            level2C.CssStyle.Height = "100px";

            VisualNode level2D = new VisualNode(level1A);
            level2D.ItemName = "level2D";
            level2D.Background = new SolidColorBrush(Color.FromRgb(80, 80, 80));
            level2D.CssStyle.Display = "Inline";
            level2D.CssStyle.Width = "200px";
            level2D.CssStyle.Height = "100px";

            VisualNode level2E = new VisualNode(level1A);
            level2E.ItemName = "level2E";
            level2E.Background = new SolidColorBrush(Color.FromRgb(100, 100, 100));
            level2E.CssStyle.Display = "Inline";
            level2E.CssStyle.Width = "200px";
            level2E.CssStyle.Height = "100px";

        }

        private void InlinesLarge(VisualNode parent)
        {

            VisualNode containter1 = new VisualNode(parent);
            containter1.ItemName = "containter1";
            containter1.Background = new SolidColorBrush(Colors.Orange);
            containter1.CssStyle.Margin = "5px 5px 5px 5px";
            containter1.CssStyle.Display = "Inline";

            VisualNode level0A = new VisualNode(containter1);
            level0A.ItemName = "level0A";
            level0A.Background = new SolidColorBrush(Colors.Red);
            level0A.CssStyle.Margin = "5px 5px 5px 5px";
            level0A.CssStyle.Display = "Inline";

            VisualNode level1A = new VisualNode(level0A);
            level1A.ItemName = "level1A";
            level1A.Background = new SolidColorBrush(Colors.Blue);
            level1A.CssStyle.Margin = "5px 5px 5px 5px";
            level1A.CssStyle.Display = "Inline";

            VisualNode level2A = new VisualNode(level1A);
            level2A.ItemName = "level2A";
            level2A.Background = new SolidColorBrush(Color.FromRgb(20, 20, 20));
            level2A.CssStyle.Display = "Inline";
            level2A.CssStyle.Width = "100px";
            level2A.CssStyle.Height = "100px";

            VisualNode level2B = new VisualNode(level1A);
            level2B.ItemName = "level2B";
            level2B.Background = new SolidColorBrush(Color.FromRgb(40, 40, 40));
            level2B.CssStyle.Display = "Inline";
            level2B.CssStyle.Width = "100px";
            level2B.CssStyle.Height = "100px";

            VisualNode level2C = new VisualNode(level1A);
            level2C.ItemName = "level2C";
            level2C.CssStyle.Display = "Inline";
            level2C.Background = new SolidColorBrush(Color.FromRgb(60, 60, 60));
            level2C.CssStyle.Width = "100px";
            level2C.CssStyle.Height = "100px";

            VisualNode level2D = new VisualNode(level1A);
            level2D.ItemName = "level2D";
            level2D.Background = new SolidColorBrush(Color.FromRgb(80, 80, 80));
            level2D.CssStyle.Display = "Inline";
            level2D.CssStyle.Width = "100px";
            level2D.CssStyle.Height = "100px";

            VisualNode level2E = new VisualNode(level1A);
            level2E.ItemName = "level2E";
            level2E.Background = new SolidColorBrush(Color.FromRgb(100, 100, 100));
            level2E.CssStyle.Display = "Inline";
            level2E.CssStyle.Width = "100px";
            level2E.CssStyle.Height = "100px";

            VisualNode level2F = new VisualNode(level1A);
            level2F.ItemName = "level2F";
            level2F.CssStyle.Display = "Inline";
            level2F.Background = new SolidColorBrush(Color.FromRgb(120, 120, 120));
            level2F.CssStyle.Width = "100px";
            level2F.CssStyle.Height = "100px";

            VisualNode level2G = new VisualNode(level1A);
            level2G.ItemName = "level2G";
            level2G.Background = new SolidColorBrush(Color.FromRgb(140, 140, 140));
            level2G.CssStyle.Display = "Inline";
            level2G.CssStyle.Width = "100px";
            level2G.CssStyle.Height = "100px";

            VisualNode level2H = new VisualNode(level1A);
            level2H.ItemName = "level2H";
            level2H.Background = new SolidColorBrush(Color.FromRgb(160, 160, 160));
            level2H.CssStyle.Display = "Inline";
            level2H.CssStyle.Width = "100px";
            level2H.CssStyle.Height = "100px";

            VisualNode level2I = new VisualNode(level1A);
            level2I.ItemName = "level2I";
            level2I.Background = new SolidColorBrush(Color.FromRgb(180, 180, 180));
            level2I.CssStyle.Display = "Inline";
            level2I.CssStyle.Width = "100px";
            level2I.CssStyle.Height = "100px";

            VisualNode level2J = new VisualNode(level1A);
            level2J.ItemName = "level2J";
            level2J.CssStyle.Display = "Inline";
            level2J.Background = new SolidColorBrush(Color.FromRgb(200, 200, 200));
            level2J.CssStyle.Width = "100px";
            level2J.CssStyle.Height = "100px";

            VisualNode level2K = new VisualNode(level1A);
            level2K.ItemName = "level2K";
            level2K.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));
            level2K.CssStyle.Display = "Inline";
            level2K.CssStyle.Width = "100px";
            level2K.CssStyle.Height = "100px";

            VisualNode level2L = new VisualNode(level1A);
            level2L.ItemName = "level2L";
            level2L.Background = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            level2L.CssStyle.Display = "Inline";
            level2L.CssStyle.Width = "100px";
            level2L.CssStyle.Height = "100px";

            VisualNode level2M = new VisualNode(level1A);
            level2M.ItemName = "level2M";
            level2M.CssStyle.Display = "Inline";
            level2M.Background = new SolidColorBrush(Color.FromRgb(250, 250, 250));
            level2M.CssStyle.Width = "100px";
            level2M.CssStyle.Height = "100px";

            //////
            //////

            VisualNode level1B = new VisualNode(parent);
            level1B.ItemName = "level1B";
            level1B.Background = new SolidColorBrush(Colors.Blue);
            level1B.CssStyle.Margin = "5px 5px 5px 5px";
            level1B.CssStyle.Display = "Inline";

            VisualNode level2A2 = new VisualNode(level1B);
            level2A2.ItemName = "level2A";
            level2A2.Background = new SolidColorBrush(Color.FromRgb(100, 100, 100));
            level2A2.CssStyle.Display = "Inline";
            level2A2.CssStyle.Width = "100px";
            level2A2.CssStyle.Height = "100px";

            VisualNode level2B2 = new VisualNode(level1B);
            level2B2.ItemName = "level2B";
            level2B2.Background = new SolidColorBrush(Color.FromRgb(120, 120, 120));
            level2B2.CssStyle.Display = "Inline";
            level2B2.CssStyle.Width = "100px";
            level2B2.CssStyle.Height = "100px";

            VisualNode level2C2 = new VisualNode(level1B);
            level2C2.ItemName = "level2C";
            level2C2.CssStyle.Display = "Inline";
            level2C2.Background = new SolidColorBrush(Color.FromRgb(140, 140, 140));
            level2C2.CssStyle.Width = "100px";
            level2C2.CssStyle.Height = "100px";

            VisualNode level2D2 = new VisualNode(level1B);
            level2D2.ItemName = "level2D";
            level2D2.Background = new SolidColorBrush(Color.FromRgb(160, 160, 160));
            level2D2.CssStyle.Display = "Inline";
            level2D2.CssStyle.Width = "100px";
            level2D2.CssStyle.Height = "100px";

            VisualNode level2E2 = new VisualNode(level1B);
            level2E2.ItemName = "level2E";
            level2E2.Background = new SolidColorBrush(Color.FromRgb(180, 180, 180));
            level2E2.CssStyle.Display = "Inline";
            level2E2.CssStyle.Width = "100px";
            level2E2.CssStyle.Height = "100px";

            VisualNode level2F2 = new VisualNode(level1B);
            level2F2.ItemName = "level2F";
            level2F2.CssStyle.Display = "Inline";
            level2F2.Background = new SolidColorBrush(Color.FromRgb(250, 250, 250));
            level2F2.CssStyle.Width = "100px";
            level2F2.CssStyle.Height = "100px";

            //// Level 1
            //parent.Add(rootNode);

            //// Level 2
            //rootNode.Add(level2A);

            ////// Level 3
            //level2A.Add(level3A);
            //level2A.Add(level3B);
            //level2A.Add(level3C);
        }

        private void Blocks(VisualNode parent)
        {
            VisualNode element1 = new VisualNode(parent);
            element1.ItemName = "element1";
            element1.Background = new SolidColorBrush(Colors.Blue);
            element1.CssStyle.Height = "100px";
            element1.CssStyle.Display = "Block";

            VisualNode element2 = new VisualNode(parent);
            element2.ItemName = "element2";
            element2.Background = new SolidColorBrush(Colors.Red);
            element2.CssStyle.Display = "Block";
            element2.CssStyle.Height = "100px";

            VisualNode element3 = new VisualNode(parent);
            element3.ItemName = "element3";
            element3.Background = new SolidColorBrush(Colors.Green);
            element3.CssStyle.Display = "Block";
            element3.CssStyle.Height = "100px";

            //parent.Add(element1);
            //parent.Add(element2);
            //parent.Add(element3);
        }

        private void BlockAndInlines(VisualNode parent)
        {
            VisualNode rootNode = new VisualNode(parent);
            rootNode.ItemName = "rootNode";
            rootNode.CssStyle.Display = "Block";
            rootNode.Background = new SolidColorBrush(Colors.Aqua);


            VisualNode level2A = new VisualNode(rootNode);
            level2A.ItemName = "level2A";
            level2A.Background = new SolidColorBrush(Colors.Blue);
            level2A.CssStyle.Display = "Block";
            level2A.CssStyle.Width = "400px";
            level2A.CssStyle.Height = "100px";

            VisualNode level2B = new VisualNode(rootNode);
            level2B.ItemName = "level2B";
            level2B.CssStyle.Display = "Inline";
            level2B.CssStyle.Padding = "5px 5px 5px 5px";
            level2B.Background = new SolidColorBrush(Colors.Red);


            VisualNode level3A = new VisualNode(level2B);
            level3A.ItemName = "level3A";
            level3A.Background = new SolidColorBrush(Colors.Green);
            level3A.CssStyle.Display = "Inline";
            level3A.CssStyle.Width = "200px";
            level3A.CssStyle.Height = "100px";

            VisualNode level3B = new VisualNode(level2B);
            level3B.ItemName = "level3B";
            level3B.Background = new SolidColorBrush(Colors.Yellow);
            level3B.CssStyle.Display = "Inline";
            level3B.CssStyle.Width = "100px";
            level3B.CssStyle.Height = "100px";

            //// Level 1
            //parent.Add(rootNode);

            //// Level 2
            //rootNode.Add(level2A);
            //rootNode.Add(level2B);

            ////// Level 3
            //level2B.Add(level3A);
            //level2B.Add(level3B);
        }
    }
}
