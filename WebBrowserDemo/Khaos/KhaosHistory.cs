using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Khaos
{
    public class KhaosHistory
    {
        List<string>URLList = new List<string>();
        List<DateTime> timeList = new List<DateTime>();
        public void AddHistory(string URL)
        {
            URLList.Add(URL);
            timeList.Add(DateTime.Now);
        }
        public int Length()
        {
            return URLList.Count;
        }
        public string GetURL(int position)
        {
            return URLList[position];
        }
        public DateTime GetTime(int position)
        {
            return timeList[position];
        }
    }
}
