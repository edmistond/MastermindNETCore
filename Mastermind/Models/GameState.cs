using System;
using System.Collections.Generic;

namespace Mastermind
{
  /// <summary>
  /// Maintains some basic information about the state of the game - number of guess attempts
  /// and the generated numbers to be guessed.
  /// </summary>
  public class GameState : IGameState
  {
    /// <summary>
    /// How many attempts the user made at guessing the numbers.
    /// </summary>
    public int Attempts { get; private set; }

    /// <summary>
    /// List (int) of generated numbers to guess.
    /// </summary>
    public List<int> Numbers { get; private set; }

    /// <summary>
    /// GameState constructor. Initializes the game by generating numbers and attempts counter.
    /// </summary>
    public GameState()
    {
      Attempts = 0;

      var rnd = new Random(DateTime.Now.Millisecond);

      Numbers = new List<int>();

      // Generate 4 random numbers between 1 and 6 to start
      for (int i = 0; i < 4; i++)
      {
        Numbers.Add(rnd.Next(1, 6));
      }
    }

    /// <summary>
    /// Attempts to increment the game counter. Throws exception if you already attempted 10 times.
    /// </summary>
    public void IncrementAttempts()
    {
      if (Attempts < 10)
      {
        Attempts++;
      }
      else
      {
        throw new GameException("The game is over -- too many attempts!");
      }
    }

    // Indicates whether max attempts are exhausted.
    public Boolean MaxAttemptsReached
    {
      get { return Attempts == 10; }
    }
  }
}
