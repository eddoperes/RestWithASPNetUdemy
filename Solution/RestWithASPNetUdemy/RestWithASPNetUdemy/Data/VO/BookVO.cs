using RestWithASPNetUdemy.Hypermedia;
using RestWithASPNetUdemy.Hypermedia.Abstract;

namespace RestWithASPNetUdemy.Data.VO
{
    public class BookVO : ISupportsHypermedia
    {

        public int Id { get; set; }

        public string Author { get; set; }

        public DateTime LaunchDate { get; set; }

        public Decimal Price { get; set; }

        public string Title { get; set; }

        public List<HypermediaLink> Links { get; set; } = new List<HypermediaLink>();

    }
}
