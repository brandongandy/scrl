using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCORLIB;
using SCORLIB.ActorInterfaces;

namespace SCORLIB.ActorClasses
{
  public class Actor : IActor
  {
    /// <summary>
    /// The Name of the Actor.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The Class of the Actor -- separate from a Character Class, this represents
    /// whether the Actor is a Human, Skeleton, Cow, etc.
    /// </summary>
    public string Classification { get; set; }

    /// <summary>
    /// An Actor's Alignment. Used to determine friendliness of certain other Actors,
    /// or eligibility for Deity worship or Equipment wielding.
    /// </summary>
    public string Alignment { get; set; }

    /// <summary>
    /// An Aspect is a property or character trait desired, embodied, or admired by
    /// the Actor. Sharing Aspects with Deities or other Actors makes them or their
    /// followers more likely to be friendly. All Actors have innate Aspects, though
    /// others may be added by specific equipment.
    /// </summary>
    public IEnumerable<int> Aspects { get; set; }

    /// <summary>
    /// The ID of the Deity worshipped by this Actor.
    /// </summary>
    public int Deity { get; set; }

    /// <summary>
    /// The Actor's Level.
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// An AttributePair containing the Actor's Current and Maximum Health values.
    /// </summary>
    public AttributePair Health { get; set; }

    /// <summary>
    /// An AttributePair containing the Actor's Current and Maximum Energy values.
    /// </summary>
    public AttributePair Energy { get; set; }

    /// <summary>
    /// Constitution represents the Actor's strength and toughness.
    /// This factors into physical damage doled out, and resistance to physical damage.
    /// </summary>
    public int Constitution { get; set; }

    /// <summary>
    /// Wisdom represents the Actor's intellect and magic casting ability.
    /// This factors into magical damage given and magic damage resistance.
    /// </summary>
    public int Wisdom { get; set; }

    /// <summary>
    /// Dexterity represents how nimble an Actor is.
    /// This factors into ability to dodge attacks, and attack accuracy, as well
    /// as Speed.
    /// </summary>
    public int Dexterity { get; set; }

    /// <summary>
    /// A unique index used to reference a Monster by other Monsters.
    /// </summary>
    public string BaseId { get; set; }

    /// <summary>
    /// The unique index of the "next level" of this Monster. Used when upgrading 
    /// Monsters.
    /// </summary>
    public string NextInClass { get; set; }

    /// <summary>
    /// The unique index of the Monster type that serves as a Minion to this Monster.
    /// </summary>
    public string Minion1 { get; set; }

    /// <summary>
    /// The unique index of the Monster type that serves as a second Minion to this
    /// Monster.
    /// </summary>
    public string Minion2 { get; set; }

    /// <summary>
    /// The minimum range of the number of Minions following this Monster.
    /// </summary>
    public int MinParty { get; set; }

    /// <summary>
    /// The maximum range of the number of Minions following this Monster.
    /// </summary>
    public int MaxParty { get; set; }

    /// <summary>
    /// The minimum number of Monsters of this type that spawn together.
    /// </summary>
    public int MinGroup { get; set; }

    /// <summary>
    /// The maximum number of Monsters of this type that spawn together.
    /// </summary>
    public int MaxGroup { get; set; }

    /// <summary>
    /// The Treasure Class accessed when the Actor is killed and must drop loot.
    /// </summary>
    public string TreasureClass { get; set; }
  }
}
