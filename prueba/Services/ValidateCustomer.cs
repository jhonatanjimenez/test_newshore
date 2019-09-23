using prueba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prueba.Services
{

    /// <summary>
    /// service that validates customers
    /// </summary>
    public class ValidateCustomer
    {
        /// <summary>
        /// variable to indicate which letter will be changed, when a match is found in the content.
        /// </summary>
        public char EscapeLetter = '#';

        /// <summary>
        /// function that validates the client list with the content character list
        /// </summary>
        /// <param name="customers">Customer list</param>
        /// <param name="content">character list of the conente</param>
        /// <returns>customer list, with validation modifications</returns>
        public List<CustomerModel> Validate(List<CustomerModel> customers, List<char> content)
        {
            if (customers == null && content == null) {
                throw new Exception("Unable to validate files");
            }

            try {
                /// the customer collection is iterated
                foreach (CustomerModel customer in customers)
                {
                    /// the name of each client is created an array of characters
                    char[] customerLetters = (!String.IsNullOrEmpty(customer.Name.Trim())) ? customer.Name.Trim().ToCharArray() : new char[0];
                    /// the length of the character array is taken
                    int customerLettersLength = customerLetters.Length;
                    /// a flag is created to validate that the client meets the requirement
                    bool validate = (customerLettersLength > 0) ? true : false;

                    /// the name of the client is iterated for each of the characters
                    for (var letter = 0; letter < customerLettersLength; letter++)
                    {
                        /// if the client character is in the list of content characters, the index is taken
                        int indexLetter = (customerLetters[letter] != this.EscapeLetter) ? content.IndexOf(customerLetters[letter]) : -1;
                        /// if the position is greater than -1, it is because a match was found, otherwise the flag is deactivated and the iteration is terminated
                        if (indexLetter > -1)
                        {
                            content[indexLetter] = this.EscapeLetter;
                        }
                        else
                        {
                            validate = false;
                            letter = customerLettersLength;
                        }
                    }
                    /// if the flag is active, a string is added to the client’s name
                    customer.Name += (validate) ? " - if it exists" : " - does not exist";

                }
            }
            catch (Exception ex) {
                throw new Exception("Unable to validate files");
            }           

            return customers;
        }
    }
}