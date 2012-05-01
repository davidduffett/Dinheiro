using System.Web.Mvc;

namespace Dinheiro.GoogleAnalytics.Example.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [GoogleAnalyticsPage("/virtualurl")]
        public ActionResult VirtualPageUrl()
        {
            // Or instead of the action filter, you could use:
            // GoogleAnalytics.Current.VirtualPageUrl = "/virtualurl";

            return View("~/Views/Home/Index.cshtml");
        }

        [GoogleAnalyticsEvent(Category = "Event Category", Action = "Event Action", Label = "Event Label")]
        public ActionResult TrackEvent()
        {
            // Or instead of the action filter, you could use:
            // GoogleAnalytics.Current.TrackEvent("Event Category", "Event Action", "Event Label");

            return View("~/Views/Home/Index.cshtml");
        }

        [GoogleAnalyticsEvent(CategoryParameter = "category", ActionParameter = "eventAction", LabelParameter = "label", ValueParameter = "value")]
        public ActionResult TrackEventFromParameters(string category, string eventAction, string label, int? value)
        {
            // Or instead of the action filter, you could use:
            // GoogleAnalytics.Current.TrackEvent(category, eventAction, label, value);

            return View("~/Views/Home/Index.cshtml");
        }

        public ActionResult TrackSocial()
        {
            GoogleAnalytics.Current.TrackSocial("facebook", "like", "http://www.mysite.com");
            return View("~/Views/Home/Index.cshtml");
        }

        public ActionResult TrackEcommerce()
        {
            var orderId = 123456;
            GoogleAnalytics.Current.AddTransaction(orderId, total: 15.99m, shipping: 3m);
            GoogleAnalytics.Current.AddItem("Product SKU", "Product name", 12.99m, 2, orderId);
            return View("~/Views/Home/Index.cshtml");
        }
    }
}