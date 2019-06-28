using Microsoft.Xna.Framework;
using SCRL.Resources;

namespace SCRL.MapObjects
{
  class Floor : Tile
  {
    public Floor(int x, int y) : base(x, y, Palette.DarkFloorForeground, Palette.DarkFloorBackground, 219)
    {
      //177
      IsBlockingLOS = false;
      IsBlockingMove = false;

      LightForeground = Palette.LightFloorForeground;
    }

    public Floor(int x, int y, int variant) : base(x, y, Palette.DeepKoamaru, Palette.DarkFloorBackground, 219)
    {
      IsBlockingLOS = false;
      IsBlockingMove = false;

      LightForeground = Palette.DeepKoamaru;
    }
  }
}
