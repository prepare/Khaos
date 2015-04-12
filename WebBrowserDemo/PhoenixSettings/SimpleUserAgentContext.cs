using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PhoenixSettings
{
    /**
     * Simple implementation of {@link org.lobobrowser.html.UserAgentContext}. 
     * This class is provided for user convenience. 
     * Usually this class should be extended in order to provide appropriate
     * user agent information and more robust content loading routines. 
     * Its setters can be called to modify certain user agent defaults.
     */
    public class SimpleUserAgentContext : UserAgentContext
    {

        private static List<string> mediaNames = new List<string>();

        static SimpleUserAgentContext()
        {
            // Media names claimed by this context.
            mediaNames.Add("screen");
            mediaNames.Add("tv");
            mediaNames.Add("tty");
            mediaNames.Add("all");
        }

        /// <summary>
        /// This implementation returns true for certain media names,
        /// such as <code>screen</code>.
        /// </summary>
        public bool IsMedia(string mediaName)
        {
            return mediaNames.Contains(mediaName.ToLower());
        }

        private bool externalCSSEnabled = false;
        private bool scriptingEnabled = false;

        private string appMinorVersion = "0";
        private string appVersion = "1";

        private string vendor = "Extreme Solutions";
        private string appCodeName = "Phoenix";
        private string appName = "Phoenix";
        private string product = "Phoenix Web Browser";

        protected string userAgent = "Phoenix 1.0";


        /// <summary>
        /// Returns the application "code name." This implementation
        /// returns the value of a local field.
        /// </summary>
        public string AppCodeName
        {
            get
            {
                return this.appCodeName;
            }
            set
            {
                this.appCodeName = value;
            }
        }

        /// <summary>
        /// Gets the "minor version" of the application. This implementation
        /// returns the value of a local field.</summary>
        public string AppMinorVersion
        {
            get
            {
                return this.appMinorVersion;
            }
            set
            {
                this.appMinorVersion = value;
            }
        }

        /// <summary>
        /// Gets the application name. This implementation returns
        /// the value of a local field.
        /// </summary>
        public string AppName
        {
            get
            {
                return this.appName;
            }
            set
            {
                this.appName = value;
            }
        }

        /// <summary>
        /// Gets the major application version. This implementation
        /// returns the value of a local field.
        /// </summary>
        public string AppVersion
        {
            get
            {
                return this.appVersion;
            }
            set
            {
                this.appVersion = value;
            }
        }

        /// <summary>
        /// Get the browser language. This implementation returns
        /// the language of the default locale.
        /// </summary>
        public virtual string BrowserLanguage
        {
            get
            {
                return System.Globalization.CultureInfo.CurrentCulture.DisplayName;
            }
        }

        /// <summary>
        /// Returns the current Platform
        /// </summary>
        public string Platform
        {
            get
            {
                return Environment.OSVersion.Platform.ToString();
            }
        }

        /// <summary>
        /// Gets the User-Agent string. This implementation returns
        /// the value of a local field.
        /// </summary>
        public string UserAgent
        {
            get
            {
                return this.userAgent;
            }
            set
            {
                this.userAgent = value;
            }
        }

        /// <summary>
        /// Returns true if cookies are supported
        /// </summary>
        public bool CookiesEnabled
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a cookie, if they are not enabled it returns an empty string
        /// </summary>
        public string GetCookie(Uri uri)
        {
            return "";
        }

        /// <summary>
        /// Determines whether scripting should be enabled.
        /// </summary>
        public bool ScriptingEnabled
        {
            get
            {
                return this.scriptingEnabled;
            }
            set
            {
                this.scriptingEnabled = value;
            }
        }


        /// <summary>
        /// This method uses the default CookieHandler, if one is available,
        /// to set a cookie value.
        /// </summary>
        public void SetCookie(Uri uri, string cookieSpec)
        {

        }

        /// <summary>
        /// Gets or Sets the vendor
        /// </summary>
        public string Vendor
        {
            get
            {
                return this.vendor;
            }
            set
            {
                this.vendor = value;
            }
        }

        /// <summary>
        /// Gets or sets the product
        /// </summary>
        public string Product
        {
            get
            {
                return this.product;
            }
            set
            {
                this.product = value;
            }
        }

        /// <summary>
        /// Determines whether external CSS loading should be enabled.
        /// </summary>
        public bool ExternalCSSEnabled
        {
            get
            {
                return this.externalCSSEnabled;
            }
            set
            {
                this.externalCSSEnabled = value;
            }
        }

    }
}
