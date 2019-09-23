using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Hosting;
using prueba.Models;

namespace prueba.Services
{

    /// <summary>
    /// service that interacts with files loaded or created in the app
    /// </summary>
    public class Files
    {
        /// <summary>
        /// property that saves, the path where the files will be stored
        /// </summary>
        public string PathRout = "App_Data";


        /// <summary>
        /// function that returns a path to save files
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>path of the path plus the file name</returns>
        public string GetPathFiles(string fileName) {

            PathRout = (!String.IsNullOrEmpty(PathRout.Trim())) ? PathRout.ToString() : "";

            string pathFile = HostingEnvironment.MapPath("~") + PathRout;
            if (!System.IO.Directory.Exists(pathFile))
            {
                System.IO.Directory.CreateDirectory(pathFile);
            }

            return pathFile + "\\" + fileName;
        }

        /// <summary>
        /// function to store files
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="file">File content</param>
        public void SaveFile(string fileName, HttpPostedFileBase file) {
            try
            {

                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }

                string pathFile = GetPathFiles(fileName);
                file.SaveAs(pathFile);
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem saving files ");
            }
        }

        /// <summary>
        /// function that creates a new file, with a list of clients
        /// </summary>
        /// <param name="customers">customer list</param>
        /// <param name="fileName">name of the file to be created</param>
        public  void SaveCustomers(List<CustomerModel> customers, string fileName)
        {

            try {
                StreamWriter fileCustomers = new StreamWriter(GetPathFiles(fileName), true);
                foreach (CustomerModel customer in customers) {
                    if (!String.IsNullOrEmpty(customer.Name)) {
                        fileCustomers.WriteLine(customer.Name);
                    }
                
                }            
                fileCustomers.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("There was an inconvenience to save the client results file ");
            }
        }

        /// <summary>
        /// function that reads the file that contains the customer data
        /// </summary>
        /// <param name="nameFile">File name</param>
        /// <returns>Customer list</returns>
        public List<CustomerModel> ReaderCustomers(string nameFile)
        {
            /// list of customers to be returned
            List<CustomerModel> customers = new List<CustomerModel>();
            try
            {
                /// line, which contains the data of each client
                string customer;

                /// Reader to customer file
                StreamReader fileCustomers = new StreamReader(GetPathFiles(nameFile));
                /// the file is iterated, for each of its lines
                while ((customer = fileCustomers.ReadLine()) != null)
                {
                    /// line is validated
                    if (!String.IsNullOrEmpty(customer.Trim()))
                    {
                        /// customer name is added to the list
                        customers.Add(new CustomerModel { Name = customer });
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem reading the stored client file");
            }
            
            return customers;
        }

        /// <summary>
        /// function that reads the file that contains the content data
        /// </summary>
        /// <param name="nameFile">File name</param>
        /// <returns>content character list</returns>
        public List<char> ReaderContent(string nameFile)
        {
            /// content character list
            List<char> content = new List<char>();
            try
            {
                /// line, which contains the data of each character of the content
                string letter;

                /// the content file is read
                StreamReader fileCustomers = new StreamReader(GetPathFiles(nameFile));
                /// the file is iterated, for each of its lines
                while ((letter = fileCustomers.ReadLine()) != null)
                {
                    /// se valida la linea 
                    if (!String.IsNullOrEmpty(letter.Trim()))
                    {
                        /// the character is added to the content character list
                        content.Add(Convert.ToChar(letter.Trim()));
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem reading the content file");
            }

            return content;
        }
    }
}