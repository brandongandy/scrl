using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCORLIB.Systems
{
  /// <summary>
  /// An instance of a Pool Item to be added to a weighted pool, such as loot
  /// or a monster.
  /// </summary>
  /// <typeparam name="T">The type of item to be pooled</typeparam>
  public class PoolItem<T>
  {
    /// <summary>
    /// The PoolItem's Weight -- how likely it is to be chosen
    /// </summary>
    /// <remarks>
    /// Think of the Weight as a percent chance of this item being chosen. Assigning
    /// a Weight of 25 means this Item has a 25% chance of being chosen, vs. perhaps
    /// another item with a Weight of 50. However, this is not perfectly random. Two
    /// Items may have a Weight of 25, and a third may have a Weight of 50. A 20 may
    /// be "rolled", but the Weight 50 Item may turn up first in the list, so it'll
    /// be chosen.
    /// </remarks>
    public int Weight { get; set; }

    /// <summary>
    /// The PoolItem itself
    /// </summary>
    public T Item { get; set; }
  }

  /// <summary>
  /// A weighted pool of objects to pick from randomly
  /// </summary>
  /// <typeparam name="T">The type of the objects which are pooled</typeparam>
  public class Pool<T>
  {
    private readonly List<PoolItem<T>> poolItems;
    private int totalWeight;

    /// <summary>
    /// Initializes an empty Pool with no items
    /// </summary>
    public Pool()
    {
      poolItems = new List<PoolItem<T>>();
    }

    /// <summary>
    /// Gets a random item from the Pool according 
    /// </summary>
    /// <returns></returns>
    public T Get()
    {
      int runningWeight = 0;
      int roll = Random.Next(1, totalWeight);

      foreach (var poolItem in poolItems)
      {
        runningWeight += poolItem.Weight;

        if (roll <= runningWeight)
        {
          Remove(poolItem);
          return poolItem.Item;
        }
      }

      throw new InvalidOperationException("Could not get an item from the pool.");
    }

    /// <summary>
    /// Adds an item to the Pool and updates the TotalWeight of the Pool
    /// </summary>
    /// <param name="item">The item to add to the Pool</param>
    /// <param name="weight">The item's weight (likelihood of being chosen)</param>
    public void Add(T item, int weight)
    {
      poolItems.Add(new PoolItem<T>{Item = item, Weight = weight });
      totalWeight += weight;
    }

    /// <summary>
    /// Removes an item from the Pool and updates the TotalWeight of the Pool
    /// </summary>
    /// <param name="poolItem">The PoolItem to remove from the Pool</param>
    public void Remove(PoolItem<T> poolItem)
    {
      if (poolItem != null && poolItems.Contains(poolItem))
      {
        poolItems.Remove(poolItem);
        totalWeight -= poolItem.Weight;
      }
    }
  }
}
