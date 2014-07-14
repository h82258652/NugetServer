using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;

namespace NuGetServer
{
    /// <summary>
    /// Summary description for UploadPackage
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UploadPackage : System.Web.Services.WebService
    {
        [WebMethod]
        public void Upload(byte[] data, string fileName)
        {
            var logFilePath = Server.MapPath("log.txt");
            try
            {
                //File.AppendAllText(logFilePath, DateTime.Now + " 收到请求" + Environment.NewLine);
                BinaryWriter binWriter = new BinaryWriter(File.Open(Path.Combine(Server.MapPath(
                    "~/Packages"), fileName), FileMode.Create, FileAccess.ReadWrite));
                binWriter.Write(data);
                binWriter.Close();
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFilePath, DateTime.Now + " 出错了" + fileName + " "  + ex.ToString() + Environment.NewLine);
            }
        }
    }
}
