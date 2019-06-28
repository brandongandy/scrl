using Microsoft.Xna.Framework;
using SCORLIB.ActorInterfaces;
using SCORLIB.ConversationClasses;
using SCORLIB.QuestClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCRL.Entities
{
  class NonPlayerCharacter : Actor, INonPlayerCharacter
  {
    #region INonPlayerCharacter Impl

    /// <summary>
    /// The available conversations for this NonPlayerCharacter.
    /// </summary>
    public List<Conversation> Conversations { get; private set; }

    /// <summary>
    /// The available quests deliverable by this NonPlayerCharacter.
    /// </summary>
    public List<Quest> Quests { get; private set; }

    /// <summary>
    /// Checks to see if the NonPlayerCharacter has any available conversations.
    /// </summary>
    public bool HasConversation { get { return Conversations.Count > 0; } }

    /// <summary>
    /// Checks to see if the NonPlayerCharacter has any available quests.
    /// </summary>
    public bool HasQuest { get { return Quests.Count > 0; } }

    #endregion

    private SCRL.Screens.ConversationConsole Conversation;

    public NonPlayerCharacter(Color foreground, Color background, int glyph) : base(foreground, background, glyph)
    {
      Name = "A Friendly NPC";
    }

    public void ShowConversation()
    {
      Point windowSize = new Point(31, 6);
      Conversation = new Screens.ConversationConsole(this, windowSize.X, windowSize.Y);
      Conversation.Center();

      Conversation.ShowMessage("Lorem ipsum dolor sit amet.");
    }
  }
}
