namespace Dinheiro.GoogleAnalytics
{
    interface IScripts
    {
        string ScriptStart { get; }
        string ScriptEnd { get; }
        string SetAccount { get; }
        string RequireEcommerce { get; }
        string SetVariable { get; }
        string TrackPageView { get; }
        string TrackVirtualPageView { get; }
        string AddTrans { get; }
        string AddTransWithCurrency { get; }
        string AddItem { get; }
        string TrackTrans { get; }
        string TrackEvent { get; }
        string TrackSocial { get; }
        string TrackSocialWithPagePath { get; }
    }

    static class ScriptImplementations
    {
        public static IScripts Asynchronous = new AsynchronousScripts();
        public static IScripts Universal = new UniversalAnalyticsScripts();
    }
}