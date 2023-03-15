using System.Xml.Serialization;

namespace HamRadioLib.QrzDotCom
{
    [XmlRoot(ElementName = "QRZDatabase", Namespace = "http://xmldata.qrz.com")]
    public class QrzDatabase
    {
        /// <summary>
        /// Version of this database result.
        /// </summary>
        [XmlAttribute(AttributeName = "version")]
        public string? Version { get; set; }

        /// <summary>
        /// Information about the session.
        /// </summary>
        [XmlElement(ElementName = "Session")]
        public Session? Session { get; set; }

        /// <summary>
        /// Information about the requested callsign.
        /// </summary>
        [XmlElement(ElementName = "Callsign")]
        public Callsign? Callsign { get; set; }

        /// <summary>
        /// Information about the requested DXCC.
        /// </summary>
        [XmlElement(ElementName = "DXCC")]
        public Dxcc? Dxcc { get; set; }
    }
}
