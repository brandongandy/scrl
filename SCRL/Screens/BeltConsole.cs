using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Console = SadConsole.Console;
using SadConsole;
using SadConsole.Shapes;
using SadConsole.Themes;
using System;

namespace SCRL.Screens
{
  /// <summary>
  /// The Belt represents the "hotbar" UI. It contains information on the Player
  /// health, mana/stamina, their hotkey commands, and more.
  /// </summary>
  class BeltConsole : Window
  {
    SadConsole.Surfaces.BasicSurface borderSurface;
    Circle health = new Circle();
    Circle stamina = new Circle();

    /// <summary>
    /// Instantiates the Belt console.
    /// </summary>
    /// <param name="width">How wide the Belt console should be.</param>
    /// <param name="height">How tall the Belt console should be.</param>
    public BeltConsole(int width, int height) : base(width, height)
    {
      SetBorderConsole();
      SetWindowProperties();
      InitHealthAndStamina();
      this.IsFocused = false;
    }

    public override void Draw(TimeSpan delta)
    {
      base.Draw(delta);
      Clear();
      // setting the Title after Clearing makes the Window draw its Border again
      // gotta be a better way but I'm moving on for now.
      Title = "";
      Print(4, 0, $"{(char)181}");
      Print(5, 0, $" Belt ", Resources.Palette.GoldenFizz);
      Print(11, 0, $"{(char)198}");

      health.Draw(this);
      stamina.Draw(this);
      DrawHealthAndStamina();
    }

    private void SetBorderConsole()
    {
      borderSurface = new SadConsole.Surfaces.BasicSurface(Width, Height, textSurface.Font);
      var editor = new SadConsole.Surfaces.SurfaceEditor(borderSurface);

      Box box = Box.Thick();

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
      Theme.TitleStyle = new Cell(Resources.Palette.GoldenFizz, Resources.Palette.Loulou);
      Title = "";
      
      Show(false);
    }

    private void InitHealthAndStamina()
    {
      health.BorderAppearance = new Cell(Resources.Palette.Mandy, Resources.Palette.Mandy);
      health.Center = new Point(7, 8);
      health.Radius = 4;

      stamina.BorderAppearance = new Cell(Resources.Palette.RoyalBlue, Resources.Palette.RoyalBlue);
      stamina.Center = new Point(51, 8);
      stamina.Radius = 4;

      int column1 = 22;
      int column2 = 26;
      int column3 = 30;
      int column4 = 34;
      int row1 = 2;
      int row2 = 6;
      int row3 = 10;

      var button1 = new SadConsole.Controls.Button(3, 3);
      button1.Text = "1";
      button1.Position = new Point(column1, row1);
      Add(button1);

      var button2 = new SadConsole.Controls.Button(3, 3);
      button2.Text = "2";
      button2.Position = new Point(column2, row1);
      Add(button2);

      var button3 = new SadConsole.Controls.Button(3, 3);
      button3.Text = "3";
      button3.Position = new Point(column3, row1);
      Add(button3);

      var button4 = new SadConsole.Controls.Button(3, 3);
      button4.Text = "4";
      button4.Position = new Point(column4, row1);
      Add(button4);

      var buttonQ = new SadConsole.Controls.Button(3, 3);
      buttonQ.Text = "Q";
      buttonQ.Position = new Point(column1, row2);
      Add(buttonQ);

      var buttonW = new SadConsole.Controls.Button(3, 3);
      buttonW.Text = "W";
      buttonW.Position = new Point(column2, row2);
      Add(buttonW);

      var buttonE = new SadConsole.Controls.Button(3, 3);
      buttonE.Text = "E";
      buttonE.Position = new Point(column3, row2);
      Add(buttonE);

      var buttonR = new SadConsole.Controls.Button(3, 3);
      buttonR.Text = "R";
      buttonR.Position = new Point(column4, row2);
      Add(buttonR);

      var buttonA = new SadConsole.Controls.Button(3, 3);
      buttonA.Text = "A";
      buttonA.Position = new Point(column1, row3);
      Add(buttonA);

      var buttonS = new SadConsole.Controls.Button(3, 3);
      buttonS.Text = "S";
      buttonS.Position = new Point(column2, row3);
      Add(buttonS);

      var buttonD = new SadConsole.Controls.Button(3, 3);
      buttonD.Text = "D";
      buttonD.Position = new Point(column3, row3);
      Add(buttonD);

      var buttonF = new SadConsole.Controls.Button(3, 3);
      buttonF.Text = "F";
      buttonF.Position = new Point(column4, row3);
      Add(buttonF);

      var buttonInv = new SadConsole.Controls.Button(3, 3);
      buttonInv.Text = "I";
      buttonInv.Position = new Point(column1 - 5, row2 - 2);
      Add(buttonInv);

      var buttonSkills = new SadConsole.Controls.Button(3, 3);
      buttonSkills.Text = "K";
      buttonSkills.Position = new Point(column4 + 5, row2 - 2);
      Add(buttonSkills);

      var buttonMain = new SadConsole.Controls.Button(5, 5);
      buttonMain.Text = "L-M";
      buttonMain.Position = new Point(column1 - 7, row3 - 2);
      Add(buttonMain);

      var buttonOffhand = new SadConsole.Controls.Button(5, 5);
      buttonOffhand.Text = "R-M";
      buttonOffhand.Position = new Point(column4 + 5, row3 - 2);
      Add(buttonOffhand);

      foreach (SadConsole.Controls.Button button in Controls)
      {
        button.Theme = GetButtonTheme();
      }
    }

    private ButtonTheme GetButtonTheme()
    {
      ButtonTheme theme = new ButtonTheme();
      var foreground = Resources.Palette.Twine;
      var foregroundHighlight = Resources.Palette.GoldenFizz;
      var background = Resources.Palette.Rope;

      theme.Normal = new Cell(foreground, background);
      theme.Focused = new Cell(background, foreground);
      theme.MouseOver = new Cell(foregroundHighlight, foreground);
      theme.MouseClicking = new Cell(foregroundHighlight, background);

      return theme;
    }

    private void DrawHealthAndStamina()
    {
      Print(3, 2, $"HP:");
      Print(stamina.Center.X - stamina.Radius, 2, $"EP:");
      Print(health.Center.X + health.Radius - 2, 2, "100", Resources.Palette.Mandy);
      Print(stamina.Center.X + stamina.Radius - 2, 2, "100", Resources.Palette.RoyalBlue);

      for (int i = 0; i < health.Radius; i++)
      {
        Algorithms.Circle(health.Center.X, health.Center.Y,
          i, (x, y) =>
          {
            if (this.IsValidCell(x, y))
              SetCell(x, y, health.BorderAppearance);
          });
      }

      for (int i = 0; i < stamina.Radius; i++)
      {
        Algorithms.Circle(stamina.Center.X, stamina.Center.Y,
          i, (x, y) =>
          {
            if (this.IsValidCell(x, y))
              SetCell(x, y, stamina.BorderAppearance);
          });
      }
    }
  }
}
