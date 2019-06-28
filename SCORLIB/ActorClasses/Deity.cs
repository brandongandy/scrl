using SCORLIB.ActorInterfaces;
using System.Collections.Generic;

namespace SCORLIB.ActorClasses
{
  public class Deity : IDeity
  {
    public string Name { get; set; }
    public List<string> Aspects{ get; private set; }
    public List<string> Aesthetics { get; private set; }

    /// <summary>
    /// Creates a new, empty Deity object
    /// </summary>
    public Deity()
    {
      Aspects = new List<string>();
      Aesthetics = new List<string>();
    }

    /// <summary>
    /// Creates a Deity object with only a Name and empty Aspects and Aesthetics
    /// </summary>
    /// <param name="name">The Name of the Deity</param>
    public Deity(string name)
      : this()
    {
      Name = name;
    }

    /// <summary>
    /// Creates a Deity with the given Name, Aspects, and Aesthetics
    /// </summary>
    /// <param name="name">The Name of the Deity</param>
    /// <param name="aspects">The Deity's Aspects</param>
    /// <param name="aesthetics">The Deity's Aesthetics</param>
    public Deity(string name, List<string> aspects, List<string> aesthetics)
      : this(name)
    {
      Aspects = aspects;
      Aesthetics = aesthetics;
    }
  }
}
