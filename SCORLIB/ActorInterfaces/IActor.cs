using System.Collections.Generic;

namespace SCORLIB.ActorInterfaces
{
  /// <summary>
  /// An Actor represents any Entity that can take actions within the game. These
  /// include NPCs, Monsters, possibly certain spells, and the Player.
  /// </summary>
  public interface IActor
  {
    /// <summary>
    /// The Name of the Actor -- i.e., "Kobold", "Roger".
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// The Class of the Actor -- separate from a Character Class, this represents
    /// whether the Actor is a Human, Skeleton, Cow, etc. Used to determine efficacy
    /// of specific attack types, or whether items can be equipped to certain Actors.
    /// </summary>
    string Classification { get; set; }

    /// <summary>
    /// An Actor's Alignment. Used to determine friendliness of certain other Actors,
    /// or eligibility for Deity worship or Equipment wielding.
    /// </summary>
    string Alignment { get; set; }

    /// <summary>
    /// A pointer to the Aspect ID in Aspect.txt describing which Aspects
    /// are held by this Actor.
    /// </summary>
    /// <remarks>
    /// An Aspect is a property or character trait desired, embodied, or admired by
    /// the Actor. Sharing Aspects with Deities or other Actors makes them or their
    /// followers more likely to be friendly. All Actors have innate Aspects, though
    /// others may be added by specific equipment.
    /// </remarks>
    IEnumerable<int> Aspects { get; }

    /// <summary>
    /// The ID of the Deity worshipped by this Actor.
    /// </summary>
    int Deity { get; }

    /// <summary>
    /// The Actor's Level.
    /// </summary>
    int Level { get; set; }

    /// <summary>
    /// An AttributePair containing the Actor's Current and Maximum Health values.
    /// </summary>
    AttributePair Health { get; set; }

    /// <summary>
    /// An AttributePair containing the Actor's Current and Maximum Energy values.
    /// </summary>
    AttributePair Energy { get; set; }

    /// <summary>
    /// Constitution represents the Actor's strength and toughness.
    /// This factors into physical damage doled out, and resistance to physical damage.
    /// </summary>
    int Constitution { get; set; }

    /// <summary>
    /// Wisdom represents the Actor's intellect and magic casting ability.
    /// This factors into magical damage given and magic damage resistance.
    /// </summary>
    int Wisdom { get; set; }

    /// <summary>
    /// Dexterity represents how nimble an Actor is.
    /// This factors into ability to dodge attacks, and attack accuracy, as well
    /// as Speed.
    /// </summary>
    int Dexterity { get; set; }

    // TODO: Update so this data can be moved back to IMonster?

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
