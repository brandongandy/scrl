using SCORLIB;
using SCORLIB.ActorInterfaces;
using SCORLIB.ItemInterfaces;
using SCRL.Resources;

namespace SCRL.Entities
{
  class Player : Actor, IPlayer
  {
    public string CharacterClass { get; set; }
    public long Experience { get; private set; }

    /// <summary>
    /// The equipment currently worn on the Head.
    /// </summary>
    public IEquipment Head { get; private set; }

    /// <summary>
    /// The equipment currently worn on the shoulders.
    /// </summary>
    public IEquipment Shoulder { get; private set; }

    /// <summary>
    /// The equipment currently worn on the chest.
    /// </summary>
    public IEquipment Chest { get; private set; }

    /// <summary>
    /// The equipment currently worn on the hands.
    /// </summary>
    public IEquipment Hands { get; private set; }

    /// <summary>
    /// The equipment currently worn as a belt.
    /// </summary>
    public IEquipment Belt { get; private set; }

    /// <summary>
    /// The equipment currently worn on the legs.
    /// </summary>
    public IEquipment Legs { get; private set; }

    /// <summary>
    /// The equipment currently worn on the feet.
    /// </summary>
    public IEquipment Feet { get; private set; }

    /// <summary>
    /// The equipment currently worn as a ring on the left hand.
    /// </summary>
    public IEquipment LeftRing { get; private set; }

    /// <summary>
    /// The equipment currently worn as a ring on the right hand.
    /// </summary>
    public IEquipment RightRing { get; private set; }

    /// <summary>
    /// The equipment currently worn as an amulet.
    /// </summary>
    public IEquipment Amulet { get; private set; }

    public Player(): base(Palette.PlayerColor, Palette.LightFloorForeground, 1)
    {
      Name = "Rogue";
      Level = 1;
      Health = new AttributePair(100);
      Energy = new AttributePair(100);

      Constitution = 10;
      Wisdom = 15;
      Dexterity = 10;
      Gold = 100;

      CharacterClass = "Rogue";
      Experience = 0;

      // SadConsole GameObject property
      IsVisible = true;
    }
  }
}
