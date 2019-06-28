using Microsoft.Xna.Framework;

namespace SCRL.MapObjects
{
  public enum Direction
  {
    North,
    East,
    South,
    West,
    Northeast,
    Northwest,
    Southeast,
    Southwest
  }

  public static class DirectionUtils
  {
    public static Point DirectionToVector(Direction dir)
    {
      Point newPosition = new Point(0, 0);

      switch (dir)
      {
        case Direction.North:
          newPosition = new Point(0, -1);
          break;
        case Direction.East:
          newPosition = new Point(1, 0);
          break;
        case Direction.South:
          newPosition = new Point(0, 1);
          break;
        case Direction.West:
          newPosition = new Point(-1, 0);
          break;
        case Direction.Northeast:
          newPosition = new Point(1, -1);
          break;
        case Direction.Northwest:
          newPosition = new Point(-1, -1);
          break;
        case Direction.Southeast:
          newPosition = new Point(1, 1);
          break;
        case Direction.Southwest:
          newPosition = new Point(-1, 1);
          break;
      }

      return newPosition;
    }
  }
}
