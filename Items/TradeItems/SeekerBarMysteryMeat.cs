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
            PerformEffect_Item mysterymeat = new PerformEffect_Item("SeekerMysteryMeat_ID", null, false)
            { // yes, this is just a copy of mystery ration - circumventing that weird whitlock bar bug again
                Item_ID = "SeekerMysteryMeat_ExtraW",
                Name = "Mystery Meat",
                Flavour = "\"Every moving thing that liveth shall be meat for you.\"",
                Description = "When this party member dies, fully heal all other party members. This item is destroyed upon activation.",
                IsShopItem = false,
                ShopPrice = 4,
                DoesPopUpInfo = true,
                StartsLocked = false,
                Icon = LoadedAssetsHandler.GetWearable("MysteryRation_SW").wearableImage,
                TriggerOn = TriggerCalls.OnDeath,
                ConsumeOnUse = true,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<FullHealEffect>(), 1, Targeting.Unit_OtherAllies),
                ],
            };

            ItemUtils.JustAddItemSoItCanBeLoaded(mysterymeat.item);
        }
    }
}
