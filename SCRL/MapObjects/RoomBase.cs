using Microsoft.Xna.Framework;

namespace SCRL.MapObjects
{
  class RoomBase
  {
    public Rectangle Room { get; set; }

    public RoomBase(int x, int y, int width, int height)
    {
      Room = new Rectangle(new Point(x, y), new Point(width, height));
    }

    public RoomBase(Rectangle newRoom)
    {
      Room = newRoom;
    }

    /// <summary>
    /// Checks to see if this room intersects with another room using AABB.
    /// </summary>
    /// <param name="otherRoom">The other room to check</param>
    /// <returns>True if the rooms intersect</returns>
    public bool Intersects(RoomBase other)
    {
      return (Room.X <= other.Room.Right && Room.Right >= other.Room.X &&
              Room.Y <= other.Room.Bottom && Room.Bottom >= other.Room.Y);
    }
  }
}
