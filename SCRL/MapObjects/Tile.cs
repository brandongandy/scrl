using Microsoft.Xna.Framework;
using System;
using SadConsole;
using SCRL.Interfaces;

namespace SCRL.MapObjects
{
  class Tile : Cell, ITile
  {
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsBlockingMove { get; set; }
    public bool IsBlockingLOS { get; set; }
    public bool IsMapped { get; set; }
    public bool IsInFOV { get; set; }
    public Color DarkBackground { get; set; }
    public Color DarkForeground { get; set; }
    public Color LightBackground { get; set; }
    public Color LightForeground { get; set; }

    /// <summary>
    /// Construct a new Tile with the given colors and glyph.
    /// </summary>
    /// <param name="x">The X coordinate of this Tile.</param>
    /// <param name="y">The Y coordinate of this Tile.</param>
    /// <param name="foreground">The foreground color for this Tile.</param>
    /// <param name="background">The background color for this Tile.</param>
    /// <param name="glyph">The glyph to display to the screen.</param>
    public Tile(int x, int y, Color foreground, Color background, int glyph) : base(foreground, background, glyph)
    {
      X = x;
      Y = y;
      DarkForeground = foreground;
      DarkBackground = background;
      LightForeground = Resources.Palette.Atlantis;
      LightBackground = DarkBackground;
    }

    /// <summary>
    /// Sets the default, unexplored and unmapped color variant for each tile.
    /// By default, this should be black, but the color palette may change.
    /// So this is its own method.
    /// </summary>
    public virtual void SetUnexploredVariant()
    {
      //Background = Color.Black;
      //Foreground = Color.Black;
    }

    /// <summary>
    /// Sets the lighter color variant for a tile that is currently within
    /// the player's FOV.
    /// </summary>
    public virtual void SetLightVariant()
    {
      Background = LightBackground;
      Foreground = LightForeground;
    }

    /// <summary>
    /// Sets the darker color variant for a tile that is currently outside
    /// the player's FOV, but is still mapped and has been explored.
    /// </summary>
    public virtual void SetDarkVariant()
    {
      Background = DarkBackground;
      Foreground = DarkForeground;
    }

    /// <summary>
    /// Determines whether two Tiles are equal. 
    /// </summary>
    /// <param name="other">The Tile to compare with this instance.</param>
    /// <returns>True if the instances are the same; false if not.</returns>
    public bool Equals(ITile other)
    {
      if (other == null)
      {
        return false;
      }

      if (ReferenceEquals(this, other))
      {
        return true;
      }

      return X == other.X &&
             Y == other.Y &&
             IsBlockingMove == other.IsBlockingMove &&
             IsBlockingLOS == other.IsBlockingLOS &&
             IsMapped == other.IsMapped &&
             IsInFOV == other.IsInFOV;
    }
  }
}
