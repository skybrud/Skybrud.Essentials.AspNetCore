# Newtonsoft

The package contains various different logic for working with Newtonsoft.Json (also referrred to as JSON.net).

## NewtonsoftJsonResult

By default, if you return a model from an API controller, the model will be serialized using `Microsoft.Text.Json`.

If you wish to use JSON.net for serialization instead, you can use the `Skybrud.Essentials.AspNetCore.Models.Json.JsonNetResult` class to wrap your models.

The `Skybrud.Essentials.AspNetCore.Json.Newtonsoft.NewtonsoftJsonResult` class features a number of static initializers for the most common status codes:

### 200 OK

```csharp
return NewtonsoftJsonResult.Ok(new { hello = "world" });
```

returns:

```json
{
    "hello": "world"
}
```

### 201 Created

```csharp
return NewtonsoftJsonResult.Created(new { hello = "world" });
```

returns:

```json
{
    "hello": "world"
}
```

### 400 Bad Request

```csharp
return NewtonsoftJsonResult.BadRequest("Nope");
```

returns:

```json
{
    "meta": {
        "code": 400,
        "error": "Nope"
    }
}
```

### 401 Unauthorized

```csharp
return NewtonsoftJsonResult.Unauthorized("Not authorized");
```

returns:

```json
{
    "meta": {
        "code": 401,
        "error": "Not authorized"
    }
}
```

### 403 Forbidden

```csharp
return NewtonsoftJsonResult.Forbidden("Forbidden");
```

returns:

```json
{
    "meta": {
        "code": 403,
        "error": "Forbidden"
    }
}
```

### 404 Not Found

```csharp
return NewtonsoftJsonResult.NotFound("Not Found");
```

returns:

```json
{
    "meta": {
        "code": 404,
        "error": "Not Found"
    }
}
```

### 500 Internal Server Error

```csharp
return NewtonsoftJsonResult.InternalError("Computer says no...");
```

returns:

```json
{
    "meta": {
        "code": 500,
        "error": "Computer says no..."
    }
}
```

## NewtonsoftJsonOnlyConfiguration

The `NewtonsoftJsonOnlyConfigurationAttribute` class is a special attribute that you may add to your API controllers, which then will result in the value being serlaized with `Newtonsoft.Json`. Eg. such as:

```csharp
[NewtonsoftJsonOnlyConfiguration]
public class MyController { }
```

By default properties will be converted to camel case, but a specific casing format may be specified:

```csharp
[NewtonsoftJsonOnlyConfiguration(TextCasing.KebabCase)]
public class MyController { }
```

and even the formatting:

```csharp
[NewtonsoftJsonOnlyConfiguration(TextCasing.KebabCase, Formatting.Indented)]
public class MyController { }
```
