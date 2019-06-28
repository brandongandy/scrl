using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCORLIB.ActorInterfaces
{
  public enum HostilityThreshold
  {
    Friendly = 20,
    Neutral = 40,
    Angry = 60,
    Hostile = 80,
    Hateful = 100
  }

  public interface IFaction
  {
    /// <summary>
    /// The name of the Faction.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Which Deity, if any, this Faction follows.
    /// </summary>
    IDeity FollowedDeity { get; }

    /// <summary>
    /// Determines how friendly or angry the members of this Faction are toward the Player 
    /// and toward other Factions.
    /// </summary>
    /// <returns></returns>
    HostilityThreshold GetHostilityThreshold();
  }
}
