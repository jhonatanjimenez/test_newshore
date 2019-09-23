using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prueba.Services
{
    /// <summary>
    /// service that saves and returns data from session variables
    /// </summary>
    public class SessionApp
    {
        /// <summary>
        /// function that returns the value of a session data
        /// </summary>
        /// <param name="key">the session variable to search for</param>
        /// <returns>value of the session variable found</returns>
        public static string GetVarSession(string key)
        {
            string value = (!String.IsNullOrEmpty(key.Trim()))? System.Web.HttpContext.Current.Session[key].ToString() : null;
            return (!String.IsNullOrEmpty(value.Trim())) ? value : null;
        }

        /// <summary>
        /// function that store a value in a session variable
        /// </summary>
        /// <param name="key">variable with which it will remain stored in the session</param>
        /// <param name="value">value to be assigned to the variable</param>
        public static void SetVarSession(string key, string value)
        {
            try
            {
                if (!String.IsNullOrEmpty(key.Trim()))
                    System.Web.HttpContext.Current.Session[key] = (!String.IsNullOrEmpty(value.Trim())) ? value : null;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not save session data from:"+ key + " = " + value);
            }
        }
    }
}