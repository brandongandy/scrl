namespace SCORLIB.ItemInterfaces
{
  public interface IArmor
  {
    /// <summary>
    /// Where on the body this armor is meant to be worn.
    /// </summary>
    Location Location { get; set; }

    /// <summary>
    /// The type of armor this is; i.e., boots, greaves,
    /// pauldrons, etc.
    /// </summary>
    string ArmorType { get; set; }

    /// <summary>
    /// The minimum Armor Class this Armor can carry.
    /// </summary>
    int MinAC { get; set; }

    /// <summary>
    /// The maximum Armor Class this Armor can carry.
    /// </summary>
    int MaxAC { get; set; }

    /// <summary>
    /// The current Armor Class for this Armor.
    /// Between MinAC and MaxAC.
    /// </summary>
    int ArmorClass { get; set; }

    /// <summary>
    /// Determines the Armor Class for this Armor between its Min and Max values. 
    /// </summary>
    /// <param name="minAc">The MinAC this Armor can carry</param>
    /// <param name="maxAc">The MaxAC this Armor can carry</param>
    /// <returns>A random value between MinAC and MaxAC</returns>
    int GetACValue(int minAC, int maxAC);
  }
}
