using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCORLIB
{
  /// <summary>
  /// Methods for interacting with TreasureClass game data
  /// </summary>
  public static class TreasureFactory
  {
    public static object GetDropFromTreasureClass(string treasureClass)
    {
      string item = GameData.TreasureClasses.GetDropFromTreasureClass(treasureClass);

      if (GameData.Armors.Keys.Contains(item))
      {
        return GameData.Armors[item].GetNew();
      }

      if (GameData.Weapons.Keys.Contains(item))
      {
        return GameData.Weapons[item].GetNew();
      }

      else return null;
    }
  }
}
