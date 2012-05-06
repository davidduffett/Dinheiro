using System.Web.Mvc;

namespace Dinheiro
{
    /// <summary>
    /// Prevents a page from being displayed within a frame or iframe element,
    /// by adding a HTTP response header: X-Frame-Options: DENY
    /// This can help to avoid clickjacking attacks, by ensuring your content is not embedded into other sites.
    /// </summary>
    public class NoIFrameAttribute : XFrameOptionsAttribute
    {
        public NoIFrameAttribute() : base(XFrameOption.DENY)
        {
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Abort if an [IFrame] attribute is applied to controller or action
            if (filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(IFrameAttribute), true).Length > 0 ||
                filterContext.ActionDescriptor.GetCustomAttributes(typeof(IFrameAttribute), true).Length > 0)
                return;

            base.OnActionExecuted(filterContext);
        }
    }
}