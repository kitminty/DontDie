using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DontDie;

public class DontDiePlayer : ModPlayer
{
    public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
    {
        int randplayer = DontDie.RandomPlayer();
        int randnpc = DontDie.RandomNpc();
        float x = Main.PlayerList[randplayer].Player.position.X;
        float y = Main.PlayerList[randplayer].Player.position.Y;
        
        Main.NewText(Main.PlayerList[randplayer].Name+" was cursed by " + randnpc + NPC.GetFullnameByID(randnpc));
        
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            NPC.NewNPC(Terraria.Entity.GetSource_None(), (int)x, (int)y, randnpc);
        } 
        else if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            ModPacket packet = ModContent.GetInstance<DontDie>().GetPacket();
            packet.Write((byte)MessageType.SpawnNpc);
            packet.Write(randnpc);
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