namespace Dinheiro.GoogleAnalytics
{
    class AsynchronousScripts : IScripts
    {
        public string ScriptStart
        {
            get 
            { 
                return 
@"<script type=""text/javascript"">
var _gaq = _gaq || [];
"; 
            }
        }

        public string ScriptEnd
        {
            get 
            {
                return
@"(function() {
  var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
  ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
  var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
})();
</script>";
            }
        }

        public string SetAccount
        {
            get { return "_gaq.push(['_setAccount','{0}']);"; }
        }

        public string RequireEcommerce
        {
            get { return string.Empty; }
        }

        public string SetVariable
        {
            get { return "_gaq.push(['_set','{Name}','{Value}']);"; }
        }

        public string TrackPageView
        {
            get { return "_gaq.push(['_trackPageview']);"; }
        }

        public string TrackVirtualPageView
        {
            get { return "_gaq.push(['_trackPageview','/{0}']);"; }
        }

        public string AddTrans
        {
            get { return "_gaq.push(['_addTrans','{OrderId}','{Affiliation}','{Total:0.00}','{Tax:0.00}','{Shipping:0.00}','{City}','{State}','{Country}']);"; }
        }

        public string AddTransWithCurrency
        {
            get { return 
@"_gaq.push(['_addTrans','{OrderId}','{Affiliation}','{Total:0.00}','{Tax:0.00}','{Shipping:0.00}','{City}','{State}','{Country}']);
_gaq.push(['_set','currencyCode','{Currency}']);"; }
        }

        public string AddItem
        {
            get { return "_gaq.push(['_addItem','{OrderId}','{Sku}','{Name}','{Category}','{Price:0.00}','{Quantity}']);"; }
        }

        public string TrackTrans
        {
            get { return "_gaq.push(['_trackTrans']);"; }
        }

        public string TrackEvent
        {
            get { return "_gaq.push(['_trackEvent','{Category}','{Action}']);"; }
        }

        public string TrackEventWithLabel
        {
            get { return "_gaq.push(['_trackEvent','{Category}','{Action}','{Label}']);"; }
        }

        public string TrackEventWithValue
        {
            get { return "_gaq.push(['_trackEvent','{Category}','{Action}','{Label}',{Value}]);"; }
        }

        public string TrackSocial
        {
            get { return "_gaq.push(['_trackSocial','{Network}','{SocialAction}','{Target}']);"; }
        }

        public string TrackSocialWithPagePath
        {
            get { return "_gaq.push(['_trackSocial','{Network}','{SocialAction}','{Target}','{PagePath}']);"; }
        }
    }
}