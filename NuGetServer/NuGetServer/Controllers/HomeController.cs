using NuGetServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NuGetServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (upload == null)
            {
                ModelState.AddModelError("upload", "Please select a file");
            }
            else
            {
                string extension = Path.GetExtension(upload.FileName);
                if (!(extension ?? "").Equals(".nupkg", StringComparison.CurrentCultureIgnoreCase))
                {
                    ModelState.AddModelError("upload", "Invalid extension. Only .nupkg files will be accepted.");
                }
            }

            if (ModelState.IsValid)
            {
                var path = Path.Combine(Server.MapPath("~/Packages"), upload.FileName);
                upload.SaveAs(path);
                ViewBag.StatusMessage = new StatusMessage
                {
                    Success = true,
                    Message = String.Format("{0} was uploaded successfully.", upload.FileName)
                };
            }

            return View("Index");
        }        
    }
}