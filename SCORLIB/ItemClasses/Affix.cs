using SCORLIB.ItemInterfaces;
using System;

namespace SCORLIB.ItemClasses
{
  public class Affix : IAffix, IEquatable<Affix>
  {
    /// <summary>
    /// The Name of the affix to be attached.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The first Ability to be modified by this Affix.
    /// </summary>
    public string Mod1Name { get; set; }

    /// <summary>
    /// The minimum mod amount for this modifier.
    /// </summary>
    public int Mod1Min { get; set; }

    /// <summary>
    /// The maximum mod amount for this modifier.
    /// </summary>
    public int Mod1Max { get; set; }

    /// <summary>
    /// The value of the mod amount for this modifier.
    /// Between Mod1Min and Mod1Max.
    /// </summary>
    public int Mod1Value { get; set; }

    /// <summary>
    /// The second Ability to be modified by this Affix.
    /// </summary>
    public string Mod2Name { get; set; }

    /// <summary>
    /// The minimum mod amount for this modifier.
    /// </summary>
    public int Mod2Min { get; set; }

    /// <summary>
    /// The maximum mod amount for this modifier.
    /// </summary>
    public int Mod2Max { get; set; }

    /// <summary>
    /// The value of the mod amount for this modifier.
    /// Between Mod2Min and Mod2Max.
    /// </summary>
    public int Mod2Value { get; set; }

    /// <summary>
    /// The third Ability to be modified by this Affix.
    /// </summary>
    public string Mod3Name { get; set; }

    /// <summary>
    /// The minimum mod amount for this modifier.
    /// </summary>
    public int Mod3Min { get; set; }

    /// <summary>
    /// The maximum mod amount for this modifier.
    /// </summary>
    public int Mod3Max { get; set; }

    /// <summary>
    /// The value of the mod amount for this modifier.
    /// Between Mod3Min and Mod3Max.
    /// </summary>
    public int Mod3Value { get; set; }

    public Affix(string name, string mod1Name, int mod1Min, int mod1Max)
    {
      Name = name;
      Mod1Name = mod1Name;
      Mod1Min = mod1Min;
      Mod1Max = mod1Max;
      Mod1Value = GetModValue(Mod1Min, Mod1Max);
    }

    public Affix(string name, string mod1Name, int mod1Min, int mod1Max, string mod2Name, int mod2Min, int mod2Max)
      : this(name, mod1Name, mod1Min, mod1Max)
    {
      Mod2Name = mod2Name;
      Mod2Min = mod2Min;
      Mod2Max = mod2Max;
      Mod2Value = GetModValue(Mod2Min, Mod2Max);
    }

    public Affix(string name, string mod1Name, int mod1Min, int mod1Max, string mod2Name, int mod2Min, int mod2Max,
      string mod3Name, int mod3Min, int mod3Max) : this(name, mod1Name, mod1Min, mod1Max, mod2Name, mod2Min, mod2Max)
    {
      Mod3Name = mod3Name;
      Mod3Min = mod3Min;
      Mod3Max = mod3Max;
      Mod3Value = GetModValue(Mod3Min, Mod3Max);
    }

    /// <summary>
    /// Determines the modifier value for a given modifier between its Min and Max values.
    /// </summary>
    /// <param name="modMin">The ModMin value for this Modifier</param>
    /// <param name="modMax">The ModMax value for this Modifier</param>
    /// <returns>A random value between ModMin and ModMax</returns>
    public int GetModValue(int modMin, int modMax)
    {
      return Systems.Random.Next(modMin, modMax);
    }

    public bool Equals(Affix other)
    {
      return Name == other.Name &&
             Mod1Name == other.Mod1Name &&
             Mod1Min == other.Mod1Min &&
             Mod1Max == other.Mod1Max &&
             Mod1Value == other.Mod1Value &&
             Mod2Name == other.Mod2Name &&
             Mod2Min == other.Mod2Min &&
             Mod2Max == other.Mod2Max &&
             Mod2Value == other.Mod2Value &&
             Mod3Name == other.Mod3Name &&
             Mod3Min == other.Mod3Min &&
             Mod3Max == other.Mod3Max &&
             Mod3Value == other.Mod3Value;
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
      {
        return false;
      }

      if (ReferenceEquals(this, obj))
      {
        return true;
      }

      return Equals(obj as Affix);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
