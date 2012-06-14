using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.RemoveUnnecessaryHeaders), "Start")]

namespace $rootnamespace$.App_Start
{
    /// <summary>
    /// Removes HTTP headers added by ASP.NET and IIS that can't be removed anywhere in the web.config.
    /// </summary>
    public static class RemoveUnnecessaryHeaders
    {
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(RemoveUnnecessaryHeadersModule));
            MvcHandler.DisableMvcResponseHeader = true;
        }
    }

    public class RemoveUnnecessaryHeadersModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += (sender, e) =>
            {
                if (HttpContext.Current != null)
                    HttpContext.Current.Response.Headers.Remove("Server");
            };
        }

        public void Dispose() { }
    }
}