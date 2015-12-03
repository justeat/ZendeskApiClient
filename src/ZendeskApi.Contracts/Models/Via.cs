using System.Runtime.Serialization;

namespace ZendeskApi.Contracts.Models
{
    [DataContract]
    public class Via
    {
        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "source")]
        public Source Source { get; set; }
    }

    [DataContract]
    public class Source
    {
        [DataMember(Name = "to")]
        public dynamic To { get; set; }

        [DataMember(Name = "from")]
        public dynamic From { get; set; }

        [DataMember(Name = "rel")]
        public string Rel { get; set; }
    }
}
