using System;

namespace SCRL.Resources
{
  public static class Rand
  {
    /// <summary>
    /// Use for a better random number from the seeded Random used by the Game.
    /// </summary>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>A random number between the min and max values, inclusive.</returns>
    public static int SeededNext(int min, int max)
    {
      return (int)Math.Abs((Math.Floor(Program.Random.NextDouble() * (1 + max - min) + min)));
    }
  }
}
