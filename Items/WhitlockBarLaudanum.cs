using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class WhitlockBarLaudanum
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect poisonThatGuy = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            poisonThatGuy._Status = StatusField.GetCustomStatusEffect("Poisoned_ID");

            DamagePercentModAndSecondaryEffect_Item laudanum = new DamagePercentModAndSecondaryEffect_Item("WhitlockLaudanum_ID", 50, false, false, false)
            {
                Item_ID = "Laudanum_ExtraW",
                Name = "Superior Laudanum",
                Flavour = "\"It was full only yesterday!\"",
                Description = "This party member takes 50% less damage. This item is destroyed at the end of combat.",
                IsShopItem = false,
                ShopPrice = 3,
                DoesPopUpInfo = true,
                StartsLocked = false,
                Icon = ResourceLoader.LoadSprite("WhitlockBarItemLaudanum"),
                TriggerOn = TriggerCalls.OnBeingDamaged,
                SecondaryConsumeOnUse = true,
                SecondaryTriggerOn = [TriggerCalls.OnCombatEnd],
                SecondaryDoesPopUpInfo = true,
                SecondaryEffects = [],
            };

            ItemUtils.JustAddItemSoItCanBeLoaded(laudanum.item);
        }
    }
}
