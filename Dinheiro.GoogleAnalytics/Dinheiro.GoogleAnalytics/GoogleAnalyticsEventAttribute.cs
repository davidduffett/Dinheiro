using System;
using System.Web.Mvc;

namespace Dinheiro.GoogleAnalytics
{
    /// <summary>
    /// Action filter attribute that can be applied to an entire controller or a single action method,
    /// specifying that a Google Analytics event should be recorded.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class GoogleAnalyticsEventAttribute : ActionFilterAttribute
    {
        public string Category { get; set; }
        public string Action { get; set; }
        public string Label { get; set; }
        public int? Value { get; set; }
        public bool NonInteraction { get; set; }

        /// <summary>
        /// Sets the event category from the value of the method parameter.
        /// </summary>
        public string CategoryParameter { get; set; }
        /// <summary>
        /// Sets the event action from the value of the method parameter.
        /// </summary>
        public string ActionParameter { get; set; }
        /// <summary>
        /// Sets the event label from the value of the method parameter.
        /// </summary>
        public string LabelParameter { get; set; }
        /// <summary>
        /// Sets the event value from the value of the method parameter.
        /// </summary>
        public string ValueParameter { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            GoogleAnalytics.Current.TrackEvent(
                string.IsNullOrWhiteSpace(CategoryParameter) ? Category : Convert.ToString(filterContext.ActionParameters[CategoryParameter]), 
                string.IsNullOrWhiteSpace(ActionParameter) ? Action : Convert.ToString(filterContext.ActionParameters[ActionParameter]),
                string.IsNullOrWhiteSpace(LabelParameter) ? Label : Convert.ToString(filterContext.ActionParameters[LabelParameter]),
                string.IsNullOrWhiteSpace(ValueParameter) ? Value : Convert.ToInt32(filterContext.ActionParameters[ValueParameter]),
                NonInteraction);
        }
    }
}