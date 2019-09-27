using System.Linq;
using Mastermind;
using NUnit.Framework;

namespace MastermindTests
{
  public class GameStateTests
  {
    [Test]
    public void RandomNumbersGeneratedCorrectly()
    {
      var state = new GameState();

      Assert.AreEqual(state.Numbers.Count, 4, "Game state should have four random numbers.");
      Assert.IsTrue(state.Numbers.All(n => n >= 0 && n <= 6), "Randomly generated numbers should be between 1 and 6.");
    }

    [Test]
    public void StateShouldReportMaxAttemptsReachedAt10()
    {
      var state = new GameState();

      Assert.IsFalse(state.MaxAttemptsReached);

      for (int i = 0; i < 10; i++)
      {
        state.IncrementAttempts();
      }

      Assert.IsTrue(state.MaxAttemptsReached);
    }
  }
}