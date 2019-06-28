using System;
using SadConsole;
using SCRL.Systems;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Security.Cryptography;

namespace SCRL
{
  class Program
  {
    private static Container MainConsole;

    public const int Width = 100;
    public const int Height = 60;

    public static Random Random { get; private set; }

    static void Main(string[] args)
    {

      // Setup the engine and create the main window.
      SadConsole.Game.Create("C64.font", Width, Height);

      // Hook the start event so we can add consoles to the system.
      SadConsole.Game.OnInitialize = Init;

      // Hook the update event that happens each frame so we can trap keys and respond.
      SadConsole.Game.OnUpdate = Update;

      // Start the game. 
      SadConsole.Game.Instance.Run();

      //
      // Code here will not run until the game window closes.
      //

      SadConsole.Game.Instance.Dispose();
    }

    private static void Update(GameTime time)
    {
      // Called each logic update.

      // As an example, we'll use the F5 key to make the game full screen
      if (SadConsole.Global.KeyboardState.IsKeyReleased(Keys.F5))
      {
        SadConsole.Settings.ToggleFullScreen();
      }

      if (Global.KeyboardState.IsKeyReleased(Keys.Escape))
      {
        SadConsole.Game.Instance.Exit();
      }

      if (Global.KeyboardState.IsKeyReleased(Keys.Left))
      {
        
      }
    }

    private static void Init()
    {
      int seed = (int)DateTime.UtcNow.Ticks;
      Random = new Random(seed);

      SCORLIB.GameData.Initialize();

      SadConsole.Game.Instance.Window.Title = "SCRL - v0.01";
      MainConsole = new Container();


      // Set our new console as the thing to render and process
      Global.CurrentScreen = MainConsole;
    }
  }
}
