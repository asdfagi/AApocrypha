using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class FourthCityAirag
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS wearablePassiveConfrontational = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            wearablePassiveConfrontational._extraPassiveAbility = Passives.GetCustomPassive("Confrontational_PA");

            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            FieldEffect_Apply_Effect ApplyShield = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyShield._Field = StatusField.Shield;

            PerformEffect_Item airag = new PerformEffect_Item("FourthCityAirag_ID", null, false)
            {
                Item_ID = "FourthCityAirag_SW",
                Name = "Fourth City Airag",
                Flavour = "\"For the Khan of Dreams.\"",
                Description = "This party member now has Confrontational as a passive. Gain 3 Shield on moving or being moved in front of an enemy.",
                IsShopItem = true,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockBlueSkyWhitlock"),
                TriggerOn = TriggerCalls.OnMoved,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ApplyShield, 3, Targeting.Slot_SelfSlot, PreviousTrue),
                ],
                EquippedModifiers = [wearablePassiveConfrontational],
            };

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(airag.item, new ItemModdedUnlockInfo("FourthCityAirag_SW", ResourceLoader.LoadSprite("UnlockBlueSkyWhitlockLocked", null, 32, null), "AApocrypha_Whitlock_Dreamer_ACH"));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Whitlock_Dreamer_ACH", "FourthCityAirag_SW");

            UnlockableModData whitlockDoulaUnlockData = new UnlockableModData("AApocrypha_Whitlock_Dreamer_Unlock")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Whitlock_Dreamer_ACH",
                hasItemUnlock = true,
                items = ["FourthCityAirag_SW"],
            };

            FinalBossCharUnlockCheck UnlockAbstractionWhitlock = Unlocks.GetOrCreateUnlock_CustomFinalBoss("BlueSky_BOSS", ResourceLoader.LoadSprite("BlueSkyPearl", null, 32, null));
            UnlockAbstractionWhitlock.AddUnlockData("Whitlock_CH", whitlockDoulaUnlockData);

            ModdedAchievements whitlockdoulaachievement = new ModdedAchievements("Fourth City Airag", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementBlueSkyWhitlock", null, 32, null), "AApocrypha_Whitlock_Dreamer_ACH");
            whitlockdoulaachievement.AddNewAchievementToCUSTOMCategory("BlueSky_BOSS", "The Dreamer");
        }
    }
}
