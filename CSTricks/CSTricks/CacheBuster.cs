using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loyalty
{
    public static class CacheBuster
    {
        static public void Init(HttpRequest req)
        {
            local_path = req.PhysicalApplicationPath;
        }

        static string local_path;

        static string ticks_for_js_file(string script)
        {
            var f = new System.IO.FileInfo(String.Format("{1}js{2}{0}.js", script, local_path, System.IO.Path.DirectorySeparatorChar));
            return f.LastWriteTime.ToString("yyyy-MM-dd--HH-mm-ss");
        }

        static string ticks_for_css_file(string style)
        {
            var f = new System.IO.FileInfo(String.Format("{1}css{2}{0}.css", style, local_path, System.IO.Path.DirectorySeparatorChar));
            return f.LastWriteTime.ToString("yyyy-MM-dd--HH-mm-ss");
        }

        public static string cache_friendly_js(string script)
        {
            return String.Format("<script language=\"javascript\" type=\"text/javascript\" src=\"/js/{0}.js?t={1}\"></script>", script, ticks_for_js_file(script));
        }

        public static string cache_friendly_css(string style)
        {
            return String.Format("<link rel=\"stylesheet\" href=\"/css/{0}.css?t={1}\" type=\"text/css\" />", style, ticks_for_css_file(style));
        }
    }
}