using MagicStorage.Components;
using MagicStorageVoidBag.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using MagicStorage;
using Terraria.ID;

namespace MagicStorageVoidBag.Hooks {
    internal class ItemSpaceForCofveveHook {
        private static readonly log4net.ILog Logger = MagicStorageVoidBag.Instance.Logger;

        // Despite the constant negative press cofveve
        public static bool Hook(On.Terraria.Player.orig_ItemSpaceForCofveve orig, Player player, Item newItem) {
            var i = player.inventory.FirstOrDefault(i => i.type == ModContent.ItemType<MSVoidBag>(), null);
            if (i == null) goto original;

            newItem = newItem.Clone();

            var bag = (MSVoidBag)i.ModItem;

            if (bag.location.X < 0 || bag.location.Y < 0) goto original;

            Tile tile = Main.tile[bag.location.X, bag.location.Y];

            if (!tile.HasTile || tile.TileType != ModContent.TileType<StorageHeart>() || tile.TileFrameX != 0 || tile.TileFrameY != 0) goto original;
            if (!TileEntity.ByPosition.TryGetValue(bag.location, out TileEntity te)) goto original;
            if (te.type != ModContent.TileEntityType<TEStorageHeart>()) goto original;

            if (Utility.HeartHasSpaceFor(newItem, (TEStorageHeart)te)) return true;

            original:
            if (player.HasItem(ItemID.VoidLens)) {
                return orig(player, newItem);
            } else {
                return false;
            }
        }
    }
}
