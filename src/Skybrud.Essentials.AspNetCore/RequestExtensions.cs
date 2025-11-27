using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Skybrud.Essentials.AspNetCore;

/// <summary>
/// Various extension methods for working with <see cref="HttpRequest"/>.
/// </summary>
public static class RequestExtensions {

    /// <summary>
    /// Returns the <see cref="Uri"/> of the specified <paramref name="request"/>.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>An instance of <see cref="Uri"/>.</returns>
    public static Uri GetUri(this HttpRequest request) {
        return new UriBuilder {
            Scheme = request.Scheme,
            Host = request.Host.Host,
            Port = request.Host.Port ?? (request.Scheme == "https" ? 443 : 80),
            Path = request.Path,
            Query = request.QueryString.ToUriComponent()
        }.Uri;
    }

    /// <summary>
    /// Returns the value of the <c>Accept</c> HTTP header, or <c>null</c> if the header is not present.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The header value.</returns>
    public static string? GetAcceptTypes(this HttpRequest? request) {
        return request?.Headers["Accept"].FirstOrDefault();
    }

    /// <summary>
    /// Returns the value of the <c>Accept-Encoding</c> HTTP header, or <c>null</c> if the header is not present.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The header value.</returns>
    public static string? GetAcceptEncoding(this HttpRequest? request) {
        return request?.Headers["Accept-Encoding"].FirstOrDefault();
    }

    /// <summary>
    /// Returns the value of the <c>Accept-Language</c> HTTP header, or <c>null</c> if the header is not present.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The header value.</returns>
    public static string? GetAcceptLanguage(this HttpRequest? request) {
        return request?.Headers["Accept-Language"].FirstOrDefault();
    }

    /// <summary>
    /// Gets the remote address of the specified <paramref name="request"/>.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The remote address.</returns>
    public static string? GetRemoteAddress(this HttpRequest? request) {
        return request?.HttpContext.Connection.RemoteIpAddress?.ToString();
    }

    /// <summary>
    /// Returns the value of the <c>Referer</c> HTTP header, or <c>null</c> if the header is not present.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The header value.</returns>
    public static string? GetReferrer(this HttpRequest? request) {
        return request?.Headers["Referer"].FirstOrDefault();
    }

    /// <summary>
    /// Gets the user agent of the specified <paramref name="request"/>.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The user agent.</returns>
    public static string? GetUserAgent(this HttpRequest? request) {
        return request?.Headers["User-Agent"].FirstOrDefault();
    }

    /// <summary>
    /// Returns the value of the <c>X-Forwarded-For</c> header, or <c>null</c> if the header is not present.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The value of the header, or <c>null</c> if not present.</returns>
    public static string? GetForwardedFor(this HttpRequest? request) {
        return request?.Headers["X-Forwarded-For"].FirstOrDefault();
    }

    /// <summary>
    /// Attempts to get the value of the <c>X-Forwarded-For</c> header.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="result">When this method returns, holds the value of the <c>X-Forwarded-For</c> header if successful; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
    public static bool TryGetForwardedFor(this HttpRequest? request, [NotNullWhen(true)] out string? result) {
        result = request?.Headers.TryGetValue("X-Forwarded-For", out StringValues values) is true ? values.FirstOrDefault() : null;
        return !string.IsNullOrEmpty(result);
    }

    /// <summary>
    /// Attempts to get the value of the <c>X-Forwarded-Host</c> header.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="result">When this method returns, holds the value of the <c>X-Forwarded-Host</c> header if successful; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
    public static bool TryGetForwardedHost(this HttpRequest? request, [NotNullWhen(true)] out string? result) {
        result = request?.Headers.TryGetValue("X-Forwarded-Host", out StringValues values) is true ? values.FirstOrDefault() : null;
        return !string.IsNullOrEmpty(result);
    }

    /// <summary>
    /// Attempts to get the value of the <c>X-Forwarded-Proto</c> header.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="result">When this method returns, holds the value of the <c>X-Forwarded-Proto</c> header if successful; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
    public static bool TryGetForwardedProto(this HttpRequest? request, [NotNullWhen(true)] out string? result) {
        result = request?.Headers.TryGetValue("X-Forwarded-Proto", out StringValues values) is true ? values.FirstOrDefault() : null;
        return !string.IsNullOrEmpty(result);
    }

    /// <summary>
    /// Attempts to get the value of the <c>Referer</c> header.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="result">When this method returns, holds the value of the <c>Referer</c> header if successful; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
    public static bool TryGetReferrer(this HttpRequest? request, [NotNullWhen(true)] out string? result) {
        result = request?.Headers.TryGetValue("Referer", out StringValues values) is true ? values.FirstOrDefault() : null;
        return !string.IsNullOrEmpty(result);
    }

    /// <summary>
    /// Attempts to get the remote address of the specified <paramref name="request"/>.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="result">When this method returns, holds the remote address if successful; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
    public static bool TryGetRemoteAddress(this HttpRequest? request, [NotNullWhen(true)] out string? result) {
        result = request?.HttpContext.Connection.RemoteIpAddress?.ToString();
        return !string.IsNullOrEmpty(result);
    }

    /// <summary>
    /// Attempts to get the value of the <c>User-Agent</c> header.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="result">When this method returns, holds the value of the <c>User-Agent</c> header if successful; otherwise, <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
    public static bool TryGetUserAgent(this HttpRequest? request, [NotNullWhen(true)] out string? result) {
        result = request?.Headers.TryGetValue("User-Agent", out StringValues values) is true ? values.FirstOrDefault() : null;
        return !string.IsNullOrEmpty(result);
    }

}