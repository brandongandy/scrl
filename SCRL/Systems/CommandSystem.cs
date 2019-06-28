using Microsoft.Xna.Framework;
using SCORLIB.Systems;
using SCRL.Entities;
using SCRL.MapObjects;
using SCRL.Resources;
using SCRL.Interfaces;

namespace SCRL.Systems
{
  class CommandSystem
  {
    public bool IsPlayerTurn { get; set; }

    public void EndPlayerTurn()
    {
      IsPlayerTurn = false;
    }

    public bool MovePlayer(Direction direction)
    {
      Point newPos = Container.Adventure.Player.Position + DirectionUtils.DirectionToVector(direction);
      if (Container.Adventure.SetActorPosition(Container.Adventure.Player, newPos))
      {
        return true;
      }

      Monster monster = Container.Adventure.GetMonsterAt(newPos);

      if (monster != null)
      {
        Attack(Container.Adventure.Player, monster);
        return true;
      }

      NonPlayerCharacter npc = Container.Adventure.GetNpcAt(newPos);

      if (npc != null)
      {
        npc.ShowConversation();
      }

      return false;
    }

    public void ActivateMonsters()
    {
      ISchedulable schedulable = Container.SchedulingSystem.Get();
      if (schedulable is Player)
      {
        IsPlayerTurn = true;
        Container.SchedulingSystem.Add(Container.Adventure.Player);
      }
      else
      {
        Monster monster = schedulable as Monster;
        if (monster != null)
        {
          monster.PerformAction(this);
          Container.SchedulingSystem.Add(monster);
        }
        ActivateMonsters();
      }
    }

    public void MoveMonster(Monster monster, ITile tile)
    {
      if (!Container.Adventure.SetActorPosition(monster, new Point(tile.X, tile.Y)))
      {
        if (Container.Adventure.Player.Position == new Point(tile.X, tile.Y))
        {
          Attack(monster, Container.Adventure.Player);
        }
      }
    }

    public void Attack(Actor attacker, Actor defender)
    {
      if (ResolveAttack(attacker, defender))
      {
        ResolveDamage(attacker, defender);
      }
    }

    public bool ResolveAttack(Actor attacker, Actor defender)
    {
      bool hits = false;

      if (Random.Next(0, 20) + attacker.Dexterity > defender.Dexterity)
      {
        if (Random.Next(0, 20) + attacker.Constitution > defender.Constitution)
        {
          hits = true;
        }
        else
        {
          MessageLog.Add($"{attacker.ColoredName}'s attack was deflected by {defender.ColoredName}!");
        }
      }
      else
      {
        MessageLog.Add($"{attacker.ColoredName} missed {defender.ColoredName} completely!");
      }

      return hits;
    }

    public void ResolveDamage(Actor attacker, Actor defender)
    {
      int damage = Random.Next(1, attacker.Constitution);

      damage -= Random.Next(1, defender.Constitution);

      if (damage > 0)
      {
        defender.Health.Damage((ushort)damage);
        MessageLog.Add($"{attacker.ColoredName} hit {defender.ColoredName} for [c:r f:{Palette.Mandy.ToParser()}]{damage} dmg[c:u]");

        if (defender.Health.CurrentValue <= 0)
        {
          ResolveDeath(defender);
        }
      }
      else
      {
        MessageLog.Add($"{attacker.ColoredName} hit {defender.ColoredName}, but {defender.ColoredName} parried the blow.");
      }
    }

    private void ResolveDeath(Actor defender)
    {
      if (defender is Player)
      {
        MessageLog.Add($"{defender.Name} was killed! Game over, man!");
      } else if (defender is Monster)
      {
        Container.Adventure.RemoveMonster((Monster)defender);
        MessageLog.Add($"{defender.ColoredName} died and dropped [c:r f:{Palette.GoldenFizz.ToParser()}]{defender.Gold} gold pieces.[c:u]");
      }
    }
  }
}
