﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Text;
using Skybrud.Essentials.Strings.Extensions;
using System.Diagnostics.CodeAnalysis;
using Skybrud.Essentials.Strings;
using System.Collections.Generic;
using Skybrud.Essentials.Enums;

// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator

namespace Skybrud.Essentials.AspNetCore {

    /// <summary>
    /// Various extension methods for working with ASP.NET Core query strings.
    /// </summary>
    public static class QueryStringExtensions {

        #region GetString...

        /// <summary>
        /// Returns the value of the first query string component with the specified <paramref name="key"/>, or
        /// <c>null</c> if not found.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <returns>The value of the first query string component matching <paramref name="key"/>; otherwise <c>null</c>.</returns>
        public static string? GetString(this IQueryCollection? query, string key) {
            return query?[key].FirstOrDefault();
        }

        /// <summary>
        /// Returns the value of the first query string component with the specified <paramref name="key"/>, or
        /// <paramref name="fallback"/> if not found.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="fallback">The fallback value.</param>
        /// <returns>The value of the first query string component matching <paramref name="key"/>; otherwise <paramref name="fallback"/>.</returns>
        public static string GetString(this IQueryCollection? query, string key, string fallback) {
            return query.GetString(key).HasValue(out string? value) ? value : fallback;
        }

        /// <summary>
        /// Returns an array of <see cref="string"/> values representing the values of each query string component
        /// matching the specified <paramref name="key"/>.
        ///
        /// Notice that this method support both multiple query string components with the same <paramref name="key"/>
        /// as well as query string components where the value is a comma separated string or similar. Supported
        /// separators are comma (<c>,</c>), space (<c> </c>), carriage return (<c>\r</c>), new line (<c>\n</c>) and
        /// tab (<c>\t</c>).
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component(s).</param>
        /// <returns>An array of <see cref="string"/>.</returns>
        public static string[] GetStringArray(this IQueryCollection? query, string key) {
            return (query?[key]).ToStringArray();
        }

        /// <summary>
        /// Returns an array of <see cref="string"/> values representing the values of each query string component
        /// matching the specified <paramref name="key"/>.
        ///
        /// Notice that this method support both multiple query string components with the same <paramref name="key"/>
        /// as well as query string components where the value is separated by one of the following <paramref name="separators"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component(s).</param>
        /// <param name="separators">An array of supported separators.</param>
        /// <returns>An array of <see cref="string"/>.</returns>
        public static string[] GetStringArray(this IQueryCollection? query, string key, params char[] separators) {
            return (query?[key]).ToStringArray(separators);
        }

        /// <summary>
        /// Returns a list of <see cref="string"/> representing the values of each query string component
        /// matching the specified <paramref name="key"/>.
        ///
        /// Notice that this method support both multiple query string components with the same <paramref name="key"/>
        /// as well as query string components where the value is a comma separated string or similar. Supported
        /// separators are comma (<c>,</c>), space (<c> </c>), carriage return (<c>\r</c>), new line (<c>\n</c>) and
        /// tab (<c>\t</c>).
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component(s).</param>
        /// <returns>An array of <see cref="string"/>.</returns>
        public static List<string> GetStringList(this IQueryCollection? query, string key) {
            return (query?[key]).ToStringList();
        }

        /// <summary>
        /// Returns a list of <see cref="string"/> values representing the values of each query string component
        /// matching the specified <paramref name="key"/>.
        ///
        /// Notice that this method support both multiple query string components with the same <paramref name="key"/>
        /// as well as query string components where the value is separated by one of the following <paramref name="separators"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component(s).</param>
        /// <param name="separators">An array of supported separators.</param>
        /// <returns>An array of <see cref="string"/>.</returns>
        public static List<string> GetStringList(this IQueryCollection? query, string key, params char[] separators) {
            return (query?[key]).ToStringList(separators);
        }

        /// <summary>
        /// Attempts to get the string value of the string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="result">When this method returns, holds the string value if successful; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetString(this IQueryCollection? query, string key, [NotNullWhen(true)] out string? result) {

            if (query is not null && query.TryGetValue(key, out StringValues value) && value.FirstOrDefault() is { } str && !string.IsNullOrWhiteSpace(str)) {
                result = str;
                return true;
            }

            result = null;
            return false;

        }

        #endregion

        #region GetInt32...

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as an <see cref="int"/>. If a matching query string component isn't found, or the value could not
        /// be converted to an integer, <c>0</c> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <returns>The converted <see cref="int"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise <c>0</c>.</returns>
        public static int GetInt32(this IQueryCollection? query, string key) {
            return query == null ? 0 : query[key].ToInt32();
        }

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as an <see cref="int"/>. If a matching query string component isn't found, or the value could not
        /// be converted to an integer, <paramref name="fallback"/> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="fallback">The fallback value.</param>
        /// <returns>The converted <see cref="int"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise <paramref name="fallback"/>.</returns>
        public static int GetInt32(this IQueryCollection? query, string key, int fallback) {
            return query == null ? fallback : query[key].ToInt32(fallback);
        }

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as an <see cref="int"/>. If a matching query string component isn't found, or the value could not
        /// be converted to an integer, <see langword="null"/> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <returns>The converted <see cref="int"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise <see langword="null"/>.</returns>
        public static int? GetInt32OrNull(this IQueryCollection? query, string key) {
            return query?[key].ToInt32OrNull();
        }

        /// <summary>
        /// Attempts to get the <see cref="int"/> value of the string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="result">When this method returns, holds the <see cref="int"/> value if successful; otherwise, <c>0</c>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetInt32(this IQueryCollection? query, string key, out int result) {
            return StringUtils.TryParseInt32(GetString(query, key), out result);
        }

        /// <summary>
        /// Attempts to get the <see cref="int"/> value of the string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="result">When this method returns, holds the <see cref="int"/> value if successful; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetInt32(this IQueryCollection? query, string key, [NotNullWhen(true)] out int? result) {
            return StringUtils.TryParseInt32(GetString(query, key), out result);
        }

        /// <summary>
        /// Returns an <see cref="int"/> array based on the values of each query string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string components.</param>
        /// <returns>An <see cref="int"/> array representing the converted values.</returns>
        /// <remarks>The value of each query string component may themselves be a separated list of <see cref="int"/>
        /// values - eg. separated by commas. Values that can not be converted to a corresponding <see cref="int"/>
        /// value will be ignored.</remarks>
        public static int[] GetInt32Array(this IQueryCollection? query, string key) {
            return query == null ? Array.Empty<int>() : query[key].ToInt32Array();
        }

        /// <summary>
        /// Returns an <see cref="int"/> array based on the values of each query string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string components.</param>
        /// <returns>An <see cref="int"/> list representing the converted values.</returns>
        /// <remarks>The value of each query string component may themselves be a separated list of <see cref="int"/>
        /// values - eg. separated by commas. Values that can not be converted to a corresponding <see cref="int"/>
        /// value will be ignored.</remarks>
        public static List<int> GetInt32List(this IQueryCollection? query, string key) {
            return query?[key].ToInt32List() ?? new List<int>();
        }

        #endregion

        #region GetInt64...

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as a <see cref="long"/>. If a matching query string component isn't found, or the value could not
        /// be converted to a <see cref="long"/>, <c>0</c> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <returns>The converted <see cref="long"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise <c>0</c>.</returns>
        public static long GetInt64(this IQueryCollection? query, string key) {
            return query == null ? 0 : query[key].ToInt64();
        }

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as a <see cref="long"/>. If a matching query string component isn't found, or the value could not
        /// be converted to a <see cref="long"/>, <paramref name="fallback"/> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="fallback">The fallback value.</param>
        /// <returns>The converted <see cref="long"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise <paramref name="fallback"/>.</returns>
        public static long GetInt64(this IQueryCollection? query, string key, long fallback) {
            return query == null ? fallback : query[key].ToInt64(fallback);
        }

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as a <see cref="long"/>. If a matching query string component isn't found, or the value could not
        /// be converted to an integer, <see langword="null"/> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <returns>The converted <see cref="long"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise <see langword="null"/>.</returns>
        public static long? GetInt64OrNull(this IQueryCollection? query, string key) {
            return query?[key].ToInt64OrNull();
        }

        /// <summary>
        /// Attempts to get the <see cref="long"/> value of the string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="result">When this method returns, holds the <see cref="long"/> value if successful; otherwise, <c>0</c>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetInt64(this IQueryCollection? query, string key, out long result) {
            return StringUtils.TryParseInt64(GetString(query, key), out result);
        }

        /// <summary>
        /// Attempts to get the <see cref="long"/> value of the string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="result">When this method returns, holds the <see cref="long"/> value if successful; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetInt64(this IQueryCollection? query, string key, [NotNullWhen(true)] out long? result) {
            return StringUtils.TryParseInt64(GetString(query, key), out result);
        }

        /// <summary>
        /// Returns a <see cref="long"/> array based of the values of each query string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string components.</param>
        /// <returns>A <see cref="long"/> array representing the converted values.</returns>
        /// <remarks>The value of each query string component may themselves be a separated list of <see cref="long"/>
        /// values - eg. separated by commas. Values that can not be converted to a corresponding <see cref="long"/>
        /// value will be ignored.</remarks>
        public static long[] GetInt64Array(this IQueryCollection? query, string key) {
            return query == null ? Array.Empty<long>() : query[key].ToInt64Array();
        }

        /// <summary>
        /// Returns a <see cref="long"/> list based of the values of each query string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string components.</param>
        /// <returns>A <see cref="long"/> list representing the converted values.</returns>
        /// <remarks>The value of each query string component may themselves be a separated list of <see cref="long"/>
        /// values - eg. separated by commas. Values that can not be converted to a corresponding <see cref="long"/>
        /// value will be ignored.</remarks>
        public static List<long> GetInt64List(this IQueryCollection? query, string key) {
            return query?[key].ToInt64List() ?? new List<long>();
        }

        #endregion

        #region GetFloat...

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as a <see cref="float"/>. If a matching query string component isn't found, or the value could
        /// not be converted to a <see cref="float"/>, <c>0</c> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <returns>The converted <see cref="float"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise <c>0</c>.</returns>
        public static float GetFloat(this IQueryCollection? query, string key) {
            return query == null ? 0 : query[key].ToFloat();
        }

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as a <see cref="float"/>. If a matching query string component isn't found, or the value could
        /// not be converted to a <see cref="float"/>, <paramref name="fallback"/> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="fallback">The fallback value.</param>
        /// <returns>The converted <see cref="float"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise <paramref name="fallback"/>.</returns>
        public static float GetFloat(this IQueryCollection? query, string key, float fallback) {
            return query == null ? fallback : query[key].ToFloat(fallback);
        }

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as a <see cref="float"/>. If a matching query string component isn't found, or the value could not
        /// be converted to an integer, <see langword="null"/> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <returns>The converted <see cref="float"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise <see langword="null"/>.</returns>
        public static float? GetFloatOrNull(this IQueryCollection? query, string key) {
            return query?[key].ToFloatOrNull();
        }

        /// <summary>
        /// Attempts to get the <see cref="float"/> value of the string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="result">When this method returns, holds the <see cref="float"/> value if successful; otherwise, <c>0</c>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetFloat(this IQueryCollection? query, string key, out float result) {
            return StringUtils.TryParseFloat(GetString(query, key), out result);
        }

        /// <summary>
        /// Attempts to get the <see cref="float"/> value of the string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="result">When this method returns, holds the <see cref="float"/> value if successful; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetFloat(this IQueryCollection? query, string key, [NotNullWhen(true)] out float? result) {
            return StringUtils.TryParseFloat(GetString(query, key), out result);
        }

        /// <summary>
        /// Returns a <see cref="float"/> array based on the values of each query string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string components.</param>
        /// <returns>A <see cref="float"/> array representing the converted values.</returns>
        /// <remarks>The value of each query string component may themselves be a separated list of <see cref="float"/>
        /// values - eg. separated by commas. Values that can not be converted to a corresponding <see cref="float"/>
        /// value will be ignored.</remarks>
        public static float[] GetFloatArray(this IQueryCollection? query, string key) {
            return query?[key].ToFloatArray() ?? Array.Empty<float>();
        }

        /// <summary>
        /// Returns a <see cref="float"/> list based on the values of each query string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string components.</param>
        /// <returns>A <see cref="float"/> list representing the converted values.</returns>
        /// <remarks>The value of each query string component may themselves be a separated list of <see cref="float"/>
        /// values - eg. separated by commas. Values that can not be converted to a corresponding <see cref="float"/>
        /// value will be ignored.</remarks>
        public static List<float> GetFloatList(this IQueryCollection? query, string key) {
            return query?[key].ToFloatList() ?? new List<float>();
        }

        #endregion

        #region GetDouble...

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as a <see cref="double"/>. If a matching query string component isn't found, or the value could
        /// not be converted to a <see cref="double"/>, <c>0</c> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <returns>The converted <see cref="double"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise <c>0</c>.</returns>
        public static double GetDouble(this IQueryCollection? query, string key) {
            return query == null ? 0 : query[key].ToDouble();
        }

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as a <see cref="double"/>. If a matching query string component isn't found, or the value could
        /// not be converted to a <see cref="double"/>, <paramref name="fallback"/> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="fallback">The fallback value.</param>
        /// <returns>The converted <see cref="double"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise <paramref name="fallback"/>.</returns>
        public static double GetDouble(this IQueryCollection? query, string key, double fallback) {
            return query == null ? fallback : query[key].ToDouble(fallback);
        }

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as a <see cref="double"/>. If a matching query string component isn't found, or the value could not
        /// be converted to an integer, <see langword="null"/> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <returns>The converted <see cref="double"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise <see langword="null"/>.</returns>
        public static double? GetDoubleOrNull(this IQueryCollection? query, string key) {
            return query?[key].ToDoubleOrNull();
        }

        /// <summary>
        /// Attempts to get the <see cref="double"/> value of the string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="result">When this method returns, holds the <see cref="double"/> value if successful; otherwise, <c>0</c>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetDouble(this IQueryCollection? query, string key, out double result) {
            return StringUtils.TryParseDouble(GetString(query, key), out result);
        }

        /// <summary>
        /// Attempts to get the <see cref="double"/> value of the string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="result">When this method returns, holds the <see cref="double"/> value if successful; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetDouble(this IQueryCollection? query, string key, [NotNullWhen(true)] out double? result) {
            return StringUtils.TryParseDouble(GetString(query, key), out result);
        }

        /// <summary>
        /// Returns a <see cref="double"/> array based on the values of each query string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string components.</param>
        /// <returns>A <see cref="double"/> array representing the converted values.</returns>
        /// <remarks>The value of each query string component may themselves be a separated list of <see cref="double"/>
        /// values - eg. separated by commas. Values that can not be converted to a corresponding <see cref="double"/>
        /// value will be ignored.</remarks>
        public static double[] GetDoubleArray(this IQueryCollection? query, string key) {
            return query == null ? Array.Empty<double>() : query[key].ToDoubleArray();
        }

        /// <summary>
        /// Returns a <see cref="double"/> list based on the values of each query string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string components.</param>
        /// <returns>A <see cref="double"/> list representing the converted values.</returns>
        /// <remarks>The value of each query string component may themselves be a separated list of <see cref="double"/>
        /// values - eg. separated by commas. Values that can not be converted to a corresponding <see cref="double"/>
        /// value will be ignored.</remarks>
        public static List<double> GetDoubleList(this IQueryCollection? query, string key) {
            return query?[key].ToDoubleList() ?? new List<double>();
        }

        #endregion

        #region GetBoolean...

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as a <see cref="bool"/>. If a matching query string component isn't found, or the value could not
        /// be converted to a <see cref="bool"/>, <c>false</c> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <returns>The converted <see cref="bool"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise <c>false</c>.</returns>
        public static bool GetBoolean(this IQueryCollection? query, string key) {
            return query != null && query[key].ToBoolean();
        }

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as a <see cref="bool"/>. If a matching query string component isn't found, or the value could not
        /// be converted to a <see cref="bool"/>, <paramref name="fallback"/> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="fallback">The fallback value.</param>
        /// <returns>The converted <see cref="bool"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise <paramref name="fallback"/>.</returns>
        public static bool GetBoolean(this IQueryCollection? query, string key, bool fallback) {
            return query == null ? fallback : query[key].ToBoolean(fallback);
        }

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as a <see cref="bool"/>. If a matching query string component isn't found, or the value could not
        /// be converted to an integer, <see langword="null"/> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <returns>The converted <see cref="bool"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise <see langword="null"/>.</returns>
        public static bool? GetBooleanOrNull(this IQueryCollection? query, string key) {
            return query?[key].ToBooleanOrNull();
        }

        /// <summary>
        /// Attempts to get the <see cref="bool"/> value of the string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="result">When this method returns, holds the <see cref="bool"/> value if successful; otherwise, <see langword="false"/>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetBoolean(this IQueryCollection? query, string key, out bool result) {
            return StringUtils.TryParseBoolean(GetString(query, key), out result);
        }

        /// <summary>
        /// Attempts to get the <see cref="bool"/> value of the string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="result">When this method returns, holds the <see cref="bool"/> value if successful; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetBoolean(this IQueryCollection? query, string key, [NotNullWhen(true)] out bool? result) {
            return StringUtils.TryParseBoolean(GetString(query, key), out result);
        }

        #endregion

        #region GetGuid...

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as a <see cref="Guid"/>. If a matching query string component isn't found, or the value could not
        /// be converted to a <see cref="Guid"/>, <see cref="Guid.Empty"/> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <returns>The converted <see cref="Guid"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise, <see cref="Guid.Empty"/>.</returns>
        public static Guid GetGuid(this IQueryCollection? query, string key) {
            return query == null ? Guid.Empty : query[key].ToGuid();
        }

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as a <see cref="Guid"/>. If a matching query string component isn't found, or the value could not
        /// be converted to a <see cref="Guid"/>, <paramref name="fallback"/> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="fallback">The fallback value.</param>
        /// <returns>The converted <see cref="Guid"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise, <paramref name="fallback"/>.</returns>
        public static Guid GetGuid(this IQueryCollection? query, string key, Guid fallback) {
            return query == null ? fallback : query[key].ToGuid(fallback);
        }

        /// <summary>
        /// Gets the value of the first query string component with the specified <paramref name="key"/>, and returns
        /// the value as a <see cref="Guid"/>. If a matching query string component isn't found, or the value could not
        /// be converted to a <see cref="Guid"/>, <see langword="null"/> is returned instead.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <returns>The converted <see cref="Guid"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise, <see langword="null"/>.</returns>
        public static Guid? GetGuidOrNull(this IQueryCollection? query, string key) {
            return query?[key].ToGuidOrNull();
        }

        /// <summary>
        /// Returns an array of <see cref="Guid"/> values based on the values of each query string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <returns>An instance of <see cref="List{Guid}"/>.</returns>
        public static Guid[] GetGuidArray(this IQueryCollection? query, string key) {
            return query?[key].ToGuidArray() ?? Array.Empty<Guid>();
        }

        /// <summary>
        /// Returns a list of <see cref="Guid"/> values based on the values of each query string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <returns>An instance of <see cref="List{Guid}"/>.</returns>
        public static List<Guid> GetGuidList(this IQueryCollection? query, string key) {
            return query?[key].ToGuidList() ?? new List<Guid>();
        }

        /// <summary>
        /// Attempts to get the <see cref="Guid"/> value of the string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="result">When this method returns, holds the <see cref="Guid"/> value if successful; otherwise, <see cref="Guid.Empty"/>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetGuid(this IQueryCollection? query, string key, out Guid result) {
            return StringUtils.TryParseGuid(GetString(query, key), out result);
        }

        /// <summary>
        /// Attempts to get the <see cref="bool"/> value of the string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="result">When this method returns, holds the <see cref="Guid"/> value if successful; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetGuid(this IQueryCollection? query, string key, [NotNullWhen(true)] out Guid? result) {
            return StringUtils.TryParseGuid(GetString(query, key), out result);
        }

        #endregion

        #region GetEnum...

        /// <summary>
        /// Returns the corresponding <typeparamref name="TEnum"/> value of the first query string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <returns>The converted <typeparamref name="TEnum"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise, the default value of <typeparamref name="TEnum"/>.</returns>
        public static TEnum GetEnum<TEnum>(this IQueryCollection? query, string key) where TEnum : struct, Enum {
            return (query?[key]).ToEnum<TEnum>();
        }

        /// <summary>
        /// Returns the corresponding <typeparamref name="TEnum"/> value of the first query string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="fallback">The fallback value in case a value isn't found or cant be converted.</param>
        /// <returns>The converted <typeparamref name="TEnum"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise, <paramref name="fallback"/>.</returns>
        public static TEnum GetEnum<TEnum>(this IQueryCollection? query, string key, TEnum fallback) where TEnum : struct, Enum {
            return (query?[key]).ToEnum(fallback);
        }

        /// <summary>
        /// Returns the corresponding <typeparamref name="TEnum"/> value of the first query string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <returns>The converted <typeparamref name="TEnum"/> value if a matching query string component is found and the
        /// conversion is successful; otherwise, <see langword="null"/>.</returns>
        public static TEnum? GetEnumOrNull<TEnum>(this IQueryCollection? query, string key) where TEnum : struct, Enum {
            return (query?[key]).ToEnumOrNull<TEnum>();
        }

        /// <summary>
        /// Returns an array of <typeparamref name="TEnum"/> values representing the values of each query string component
        /// matching the specified <paramref name="key"/>.
        ///
        /// Notice that this method support both multiple query string components with the same <paramref name="key"/>
        /// as well as query string components where the value is a comma separated string or similar. Supported
        /// separators are comma (<c>,</c>), space (<c> </c>), carriage return (<c>\r</c>), new line (<c>\n</c>) and
        /// tab (<c>\t</c>).
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component(s).</param>
        /// <returns>An array of <typeparamref name="TEnum"/>.</returns>
        public static TEnum[] GetEnumArray<TEnum>(this IQueryCollection? query, string key) where TEnum : struct, Enum {
            return (query?[key]).ToEnumArray<TEnum>();
        }

        /// <summary>
        /// Returns an array of <typeparamref name="TEnum"/> values representing the values of each query string component
        /// matching the specified <paramref name="key"/>.
        ///
        /// Notice that this method support both multiple query string components with the same <paramref name="key"/>
        /// as well as query string components where the value is separated by one of the following <paramref name="separators"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component(s).</param>
        /// <param name="separators">An array of supported separators.</param>
        /// <returns>An array of <typeparamref name="TEnum"/>.</returns>
        public static TEnum[] GetEnumArray<TEnum>(this IQueryCollection? query, string key, params char[] separators) where TEnum : struct, Enum {
            return (query?[key]).ToEnumArray<TEnum>(separators);
        }

        /// <summary>
        /// Returns a list of <typeparamref name="TEnum"/> values representing the values of each query string component
        /// matching the specified <paramref name="key"/>.
        ///
        /// Notice that this method support both multiple query string components with the same <paramref name="key"/>
        /// as well as query string components where the value is a comma separated string or similar. Supported
        /// separators are comma (<c>,</c>), space (<c> </c>), carriage return (<c>\r</c>), new line (<c>\n</c>) and
        /// tab (<c>\t</c>).
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component(s).</param>
        /// <returns>A list of <typeparamref name="TEnum"/>.</returns>
        public static List<TEnum> GetEnumList<TEnum>(this IQueryCollection? query, string key) where TEnum : struct, Enum {
            return (query?[key]).ToEnumList<TEnum>();
        }

        /// <summary>
        /// Returns a list of <typeparamref name="TEnum"/> values representing the values of each query string component
        /// matching the specified <paramref name="key"/>.
        ///
        /// Notice that this method support both multiple query string components with the same <paramref name="key"/>
        /// as well as query string components where the value is separated by one of the following <paramref name="separators"/>.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component(s).</param>
        /// <param name="separators">An array of supported separators.</param>
        /// <returns>A list of <typeparamref name="TEnum"/>.</returns>
        public static List<TEnum> GetEnumList<TEnum>(this IQueryCollection? query, string key, params char[] separators) where TEnum : struct, Enum {
            return (query?[key]).ToEnumList<TEnum>(separators);
        }

        /// <summary>
        /// Attempts to get the enum value of type <typeparamref name="TEnum"/> from the first query string component with the specified <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="result">When this method returns, contains the parsed <typeparamref name="TEnum"/> value if successful; otherwise, <see langword="null"/>. This parameter is passed uninitialized.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetEnum<TEnum>(this IQueryCollection? query, string key, out TEnum result) where TEnum : struct, Enum {
            result = default;
            return TryGetString(query, key, out string? value) && EnumUtils.TryParseEnum(value, out result);
        }

        /// <summary>
        /// Attempts to get the enum value of type <typeparamref name="TEnum"/> from the header with the specified <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="query">The query string.</param>
        /// <param name="key">The key of the query string component.</param>
        /// <param name="result">When this method returns, contains the parsed <typeparamref name="TEnum"/> value if successful; otherwise, the default value of <typeparamref name="TEnum"/>. This parameter is passed uninitialized.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetEnum<TEnum>(this IQueryCollection? query, string key, out TEnum? result) where TEnum : struct, Enum {
            result = null;
            return TryGetString(query, key, out string? value) && EnumUtils.TryParseEnum(value, out result);
        }

        #endregion

        /// <summary>
        /// Returns an URL encoded string representing the specified <paramref name="query"/>.
        /// </summary>
        /// <param name="query">The query string to be encoded.</param>
        /// <returns>The URL encoded version of the query string.</returns>
        public static string ToUrlEncodedString(this IQueryCollection? query) {

            if (query == null) return string.Empty;

            StringBuilder sb = new();

            int i = 0;

            foreach ((string key, StringValues stringValues) in query) {

                foreach (string? value in stringValues) {
                    if (value is null) continue;
                    if (i++ > 0) sb.Append('&');
                    sb.Append(WebUtility.UrlEncode(key));
                    sb.Append('=');
                    sb.Append(WebUtility.UrlEncode(value));
                }

            }

            return sb.ToString();

        }

    }

}