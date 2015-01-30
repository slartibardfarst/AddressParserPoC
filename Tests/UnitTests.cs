using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressCommon.DataStructures;
using NewAddressParserPoC;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Tests
{
    public class UnitTests
    {


        [TestCase("123 5th Street,Blaine,WA,98230", "{ address_line: '123 5th Street', street_no: '123', street_direction: null, street: '5th',  street_suffix: 'Street', street_post_direction: null, city: 'Blaine', state: 'WA', zip: '98230', unit: null }")]
        public void Test1(string addressString, string expectedAddressObjAsString)
        {
            var ap = new AddressParser3();

            var parsed = ap.ParseAddress(addressString);

            var jExp = JObject.Parse(expectedAddressObjAsString);
            var expected = jExp.ToObject<Address>();

            AssertAddressAreEqual(expected, parsed);
        }

        [TestCase("2070 Pacific Ave 502, San Francisco, CA, 94109",  "{ street_no: '2070', street_direction: null, street: 'Pacific', street_suffix: 'Ave', city: 'San Francisco', state: 'CA', zip: '94109', unit: '502' }")]
        [TestCase("601 Van Ness 1006, San Francisco, CA, 94102",     "{ street_no: '601', street_direction: null, street: 'Van Ness', street_suffix: null, city: 'San Francisco', state: 'CA', zip: '94102', unit: '1006' }")]
        [TestCase("2981 Country Manor Ln 123, Las Vegas, NV, 89115", "{ street_no: '2981', street_direction: null, street: 'Country Manor', street_suffix: 'Ln', city: 'Las Vegas', state: 'NV', zip: '89115', unit: '123' }")]
        //[TestCase("2070 Pacific Ave502, San Francisco, CA, 94109", "{ address_line: '2070 Pacific Ave # 502', street_no: '2070', street_direction: null, street: 'Pacific', street_suffix: 'Ave', city: 'San Francisco', state: 'CA', zip: '94109', unit: '# 502' }")]
        //[TestCase("1461 Alice St108, Oakland, CA, 94612", "{ address_line: '1461 Alice St # 108', street_no: '1461', street_direction: null, street: 'Alice', street_suffix: 'St', city: 'Oakland', state: 'CA', zip: '94612', unit: '# 108' }")]
        [TestCase("3401  Hidden Oak 11,Sacramento,CA,95821",          "{ state: 'CA', city: 'Sacramento', zip: '95821', street_no: '3401', street: 'Hidden Oak', street_suffix: null, unit: '11', }")]
        [TestCase("1718 SE 11th,Camas,WA,98607", "{ address_line: '1718 SE 11th', state: 'WA', city: 'Camas', zip: '98607', street_no: '1718', street_direction: 'SE', street: '11th', street_suffix: null}")]
        //[TestCase("2981 Country Manor Ln123, Las Vegas, NV, 89115", "{ address_line: '2981 Country Manor Ln # 123', street_no: '2981', street_direction: null, street: 'Country Manor', street_suffix: 'Ln', city: 'Las Vegas', state: 'NV', zip: '89115', unit: '# 123' }")]
        //[TestCase("2901 PALO VERDE LN#20, Yuma, AZ, 85365", "{ address_line: '2901 Palo Verde Ln # 20', street_no: '2901', street_direction: null, street: 'Palo Verde', street_suffix: 'Ln', city: 'Yuma', state: 'AZ', zip: '85365', unit: '# 20' }")]
        //[TestCase("601 Van Ness1006, San Francisco, CA, 94102", "{ address_line: '601 Van Ness # 1006', street_no: '601', street_direction: null, street: 'Van Ness', street_suffix: null, city: 'San Francisco', state: 'CA', zip: '94102', unit: '# 1006' }")]
        //[TestCase("296 E 38TH ST5F,NEW YORK,NY,11203", "{ address_line: '296 E 38th St # 5F', street_no: '296', street_direction: 'E', street: '38th', street_suffix: 'St', city: 'New York', state: 'NY', zip: '11203', unit: '# 5F' }")]
        public void VerifyInternalParser_SDSWONE_897(string addressString, string expectedAddressObjAsString)
        {
            var ap = new AddressParser3();

            var parsed = ap.ParseAddress(addressString);

            var jExp = JObject.Parse(expectedAddressObjAsString);
            var expected = jExp.ToObject<Address>();

            AssertAddressAreEqual(expected, parsed);
        }


        private static void AssertAddressAreEqual(Address expected, AddressEx result)
        {
            Assert.NotNull(result);

            Assert.AreEqual(expected.unit, result.unit, "unit");
            Assert.AreEqual(expected.city, result.city, "city");
            Assert.AreEqual(expected.state, result.state, "state");
            Assert.AreEqual(expected.street, result.street, "street name");
            Assert.AreEqual(expected.street_direction, result.street_direction, "street_direction");
            Assert.AreEqual(expected.street_no, result.street_no, "street_no");
            Assert.AreEqual(expected.street_post_direction, result.street_post_direction, "street_post_direction");
            Assert.AreEqual(expected.street_suffix, result.street_suffix, "street_suffix");
            Assert.AreEqual(expected.zip, result.zip, "zip");
        }
    }
}
