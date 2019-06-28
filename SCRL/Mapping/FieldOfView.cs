using SCRL.MapObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SCRL.Interfaces;

namespace SCRL.Mapping
{
  class FieldOfView
  {
    private readonly Map map;
    private readonly HashSet<int> inFov;

    public FieldOfView(Map map)
    {
      this.map = map;
      inFov = new HashSet<int>();
    }

    public bool IsInFov(int x, int y)
    {
      return inFov.Contains(map.IndexFor(x, y));
    }

    public ReadOnlyCollection<ITile> ComputeFov(int xOrigin, int yOrigin, int radius, bool lightWalls)
    {
      ClearFov();
      return AppendFov(xOrigin, yOrigin, radius, lightWalls);
    }

    public ReadOnlyCollection<ITile> AppendFov(int xOrigin, int yOrigin, int radius, bool lightWalls)
    {
      foreach (ITile borderTile in map.GetBorderTilesInSquare(xOrigin, yOrigin, radius))
      {
        foreach(ITile tile in map.GetTilesAlongLine(xOrigin, yOrigin, borderTile.X, borderTile.Y))
        {
          if ((Math.Abs(tile.X - xOrigin) + Math.Abs(tile.Y - yOrigin)) > radius)
          {
            break;
          }

          if (!tile.IsBlockingLOS)
          {
            inFov.Add(map.IndexFor(tile));
          }
          else
          {
            if (lightWalls)
            {
              inFov.Add(map.IndexFor(tile));
            }
            break;
          }
        }
      }

      if (lightWalls)
      {
        foreach (ITile tile in map.GetTilesInSquare(xOrigin, yOrigin, radius))
        {
          if (tile.X > xOrigin)
          {
            if (tile.Y > yOrigin)
            {
              PostProcessFovQuadrant(tile.X, tile.Y, Quadrant.SE);
            } else if (tile.Y < yOrigin)
            {
              PostProcessFovQuadrant(tile.X, tile.Y, Quadrant.NE);
            }
          } else if (tile.X < xOrigin)
          {
            if (tile.Y > yOrigin)
            {
              PostProcessFovQuadrant(tile.X, tile.Y, Quadrant.SW);
            } else if (tile.Y < yOrigin)
            {
              PostProcessFovQuadrant(tile.X, tile.Y, Quadrant.NW);
            }
          }
        }
      }

      return TilesInFov();
    }

    private ReadOnlyCollection<ITile> TilesInFov()
    {
      var tiles = new List<ITile>();
      foreach (int index in inFov)
      {
        tiles.Add(map.TileFor(index));
      }
      return new ReadOnlyCollection<ITile>(tiles);
    }

    private void ClearFov()
    {
      inFov.Clear();
    }

    private void PostProcessFovQuadrant(int x, int y, Quadrant quadrant)
    {
      int x1 = x;
      int y1 = y;
      int x2 = x;
      int y2 = y;

      switch (quadrant)
      {
        case Quadrant.NE:
          y1 = y + 1;
          x2 = x - 1;
          break;
        case Quadrant.SE:
          y1 = y - 1;
          x2 = x - 1;
          break;
        case Quadrant.SW:
          y1 = y - 1;
          x2 = x + 1;
          break;
        case Quadrant.NW:
          y1 = y + 1;
          x2 = x + 1;
          break;
      }

      if (!IsInFov(x, y) && map.GetTile(x, y).IsBlockingLOS)
      {
        if ((!map.GetTile(x1, y1).IsBlockingLOS && IsInFov(x1, y1)) ||
            (!map.GetTile(x2, y2).IsBlockingLOS && IsInFov(x2, y2)) ||
            (!map.GetTile(x2, y1).IsBlockingLOS && IsInFov(x2, y1)))
        {
          inFov.Add(map.IndexFor(x, y));
        }
      }
    }

    private enum Quadrant
    {
      NE = 1,
      SE = 2,
      SW = 3,
      NW = 4
    }
  }
}
