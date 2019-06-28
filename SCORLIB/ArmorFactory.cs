using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCORLIB.ItemClasses;

namespace SCORLIB
{
  /// <summary>
  /// Methods for interacting with the Armors list in SCORLIB.GameData.
  /// </summary>
  public static class ArmorFactory
  {
    /// <summary>
    /// Gets any Armor with a level matching the level specified.
    /// </summary>
    /// <param name="level">The Armor level to search through</param>
    /// <returns>A New piece of Armor whose level matches the level given</returns>
    public static Armor GetArmorAtLevel(int level)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Gets any Armor with a level at or below the level specified.
    /// </summary>
    /// <param name="level">The maximum level Armor desired</param>
    /// <returns>A New piece of Armor whose level is at or below the level given</returns>
    public static Armor GetArmorAtOrBelowLevel(int level)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Gets a New piece of Armor whose name matches the name given.
    /// </summary>
    /// <param name="name">The Name of the Armor to search for</param>
    /// <returns>A New piece of Armor whose name matches the name given</returns>
    public static Armor GetArmorByName(string name)
    {
      if (GameData.Armors.Keys.Contains(name))
      {
        return GameData.Armors.Where(a => a.Key == name).FirstOrDefault().Value.GetNew();
      }
      else
      {
        throw new KeyNotFoundException();
      }
    }

    /// <summary>
    /// Gets any Armor with a Type matching the Type specified.
    /// </summary>
    /// <param name="type">The Type of armor desired</param>
    /// <returns>A New piece of Armor of the Type given</returns>
    public static Armor GetArmorByType(string type)
    {
      throw new NotImplementedException();
    }
  }
}
