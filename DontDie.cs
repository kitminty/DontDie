using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace DontDie;

public class DontDie : Mod
{
	public override void HandlePacket(BinaryReader reader, int whoAmI)
	{
		MessageType messageType = (MessageType)reader.ReadByte();
		if (messageType == MessageType.SpawnNpc)
		{
			int npcType = reader.ReadInt32();
			int x = reader.ReadInt32();
			int y = reader.ReadInt32();
			NPC.NewNPC(NPC.GetSource_None(), x, y, npcType);
		}
	}
}