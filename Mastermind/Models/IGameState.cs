using System;
using System.Collections.Generic;

namespace Mastermind
{
  public interface IGameState
  {
    int Attempts { get; }
    List<int> Numbers { get; }
    void IncrementAttempts();
    Boolean MaxAttemptsReached { get; }
  }
}
