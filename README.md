# Dinheiro - Ecommerce Helpers for ASP.NET MVC

Dinheiro is a set of handy ecommerce helpers for developers integrating with third-party services.

The aim is to cover the essentials that all ecommerce websites need.

Note that in order to use these, you need to have relevant accounts in place with each third party.

## Ready to go
### [Dinheiro.GoogleAnalytics](https://github.com/davidduffett/Dinheiro/tree/master/Dinheiro.GoogleAnalytics)
Allows you to easily track page views, events, social actions and ecommerce by using action filter attributes or an easy API.

You can install using [NuGet](http://nuget.org/):
<pre>
  PM> Install-Package Dinheiro.GoogleAnalytics
</pre>

### [Dinheiro.P3PPolicy](https://github.com/davidduffett/Dinheiro/tree/master/Dinheiro.P3PPolicy)
Adds a P3P Policy to your website (stop Internet Explorer from blocking your cookies!).
<pre>
  PM> Install-Package Dinheiro.P3PPolicy
</pre>

### Dinheiro.Core
The Dinheiro.Core library contains a collection of optional handy MVC helpers:

#### [IFrame] and [NoIFrame] attributes
By decorating your controller or action (or applying globally) with `[NoIFrame]` the page will be prevented from being embedded within `<frame>` or `<iframe>` elements.
This can help avoid **clickjacking attacks**.  It does this by adding the HTTP header `X-Frame-Options: DENY`.

	[NoIFrame]
	public class HomeController {}

You can then override this to allow a page to be displayed within an `<iframe>`, but only on **your own site** by using the `[IFrame]` attribute.
This will add the HTTP header `X-Frame-Options: SAMEORIGIN`.

	[IFrame]
	public ActionResult MyIFrame()
	{
	}

More details on X-Frame-Options and clickjacking can be found here: [https://developer.mozilla.org/en/The_X-FRAME-OPTIONS_response_header](https://developer.mozilla.org/en/The_X-FRAME-OPTIONS_response_header)

## On the roadmap
Facilities for host name redirects, Flash cross-domain policies, open search, card processing, PayPal, Google Checkout, online chat, abandoned basket emails and more!
