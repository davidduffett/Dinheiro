using Dinheiro.GoogleAnalytics.Utilities;
using Machine.Fakes;
using Machine.Specifications;

namespace Dinheiro.GoogleAnalytics.Specs
{
    public abstract class InMemoryRenderContext : WithFakes
    {
        Establish context = () =>
            GoogleAnalytics.StateStorage = new InMemoryStateStorage();

        Because of = () =>
            Output = GoogleAnalytics.Render().ToString();

        Cleanup after = () =>
            GoogleAnalytics.Reset();

        protected static string Output;
    }

    [Behaviors]
    public class BasicTrackingConfigurationSet
    {
        It should_set_account = () =>
            Output.ShouldContain("_gaq.push(['_setAccount','" + GoogleAnalytics.Account + "']);");

        It should_track_page_view = () =>
            Output.ShouldContain("_gaq.push(['_trackPageview']);");

        protected static string Output;
    }

    [Subject(typeof(GoogleAnalytics))]
    public class When_rendering_google_analytics : InMemoryRenderContext
    {
        Behaves_like<BasicTrackingConfigurationSet> basic_configuration_is_set;

        Establish context = () =>
            GoogleAnalytics.Account = "UA-123456-7";
    }

    [Subject(typeof(GoogleAnalytics))]
    public class When_a_virtual_page_url_has_been_set : InMemoryRenderContext
    {
        It should_track_page_view_with_forward_slash_and_that_url = () =>
            Output.ShouldContain("_gaq.push(['_trackPageview','/landingpage']);");

        Establish context = () =>
            GoogleAnalytics.Current.VirtualPageUrl = "landingpage";
    }

    [Subject(typeof(GoogleAnalytics))]
    public class When_a_virtual_page_url_has_been_set_with_forward_slash : InMemoryRenderContext
    {
        It should_track_page_view_with_that_url = () =>
            Output.ShouldContain("_gaq.push(['_trackPageview','/landingpagewithslash']);");

        Establish context = () =>
            GoogleAnalytics.Current.VirtualPageUrl = "/landingpagewithslash";
    }

    [Subject(typeof(GoogleAnalytics))]
    public class When_events_are_to_be_tracked : InMemoryRenderContext
    {
        Behaves_like<BasicTrackingConfigurationSet> basic_configuration_is_set;

        It should_track_events_for_each_one = () =>
        {
            Output.ShouldContain("_gaq.push(['_trackEvent','Product','View','ES123456',]);");
            Output.ShouldContain("_gaq.push(['_trackEvent','Basket','Add Item','ES123456',2]);");
        };

        It should_track_events_before_tracking_page_view = () =>
            Output.IndexOf("'_trackEvent'").ShouldBeLessThan(Output.IndexOf("'_trackPageview'"));

        Establish context = () =>
        {
            GoogleAnalytics.Current.TrackEvent("Product", "View", "ES123456");
            GoogleAnalytics.Current.TrackEvent("Basket", "Add Item", "ES123456", 2, true);
        };
    }

    [Subject(typeof(GoogleAnalytics))]
    public class When_social_events_are_to_be_tracked : InMemoryRenderContext
    {
        Behaves_like<BasicTrackingConfigurationSet> basic_configuration_is_set;

        It should_track_social_for_each_one = () =>
        {
            Output.ShouldContain("_gaq.push(['_trackSocial','facebook','like','http://www.mysite.com']);");
            Output.ShouldContain("_gaq.push(['_trackSocial','twitter','tweet','ES123456','/landingpage']);");
            Output.ShouldContain("_gaq.push(['_trackSocial','facebook','unlike','ES123456','/landingpagewithslash']);");
        };

        It should_track_social_before_tracking_page_view = () =>
            Output.IndexOf("'_trackSocial'").ShouldBeLessThan(Output.IndexOf("'_trackPageview'"));

        Establish context = () =>
        {
            GoogleAnalytics.Current.TrackSocial("facebook", "like", "http://www.mysite.com");
            GoogleAnalytics.Current.TrackSocial("twitter", "tweet", "ES123456", "landingpage");
            GoogleAnalytics.Current.TrackSocial("facebook", "unlike", "ES123456", "/landingpagewithslash");
        };
    }

    [Subject(typeof(GoogleAnalytics))]
    public class When_a_transaction_and_items_have_been_added : InMemoryRenderContext
    {
        Behaves_like<BasicTrackingConfigurationSet> basic_configuration_is_set;

        It should_add_transaction = () =>
            Output.ShouldContain("_gaq.push(['_addTrans','1234','Womens Apparel','28.28','1.29','15.00','San Jose','California','USA']);");

        It should_add_item_for_each_one = () =>
        {
            Output.ShouldContain("_gaq.push(['_addItem','1234','DD44','T-Shirt','Olive Medium','11.99','1']);");
            Output.ShouldContain("_gaq.push(['_addItem','1234','EE66','Pants','Khaki','12.00','2']);");
        };

        It should_track_transaction = () =>
            Output.ShouldContain("_gaq.push(['_trackTrans']);");

        It should_add_transaction_before_adding_items = () =>
            Output.IndexOf("'_addTrans'").ShouldBeLessThan(Output.IndexOf("'_addItem'"));

        It should_add_items_before_tracking_page_view = () =>
            Output.IndexOf("'_addItem'").ShouldBeLessThan(Output.IndexOf("'_trackPageview'"));

        It should_track_transaction_after_tracking_page_view = () =>
            Output.IndexOf("'_trackTrans'").ShouldBeGreaterThan(Output.IndexOf("'_trackPageview'"));

        Establish context = () =>
        {
            GoogleAnalytics.Current.AddTransaction(1234, 28.28m, 1.29m, 15m, "Womens Apparel", "San Jose", "California", "USA");
            GoogleAnalytics.Current.AddItem("DD44", "T-Shirt", 11.99m, 1, 1234, "Olive Medium");
            GoogleAnalytics.Current.AddItem("EE66", "Pants", 12m, 2, 1234, "Khaki");
        };
    }

    [Subject(typeof(GoogleAnalytics))]
    public class When_a_transaction_and_items_have_been_added_containing_javascript_characters : InMemoryRenderContext
    {
        It should_add_transaction_with_strings_encoded = () =>
            Output.ShouldContain(@"_gaq.push(['_addTrans','1234','Womens \\n Apparel','28.28','1.29','15.00','San\\Jose','Calif\u003cornia','USA']);");

        It should_add_item_with_strings_encoded = () =>
            Output.ShouldContain(@"_gaq.push(['_addItem','1234','DD44','\u0027Shark\u0027 T-Shirt','Olive \u0026 Medium','11.99','1']);");

        Establish context = () =>
        {
            GoogleAnalytics.Current.AddTransaction(1234, 28.28m, 1.29m, 15m, @"Womens \n Apparel", @"San\Jose", @"Calif<ornia", "USA");
            GoogleAnalytics.Current.AddItem("DD44", @"'Shark' T-Shirt", 11.99m, 1, 1234, "Olive & Medium");
        };
    }
}