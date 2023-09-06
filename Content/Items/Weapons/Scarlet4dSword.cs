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
    internal class Scarlet4dSword : ModItem
    {
        private int cooldownDuration = 180; // Duração da recarga em frames (60 frames = 1 segundo)
        private int cooldownTimer = 0; // Contador de tempo de recarga

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = 1;
            Item.knockBack = 5;
            Item.value = 16000;
            Item.rare = 4;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.shoot = 540;
            Item.shootSpeed = 5;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (cooldownTimer > 0)
                {
                    return false;
                }
                else
                {
                    cooldownTimer = cooldownDuration;
                    Item.shoot = ProjectileID.PrincessWeapon;
                    Item.shootSpeed = 5;
                    Item.autoReuse = true;
                    Item.useTurn = true;
                }

            }
            else
            {
                Item.shoot = ProjectileID.FlaironBubble;
                Item.shootSpeed = 12;
            }

            return base.CanUseItem(player);
        }

        public override void HoldItem(Player player)
        {
            // Atualiza o contador de tempo de recarga
            if (cooldownTimer > 0)
            {
                cooldownTimer--;
            }
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (player.altFunctionUse != 2)
            {
                // Asphalt, SpectreStaff, Wraith
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.SpectreStaff, player.velocity.X * 0.2f + (player.direction * 3), player.velocity.Y * 0.2f, 100, default(Color), 2.5f);
                Main.dust[dust].noGravity = true;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(12)); //12 is the spread in degrees, although like with Set Spread it's technically a 24 degree spread due to the fact that it's randomly between 12 degrees above and 12 degrees below your cursor.
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI); //create the projectile
                }

                return true;
            }

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}
