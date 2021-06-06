using System;

using MinimalExtended;
using Sandbox;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace IsEven
{

  /// <summary>
  /// But is it even?
  /// </summary>
  [Library("is-even")]
  public partial class IsEven : AddonClass
  {
    public static bool Even(int number)
    {
      return !IsOdd.IsOdd.Odd(number);
    }

  }

}
