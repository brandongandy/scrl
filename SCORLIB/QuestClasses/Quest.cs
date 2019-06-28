using SCORLIB.ItemClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCORLIB.QuestClasses
{
  /// <summary>
  /// Holds information on the items required to complete a Quest.
  /// </summary>
  public struct ItemQuantity
  {
    /// <summary>
    /// The BaseID of the Item.
    /// </summary>
    public string ItemId { get; set; }

    /// <summary>
    /// The Count of the Item required.
    /// </summary>
    public int Quantity { get; set; }

    public ItemQuantity(string itemId, int quantity)
    {
      ItemId = itemId;
      Quantity = quantity;
    }
  }

  public class Quest
  {
    /// <summary>
    /// A unique identifier for this particular quest.
    /// </summary>
    public int QuestId { get; private set; }

    /// <summary>
    /// A user-friendly Name for the Quest, possibly for display in a Journal
    /// or similar interface.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// A user-friendly description of the Quest and what's required to complete it.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// The Items required to complete this Quest.
    /// </summary>
    public List<ItemQuantity> ItemsToComplete { get; private set; }

    /// <summary>
    /// How much EXP is provided upon completion of the Quest.
    /// </summary>
    public int RewardEXP { get; private set; }

    /// <summary>
    /// How much gold is provided upon completion of the Quest.
    /// </summary>
    public int RewardGold { get; private set; }

    /// <summary>
    /// The TreasureClass of the reard items, awarded upon completion of the Quest.
    /// </summary>
    public TreasureClass RewardItems { get; private set; }

    /// <summary>
    /// Creates a new Quest with the provided values.
    /// </summary>
    /// <param name="questId">A unique identifier for this Quest</param>
    /// <param name="name">The user-friendly Name of the Quest</param>
    /// <param name="description">The user-friendly Description of the Quest</param>
    /// <param name="itemsToComplete">The complete list of ItemsToComplete for the Quest</param>
    /// <param name="rewardEXP">How many experience points are rewarded upon completion</param>
    /// <param name="rewardGold">How much gold is rewarded upon completion</param>
    /// <param name="rewardItems">The TreasureClass for items rewarded upon completion</param>
    public Quest(int questId, string name, string description, List<ItemQuantity> itemsToComplete,
      int rewardEXP, int rewardGold, TreasureClass rewardItems)
    {
      QuestId = questId;
      Name = name;
      Description = description;
      ItemsToComplete = itemsToComplete;
      RewardEXP = rewardEXP;
      RewardGold = rewardGold;
      RewardItems = rewardItems;
    }

    /// <summary>
    /// Creates a new Quest with the provided values.
    /// </summary>
    /// <param name="questID">A unique identifier for this Quest</param>
    /// <param name="name">The user-friendly Name of the Quest</param>
    /// <param name="description">The user-friendly Description of the Quest</param>
    /// <param name="rewardExp">How many experience points are rewarded upon completion</param>
    /// <param name="rewardGold">How much gold is rewarded upon completion</param>
    /// <param name="rewardItems">The TreasureClass for items rewarded upon completion</param>
    /// <param name="itemsToComplete">ItemsToComplete for the Quest</param>
    public Quest(int questID, string name, string description, int rewardExp, int rewardGold,
      TreasureClass rewardItems, params ItemQuantity[] itemsToComplete)
    {
      QuestId = questID;
      Name = name;
      Description = description;
      RewardEXP = rewardExp;
      RewardGold = rewardGold;
      RewardItems = rewardItems;

      ItemsToComplete = new List<ItemQuantity>();
      foreach (var item in itemsToComplete)
      {
        ItemsToComplete.Add(item);
      }
    }
  }
}
