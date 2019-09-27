using System;
using System.Collections.Generic;

namespace Mastermind
{
  public class GameState : IGameState
  {
    public int Attempts { get; private set; }
    public List<int> Numbers { get; private set; }

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

    public Boolean MaxAttemptsReached
    {
      get { return Attempts == 10; }
    }
  }
}
