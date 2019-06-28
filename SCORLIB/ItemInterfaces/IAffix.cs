namespace SCORLIB.ItemInterfaces
{
  /// <summary>
  /// Affixes are attached to Items either as Prefixes or Suffixes.
  /// There can only be one Prefix and one Suffix per Item, but each
  /// Affix itself can modify more than one value at once.
  /// </summary>
  public interface IAffix
  {
    /// <summary>
    /// The Name of the affix to be attached.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// The first Ability to be modified by this Affix.
    /// </summary>
    string Mod1Name { get; set; }

    /// <summary>
    /// The minimum mod amount for this modifier.
    /// </summary>
    int Mod1Min { get; set; }

    /// <summary>
    /// The maximum mod amount for this modifier.
    /// </summary>
    int Mod1Max { get; set; }

    /// <summary>
    /// The value of the mod amount for this modifier.
    /// Between Mod1Min and Mod1Max.
    /// </summary>
    int Mod1Value { get; set; }

    /// <summary>
    /// The second Ability to be modified by this Affix.
    /// </summary>
    string Mod2Name { get; set; }

    /// <summary>
    /// The minimum mod amount for this modifier.
    /// </summary>
    int Mod2Min { get; set; }

    /// <summary>
    /// The maximum mod amount for this modifier.
    /// </summary>
    int Mod2Max { get; set; }

    /// <summary>
    /// The value of the mod amount for this modifier.
    /// Between Mod2Min and Mod2Max.
    /// </summary>
    int Mod2Value { get; set; }

    /// <summary>
    /// The third Ability to be modified by this Affix.
    /// </summary>
    string Mod3Name { get; set; }

    /// <summary>
    /// The minimum mod amount for this modifier.
    /// </summary>
    int Mod3Min { get; set; }

    /// <summary>
    /// The maximum mod amount for this modifier.
    /// </summary>
    int Mod3Max { get; set; }

    /// <summary>
    /// The value of the mod amount for this modifier.
    /// Between Mod3Min and Mod3Max.
    /// </summary>
    int Mod3Value { get; set; }

    /// <summary>
    /// Determines the modifier value for a given modifier between its Min and Max values.
    /// </summary>
    /// <param name="modMin">The ModMin value for this Modifier</param>
    /// <param name="modMax">The ModMax value for this Modifier</param>
    /// <returns>A random value between ModMin and ModMax</returns>
    int GetModValue(int modMin, int modMax);
  }
}
