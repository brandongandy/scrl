using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCORLIB.ItemInterfaces;

namespace SCORLIB.ItemClasses
{
  public abstract class ConsumableBase : ItemBase, IConsumable
  {
    /// <summary>
    /// How many uses are left before the consumable is destroyed.
    /// </summary>
    public int RemainingUses { get; private set; }

    /// <summary>
    /// Whether or not the Consumable can be stacked inside an Inventory.
    /// </summary>
    public bool Stackable { get; private set; }

    /// <summary>
    /// The maximum number of Consumables that can be stacked together at once.
    /// </summary>
    public int MaxStack { get; private set; }

    /// <summary>
    /// If stackable, how many are currently held in an Inventory slot.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    /// Initializes an empty Consumble.
    /// </summary>
    protected ConsumableBase() : base()
    {

    }

    protected ConsumableBase(string name, int level, int value, int remainingUses, bool stackable, 
      int maxStack, int count)
      : base(name, level, value)
    {
      RemainingUses = remainingUses;
      Stackable = stackable;
      MaxStack = maxStack;
      Count = count;
    }

    /// <summary>
    /// If the Consumable is Stackable and under its Max Stack, then the current count is incremented.
    /// </summary>
    /// <returns>True if the stack was successful, false if the item could not be stacked</returns>
    public bool Stack()
    {
      if (Stackable && Count < MaxStack)
      {
        Count++;
        return true;
      }

      return false;
    }

    /// <summary>
    /// If the Consumable is stacked and has enough items in the stack to remove one, 
    /// then the current count is decremented.
    /// </summary>
    /// <returns>True if the unstack was successful, false if there were no items to unstack</returns>
    public bool Unstack()
    {
      if (Count < 1)
      {
        return false;
      }

      Count--;
      return true;
    }

    public abstract bool Use();
  }
}
