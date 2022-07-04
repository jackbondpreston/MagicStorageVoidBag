using MagicStorage;
using MagicStorage.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace MagicStorageVoidBag {
    internal static class Utility {
        internal static bool HeartHasSpaceFor(Item newItem, TEStorageHeart heart) {
            foreach (TEAbstractStorageUnit storageUnit in heart.GetStorageUnits()) {
                if (!storageUnit.Inactive) {
                    var unitItems = storageUnit.GetItems();

                    if (!storageUnit.IsFull) return true;
                    if (storageUnit.HasSpaceInStackFor(newItem)) return true;
                }
            }

            return false;
        }
    }
}
