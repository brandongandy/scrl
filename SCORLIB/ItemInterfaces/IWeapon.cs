namespace SCORLIB.ItemInterfaces
{
  public interface IWeapon
  {
    /// <summary>
    /// How many hands are required to wield this weapon.
    /// </summary>
    HandsRequired HandsRequired { get; set; }

    /// <summary>
    /// The type of weapon; i.e., sword, wand, staff, etc.
    /// </summary>
    string WeaponType { get; set; }

    /// <summary>
    /// The minimum amount of damage this weapon can inflict.
    /// </summary>
    int MinDamage { get; set; }

    /// <summary>
    /// The maximum amount of damage this weapon can inflict.
    /// </summary>
    int MaxDamage { get; set; }
  }
}
