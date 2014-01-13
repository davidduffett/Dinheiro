namespace Dinheiro.GoogleAnalytics
{
    public class GaVariable
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class GaEvent
    {
        public string Category { get; set; }
        public string Action { get; set; }
        public string Label { get; set; }
        public int? Value { get; set; }
        public bool NonInteraction { get; set; }
    }

    public class GaSocialEvent
    {
        public string Network { get; set; }
        public string SocialAction { get; set; }
        public string Target { get; set; }

        string _pagePath;

        /// <summary>
        /// Ensure forward slash is added to the page path.
        /// </summary>
        public string PagePath
        {
            get { return _pagePath; }
            set { _pagePath = value == null || value.StartsWith("/")
                    ? value 
                    : "/" + value; }
        }
    }

    public class GaItem
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public object OrderId { get; set; }
        public string Category { get; set; }
    }

    public class GaTransaction
    {
        public object OrderId { get; set; }
        public decimal Total { get; set; }
        public decimal? Tax { get; set; }
        public decimal? Shipping { get; set; }
        public string Affiliation { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
    }
}