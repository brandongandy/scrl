using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SCRL.MapObjects;
using SCRL.Entities;
using SCRL.Interfaces;

namespace SCRL.Mapping
{
  class Map
  {
    private FieldOfView fieldOfView;
    public int Width { get; private set; }
    public int Height { get; private set; }
    public ObservableCollection<Actor> Entities;
    public Tile[] Tiles;
    public List<RoomBase> Rooms;
    public List<Door> Doors;

    public Map(int width, int height)
    {
      Width = width;
      Height = height;

      Tiles = new Tile[width * height];
      FillMap();

      // y * width + x = index of x,y combo

      Entities = new ObservableCollection<Actor>();
      fieldOfView = new FieldOfView(this);
    }

    #region Tile Property Methods

    public void SetTileColors()
    {
      foreach (var tile in GetAllTiles())
      {
        if (!tile.IsMapped)
        {
          tile.SetUnexploredVariant();
        }

        if (IsInFov(tile.X, tile.Y))
        {
          tile.SetLightVariant();
        }
        else if (tile.IsMapped)
        {
          tile.SetDarkVariant();
        }
      }
    }

    public bool IsTileWalkable(int x, int y)
    {
      if (x < 0 || y < 0 || x >= Width || y >= Height)
        return false;

      return !Tiles[y * Width + x].IsBlockingMove;
    }

    public bool IsInFov(int x, int y)
    {
      return fieldOfView.IsInFov(x, y);
    }

    public ITile GetTile(int x, int y)
    {
      return Tiles[y * Width + x];
    }

    public ITile GetTile(Point position)
    {
      return Tiles[position.Y * Width + position.X];
    }

    // From RogueSharp
    public IEnumerable<ITile> GetTilesAlongLine(int xOrigin, int yOrigin, int xDestination, int yDestination)
    {
      xOrigin = ClampX(xOrigin);
      yOrigin = ClampY(yOrigin);
      xDestination = ClampX(xDestination);
      yDestination = ClampY(yDestination);

      int xDistance = Math.Abs(xDestination - xOrigin);
      int yDistance = Math.Abs(yDestination - yOrigin);

      int sx = xOrigin < xDestination ? 1 : -1;
      int sy = yOrigin < yDestination ? 1 : -1;
      int err = xDistance - yDistance;

      while (true)
      {
        yield return GetTile(xOrigin, yOrigin);
        if (xOrigin == xDestination && yOrigin == yDestination)
        {
          break;
        }
        int e2 = 2 * err;
        if (e2 > -yDistance)
        {
          err = err - yDistance;
          xOrigin = xOrigin + sx;
        }
        if (e2 < xDistance)
        {
          err = err + xDistance;
          yOrigin = yOrigin + sy;
        }
      }
    }

    public int ClampX(int x)
    {
      return (x < 0) ? 0 : (x > Width - 1) ? Width - 1 : x;
    }

    public int ClampY(int y)
    {
      return (y < 0) ? 0 : (y > Height - 1) ? Height - 1 : y;
    }

    public IEnumerable<ITile> GetTilesInSquare(int xCenter, int yCenter, int distance)
    {
      int xMin = Math.Max(0, xCenter - distance);
      int xMax = Math.Min(Width - 1, xCenter + distance);
      int yMin = Math.Max(0, yCenter - distance);
      int yMax = Math.Min(Height - 1, yCenter + distance);

      for (int y = yMin; y <= yMax; y++)
      {
        for (int x = xMin; x <= xMax; x++)
        {
          yield return GetTile(x, y);
        }
      }
    }
    public IEnumerable<ITile> GetTilesInDiamond(int xCenter, int yCenter, int distance)
    {
      var discovered = new HashSet<int>();

      int xMin = Math.Max(0, xCenter - distance);
      int xMax = Math.Min(Width - 1, xCenter + distance);
      int yMin = Math.Max(0, yCenter - distance);
      int yMax = Math.Min(Height - 1, yCenter + distance);

      for (int i = 0; i <= distance; i++)
      {
        for (int j = distance; j >= 0 + i; j--)
        {
          ITile cell;
          if (AddToHashSet(discovered, Math.Max(xMin, xCenter - i), Math.Min(yMax, yCenter + distance - j), out cell))
          {
            yield return cell;
          }
          if (AddToHashSet(discovered, Math.Max(xMin, xCenter - i), Math.Max(yMin, yCenter - distance + j), out cell))
          {
            yield return cell;
          }
          if (AddToHashSet(discovered, Math.Min(xMax, xCenter + i), Math.Min(yMax, yCenter + distance - j), out cell))
          {
            yield return cell;
          }
          if (AddToHashSet(discovered, Math.Min(xMax, xCenter + i), Math.Max(yMin, yCenter - distance + j), out cell))
          {
            yield return cell;
          }
        }
      }
    }

    public IEnumerable<ITile> GetBorderTilesInSquare(int xCenter, int yCenter, int distance)
    {
      int xMin = Math.Max(0, xCenter - distance);
      int xMax = Math.Min(Width - 1, xCenter + distance);
      int yMin = Math.Max(0, yCenter - distance);
      int yMax = Math.Min(Height - 1, yCenter + distance);

      for (int x = xMin; x <= xMax; x++)
      {
        yield return GetTile(x, yMin);
        yield return GetTile(x, yMax);
      }

      for (int y = yMin; y <= yMax; y++)
      {
        yield return GetTile(xMin, y);
        yield return GetTile(xMax, y);
      }
    }
    public IEnumerable<ITile> GetBorderTilesInDiamond(int xCenter, int yCenter, int distance)
    {
      var discovered = new HashSet<int>();

      int xMin = Math.Max(0, xCenter - distance);
      int xMax = Math.Min(Width - 1, xCenter + distance);
      int yMin = Math.Max(0, yCenter - distance);
      int yMax = Math.Min(Height - 1, yCenter + distance);

      ITile cell;
      if (AddToHashSet(discovered, xCenter, yMin, out cell))
      {
        yield return cell;
      }
      if (AddToHashSet(discovered, xCenter, yMax, out cell))
      {
        yield return cell;
      }
      for (int i = 1; i <= distance; i++)
      {
        if (AddToHashSet(discovered, Math.Max(xMin, xCenter - i), Math.Min(yMax, yCenter + distance - i), out cell))
        {
          yield return cell;
        }
        if (AddToHashSet(discovered, Math.Max(xMin, xCenter - i), Math.Max(yMin, yCenter - distance + i), out cell))
        {
          yield return cell;
        }
        if (AddToHashSet(discovered, Math.Min(xMax, xCenter + i), Math.Min(yMax, yCenter + distance - i), out cell))
        {
          yield return cell;
        }
        if (AddToHashSet(discovered, Math.Min(xMax, xCenter + i), Math.Max(yMin, yCenter - distance + i), out cell))
        {
          yield return cell;
        }
      }
    }

    public int IndexFor(int x, int y)
    {
      return y * Width + x;
    }

    public int IndexFor(ITile tile)
    {
      if (tile == null)
      {
        throw new ArgumentNullException("Tile cannot be null!");
      }

      return tile.Y * Width + tile.X;
    }

    /// <summary>
    /// Get the Tile at the specified single-dimensional array index.
    /// </summary>
    /// <param name="index">The single-dimensional array index for the Tile to get.</param>
    /// <returns>The Tile at the specific single-dimensional array index.</returns>
    public ITile TileFor(int index)
    {
      int x = index % Width;
      int y = index / Width;

      return GetTile(x, y);
    }

    public IEnumerable<ITile> GetAllTiles()
    {
      for (int y = 0; y < Height; y++)
      {
        for (int x = 0; x < Width; x++)
        {
          yield return GetTile(x, y);
        }
      }
    }

    #endregion

    #region FOV

    public ReadOnlyCollection<ITile> ComputeFov(int xOrigin, int yOrigin, int radius, bool lightWalls)
    {
      return fieldOfView.ComputeFov(xOrigin, yOrigin, radius, lightWalls);
    }

    public ReadOnlyCollection<ITile> AppendFov(int xOrigin, int yOrigin, int radius, bool lightWalls)
    {
      return fieldOfView.AppendFov(xOrigin, yOrigin, radius, lightWalls);
    }

    #endregion

    #region Map Creation Methods

    private void FillMap()
    {
      for (int x = 0; x < Width; x++)
      {
        for (int y = 0; y < Height; y++)
        {
          Tiles[y * Width + x] = new Wall(x, y);
        }
      }
    }

    public void CreateRoom(RoomBase created)
    {
      for (int x = created.Room.X + 1; x < created.Room.Right; x++)
      {
        for (int y = created.Room.Y + 1; y < created.Room.Bottom; y++)
        {
            Tiles[y * Width + x] = new Floor(x, y);
        }
      }
    }

    public void CreateHorizontalTunnel(int x1, int x2, int y)
    {
      for (int x = Math.Min(x1, x2); x <= Math.Max(x1, x2); x++)
      {
        Tiles[y * Width + x] = new Floor(x, y);
      }
    }

    public void CreateVerticalTunnel(int y1, int y2, int x)
    {
      for (int y = Math.Min(y1, y2); y <= Math.Max(y1, y2); y++)
      {
        Tiles[y * Width + x] = new Floor(x, y);
      }
    }

    public Point PickStartingPoint()
    {
      return new Point(Resources.Rand.SeededNext(1, Width),
        Resources.Rand.SeededNext(1, Height));
    }

    #endregion

    #region Doors
    
    public Door GetDoor(int x, int y)
    {
      return Doors.SingleOrDefault(d => d.X == x && d.Y == y);
    }

    public void CreateDoors(RoomBase room)
    {
      int xMin = room.Room.Left;
      int xMax = room.Room.Right;
      int yMin = room.Room.Top;
      int yMax = room.Room.Bottom;

      List<ITile> borderTiles = GetTilesAlongLine(xMin, yMin, xMax, yMin).ToList();
      borderTiles.AddRange(GetTilesAlongLine(xMin, yMin, xMin, yMax));
      borderTiles.AddRange(GetTilesAlongLine(xMin, yMax, xMax, yMax));
      borderTiles.AddRange(GetTilesAlongLine(xMax, yMin, xMax, yMax));

      foreach (var tile in borderTiles)
      {
        if (IsPotentialDoor(tile))
        {
          Tiles[tile.Y * Width + tile.X] = new Door(tile.X, tile.Y);
          Doors.Add(new Door(tile.X, tile.Y));
        }
      }
    }

    private bool IsPotentialDoor(ITile tile)
    {
      if (tile.IsBlockingMove)
      {
        return false;
      }

      var right = GetTile(tile.X + 1, tile.Y);
      var left = GetTile(tile.X - 1, tile.Y);
      var top = GetTile(tile.X, tile.Y - 1);
      var bottom = GetTile(tile.X, tile.Y + 1);

      if (GetDoor(tile.X, tile.Y) != null ||
          GetDoor(right.X, right.Y) != null ||
          GetDoor(left.X, left.Y) != null ||
          GetDoor(top.X, top.Y) != null ||
          GetDoor(bottom.X, bottom.Y) != null)
      {
        return false;
      }

      if (!right.IsBlockingMove &&
          !left.IsBlockingMove &&
          top.IsBlockingMove &&
          bottom.IsBlockingMove)
      {
        return true;
      }

      if (right.IsBlockingMove &&
          left.IsBlockingMove &&
          !top.IsBlockingMove &&
          !bottom.IsBlockingMove)
      {
        return true;
      }

      return false;
    }

    #endregion

    #region Map Object Placement Methods

    public Point GetRandomWalkableLocationInRoom(RoomBase room)
    {
      int x = SCORLIB.Systems.Random.Next(1 + room.Room.Left, room.Room.Right);
      int y = SCORLIB.Systems.Random.Next(1 + room.Room.Top, room.Room.Bottom);
      
      while (GetTile(x, y).IsBlockingMove)
      {
        x = SCORLIB.Systems.Random.Next(1 + room.Room.Left, room.Room.Right); 
        y = SCORLIB.Systems.Random.Next(1 + room.Room.Top, room.Room.Bottom);
      }

      return new Point(x, y);
    }

    public void SetIsWalkable(Point position, bool isWalkable)
    {
      GetTile(position.X, position.Y).IsBlockingMove = !isWalkable;
    }

    private bool AddToHashSet(HashSet<int> hashSet, int x, int y, out ITile cell)
    {
      cell = GetTile(x, y);
      return hashSet.Add(IndexFor(cell));
    }

    private bool AddToHashSet(HashSet<int> hashSet, ITile cell)
    {
      return hashSet.Add(IndexFor(cell));
    }

    #endregion
  }
}
