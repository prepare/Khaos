using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using PhoenixSettings;
using System.IO;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace PhoenixOI
{
    public class Request
    {

        WebRequest internalRequest;
        WebResponse internalResponse;

        static Request()
        {
            ProxyServer = "";
        }

        public static string ProxyServer { get; set; }
        public static int ProxyPort { get; set; }

        public WebResponse Response
        {
            get
            {
                return internalResponse;
            }
        }

        public void CreateWebRequest(string uri)
        {

            if (System.IO.File.Exists(uri))
            {
                try
                {
                    this.CreateFileRequest(new Uri(uri));
                }
                catch (ArgumentNullException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                catch (UriFormatException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            else
            {
                System.Globalization.CompareInfo cmpUrl = System.Globalization.CultureInfo.InvariantCulture.CompareInfo;
                if (cmpUrl.IsPrefix(uri, "http://") == false)
                {
                    uri = "http://" + uri;
                }
                Regex RgxUrl = new Regex("(([a-zA-Z][0-9a-zA-Z+\\-\\.]*:)?/{0,2}[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?(#[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?");
                if (RgxUrl.IsMatch(uri))
                {
                    try
                    {
                        this.CreateWebRequest(new Uri(uri));
                    }
                    catch (ArgumentNullException ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                    catch (UriFormatException ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            }
        }

        public void CreateFileRequest(Uri uri)
        {
            FileWebRequest iRequest = WebRequest.Create(uri) as FileWebRequest;
            internalRequest = iRequest;
        }

        public void CreateWebRequest(Uri uri)
        {
            this.CreateWebRequest(uri, null, "Get");
        }

        /// <summary>
        /// This method creates web request.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="collHeader">This parameter of type
        ///    NameValueCollection may contain any extra header
        ///    elements to be included in this request      </param>
        /// <param name="RequestMethod">Value can POST OR GET</param>
        /// <returns></returns>
        public void CreateWebRequest(string uri, NameValueCollection collHeader, string requestMethod)
        {
            this.CreateWebRequest(new Uri(uri), collHeader, requestMethod);
        }

        public void CreateWebRequest(Uri uri, NameValueCollection collHeader, string requestMethod)
        {
            HttpWebRequest iRequest = WebRequest.Create(uri) as HttpWebRequest;

            iRequest.UserAgent = PhoenixSettings.Phoenix.UserAgentContext.UserAgent;
            iRequest.Method = requestMethod;
            iRequest.KeepAlive = false;
            iRequest.Timeout = 60000;
           
            // Post KeyvaluePairs
            if (collHeader != null)
            {
                int iCount = collHeader.Count;
                string key, keyvalue;

                for (int i = 0; i < iCount; i++)
                {
                    key = collHeader.Keys[i];
                    keyvalue = collHeader[i];
                    iRequest.Headers.Add(key, keyvalue);
                }
            }

            iRequest.ContentType = "text/html";

            // Content types
            //"text/html";
            //"application/x-www-form-urlencoded";

            if (ProxyServer.Length > 0)
            {
                iRequest.Proxy = new
                 WebProxy(ProxyServer, ProxyPort);
            }
            iRequest.AllowAutoRedirect = true;


            //Remove collection elements
            if (collHeader != null)
                collHeader.Clear();

            internalRequest = iRequest;
        }

        public void GetResponse()
        {
            if (internalRequest != null)
            {
                try
                {
                    internalResponse = internalRequest.GetResponse() as WebResponse;
                }
                catch (WebException ex)
                {

                }
                catch (NotSupportedException ex)
                {

                }
                catch (ProtocolViolationException ex)
                {

                }
                catch (InvalidOperationException ex)
                {

                }

            }

            //response.ContentLength
            //response.ContentType
            //response.Cookies
            //response.CharacterSet
            //response.ContentEncoding
            //response.IsFromCache
            //response.Headers
            //response.Method
            //response.Server
            //response.StatusCode
            //response.StatusDescription
            //response.ProtocolVersion
            //response.GetResponseStream();

        }

        public Uri ResolveAddress(string address)
        {
            return new Uri(internalResponse.ResponseUri, address);
        }

        public Uri ResolveAddress(Uri address)
        {
            return new Uri(internalResponse.ResponseUri, address);
        }

        public static string GetText(WebResponse response, string uri)
        {
            // used to build entire input
            StringBuilder sb = new StringBuilder();

            // used on each read operation
            byte[] buf = new byte[8192];

            // we will read data via the response stream
            Stream resStream = response.GetResponseStream();

            string tempString = null;
            int count = 0;

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

        public static Stream StringToStream(string s)
        {
            return new MemoryStream(Encoding.Unicode.GetBytes(s));
        }

        public Request GetRelativeObject(string address)
        {
            Request req = new Request();
            req.CreateWebRequest( ResolveAddress(address) );
            return req;
        }
    }

}
