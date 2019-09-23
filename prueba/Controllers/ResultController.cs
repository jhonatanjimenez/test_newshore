using prueba.Models;
using prueba.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace prueba.Controllers
{
    /// <summary>
    /// Controller result customers
    /// </summary>
    public class ResultController : Controller
    {
        
        public ActionResult Index()
        {
            try
            {
                Files oFiles = new Files();
                ValidateCustomer oCustomers = new ValidateCustomer();
                List<CustomerModel> customers = new List<CustomerModel>();

                /// check customers data
                customers = oCustomers.Validate(oFiles.ReaderCustomers(SessionApp.GetVarSession("fileCustomers")), oFiles.ReaderContent(SessionApp.GetVarSession("fileContent")));
                /// file name save to session
                SessionApp.SetVarSession("fileResult", "Resultados.txt");
                /// saved to results
                oFiles.SaveCustomers(customers, SessionApp.GetVarSession("fileResult"));

                ViewBag.Message = "Check Customers";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "ERROR: There was an error to validate the files";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        /// <summary>
        /// function to return the download file
        /// </summary>
        /// <returns></returns>
        public FileResult Download()
        {
            Files oFiles = new Files();
            /// The validated customer file is sent to the user
            return File(oFiles.GetPathFiles(SessionApp.GetVarSession("fileResult")), "text/plain", SessionApp.GetVarSession("fileResult"));
        }
    }
}