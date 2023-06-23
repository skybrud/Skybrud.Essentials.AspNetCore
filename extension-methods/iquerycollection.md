# IQueryCollection

#### Strings

```cshtml
@using Microsoft.AspNetCore.Http
@using Microsoft.Extensions.Primitives
@using Skybrud.Essentials.AspNetCore
@{

    IQueryCollection query = new QueryCollection(new Dictionary<string, StringValues> {
        {"id", new StringValues("1")},
        {"ids", new StringValues(new []{"1", "2", "3"})},
        {"moreIds", new StringValues(new []{"5,6", "7", "8"})}
    });

    // Prints "1"
    string? id = query.GetString("id");
    <pre>@id</pre>

    // Prints "1,2,3"
    string[] ids = query.GetStringArray("ids");
    <pre>@string.Join(",", ids)</pre>

    // Prints "5,6,7,8"
    List<string> moreIds = query.GetStringList("moreIds");
    <pre>@string.Join(",", moreIds)</pre>

    // Prints "'nope' not found"
    if (query.TryGetString("nope", out string? nope)) {
        <pre>@nope</pre>
    } else {
        <pre>'nope' not found.</pre>
    }

}
```

#### Int32

```cshtml
@using Microsoft.AspNetCore.Http
@using Microsoft.Extensions.Primitives
@using Skybrud.Essentials.AspNetCore
@{

    IQueryCollection query = new QueryCollection(new Dictionary<string, StringValues> {
        {"id", new StringValues("1")},
        {"ids", new StringValues(new []{"1", "2", "3"})},
        {"moreIds", new StringValues(new []{"5,6", "7", "8"})}
    });

    // Prints "1"
    int id = query.GetInt32("id");
    <pre>@id</pre>

    // Prints "2" (via fallback)
    int id2 = query.GetInt32("id2", 2);
    <pre>@id2</pre>

    // Prints "" (since null is rendered as empty)
    int? id3 = query.GetInt32OrNull("id3");
    <pre>@id3</pre>

    // Prints "1,2,3"
    int[] ids = query.GetInt32Array("ids");
    <pre>@string.Join(",", ids)</pre>

    // Prints "5,6,7,8"
    List<int> moreIds = query.GetInt32List("moreIds");
    <pre>@string.Join(",", moreIds)</pre>

    // Prints "'nope' not found"
    if (query.TryGetInt32("nope", out int? nope)) {
        <pre>@nope</pre>
    } else {
        <pre>'nope' not found.</pre>
    }

}
```

#### Int64

```cshtml
@using Microsoft.AspNetCore.Http
@using Microsoft.Extensions.Primitives
@using Skybrud.Essentials.AspNetCore
@{

    IQueryCollection query = new QueryCollection(new Dictionary<string, StringValues> {
        {"id", new StringValues("1")},
        {"ids", new StringValues(new []{"1", "2", "3"})},
        {"moreIds", new StringValues(new []{"5,6", "7", "8"})}
    });

    // Prints "1"
    long id = query.GetInt64("id");
    <pre>@id</pre>

    // Prints "2" (via fallback)
    long id2 = query.GetInt64("id2", 2);
    <pre>@id2</pre>

    // Prints "" (since null is rendered as empty)
    long? id3 = query.GetInt64OrNull("id3");
    <pre>@id3</pre>

    // Prints "1,2,3"
    long[] ids = query.GetInt64Array("ids");
    <pre>@string.Join(",", ids)</pre>

    // Prints "5,6,7,8"
    List<long> moreIds = query.GetInt64List("moreIds");
    <pre>@string.Join(",", moreIds)</pre>

    // Prints "'nope' not found"
    if (query.TryGetInt64("nope", out long? nope)) {
        <pre>@nope</pre>
    } else {
        <pre>'nope' not found.</pre>
    }

}
```

#### Float

```cshtml
@using Microsoft.AspNetCore.Http
@using Microsoft.Extensions.Primitives
@using Skybrud.Essentials.AspNetCore
@{

    IQueryCollection query = new QueryCollection(new Dictionary<string, StringValues> {
        {"pi", new StringValues("3.14")},
        {"values", new StringValues(new []{"3.14", "6.28", "9.42"})},
        {"otherValues", new StringValues(new []{"3.14,6.28", "9.42"})}
    });

    // Prints "3.14"
    float pi = query.GetFloat("pi");
    <pre>@pi</pre>

    // Prints "1.23" (via fallback)
    float meh = query.GetFloat("meh", 1.23f);
    <pre>@meh</pre>

    // Prints "" (since null is rendered as empty)
    float? meh2 = query.GetFloatOrNull("meh");
    <pre>@meh2</pre>

    // Prints "3.14,6.28,9.42"
    float[] values = query.GetFloatArray("values");
    <pre>@string.Join(",", values)</pre>

    // Prints "3.14,6.28,9.42"
    List<float> otherValues = query.GetFloatList("otherValues");
    <pre>@string.Join(",", otherValues)</pre>

    // Prints "'nope' not found"
    if (query.TryGetFloat("nope", out float? nope)) {
        <pre>@nope</pre>
    } else {
        <pre>'nope' not found.</pre>
    }

}
```

#### Double

```cshtml
@using Microsoft.AspNetCore.Http
@using Microsoft.Extensions.Primitives
@using Skybrud.Essentials.AspNetCore
@{

    IQueryCollection query = new QueryCollection(new Dictionary<string, StringValues> {
        {"pi", new StringValues("3.1415926535")},
        {"values", new StringValues(new []{"3.1415926535", "6.283185307", "9.4247779605"})},
        {"otherValues", new StringValues(new []{"3.1415926535,6.283185307", "9.4247779605"})}
    });

    // Prints "3.1415926535"
    double pi = query.GetDouble("pi");
    <pre>@pi</pre>

    // Prints "1.23" (via fallback)
    double meh = query.GetDouble("meh", 1.23);
    <pre>@meh</pre>

    // Prints "" (since null is rendered as empty)
    double? meh2 = query.GetDoubleOrNull("meh");
    <pre>@meh2</pre>

    // Prints "3.1415926535,6.283185307,9.4247779605"
    double[] values = query.GetDoubleArray("values");
    <pre>@string.Join(",", values)</pre>

    // Prints "3.1415926535,6.283185307,9.4247779605"
    List<double> otherValues = query.GetDoubleList("otherValues");
    <pre>@string.Join(",", otherValues)</pre>

    // Prints "'nope' not found"
    if (query.TryGetDouble("nope", out double? nope)) {
        <pre>@nope</pre>
    } else {
        <pre>'nope' not found.</pre>
    }

}
```

#### Boolean

```cshtml
@using Microsoft.AspNetCore.Http
@using Microsoft.Extensions.Primitives
@using Skybrud.Essentials.AspNetCore
@{

    IQueryCollection query = new QueryCollection(new Dictionary<string, StringValues> {
        {"a", new StringValues("true")},
        {"b", new StringValues("1")}
    });

    // Prints "True"
    bool a = query.GetBoolean("a");
    <pre>@a</pre>

    // Prints "True"
    bool b = query.GetBoolean("b");
    <pre>@b</pre>

    // Prints "False"
    bool c = query.GetBoolean("c");
    <pre>@c</pre>

    // Prints "True" (via fallback)
    bool d = query.GetBoolean("d", true);
    <pre>@d</pre>

    // Prints "" (since null is rendered as empty)
    bool? e = query.GetBooleanOrNull("e");
    <pre>@e</pre>

    // Prints "'nope' not found"
    if (query.TryGetBoolean("nope", out bool? nope)) {
        <pre>@nope</pre>
    } else {
        <pre>'nope' not found.</pre>
    }

}
```
