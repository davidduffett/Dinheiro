namespace Dinheiro.GoogleAnalytics
{
    public enum GoogleAnalyticsTrackingType
    {
        /// <summary>
        /// Asynchronous tracking using ga.js which will eventually be phased out.
        /// </summary>
        ga_js,
        /// <summary>
        /// Universal Analytics - the "new operating standard" for Google Analytics.
        /// </summary>
        analytics_js
    }
}