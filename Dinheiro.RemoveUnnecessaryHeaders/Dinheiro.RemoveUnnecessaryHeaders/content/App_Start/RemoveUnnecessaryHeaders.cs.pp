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
            // This only works if running in IIS7+ Integrated Pipeline mode
            if (!HttpRuntime.UsingIntegratedPipeline) return;

            context.PreSendRequestHeaders += (sender, e) =>
            {
                var app = sender as HttpApplication;
                if (app != null && app.Context != null)
                {
                    app.Context.Response.Headers.Remove("Server");
                }
            };
        }

        public void Dispose() { }
    }
}