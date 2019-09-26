using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind
{
  public static class GuessValidator
  {
    private const string VictoryString = "++++";

    public static GuessValidation Validate(IGameState state, List<int> guess) {
      // Victory condition; guess and generated both match exactly. We can stop right here.
      // Technically, we don't need to do this and if it was a longer/more complex string
      // where performance is critical, I probably wouldn't. I think it simplifies the
      // readability a bit because you can now mentally discard success scenarios, but
      // it doesn't actually simplify the *logic* any because you can comment this check out
      // and all the unit tests will still pass.
      if (state.Numbers.SequenceEqual(guess))
      {
        return new GuessValidation { VictoryCondition = true, GuessResult = VictoryString };
      }

      // I'd really prefer to do all this with LINQ, but using Intersect() isn't a perfect solution
      // because it won't handle duplicated values. That is to say, if you had 2,2,1,1 and 1,1,2,2
      // Intersect() would only give you 1,2. Not too helpful! So, we do it the old-school array
      // enumeration way.
      // This would be O(n) best case (winning guess) except that won't happen here because we just
      // checked that. Worst case would be O(n^2) if we have to hit the inner loop for
      // all 4 digits in the guess.
      var nums = new List<int>(state.Numbers);

      var statusString = "";

      for (int i = 0; i < guess.Count; i++)
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
        for (int k = 0; k < nums.Count; k++)
        {
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
      }

      return new GuessValidation {
        VictoryCondition = statusString == VictoryString,
        GuessResult = String.Concat(statusString.OrderBy(c => c))
      };
    }
  }
}
