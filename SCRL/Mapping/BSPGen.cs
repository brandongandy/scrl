using Microsoft.Xna.Framework;
using SCORLIB.Systems;
using SCRL.MapObjects;
using System.Collections.Generic;

namespace SCRL.Mapping
{
  class Leaf
  {
    public Rectangle LeafRect { get; set; }
    public Leaf LeftChild { get; set; }
    public Leaf RightChild { get; set; }
    public Rectangle? Room { get; set; } = null;

    public int minLeafSize { get; private set; }

    public Leaf(Rectangle leaf, int minSize = 10)
    {
      LeafRect = leaf;
      minLeafSize = minSize;
    }
  }

  class BSPGen : Map
  {
    private readonly int MAX_LEAF_SIZE = 20;
    List<Leaf> Leafs = new List<Leaf>();

    public BSPGen(int width, int height) : base(width, height)
    {
      Rooms = new List<RoomBase>();
      Doors = new List<Door>();
      GenerateLeafs();
      CarveRoomsFromLeafs();
    }

    /// <summary>
    /// Recursively generates the Leafs used by the BSP generator
    /// </summary>
    private void GenerateLeafs()
    {
      Leaf root = new Leaf(new Rectangle(0, 0, Width, Height), 10);
      Leafs.Add(root);

      bool didSplit = true;
      while (didSplit)
      {
        didSplit = false;
        foreach (var l in Leafs.ToArray())
        {
          // if we're not already split...
          if (l.LeftChild == null && l.RightChild == null)
          {
            // if the leaf is too big, or 75% chance...
            if (l.LeafRect.Width > MAX_LEAF_SIZE || l.LeafRect.Height > MAX_LEAF_SIZE ||
                Program.Random.NextDouble() > 0.25)
            {
              if (Split(l))
              {
                // split! if we did, do the thing
                Leafs.Add(l.LeftChild);
                Leafs.Add(l.RightChild);
                didSplit = true;
              }
            }
          }
        }
      }
      // Iterate through each Leaf and create rooms in each one.
      CreateRooms(root);
    }


    /// <summary>
    /// Goes through each leaf and, if it has a room, constructs a room inside that leaf.
    /// Then adds the Room to the list for map utils.
    /// </summary>
    private void CarveRoomsFromLeafs()
    {
      foreach (var leaf in Leafs)
      {
        if (leaf.Room != null)
        {
          RoomBase newRoom = new RoomBase(leaf.Room.Value);
          Rooms.Add(newRoom);
          CreateRoom(newRoom);
          CreateDoors(newRoom);
        }
      }
    }

    #region BSP Region

    /// <summary>
    /// Split the leaf into two children.
    /// </summary>
    /// <returns>True if Leaf split, false if not.</returns>
    public bool Split(Leaf leaf)
    {
      if (leaf.LeftChild != null || leaf.RightChild != null)
        // we have already split
        return false;


      // determine split direction
      bool splitHorizontally = Program.Random.NextDouble() > 0.5;
      if (leaf.LeafRect.Width > leaf.LeafRect.Height && leaf.LeafRect.Width / leaf.LeafRect.Height >= 1.25)
      {
        splitHorizontally = false;
      }
      else if (leaf.LeafRect.Height > leaf.LeafRect.Width && leaf.LeafRect.Height / leaf.LeafRect.Width >= 1.25)
      {
        splitHorizontally = true;
      }

      int maxSize = (splitHorizontally ? leaf.LeafRect.Height : leaf.LeafRect.Width) - leaf.minLeafSize;
      if (maxSize <= leaf.minLeafSize)
      {
        // the area is too small to split anymore
        return false;
      }

      // determine where the split will take place
      int split = Random.Next(leaf.minLeafSize, maxSize);

      if (splitHorizontally)
      {
        leaf.LeftChild = new Leaf(new Rectangle(leaf.LeafRect.X, leaf.LeafRect.Y, leaf.LeafRect.Width, split), leaf.minLeafSize);
        leaf.RightChild = new Leaf(new Rectangle(leaf.LeafRect.X, leaf.LeafRect.Y + split, leaf.LeafRect.Width, leaf.LeafRect.Height - split), leaf.minLeafSize);
      }
      else
      {
        leaf.LeftChild = new Leaf(new Rectangle(leaf.LeafRect.X, leaf.LeafRect.Y, split, leaf.LeafRect.Height), leaf.minLeafSize);
        leaf.RightChild = new Leaf(new Rectangle(leaf.LeafRect.X + split, leaf.LeafRect.Y, leaf.LeafRect.Width - split, leaf.LeafRect.Height), leaf.minLeafSize);
      }

      return true;
    }

    /// <summary>
    /// Goes through a Leaf and all of its children recursively
    /// in order to add Rooms and then connect each room to its nearest
    /// neighbor with a Hallway.
    /// </summary>
    /// <param name="leaf">The highest-level Leaf to start creating rooms in.</param>
    public void CreateRooms(Leaf leaf)
    {
      if (leaf.LeftChild != null || leaf.RightChild != null)
      {
        // this leaf's been split, so go into its children 
        if (leaf.LeftChild != null)
        {
          CreateRooms(leaf.LeftChild);
        }

        if (leaf.RightChild != null)
        {
          CreateRooms(leaf.RightChild);
        }

        if (leaf.LeftChild != null && leaf.RightChild != null)
        {
          ConnectRooms(GetRoom(leaf.LeftChild), GetRoom(leaf.RightChild));
        }
      }
      else
      {
        int minRoomSize = 3;
        Point roomSize = new Point(Random.Next(minRoomSize, leaf.LeafRect.Width - 2),
          Random.Next(minRoomSize, leaf.LeafRect.Height - 2));
        Point roomPos = new Point(Random.Next(1, leaf.LeafRect.Width - roomSize.X - 1),
          Random.Next(1, leaf.LeafRect.Height - roomSize.Y - 1));

        leaf.Room = new Rectangle(leaf.LeafRect.X + roomPos.X, leaf.LeafRect.Y + roomPos.Y , roomSize.X, roomSize.Y);
      }
    }

    /// <summary>
    /// Returns the Room of a Leaf, if it has one, or a Room
    /// from one of the Leaf's children if it doesn't.
    /// </summary>
    /// <param name="leaf">The Leaf that should have a Room.</param>
    /// <returns>The Room within a Leaf, or within the Leaf's first child.</returns>
    public Rectangle? GetRoom(Leaf leaf)
    {
      if (leaf.Room != null)
      {
        return leaf.Room;
      }
      else
      {
        Rectangle? leftRoom = null;
        Rectangle? rightRoom = null;
        if (leaf.LeftChild != null)
        {
          leftRoom = GetRoom(leaf.LeftChild);
        }
        if (leaf.RightChild != null)
        {
          rightRoom = GetRoom(leaf.RightChild);
        }

        if (leftRoom == null && rightRoom == null)
        {
          return null;
        }
        else if (rightRoom == null)
        {
          return leftRoom;
        }
        else if (leftRoom == null)
        {
          return rightRoom;
        }
        else if (Program.Random.NextDouble() > 0.5)
        {
          return leftRoom;
        }
        else
        {
          return rightRoom;
        }
      }
    }

    /// <summary>
    /// Connects Rooms within Leafs with hallways.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public void ConnectRooms(Rectangle? left, Rectangle? right)
    {
      if (left == null || right == null)
      {
        // this should never be possible, but... just in case
        return;
      }

      // using "Value.prop" is too messy and verbose
      // find a better way to allow for nullable params
      Point point1 = new Point(Random.Next(left.Value.Left + 1, left.Value.Right - 2),
        Random.Next(left.Value.Top + 1, left.Value.Bottom - 2));

      Point point2 = new Point(Random.Next(right.Value.Left + 1, right.Value.Right - 2),
        Random.Next(right.Value.Top + 1, right.Value.Bottom - 2));

      if (Random.NextDouble() > 0.5)
      {
        CreateHorizontalTunnel(point1.X, point2.X, point1.Y);
        CreateVerticalTunnel(point2.Y, point1.Y, point2.X);
      }
      else
      {
        CreateVerticalTunnel(point2.Y, point1.Y, point2.X);
        CreateHorizontalTunnel(point1.X, point2.X, point1.Y);
      }
    }

    #endregion

  }
}
