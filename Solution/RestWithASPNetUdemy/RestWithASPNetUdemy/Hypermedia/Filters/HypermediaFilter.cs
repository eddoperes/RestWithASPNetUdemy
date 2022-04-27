using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RestWithASPNetUdemy.Hypermedia.Filters
{
    public class HypermediaFilter: ResultFilterAttribute
    {

        private readonly HypermediaFilterOptions _hipermediaFilterOptions;

        public HypermediaFilter(HypermediaFilterOptions hipermediaFilterOptions)
        {
            _hipermediaFilterOptions = hipermediaFilterOptions;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            TryEnrichResult(context);
            base.OnResultExecuting(context);
        }

        private void TryEnrichResult(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult) {
                var enricher = _hipermediaFilterOptions.ContentResponseEnricherList.FirstOrDefault(x => x.CanEnrich(context));
                if (enricher != null) { 
                    Task.FromResult(enricher.Enrich(context));
                }
            }
        }

    }

}
