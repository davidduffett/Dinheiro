using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Dinheiro.GoogleAnalytics.Utilities;

namespace Dinheiro.GoogleAnalytics
{
    public interface IGoogleAnalytics
    {
        /// <summary>
        /// Sets a custom virtual page name for this request.
        /// If this is not provided, Google Analytics uses the normal page URL.
        /// A slash is inserted at the beginning of the URL, if not already provided.
        /// </summary>
        string VirtualPageUrl { get; set; }

        /// <summary>
        /// Sets the currency of the transactions being added in this request.
        /// </summary>
        /// <param name="currencyCode">ISO 4217 currency code.</param>
        void SetCurrency(string currencyCode);

        /// <summary>
        /// Creates a transaction object with the given values.
        /// </summary>
        /// <param name="orderId">Internal unique order id number for this transaction.</param>
        /// <param name="total">Total amount of the transaction.</param>
        /// <param name="tax">Optional tax amount of the transaction.</param>
        /// <param name="shipping">Optional shipping charge for the transaction.</param>
        /// <param name="affiliation">Optional. Partner or store affiliation (undefined if absent).</param>
        /// <param name="city">Optional city to associate with transaction.</param>
        /// <param name="state">Optional state to associate with transaction.</param>
        /// <param name="country">Optional country to associate with transaction.</param>
        void AddTransaction(object orderId, decimal total,
                            decimal? tax = null, decimal? shipping = null,
                            string affiliation = null, string city = null, string state = null, string country = null);

        /// <summary>
        /// Use this method to track items purchased by visitors to your ecommerce site.
        /// </summary>
        /// <param name="sku">Item's SKU code.</param>
        /// <param name="name">Product name. Required to see data in the product detail report.</param>
        /// <param name="price">Product price.</param>
        /// <param name="quantity">Purchase quantity.</param>
        /// <param name="orderId">Optional order ID of the transaction to associate with item.</param>
        /// <param name="category">Product category.</param>
        void AddItem(string sku, string name, decimal price, int quantity,
                     object orderId = null, string category = null);

        /// <summary>
        /// Tracks a custom event.
        /// </summary>
        /// <param name="category">The general category of the event.</param>
        /// <param name="action">The action of the event.</param>
        /// <param name="label">An optional descriptor for the event.</param>
        /// <param name="value">An optional value associated with the event.</param>
        /// <param name="nonInteraction">Defaults to false.  If set to true, the event will not be used in bounce rate calculations.</param>
        void TrackEvent(string category, string action,
                                        string label = null, int? value = null, bool nonInteraction = false);

        /// <summary>
        /// Records clicks on social sharing buttons other than Google +1.
        /// </summary>
        /// <param name="network">The social network. <example>facebook</example></param>
        /// <param name="socialAction">The type of action that happens.  <example>like</example></param>
        /// <param name="target">Optional subject of the action, usually the URL that is being shared.</param>
        /// <param name="pagePath">Optional page path from which the action occurred.  Only needed if you use virtual page paths.</param>
        void TrackSocial(string network, string socialAction,
                                         string target = null, string pagePath = null);

        IEnumerable<GaVariable> Variables { get; } 
        IEnumerable<GaEvent> Events { get; }
        IEnumerable<GaSocialEvent> SocialEvents { get; }
        IEnumerable<GaTransaction> Transactions { get; }
        IEnumerable<GaItem> Items { get; }
        string Currency { get; }
    }

    public class GoogleAnalytics : IGoogleAnalytics
    {
        public const string StateKey = "__googleanalytics__";
        public static IStateStorage StateStorage = new HttpContextStateStorage();

        /// <summary>
        /// Your Google Analytics account, or web property ID.
        /// </summary>
        public static string Account = "UA-XXXXX-X";

        /// <summary>
        /// Set to whichever type of Google Analytics your profile is setup as.
        /// This determines how the scripts for web tracking are rendered.
        /// </summary>
        public static GoogleAnalyticsTrackingType TrackingType = GoogleAnalyticsTrackingType.ga_js;

        /// <summary>
        /// Enable/disable the Google Analytics Display Features plugin.
        /// See https://developers.google.com/analytics/devguides/collection/analyticsjs/display-features for more information.
        /// </summary>
        public static bool EnableDisplayFeatures = false;

        static IScripts Scripts
        {
            get
            {
                return TrackingType == GoogleAnalyticsTrackingType.ga_js
                           ? ScriptImplementations.Asynchronous
                           : ScriptImplementations.Universal;
            }
        }

        /// <summary>
        /// GoogleAnalytics state for the current request.
        /// </summary>
        public static IGoogleAnalytics Current
        {
            get
            {
                var current = StateStorage.Get<IGoogleAnalytics>(StateKey);
                if (current == null)
                {
                    current = new GoogleAnalytics();
                    StateStorage.Set(StateKey, current);
                }
                return current;
            }
        }

        /// <summary>
        /// Clears the current GoogleAnalytics state.
        /// </summary>
        public static void Reset()
        {
            StateStorage.Remove(StateKey);
        }

        /// <summary>
        /// Renders the required JavaScript onto the page for Google Analytics tracking.
        /// Recommended to include this within the head element of the page.
        /// </summary>
        public static IHtmlString Render()
        {
            var ga = new StringBuilder(Scripts.ScriptStart);

            ga.AppendLine(string.Format(Scripts.SetAccount, Account));

            // Variables
            foreach (var gaVariable in Current.Variables)
                ga.AppendLine(Scripts.SetVariable.FormatWithForJavascript(gaVariable));

            // Events
            foreach (var gaEvent in Current.Events)
                ga.AppendLine(gaEvent.Value.HasValue ? Scripts.TrackEventWithValue.FormatWithForJavascript(gaEvent)
                    : !string.IsNullOrWhiteSpace(gaEvent.Label) ? Scripts.TrackEventWithLabel.FormatWithForJavascript(gaEvent)
                    : Scripts.TrackEvent.FormatWithForJavascript(gaEvent));

            // Social
            foreach (var socialEvent in Current.SocialEvents)
                ga.AppendLine(string.IsNullOrWhiteSpace(socialEvent.PagePath)
                    ? Scripts.TrackSocial.FormatWithForJavascript(socialEvent)
                    : Scripts.TrackSocialWithPagePath.FormatWithForJavascript(socialEvent));

            // Transaction
            if (Current.Transactions.Any())
            {
                ga.AppendLine(Scripts.RequireEcommerce);
                foreach (var trans in Current.Transactions)
                    ga.AppendLine(string.IsNullOrWhiteSpace(trans.Currency)
                        ? Scripts.AddTrans.FormatWithForJavascript(trans)
                        : Scripts.AddTransWithCurrency.FormatWithForJavascript(trans));

                // Items
                if (Current.Items.Any())
                    foreach (var item in Current.Items)
                        ga.AppendLine(Scripts.AddItem.FormatWithForJavascript(item));
            }

            // Display Features Plugin
            if (EnableDisplayFeatures)
                ga.AppendLine(Scripts.RequireDisplayFeatures);

            // Page view
            ga.AppendLine(string.IsNullOrWhiteSpace(Current.VirtualPageUrl)
                ? Scripts.TrackPageView
                : string.Format(Scripts.TrackVirtualPageView, Current.VirtualPageUrl.TrimStart('/')));

            // Track transaction
            if (Current.Transactions.Any())
                ga.AppendLine(Scripts.TrackTrans);

            ga.AppendLine(Scripts.ScriptEnd);
            return new HtmlString(ga.ToString());
        }
        
        readonly IList<GaVariable> _variables = new List<GaVariable>();
        public IEnumerable<GaVariable> Variables
        {
            get { return _variables; }
        }

        readonly IList<GaEvent> _events = new List<GaEvent>();
        public IEnumerable<GaEvent> Events
        {
            get { return _events; }
        }

        readonly IList<GaSocialEvent> _socialEvents = new List<GaSocialEvent>();
        public IEnumerable<GaSocialEvent> SocialEvents
        {
            get { return _socialEvents; }
        }

        readonly IList<GaItem> _items = new List<GaItem>();
        public IEnumerable<GaItem> Items
        {
            get { return _items; }
        }

        readonly IList<GaTransaction> _transactions = new List<GaTransaction>();
        public IEnumerable<GaTransaction> Transactions
        {
            get { return _transactions; }
        }

        public string Currency { get; set; }

        public string VirtualPageUrl { get; set; }

        public void TrackEvent(string category, string action,
                               string label = null, int? value = null, bool nonInteraction = false)
        {
            _events.Add(new GaEvent
                            {
                                Category = category,
                                Action = action,
                                Label = label,
                                Value = value,
                                NonInteraction = nonInteraction
                            });
        }

        public void TrackSocial(string network, string socialAction,
                                string target = null, string pagePath = null)
        {
            _socialEvents.Add(new GaSocialEvent
                                  {
                                      Network = network,
                                      SocialAction = socialAction,
                                      Target = target,
                                      PagePath = pagePath
                                  });
        }

        public void SetCurrency(string currencyCode)
        {
            Currency = currencyCode;
            foreach (var transaction in _transactions)
                transaction.Currency = Currency;
        }

        public void AddTransaction(object orderId, decimal total, 
                                   decimal? tax = null, decimal? shipping = null, 
                                   string affiliation = null, string city = null, string state = null, string country = null)
        {
            _transactions.Add(new GaTransaction
                                  {
                                      OrderId = orderId,
                                      Total = total,
                                      Tax = tax,
                                      Shipping = shipping,
                                      Affiliation = affiliation,
                                      City = city,
                                      State = state,
                                      Country = country,
                                      Currency = Currency
                                  });
        }

        public void AddItem(string sku, string name, decimal price, int quantity, 
                            object orderId = null, string category = null)
        {
            _items.Add(new GaItem
                           {
                               Sku = sku,
                               Name = name,
                               Price = price,
                               Quantity = quantity,
                               OrderId = orderId,
                               Category = category
                           });
        }
    }
}
