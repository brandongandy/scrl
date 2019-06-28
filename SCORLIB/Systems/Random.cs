using System;
using System.Security.Cryptography;

namespace SCORLIB.Systems
{
  public static class Random
  {
    // Thanks to: https://scottlilly.com/create-better-random-numbers-in-c/
    private static RNGCryptoServiceProvider Rand = new RNGCryptoServiceProvider();

    /// <summary>
    /// Use when you want to get a "true" random number -- unseeded.
    /// </summary>
    /// <param name="minValue">The minimum value.</param>
    /// <param name="maxValue">The maximum value.</param>
    /// <returns>A random number between the min and max values, inclusive.</returns>
    public static int Next(int minValue, int maxValue)
    {
      byte[] randomNumber = new byte[1];
      Rand.GetBytes(randomNumber);

      double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

      // Ensure the multiplier will always be between 0.0 and *just* under 1.0.
      double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

      // Add 1 to the range to allow for the rounding done in Math.Floor
      int range = maxValue - minValue + 1;

      double randomValueInRange = Math.Floor(multiplier * range);

      return (int)(minValue + randomValueInRange);
    }

    /// <summary>
    /// Use when you want a "true" random double -- unseeded.
    /// </summary>
    /// <returns>Value between 0.0 and 1.0, inclusive.</returns>
    public static double NextDouble()
    {
      double minValue = 0.0;
      double maxValue = 1.0;
      byte[] randomNumber = new byte[1];
      Rand.GetBytes(randomNumber);

      double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

      // Ensure the multiplier will always be between 0.0 and *just* under 1.0.
      double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

      // Add 1 to the range to allow for the rounding done in Math.Floor
      double range = maxValue - minValue + 1;

      double randomValueInRange = Math.Floor(multiplier * range);

      return (minValue + randomValueInRange);
    }
  }
}
