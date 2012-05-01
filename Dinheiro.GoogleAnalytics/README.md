# Dinheiro.GoogleAnalytics
ASP.NET MVC Helpers for Google Analytics.

Allows you to easily track page views, events, social actions and ecommerce by using action filter attributes or an easy API.

You can also install using [NuGet](http://nuget.org/):
<pre>
  PM> Install-Package Dinheiro.GoogleAnalytics
</pre>

Also, see the **Dinheiro.GoogleAnalytics.Example** MVC app for detailed examples.

## Usage:
In your application startup, be sure to set your Google Analytics Account ID:

	using Dinheiro.GoogleAnalytics;
	...
	GoogleAnalytics.Account = "UA-12345-6";

In your `_Layout.cshtml` file:

	@using Dinheiro.GoogleAnalytics
	<html>
	<head>
		...
		@GoogleAnalytics.Render()
	</head>
	<body>
		...
	</body>
	</html>

This will automatically output the required Google Analytics scripts to start tracking your page views.

### Tracking an event
Use the handy action filter:

	[GoogleAnalyticsEvent(Category = "Event Category", Action = "Event Action")]
	public ActionResult Index()
	{
		return View();
	}

You can even ask it to use values from query parameters:

	[GoogleAnalyticsEvent(CategoryParameter = "category", Action = "Event Action", LabelParameter = "label")]
	public ActionResult Index(string category, string label)
	{
		return View();
	}

Or call the API directly:

	public ActionResult Index()
	{
		GoogleAnalytics.Current.TrackEvent(category, action, label, value);
	}
	
