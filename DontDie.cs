using System;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
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

	public static int RandomNpc()
	{
		Random rnd = new Random();
		int randomNpc = rnd.Next(-65, 687);
		
		//if NPC can do emotes then rerun random
		if (NPCID.Sets.FaceEmote[randomNpc] > 0)
		{
			Main.NewText("dundun");
			return RandomNpc();
		}
		
		return randomNpc;
	}
}