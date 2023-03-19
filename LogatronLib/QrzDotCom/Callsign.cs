using System.Xml.Serialization;

namespace Logatron.QrzDotCom
{
    public class Callsign
    {
        /// <summary>
        /// callsign
        /// </summary>
        [XmlElement(ElementName = "call")]
        public string? Call { get; set; }

        /// <summary>
        /// Cross reference: the query callsign that returned this record
        /// </summary>
        [XmlElement(ElementName = "xref")]
        public string? Xref { get; set; }

        /// <summary>
        /// Other callsigns that resolve to this record
        /// </summary>
        [XmlElement(ElementName = "aliases")]
        public string? Aliases { get; set; }

        /// <summary>
        /// DXCC entity ID (country code) for the callsign
        /// </summary>
        [XmlElement(ElementName = "dxcc")]
        public uint? DXCC { get; set; }

        /// <summary>
        /// first name
        /// </summary>
        [XmlElement(ElementName = "fname")]
        public string? FirstName { get; set; }

        /// <summary>
        /// last name
        /// </summary>
        [XmlElement(ElementName = "name")]
        public string? LastName { get; set; }

        /// <summary>
        /// address line 1 (i.e. house # and street)
        /// </summary>
        [XmlElement(ElementName = "addr1")]
        public string? Address1 { get; set; }

        /// <summary>
        /// address line 2 (i.e, city name)
        /// </summary>
        [XmlElement(ElementName = "addr2")]
        public string? Address2 { get; set; }

        /// <summary>
        /// state (USA Only)
        /// </summary>
        [XmlElement(ElementName = "state")]
        public string? State { get; set; }

        /// <summary>
        /// Zip/postal code
        /// </summary>
        [XmlElement(ElementName = "zip")]
        public uint? Zip { get; set; }

        /// <summary>
        /// country name for the QSL mailing address
        /// </summary>
        [XmlElement(ElementName = "country")]
        public string? Country { get; set; }

        /// <summary>
        /// dxcc entity code for the mailing address country
        /// </summary>
        [XmlElement(ElementName = "ccode")]
        public uint? CountryCode { get; set; }

        /// <summary>
        /// lattitude of address (signed decimal) S < 0 > N
        /// </summary>
        [XmlElement(ElementName = "lat")]
        public double? Latitude { get; set; }

        /// <summary>
        /// longitude of address (signed decimal) W < 0 > E
        /// </summary>
        [XmlElement(ElementName = "lon")]
        public double? Longitude { get; set; }

        /// <summary>
        /// grid locator
        /// </summary>
        [XmlElement(ElementName = "grid")]
        public string? Grid { get; set; }

        /// <summary>
        /// county name (USA)
        /// </summary>
        [XmlElement(ElementName = "county")]
        public string? County { get; set; }

        /// <summary>
        /// FIPS county identifier (USA)
        /// </summary>
        [XmlElement(ElementName = "fips")]
        public uint? FipsCountryIdentifier { get; set; }

        /// <summary>
        /// DXCC country name of the callsign
        /// </summary>
        [XmlElement(ElementName = "land")]
        public string? Land { get; set; }

        /// <summary>
        /// license effective date (USA). Use property LicenseEffectiveDate.
        /// </summary>
        [XmlElement(ElementName = "efdate")]
        public string? LicenseEffectiveDateStr { get; set; }

        /// <summary>
        /// license effective date (USA)
        /// </summary>
        public DateTime? LicenseEffectiveDate => Util.ParseDateTimeCallsign(LicenseEffectiveDateStr);

        /// <summary>
        /// license expiration date (USA). Use property LicenseExpirationDate.
        /// </summary>
        [XmlElement(ElementName = "expdate")]
        public string? LicenseExpirationDateStr { get; set; }

        /// <summary>
        /// license expiration date (USA)
        /// </summary>
        public DateTime? LicenseExpirationDate => Util.ParseDateTimeCallsign(LicenseExpirationDateStr);

        /// <summary>
        /// previous callsign
        /// </summary>
        [XmlElement(ElementName = "pcall")]
        public string? PreviousCallsign { get; set; }

        /// <summary>
        /// license class
        /// </summary>
        [XmlElement(ElementName = "class")]
        public string? LicenseClass { get; set; }

        /// <summary>
        /// license type codes (USA)
        /// </summary>
        [XmlElement(ElementName = "codes")]
        public string? LicenseTypeCodes { get; set; }

        /// <summary>
        /// QSL manager info
        /// </summary>
        [XmlElement(ElementName = "qslmgr")]
        public string? QslManager { get; set; }

        /// <summary>
        /// email address
        /// </summary>
        [XmlElement(ElementName = "email")]
        public string? Email { get; set; }

        /// <summary>
        /// web page address
        /// </summary>
        [XmlElement(ElementName = "url")]
        public string? Url { get; set; }

        /// <summary>
        /// QRZ web page views
        /// </summary>
        [XmlElement(ElementName = "u_views")]
        public uint? QrzUrlViews { get; set; }

        /// <summary>
        /// approximate length of the bio HTML in bytes
        /// </summary>
        [XmlElement(ElementName = "bio")]
        public uint? BioLength { get; set; }

        /// <summary>
        /// date of the last bio update. Use property BioDate.
        /// </summary>
        [XmlElement(ElementName = "biodate")]
        public string? BioDateStr { get; set; }

        /// <summary>
        /// date of the last bio update
        /// </summary>
        public DateTime? BioDate => Util.ParseDateTimeCallsign(BioDateStr);

        /// <summary>
        /// full URL of the callsign's primary image
        /// </summary>
        [XmlElement(ElementName = "image")]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// height:width:size in bytes, of the image file
        /// </summary>
        [XmlElement(ElementName = "imageinfo")]
        public string? ImageInfo { get; set; }

        /// <summary>
        /// QRZ db serial number
        /// </summary>
        [XmlElement(ElementName = "serial")]
        public uint? QrzSerialNumber { get; set; }

        /// <summary>
        /// QRZ callsign last modified date. Use property QrzLastModifiedDate.
        /// </summary>
        [XmlElement(ElementName = "moddate")]
        public string? QrzLastModifiedDateStr { get; set; }

        /// <summary>
        /// QRZ callsign last modified date
        /// </summary>
        public DateTime? QrzLastModifiedDate => Util.ParseDateTimeCallsign(QrzLastModifiedDateStr);

        /// <summary>
        /// Metro Service Area (USPS)
        /// </summary>
        [XmlElement(ElementName = "MSA")]
        public uint? MetroServiceArea { get; set; }

        /// <summary>
        /// Telephone Area Code (USA)
        /// </summary>
        [XmlElement(ElementName = "AreaCode")]
        public uint? TelephoneAreaCode { get; set; }

        /// <summary>
        /// Time Zone (USA)
        /// </summary>
        [XmlElement(ElementName = "TimeZone")]
        public string? TimeZone { get; set; }

        /// <summary>
        /// GMT Time Offset
        /// </summary>
        [XmlElement(ElementName = "GMTOffset")]
        public int? GmtTimeOffset { get; set; }

        /// <summary>
        /// Daylight Saving Time Observed
        /// </summary>
        [XmlElement(ElementName = "DST")]
        public string? DaylightSavingTimeObserved { get; set; }

        /// <summary>
        /// Will accept e-qsl (0/1 or blank if unknown)
        /// </summary>
        [XmlElement(ElementName = "eqsl")]
        public string? Eqsl { get; set; }

        /// <summary>
        /// Will return paper QSL (0/1 or blank if unknown)
        /// </summary>
        [XmlElement(ElementName = "mqsl")]
        public string? Mqsl { get; set; }

        /// <summary>
        /// CQ Zone identifier
        /// </summary>
        [XmlElement(ElementName = "cqzone")]
        public uint? CqZone { get; set; }

        /// <summary>
        /// ITU Zone identifier
        /// </summary>
        [XmlElement(ElementName = "ituzone")]
        public uint? ItuZone { get; set; }

        /// <summary>
        /// operator's year of birth
        /// </summary>
        [XmlElement(ElementName = "born")]
        public uint? Born { get; set; }

        /// <summary>
        /// User who manages this callsign on QRZ
        /// </summary>
        [XmlElement(ElementName = "user")]
        public string? ManagingUser { get; set; }

        /// <summary>
        /// Will accept LOTW (0/1 or blank if unknown)
        /// </summary>
        [XmlElement(ElementName = "lotw")]
        public string? LotW { get; set; }

        /// <summary>
        /// IOTA Designator (blank if unknown)
        /// </summary>
        [XmlElement(ElementName = "iota")]
        public string? Iota { get; set; }

        /// <summary>
        /// Describes source of lat/long data
        /// </summary>
        [XmlElement(ElementName = "geoloc")]
        public string? GeoLoc { get; set; }

        /// <summary>
        /// Attention address line, this line should be prepended to the address
        /// </summary>
        [XmlElement(ElementName = "attn")]
        public string? AttentionAddressLine { get; set; }

        /// <summary>
        /// A different or shortened name used on the air
        /// </summary>
        [XmlElement(ElementName = "nickname")]
        public string? Nickname { get; set; }

        /// <summary>
        /// Combined full name and nickname in the format used by QRZ. This fortmat is subject to change.
        /// </summary>
        [XmlElement(ElementName = "name_fmt")]
        public string? NameFormat { get; set; }
    }
}
