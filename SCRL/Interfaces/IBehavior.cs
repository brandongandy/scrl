using SCRL.Entities;
using SCRL.Systems;

namespace SCRL.Interfaces
{
  interface IBehavior
  {
    bool Act(Monster monster, CommandSystem commandSystem);
  }
}
