using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCORLIB.ItemClasses
{
  public class TreasureClass
  {
    public Dictionary<string, List<string>> LootTable { get; private set; }

    public TreasureClass()
    {
      LootTable = new Dictionary<string, List<string>>();
    }

    /// <summary>
    /// Adds items to the Loot Table. The Key must be unique, and the Values may refer to
    /// individual items, or to another Treasure Class Key.
    /// </summary>
    /// <param name="key">A unique key to define the Treasure Class</param>
    /// <param name="values">Items or other Treasure Classes which may drop from this one</param>
    public void AddTreasureClass(string key, params string[] values)
    {
      List<string> vals = new List<string>();

      foreach (string value in values)
      {
        vals.Add(value);
      }

      LootTable.Add(key, vals);
    }

    /// <summary>
    /// Recursively scans through the available treasure in the LootTable and returns 
    /// all items in the tree all the way down.
    /// </summary>
    /// <param name="key">The TreasureClass name to begin searching at</param>
    /// <returns>All children of the TreasureClass, and their children</returns>
    public IEnumerable<string> GetTreasureClassDescendants(string key)
    {
      if (LootTable.Keys.Contains(key))
      {
        foreach (var item in LootTable[key])
        {
          foreach (var subItem in GetTreasureClassDescendants(item))
          {
            yield return subItem;
          }

          yield return item;
        }
      }
    }

    /// <summary>
    /// Iterates through a TreasureClass item and its descendents, picking a child
    /// at random before selecting, then returns a key for the first item found.
    /// </summary>
    /// <param name="key">The TreasureClass name to begin searching at</param>
    /// <returns>The first non-TreasureClass item found among the TreasureClass's children</returns>
    public string GetDropFromTreasureClass(string key)
    {
      string i = null;
      if (LootTable.Keys.Contains(key))
      {
        List<string> items = new List<string>();
        LootTable.TryGetValue(key, out items);

        int index = Systems.Random.Next(0, items.Count - 1);
        if (items[index].StartsWith("tc"))
        {
          i = GetDropFromTreasureClass(items[index]);
        }
        else
        {
          i = items[index];
        }
      }
      return i ?? throw new KeyNotFoundException();
    }
  }
}
