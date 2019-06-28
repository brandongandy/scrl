using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Console = SadConsole.Console;
using SadConsole;
using SCRL.Entities;
using SCRL.Interfaces;
using SCRL.Resources;
using System;

namespace SCRL.Screens
{
  /// <summary>
  /// The Belt represents the "hotbar" UI. It contains information on the Player
  /// health, mana/stamina, their hotkey commands, and more.
  /// </summary>
  class InventoryConsole : Window
  {
    private string invTitle = $"Inventory";
    private Console con;

    SadConsole.Surfaces.BasicSurface borderSurface;
    /// <summary>
    /// Instantiates the Belt console.
    /// </summary>
    /// <param name="width">How wide the Belt console should be.</param>
    /// <param name="height">How tall the Belt console should be.</param>
    public InventoryConsole(int width, int height) : base(width, height)
    {
      con = new Console(Width - 2, Height - 2)
      {
        Position = new Point(1, 1)
      };
      Children.Add(con);
      con.VirtualCursor.ResetAppearanceToConsole();
      con.VirtualCursor.PrintAppearance = new Cell(Resources.Palette.LightSteelBlue, Color.Transparent);

      SetBorderConsole();
      SetWindowProperties();
    }

    public override void Draw(TimeSpan delta)
    {
      base.Draw(delta);
      // 206
      Title = "";
      // this is awful awful awful
      // FIXME
      Print(4, 0, $"{(char)181}");
      Print(5, 0, $" Inventory ", Resources.Palette.GoldenFizz);
      Print(16, 0, $"{(char)198}");
      Print(18, 0, $"{(char)181} Stats {(char)198}");
      Print(28, 0, $"{(char)181} Menu {(char)198}");

      con.VirtualCursor.Position = new Point(1, 1);
      con.VirtualCursor.Print("This is where player equipment and inventory will be shown");
    }

    private void SetBorderConsole()
    {
      borderSurface = new SadConsole.Surfaces.BasicSurface(Width, Height, textSurface.Font);
      var editor = new SadConsole.Surfaces.SurfaceEditor(borderSurface);

      SadConsole.Shapes.Box box = SadConsole.Shapes.Box.Thick();

      box.Width = borderSurface.Width;
      box.Height = borderSurface.Height;
      box.Foreground = Resources.Palette.Rope;
      box.BorderBackground = TextSurface.DefaultBackground;
      Border = box;
      Border.Draw(editor);
      //box.Draw(editor);
      Renderer.Render(borderSurface);
    }

    private void SetWindowProperties()
    {
      Theme.BorderStyle = new Cell(Resources.Palette.Rope, Resources.Palette.Loulou);
      Theme.FillStyle = new Cell(Resources.Palette.LightSteelBlue, Resources.Palette.Loulou);
      //Theme.TitleStyle = new Cell(Resources.Palette.GoldenFizz, Resources.Palette.Loulou);
      Title = invTitle;

      Show(false);
    }
  }
}
