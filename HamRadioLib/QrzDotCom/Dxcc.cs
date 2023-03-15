using System.Xml.Serialization;

namespace HamRadioLib.QrzDotCom
{
    public class Dxcc
    {
        /// <summary>
        /// DXCC entity number for this record
        /// </summary>
        [XmlElement(ElementName = "dxcc")]
        public uint? DxccEntity { get; set; }

        /// <summary>
        /// 2-letter country code (ISO-3166)
        /// </summary>
        [XmlElement(ElementName = "cc")]
        public string? Cc { get; set; }

        /// <summary>
        /// 3-letter country code (ISO-3166)
        /// </summary>
        [XmlElement(ElementName = "ccc")]
        public string? Ccc { get; set; }

        /// <summary>
        /// long name
        /// </summary>
        [XmlElement(ElementName = "name")]
        public string? Name { get; set; }

        /// <summary>
        /// 	2-letter continent designator
        /// </summary>
        [XmlElement(ElementName = "continent")]
        public string? Continent { get; set; }

        /// <summary>
        /// ITU Zone
        /// </summary>
        [XmlElement(ElementName = "ituzone")]
        public uint? ItuZone { get; set; }

        /// <summary>
        /// CQ Zone
        /// </summary>
        [XmlElement(ElementName = "cqzone")]
        public uint? CqZone { get; set; }

        /// <summary>
        /// UTC timezone offset +/-
        /// </summary>
        [XmlElement(ElementName = "timezone")]
        public int? UtcTimezoneOffset { get; set; }

        /// <summary>
        /// Latitude (approx.)
        /// </summary>
        [XmlElement(ElementName = "lat")]
        public double? Latitude { get; set; }

        /// <summary>
        /// Longitude (approx.)
        /// </summary>
        [XmlElement(ElementName = "lon")]
        public double? Longitude { get; set; }

        /// <summary>
        /// Special notes and/or exceptions
        /// </summary>
        [XmlElement(ElementName = "notes")]
        public string? Notes { get; set; }
    }
}
