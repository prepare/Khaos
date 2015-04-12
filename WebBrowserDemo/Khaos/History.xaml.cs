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
using System.Windows.Shapes;

namespace Khaos
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Window
    {
        DockPanel MyDock = new DockPanel();
        int displayMode = 0;
        public History()
        {
            InitializeComponent();
            MyDock.Children.Add(MakeLabel("Test"));
        }

        public History(KhaosHistory myHistory, int displayMode)
        {
            this.displayMode = displayMode;
            FillDock(myHistory);
            this.Content = MyDock;
        }

        private void FillDock(KhaosHistory myHistory)
        {
            if (displayMode == 1)
            {
                if (MyDock.Children.Count != 0)
                    MyDock.Children.Clear();
                int i = 0;
                while (i < myHistory.Length())
                {
                    Button myButton = MakeButton(myHistory, i);
                    DockPanel.SetDock(myButton, Dock.Top);
                    MyDock.Children.Add(myButton);
                    i++;
                }
                MyDock.Height = MyDock.Children.Count * 30;
                MyDock.VerticalAlignment = VerticalAlignment.Top;
                MyDock.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else
            {
                if (MyDock.Children.Count != 0)
                    MyDock.Children.Clear();
                int i = 0;
                while (i < myHistory.Length())
                {
                    Button myButton = MakeButton(myHistory, i);
                    DockPanel.SetDock(myButton, Dock.Top);
                    MyDock.Children.Add(myButton);
                    i++;
                }
                MyDock.Height = MyDock.Children.Count * 30;
                MyDock.VerticalAlignment = VerticalAlignment.Top;
                MyDock.HorizontalAlignment = HorizontalAlignment.Left;
            }
        }

        private Label MakeLabel(string myString)
        {
            Label myLabel = new Label();
            myLabel.Content = myString;
            return myLabel;
        }
        private Button MakeButton(KhaosHistory myHistory, int i)
        {

            Button myButton = new Button();
            myButton.Content = myHistory.GetTime(i).ToString() + "   Accessed   " + myHistory.GetURL(i);
            myButton.Height = 30;
            myButton.HorizontalContentAlignment = HorizontalAlignment.Left;
            myButton.Click += new RoutedEventHandler(OpenLink);
            return myButton;
        }

        private void OpenLink(object sender, RoutedEventArgs e)
        {
            int i = 0;
            string myURL = "";
            bool myCondition = false;
            string tempString = ((Button)sender).Content.ToString();
            while (i < tempString.Length)
            {
                if (myCondition == false)
                {
                    if (tempString[i].ToString() == "A" && tempString[i + 1].ToString() == "c")
                    {
                        i = i + 11;
                        myCondition = true;
                    }
                }
                if (myCondition)
                    myURL = myURL + tempString[i];

                i++;
            }
            KhaosBrowser newBrowser = new KhaosBrowser(myURL);
            newBrowser.Show();
        }
    }
}