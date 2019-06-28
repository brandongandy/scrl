using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCRL.Interfaces;

namespace SCRL.Systems
{
  class SchedulingSystem
  {
    private int time;
    private readonly SortedDictionary<int, List<ISchedulable>> schedulables;

    public SchedulingSystem()
    {
      time = 0;
      schedulables = new SortedDictionary<int, List<ISchedulable>>();
    }

    /// <summary>
    /// Adds a new object to the Schedule and places it at the current time
    /// plus the object's Time property.
    /// </summary>
    /// <param name="schedulable">The schedulable object to add.</param>
    public void Add(ISchedulable schedulable)
    {
      int key = time + schedulable.Time;
      if (!schedulables.ContainsKey(key))
      {
        schedulables.Add(key, new List<ISchedulable>());
      }
      schedulables[key].Add(schedulable);
    }

    /// <summary>
    /// Removes a specific object from the schedule.
    /// Use when a monster is killed to prevent a dead monster from trying
    /// to take an action.
    /// </summary>
    /// <param name="schedulable">The schedulable object to remove.</param>
    public void Remove(ISchedulable schedulable)
    {
      KeyValuePair<int, List<ISchedulable>> schedulableListFound =
        new KeyValuePair<int, List<ISchedulable>>(-1, null);

      foreach (var schedulablesList in schedulables)
      {
        if (schedulablesList.Value.Contains(schedulable))
        {
          schedulableListFound = schedulablesList;
          break;
        }
      }

      if (schedulableListFound.Value != null)
      {
        schedulableListFound.Value.Remove(schedulable);
        if (schedulableListFound.Value.Count <= 0)
        {
          schedulables.Remove(schedulableListFound.Key);
        }
      }
    }

    /// <summary>
    /// Gets the next object in the schedule to take its turn,
    /// and advances time if necessary.
    /// </summary>
    /// <returns></returns>
    public ISchedulable Get()
    {
      var firstSchedulableGroup = schedulables.First();
      var firstSchedulable = firstSchedulableGroup.Value.First();

      Remove(firstSchedulable);
      time = firstSchedulableGroup.Key;
      return firstSchedulable;
    }

    public int GetTime()
    {
      return time;
    }

    public void Clear()
    {
      time = 0;
      schedulables.Clear();
    }
  }
}
