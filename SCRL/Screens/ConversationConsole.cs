using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadConsole;
using SadConsole.Shapes;
using SadConsole.Themes;
using SCRL.Entities;

namespace SCRL.Screens
{
  class ConversationConsole : Window
  {
    private Actor Sender;
    public ConversationConsole(Actor sender, int width, int height) : base(width, height)
    {
      Sender = sender ?? throw new ArgumentNullException("sender", "Conversation sender cannot be null!");
      SetWindowProperties();
      IsVisible = false;
      IsFocused = true;
    }

    private void SetWindowProperties()
    {
      Theme.BorderStyle = new Cell(Resources.Palette.Rope, Resources.Palette.Loulou);
      Theme.FillStyle = new Cell(Resources.Palette.LightSteelBlue, Resources.Palette.Loulou);
      Theme.TitleStyle = new Cell(Resources.Palette.GoldenFizz, Resources.Palette.Loulou);
      Title = Sender.Name;

      var okButton = new SadConsole.Controls.Button(5);
      okButton.Text = "Ok!";
      okButton.Position = new Microsoft.Xna.Framework.Point(Width / 2 - okButton.Width / 2, Height - okButton.Height - 1);
      okButton.Click += OkButton_Click;
      Add(okButton);
    }

    private void OkButton_Click(object sender, EventArgs e)
    {
      DialogResult = true;
      Hide();
    }

    public void ShowMessage(string message)
    {
      Print(2, 2, message);
      Show(true);
    }
  }
}
