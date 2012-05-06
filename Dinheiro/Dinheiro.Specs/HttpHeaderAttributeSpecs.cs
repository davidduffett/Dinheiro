using Machine.Fakes;
using Machine.Specifications;

namespace Dinheiro.Specs
{
    [Subject(typeof(HttpHeaderAttribute))]
    public class After_action_is_executed_with_http_header : ActionExecutedFilterContext<HttpHeaderAttribute>
    {
        It should_add_http_header_to_response = () =>
            Response.WasToldTo(x => x.AppendHeader("name", "value"));

        Establish context = () =>
            SUT = new HttpHeaderAttribute("name", "value");
    }

    [Subject(typeof(HttpHeaderAttribute))]
    public class When_no_http_header_name_is_specified : ActionExecutedFilterContext<HttpHeaderAttribute>
    {
        It should_not_add_http_header_to_response = () =>
            Response.WasNotToldTo(x => x.AppendHeader(Param<string>.IsAnything, Param<string>.IsAnything));

        Establish context = () =>
            SUT = new HttpHeaderAttribute();
    }
}