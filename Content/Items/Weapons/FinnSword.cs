using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TalesOfOoo.Content.Items.Weapons
{
    public class FinnSword : ModItem
    {
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.TalesOfOoo.hjson file.
        private int cooldownDuration = 600; // Duração da recarga em frames (60 frames = 1 segundo)
        private int cooldownTimer = 0; // Contador de tempo de recarga

        public override void SetDefaults()
        {
            Item.damage = 150;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.crit = 6;
            Item.value = 10000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
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
            }

            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                cooldownTimer = cooldownDuration;
                int empressProjectile = Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, player.velocity, ProjectileID.EmpressBlade, Item.damage, Item.knockBack, Main.myPlayer);
                Main.projectile[empressProjectile].timeLeft = 300;
            }
            else
            {
                //Item.useStyle = ItemUseStyleID.Swing;
                Item.shoot = 0;
                //Item.noUseGraphic = false;
            }

            return true;
        }

        public override void HoldItem(Player player)
        {
            // Atualiza o contador de tempo de recarga
            if (cooldownTimer > 0)
            {
                cooldownTimer--;
            }
        }

        //public override void AddRecipes()
        //{
        //    Recipe recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.DirtBlock, 10);
        //    recipe.AddTile(TileID.WorkBenches);
        //    recipe.Register();
        //}
    }
}