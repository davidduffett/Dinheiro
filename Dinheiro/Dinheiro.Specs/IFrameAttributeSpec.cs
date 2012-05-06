using Machine.Fakes;
using Machine.Specifications;

namespace Dinheiro.Specs
{
    [Subject(typeof(IFrameAttribute))]
    public class After_action_is_executed_with_iframe_attribute : ActionExecutedFilterContext<IFrameAttribute>
    {
        It should_add_x_frame_options_same_origin_header = () =>
            Response.WasToldTo(x => x.AppendHeader("X-Frame-Options", "SAMEORIGIN"));

        Establish context = () =>
            SUT = new IFrameAttribute();
    }
}