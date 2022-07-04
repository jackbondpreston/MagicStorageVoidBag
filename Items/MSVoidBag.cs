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
