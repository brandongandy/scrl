using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Console = SadConsole.Console;
using SadConsole;
using SadConsole.Renderers;
using SadConsole.Surfaces;
using SCRL.Interfaces;
using SCRL.Mapping;
using SCRL.MapObjects;
using System;
using SadConsole.Input;

namespace SCRL.Screens
{
  class Dungeon : Console
  {
    #region Fields

    private SurfaceRenderer renderer = new SurfaceRenderer();
    private DrawCallSurface drawCall;

    #endregion

    #region Properties

    public Point MousePosition { get; private set; }

    public ConsoleMetadata Metadata
    {
      get 
      {
        return new ConsoleMetadata()
        {
          Title = "Dungeon",
          Summary = "We're gonna do the thing!"
        };
      }
    }

    #endregion

    public Point MapViewPoint
    {
      get 
      {
        return TextSurface.RenderArea.Location;
      }
      set 
      {
        TextSurface.RenderArea = new Rectangle(value, new Point(Container.AdventureSize.X, Container.AdventureSize.Y));
      }
    }

    public Dungeon(int screenX, int screenY, int screenWidth, int screenHeight) : base(screenWidth, screenHeight)
    {
      Position = new Point(screenX, screenY);
    }

    public void LoadMap(Map map)
    {
      TextSurface = new BasicSurface(map.Width, map.Height, map.Tiles, 
        Global.FontDefault, new Rectangle(0, 0, map.Width, map.Height));
      drawCall = new DrawCallSurface(TextSurface, position, false);
    }

    public override void Draw(TimeSpan timeElapsed)
    {
      renderer.Render(TextSurface);
      Global.DrawCalls.Add(drawCall);
      base.Draw(timeElapsed);
    }

    public override bool ProcessMouse(MouseConsoleState state)
    {
      if (state != null)
        MousePosition = state.CellPosition;
      return base.ProcessMouse(state);
    }
  }
}
