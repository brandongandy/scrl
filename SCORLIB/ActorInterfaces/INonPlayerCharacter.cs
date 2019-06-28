using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCORLIB.QuestClasses;
using SCORLIB.ConversationClasses;

namespace SCORLIB.ActorInterfaces
{
  public interface INonPlayerCharacter
  {
    /// <summary>
    /// The available conversations for this NonPlayerCharacter.
    /// </summary>
    List<Conversation> Conversations { get; }

    /// <summary>
    /// The available quests deliverable by this NonPlayerCharacter.
    /// </summary>
    List<Quest> Quests { get; }

    /// <summary>
    /// Checks to see if the NonPlayerCharacter has any available conversations.
    /// </summary>
    bool HasConversation { get; }

    /// <summary>
    /// Checks to see if the NonPlayerCharacter has any available quests.
    /// </summary>
    bool HasQuest { get; }
  }
}
