using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Console = SadConsole.Console;
using SadConsole;
using SCRL.Entities;
using SCRL.Interfaces;
using SCRL.Mapping;
using SCRL.MapObjects;
using SCRL.Systems;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using SadConsole.Input;

namespace SCRL.Screens
{
  /// <summary>
  /// Represents the whole screen while adventuring in a town or dungeon.
  /// Coordinates between child screens -- bungeon to town to other dungeon, etc.
  /// </summary>
  class Adventure : Console
  {
    #region Fields
    
    //private ObservableCollection<Monster> Monsters = new ObservableCollection<Monster>();

    #endregion

    #region Properties

    public ConsoleMetadata Metadata
    {
      get { return DungeonScreen.Metadata; }
    }

    public Player Player;
    public Dungeon DungeonScreen;
    public Map Map { get; private set; }

    public Point MapViewPoint
    {
      get { return DungeonScreen.MapViewPoint; }
      set {
        DungeonScreen.MapViewPoint = value;
        SyncEntityOffset();
      }
    }

    #endregion

    public Adventure(int width, int height) : base(width, height)
    {
      DungeonScreen = new Dungeon(0, 0, 20, 20);
      Children.Add(DungeonScreen);

      var newMap = new RandomWalkGen(200, 200);
      
      LoadMap(newMap);
    }

    #region Init

    public void LoadMap(Map map)
    {
      if (Map != null)
      {
        Map.Entities.Clear();
        Container.SchedulingSystem.Clear();
        Map.Entities.CollectionChanged -= Entities_CollectionChanged;
      }

      DungeonScreen.LoadMap(map);

      map.Entities.CollectionChanged += Entities_CollectionChanged;

      Map = map;
      AddMonsters();

      AddPlayer();

      Map.Entities.Add(Player);
      CenterScreenOnPlayer();
      UpdatePlayerFOV();
      MessageLog.Add($"{Player.ColoredName} has arrived at: {Player.Position.X}, {Player.Position.Y}.");
    }

    public bool SetActorPosition(Actor actor, Point pos)
    {
      if (!Map.GetTile(pos.X, pos.Y).IsBlockingMove)
      {
        Map.GetTile(actor.Position).IsBlockingMove = false;
        actor.Position = pos;
        Map.GetTile(actor.Position).IsBlockingMove = true;

        if (actor is Player)
        {
          CenterScreenOnPlayer();
          UpdatePlayerFOV();
        }
        return true;
      }

      return false;
    }

    #endregion

    #region Player

    public void AddPlayer()
    {
      Point position = Map.Rooms.First().Room.Center;
      Player = new Player()
      {
        Position = position
      };

      Container.SchedulingSystem.Add(Player);
    }

    /// <summary>
    /// Iterates through tiles within the Player's Awareness and determines whether
    /// they're also within sight.
    /// </summary>
    private void UpdatePlayerFOV()
    {
      Map.ComputeFov(Player.Position.X, Player.Position.Y, Player.Wisdom, true);

      foreach (var tile in Map.GetAllTiles())
      {
        if (Map.IsInFov(tile.X, tile.Y))
        {
          tile.IsMapped = true;
        }
      }

      foreach (Monster mob in Map.Entities.Where(m => m is Monster))
      {
        if (Map.IsInFov(mob.Position.X, mob.Position.Y))
        {
          mob.IsVisible = true;
        }
        else
        {
          mob.IsVisible = false;
        }
      }

      foreach (var npc in Map.Entities.Where(n => n is NonPlayerCharacter))
      {
        if (Map.IsInFov(npc.Position.X, npc.Position.Y))
        {
          npc.IsVisible = true;
        } else
        {
          npc.IsVisible = false;
        }
      }

      Map.SetTileColors();
    }

    #endregion

    #region NPCs

    private void AddNpcs()
    {
      var npc = new NonPlayerCharacter(Resources.Palette.Atlantis, Resources.Palette.LightFloorForeground, 3);
      npc.Position = new Point(49, 20);
      Map.Entities.Add(npc);
      Map.GetTile(npc.Position).IsBlockingMove = true;
    }

    public NonPlayerCharacter GetNpcAt(Point pos)
    {
      return (NonPlayerCharacter)Map.Entities.Where(n => n is NonPlayerCharacter).FirstOrDefault(n => n.Position == pos);
    }

    #endregion

    #region Monsters

    private void AddMonsters()
    {
      foreach (var room in Map.Rooms)
      {
        if (SCORLIB.Systems.Random.NextDouble() > 0.5)
        {
          var kobold = new SmallOrc()
          {
            Position = Map.GetRandomWalkableLocationInRoom(room)
            //Position = new Point(20, 10)
          };
          Map.Entities.Add(kobold);
          Map.GetTile(kobold.Position.X, kobold.Position.Y).IsBlockingMove = true;
          //MessageLog.Add($"{kobold.Name} added at: {kobold.Position.X}, {kobold.Position.Y}");
          Container.SchedulingSystem.Add(kobold);
        }
      }
    }

    public void RemoveMonster(Monster monster)
    {
      Map.Entities.Remove(monster);
      Map.SetIsWalkable(monster.Position, true);
      Container.SchedulingSystem.Remove(monster);
    }

    public Monster GetMonsterAt(Point pos)
    {
      // we're making sure the entity is a monster in linq but just in case, specify cast
      return (Monster)Map.Entities.Where(m => m is Monster).FirstOrDefault(m => m.Position == pos);
    }

    #endregion

    #region Entity Management

    private void Entities_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      switch (e.Action)
      {
        case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
          foreach (var item in e.NewItems)
            DungeonScreen.Children.Add((Actor)item);
          break;
        case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
          foreach (var item in e.OldItems)
            DungeonScreen.Children.Remove((Actor)item);
          break;
        case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
          foreach (var item in e.NewItems)
            DungeonScreen.Children.Add((Actor)item);
          foreach (var item in e.OldItems)
            DungeonScreen.Children.Remove((Actor)item);
          break;
        case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
          DungeonScreen.Children.Clear();
          break;
        default:
          break;
      }

      SyncEntityOffset();
    }

    private void SyncEntityOffset()
    {
      foreach (var item in Map.Entities)
      {
        // Make sure that the entity draws based on the current map scrolling values
        item.PositionOffset = new Point(-DungeonScreen.MapViewPoint.X, -DungeonScreen.MapViewPoint.Y);
      }
    }

    #endregion

    #region Monogame

    public override void Update(TimeSpan timeElapsed)
    {
      base.Update(timeElapsed);

      if (Container.CommandSystem.IsPlayerTurn)
      {
        if (CheckPlayerAction())
        {
          Container.CommandSystem.EndPlayerTurn();
        }
      }
      else
      {
        Container.CommandSystem.ActivateMonsters();
      }

      CheckMapInput();
      CheckMapSwitch();
      Mouse();
    }

    #endregion

    #region Camera

    private void CenterScreenOnPlayer()
    {
      var newViewX = Player.Position.X - Width / 2;
      var newViewY = Player.Position.Y - Height / 2;
      MapViewPoint = new Point(newViewX, newViewY);
    }

    private void CheckMapInput()
    {
      if (Global.KeyboardState.IsKeyDown(Keys.W))
      {
        MapViewPoint = new Point(MapViewPoint.X, MapViewPoint.Y - 1);
      }
      if (Global.KeyboardState.IsKeyDown(Keys.D))
      {
        MapViewPoint = new Point(MapViewPoint.X + 1, MapViewPoint.Y);
      }
      if (Global.KeyboardState.IsKeyDown(Keys.S))
      {
        MapViewPoint = new Point(MapViewPoint.X, MapViewPoint.Y + 1);
      }
      if (Global.KeyboardState.IsKeyDown(Keys.A))
      {
        MapViewPoint = new Point(MapViewPoint.X - 1, MapViewPoint.Y);
      }
    }

    #endregion

    #region Input Methods

    private bool CheckPlayerAction()
    {
      bool didPlayerAct = false;
      if (Global.KeyboardState.IsKeyPressed(Keys.Left) || Global.KeyboardState.IsKeyPressed(Keys.NumPad4))
      {
        didPlayerAct = Container.CommandSystem.MovePlayer(Direction.West);
      }
      else if (Global.KeyboardState.IsKeyPressed(Keys.Right) || Global.KeyboardState.IsKeyPressed(Keys.NumPad6))
      {
        didPlayerAct = Container.CommandSystem.MovePlayer(Direction.East);
      }

      if (Global.KeyboardState.IsKeyPressed(Keys.Up) || Global.KeyboardState.IsKeyPressed(Keys.NumPad8))
      {
        didPlayerAct = Container.CommandSystem.MovePlayer(Direction.North);
      }
      else if (Global.KeyboardState.IsKeyPressed(Keys.Down) || Global.KeyboardState.IsKeyPressed(Keys.NumPad2))
      {
        didPlayerAct = Container.CommandSystem.MovePlayer(Direction.South);
      }

      if (Global.KeyboardState.IsKeyPressed(Keys.NumPad1))
      {
        didPlayerAct = Container.CommandSystem.MovePlayer(Direction.Southwest);
      }
      else if (Global.KeyboardState.IsKeyPressed(Keys.NumPad9))
      {
        didPlayerAct = Container.CommandSystem.MovePlayer(Direction.Northeast);
      }

      if (Global.KeyboardState.IsKeyPressed(Keys.NumPad3))
      {

        didPlayerAct = Container.CommandSystem.MovePlayer(Direction.Southeast);
      }
      else if (Global.KeyboardState.IsKeyPressed(Keys.NumPad7))
      {
        didPlayerAct = Container.CommandSystem.MovePlayer(Direction.Northwest);
      }

      if (Global.KeyboardState.IsKeyReleased(Keys.OemPeriod))
      {
        didPlayerAct = true;
        MessageLog.Add($"{Player.ColoredName} waits for one turn...");
      }

      return didPlayerAct;
    }

    private void CheckMapSwitch()
    {
      if (Global.KeyboardState.IsKeyReleased(Keys.Z))
      {
        var bspMap = new BSPGen(100, 100);
        LoadMap(bspMap);
      }
      if (Global.KeyboardState.IsKeyReleased(Keys.X))
      {
        var brgMap = new BasicRoomGen(100, 100);
        LoadMap(brgMap);
      }
      if (Global.KeyboardState.IsKeyReleased(Keys.C))
      {
        var rwMap = new RandomWalkGen(100, 100);
        LoadMap(rwMap);
      }
    }

    #endregion

    void Mouse()
    {
      if (DungeonScreen.MousePosition.X <= 0 || DungeonScreen.MousePosition.X > Width - 1)
      {
        return;
      }

      if (DungeonScreen.MousePosition.Y <= 0 || DungeonScreen.MousePosition.Y > Height - 1)
      {
        return;
      }
      
      if (Global.MouseState.LeftClicked)
      {
        MessageLog.Add($"Left click at: {DungeonScreen.MousePosition}");
      }
    }
  }
}
