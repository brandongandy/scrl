using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCRL.Systems
{
  /// <summary>
  /// A queue of messages that can be added to.
  /// </summary>
  static class MessageLog
  {
    private static readonly int maxLines = 13;
    public static Queue<string> Lines = new Queue<string>();

    /// <summary>
    /// Add a message to the queue to be displayed inside the MessageLog Console
    /// </summary>
    /// <param name="message">The message to display</param>
    public static void Add(string message)
    {
      Lines.Enqueue(message);

      if (Lines.Count > maxLines)
      {
        Lines.Dequeue();
      }
    }
    
  }
}
