using System;
namespace Mastermind
{
  public class GameException : Exception
  {
    public GameException(string message) : base(message)
    {
    }
  }
}
