using System.Collections.Generic;
using System.Linq;

namespace Mastermind.Validation
{
  /// <summary>
  ///   Guess validator static class.
  /// </summary>
  public static class GuessValidator
  {
    private const string VictoryString = "++++";

    /// <summary>
    ///   Validates the guess against the game state. Please see commentary within for details.
    /// </summary>
    /// <param name="state">Game state object for gaining access to the generated numbers.</param>
    /// <param name="guess">Guessed numbers input by the user.</param>
    /// <returns>
    ///   GuessValidationResult indicating victory condition (true/false) and hints about
    ///   whether or not the numbers in the guess exist in the actual numbers and in what position.
    /// </returns>
    public static GuessValidationResult Validate(IGameState state, IEnumerable<int> guess)
    {
      // Note that we could make this marginally more efficient by having the guess be an array.
      // However, I don't feel this is a worthwhile optimization given it's 4 characters. Regardless,
      // ReSharper WILL complain about multiple enumerations on guess.

      // Victory condition; guess and generated both match exactly. We can stop right here.
      // Technically, we don't need to do this and if it was a longer/more complex string
      // where performance is critical, I probably wouldn't. I think it simplifies the
      // readability a bit because you can now mentally discard success scenarios, but
      // it doesn't actually simplify the *logic* any because you can comment this check out
      // and all the unit tests will still pass.
      if (state.Numbers.SequenceEqual(guess))
        return new GuessValidationResult {VictoryCondition = true, GuessResult = VictoryString};

      // I'd really prefer to do all this with LINQ, but using Intersect() isn't a perfect solution
      // because it won't handle duplicated values. That is to say, if you had 2,2,1,1 and 1,1,2,2
      // Intersect() would only give you 1,2. Not too helpful! So, we do it the old-school array
      // enumeration way.
      var nums = new List<int>(state.Numbers);

      var statusString = "";

      for (var i = 0; i < guess.Count(); i++)
      {
        var digit = guess.ElementAt(i);

        // Digit at i exactly matches generated at i. This would result in a +.
        if (digit == nums[i])
        {
          statusString += "+";

          // Mark it in the generated copy as an out-of-bounds value to indicate it's been matched;
          // we don't want to accidentally pick it up again in the future.
          nums[i] = 0;

          // We can stop here and move to the next digit in the guess.
          continue;
        }

        // We don't have an exact match, so we'll have to scan the generated array
        // to see if that digit exists elsewhere in it. Then we'll change it to an
        // out of bounds value so we don't accidentally match it twice.
        for (var k = 0; k < nums.Count; k++)
          // We found a digit but it's in the wrong place. This would result in a -.
          if (nums[k] == digit)
          {
            statusString += "-";

            // Now mark the generated digit as processed.
            nums[k] = 0;

            // And break, since we don't want to continue this inner for loop.
            break;
          }
      }

      return new GuessValidationResult
      {
        VictoryCondition = statusString == VictoryString,
        GuessResult = string.Concat(statusString.OrderBy(c => c))
      };
    }
  }
}