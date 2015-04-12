using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoenixSettings
{
    /**
     * Provides information about the user agent (browser) driving
     * the parser and/or renderer.
     * @see HtmlRendererContext#getUserAgentContext()
     * @see org.lobobrowser.html.parser.DocumentBuilderImpl#DocumentBuilderImpl(UserAgentContext)
     */
    public interface UserAgentContext
    {

        ///// <summary>
        ///// Creates an instance of HttpRequest which
        ///// can be used by the renderer to load images, scripts, external style sheets, 
        ///// and implement the Javascript XMLHttpRequest class (AJAX). 
        ///// </summary>
        //HttpRequest CreateHttpRequest();

        /// <summary>
        /// Gets browser "code" name.
        /// </summary>
        string AppCodeName
        { get; set; }

        /// <summary>
        /// Gets browser application name.
        /// </summary>
        string AppName
        { get; set; }

        /// <summary>
        /// Gets browser application version.
        /// </summary>
        string AppVersion
        { get; set; }

        /// <summary>
        /// Gets browser application minor version.
        /// </summary>
        string AppMinorVersion
        { get; set; }
        
        /// <summary>
        /// Gets browser language code. See http://en.wikipedia.org/wiki/List_of_ISO_639-1_codes ISO 639-1 codes. (eg: eng)
        /// </summary>
        string BrowserLanguage
        { get; }

        /// <summary>
        /// Returns a boolean value indicating whether cookies are
        /// enabled in the user agent. This value is used for reporting
        /// purposes only.
        /// </summary>
        bool CookiesEnabled
        { get; }

        /// <summary>
        /// Returns a boolean value indicating whether scripting
        /// is enabled in the user agent. If this value is <code>false</code>,
        /// the parser will not process scripts and Javascript element
        /// attributes will have no effect.
        /// </summary>
        bool ScriptingEnabled
        { get; set; }

        /// <summary>
        /// Returns a boolean value indicating whether remote 
        /// (non-inline) CSS documents should be loaded.
        /// </summary>
        bool ExternalCSSEnabled
        { get; set; }

        /// <summary>
        /// Gets the name of the user's operating system.
        /// </summary>
        string Platform
        { get; }

        /// <summary>
        /// Should return the string used in the User-Agent header. 
        /// </summary>
        string UserAgent
        { get; set; }


        /// <summary>
        /// Method used to implement Javascript <code>document.cookie</code> property.
        /// </summary>
        /// <param name="uri">Maker Uri</param>
        string GetCookie(Uri uri);

        /// <summary>
        /// Method used to implement <code>HtmlDocument.Cookie</code> property.
        /// </summary>
        /// <param name="uri">Maker Uri</param>
        void SetCookie(Uri uri, string cookieSpec);

        /// <summary>
        /// Media name, which may be <code>screen</code>, <code>tty</code>, etc. 
        /// (See http://www.w3.org/TR/REC-html40/types.html#type-media-descriptors">HTML Specification).
        /// </summary>
        /// <param name="mediaName"></param>
        /// <returns>Returns true if the current media matches the name provided.</returns>
        bool IsMedia(string mediaName);

        /// <summary>
        /// Name of the application Vender
        /// </summary>
        string Vendor
        { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        string Product
        { get; set; }
    }
}
