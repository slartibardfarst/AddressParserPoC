using AddressCommon.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAddressParserPoC
{
    public class AddressEx : Address, IAddress
    {
        private string _unitDescriptor;
        private string _unitValue;


        /// <summary>
        /// The address's name field is used for building names or community names, etc
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// The cross address, often provided as a cross street 
        /// </summary>
        public Address cross { get; set; }

        /// <summary>
        /// The underlying 'unit' fields is updated whenever we update unit descriptor or unit value
        /// </summary>
        public string unitDescriptor
        {
            get
            {
                //if _unitDescriptor not set, but underlying unit is set, then parse descriptor from unit string
                if ((null == _unitDescriptor) && !string.IsNullOrEmpty(unit))
                {
                    string unused;
                    SimpleUnitSplitter(base.unit, out _unitDescriptor, out unused);
                }

                return _unitDescriptor;
            }

            set
            {
                _unitDescriptor = value;
                SetUnderlyingUnitString();
            }
        }

        /// <summary>
        /// The underlying 'unit' fields is updated whenever we update unit descriptor or unit value
        /// </summary>
        public string unitValue
        {
            get
            {
                //if _unitDescriptor not set, but underlying unit is set, then parse descriptor from unit string
                if ((null == _unitValue) && !string.IsNullOrEmpty(unit))
                {
                    string unused;
                    SimpleUnitSplitter(base.unit, out unused, out _unitValue);
                }

                return _unitValue;
            }

            set
            {
                _unitValue = value;
                SetUnderlyingUnitString();
            }
        }

        public override string unit
        {
            get
            {
                return base.unit;
            }

            set
            {
                SimpleUnitSplitter(value, out _unitDescriptor, out _unitValue);
                SetUnderlyingUnitString();
            }
        }

        public AddressEx()
            : base()
        { }

        public AddressEx(Address src)
            : base(src)
        { }



        public AddressEx(AddressEx src)
            : base(src)
        {
            //specific to this derived class
            name = src.name;
            cross = (null != src.cross) ? (Address)src.cross.Clone() : null;
            unitDescriptor = src.unitDescriptor;
            unitValue = src.unitValue;
        }

        public override Address Clone()
        {
            var result = new AddressEx(this) as Address;

            return result;
        }

        private void SimpleUnitSplitter(string unitStr, out string desc, out string val)
        {
            if (string.IsNullOrEmpty(unitStr))
            {
                desc = null;
                val = null;
                return;
            }

            var els = unitStr.Split(new char[] { ' ' });
            if ((els.Length == 0) || (els.Length == 1))
            {
                desc = null;
                val = unitStr;
            }
            else if (els.Length == 2)
            {
                desc = els[0].Trim();
                val = els[1].Trim();
            }
            else
            {
                desc = els[0].Trim();
                val = string.Join(" ", els.Skip(1)).Trim();
            }

            if (string.IsNullOrEmpty(desc))
                desc = null;

            if (string.IsNullOrEmpty(val))
                val = null;
        }

        private void SetUnderlyingUnitString()
        {
            base.unit = string.Join(" ", _unitDescriptor ?? "", _unitValue ?? "").Trim();
            if (string.IsNullOrEmpty(base.unit))
                base.unit = null;
        }

        public string GetFormattedFullAddressString()
        {
            var addressParts = new string[] { CleanseEl(address_line), CleanseEl(city), CleanseEl(state), CleanseEl(zip) }
                .Where(x => !string.IsNullOrEmpty(x));

            string result = string.Join(",", addressParts);
            return result;
        }

        public void RebuildAddressLine(bool includeCrossStreet = false)
        {
            string addressLineToSet = null;
            string crossStreet = null;

            //prepare cross street part of the address line (if required)
            if (includeCrossStreet && (null != cross))
                crossStreet = BuildAddressLine(cross);

            addressLineToSet = BuildAddressLine(this);

            //updated the current address_line value in this AddressEx object
            if (includeCrossStreet && !string.IsNullOrEmpty(crossStreet))
                this.address_line = string.Join(" and ", CleanseEl(addressLineToSet), CleanseEl(crossStreet));
            else
                this.address_line = CleanseEl(addressLineToSet);
        }

        private static string BuildAddressLine(Address address)
        {
            //build the address line from the components (or if components are missing, use existing address_line value)
            if (string.IsNullOrEmpty(address.street) && string.IsNullOrEmpty(address.street_no) && !string.IsNullOrEmpty(address.address_line))
                return address.address_line ?? "";

            return BuildStreetAddress(address);
        }

        private static string BuildStreetAddress(Address address)
        {
            var addressParts = new string[]
            { 
                CleanseEl(address.street_no),
                CleanseEl(address.street_direction),
                CleanseEl(address.street),
                CleanseEl(address.street_suffix),
                CleanseEl(address.street_post_direction),
                CleanseEl(address.unit)
            }.Where(x => !string.IsNullOrEmpty(x));
            return string.Join(" ", addressParts).Trim();
        }


        private static string CleanseEl(string el)
        {
            return string.IsNullOrEmpty(el) ? "" : el.Trim();
        }
    }
}
