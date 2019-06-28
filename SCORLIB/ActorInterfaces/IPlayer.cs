using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCORLIB.ItemClasses;
using SCORLIB.ItemInterfaces;

namespace SCORLIB.ActorInterfaces
{
  public interface IPlayer
  {
    /// <summary>
    /// The Class of the Actor -- i.e., "Fighter", "Sorcerer", etc.
    /// </summary>
    string CharacterClass { get; set; }

    /// <summary>
    /// Holds how many experience points the Player has accrued so far. 
    /// </summary>
    long Experience { get; }

    /// <summary>
    /// The equipment currently worn on the Head.
    /// </summary>
    IEquipment Head { get; }

    /// <summary>
    /// The equipment currently worn on the shoulders.
    /// </summary>
    IEquipment Shoulder { get; }

    /// <summary>
    /// The equipment currently worn on the chest.
    /// </summary>
    IEquipment Chest { get; }

    /// <summary>
    /// The equipment currently worn on the hands.
    /// </summary>
    IEquipment Hands { get; }

    /// <summary>
    /// The equipment currently worn as a belt.
    /// </summary>
    IEquipment Belt { get; }

    /// <summary>
    /// The equipment currently worn on the legs.
    /// </summary>
    IEquipment Legs { get; }

    /// <summary>
    /// The equipment currently worn on the feet.
    /// </summary>
    IEquipment Feet { get; }

    /// <summary>
    /// The equipment currently worn as a ring on the left hand.
    /// </summary>
    IEquipment LeftRing { get; }

    /// <summary>
    /// The equipment currently worn as a ring on the right hand.
    /// </summary>
    IEquipment RightRing { get; }

    /// <summary>
    /// The equipment currently worn as an amulet.
    /// </summary>
    IEquipment Amulet { get; }
  }
}
