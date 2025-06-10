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
        float x = Main.PlayerList[randomPlayer].Player.position.X;
        float y = Main.PlayerList[randomPlayer].Player.position.Y;
        
        Main.NewText(Main.PlayerList[randomPlayer].Name+" was cursed");
        
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            NPC.NewNPC(NPC.GetSource_None(), (int)x, (int)y, DontDie.RandomNpc());
        } 
        else if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            ModPacket packet = ModContent.GetInstance<DontDie>().GetPacket();
            packet.Write((byte)MessageType.SpawnNpc);
            packet.Write(DontDie.RandomNpc());
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