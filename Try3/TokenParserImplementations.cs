using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NewAddressParserPoC.Try3
{
    public static class TokenParserImplementations
    {
        public static bool StreetNumber_1(string token, AddressEx acc)
        {
            if (Regex.IsMatch(token, @"^\d+$"))
            {
                acc.street_no = token;
                return true;
            }

            return false;
        }

        public static bool Street_1(string token, AddressEx acc)
        {
            if (Regex.IsMatch(token, @"^\w+$"))
            {
                acc.street = token;
                return true;
            }

            return false;
        }


        public static bool Suffix_1(string token, AddressEx acc)
        {
            if (Regex.IsMatch(token, @"Street|St|Avenue|Ave", RegexOptions.IgnoreCase))
            {
                acc.street_suffix = token;
                return true;
            }

            return false;
        }


        public static bool City_1(string token, AddressEx acc)
        {
            if (Regex.IsMatch(token, @"^\w+$"))
            {
                acc.city = token;
                return true;
            }

            return false;
        }

        public static bool City_2(string token1, string token2, AddressEx acc)
        {
            if (Regex.IsMatch(token1, @"^\w+$") && Regex.IsMatch(token2, @"^\w+$"))
            {
                acc.city = token1 + " " + token2;
                return true;
            }

            return false;
        }

        public static bool State_1(string token, AddressEx acc)
        {
            if (Regex.IsMatch(token, @"^\w\w$"))
            {
                acc.state = token;
                return true;
            }

            return false;
        }


        public static bool Zip_1(string token, AddressEx acc)
        {
            if (Regex.IsMatch(token, @"^\d\d\d\d\d$"))
            {
                acc.zip = token;
                return true;
            }

            return false;
        }


    }
}
