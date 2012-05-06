using System.Web;
using System.Web.Mvc;
using Machine.Fakes;
using Machine.Specifications;

namespace Dinheiro.Specs
{
    public abstract class ActionExecutedFilterContext<T> : WithFakes
        where T : ActionFilterAttribute
    {
        Establish context = () =>
        {
            Response = An<HttpResponseBase>();
            FilterContext = An<ActionExecutedContext>();
            FilterContext.WhenToldTo(x => x.HttpContext.Response)
                .Return(Response);
        };

        Because of = () =>
            SUT.OnActionExecuted(FilterContext);

        protected static T SUT;
        protected static ActionExecutedContext FilterContext;
        protected static HttpResponseBase Response;
    }
}