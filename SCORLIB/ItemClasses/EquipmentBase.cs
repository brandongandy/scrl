using System;
using SCORLIB.ActorInterfaces;
using SCORLIB.ItemInterfaces;
using SCORLIB.Systems;
using System.Collections.Generic;

namespace SCORLIB.ItemClasses
{
  public class EquipmentBase : ItemBase, IEquipment
  {
    public string RestrictedClass { get; }
    public Affix Prefix { get; set; }
    public Affix Suffix { get; set; }
    public string FixedName
    {
      get {
        return NameFixer();
      }
    }

    /// <summary>
    /// Initializes an empty instance of Equipment.
    /// Use for serialization or deep copying only.
    /// </summary>
    public EquipmentBase() : base()
    {

    }

    public EquipmentBase(string name, int level, int value) : this()
    {
      Name = name;
      Level = level;
      Value = value;
    }

    public EquipmentBase(string name, int level, int value, string restrictedClass)
      : this(name, level, value)
    {
      if (restrictedClass != null)
        RestrictedClass = restrictedClass;
    }

    public void AddMagicProperties()
    {
      if (Systems.Random.NextDouble() > 0.5)
      {
        AddAffix(GameData.GeneratePrefix(), true);
      }

      if (Systems.Random.NextDouble() < 0.5)
      {
        AddAffix(GameData.GenerateSuffix(), false);
      }
    }

    /// <summary>
    /// Adds an Affix to this piece of Equipment.
    /// </summary>
    /// <param name="affix">The Affix to add to this Equipment</param>
    /// <param name="isPrefix">True if the Affix is a Prefix</param>
    public void AddAffix(Affix affix, bool isPrefix)
    {
      if (isPrefix)
      {
        Prefix = affix;
      }
      else
      {
        Suffix = affix;
      }
    }

    /// <summary>
    /// Concatenates the Affixes to the Name, if they exist, and formats the whole in a pretty manner for display.
    /// </summary>
    /// <returns>A display-fixed Name</returns>
    private string NameFixer()
    {
      string name = " ";
      name += (Prefix == null) ? "" : $"{Prefix.Name.ToDisplayString(true)} ";
      name += Name.ToDisplayString(true);
      name += (Suffix == null) ? "" : $" {Suffix.Name.ToDisplayString(true)}";
      return name;
    }

    /// <summary>
    /// Checks to see if an Actor can equip this Equipment.
    /// </summary>
    /// <param name="actor"></param>
    /// <returns></returns>
    public bool CanEquip(IPlayer player)
    {
      throw new NotImplementedException();
    }
  }
}
