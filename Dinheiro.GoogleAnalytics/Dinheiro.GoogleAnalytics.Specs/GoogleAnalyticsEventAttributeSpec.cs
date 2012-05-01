using System.Web.Mvc;
using Dinheiro.GoogleAnalytics.Utilities;
using Machine.Fakes;
using Machine.Specifications;

namespace Dinheiro.GoogleAnalytics.Specs
{
    public abstract class GoogleAnalyticsEventAttributeContext : WithFakes
    {
        Establish context = () =>
        {
            GA = An<IGoogleAnalytics>();
            GoogleAnalytics.StateStorage = An<IStateStorage>();
            GoogleAnalytics.StateStorage.WhenToldTo(x => x.Get<IGoogleAnalytics>(GoogleAnalytics.StateKey))
                .Return(GA);
            ActionContext = An<ActionExecutingContext>();
        };

        Because of = () =>
            SUT.OnActionExecuting(ActionContext);

        protected static GoogleAnalyticsEventAttribute SUT;
        protected static ActionExecutingContext ActionContext;
        protected static IGoogleAnalytics GA;
    }

    [Subject(typeof(GoogleAnalyticsEventAttribute))]
    public class When_action_executing_with_event_attribute_and_values_provided : GoogleAnalyticsEventAttributeContext
    {
        It should_call_track_event_with_provided_values = () =>
            GA.WasToldTo(x => x.TrackEvent("Product", "View", "ES123456", null, false));

        Establish context = () =>
            SUT = new GoogleAnalyticsEventAttribute
                      {
                          Category = "Product",
                          Action = "View",
                          Label = "ES123456"
                      };
    }

    [Subject(typeof(GoogleAnalyticsEventAttribute))]
    public class When_action_executing_with_event_attribute_and_parameter_names_provided : GoogleAnalyticsEventAttributeContext
    {
        It should_call_track_event_with_parameter_values = () =>
            GA.WasToldTo(x => x.TrackEvent("Basket", "Add Item", "ES987654", 1, false));

        Establish context = () =>
        {
            SUT = new GoogleAnalyticsEventAttribute
                      {
                          CategoryParameter = "category",
                          ActionParameter = "action",
                          LabelParameter = "label",
                          ValueParameter = "value"
                      };

            ActionContext.WhenToldTo(x => x.ActionParameters["category"])
                .Return("Basket");
            ActionContext.WhenToldTo(x => x.ActionParameters["action"])
                .Return("Add Item");
            ActionContext.WhenToldTo(x => x.ActionParameters["label"])
                .Return("ES987654");
            ActionContext.WhenToldTo(x => x.ActionParameters["value"])
                .Return(1);
        };
    }
}