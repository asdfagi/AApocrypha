using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomStatusField;
using BrutalAPI.Items;

namespace A_Apocrypha.Items.TradeItems
{
    public class SeekerBarMysteryMeat
    {
        public static void Add()
        {
            PerformEffectWearable originalmeat = LoadedAssetsHandler.GetWearable("MysteryRation_SW") as PerformEffectWearable;

            PerformEffect_Item mysterymeat = new PerformEffect_Item("SeekerMysteryMeat_ID", null, false)
            { // yes, this is just a copy of mystery ration - turns out there is no such thing as "the whitlock bar bug" but :sparkles:flavour:sparkles:
                Item_ID = "SeekerMysteryMeat_ExtraW",
                Name = "Mystery Meat",
                Flavour = "\"Every moving thing that liveth shall be meat for you.\"",
                Description = "When this party member dies, fully heal all other party members. This item is destroyed upon activation.",
                IsShopItem = false,
                ShopPrice = 4,
                DoesPopUpInfo = true,
                StartsLocked = false,
                Icon = originalmeat.wearableImage,
                TriggerOn = TriggerCalls.OnDeath,
                ConsumeOnUse = true,
                Effects = originalmeat.effects,
            };

            mysterymeat.item._ItemTypeIDs =
            [
                "FoodID",
                "Meat",
            ];

            ItemUtils.JustAddItemSoItCanBeLoaded(mysterymeat.item);
        }
    }
}
