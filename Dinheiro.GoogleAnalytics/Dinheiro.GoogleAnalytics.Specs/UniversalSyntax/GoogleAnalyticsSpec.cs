using Dinheiro.GoogleAnalytics.Utilities;
using Machine.Fakes;
using Machine.Specifications;

namespace Dinheiro.GoogleAnalytics.Specs.UniversalSyntax
{
    public abstract class InMemoryRenderContext : WithFakes
    {
        Establish context = () =>
        {
            GoogleAnalytics.TrackingType = GoogleAnalyticsTrackingType.analytics_js;
            GoogleAnalytics.StateStorage = new InMemoryStateStorage();
        };

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
            Output.ShouldContain("ga('create','" + GoogleAnalytics.Account + "','auto');");

        It should_track_page_view = () =>
            Output.ShouldContain("ga('send','pageview');");

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
            Output.ShouldContain("ga('send','pageview','/landingpage');");

        Establish context = () =>
            GoogleAnalytics.Current.VirtualPageUrl = "landingpage";
    }

    [Subject(typeof(GoogleAnalytics))]
    public class When_a_virtual_page_url_has_been_set_with_forward_slash : InMemoryRenderContext
    {
        It should_track_page_view_with_that_url = () =>
            Output.ShouldContain("ga('send','pageview','/landingpagewithslash');");

        Establish context = () =>
            GoogleAnalytics.Current.VirtualPageUrl = "/landingpagewithslash";
    }

    [Subject(typeof(GoogleAnalytics))]
    public class When_events_are_to_be_tracked : InMemoryRenderContext
    {
        Behaves_like<BasicTrackingConfigurationSet> basic_configuration_is_set;

        It should_track_events_for_each_one = () =>
        {
            Output.ShouldContain("ga('send','event','Unsubscribe','Submit');");
            Output.ShouldContain("ga('send','event','Product','View','ES123456');");
            Output.ShouldContain("ga('send','event','Basket','Add Item','ES123456',2);");
        };

        Establish context = () =>
        {
            GoogleAnalytics.Current.TrackEvent("Unsubscribe", "Submit");
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
            Output.ShouldContain("ga('send','social','facebook','like','http://www.mysite.com');");
            Output.ShouldContain("ga('send','social','twitter','tweet','ES123456',{'page':'/landingpage'});");
            Output.ShouldContain("ga('send','social','facebook','unlike','ES123456',{'page':'/landingpagewithslash'});");
        };

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

        It should_require_ecommerce_module = () =>
            Output.ShouldContain("ga('require','ecommerce','ecommerce.js');");

        It should_add_transaction = () =>
            Output.ShouldContain("ga('ecommerce:addTransaction',{'id':'1234','affiliation':'Womens Apparel','revenue':'28.28','shipping':'15.00','tax':'1.29'});");

        It should_add_item_for_each_one = () =>
        {
            Output.ShouldContain("ga('ecommerce:addItem',{'id':'1234','name':'T-Shirt','sku':'DD44','category':'Olive Medium','price':'11.99','quantity':'1'});");
            Output.ShouldContain("ga('ecommerce:addItem',{'id':'1234','name':'Pants','sku':'EE66','category':'Khaki','price':'12.00','quantity':'2'});");
        };

        It should_track_transaction = () =>
            Output.ShouldContain("ga('ecommerce:send');");

        It should_require_ecommerce_before_adding_transactions = () =>
            Output.IndexOf("ga('require','ecommerce'").ShouldBeLessThan(Output.IndexOf("ga('ecommerce:addTransaction'"));

        It should_add_transaction_before_adding_items = () =>
            Output.IndexOf("ga('ecommerce:addTransaction'").ShouldBeLessThan(Output.IndexOf("ga('ecommerce:addItem'"));

        It should_track_transaction_after_adding_items = () =>
            Output.IndexOf("ga('ecommerce:send'").ShouldBeGreaterThan(Output.IndexOf("ga('ecommerce:addItem'"));

        Establish context = () =>
        {
            GoogleAnalytics.Current.AddTransaction(1234, 28.28m, 1.29m, 15m, "Womens Apparel");
            GoogleAnalytics.Current.AddItem("DD44", "T-Shirt", 11.99m, 1, 1234, "Olive Medium");
            GoogleAnalytics.Current.AddItem("EE66", "Pants", 12m, 2, 1234, "Khaki");
        };
    }

    [Subject(typeof(GoogleAnalytics))]
    public class When_a_transaction_and_items_have_been_added_containing_javascript_characters : InMemoryRenderContext
    {
        It should_add_transaction_with_strings_encoded = () =>
            Output.ShouldContain(@"ga('ecommerce:addTransaction',{'id':'1234','affiliation':'Womens \\n App\u003carel','revenue':'28.28','shipping':'15.00','tax':'1.29'});");

        It should_add_item_with_strings_encoded = () =>
            Output.ShouldContain(@"ga('ecommerce:addItem',{'id':'1234','name':'\u0027Shark\u0027 T-Shirt','sku':'DD44','category':'Olive \u0026 Medium','price':'11.99','quantity':'1'});");

        Establish context = () =>
        {
            GoogleAnalytics.Current.AddTransaction(1234, 28.28m, 1.29m, 15m, @"Womens \n App<arel");
            GoogleAnalytics.Current.AddItem("DD44", @"'Shark' T-Shirt", 11.99m, 1, 1234, "Olive & Medium");
        };
    }

    [Subject(typeof(GoogleAnalytics))]
    public class When_currency_has_been_set : InMemoryRenderContext
    {
        Behaves_like<BasicTrackingConfigurationSet> basic_configuration_is_set;

        It should_set_currency_code_when_adding_transaction = () =>
            Output.ShouldContain("ga('ecommerce:addTransaction',{'id':'1234','affiliation':'Womens Apparel','revenue':'28.28','shipping':'15.00','tax':'1.29','currency':'EUR'});");

        Establish context = () =>
        {
            GoogleAnalytics.Current.SetCurrency("EUR");
            GoogleAnalytics.Current.AddTransaction(1234, 28.28m, 1.29m, 15m, "Womens Apparel");
            GoogleAnalytics.Current.AddItem("DD44", "T-Shirt", 11.99m, 1, 1234, "Olive Medium");
            GoogleAnalytics.Current.AddItem("EE66", "Pants", 12m, 2, 1234, "Khaki");
        };
    }

    public class When_display_features_plugin_is_not_enabled : InMemoryRenderContext
    {
        Behaves_like<BasicTrackingConfigurationSet> basic_configuration_is_set;

        It should_not_load_the_display_features_plugin = () =>
            Output.ShouldNotContain("ga('require', 'displayfeatures');");

        Establish context = () =>
            GoogleAnalytics.EnableDisplayFeaturesPlugin = false;
    }

    [Subject(typeof (GoogleAnalytics))]
    public class When_display_features_plugin_is_enabled : InMemoryRenderContext
    {
        Behaves_like<BasicTrackingConfigurationSet> basic_configuration_is_set;

        It should_load_the_display_features_plugin = () =>
            Output.ShouldContain("ga('require', 'displayfeatures');");

        It should_place_the_display_features_require_after_the_create_tag = () =>
            Output.IndexOf("ga('require', 'displayfeatures');").ShouldBeGreaterThan(Output.IndexOf("ga('create'"));

        It should_place_the_display_features_require_before_sending_the_pageview = () =>
            Output.IndexOf("ga('require', 'displayfeatures');").ShouldBeLessThan(Output.IndexOf("ga('send','pageview');"));

        Establish context = () =>
            GoogleAnalytics.EnableDisplayFeaturesPlugin = true;
    }
}