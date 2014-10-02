using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAddressParserPoC
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = string.Format("{0}{1}", null, "not null");
            //AddressParser parser = new AddressParser();

            //Play play = new Play();
            //play.Go();

            //AddressParser2 ap2 = new AddressParser2();

            AddressParser3 ap3 = new AddressParser3();
            ap3.Go();
        }
    }


}
