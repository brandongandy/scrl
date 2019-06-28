using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCRL.Interfaces;
using SCRL.Entities;
using SCRL.Systems;
using SCRL.Mapping;

namespace SCRL.Behaviors
{
  class StandardMoveAndAttack : IBehavior
  {
    public bool Act(Monster monster, CommandSystem commandSystem)
    {
      Map map = Container.Adventure.Map;
      Player player = Container.Adventure.Player;

      FieldOfView monsterFov = new FieldOfView(map);

      if (!monster.TurnsAlerted.HasValue)
      {
        monsterFov.ComputeFov(monster.Position.X, monster.Position.Y, monster.Wisdom, true);

        if (monsterFov.IsInFov(player.Position.X, player.Position.Y))
        {
          MessageLog.Add($"{monster.ColoredName} has spotted {player.ColoredName}!");
          monster.TurnsAlerted = 1;
        }
      }

      if (monster.TurnsAlerted.HasValue)
      {
        map.GetTile(monster.Position).IsBlockingMove = false;
        map.GetTile(player.Position).IsBlockingMove = false;

        PathFinder pathFinder = new PathFinder(map, 1.41);
        Path path = null;

        try
        {
          path = pathFinder.ShortestPath(map.GetTile(monster.Position), map.GetTile(player.Position));
        }
        catch (PathNotFoundException)
        {
          MessageLog.Add($"{monster.ColoredName} waits for a turn...");
        }

        map.GetTile(monster.Position).IsBlockingMove = true;
        map.GetTile(player.Position).IsBlockingMove = true;

        if (path != null)
        {
          try
          {
            commandSystem.MoveMonster(monster, path.StepForward());
          } catch (NoMoreStepsException)
          {
            MessageLog.Add($"{monster.ColoredName} growls in frustration.");
          }
        }

        monster.TurnsAlerted++;

        if (monster.TurnsAlerted > 15)
        {
          monster.TurnsAlerted = null;
        }
      }

      return true;
    }
  }
}
