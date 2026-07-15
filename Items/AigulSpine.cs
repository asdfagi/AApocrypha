using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Items
{
    public class AigulSpine
    {
        public static void Add()
        {
            if (!LoadedDBsHandler._StatusFieldDB._StatusEffects.ContainsKey("Weakness_ID")) { return; }

            StatusEffect_Apply_Effect weakerer = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            weakerer._Status = StatusField.GetCustomStatusEffect("Weakness_ID");

            DamageIntModCasterStatusAndSecondaryEffect_Item aigul = new DamageIntModCasterStatusAndSecondaryEffect_Item("SpineOfRegret_ID", 2, true, false, true, "Weakness_ID")
            {
                Item_ID = "SpineOfRegret_TW",
                Name = "Spine of Regret",
                Flavour = "\"He would give up anything, except us.\"",
                Description = "Weakness increases damage dealt by this party member instead of decreasing it." +
                "\nAt the start of each turn and when getting a kill, apply 1 Weakness to this party member.",
                IsShopItem = false,
                ShopPrice = 9,
                StartsLocked = true,
                DoesPopUpInfo = true,
                Icon = ResourceLoader.LoadSprite("UnlockBossLornFluke"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                SecondaryDoesPopUpInfo = true,
                SecondaryConsumeOnUse = false,
                SecondaryTriggerOn = [TriggerCalls.OnTurnStart, TriggerCalls.OnKill],
                SecondaryConditions = [],
                SecondaryEffects = [
                    Effects.GenerateEffect(weakerer, 1, Targeting.Slot_SelfSlot),
                ],
                OnUnlockUsesTHE = true,
            };

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(aigul.item, new ItemModdedUnlockInfo(aigul.Item_ID, ResourceLoader.LoadSprite("UnlockBossLornFlukeLocked", null, 32, null), "LornFlukeBoss_ACH"));
        }
    }
}
