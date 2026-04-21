using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class AnomalousScepter
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS wearablePassiveZelator = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            wearablePassiveZelator._extraPassiveAbility = Passives.GetCustomPassive("Zelator_PA");

            StatusEffect_Apply_Effect HexedApply = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            HexedApply._Status = StatusField.GetCustomStatusEffect("Hexed_ID");

            PerformEffect_Item anomalyscepter = new PerformEffect_Item("AnomalousScepter_ID", null, false)
            {
                Item_ID = "AnomalousScepter_TW",
                Name = "Anomalous Scepter",
                Flavour = "\"Channels hexes, crushes skulls.\"",
                Description = "This party member now has Zelator as a passive, increasing the damage they deal to Hexed units." +
                "\nApply 2 Hexed to the Opposing enemy at the start of each turn.",
                IsShopItem = false,
                ShopPrice = 8,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockBossAmdusias"),
                TriggerOn = TriggerCalls.OnTurnStart,
                Effects =
                [
                    Effects.GenerateEffect(HexedApply, 2, Targeting.Slot_Front),
                ],
                EquippedModifiers = [wearablePassiveZelator],
                OnUnlockUsesTHE = true,
            };

            anomalyscepter.item._ItemTypeIDs =
            [
                ItemType_GameIDs.Magic.ToString(),
            ];

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(anomalyscepter.item, new ItemModdedUnlockInfo("AnomalousScepter_TW", ResourceLoader.LoadSprite("UnlockBossAmdusiasLocked", null, 32, null), "AmdusiasBoss_ACH"));
        }
    }
}
