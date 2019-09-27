using System;
using System.Collections.Generic;

namespace Mastermind
{
  /// <summary>
  /// Interface for the GameState - used for mocking in the unit tests.
  /// </summary>
  public interface IGameState
  {
    int Attempts { get; }
    List<int> Numbers { get; }
    void IncrementAttempts();
    Boolean MaxAttemptsReached { get; }
  }
}
