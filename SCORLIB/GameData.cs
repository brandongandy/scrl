using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCORLIB.ItemClasses;
using SCORLIB.ActorClasses;
using SCORLIB.Systems;
using LumenWorks.Framework.IO.Csv;
using System.IO;
using System.Reflection;
using SCORLIB.ConversationClasses;

namespace SCORLIB
{
  /// <summary>
  /// This is the main entry class for SCORLIB. Use it to initialize,
  /// serialize, and deserialize data from the Content directory. Use 
  /// its properties to access a static list of available game items
  /// such as weapons, armor, etc. 
  /// </summary>
  public static class GameData
  {
    #region Fields
    
    private static string DataPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    #endregion

    #region Properties

    /// <summary>
    /// Holds a list of all Aspects available for use by Actors and Items.
    /// </summary>
    public static List<string> Aspects { get; private set; }

    /// <summary>
    /// Holds a list of all Prefixes available to attach to items.
    /// </summary>
    public static List<Affix> Prefixes { get; private set; }

    /// <summary>
    /// Holds a list of all Suffixes available to attach to items.
    /// </summary>
    public static List<Affix> Suffixes { get; private set; }

    /// <summary>
    /// Holds a key-value pair collection of Armors available for use by Actors.
    /// Use the .New() method to return a new Armor from the collection.
    /// </summary>
    public static Dictionary<string, Armor> Armors { get; private set; }

    /// <summary>
    /// Holds a key-value pair collection of Weapons available for use by Actors.
    /// Use the .New() method to return a new Weapon from the collection.
    /// </summary>
    public static Dictionary<string, Weapon> Weapons { get; private set; }

    /// <summary>
    /// Holds a list of TreasureClasses, referenced when a monster dies to
    /// generate loot.
    /// </summary>
    public static TreasureClass TreasureClasses { get; private set; }

    /// <summary>
    /// Holds a list of Monsters available to summon in-game.
    /// </summary>
    public static List<Actor> Monsters { get; private set; }

    #endregion

    /// <summary>
    /// Initializes all game data. Sets the DataPath and then reads through
    /// the DataPath folder to find and parse game data into static lists
    /// for access by whoever is using SCORLIB.
    /// </summary>
    /// <param name="path">The path where game data can be found -- by default, a local folder \Content\</param>
    public static void Initialize(string path = @"\Content\")
    {
      if (!string.IsNullOrWhiteSpace(path))
      {
        DataPath += path;
      }

      InitializeAspects();
      InitializePrefixes();
      InitializeSuffixes();
      InitializeArmor();
      InitializeWeapons();
      InitializeTreasureClasses();
      InitializeMonsters();
    }

    public static Affix GeneratePrefix()
    {
      int index = Systems.Random.Next(0, Prefixes.Count - 1);
      return Prefixes[index];
    }

    public static Affix GenerateSuffix()
    {
      int index = Systems.Random.Next(0, Suffixes.Count - 1);
      return Suffixes[index];
    }

    #region Deserializers

    /// <summary>
    /// Reads in Aspect data, which are used as properties on most game objects.
    /// </summary>
    private static void InitializeAspects()
    {
      Aspects = new List<string>();
    }

    /// <summary>
    /// Parses Prefix data and populates a static list of Prefixes.
    /// </summary>
    private static void InitializePrefixes()
    {
      Prefixes = new List<Affix>();
      string prefixDataPath = $@"{DataPath}\MagicPrefix.txt";

      using (CsvReader csv = new CsvReader(new StreamReader(prefixDataPath), true, '\t'))
      {
        while (csv.ReadNextRecord())
        {
          string name = csv["name"];
          string mod1Code = csv["mod1code"];
          int mod1Min = Convert.ToInt32(csv["mod1min"]);
          int mod1Max = Convert.ToInt32(csv["mod1max"]);

          Affix prefix = new Affix(name, mod1Code, mod1Min, mod1Max);
          Prefixes.Add(prefix);
        }
      }
    }

    /// <summary>
    /// Parses Suffix data and populates a static list of Suffixes.
    /// </summary>
    private static void InitializeSuffixes()
    {
      Suffixes = new List<Affix>();
      string suffixDataPath = $@"{DataPath}\MagicSuffix.txt";

      using (CsvReader csv = new CsvReader(new StreamReader(suffixDataPath), true, '\t'))
      {
        while (csv.ReadNextRecord())
        {
          string name = csv["name"];
          string mod1Code = csv["mod1code"];
          int mod1Min = Convert.ToInt32(csv["mod1min"]);
          int mod1Max = Convert.ToInt32(csv["mod1max"]);

          Affix suffix = new Affix(name, mod1Code, mod1Min, mod1Max);
          Suffixes.Add(suffix);
        }
      }
    }

    /// <summary>
    /// Parses Armor data and populates a static list of Armor objects.
    /// </summary>
    private static void InitializeArmor()
    {
      Armors = new Dictionary<string, Armor>();
      string armorDataPath = $@"{DataPath}\Armor.txt";

      using (CsvReader csv = new CsvReader(new StreamReader(armorDataPath), true, '\t'))
      {
        while (csv.ReadNextRecord())
        {
          string name = csv["name"];
          int minAc = Convert.ToInt32(csv["minac"]);
          int maxAc = Convert.ToInt32(csv["maxac"]);

          // TODO: Fill in data file with this data.
          Armor armor = new Armor(name, 1, 100, ItemInterfaces.Location.Body, "Armor", minAc, maxAc, "Fighter");
          Armors.Add(armor.Name, armor);
        }
      }
    }

    /// <summary>
    /// Parses Weapon data and populates a static list of Weapon objects.
    /// </summary>
    private static void InitializeWeapons()
    {
      Weapons = new Dictionary<string, Weapon>();
      string weaponDataPath = $@"{DataPath}\Weapon.txt";

      using (CsvReader csv = new CsvReader(new StreamReader(weaponDataPath), true, '\t'))
      {
        while (csv.ReadNextRecord())
        {
          string name = csv["name"];
          int minDamage = Convert.ToInt32(csv["mindam"]);
          int maxDamage = Convert.ToInt32(csv["maxdam"]);
          string weaponType = csv["wclass"];
          int level = Convert.ToInt32(csv["lvl"]);
          int value = Convert.ToInt32(csv["value"]);
          ItemInterfaces.HandsRequired handsRequired = (ItemInterfaces.HandsRequired)Convert.ToInt32(csv["hands"]);

          Weapon weapon = new Weapon(name, level, value, minDamage, maxDamage, handsRequired, weaponType, "Fighter");
          Weapons.Add(weapon.Name, weapon);
        }
      }
    }

    /// <summary>
    /// Parses TreasureClass data, used by Monsters when they die to generate loot.
    /// </summary>
    private static void InitializeTreasureClasses()
    {
      TreasureClasses = new TreasureClass();
      string treasureClassPath = $@"{DataPath}\TreasureClass.txt";

      using (CsvReader csv = new CsvReader(new StreamReader(treasureClassPath), true, '\t'))
      {
        while (csv.ReadNextRecord())
        {
          string key = csv["treasureclass"];
          string val1 = csv["item1"];
          string val2 = csv["item2"];
          string val3 = csv["item3"];

          TreasureClasses.AddTreasureClass(key, val1, val2, val3);
        }
      }
    }

    /// <summary>
    /// Reads in Monster data and populates a static list of Monsters.
    /// </summary>
    private static void InitializeMonsters()
    {
      Monsters = new List<Actor>();
      string monsterDataPath = $@"{DataPath}\MonStats.txt";

      using (CsvReader csv = new CsvReader(new StreamReader(monsterDataPath), true, '\t'))
      {
        while (csv.ReadNextRecord())
        {
          string baseId = csv["baseid"];
          string nextInClass = csv["nextinclass"];
          string name = csv["namestr"].ToDisplayString();
          string classification = csv["classification"];
          string alignment = csv["alignment"];
          int aspect1 = Convert.ToInt32(csv["aspect1"]);
          int aspect2 = Convert.ToInt32(csv["aspect2"]);
          int aspect3 = Convert.ToInt32(csv["aspect3"]);
          int deity = Convert.ToInt32(csv["deity"]);
          int level = Convert.ToInt32(csv["level"]);
          AttributePair health = new AttributePair(Convert.ToInt32(csv["maxhealth"]));
          AttributePair energy = new AttributePair(Convert.ToInt32(csv["maxenergy"]));
          int con = Convert.ToInt32(csv["constitution"]);
          int wis = Convert.ToInt32(csv["wisdom"]);
          int dex = Convert.ToInt32(csv["dexterity"]);
          string minion1 = csv["minion1"];
          string minion2 = csv["minion2"];
          int minParty = Convert.ToInt32(csv["minparty"]);
          int maxParty = Convert.ToInt32(csv["maxparty"]);
          int minGroup = Convert.ToInt32(csv["mingroup"]);
          int maxGroup = Convert.ToInt32(csv["maxgroup"]);
          string treasureClass = csv["treasureclass1"];

          List<int> aspects = new List<int>() { aspect1, aspect2, aspect3 };

          Actor monster = new Actor()
          {
            BaseId = baseId,
            NextInClass = nextInClass,
            Name = name,
            Classification = classification,
            Alignment = alignment,
            Aspects = aspects,
            Deity = deity,
            Level = level,
            Health = health,
            Energy = energy,
            Constitution = con,
            Wisdom = wis,
            Dexterity = dex,
            Minion1 = minion1,
            Minion2 = minion2,
            MinParty = minParty,
            MaxParty = maxParty,
            MinGroup = minGroup,
            MaxGroup = maxGroup,
            TreasureClass = treasureClass
          };

          Monsters.Add(monster);
        }
      }
    }

    private static void InitializeConversations()
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
