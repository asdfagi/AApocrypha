using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class WhitlockBarPoison
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect poisonThatGuy = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            poisonThatGuy._Status = StatusField.GetCustomStatusEffect("Poisoned_ID");

            PerformEffect_Item poison = new PerformEffect_Item("WhitlockAbominableSalts_ID", null, false)
            {
                Item_ID = "AbominableSalts_ExtraW",
                Name = "Flask of Abominable Salts",
                Flavour = "\"WARNING: consumption may cause vomiting, paralysis and ennui.\"",
                Description = "At the start of combat, apply 20 Poisoned to the Opposing enemy and destroy this item.",
                IsShopItem = false,
                ShopPrice = 3,
                DoesPopUpInfo = true,
                StartsLocked = false,
                Icon = ResourceLoader.LoadSprite("WhitlockBarItemAbominableSalts"),
                TriggerOn = TriggerCalls.OnCombatStart,
                ConsumeOnUse = true,
                Effects =
                [
                    Effects.GenerateEffect(poisonThatGuy, 20, Targeting.Slot_Front),
                ],
            };

            ItemUtils.JustAddItemSoItCanBeLoaded(poison.item);
        }
    }
}
