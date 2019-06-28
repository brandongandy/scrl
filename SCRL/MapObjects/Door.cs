using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCRL.Resources;

namespace SCRL.MapObjects
{
  class Door : Tile
  {
    public bool IsOpen { get; set; }
    public Door(int x, int y) : base(x, y, Palette.DarkWallForeground, Palette.DarkWallBackground, '+')
    {
      IsBlockingLOS = true;
      IsBlockingMove = false;

      LightForeground = Palette.DarkWallBackground;
      LightBackground = Palette.LightFloorForeground;
    }
  }
}
