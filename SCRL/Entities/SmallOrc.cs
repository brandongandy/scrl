using Microsoft.Xna.Framework;
using SCRL.Resources;

namespace SCRL.Entities
{
  class SmallOrc : Monster
  {
    public SmallOrc() : base(Palette.TahitiGold, Palette.LightFloorForeground, 'o')
    {
      Clone(SCORLIB.MonsterFactory.GetMonsterByName("orc1"));
      Gold = 10;
      Health = new SCORLIB.AttributePair(10);
      Name = "Small Orc";

      Constitution = 6;
      Wisdom = 5;
      Dexterity = 6;
    }
  }
}
