using RestWithASPNetUdemy.Hypermedia.Abstract;

namespace RestWithASPNetUdemy.Hypermedia.Filters
{
    public class HypermediaFilterOptions
    {

        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();

    }

}
