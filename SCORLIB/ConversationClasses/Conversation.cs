using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCORLIB.ConversationClasses
{
  /// <summary>
  /// A class representing Conversations that can be held with NPCs within the game.
  /// </summary>
  public class Conversation
  {
    /// <summary>
    /// The NPC who "owns" this Conversation.
    /// </summary>
    public string Owner { get; private set; }

    /// <summary>
    /// Nodes of a conversation.
    /// </summary>
    public Dictionary<string, string> Nodes { get; private set; }

    /// <summary>
    /// Initializes an empty Conversation with no Owner.
    /// </summary>
    public Conversation() => Nodes = new Dictionary<string, string>();

    /// <summary>
    /// Initializes an empty Conversation with the Owner provided.
    /// </summary>
    /// <param name="owner">The name of the NPC who owns this Conversation</param>
    public Conversation(string owner) : this() => Owner = owner;

    /// <summary>
    /// Initializes a Conversation with the Owner and Nodes provided.
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="nodes"></param>
    public Conversation(string owner, Dictionary<string, string> nodes) : this(owner) => Nodes = nodes;

    /// <summary>
    /// Adds a Node to the Conversation with the given Key and Text. If the given Key
    /// already exists, then the Node will be overwritten with the given Text.
    /// </summary>
    /// <param name="key">A unique identifier for the given Node</param>
    /// <param name="text">The Text to be shown during NPC interaction</param>
    public void AddNode(string key, string text)
    {
      if (Nodes.Keys.Contains(key))
      {
        Nodes[key] = text;
      }
      else
      {
        Nodes.Add(key, text);
      }
    }

    public string GetConversationTextByNode(string nodeID)
    {
      if (Nodes.Keys.Contains(nodeID))
      {
        return Nodes[nodeID];
      }
      else
      {
        throw new KeyNotFoundException();
      }
    }
  }
}
