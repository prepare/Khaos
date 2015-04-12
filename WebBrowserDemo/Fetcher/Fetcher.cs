using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Fetching
{
    public static class Fetcher
    {

        public static WebResponse FetchResponce(string uri)
        {
            WebRequest request = HttpWebRequest.Create(uri);
            WebResponse response = request.GetResponse();
            return response;
        }

        public static string FetchHtml(string uri)
        {
            // used to build entire input
            StringBuilder sb = new StringBuilder();

            // used on each read operation
            byte[] buf = new byte[8192];


            WebResponse response = FetchResponce(uri);
            
            // we will read data via the response stream
	        Stream resStream = response.GetResponseStream();

	        string tempString = null;
	        int    count      = 0;

	        do
	        {
		        // fill the buffer with data
		        count = resStream.Read(buf, 0, buf.Length);

		        // make sure we read some data
		        if (count != 0)
		        {
			        // translate from bytes to ASCII text
			        tempString = Encoding.ASCII.GetString(buf, 0, count);

			        // continue building the string
			        sb.Append(tempString);
		        }
	        }
	        while (count > 0); // any more data to read?

	        // print out page source
	        return sb.ToString();

        }

        public static Stream FetchHtmlStream(string uri)
        {
            WebResponse response = FetchResponce(uri);

            // we will read data via the response stream
            Stream resStream = response.GetResponseStream();
            return resStream;

        }

        public static Stream StringToStream(string s)
        {
            return new MemoryStream(Encoding.Unicode.GetBytes(s));
        }
    }

}
