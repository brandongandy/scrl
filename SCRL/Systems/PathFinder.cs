﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp.Algorithms;
using SCRL.Interfaces;
using SCRL.Mapping;

namespace SCRL.Systems
{
  class PathFinder
  {
    private readonly EdgeWeightedDigraph _graph;
    private readonly Map _map;

    /// <summary>
    /// Constructs a new PathFinder instance for the specified Map that will not consider diagonal movements to be valid.
    /// </summary>
    /// <param name="map">The Map that this PathFinder instance will run shortest path algorithms on</param>
    /// <exception cref="ArgumentNullException">Thrown when a null map parameter is passed in</exception>
    public PathFinder(Map map)
    {
      if (map == null)
      {
        throw new ArgumentNullException("map", "Map cannot be null");
      }

      _map = map;
      _graph = new EdgeWeightedDigraph(_map.Width * _map.Height);
      foreach (ITile cell in _map.GetAllTiles())
      {
        if (!cell.IsBlockingMove)
        {
          int v = IndexFor(cell);
          foreach (ITile neighbor in _map.GetBorderTilesInDiamond(cell.X, cell.Y, 1))
          {
            if (!cell.IsBlockingMove)
            {
              int w = IndexFor(neighbor);
              _graph.AddEdge(new DirectedEdge(v, w, 1.0));
              _graph.AddEdge(new DirectedEdge(w, v, 1.0));
            }
          }
        }
      }
    }

    /// <summary>
    /// Constructs a new PathFinder instance for the specified Map that will consider diagonal movement by using the specified diagonalCost
    /// </summary>
    /// <param name="map">The Map that this PathFinder instance will run shortest path algorithms on</param>
    /// <param name="diagonalCost">
    /// The cost of diagonal movement compared to horizontal or vertical movement. 
    /// Use 1.0 if you want the same cost for all movements.
    /// On a standard cartesian map, it should be sqrt(2) (1.41)
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown when a null map parameter is passed in</exception>
    public PathFinder(Map map, double diagonalCost)
    {
      if (map == null)
      {
        throw new ArgumentNullException("map", "Map cannot be null");
      }

      _map = map;
      _graph = new EdgeWeightedDigraph(_map.Width * _map.Height);
      foreach (ITile cell in _map.GetAllTiles())
      {
        if (!cell.IsBlockingMove)
        {
          int v = IndexFor(cell);
          foreach (ITile neighbor in _map.GetBorderTilesInSquare(cell.X, cell.Y, 1))
          {
            if (!cell.IsBlockingMove)
            {
              int w = IndexFor(neighbor);
              if (neighbor.X != cell.X && neighbor.Y != cell.Y)
              {
                _graph.AddEdge(new DirectedEdge(v, w, diagonalCost));
                _graph.AddEdge(new DirectedEdge(w, v, diagonalCost));
              }
              else
              {
                _graph.AddEdge(new DirectedEdge(v, w, 1.0));
                _graph.AddEdge(new DirectedEdge(w, v, 1.0));
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Returns a shortest Path containing a list of Cells from a specified source Cell to a destination Cell
    /// </summary>
    /// <param name="source">The Cell which is at the start of the path</param>
    /// <param name="destination">The Cell which is at the end of the path</param>
    /// <exception cref="ArgumentNullException">Thrown when source or destination is null</exception>
    /// <exception cref="PathNotFoundException">Thrown when there is not a path from the source to the destination</exception>
    /// <returns>Returns a shortest Path containing a list of Cells from a specified source Cell to a destination Cell</returns>
    public Path ShortestPath(ITile source, ITile destination)
    {
      Path shortestPath = TryFindShortestPath(source, destination);

      if (shortestPath == null)
      {
        throw new PathNotFoundException(string.Format("Path from ({0}, {1}) to ({2}, {3}) not found", source.X, source.Y, destination.X, destination.Y));
      }

      return shortestPath;
    }

    /// <summary>
    /// Returns a shortest Path containing a list of Cells from a specified source Cell to a destination Cell
    /// </summary>
    /// <param name="source">The Cell which is at the start of the path</param>
    /// <param name="destination">The Cell which is at the end of the path</param>
    /// <exception cref="ArgumentNullException">Thrown when source or destination is null</exception>
    /// <returns>Returns a shortest Path containing a list of Cells from a specified source Cell to a destination Cell. If no path is found null will be returned</returns>
    public Path TryFindShortestPath(ITile source, ITile destination)
    {
      if (source == null)
      {
        throw new ArgumentNullException("source");
      }

      if (destination == null)
      {
        throw new ArgumentNullException("destination");
      }

      var cells = ShortestPathCells(source, destination).ToList();
      if (cells[0] == null)
      {
        return null;
      }
      return new Path(cells);
    }

    private IEnumerable<ITile> ShortestPathCells(ITile source, ITile destination)
    {
      IEnumerable<DirectedEdge> path = DijkstraShortestPath.FindPath(_graph, IndexFor(source), IndexFor(destination));
      if (path == null)
      {
        yield return null;
      }
      else
      {
        yield return source;
        foreach (DirectedEdge edge in path)
        {
          yield return CellFor(edge.To);
        }
      }
    }

    private int IndexFor(ITile cell)
    {
      return (cell.Y * _map.Width) + cell.X;
    }

    private ITile CellFor(int index)
    {
      int x = index % _map.Width;
      int y = index / _map.Width;

      return _map.GetTile(x, y);
    }
  }
}
