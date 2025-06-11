using System;
using System.Collections.Generic;
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
	
	public static int RandomPlayer() 
	{
		Random rnd = new Random();
		int randomPlayer = rnd.Next(0, Main.CurrentFrameFlags.ActivePlayersCount);
		
		return randomPlayer;
	}

	public static int RandomNpc()
	{
		Random rnd = new Random();
		List<int> excludedNpcs = new List<int>() 
		{
			//Town NPC's
			22, 17, 18, 227, 207, 633, 588, 589, 208, 369, 376, 353, 354, 38, 20, 550, 579, 19, 107, 105, 228, 54, 124, 123, 441, 229, 160, 108, 106, 178, 142, 663, 368, 37, 453
		};
		int randomNpc = rnd.Next(-65, NPCLoader.NPCCount+1);
		
		//if random npc is bigger than vanilla npc count check modded npc type and not check non modded npc type
		if (excludedNpcs.Contains(randomNpc) || (randomNpc >= NPCID.Count ? NPCLoader.GetNPC(randomNpc).NPC.townNPC : false))
		{
			Main.NewText("Rolled excluded npc, Rerolling");
			return RandomNpc();
		}
		
		return randomNpc;
	}
}