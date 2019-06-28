using Microsoft.Xna.Framework;
using SadConsole;
using SCORLIB.ActorInterfaces;
using SCRL.Behaviors;
using SCRL.Resources;
using SCRL.Systems;
using System;

namespace SCRL.Entities
{
  class Monster : Actor
  {
    public int? TurnsAlerted { get; set; }
    StandardMoveAndAttack behavior = new StandardMoveAndAttack();

    public Monster(Color foreground, Color background, int glyph) : base(foreground, background, glyph)
    {
      
    }

    public Monster(Color foreground, Color background, int glyph, IActor baseMonster) : this(foreground, background, glyph)
    {
      Clone(baseMonster);
    }

    public void Clone(IActor actor)
    {
      Name = actor.Name;
      BaseId = actor.BaseId;
      NextInClass = actor.NextInClass;
      Classification = actor.Classification;
      Alignment = actor.Alignment;
      Aspects = actor.Aspects;
      Deity = actor.Deity;
      Level = actor.Level;
      Health = actor.Health;
      Energy = actor.Energy;
      Constitution = actor.Constitution;
      Dexterity = actor.Dexterity;
      Wisdom = actor.Wisdom;
      Minion1 = actor.Minion1;
      Minion2 = actor.Minion2;
      MinParty = actor.MinParty;
      MaxParty = actor.MaxParty;
      MinGroup = actor.MinGroup;
      MaxGroup = actor.MaxGroup;
      TreasureClass = actor.TreasureClass;
    }

    public virtual void PerformAction(CommandSystem commandSystem)
    {
      behavior.Act(this, commandSystem);
    }
  }
}
