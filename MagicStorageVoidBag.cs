
using Terraria.ModLoader;
using MagicStorageVoidBag.ILPatches;
using MagicStorageVoidBag.Hooks;

namespace MagicStorageVoidBag {
    public class MagicStorageVoidBag : Mod {
        public static MagicStorageVoidBag Instance => ModContent.GetInstance<MagicStorageVoidBag>();

        // IL Patches
        private PlayerUpdatePatch playerUpdatePatch = new();

        public override void Load() {
            IL.Terraria.Player.Update += playerUpdatePatch.Patch;

            On.Terraria.Player.GetItem_VoidVault += GetItemVoidVaultHook.Hook;
            On.Terraria.Player.ItemSpaceForCofveve += ItemSpaceForCofveveHook.Hook;
            On.MagicStorage.Components.StorageHeart.RightClick += StorageHeartRightClickHook.Hook;
        }

        public override void Unload() {
            IL.Terraria.Player.Update -= playerUpdatePatch.Patch;

            On.Terraria.Player.GetItem_VoidVault -= GetItemVoidVaultHook.Hook;
            On.Terraria.Player.ItemSpaceForCofveve -= ItemSpaceForCofveveHook.Hook;
            On.MagicStorage.Components.StorageHeart.RightClick -= StorageHeartRightClickHook.Hook;

            base.Unload();
        }
    }
}