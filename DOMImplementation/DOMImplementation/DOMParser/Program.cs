using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DOMCore
{
    public class Program
    {

        public static int Main()
        {
            DOMParser domp;
            List<TagInfo> objstream;
            string page2 = @"<html>
                                <head>
                                </head>
                                <body background='blabla.jpg' style='padding: 5px 5px 5px 5px;'>
                                    <div style='background-color: gray;'>
                                        <center>
                                            <div style='width: 80%; height: 300px; border-left: solid 5px green; border-right: solid 3px blue;'>
                                                Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text. Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text .
                                                <div style='top: 5px; left: 5px; width: 100px; height: 100px'>
                                                    Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text. 
                                                </div>
                                            </div>
                                        </center>
                                        <span style='background-color: green;'>
                                            Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text.
                                        </span>
                                        <span style='background-color: red; border-color: black; border-width: 3px; border-style: solid;'>
                                            Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text.
                                        </span>
                                        <div style='background-color: orange;'>
                                            Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text Text.
                                        </div>
                                    </div>
                                </body>
                            </html>";
            string page = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN'
    'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>

<html xmlns='http://www.w3.org/1999/xhtml' xml:lang='en' lang='en'>
<head>
<meta http-equiv='X-UA-Compatible' content='IE=EmulateIE7'/>

<title>Cuil</title>
<link rel='search' type='application/opensearchdescription+xml' title='Cuil' href='http://www.cuil.com/static/plugin.xml' />


<link rel='stylesheet' href='static/v2/css/modern.min.css?1239297820' type='text/css' media='all' />

<style type='text/css' media='screen'>
body {
    background-color: #000;
}

body.h {
    margin: 0;
    color: #484D4F;
    font-size: 8.5pt;
}

#cluster {
    position: absolute;
    width: 390px;
    text-align: right;
    left: 50%;
    top: 50%;
    margin-left: -320px;
    margin-top: -70px;
}

#hlogo {
    display: block;
    float: right;
    margin-bottom: 4px;
	display: inline;
}

#links {
	margin-top: 25px;
}
#links, #copy {
    text-align: right;
    line-height: 1.6em;
}
#hlogo, #links, #copy, #search_count {
    margin-right: 70px;
}

#links span.pipe {
    padding: 0 4px;
}

#search_count {
    padding-top: 6px;
    color: #c8cbce;
    clear: both;
}
</style>
<meta name='viewport' content='width=600' />
<!--[if IE]>
<style type='text/css' media='screen'>
    #search_count {
        padding-top: 3px;
    }
</style>
<![endif]-->

<!--[if lt IE 7]>
<style type='text/css' media='screen'>
    #search_count {
        padding-top: 0px;
    }
</style>
<![endif]-->

<script type='text/javascript' charset='utf-8'>
var gPopup = null;
var gUseProxy = false;
</script>
</head>

<body class='h'>

<div id='cluster'>
    <img id='hlogo' alt='Cuil' src='http://www.cuilimg.com/static/v2/images/new/flattened/cuil-home_id.png' />
    <form action='/search' method='get' id='form_h'>
        <input id='fld_q' class='search' name='q' type='text' maxlength='250' autocomplete='off' /> <button class='search' type='submit' title='Search'>Search</button>

    </form>
	
	<div id='advanced-description'>
		<img src='http://www.cuilimg.com/static/v2/images/popup_arrow.gif' />
		<div>
			<strong>Query by Category</strong>
			<p>Find pages that referrence concepts in a category. For example, use 'category: american-presidents' to search for pages that talk about Presidents of the USA.</p>
			<p><a href='http://www.cuil.com/info/features/'>More Information</a></p>

		</div>
	</div>
	
    <div id='search_count'>Search 124,426,951,803 web pages</div>
	
    <div id='links'>
        <a href='http://www.cuil.com/info/'>About Cuil</a>
        <span class='pipe'>|</span>
        <a href='http://www.cuil.com/info/privacy'>Your Privacy</a>

        <span id='span_add_ext' style='display:none'>
        <span class='pipe'>|</span>
        <a id='add_ext' href='#'>Add Cuil</a>
        </span>
    </div>
    <div id='copy'>
        &copy; 2009 Cuil, Inc.
    </div>

</div>

<script src='http://www.cuilimg.com/static/v2/js/mootools.min.js?1239297822' type='text/javascript'></script>
<script src='http://www.cuilimg.com/static/v2/js/ajax.min.js?1239297820' type='text/javascript'></script>

<script type='text/javascript' charset='utf-8'>
function getCategory(item) {
	return item.innerHTML.replace('<span class='hint'>Category: </span>', 'category:').replace(/\s/g, '-');
};

window.addEvent('load',function() {
    var form = document.forms[0];
    
    var fld = id('fld_q');
    initField(fld);
    fld.focus();
    
    form.onsubmit = function() {
        track(logstr('sub', { q: fld.value }));
        return true;
    };
	
    if (Cookie.get('suggest', '1')=='1') {
        gPopup = new Popup(fld);
        gPopup.itemPicked = function(item) {
            if (this.itemType(item) == 'nav') {
                doNav(item);
            } else {
				if (this.itemType(item) == 'cat') {
					fld.value = getCategory(item);
				} else {
					fld.value = item.innerText;
				}
				track(logstr('suggest', { q: fld.value }));
				form.submit();
			}
        };
    }
    showPlugin();
});
</script>
</body>
</html>

";

            //string page = "<html><head><title>Heading</title></head><body bgcolor=Red><a>Text</a></body></html>";

            MemoryStream memStream = new MemoryStream();
            byte[] data = Encoding.ASCII.GetBytes(page);
            memStream.Write(data, 0, data.Length);
            memStream.Position = 0;
            StreamReader sReader = new StreamReader(memStream);

            //Request request = new Request();

            //request.CreateWebRequest("www.cuil.com");
            //request.GetResponse(); 
            //StreamReader sReader = new StreamReader(request.Response.GetResponseStream());

            //parse html text to list of AttributeLists
            HtmlParser parse = new HtmlParser();
            objstream = parse.ParseHTML(sReader);

            printObjstream(objstream);

            //parse list of AttributeLists to HTMLDocument
            domp = new DOMParser(objstream, null);
            HTMLDocument d = domp.ParseDom();

            IElement e = d.DocumentElement;

            printNode(d,0);

            Console.Read();
            return 0;
        }

        public static void printObjstream(List<TagInfo> objstream)
        {
            foreach (TagInfo ti in objstream)
            {
                if (ti.Name == "#text")
                {
                    System.Console.WriteLine("#text:" + ti.Value);
                }
                else
                {
                    //print parsed information
                    System.Console.Write("<" + ti.Name);
                    for (int i = 0; i < ti.Attributes.Count; i++)
                    {
                        System.Console.Write(" " + ti.Attributes.GetKey(i) + "='" + ti.Attributes.Get(i) + "'");
                    }
                    System.Console.WriteLine(">");
                }
            }
        }

        public static void printNode(INode n, int lvl)
        {
            for (int i = 1; i < lvl; i++)
                System.Console.Write("-");
            if (n.NodeType == NodeType.TEXT_NODE)
                System.Console.WriteLine("#text:" + n.NodeValue);
            else
                System.Console.WriteLine("" + n.NodeName);
            for (int i = 0; i < n.ChildNodes.Length; i++)
                printNode(n.ChildNodes[i], lvl + 1);
        }
    }
}
