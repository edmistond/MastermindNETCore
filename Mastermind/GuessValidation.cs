using System;
using System.Security.Cryptography.X509Certificates;

namespace Mastermind
{
  public class GuessValidation
  {
    public bool VictoryCondition { get; set; }
    public string GuessResult { get; set; }
  }
}
