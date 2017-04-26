using System.Runtime.Serialization;

namespace Models.Sportative
{
    [DataContract]
    public class ReceiveData
    {
        [DataMember(Name = "a", Order = 0)]
        public string Action { get; set; }

        [DataMember(Name = "v", Order = 1)]
        public string Version { get; set; }

        [DataMember(Name = "d", Order = 2)]
        public string Data { get; set; }
    }
}
