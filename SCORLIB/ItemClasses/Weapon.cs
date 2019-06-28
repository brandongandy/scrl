using System;
using System.Text;
using SCORLIB.ItemInterfaces;
using SCORLIB.ActorInterfaces;
using System.Linq;

namespace SCORLIB.ItemClasses
{
  [Serializable]
  public class Weapon : EquipmentBase, IWeapon, IEquatable<Weapon>, ICloneable<Weapon>
  {
    /// <summary>
    /// How many hands are required to wield this weapon.
    /// </summary>
    public HandsRequired HandsRequired { get; set; }

    /// <summary>
    /// The type of weapon; i.e., sword, wand, staff, etc.
    /// </summary>
    public string WeaponType { get; set; }

    /// <summary>
    /// The minimum amount of damage this weapon can inflict.
    /// </summary>
    public int MinDamage { get; set; }

    /// <summary>
    /// The maximum amount of damage this weapon can inflict.
    /// </summary>
    public int MaxDamage { get; set; }
    
    /// <summary>
    /// Initializes an empty instance of a Weapon.
    /// Use for serialization or deep copying only.
    /// </summary>
    public Weapon() : base()
    {

    }

    /// <summary>
    /// Creates a new Weapon with the given parameters and sets it AllowableClasses so only those player classes
    /// can wield this Weapon.
    /// </summary>
    /// <param name="name">The Weapon name</param>
    /// <param name="level">The minimum level required to wield the Weapon</param>
    /// <param name="value">The Weapon's value in gold</param>
    /// <param name="minDamage">The Weapon's minimum damage output</param>
    /// <param name="maxDamage">The Weapon's maximum damage output</param>
    /// <param name="handsRequired">How many hans are required to weild the Weapon</param>
    /// <param name="weaponType">The type of Weapon (sword, mace, etc.)</param>
    /// <param name="restricedClass">An array of Classes that will be allowed to use this Weapon</param>
    public Weapon(string name, int level, int value, int minDamage, int maxDamage, HandsRequired handsRequired, string weaponType, 
      string restricedClass) 
      : base(name, level, value, restricedClass)
    {
      MinDamage = minDamage;
      MaxDamage = maxDamage;
      HandsRequired = handsRequired;
      WeaponType = weaponType;
    }

    /// <summary>
    /// Creates a friendly string representation of the Weapon.
    /// </summary>
    /// <returns>A string with fixed formatting and damage output formatted in a friendly manner</returns>
    public override string ToString()
    {
      StringBuilder wpnString = new StringBuilder();

      wpnString.AppendLine(FixedName);
      wpnString.AppendLine($"Damage: {MinDamage} - {MaxDamage}");

      return wpnString.ToString();
    }

    #region IEquatable Region

    /// <summary>
    /// Determines whether the given Weapon is the same as this Weapon by values.
    /// </summary>
    /// <param name="other">The Weapon to check for equality</param>
    /// <returns>True if all values are the same</returns>
    public bool Equals(Weapon other)
    {
      if (other is null)
      {
        return false;
      }

      return Name == other.Name &&
        Level == other.Level &&
        Value == other.Value &&
        MinDamage == other.MinDamage &&
        MaxDamage == other.MaxDamage &&
        HandsRequired == other.HandsRequired &&
        WeaponType == other.WeaponType &&
        RestrictedClass == other.RestrictedClass &&
        Prefix == other.Prefix &&
        Suffix == other.Suffix;
    }

    /// <summary>
    /// Determines whether the given Weapon is the same as this Weapon by reference or by values.
    /// </summary>
    /// <param name="obj">The Weapon to check for equality</param>
    /// <returns>True if all values are the same, or if the reference is the same</returns>
    public override bool Equals(object obj)
    {
      if (obj == null)
      {
        return false;
      }

      if (ReferenceEquals(obj, this))
      {
        return true;
      }

      if (obj.GetType() != GetType())
      {
        return false;
      }

      return Equals(obj as Weapon);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    #endregion

    #region ICloneable Region

    /// <summary>
    /// Creates a deep clone of the Weapon.
    /// </summary>
    /// <returns>A new Weapon object with the same properties as this Weapon</returns>
    public Weapon Clone()
    {
      Weapon weapon = new Weapon(
        Name, 
        Level, 
        Value, 
        MinDamage,
        MaxDamage,
        HandsRequired,
        WeaponType,
        RestrictedClass);

      weapon.AddAffix(Prefix, true);
      weapon.AddAffix(Suffix, false);

      return weapon;
    }

    /// <summary>
    /// Creates a new instances of this Weapon object, and randomly determines its
    /// Affixes.
    /// </summary>
    /// <returns>A New Weapon with new Affixes</returns>
    public Weapon GetNew()
    {
      Weapon weapon = new Weapon(
        Name,
        Level,
        Value,
        MinDamage,
        MaxDamage,
        HandsRequired,
        WeaponType,
        RestrictedClass);

      weapon.AddMagicProperties();

      return weapon;
    }

    #endregion
  }
}
