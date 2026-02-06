using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    internal class FiiF
    {
        public static void Add()
        {
            PercentageEffectCondition OneInThree = ScriptableObject.CreateInstance<PercentageEffectCondition>();
            OneInThree.percentage = 33;

            DoublePerformEffect_Item fiif = new DoublePerformEffect_Item("FiiF_ID", null, false)
            {
                Item_ID = "FiiF_FishW",
                Name = "FiiF",
                Flavour = "\"You caught a... FiiF ...a thguac uoY\"",
                Description = "At the start of each turn, copy an ability from a random enemy in combat onto this party member. This item is destroyed at the end of combat.",
                IsShopItem = false,
                ShopPrice = -4,
                DoesPopUpInfo = true,
                SecondaryDoesPopUpInfo = false,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockOsmanAnnaMolly"),
                TriggerOn = TriggerCalls.OnTurnStart,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<FiiFItemEffect>(), 1, Targeting.Unit_AllOpponents),
                ],
                SecondaryTriggerOn = [TriggerCalls.OnCombatEnd],
                SecondaryEffects = [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ConsumeItemEffect>(), 1, Targeting.Slot_SelfSlot),
                ],
                UsesSpecialUnlockText = true,
                SpecialUnlockID = UILocID.ItemFishLocationLabel,
                OnUnlockUsesTHE = true,
            };

            ItemUtils.AddItemToCustomStatsCategoryAndGamePool(fiif.item, "Fish", "Fish", new ItemModdedUnlockInfo("FiiF_FishW", ResourceLoader.LoadSprite("UnlockOsmanAnnaMollyLocked", null, 32, null), "AApocrypha_AnnaMolly_Witness_ACH"));
            ItemUtils.AddItemFishingRodPool(fiif.item, 2, fiif.item.startsLocked);
            ItemUtils.AddItemCanOfWormsPool(fiif.item, 2, fiif.item.startsLocked);

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_AnnaMolly_Witness_ACH", "FiiF_FishW");

            UnlockableModData UnlockData = new UnlockableModData("AApocrypha_AnnaMolly_Witness_Unlock")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_AnnaMolly_Witness_ACH",
                hasItemUnlock = true,
                items = ["FiiF_FishW"],
            };

            FinalBossCharUnlockCheck UnlockCheck = Unlocks.GetUnlock_OsmanFinalBoss();
            UnlockCheck.AddUnlockData("ThresholdFool_CH", UnlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("FiiF", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementOsmanAnnaMolly", null, 32, null), "AApocrypha_AnnaMolly_Witness_ACH");
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.WitnessTitleLabel);
        }
    }
}
