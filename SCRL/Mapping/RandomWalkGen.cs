using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SCRL.MapObjects;
using SCRL.Resources;

namespace SCRL.Mapping
{
  class RandomWalkGen : Map
  {
    private int maxCellsToTurn;

    public RandomWalkGen(int width, int height, int max = 4000) : base(width, height)
    {
      Rooms = new List<RoomBase>();
      maxCellsToTurn = max;

      CarveCaves();
      CreateNewRoom();
    }

    private void CarveCaves()
    {
      Point position = PickStartingPoint();
      Tiles[position.Y * Width + position.X] = new Floor(position.X, position.Y);
      int turnedCells = 1;

      while (turnedCells < maxCellsToTurn)
      {
        var oldPosition = position;
        position = GetNextPoint(position);
        while (position.X < 1 || position.Y < 1 || position.X >= Width - 1 || position.Y >= Height - 1)
        {
          position = PickStartingPoint();
        }

        if (Tiles[position.Y * Width + position.X].GetType() == typeof(Wall))
        {
          Tiles[position.Y * Width + position.X] = new Floor(position.X, position.Y);
          turnedCells++;
        }
      }
    }

    private Point GetNextPoint(Point curPos)
    {
      int dir = Rand.SeededNext(0, 3);
      Point change = new Point();
      switch (dir)
      {
        case 0:
          change = new Point(-1, 0);
          break;
        case 1:
          change = new Point(1, 0);
          break;
        case 2:
          change = new Point(0, -1);
          break;
        case 3:
          change = new Point(0, 1);
          break;
      }
      return curPos + change;
    }

    private void CreateNewRoom()
    {
      Point roomPos = PickStartingPoint();

      while (Tiles[roomPos.Y * Width + roomPos.X].IsBlockingMove)
      {
        roomPos = PickStartingPoint();
      }

      RoomBase room = new RoomBase(new Rectangle(roomPos, new Point(1, 1)));
      Rooms.Add(room);
    }
  }
}
