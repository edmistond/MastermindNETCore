using System;
using System.Collections.Generic;

namespace Mastermind
{
  public interface IGameState
  {
    public List<int> Numbers { get; }
    public void IncrementAttempts();
    public Boolean MaxAttemptsReached { get; }
  }
}
