using MagicStorageVoidBag.Items;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace MagicStorageVoidBag.ILPatches {
    internal class PlayerUpdatePatch : ILPatch {
        private static readonly log4net.ILog Logger = MagicStorageVoidBag.Instance.Logger;
        public void Patch(ILContext il) {
            if (il == null) {
                Logger.Error("ILContext null!");
                return;
            }

            Logger.Debug("Patching Terraria.Player.Update IL...");

            var c = new ILCursor(il);
            var setterMethod = typeof(Player).GetProperty(nameof(Player.IsVoidVaultEnabled)).GetSetMethod();
            if (!c.TryGotoNext(i => i.MatchCallOrCallvirt(setterMethod))) {
                Logger.Warn("Failed to go to next call or callvirt! :(");
                return;
            }

            c.Emit(OpCodes.Ldarg_0);
            c.Emit(OpCodes.Ldc_I4, ModContent.ItemType<MSVoidBag>());
            var hasItemMethod = typeof(Player).GetMethod(nameof(Player.HasItem));
            c.Emit(OpCodes.Call, hasItemMethod);
            c.Emit(OpCodes.Or);

            Logger.Debug("...Complete!");
        }
    }
}
