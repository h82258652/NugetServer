using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using NugetServer.Models;

namespace NugetServer.Controllers
{
    public class PackageController : Controller
    {
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

        public ActionResult Delete( string id )
        {
            string path = Path.Combine(Server.MapPath("~/Packages"), id);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                ViewBag.StatusMessage = new StatusMessage
                {
                    Success = true,
                    Message = String.Format("{0} was deleted successfully.", id)
                };
            }
            else
            {
                ViewBag.StatusMessage = new StatusMessage
                {
                    Success = false,
                    Message = String.Format("{0} does not exist.", id)
                };
            }

            return View("Index");
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
