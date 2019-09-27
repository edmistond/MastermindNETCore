namespace Mastermind
{
  /// <summary>
  /// Convenience object for getting result data back from the GuessValidator.
  /// </summary>
  public class GuessValidationResult
  {
    /// <summary>
    ///   Indicates if the guess won the game.
    /// </summary>
    public bool VictoryCondition { get; set; }

    /// <summary>
    ///   Holds data about the status of the guess.
    ///   + indicates a number was in the correct position.
    ///   - indicates that the number exists in the generated number, but is
    ///   in the wrong position.
    /// </summary>
    public string GuessResult { get; set; }
  }
}