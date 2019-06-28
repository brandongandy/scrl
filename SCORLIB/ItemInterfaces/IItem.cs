namespace SCORLIB.ItemInterfaces
{
  public interface IItem
  {
    /// <summary>
    /// The minimum level required to use or wield the item.
    /// Also used for determining minimum level for item spawning in the world.
    /// </summary>
    int Level { get; set; }

    /// <summary>
    /// The name of the item.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// The item's base monetary value.
    /// </summary>
    int Value { get; set; }
  }
}
