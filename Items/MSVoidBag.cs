using MagicStorage.Items;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace MagicStorageVoidBag.Items {
    [ExtendsFromMod("MagicStorage")]
    public class MSVoidBag : PortableAccess {

        /* 
         Code here largely thanks to original MagicStorage source, which can be found at
         https://github.com/blushiemagic/MagicStorage/
         License:
            MIT License

            Copyright(c) 2017 Kaylee Minsuh Kim

            Permission is hereby granted, free of charge, to any person obtaining a copy
            of this software and associated documentation files(the "Software"), to deal
            in the Software without restriction, including without limitation the rights
            to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
            copies of the Software, and to permit persons to whom the Software is
            furnished to do so, subject to the following conditions:

            The above copyright notice and this permission notice shall be included in all
            copies or substantial portions of the Software.

            THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
            IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
            FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
            AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
            LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
            OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
            SOFTWARE.
        */

        public override void SetStaticDefaults() {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults() {
            Item.width = 26;
            Item.height = 34;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Purple;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 28;
            Item.useTime = 28;
            Item.value = Item.sellPrice(gold: 10);
        }

        public override void ModifyTooltips(List<TooltipLine> lines) {
            bool isSet = location.X >= 0 && location.Y >= 0;
            for (int k = 0; k < lines.Count; k++)
                if (isSet && lines[k].Mod == "Terraria" && lines[k].Name == "Tooltip1") {
                    lines[k].Text = Language.GetTextValue("Mods.MagicStorage.SetTo", location.X, location.Y);
                } else if (!isSet && lines[k].Mod == "Terraria" && lines[k].Name == "Tooltip2") {
                    lines.RemoveAt(k);
                    k--;
                }
        }

        public override void AddRecipes() {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<PortableAccess>();
            recipe.AddIngredient(ItemID.VoidLens);
            recipe.Register();
        }
    }
}
