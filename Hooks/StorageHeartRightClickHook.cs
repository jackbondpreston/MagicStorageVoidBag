using MagicStorage.Components;
using MagicStorage.Items;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;


namespace MagicStorageVoidBag.Hooks {
    internal class StorageHeartRightClickHook {
        private static readonly log4net.ILog Logger = MagicStorageVoidBag.Instance.Logger;
        public static bool Hook(On.MagicStorage.Components.StorageHeart.orig_RightClick orig, MagicStorage.Components.StorageHeart heart, int i, int j) {
            // https://github.com/blushiemagic/MagicStorage/blob/1.4-stable/Components/StorageHeart.cs#L23
            Player player = Main.LocalPlayer;
            Item item = player.HeldItem;

            if (!item.IsAir && item.ModItem is Locator locator) {
                if (Main.tile[i, j].TileFrameX % 36 == 18) i--;
                if (Main.tile[i, j].TileFrameY % 36 == 18) j--;

                locator.location = new Point16(i, j);
                if (player.selectedItem == 58) {
                    Main.mouseItem = item.Clone();
                }

                Main.NewText(Language.GetTextValue("Mods.MagicStorage.LocatorSet", i, j));
                return true;
            }

            return orig(heart, i, j);
        }
    }
}
