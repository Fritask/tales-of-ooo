using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Personalities;
using TalesOfOoo.Content.Items.Weapons;

namespace TalesOfOoo.Content.NPCs
{
    //[AutoloadHead]
    internal class SliminNPC : ModNPC
    {
        //public override string Texture => "TalesOfOoo/Content/NPCs/JakeNPC";

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 10;

            //NPCID.Sets.ExtraFramesCount[Type] = 9;
            //NPCID.Sets.AttackFrameCount[Type] = 4;
            NPCID.Sets.DangerDetectRange[Type] = 1500;
            NPCID.Sets.AttackType[Type] = 0;
            NPCID.Sets.AttackTime[Type] = 25;
            NPCID.Sets.AttackAverageChance[Type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 4;

            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
                Direction = 1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but JakeNPC will be drawn facing the right
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            // Set Example Person's biome and neighbor preferences with the NPCHappiness hook. You can add happiness text and remarks with localization (See an example in ExampleMod/Localization/en-US.lang).
            // NOTE: The following code uses chaining - a style that works due to the fact that the SetXAffection methods return the same NPCHappiness instance they're called on.
            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like) // Example Person prefers the forest.
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Dislike) // Example Person dislikes the snow.
                .SetNPCAffection(NPCID.Dryad, AffectionLevel.Love) // Loves living near the dryad.
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like) // Likes living near the guide.
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Dislike) // Dislikes living near the merchant.
                .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Hate) // Hates living near the demolitionist.
            ; // < Mind the semicolon!
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.TownSlimeBlue);
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.scale = 0.08f;
            NPC.aiStyle = 7; //mt informação daora aqui dentro
            NPC.damage = 40;
            NPC.defense = 30;
            NPC.lifeMax = 550;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.knockBackResist = 0.5f;
            //DrawOffsetY = -2;

            AnimationType = NPCID.TownSlimeBlue;
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "slime doido"
            };
        }

        public override string GetChat()
        {
            int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
            if (partyGirl >= 0 && Main.rand.NextBool(4))
            {
                return "That " + Main.npc[partyGirl].GivenName + " is quite flamboyant! Be like her!";
            }
            switch (Main.rand.Next(3))
            {
                case 0:
                    return "I'm Jake the dog!";
                case 1:
                    {
                        Main.npcChatCornerItem = (ModContent.ItemType<ScarletSword>());
                        return $"Take that sword.";
                    }
                case 2:
                    return "Bacon pancakes, making bacon pancakes.";
                default:
                    return "I'm Jake the dog!";
            }
        }
    }
}
