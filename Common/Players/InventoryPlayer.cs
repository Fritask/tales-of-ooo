
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
                new Item(ModContent.ItemType<Scarlet>()),
                //new Item(ItemID.GoldBroadsword, 1),
            };
        }
    }
}
