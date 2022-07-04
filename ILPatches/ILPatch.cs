using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicStorageVoidBag.ILPatches {
    internal interface ILPatch {
        public abstract void Patch(ILContext il);
    }
}
