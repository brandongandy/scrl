using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCORLIB.ActorInterfaces;
using SCORLIB.ItemInterfaces;
using SCORLIB.Systems;

namespace SCORLIB.ItemClasses
{
  public class Armor : EquipmentBase, IArmor, ICloneable<Armor>
  {
    /// <summary>
    /// Where on the body this armor is meant to be worn.
    /// </summary>
    public Location Location { get; set; }

    /// <summary>
    /// The type of armor this is; i.e., boots, greaves,
    /// pauldrons, etc.
    /// </summary>
    public string ArmorType { get; set; }

    /// <summary>
    /// The minimum Armor Class this Armor can carry.
    /// </summary>
    public int MinAC { get; set; }

    /// <summary>
    /// The maximum Armor Class this Armor can carry.
    /// </summary>
    public int MaxAC { get; set; }

    /// <summary>
    /// The current Armor Class for this Armor.
    /// Between MinAC and MaxAC.
    /// </summary>
    public int ArmorClass { get; set; }

    /// <summary>
    /// Initializes an empty instance of a Weapon.
    /// Use for serialization or deep copying only.
    /// </summary>
    public Armor() : base()
    {

    }

    /// <summary>
    /// Creates a new Armor with the given parameters and sets it AllowableClasses so only those player classes
    /// can equip this Armor.
    /// </summary>
    /// <param name="name">The Armor name</param>
    /// <param name="level">The minimum level required to wear this Armor</param>
    /// <param name="value">The Armor's value in gold</param>
    /// <param name="location">Where on the body the Armor is worn (Head, Chest, etc.)</param>
    /// <param name="armorType">The type of Armor (cuirass, pauldrons, hat, etc.)</param>
    /// <param name="minAC">The minimum Armor Class of the Armor</param>
    /// <param name="maxAC">The maxmimum Armor Class of the Armor</param>
    /// <param name="restrictedClass">An array of Classes that will be allowed to wear this Armor</param>
    public Armor(string name, int level, int value, Location location, string armorType,
      int minAC, int maxAC, string restrictedClass) 
      : base(name, level, value, restrictedClass)
    {
      Location = location;
      ArmorType = armorType;
      MinAC = minAC;
      MaxAC = maxAC;
      ArmorClass = GetACValue(MinAC, MaxAC);
    }

    /// <summary>
    /// Determines the Armor Class for this Armor between its Min and Max values. 
    /// </summary>
    /// <param name="minAC">The MinAC this Armor can carry</param>
    /// <param name="maxAC">The MaxAC this Armor can carry</param>
    /// <returns>A random value between MinAC and MaxAC</returns>
    public int GetACValue(int minAC, int maxAC)
    {
      return Systems.Random.Next(minAC, maxAC);
    }

    /// <summary>
    /// Creates a friendly string representation of the Armor.
    /// </summary>
    /// <returns>A string with fixed formatting and AC formatted in a friendly manner</returns>
    public override string ToString()
    {
      StringBuilder armorString = new StringBuilder();

      armorString.AppendLine(FixedName);
      armorString.AppendLine($"  Armor Class: {ArmorClass}");
      if (Prefix != null)
      {
        armorString.AppendLine($"  + {Prefix.Mod1Value} {Prefix.Mod1Name.ToDisplayString()}");
      }
      if (Suffix != null)
      {
        armorString.AppendLine($"  + {Suffix.Mod1Value} {Suffix.Mod1Name.ToDisplayString()}");
      }

      return armorString.ToString();
    }

    #region IEquatable Region

    /// <summary>
    /// Determines whether the given Armor is the same as this Armor by values.
    /// </summary>
    /// <param name="other">The Armor to check for equality</param>
    /// <returns>True if all values are the same</returns>
    public bool Equals(Armor other)
    {
      if (other == null)
      {
        return false;
      }

      return Name == other.Name &&
        Level == other.Level &&
        Value == other.Value &&
        Location == other.Location &&
        ArmorType == other.ArmorType &&
        MinAC == other.MinAC &&
        MaxAC == other.MaxAC &&
        ArmorClass == other.ArmorClass &&
        RestrictedClass == other.RestrictedClass &&
        Prefix == other.Prefix &&
        Suffix == other.Suffix;
    }

    /// <summary>
    /// Determines whether the given Armor is the same as this Armor by reference or by values.
    /// </summary>
    /// <param name="obj">The Armor to check for equality</param>
    /// <returns>True if all values are the same, or if the reference is the same</returns>
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

      if (obj.GetType() != GetType())
      {
        return false;
      }

      return Equals(obj as Armor);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    #endregion

    #region ICloneable Region

    /// <summary>
    /// Creates a deep clone of the Armor.
    /// </summary>
    /// <returns>A new Armor object with the same properties as this Armor</returns>
    public Armor Clone()
    {
      Armor armor = new Armor(
        Name,
        Level,
        Value,
        Location,
        ArmorType,
        MinAC,
        MaxAC,
        RestrictedClass)
      {
        ArmorClass = ArmorClass
      };

      armor.AddAffix(Prefix, true);
      armor.AddAffix(Suffix, false);

      return armor;
    }

    /// <summary>
    /// Creates a new instance of this Armor object, and randomly determines its
    /// AC and Affixes.
    /// </summary>
    /// <returns>A New Armor with new AC and Affixes</returns>
    public Armor GetNew()
    {
      Armor armor = new Armor(
        Name,
        Level,
        Value,
        Location,
        ArmorType,
        MinAC,
        MaxAC,
        RestrictedClass);

      armor.AddMagicProperties();

      return armor;
    }

    #endregion
  }
}
