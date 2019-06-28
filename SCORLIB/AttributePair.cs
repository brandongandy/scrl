using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCORLIB
{
  /// <summary>
  /// An object which holds the current value and the maximum value of a given attribute.
  /// </summary>
  public class AttributePair
  {
    #region Fields

    private int currentValue;
    private int maximumValue;

    #endregion

    #region Properties

    /// <summary>
    /// The maximum allowed value for the AttributePair.
    /// </summary>
    public int MaximumValue
    {
      get { return maximumValue; }
      set { maximumValue = value; Clamp(); }
    }

    /// <summary>
    /// The current value of the AttributePair, clamped
    /// between zero and the MaximumValue.
    /// </summary>
    public int CurrentValue
    {
      get { return currentValue; }
      set { currentValue = value;  Clamp(); }
    }

    /// <summary>
    /// Creates a "zeroed" AttributePair.
    /// </summary>
    public static AttributePair Zero
    {
      get { return new AttributePair(); }
    }

    #endregion

    #region Constructor

    private AttributePair()
    {
      MaximumValue = 0;
      CurrentValue = 0;
    }

    /// <summary>
    /// Creates a new AttributePair with the current value set to the max value.
    /// </summary>
    /// <param name="maxValue">The maximum value of the AttributePair</param>
    public AttributePair(int maxValue)
    {
      MaximumValue = maxValue;
      CurrentValue = maxValue;
    }

    #endregion

    #region Heal and Damage Methods

    /// <summary>
    /// Adds to the CurrentValue of the AttributePair
    /// </summary>
    /// <param name="value">The amount to add</param>
    public void Heal(int value)
    {
      CurrentValue += value;
    }

    /// <summary>
    /// Removes from the CurrentValue of the AttributePair
    /// </summary>
    /// <param name="value">The amount to remove</param>
    public void Damage(int value)
    {
      CurrentValue -= value;
    }

    #endregion

    #region Clamp

    /// <summary>
    /// Clamps the Current Value between 0 and the MaximumValue
    /// </summary>
    private void Clamp()
    {
      if (CurrentValue < 0)
      {
        CurrentValue = 0;
      }

      if (CurrentValue > MaximumValue)
      {
        CurrentValue = MaximumValue;
      }
    }

    #endregion
  }
}
