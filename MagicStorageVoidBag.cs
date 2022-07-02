using Mono.Cecil.Cil;
using Mono.Cecil;
using MonoMod.Cil;
using Terraria.ModLoader;
using MagicStorageVoidBag.Items;

namespace MagicStorageVoidBag {
    public class MagicStorageVoidBag : Mod {
        public override void Load() {
            IL.MagicStorage.Components.StorageHeart.RightClick += HeartRightClickPatch;
        }

        public override void Unload() {
            base.Unload();
        }

        // Patch MagicStorage IL to change the Storage Heart right click handler to also work with
        //  MSVoidBag.
        private void HeartRightClickPatch(ILContext il) {
            if (il == null) {
                Logger.Error("ILContext null!");
                return;
            }

            Logger.Debug("Patching MagicStorage IL...");

            var c = new ILCursor(il);

            if (!c.TryGotoNext(i => i.MatchCallOrCallvirt("Terraria.ModLoader.ModContent", "ItemType"))) {
                Logger.Warn("IL patching failed! :(");
            }
            c = c.GotoPrev().GotoPrev();

            var c2 = c.Clone();

            // copy IL up to call
            while (c2.Next.OpCode != OpCodes.Call) {
                if (c2.Next.Operand != null) {
                    c.Emit(c2.Next.OpCode, c2.Next.Operand);
                } else {
                    c.Emit(c2.Next.OpCode);
                }

                c2 = c2.GotoNext();
            }

            var method = typeof(ModContent).GetMethod("ItemType").MakeGenericMethod(typeof(MSVoidBag));

            c.Emit(c2.Next.OpCode, method);
            c2.GotoNext();
            c.Emit(c2.Next.OpCode, c2.Next.Operand);

            Logger.Debug("...MagicStorage IL patching complete!");
        }

    }
}