using System;
using System.Web;
using System.Web.Mvc;
using Machine.Fakes;
using Machine.Specifications;

namespace Dinheiro.Specs
{
    [Subject(typeof(NoIFrameAttribute))]
    public class After_action_is_executed_with_noiframe_attribute : ActionExecutedFilterContext<NoIFrameAttribute>
    {
        It should_add_x_frame_options_deny_header = () =>
            Response.WasToldTo(x => x.AppendHeader("X-Frame-Options", "DENY"));

        Establish context = () =>
        {
            FilterContext.WhenToldTo(x => x.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(Param<Type>.IsAnything, Param<bool>.IsAnything))
                .Return(new object[0]);
            FilterContext.WhenToldTo(x => x.ActionDescriptor.GetCustomAttributes(Param<Type>.IsAnything, Param<bool>.IsAnything))
                .Return(new object[0]);
            SUT = new NoIFrameAttribute();
        };
    }

    [Subject(typeof(NoIFrameAttribute))]
    public class When_an_iframe_attribute_is_specified_on_the_controller : ActionExecutedFilterContext<NoIFrameAttribute>
    {
        It should_not_add_x_frame_options_deny = () =>
            Response.WasNotToldTo(x => x.AppendHeader(Param<string>.IsAnything, Param<string>.IsAnything));

        Establish context = () =>
        {
            FilterContext.WhenToldTo(x => x.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(Param<Type>.IsAnything, Param<bool>.IsAnything))
                .Return(new object[] { new IFrameAttribute() });
            FilterContext.WhenToldTo(x => x.ActionDescriptor.GetCustomAttributes(Param<Type>.IsAnything, Param<bool>.IsAnything))
                .Return(new object[0]);
            SUT = new NoIFrameAttribute();
        };
    }

    [Subject(typeof(NoIFrameAttribute))]
    public class When_an_iframe_attribute_is_specified_on_the_action_method : ActionExecutedFilterContext<NoIFrameAttribute>
    {
        It should_not_add_x_frame_options_deny = () =>
            Response.WasNotToldTo(x => x.AppendHeader(Param<string>.IsAnything, Param<string>.IsAnything));

        Establish context = () =>
        {
            FilterContext.WhenToldTo(x => x.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(Param<Type>.IsAnything, Param<bool>.IsAnything))
                .Return(new object[0]);
            FilterContext.WhenToldTo(x => x.ActionDescriptor.GetCustomAttributes(Param<Type>.IsAnything, Param<bool>.IsAnything))
                .Return(new object[] { new IFrameAttribute() });
            SUT = new NoIFrameAttribute();
        };
    }
}