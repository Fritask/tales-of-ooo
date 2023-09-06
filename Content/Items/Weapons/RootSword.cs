using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TalesOfOoo.Content.Items.Weapons
{
    public class RootSword : ModItem
    {
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.TalesOfOoo.hjson file.

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6.5f;
            Item.crit = 4;
            Item.value = 11000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
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