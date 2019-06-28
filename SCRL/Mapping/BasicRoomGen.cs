using System.Collections.Generic;
using SCORLIB.Systems;
using SCRL.MapObjects;

namespace SCRL.Mapping
{
  class BasicRoomGen : Map
  {
    private readonly int ROOM_MAX_SIZE = 10;
    private readonly int ROOM_MIN_SIZE = 6;
    private readonly int MAX_ROOMS = 30;

    public BasicRoomGen(int width, int height) : base(width, height)
    {
      MakeMap();
    }

    public void MakeMap()
    {
      Rooms = new List<RoomBase>();

      CarveRooms();
      CarveTunnels();
    }

    private void CarveRooms()
    {
      for (int i = 0; i < MAX_ROOMS; i++)
      {
        var width = Random.Next(ROOM_MIN_SIZE, ROOM_MAX_SIZE);
        var height = Random.Next(ROOM_MIN_SIZE, ROOM_MAX_SIZE);

        var x = Random.Next(0, Width - width);
        var y = Random.Next(0, Height - height);

        var newRoom = new RoomBase(x, y, width, height);
        bool failed = false;
        foreach (var otherRoom in Rooms)
        {
          if (newRoom.Intersects(otherRoom))
          {
            failed = true;
            continue;
          }
        }

        if (!failed)
        {
          // no intersections; the room is valid
          CreateRoom(newRoom);
          Rooms.Add(newRoom);
        }
      }
    }

    private void CarveTunnels()
    {
      for (int i = 1; i < Rooms.Count; i++)
      {
        int prevCenterX = Rooms[i - 1].Room.Center.X;
        int prevCenterY = Rooms[i - 1].Room.Center.Y;
        int curCenterX = Rooms[i].Room.Center.X;
        int curCenterY = Rooms[i].Room.Center.Y;


        if (Random.Next(0, 1) == 1)
        {
          CreateHorizontalTunnel(prevCenterX, curCenterX, prevCenterY);
          CreateVerticalTunnel(prevCenterY, curCenterY, curCenterX);
        }
        else
        {
          CreateVerticalTunnel(prevCenterY, curCenterY, prevCenterX);
          CreateHorizontalTunnel(prevCenterX, curCenterX, curCenterY);
        }
      }
    }
  }
}
