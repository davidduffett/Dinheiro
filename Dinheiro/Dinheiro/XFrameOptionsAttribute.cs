namespace Dinheiro
{
    public enum XFrameOption
    {
        /// <summary>
        /// No X-Frame-Options header will be set.
        /// </summary>
        NONE,
        /// <summary>
        /// This page cannot be rendered in a frame or iframe.
        /// </summary>
        DENY,
        /// <summary>
        /// This page can be rendered in an iframe, but only on the same site.
        /// </summary>
        SAMEORIGIN
    }

    public class XFrameOptionsAttribute : HttpHeaderAttribute
    {
        public XFrameOptionsAttribute(XFrameOption option)
        {
            if (option == XFrameOption.NONE) return;
            Name = "X-Frame-Options";
            Value = option.ToString();
        }
    }
}