namespace SCRL.Interfaces
{
  interface ISchedulable
  {
    /// <summary>
    /// How many turns can pass through the Scheduling system before
    /// the ISchedulable gets its turn again.
    /// </summary>
    int Time { get; }
  }
}
