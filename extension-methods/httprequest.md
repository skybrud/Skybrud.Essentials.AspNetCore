# HttpRequest

The `RequestExtensions` class contains a number of extension methods for ASP.NET Core's `HttpRequest` class:

## `GetUri`

In ASP.NET Core, there isn't a direct way for getting the `Uri` of a request, but the different parts of information is still available through other properties on the `HttpRequest` instance. The `.GetUri()` extension method uses this information for returning a new `Uri` instance with this information.

```cshtml
@using Skybrud.Essentials.AspNetCore

@{

    // Get the URI of the current request
    Uri uri = Context.Request.GetUri();

    <pre>Scheme: @uri.Scheme</pre>
    <pre>Host: @uri.Host</pre>
    <pre>Port: @uri.Port</pre>
    <pre>PathAndQuery: @uri.PathAndQuery</pre>
    <pre>ToString: @uri.ToString()</pre>
    <pre>Authority: @uri.GetLeftPart(UriPartial.Authority)</pre>

}
```

## `GetAcceptEncoding`

Returns the value of the `Accept-Encoding` header, or `null` if the header wasn't found.

```cshtml
@using Skybrud.Essentials.AspNetCore
<pre>Accept Encoding: @Context.Request.GetAcceptEncoding()</pre>
```

## `GetAcceptLanguage`

Returns the value of the `Accept-Encoding` header, or `null` if the header wasn't found.

```cshtml
@using Skybrud.Essentials.AspNetCore
<pre>Accept Languages: @Context.Request.GetAcceptLanguage()</pre>
```

## `GetAcceptTypes`

Returns the value of the `Accept` header, or `null` if the header wasn't found.

```cshtml
@using Skybrud.Essentials.AspNetCore
<pre>Accept Types: @Context.Request.GetAcceptTypes()</pre>
```

## `GetReferrer`

Returns the value of the `Referer` header, or `null` if the header wasn't found. (yes, it's spelled incorrectly in the HTTP specification)

```cshtml
@using Skybrud.Essentials.AspNetCore
<pre>Referrer: @Context.Request.GetReferrer()</pre>
```

## `GetRemoteAddress`

Returns the remote address of the request.

```cshtml
@using Skybrud.Essentials.AspNetCore
<pre>Remote Address: @Context.Request.GetRemoteAddress()</pre>
```

## GetUserAgent

Returns the value of the `Referer` header, or `null` if the header wasn't found.

```cshtml
@using Skybrud.Essentials.AspNetCore
<pre>User Agent: @Context.Request.GetUserAgent()</pre>
```
