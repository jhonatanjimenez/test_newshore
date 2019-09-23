using prueba.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prueba.Controllers
{
    /// <summary>
    /// Home Controller principal page
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Response view init
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Function: saved files to send 
        /// </summary>
        /// <param name="fileCustomers">file customers</param>
        /// <param name="fileContent">File content</param>
        /// <returns>nueva vista</returns>
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase fileCustomers, HttpPostedFileBase fileContent)
        {
            /// check files not null or emptu 
            if (fileCustomers != null && fileCustomers.ContentLength > 0 && fileContent != null && fileContent.ContentLength > 0)
                try
                {
                    Files oFiles = new Files();
                    // saved file customers
                    oFiles.SaveFile(fileCustomers.FileName, fileCustomers);
                    SessionApp.SetVarSession("fileCustomers", fileCustomers.FileName);
                    /// saved file content
                    oFiles.SaveFile(fileContent.FileName, fileContent);
                    SessionApp.SetVarSession("fileContent", fileContent.FileName);
                    /// return message to page new
                    ViewBag.Message = "Ficheros cargagos";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR: al cargar los ficheros" ;
                    return RedirectToAction("Index");
                }
            else
            {
                ViewBag.Message = "No se han cargado los ficheros";
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}