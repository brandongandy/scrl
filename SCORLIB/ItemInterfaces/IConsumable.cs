namespace SCORLIB.ItemInterfaces
{
  public interface IConsumable
  {
    /// <summary>
    /// How many uses are left before the consumable is destroyed.
    /// </summary>
    int RemainingUses { get; }

    /// <summary>
    /// Whether or not the Consumable can be stacked inside an Inventory.
    /// </summary>
    bool Stackable { get; }
    
    /// <summary>
    /// The maximum number of Consumables that can be stacked together at once.
    /// </summary>
    int MaxStack { get; }

    /// <summary>
    /// If stackable, how many are currently held in an Inventory slot.
    /// </summary>
    int Count { get; }

    bool Stack();

    /// <summary>
    /// When used, attempts to Use the Consumable.
    /// </summary>
    /// <returns>True if the Use was successful</returns>
    bool Use();
  }
}
