using System;

namespace Mastermind.Utility
{
  public static class StringExtension
  {
    // Convenience extension method so you needn't wrap everything in String.IsNullOrWhiteSpace() checks
    public static bool IsNullOrWhiteSpace(this string checkString)
    {
      return String.IsNullOrWhiteSpace(checkString);
    }
  }
}
