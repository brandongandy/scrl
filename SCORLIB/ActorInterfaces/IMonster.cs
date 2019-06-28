using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCORLIB.ActorInterfaces
{
  public interface IMonster
  {
    /// <summary>
    /// A unique index used to reference a Monster by other Monsters.
    /// </summary>
    string BaseId { get; }

    /// <summary>
    /// The unique index of the "next level" of this Monster. Used when upgrading 
    /// Monsters.
    /// </summary>
    string NextInClass { get; }

    /// <summary>
    /// The unique index of the Monster type that serves as a Minion to this Monster.
    /// </summary>
    string Minion1 { get; }

    /// <summary>
    /// The unique index of the Monster type that serves as a second Minion to this
    /// Monster.
    /// </summary>
    string Minion2 { get; }

    /// <summary>
    /// The minimum range of the number of Minions following this Monster.
    /// </summary>
    int MinParty { get; }

    /// <summary>
    /// The maximum range of the number of Minions following this Monster.
    /// </summary>
    int MaxParty { get; }

    /// <summary>
    /// The minimum number of Monsters of this type that spawn together.
    /// </summary>
    int MinGroup { get; }

    /// <summary>
    /// The maximum number of Monsters of this type that spawn together.
    /// </summary>
    int MaxGroup { get; }

    /// <summary>
    /// The Treasure Class accessed when the Actor is killed and must drop loot.
    /// </summary>
    string TreasureClass { get; set; }
  }
}
