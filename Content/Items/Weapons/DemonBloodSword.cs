using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace TalesOfOoo.Content.Items.Weapons
{
    internal class DemonBloodSword : ModItem
    {
        private bool isRightButtonHeld; // Controla se o jogador está segurando o botão direito
        private int cooldownDuration = 10; // Duração da recarga em frames (60 frames = 1 segundo)
        private int cooldownTimer = 0; // Contador de tempo de recarga

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Melee;
            Item.width = 21;
            Item.height = 21;
            Item.scale = 0.9f;
            Item.useTime = 27;
            Item.useAnimation = 27;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = 21000;
            Item.rare = 4;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void UpdateInventory(Player player)
        {
            // Verifique se o botão do mouse está pressionado
            if (Main.mouseRight)
            {
                isRightButtonHeld = true;
            }
            else
            {
                isRightButtonHeld = false;
            }
        }

        public override void HoldItem(Player player)
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer--;
            }

            if (isRightButtonHeld)
            {
                Item.holdStyle = ItemHoldStyleID.HoldUp;

                int target = TargetClosest(player);

                if (target != -1)
                {
                    if (cooldownTimer <= 0)
                    {
                        cooldownTimer = cooldownDuration;
                        // Drena vida do alvo
                        player.lifeSteal += 0.1f; // Aumenta a taxa de roubo de vida temporariamente
                        player.HealEffect(10); // Cura o jogador
                        player.statLife += 10; // Aumenta a vida do jogador
                        NetMessage.SendData(MessageID.SpiritHeal, -1, -1, null, player.whoAmI, 10f); // Exibe o efeito de cura para outros jogadores
                        //Main.PlaySound(SoundID.Item8, player.position); // Toca um som de cura

                        // Drene vida do inimigo
                        NPC npc = Main.npc[target];
                        npc.StrikeNPC(npc.CalculateHitInfo(20, player.direction, false, Item.knockBack));

                        // Atualize a vida do jogador
                        player.UpdateLifeRegen();
                        player.lifeRegenTime = 0;
                        player.lifeRegen = 0;

                        // Atualize a vida do inimigo
                        npc.netUpdate = true;

                        for (int i = 0; i < 10; i++) // 10 é o número de partículas a serem criadas
                        {
                            Vector2 npcPosition = npc.Center;
                            Vector2 velocity = player.Center - npcPosition;

                            velocity *= Main.rand.NextFloat(2f, 4f); // Velocidade aleatória

                            int dust = Dust.NewDust(npcPosition, 1, 1, DustID.Blood, 0, 0, 0, default, 1f);
                            Main.dust[dust].velocity = (velocity/50);
                        }
                    }
                }
            }
            else
            {
                Item.holdStyle = ItemHoldStyleID.None;
            }

        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (player.altFunctionUse != 2)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Blood, player.velocity.X * 0.2f + (player.direction * 3), player.velocity.Y * 0.2f, 100, default(Color), 2.5f);
                Main.dust[dust].noGravity = true;
            }
        }

        // Função auxiliar para encontrar o inimigo mais próximo **TO-DO: Implementar regra de estar com o mouse em cima
        private int TargetClosest(Player player)
        {
            float maxDistance = 375f; // Distância máxima para procurar inimigos
            int target = -1;

            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.active && !npc.friendly && npc.lifeMax > 5 && !npc.dontTakeDamage)
                {
                    float distance = Vector2.Distance(player.Center, npc.Center);

                    if (distance < maxDistance)
                    {
                        maxDistance = distance;
                        target = i;
                    }
                }
            }

            return target;
        }
    }
}
