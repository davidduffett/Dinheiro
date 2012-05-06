# Dinheiro.P3PPolicy
Adds a P3P Policy to your ASP.NET application.  Requires IIS 7+.

You can also install using [NuGet](http://nuget.org/):
<pre>
  PM> Install-Package Dinheiro.GoogleAnalytics
</pre>

Also, see the **Dinheiro.P3PPolicy.Example** MVC app for detailed examples.

## What does it do?
This package does 2 very simple things:

1. Adds a P3P Policy Reference file to your project (at `w3c/p3p.xml`)
2. Adds a P3P Compact Policy to your HTTP response headers (in `web.config`):

Here is where the compact policy is added:

	<system.webServer>
		<httpProtocol>
			<customHeaders>
				<!-- Important: ensure your compact policy matches your business privacy policy -->
        		<add name="P3P" value="policyref=&quot;/w3c/p3p.xml&quot;, CP=&quot;IDC DSP COR CUR DEV PSA IVA IVD CONo HIS OUR DEL BUS UNI&quot;" />
			</customHeaders>
		</httpProtocol>
	</system.webServer>

## Now this is important: review before using!
Note that this package adds a P3P policy but that policy **might not match your business privacy practices**.  It is extremely important that you review both the Policy Reference File (under the `w3c` folder) and the P3P Compact Policy header (in `web.config`) before using it on your website.

You can find out what each element of the policy means here:

* [W3C P3P Specification](http://www.w3.org/TR/P3P/)
* [A great P3P Compact Policy definition document](http://www.p3pwriter.com/LRN_111.asp)
* [W3C P3P Validator](http://www.w3.org/P3P/validator.html)

## What is a P3P policy?
P3P stands for the Platform for Privacy Preferences Project, and is a protocol allowing websites to declare, digitally, their intended use of information they collection about users.  It is basically your privacy policy turned into a machine readable form.

It is also a dead standard - development has ceased and Internet Explorer is the only browser that supports it.

## So why do I need it?
I wish you didn't need it!  Unfortunately if you don't have it, you may find that some of your Internet Explorer users will have their cookies blocked.

This is because Internet Explorer allows users to block cookies for sites that don't include a P3P policy.

If the user has their privacy level set to "High" then your cookies will be accepted by Internet Explorer if you have a P3P policy.

Want a real world example of the problems caused by this?  See [this StackOverflow question](http://stackoverflow.com/questions/389456/cookie-blocked-not-saved-in-iframe-in-internet-explorer).
