
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using TalesOfOoo.Content.Items.Weapons;

namespace TutorialMod.Common.Players
{
    public class InventoryPlayer : ModPlayer
    {
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            return new[] {
                new Item(ModContent.ItemType<ScarletSword>()),
                //new Item(ItemID.GoldBroadsword, 1),
            };
        }

        //public override void PreUpdate()
        //{
        //    if ()
        //}
    }
}
