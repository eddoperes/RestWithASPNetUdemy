//using System.Text.Json.Serialization;

using RestWithASPNetUdemy.Hypermedia;
using RestWithASPNetUdemy.Hypermedia.Abstract;

namespace RestWithASPNetUdemy.Data.VO
{
    public class PersonVO: ISupportsHypermedia
    {

        //[JsonPropertyName("code")]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public bool Enabled { get; set; }

        public List<HypermediaLink> Links { get; set; } = new List<HypermediaLink>();

    }
}
