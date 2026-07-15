using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items.TradeItems
{
    public class WhitlockBarTincture //OBSOLETE
    {
        public static void Add()
        {
            FullHealthDetectionEffectorCondition Injured = ScriptableObject.CreateInstance<FullHealthDetectionEffectorCondition>();
            Injured.checkFullHealth = false;

            ExtraLootOptionsEffect HalfFull = ScriptableObject.CreateInstance<ExtraLootOptionsEffect>();
            HalfFull._itemName = "TinctureOfVigourHalf_ExtraW";

            PerformEffect_Item tincture = new PerformEffect_Item("WhitlockTinctureOfVigour_ID", null, false)
            {
                Item_ID = "TinctureOfVigour2_ExtraW",
                Name = "Tincture of Vigour",
                Flavour = "\"Cures pain, sets bones, curls hair, wards off spiders.\"",
                Description = "At the start of combat, if this party member is not at full health, heal them 10 health.\nContains two doses.",
                IsShopItem = false,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = false,
                Icon = ResourceLoader.LoadSprite("UnlockOsmanWhitlock"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Conditions = [Injured],
                ConsumeOnUse = true,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 10, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(HalfFull),
                ],
            };

            ItemUtils.JustAddItemSoItCanBeLoaded(tincture.item);
        }
    }
}
