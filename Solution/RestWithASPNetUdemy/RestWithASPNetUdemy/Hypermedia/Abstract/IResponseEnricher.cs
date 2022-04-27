using Microsoft.AspNetCore.Mvc.Filters;

namespace RestWithASPNetUdemy.Hypermedia.Abstract
{
    public interface IResponseEnricher
    {

        bool CanEnrich(ResultExecutingContext context);

        Task Enrich(ResultExecutingContext context);

    }

}
