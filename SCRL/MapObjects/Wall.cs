using Microsoft.Xna.Framework;
using SCRL.Resources;

namespace SCRL.MapObjects
{
  class Wall : Tile
  {
    public Wall(int x, int y) : base(x, y, Palette.DarkWallForeground, Palette.DarkWallBackground, 176)
    {
      IsBlockingLOS = true;
      IsBlockingMove = true;

      LightForeground = Palette.LightWallForeground;
    }
  }
}
