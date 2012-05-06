using System.Web.Mvc;

namespace Dinheiro
{
    /// <summary>
    /// Adds a HTTP header to the response after an action result is executed.
    /// </summary>
    public class HttpHeaderAttribute : ActionFilterAttribute
    {
        public HttpHeaderAttribute()
        {
        }

        public HttpHeaderAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!string.IsNullOrWhiteSpace(Name))
                filterContext.HttpContext.Response.AppendHeader(Name, Value);
        }
    }
}