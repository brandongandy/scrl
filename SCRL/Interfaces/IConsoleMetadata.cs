using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCRL.Interfaces
{
  public interface IConsoleMetadata : IConsole
  {
    ConsoleMetadata Metadata { get; }
  }

  public struct ConsoleMetadata
  {
    public string Title;
    public string Summary;
  }
}
