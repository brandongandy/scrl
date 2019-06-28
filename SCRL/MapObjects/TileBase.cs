using SadConsole;
using SCRL.Resources;
using Microsoft.Xna.Framework;

namespace SCRL.MapObjects
{
  class TileBase : Cell
  {
    public int X;
    public int Y;
    public bool IsBlockingMove;
    public bool IsBlockingLOS;
    public bool IsMapped = false;
    public bool IsInFOV = false;
    public Color DarkBackground;
    public Color DarkForeground;
    public Color LightBackground;
    public Color LightForeground;

    public TileBase(Color foreground, Color background, int glyph) : base(foreground, background, glyph)
    {
      DarkForeground = foreground;
      DarkBackground = background;
      LightForeground = Resources.Palette.Atlantis;
      LightBackground = DarkBackground;
    }

    public void SetUnexploredVariant()
    {
      Background = Color.Black;
      Foreground = Color.Black;
    }

    public void SetLightVariant()
    {
      Background = LightBackground;
      Foreground = LightForeground;
    }

    public void SetDarkVariant()
    {
      Background = DarkBackground;
      Foreground = DarkForeground;
    }
  }
}
