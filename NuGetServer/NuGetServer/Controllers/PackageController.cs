using NuGetServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace NuGetServer.Controllers
{
    public class PackageController : Controller
    {
        // GET: Package
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult PackageList()
        {
            var doc = new XmlDocument();
            string path = GetUrl("/nuget/Packages").ToString();
            doc.Load(path);
            var result = new XmlActionResult();
            result.Document = doc;
            result.TransformSource = Server.MapPath("~/Content/xsl/packages.xsl");
            return result;
        }


        private static Uri GetUrl(string relativePath)
        {
            if (System.Web.HttpContext.Current != null)
            {
                var uri = System.Web.HttpContext.Current.Request.Url;
                return new UriBuilder(uri.Scheme, uri.Host, uri.Port, relativePath).Uri;
            }

            var defaultUri = new Uri("http://localhost");
            return new UriBuilder(defaultUri.Scheme, defaultUri.Host, defaultUri.Port, relativePath).Uri;
        }
    }
}