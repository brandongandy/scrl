using System;
using System.Collections.ObjectModel;

namespace SCORLIB.ActorInterfaces
{
  /// <summary>
  /// A Deity, typically, is someting worshipped by an Actor.
  /// Actors worshipping the same Deities are typically friendlier,
  /// while diametrically opposed worshippers may be more aggressive.
  /// </summary>
  public class IDeity
  {
    /// <summary>
    /// The Name of the Deity
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Aspects held by the Deity
    /// </summary>
    Collection<string> Aspects { get; }

    /// <summary>
    /// Aesthetics of the Deity
    /// </summary>
    Collection<string> Aesthetics { get; }
  }
}
