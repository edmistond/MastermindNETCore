using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind
{
  class Program
  {
    private const string DebugFlag = "--debug";

    static void Main(string[] args)
    {
      GameState state = new GameState();
      bool debug = false;

      if (args.Length > 0 && args[0].ToLower() == DebugFlag) {
        debug = true;
        Console.WriteLine($"DEBUGGING ENABLED - the digits I'm thinking of are {string.Join("", state.Numbers)}");
      }

      Console.WriteLine("I am thinking of a number. Can you guess what it is?");
      Console.WriteLine("Enter four digits between 1 and 6 and press enter.");

      while (!state.MaxAttemptsReached)
      {
        string guess;
        guess = Console.ReadLine();

        // Validate that it's only 4 characters.
        if (guess.Length != 4)
        {
          Console.WriteLine("Please only enter four digits. No more and no less.");
        }

        // Validate that all characters are digits.
        if (!guess.All(char.IsDigit))
        {
          Console.WriteLine("Please only enter digits between 1 and 6");
          continue;
        }

        // Coerce the string input to integers. This is a little gross; I could also do .TryParse() here but
        // it's a bit superfluous since we just LINQ .All()'ed the string verifying that all we have are digits.
        var guessDigits = new List<int>();
        foreach (var digit in guess)
        {
          guessDigits.Add(int.Parse(digit.ToString()));
        }

        // Validate all digits are between 1 and 6.
        if (guessDigits.Any(gd => gd < 1 || gd > 6))
        {
          Console.WriteLine("Please only enter digits between 1 and 6.");
          continue;
        }

        // Ok. We know this is valid, so we can actually work with it now. First, increment the attempts.
        state.IncrementAttempts();

        // Run it through the validator.
        var validationResult = GuessValidator.Validate(state, guessDigits);

        // Print the "you won the game" message, if you in fact won the game.
        if (validationResult.VictoryCondition)
        {
          Console.WriteLine($"You won in {state.Attempts} attempt(s){(debug ? "... you cheater." : "!")}");
          Environment.Exit(0);
        }
        else
        {
          // Print any positioning info we're going to provide to the user, assuming there is any.
          if (!validationResult.GuessResult.IsNullOrWhiteSpace())
          {
            Console.WriteLine(validationResult.GuessResult);
          }
        }
      }

      // Maximum attempts exhausted; regretful message and a quick goodbye.
      // You'll note this returns an exit code back to the environment upon termination;
      // though it's really not a big deal here given this is a toy game, it's good practice
      // to return an exit code from applications so calling scripts can use that for flow
      // control if needed.
      Console.WriteLine("I'm afraid you are not a winner. Have a nice day.");
      Environment.Exit(1);
    }
  }
}
