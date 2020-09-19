using Newtonsoft.Json;

namespace AppUnderTest.Documents
{
    public class Customer
    {
        public const string TypeString = "customer";

        public long Id { get; set; }

        [JsonProperty("fname")]
        public string FirstName { get; set; }

        [JsonProperty("lname")]
        public string LastName { get; set; }

        public string Type => TypeString;

        public string GetKey() => GetKey(Id);

        public static string GetKey(long id) => $"{TypeString}_{id}";
    }
}
