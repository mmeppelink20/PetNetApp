using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataObjects
{
    public static class ValidationHelpers
    {
        /// <summary>
        /// Updated by: Barry Mikulas
        /// 2023-03-04
        /// Changed regex for zipcode to allow only 5 digits due to all zip coe data only being 5 digits
        /// </summary>
        public static Regex ZipcodeRegex { get; private set; } = new Regex(@"^(\d{5})$");
        public static Regex PhoneRegex { get; private set; } = new Regex(@"^\d{10,13}$");
        public static Regex AmountRegex { get; private set; } = new Regex(@"^(([1-9]\d{0,4})|0)(\.\d{1,2})?$");
        public static Regex EmailRegex { get; private set; } = new Regex(@"^(?=^.{1,64}@)[a-zA-Z0-9]+([-_\.]?[a-zA-Z0-9]+)*@[a-zA-Z0-9]+(-?[a-zA-Z0-9]+)*\.[a-zA-Z0-9]{2,}([-\.]?[a-zA-Z0-9]{2,})*$");
        public static Regex FirstNameRegex { get; private set; } = new Regex(@"^[a-zA-Z][a-zA-Z0-9\-\. ']*$");
        public static Regex LastNameRegex { get; private set; } = new Regex(@"^[a-zA-Z]([a-zA-Z\.\- '][a-zA-Z0-9\.\- ']*)?$");

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/15
        /// 
        /// Confirms name is less than 50 characters and meets the guidelines from https://fsawebenroll.ed.gov/RoboHelp/Last_Name.htm
        /// </summary>
        /// <returns>Whether the string is a valid last name</returns>
        public static bool IsValidLastName(this string name)
        {
            return name != null && name.Length <= 50 && LastNameRegex.IsMatch(name);
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/15
        /// 
        /// Confirms name is less than 50 characters and meets the guidelines from https://fsawebenroll.ed.gov/RoboHelp/First_Name.htm
        /// </summary>
        /// <returns>Whether the string is a valid first name</returns>
        public static bool IsValidFirstName(this string name)
        {
            return name != null && name.Length <= 50 && FirstNameRegex.IsMatch(name);
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/15
        /// 
        /// Confirms description is less than 250 characters, can be null and empty
        /// </summary>
        /// <returns>Whether the string is a valid short description</returns>
        public static bool IsValidShortDescription(this string description)
        {
            return description == null || description.Length <= 250;
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/15
        /// 
        /// Confirms description is less than 250 characters, must be at least 5 characters long
        /// </summary>
        /// <returns>Whether the string is a valid short description</returns>
        public static bool IsValidRequiredShortDescription(this string description)
        {
            return description != null && description.Length >= 5 && description.Length <= 250;
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/15
        /// 
        /// Confirms description is less than 500 characters, can be null and empty
        /// </summary>
        /// <returns>Whether the string is a valid long description</returns>
        public static bool IsValidLongDescription(this string description)
        {
            return description == null || description.Length <= 500;
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/15
        /// 
        /// Confirms description is less than 500 characters, must be at least 5 characters long
        /// </summary>
        /// <returns>Whether the string is a valid long description</returns>
        public static bool IsValidRequiredLongDescription(this string description)
        {
            return description != null && description.Length >= 5 && description.Length <= 500;
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/15
        /// 
        /// Confirms address is less than 50 characters, cannot be null and empty
        /// </summary>
        /// <returns>Whether the string is a valid address</returns>
        public static bool IsValidAddress(this string address)
        {
            return address != "" && address != null && address.Length <= 50;
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/15
        /// 
        /// Confirms address is less than 50 characters, can be null and empty
        /// </summary>
        /// <returns>Whether the string is a valid address</returns>
        public static bool IsValidAddress2(this string address2)
        {
            return address2 == null || address2.Length <= 50;
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/15
        /// 
        /// Confirms zipcode is 5 digits
        /// </summary>
        /// Updated by Barry Mikulas 2023-03-04    changed regex to only allow 5 digits
        /// <returns>Whether the string is a valid zipcode</returns>
        public static bool IsValidZipcode(this string zipcode)
        {
            return zipcode != null && ZipcodeRegex.IsMatch(zipcode);
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/15
        /// 
        /// Confirms email matches https://help.xmatters.com/ondemand/trial/valid_email_format.htm
        /// </summary>
        /// <returns>Whether the string is a valid email</returns>
        public static bool IsValidEmail(this string email)
        {
            return email != null && EmailRegex.IsMatch(email) && email.Length <= 254;
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/15
        /// 
        /// Confirms phone number is 10-13 digits
        /// </summary>
        /// <returns>Whether the string is a valid phone number</returns>
        public static bool IsValidPhone(this string phone)
        {
            return phone != null && PhoneRegex.IsMatch(phone);
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/15
        /// 
        /// Confirms amount entered is 5 digits with up to 2 optional digits of precision
        /// </summary>
        /// <returns>Whether the string is a valid amount</returns>
        public static bool IsValidAmount(this string amount)
        {
            return amount != null && AmountRegex.IsMatch(amount);
        }
        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// 
        /// Confirms shelter name is not null, blank, or greater than the nvarchar(50) limit
        /// </summary>
        /// <returns>Whether the string is a valid shelter name</returns>
        public static bool IsValidShelterName(this string shelterName)
        {
            return shelterName != "" && shelterName != null && shelterName.Length <= 50;
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/03/02
        /// 
        /// Confirms the text is between 5 and 50 characters inclusive
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool IsValidTitle(this string title)
        {
            return title != null && title.Length >= 5 && title.Length <= 50;
        }
        /// <summary>
        /// Brian Collum
        /// Created: 2023/03/09
        /// Validator for a Generic NVarchar of length X
        /// Confirms that the supplied string is not null, empty, and is shorter than the supplied character limit
        /// </summary>
        /// <returns>Whether the string is a valid shelter name</returns>
        public static bool IsValidNVarcharX(this string targetString, int characterLimit)
        {
            return targetString != "" && targetString != null && targetString.Length <= characterLimit;
        }
    }
}