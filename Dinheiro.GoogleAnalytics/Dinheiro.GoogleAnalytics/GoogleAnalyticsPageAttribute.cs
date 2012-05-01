using System;
using System.Web.Mvc;

namespace Dinheiro.GoogleAnalytics
{
    /// <summary>
    /// Action filter attribute that can optionally be used to specify a virtual page URL for 
    /// the Google Analytics request.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class GoogleAnalyticsPageAttribute : ActionFilterAttribute
    {
        private readonly string _virtualPageUrl;

        /// <summary>
        /// Specifies a virtual page URL for the Google Analytics request.
        /// </summary>
        /// <param name="virtualPageUrl">Virtual page URL. If not already provided, a forward slash is added to the beginning.</param>
        public GoogleAnalyticsPageAttribute(string virtualPageUrl)
        {
            if (string.IsNullOrWhiteSpace(virtualPageUrl)) throw new ArgumentNullException("virtualPageUrl");
            _virtualPageUrl = virtualPageUrl;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            GoogleAnalytics.Current.VirtualPageUrl = _virtualPageUrl;
        }
    }
}