using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DontDie;

public class DontDiePlayer : ModPlayer
{
    public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
    {
        Random rnd = new Random();
        int randomPlayer = rnd.Next(0, Main.CurrentFrameFlags.ActivePlayersCount);
        int randomNpc = rnd.Next(-65, 687);
        // figure out how to exclude entries
        float x = Main.PlayerList[randomPlayer].Player.position.X;
        float y = Main.PlayerList[randomPlayer].Player.position.Y;
        
        Main.NewText(Main.PlayerList[randomPlayer].Name);
        
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            NPC.NewNPC(NPC.GetSource_None(), (int)x, (int)y, randomNpc);
        } 
        else if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            ModPacket packet = ModContent.GetInstance<DontDie>().GetPacket();
            packet.Write((byte)MessageType.SpawnNpc);
            packet.Write(randomNpc);
            packet.Write((int)x);
            packet.Write((int)y);
            packet.Send();
        }
    }
}

public enum MessageType : byte
{
    SpawnNpc
}