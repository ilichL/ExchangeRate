using Microsoft.AspNetCore.Mvc.Filters;

namespace ExchangeRate.Filters
{
    public class SampleResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            context.HttpContext.Response.Cookies.Append("LastVisit", DateTime.Now.ToString("R"));
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
