using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Console = SadConsole.Console;
using SadConsole;
using SCRL.Systems;
using System;

namespace SCRL.Screens
{
  /// <summary>
  /// The Belt represents the "hotbar" UI. It contains information on the Player
  /// health, mana/stamina, their hotkey commands, and more.
  /// </summary>
  class MessageLogConsole : Window
  {
    SadConsole.Surfaces.BasicSurface borderSurface;
    private Console messages;

    /// <summary>
    /// Instantiates the Belt console.
    /// </summary>
    /// <param name="width">How wide the Belt console should be.</param>
    /// <param name="height">How tall the Belt console should be.</param>
    public MessageLogConsole(int width, int height) : base(width, height)
    {
      messages = new Console(new SadConsole.Surfaces.SurfaceView(this.TextSurface, new Rectangle(2, 2, Width - 4, Height - 4)));

      messages.VirtualCursor.PrintAppearance = new Cell(Resources.Palette.LightSteelBlue, Resources.Palette.Loulou);

      SetBorderConsole();
      SetWindowProperties();
    }

    public override void Update(TimeSpan delta)
    {
      base.Update(delta);

    }

    public override void Draw(TimeSpan delta)
    {
      base.Draw(delta);
      Clear();
      Title = "";
      Print(4, 0, $"{(char)181}");
      Print(5, 0, $" Messages ", Resources.Palette.GoldenFizz, Resources.Palette.Loulou);
      Print(15, 0, $"{(char)198}");

      foreach (var line in MessageLog.Lines)
      {
        messages.VirtualCursor.NewLine().Print(line);
      }
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
      Title = "Messages";

      Show(false);
    }
  }
}
