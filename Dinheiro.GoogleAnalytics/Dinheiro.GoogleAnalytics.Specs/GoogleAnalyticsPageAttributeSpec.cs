using System.Web.Mvc;
using Dinheiro.GoogleAnalytics.Utilities;
using Machine.Fakes;
using Machine.Specifications;

namespace Dinheiro.GoogleAnalytics.Specs
{
    [Subject(typeof(GoogleAnalyticsPageAttribute))]
    public class When_action_is_executing_with_google_analytics_page_attribute : WithFakes
    {
        It should_set_virtual_page_url_to_that_specified = () =>
            GoogleAnalytics.Current.VirtualPageUrl.ShouldEqual("/landingpage");

        Establish context = () =>
        {
            GoogleAnalytics.StateStorage = new InMemoryStateStorage();
            actionContext = An<ActionExecutingContext>();
            SUT = new GoogleAnalyticsPageAttribute("/landingpage");
        };

        Because of = () =>
            SUT.OnActionExecuting(actionContext);

        Cleanup after = () =>
            GoogleAnalytics.Reset();

        static GoogleAnalyticsPageAttribute SUT;
        static ActionExecutingContext actionContext;
    }
}