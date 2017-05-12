# Dinheiro.RemoveUnnecessaryHeaders
Removes unnecessary ASP.NET, IIS and ASP.NET MVC headers from your application.  Requires IIS 7+, ASP.NET MVC3.

You can also install using [NuGet](http://nuget.org/):
<pre>
  PM> Install-Package Dinheiro.RemoveUnnecessaryHeaders
</pre>

To remove the Server header from all requests you must add runAllManagedModulesForAllRequests="true" to system.webServer/modules in your web.config

## What does it do?
Removes the following HTTP headers from your application responses:

Server: Microsoft-IIS/7.5
X-Powered-By: ASP.NET
X-AspNet-Version: 4.0.30319
X-AspNetMvc-Version: 3.0

## Why?
Because they are unnecessary, and also to obscure the technology your site is using to help prevent attackers trying vulnerabilities in that technology.

See the following great article:
[Shhh... don't let your response headers talk too loudly](http://www.troyhunt.com/2012/02/shhh-dont-let-your-response-headers.html) by [Troy Hunt](http://www.troyhunt.com)
