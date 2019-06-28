using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCORLIB
{
  /// <summary>
  /// A Modifer may alter a skill, an attribute, damage, resistance, or more.
  /// It may negatively or positively alter any of these, indefinitely or for
  /// a specific amount of time.
  /// </summary>
  public struct Modifier
  {
    /// <summary>
    /// The skill or attribute to be modified.
    /// </summary>
    public string Modifying { get; }

    /// <summary>
    /// How much to modify the skill or attribute
    /// </summary>
    public int Amount { get; private set; }

    /// <summary>
    /// Duration of the Modifier in seconds
    /// </summary>
    public int Duration { get; private set; }

    /// <summary>
    /// How much time is left on the modifier
    /// </summary>
    public TimeSpan TimeLeft { get; private set; }

    /// <summary>
    /// Creates a Modifier that lasts indefinitely or until something else removes it
    /// </summary>
    /// <param name="modifying">The skill or attribute to be modified</param>
    /// <param name="amount">How much the skill or attribute will be modified</param>
    public Modifier(string modifying, int amount)
    {
      Modifying = modifying;
      Amount = amount;
      Duration = -1;
      TimeLeft = TimeSpan.Zero;
    }

    /// <summary>
    /// Creates a Modifier that lasts the given number of seconds
    /// </summary>
    /// <param name="modifying">The skill or attribute to be modified</param>
    /// <param name="amount">How much the skill or attribute will be modified</param>
    /// <param name="duration">How long, in seconds, to modify the skill or attribute</param>
    public Modifier(string modifying, int amount, int duration)
    {
      Modifying = modifying;
      Amount = amount;
      Duration = duration;
      TimeLeft = TimeSpan.FromSeconds(duration);
    }

    /// <summary>
    /// Updates the Modifier to increment its TimeLeft
    /// </summary>
    /// <param name="delta">TimeSpawn, or ElapsedGametime</param>
    public void Update(TimeSpan delta)
    {
      if (Duration == -1)
      {
        return;
      }

      TimeLeft -= delta;

      if (TimeLeft.TotalMilliseconds < 0)
      {
        TimeLeft = TimeSpan.Zero;
        Amount = 0;
      }
    }

    /// <summary>
    /// Increases or alters the TimeLeft of a Modifier by the given number of seconds
    /// </summary>
    /// <param name="seconds">How many seconds to alter the TimeLeft by</param>
    public void AddTime(TimeSpan seconds)
    {
      TimeLeft += seconds;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is Modifier))
      {
        return false;
      }

      return Equals((Modifier)obj);
    }

    public bool Equals(Modifier other)
    {
      if (ReferenceEquals(this, other))
      {
        return true;
      }

      return Modifying == other.Modifying &&
             Amount == other.Amount &&
             Duration == other.Duration &&
             TimeLeft == other.TimeLeft;
    }

    public static bool operator ==(Modifier modifier1, Modifier modifier2)
    {
      return modifier1.Equals(modifier2);
    }

    public static bool operator !=(Modifier modifier1, Modifier modifier2)
    {
      return !modifier1.Equals(modifier2);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
