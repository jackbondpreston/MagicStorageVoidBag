using MagicStorage.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MagicStorageVoidBag.Items {
    internal class GlobalItem : Terraria.ModLoader.GlobalItem {
        public override bool OnPickup(Item item, Player player) {
            var i = player.inventory.Where(i => i.type == ModContent.ItemType<MSVoidBag>()).DefaultIfEmpty(null).First();
            if (i != null && !player.ItemSpace(item).CanTakeItem) {
                var bag = (MSVoidBag)i.ModItem;

                if (bag.location.X < 0 || bag.location.Y < 0) return false;

                Tile tile = Main.tile[bag.location.X, bag.location.Y];

                if (!tile.HasTile || tile.TileType != ModContent.TileType<StorageHeart>() || tile.TileFrameX != 0 || tile.TileFrameY != 0) return false;
                if (!TileEntity.ByPosition.TryGetValue(bag.location, out TileEntity te)) return false;
                if (te.type != ModContent.TileEntityType<TEStorageHeart>()) return false;

                TEStorageHeart heart = (TEStorageHeart)te;

                int oldStack = item.stack;
                heart.TryDeposit(item);
                if (item.stack == 0) return true;

            }

            return base.OnPickup(item, player);
        }
    }
}
