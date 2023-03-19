using System.Xml.Serialization;

namespace Logatron.QrzDotCom
{
    public class Session
    {
        /// <summary>
        /// a valid user session key
        /// </summary>
        [XmlElement(ElementName = "Key")]
        public string? Key { get; set; }

        /// <summary>
        /// Number of lookups performed by this user in the current 24 hour period
        /// </summary>
        [XmlElement(ElementName = "Count")]
        public uint? Count { get; set; }

        /// <summary>
        /// time and date that the users subscription will expire - or - "non-subscriber"
        /// </summary>
        [XmlElement(ElementName = "SubExp")]
        public string? SubscriptionExpiresStr { get; set; }

        /// <summary>
        /// time and date that the users subscription will expire
        /// </summary>
        public DateTime? SubscriptionExpires => HasSubscription ? Util.ParseDateTimeSession(SubscriptionExpiresStr) : null;

        /// <summary>
        /// true if the user has a subscription
        /// </summary>
        public bool HasSubscription => SubscriptionExpiresStr != "non-subscriber";

        /// <summary>
        /// Time stamp for this message. Use property Time.
        /// </summary>
        [XmlElement(ElementName = "GMTime")]
        public string? TimeStr { get; set; }

        /// <summary>
        /// Time stamp for this message
        /// </summary>
        public DateTime? Time => Util.ParseDateTimeSession(TimeStr);

        /// <summary>
        /// An informational message for the user
        /// </summary>
        [XmlElement(ElementName = "Message")]
        public string? Message { get; set; }

        /// <summary>
        /// XML system error message
        /// </summary>
        [XmlElement(ElementName = "Error")]
        public string? Error { get; set; }

        /// <summary>
        /// Server-side remarks
        /// </summary>
        [XmlElement(ElementName = "Remark")]
        public string? Remark { get; set; }
    }
}
