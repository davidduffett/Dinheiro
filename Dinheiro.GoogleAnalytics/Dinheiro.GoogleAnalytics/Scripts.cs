using System.Text;

namespace Dinheiro.GoogleAnalytics
{
    public static class Scripts
    {
        public const string ScriptStart =
            @"<script type=""text/javascript"">
var _gaq = _gaq || [];
";

        public const string ScriptEnd = 
            @"(function() {
  var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
  ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
  var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
})();
</script>";

        public const string GaqPush = "_gaq.push([{0}]);";

        public const string SetAccount = "'_setAccount','{0}'";
        public const string TrackPageView = "'_trackPageview'";
        public const string TrackVirtualPageView = "'_trackPageview','/{0}'";

        public const string AddTrans = "'_addTrans','{OrderId}','{Affiliation}','{Total:0.00}','{Tax:0.00}','{Shipping:0.00}','{City}','{State}','{Country}'";
        public const string AddItem = "'_addItem','{OrderId}','{Sku}','{Name}','{Category}','{Price:0.00}','{Quantity}'";
        public const string TrackTrans = "'_trackTrans'";

        public const string TrackEvent = "'_trackEvent','{Category}','{Action}','{Label}',{Value}";
        public const string TrackSocial = "'_trackSocial','{Network}','{SocialAction}','{Target}'";
        public const string TrackSocialWithPagePath = "'_trackSocial','{Network}','{SocialAction}','{Target}','{PagePath}'";

        public static void Push(this StringBuilder builder, string script)
        {
            builder.AppendLine(string.Format(GaqPush, script));
        }
    }
}