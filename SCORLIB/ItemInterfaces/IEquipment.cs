using SCORLIB.ActorInterfaces;
using SCORLIB.ItemClasses;
using System.Collections.Generic;

namespace SCORLIB.ItemInterfaces
{
  /// <summary>
  /// Denotes how many hands are requried to wield or use an item.
  /// </summary>
  public enum HandsRequired
  {
    None = 0,
    One = 1,
    Two = 2
  }

  /// <summary>
  /// The location an item may be worn or used.
  /// </summary>
  public enum Location
  {
    Head = 0,
    Shoulders = 1,
    Neck = 2,
    Body = 3,
    Hands = 4,
    Shield = 5,
    Ring = 6,
    Belt = 7,
    Legs = 8,
    Feet = 9
  }

  public interface IEquipment
  {
    /// <summary>
    /// Which class types are allowed to wield this equipment.
    /// </summary>
    string RestrictedClass { get; }

    /// <summary>
    /// The Prefix attached to this equipment.
    /// </summary>
    Affix Prefix { get; set; }

    /// <summary>
    /// The Suffix attached to this equipment.
    /// </summary>
    Affix Suffix { get; set; }

    bool CanEquip(IPlayer player);
  }
}
