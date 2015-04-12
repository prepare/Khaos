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
using PhoenixEngine;
using System.Windows.Threading;
using System.Diagnostics;

namespace Khaos
{
    /// <summary>
    /// Interaction logic for KhaosBrowser.xaml
    /// </summary>
    public partial class KhaosBrowser : Window
    {
        public readonly static RoutedUICommand NewTabCommand;
        public readonly static RoutedUICommand ViewToolsCommand;
        public readonly static RoutedUICommand ViewAboutCommand;
        public readonly static RoutedUICommand ViewHistoryCommand;

        static KhaosBrowser()
        {
            NewTabCommand = new RoutedUICommand("NewTab", "NewTab", typeof(KhaosBrowser));
            ViewToolsCommand = new RoutedUICommand("ViewTools", "ViewTools", typeof(KhaosBrowser));
            ViewAboutCommand = new RoutedUICommand("ViewAbout", "ViewAbout", typeof(KhaosBrowser));
            ViewHistoryCommand = new RoutedUICommand("ViewHistory", "ViewHistory", typeof(KhaosBrowser));
        }

        private KhaosHistory myKhaosHistory = new KhaosHistory();

        public KhaosBrowser()
        {
            InitializeComponent();

            NavTextBox.MouseDoubleClick += delegate(object sender, MouseButtonEventArgs e)
            {
                NavTextBox tb = (sender as NavTextBox);
                if (tb != null)
                    tb.SelectAll();
            };

        }

        public KhaosBrowser(string givenURL) : this()
        {
            GetCurrentBrowserControl().Navigate(givenURL);
        }

        private void NavTextBox_Navigate(object sender, RoutedEventArgs e)
        {
            // Navigate
            GetCurrentBrowserControl().Navigate(NavTextBox.Text);
        }

        private BrowserControl GetCurrentBrowserControl()
        {
            return tabControl.SelectedContent as BrowserControl;
        }

        private void tabControl_TabItemAdded(object sender, Wpf.Controls.TabItemEventArgs e)
        {
            // Add an Icon to the tabItem
            BitmapImage image = new BitmapImage(new Uri("pack://application:,,,/Khaos;component/Images/logo.png"));
            Image img = new Image();
            img.Source = image;
            img.Width = 16;
            img.Height = 16;
            img.Margin = new Thickness(2, 0, 2, 0);

            e.TabItem.Icon = img;

            // wrap the header in a textblock, this gives us the  character ellipsis (...) when trimmed
            TextBlock tb = new TextBlock();
            tb.Text = "New Tab";
            tb.TextTrimming = TextTrimming.CharacterEllipsis;
            tb.TextWrapping = TextWrapping.NoWrap;

            e.TabItem.Header = tb;

            BrowserControl browser = new PhoenixEngine.BrowserControl();
            browser.Margin = new Thickness(2);
            browser.Name = "browser";
            browser.ShowHome();

            browser.Navigated += new EventHandler(browser_Navigated);
            browser.StatusChanged += new EventHandler(browser_StatusChanged);
            browser.TitleChanged += delegate
            {
                this.Dispatcher.Invoke(
                    
                    (Action)delegate
                    {
                        tb.Text = browser.DocumentTitle;
                    });
            };

            e.TabItem.Content = browser;
        }

        void browser_Navigated(object sender, EventArgs e)
        {
            if (!this.CheckAccess())
            {
                Dispatcher.Invoke(
                    
                    (Action)delegate
                    {
                        BrowserControl browser = GetCurrentBrowserControl();
                        if (sender == browser)
                        {
                            NavTextBox.Text = browser.Uri.ToString();
                        }
                    });
            }
            else
            {
                BrowserControl browser = GetCurrentBrowserControl();
                if (sender == browser)
                {
                    NavTextBox.Text = browser.Uri.ToString();
                }
            }
        }

        void browser_StatusChanged(object sender, EventArgs e)
        {
            if (!this.CheckAccess())
            {
                Dispatcher.Invoke(
                    
                    (Action)delegate
                {
                    BrowserControl browser = GetCurrentBrowserControl();
                    if (sender == browser)
                    {
                        status.Text = browser.Status;
                    }
                });
            }
            else
            {
                BrowserControl browser = GetCurrentBrowserControl();
                if (sender == browser)
                {
                    status.Text = browser.Status;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tabControl.AddTabItem();
        }

        #region // Commands

        private void AlwaysCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void NewWindowCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            new KhaosBrowser().Show();
            e.Handled = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            this.Close();
        }

        private void NewTab_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            tabControl.AddTabItem();
            e.Handled = true;
        }

        private void Tools_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GetCurrentBrowserControl().ShowTools();
            e.Handled = true;
        }

        private void About_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show(
                @"Khaos Browser
                Written in C# 3.5 WPF");
            //Daniel Little
            //Erik Poppe
            //Taka Uniya
            //Emily Poole
            //Robert Denney
            //Fredy Grandie
            e.Handled = true;
        }

        #endregion

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabControl.SelectedIndex > -1 && tabControl.Items.Count > tabControl.SelectedIndex)
            {
                BrowserControl browser = (tabControl.Items[tabControl.SelectedIndex] as TabItem).Content as BrowserControl;

                if (!this.CheckAccess())
                {
                    Dispatcher.Invoke(
                        
                        (Action)delegate
                        {
                            if (browser != null)
                                NavTextBox.Text = browser.Uri;
                        });
                }
                else
                {
                    if (browser != null)
                        NavTextBox.Text = browser.Uri;
                }

                if (browser != null)
                {
                    //browser.
                }
            }
        }

        private void BasicHistory_Click(object sender, RoutedEventArgs e)
        {
            History newHis = new History(myKhaosHistory, 1);
            newHis.Show();
        }

        private void AdvancedHistory_Click(object sender, RoutedEventArgs e)
        {
            History newHis = new History(myKhaosHistory, 0);
            newHis.Show();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            GetCurrentBrowserControl().NavigateBack();
        }

        private void btnForw_Click(object sender, RoutedEventArgs e)
        {
            GetCurrentBrowserControl().NavigateForward();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            GetCurrentBrowserControl().ShowHome();
        }

    }
}
