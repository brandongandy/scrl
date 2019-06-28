using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCORLIB.ActorClasses;
using SCORLIB.ActorInterfaces;

namespace SCORLIB
{
  /// <summary>
  /// Methods for interacting with the GameData.Monsters list.
  /// </summary>
  public static class MonsterFactory
  {
    /// <summary>
    /// Gets any Monster whose level matches the level specified.
    /// </summary>
    /// <param name="level">The Monster level to search through</param>
    /// <returns>A reference to a Monster class whosel evel matches the level given</returns>
    public static Actor GetMonsterAtLevel(int level)
    {
      var monsterList = GameData.Monsters.FindAll(m => m.Level == level);
      return monsterList[Systems.Random.Next(0, monsterList.Count - 1)];
    }

    /// <summary>
    /// Gets any Monster with a level at or below the level specified.
    /// </summary>
    /// <param name="level">The maximum level Monster desired</param>
    /// <returns>A reference to the first Monster found in the list whose level is at or below the level given</returns>
    public static Actor GetMonsterAtOrBelowLevel(int level)
    {
      var monsterList = GameData.Monsters.FindAll(m => m.Level <= level);
      return monsterList[Systems.Random.Next(0, monsterList.Count - 1)];
    }

    /// <summary>
    /// Gets an instance of a Monster whose name matches the name given.
    /// </summary>
    /// <param name="name">The unique identifier for a Monster</param>
    /// <returns>A reference to the Monster class matching the unique name given</returns>
    public static Actor GetMonsterByName(string baseId)
    {
      return GameData.Monsters.Where(m => m.BaseId == baseId).FirstOrDefault();
    }

    /// <summary>
    /// Gets an instance of a Monster with a Classification matching the classification given.
    /// </summary>
    /// <param name="classification">The Classification to search for (e.g. Skeleton)</param>
    /// <returns>A reference to the first Monster found matching the Classification given</returns>
    public static Actor GetMonsterByClassification(string classification)
    {
      // TODO: Inefficient. Fix.
      var monsterList = GameData.Monsters.FindAll(m => m.Classification == classification);
      return monsterList[Systems.Random.Next(0, monsterList.Count - 1)];
    }
  }
}
