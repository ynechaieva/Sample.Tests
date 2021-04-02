using System.Runtime.Serialization;

namespace Sample.Test
{
    public class Support
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }
    }

}
