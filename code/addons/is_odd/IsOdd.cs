using System;

using MinimalExtended;
using Sandbox;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace IsOdd
{

  /// <summary>
  /// Isn't it odd?
  /// </summary>
  [Library("is-odd")]
  public partial class IsOdd : AddonClass
  {
    public static bool Odd(int number)
    {
      // Support negatives
      number = Math.Abs(number);
      while (number > 0)
      {
        number -= 2;
      }
      if (number == 0)
      {
        return false;
      }
      if (number < 0)
      {
        return true;
      }
      return false;
    }

  }

}
