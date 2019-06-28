using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCORLIB.ItemInterfaces
{
  public interface ICloneable<T> where T : ICloneable<T>
  {
    T Clone();
    T GetNew();
  }
}
