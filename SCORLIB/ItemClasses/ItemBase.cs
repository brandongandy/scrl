using System;
using SCORLIB.ItemInterfaces;

namespace SCORLIB.ItemClasses
{
  public abstract class ItemBase : IItem, IEquatable<ItemBase>
  {
    public string Name { get; set; }
    public int Level { get; set; }
    public int Value { get; set; }

    /// <summary>
    /// Initializes an empty instance of an Item.
    /// Use for serialization or deep copying only.
    /// </summary>
    protected ItemBase()
    {

    }

    protected ItemBase(string name, int level, int value)
    {
      Name = name;
      Level = level;
      Value = value;
    }

    /// <summary>
    /// Determines if an ItemBase item is equal to another by reference. Use only
    /// when comparing inherited classes, such as within the Inventory.
    /// </summary>
    /// <param name="other">The other Item to check for equality</param>
    /// <returns>True if the reference equals this ItemBase</returns>
    public bool Equals(ItemBase other)
    {
      if (other == null)
      {
        return false;
      }
      
      if (ReferenceEquals(this, other))
      {
        return true;
      }
      else
      {
        return false;
      }
    }
  }
}
