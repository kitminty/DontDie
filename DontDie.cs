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
	
	public static int RandomPlayer() 
	{
		Random rnd = new Random();
		int randomPlayer = rnd.Next(0, Main.CurrentFrameFlags.ActivePlayersCount);
		
		return randomPlayer;
	}
	//211
	public static int RandomNpc()
	{
		Random rnd = new Random();
		//int randomNpc = rnd.Next(-65, NPCLoader.NPCCount);
		int randomNpc = rnd.Next(104, 106);
		
		//if random npc is bigger check modded npc and not check if can do face emotes
		if (randomNpc >= NPCID.Count ? NPCLoader.GetNPC(randomNpc).NPC.townNPC : NPCID.Sets.FaceEmote[randomNpc] > 0)
		{
			Main.NewText("Rolled excluded npc, Rerolling");
			return RandomNpc();
		}
		
		return randomNpc;
	}
}