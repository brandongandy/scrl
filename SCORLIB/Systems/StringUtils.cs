using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCORLIB.Systems
{
  /// <summary>
  /// Holds extension methods for handling SCORLIB strings.
  /// </summary>
  public static class StringUtils
  {
    private static TextInfo myTextInfo = new CultureInfo("en-US", false).TextInfo;
    /// <summary>
    /// Makes pretty the display string text from Content files by removing things like underscores.
    /// </summary>
    /// <param name="text">The string to modify</param>
    /// <param name="titleCase">Whether the string should be converted to Title Text or not</param>
    /// <returns>A prettier string with spaces and Title Case if desired</returns>
    public static string ToDisplayString(this string text, bool titleCase = false)
    {
      string displayText = text.Replace('_', ' ');

      if (titleCase)
      {
        displayText = myTextInfo.ToTitleCase(displayText);
      }

      return displayText;
    }
  }
}
