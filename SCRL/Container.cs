using System;
using System.Collections.Generic;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using SadConsole;
using SCRL.Entities;
using SCRL.Screens;
using SCRL.Systems;
using SadConsole.Input;

namespace SCRL
{
  class Container : ConsoleContainer
  {
    public static Adventure Adventure;
    public static Point AdventureSize = new Point(100, 45);

    public static BeltConsole Belt;
    public static Point BeltSize = new Point(59, 15);

    public static MessageLogConsole Log;
    public static Point LogSize = new Point(41, 15);

    //public static InventoryConsole Inventory;
    //public static Point InventorySize = new Point(40, 45);

    public static CommandSystem CommandSystem { get; private set; }
    public static SchedulingSystem SchedulingSystem { get; private set; }

    public Container()
    {
      CommandSystem = new CommandSystem();
      SchedulingSystem = new SchedulingSystem();

      //Inventory = new InventoryConsole(InventorySize.X, InventorySize.Y);
      Belt = new BeltConsole(BeltSize.X, BeltSize.Y);
      Log = new MessageLogConsole(LogSize.X, LogSize.Y);
      Adventure = new Adventure(AdventureSize.X, AdventureSize.Y);
      
      MoveNextConsole();
    }

    public void MoveNextConsole()
    {
      Children.Clear();
      Children.Add(Belt);
      Children.Add(Log);
      //Children.Add(debug);
      //Children.Add(Inventory);
      Children.Add(Adventure);


      Belt.Position = new Point(0, Program.Height - Belt.Height);
      Log.Position = new Point(Belt.Width, Program.Height - Log.Height);
      //Inventory.Position = new Point(Program.Width - Inventory.Width, 0);

      Global.FocusedConsoles.Set(Adventure);
    }
  }
}
