using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows;

namespace CascadingStyleSheets
{
    public static class Logger
    {

        public static void RecordError(string message)
        {
            MessageBox.Show(message);// + " : " + trace.ToString());
        }

        public static void RecordError(Exception exception)
        {
            MessageBox.Show(exception.Message);
        }

        public static void RecordWarning(string message)
        {
            Debug.WriteLine(message);// + " : " + trace.ToString());
        }

    }
}
