using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;
using Utility.SerializableCollection;

namespace A_Apocrypha.Items
{
    public class AnomalousSymbol
    {
        public static void Add()
        {
            TargetSplitOrReplaceHealthEffect purplify = ScriptableObject.CreateInstance<TargetSplitOrReplaceHealthEffect>();
            purplify._color = Pigments.Purple;
            purplify._colorBlacklist = [Pigments.Grey];

            RandomizeCostsToColorsEffect purplecosts = ScriptableObject.CreateInstance<RandomizeCostsToColorsEffect>();
            purplecosts._mana = [Pigments.Purple];

            PerformEffect_Item anomalysymbol = new PerformEffect_Item("AnomalousSymbol_ID", null, false)
            {
                Item_ID = "AnomalousSymbol_TW",
                Name = "Anomalous Symbol",
                Flavour = "\"Absolute truth, embodied in chaos.\"",
                Description = "At the start of combat, split purple into the health colors of all party members and enemies. Grey health is instead replaced with purple. Change all of this party member's ability costs to purple.",
                IsShopItem = false,
                ShopPrice = 8,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockMinibossThresholdAlt"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(purplify, 1, Targeting.Unit_AllOpponentSlots),
                    Effects.GenerateEffect(purplify, 1, Targeting.Unit_AllAllySlots),
                    Effects.GenerateEffect(purplecosts, 1, Targeting.Slot_SelfSlot),
                ],
            };

            anomalysymbol.item._ItemTypeIDs =
            [];

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(anomalysymbol.item, new ItemModdedUnlockInfo("AnomalousSymbol_TW", ResourceLoader.LoadSprite("UnlockMinibossThresholdLocked", null, 32, null), "AApocrypha_Miniboss_Threshold_ACH"));
            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Miniboss_Threshold_ACH", "AnomalousSymbol_TW");

            UnlockableModData thresholdMinibossUnlockData = new UnlockableModData("MinibossThreshold")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Miniboss_Threshold_ACH",
                hasItemUnlock = true,
                items = ["AnomalousSymbol_TW"],
            };

            EnemyDeathUnlockCheck ThresholdDeathUnlock = ScriptableObject.CreateInstance<EnemyDeathUnlockCheck>();
            ThresholdDeathUnlock.usesSimpleDeathData = true;
            ThresholdDeathUnlock.enemyID = "Threshold_EN";
            ThresholdDeathUnlock.simpleDeathData = thresholdMinibossUnlockData;
            ThresholdDeathUnlock.specialDeathData = new SerializableDictionary<string, UnlockableModData>();

            ModdedAchievements thresholdminibossachievement = new ModdedAchievements("Turned Away at the Precipice", "Cross the Threshold.", ResourceLoader.LoadSprite("AchievementMinibossThreshold", null, 32, null), "AApocrypha_Miniboss_Threshold_ACH");
            thresholdminibossachievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.ComediesTitleLabel);

            Unlocks.AddUnlock_EnemyDeath(ThresholdDeathUnlock);
        }
    }
}
