# `StringValues`

.NET uses the `StringValues` class to represent a collection of string values. Eg. in a query string, multiple parameters may be specified with the same key, in which case `Request.Query["myKey"]` returns a `StringValues` instance with the value of each parameter with the key `myKey`.

A `StringValues` instance may easily be converted to a corresponding `string` value - eg. since `StringValues` uses operator overloading. But sometimes it's would be more usable to get the parameter value(s) as some other type, which is what most of these exrension methods are for.

## ToString...

The `.ToStringArray()` extension methods gets the underlying `StringValues` instance and converts it's values to a corresponding string array. The method will also look for the `,`, ` `, `\r`, `\n` and `\t` separator. Eg. a value like `hello world` will be converted to a string array containing the values `hello` and `world`. The default separators might not alway be suitable, so an overload supports a second parameter with the separator(s) to be used instead.

If converting to a string array isn't suitable, there are similar `.ToStringList()` extension methods.

```cshtml
@using Microsoft.Extensions.Primitives
@using Skybrud.Essentials.AspNetCore
@{

    // Initialize a new instance with two values
    StringValues? values = new StringValues(new[] { "hello-world", "hi-world" });

    // Get the value(s) as a string array
    string[] array1 = values.ToStringArray();
    string[] array2 = values.ToStringArray('-');

    <pre>@array1.Length</pre> // Outputs 2
    <pre>@array2.Length</pre> // Outputs 4

    // Get the value(s) as a string list
    List<string> list1 = values.ToStringList();
    List<string> list2 = values.ToStringList('-');

    <pre>@list1.Count</pre> // Outputs 2
    <pre>@list2.Count</pre> // Outputs 4

}
```
