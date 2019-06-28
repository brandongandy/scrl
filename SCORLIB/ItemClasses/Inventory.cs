using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCORLIB.ActorInterfaces;
using SCORLIB.ItemInterfaces;

namespace SCORLIB.ItemClasses
{
  /// <summary>
  /// The Inventory represents all that is currently held by an Actor.
  /// This includes both equipped items and held items, such as in a backpack
  /// or on a belt.
  /// </summary>
  public class Inventory
  {
    /// <summary>
    /// The list of items currently in the Player's Bag (i.e., not equipped)
    /// </summary>
    public List<ItemBase> BagItems { get; private set; }

    /// <summary>
    /// How many Items are currently in the Player's Bag
    /// </summary>
    public int ItemCount { get { return BagItems.Count(); } }

    /// <summary>
    /// The maximum allowed number of items in the Player's Bag.
    /// </summary>
    public int MaxItemCount { get { return 26; } }

    public Inventory()
    {
      BagItems = new List<ItemBase>();
    }

    /// <summary>
    /// Adds an Item to the Inventory, stacking it if necessary.
    /// </summary>
    /// <param name="item">The Item to add to the Inventory</param>
    /// <returns>True if the Item was successfully added to the Inventory, false if not</returns>
    public bool AddItem(ItemBase item)
    {
      if (item != null && ItemCount < MaxItemCount)
      {
        if (item is ConsumableBase)
        {
          var stackable = item as ConsumableBase;

          if (!stackable.Stack())
          {
            BagItems.Add(stackable);
          }

          return true;
        }
        else
        {
          BagItems.Add(item);
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Removes an Item from the Inventory, unstacking it if necessary.
    /// </summary>
    /// <param name="item">The Item to remove from the Inventory</param>
    public bool RemoveItem(ItemBase item)
    {
      if (item != null && BagItems.Contains(item))
      {
        if (item is ConsumableBase)
        {
          var stackable = item as ConsumableBase;

          if (!stackable.Unstack())
          {
            BagItems.Remove(stackable);
            return true;
          }
          else
          {
            return true;
          }
        }
        else
        {
          BagItems.Remove(item);
          return true;
        }
      }

      return false;
    }

    public void Empty()
    {
      BagItems.Clear();
    }
  }
}
