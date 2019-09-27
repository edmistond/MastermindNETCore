using System;

namespace Mastermind.Utility
{
  public static class StringExtension
  {
    /// <summary>
    /// Convenience extension for checking if a string is null, empty, or whitespace, so you don't have to
    /// wrap everything in String.IsNullOrWhiteSpace().
    /// </summary>
    /// <param name="checkString">String to be checked.</param>
    /// <returns>True if null, empty, or whitespace. Otherwise false.</returns>
    public static bool IsNullOrWhiteSpace(this string checkString)
    {
      return String.IsNullOrWhiteSpace(checkString);
    }
  }
}
