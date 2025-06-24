using System;
using System.Globalization;

namespace ProjectManager_Server.Helpers;

/// <summary>
///     Extension for string
/// </summary>
public static class StringExtension
{
    /// <summary>
    ///     Convert input string into an UTC DateTime
    /// </summary>
    /// <param name="dt">The DateTime Provided</param>
    /// <param name="format">The format used to parse the input</param>
    /// <returns>A DateTime</returns>
    public static DateTime? ToUniversalDateTime(this string dt, string format = "yyyyMMdd")
    {
        return DateTime.ParseExact(dt, format, CultureInfo.InvariantCulture).ToUniversalTime();
    }
    
    /// <summary>
    ///     Convert input string into an UTC DateTime if input is not null, else return null
    /// </summary>
    /// <param name="dt">The DateTime Provided</param>
    /// <param name="format">The format used to parse the input</param>
    /// <returns>A DateTime</returns>
    public static DateTime? ToUniversalDateTimeNullable(this string? dt, string format = "yyyyMMdd")
    {
        return dt is null ? null : ToUniversalDateTime(dt, format);
    }
}