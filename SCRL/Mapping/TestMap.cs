using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCRL.MapObjects;

namespace SCRL.Mapping
{
  class TestMap : Map
  {

    public TestMap(int width, int height) : base(width, height)
    {
      Rooms = new List<RoomBase>();
      CarveRoom();
    }

    public void CarveRoom()
    {
      var room = new RoomBase(1, 1, Width - 3, Height - 3);

      CreateRoom(room);
      Rooms.Add(room);
    }
  }
}
