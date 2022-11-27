using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Video_Upload_MVC.Controllers
{
    public class MvcUploadController : Controller
    {
        // GET: MvcUpload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase videoFile)
        {
            if (videoFile != null)
            {
                string fileName = Path.GetFileName(videoFile.FileName);
                if(videoFile.ContentLength < 104857600)
                {
                    videoFile.SaveAs(Server.MapPath($"~/VideoFiles/{fileName}"));
                }
            }

            return View();
        }
    }
}