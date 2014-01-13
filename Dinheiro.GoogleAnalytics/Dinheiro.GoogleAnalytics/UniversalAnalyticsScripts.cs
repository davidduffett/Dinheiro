namespace Dinheiro.GoogleAnalytics
{
    class UniversalAnalyticsScripts : IScripts
    {
        public string ScriptStart
        {
            get
            {
                return
@"<script>
(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
})(window,document,'script','//www.google-analytics.com/analytics.js','ga');
";
            }
        }

        public string ScriptEnd
        {
            get { return @"</script>"; }
        }

        public string SetAccount
        {
            get { return "ga('create','{0}','auto');"; }
        }

        public string RequireEcommerce
        {
            get { return "ga('require','ecommerce','ecommerce.js');"; }
        }

        public string SetVariable
        {
            get { return "ga('set','{Name}','{Value}');"; }
        }

        public string TrackPageView
        {
            get { return "ga('send','pageview');"; }
        }

        public string TrackVirtualPageView
        {
            get { return "ga('send','pageview','/{0}');"; }
        }

        public string AddTrans
        {
            get { return "ga('ecommerce:addTransaction',{{'id':'{OrderId}','affiliation':'{Affiliation}','revenue':'{Total:0.00}','shipping':'{Shipping:0.00}','tax':'{Tax:0.00}'}});"; }
        }

        public string AddTransWithCurrency
        {
            get { return "ga('ecommerce:addTransaction',{{'id':'{OrderId}','affiliation':'{Affiliation}','revenue':'{Total:0.00}','shipping':'{Shipping:0.00}','tax':'{Tax:0.00}','currency':'{Currency}'}});"; }
        }

        public string AddItem
        {
            get { return "ga('ecommerce:addItem',{{'id':'{OrderId}','name':'{Name}','sku':'{Sku}','category':'{Category}','price':'{Price:0.00}','quantity':'{Quantity}'}});"; }
        }

        public string TrackTrans
        {
            get { return "ga('ecommerce:send');"; }
        }

        public string TrackEvent
        {
            get { return "ga('send','event','{Category}','{Action}','{Label}',{Value});"; }
        }

        public string TrackSocial
        {
            get { return "ga('send','social','{Network}','{SocialAction}','{Target}');"; }
        }

        public string TrackSocialWithPagePath
        {
            get { return "ga('send','social','{Network}','{SocialAction}','{Target}',{{'page':'{PagePath}'}});"; }
        }
    }
}