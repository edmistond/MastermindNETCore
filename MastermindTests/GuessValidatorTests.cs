using System.Collections.Generic;
using Mastermind;
using Moq;
using NUnit.Framework;

namespace MastermindTests
{
  public class GuessValidatorTests
  {

    [Test]
    public void ExactGuessShouldSetVictoryCondition()
    {
      var stateMock = GameStateMockBuilder(new int[] { 1, 2, 3, 4 });
      var guess = new List<int> { 1, 2, 3, 4 };

      var validation = GuessValidator.Validate(stateMock.Object, guess);

      Assert.IsTrue(validation.VictoryCondition);
    }

    [Test]
    public void IncorrectGuessShouldNotTriggerVictoryCondition()
    {
      var stateMock = GameStateMockBuilder(new int[] { 1, 2, 3, 4 });
      var guess = new List<int> { 1, 1, 2, 2 };

      var validation = GuessValidator.Validate(stateMock.Object, guess);

      Assert.IsFalse(validation.VictoryCondition);
    }

    [Test]
    public void MatchingElementsAtMatchingPositionsShouldReturnPlus()
    {
      var stateMock = GameStateMockBuilder(new int[] { 1, 2, 3, 4 });

      var guess = new List<int> { 1, 2, 5, 6 };

      var validation = GuessValidator.Validate(stateMock.Object, guess);

      Assert.AreEqual(validation.GuessResult, "++");
    }

    [Test]
    public void MatchingElementsAtWrongPositionsShouldReturnMinus()
    {
      var stateMock = GameStateMockBuilder(new int[] { 1, 2, 3, 4 });
      var guess = new List<int> { 4, 3, 2, 1 };

      var validation = GuessValidator.Validate(stateMock.Object, guess);

      Assert.AreEqual(validation.GuessResult, "----");
    }

    [Test]
    public void MixOfCorrectAndIncorrectPositionsI()
    {
      var stateMock = GameStateMockBuilder(new int[] { 1, 2, 3, 4 });
      var guess = new List<int> { 4, 2, 3, 1 };

      var validation = GuessValidator.Validate(stateMock.Object, guess);

      Assert.AreEqual("++--", validation.GuessResult);
    }

    [Test]
    public void MixOfCorrectAndIncorrectPositionsII()
    {
      var stateMock = GameStateMockBuilder(new int[] { 1, 2, 3, 4 });
      var guess = new List<int> { 1, 3, 2, 4 };

      var validation = GuessValidator.Validate(stateMock.Object, guess);

      Assert.AreEqual("++--", validation.GuessResult);
    }

    [Test]
    public void IncorrectPositionTestI()
    {
      var stateMock = GameStateMockBuilder(new int[] { 6, 5, 4, 3 });
      var guess = new List<int> { 1, 2, 3, 5 };

      var validation = GuessValidator.Validate(stateMock.Object, guess);

      Assert.AreEqual("--", validation.GuessResult);
    }

    // Quick helper method since I'm writing the same game state mocks a bunch
    private Mock<IGameState> GameStateMockBuilder(int[] generated)
    {
      var stateMock = new Mock<IGameState>();
      stateMock.Setup(m => m.Numbers).Returns(new List<int>(generated));

      return stateMock;
    }
  }
}
