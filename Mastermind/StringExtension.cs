using System;
namespace Mastermind
{
  public static class StringExtension
  {
    // Convenience extension method so you needn't wrap everything in String.IsNullOrWhiteSpace() checks
    public static bool IsNullOrWhiteSpace(this string CheckString)
    {
      return String.IsNullOrWhiteSpace(CheckString);
    }
  }
}
