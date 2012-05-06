namespace Dinheiro
{
    /// <summary>
    /// Specifies that the action result is to be rendered within an iframe,
    /// but ensures that it can only be rendered within an iframe on the same site
    /// by adding the HTTP response header: X-Frame-Options: SAMEORIGIN
    /// This can help to avoid clickjacking attacks, by ensuring your content is not embedded into other sites.
    /// </summary>
    public class IFrameAttribute : XFrameOptionsAttribute
    {
        public IFrameAttribute() : base(XFrameOption.SAMEORIGIN)
        {
        }
    }
}