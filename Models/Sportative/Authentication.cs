namespace Models.Sportative
{
    [DataContract]
    public class Authentication
    {
        [DataMember(Name = "SN", Order = 0)]
        public string SN { get; set; }

        [DataMember(Name = "SK", Order = 1)]
        public string SK { get; set; }
    }
}
