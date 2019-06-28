using System;
using SadConsole;
using SCRL.Resources;
using Microsoft.Xna.Framework;

namespace SCRL.Interfaces
{
  interface ITile : IEquatable<ITile>
  {
    int X { get; set; }
    int Y { get; set; }
    bool IsBlockingMove { get; set; }
    bool IsBlockingLOS { get; set; }
    bool IsMapped { get; set; }
    bool IsInFOV { get; set; }
    Color DarkBackground { get; set; }
    Color DarkForeground { get; set; }
    Color LightBackground { get; set; }
    Color LightForeground { get; set; }


    void SetUnexploredVariant();

    void SetLightVariant();

    void SetDarkVariant();
  }
}
