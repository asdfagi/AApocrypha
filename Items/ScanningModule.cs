using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    internal class ScanningModule
    {
        public static void Add()
        {
            OpponentByAnalysisStoredValueTargeting AnalysisTarget = ScriptableObject.CreateInstance<OpponentByAnalysisStoredValueTargeting>();
            AnalysisTarget.targetUnitAllySlots = false;
            AnalysisTarget.getAllUnitSelfSlots = false;
            AnalysisTarget._storedValueID = "NaudizCurrentStoredValue";

            ExtraPassiveAbility_Wearable_SMS wearablePassiveAnalyzer = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            wearablePassiveAnalyzer._extraPassiveAbility = LoadedAssetsHandler.GetCharacter("Naudiz4_CH").passiveAbilities[0];

            StatusEffect_Apply_Effect AddScars = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            AddScars._Status = StatusField.Scars;

            PerformEffect_Item scanner = new PerformEffect_Item("PhasicScanningModule_ID", null, false)
            {
                Item_ID = "PhasicScanningModule_SW",
                Name = "Phasic Scanning Module",
                Flavour = "\"R&D's second-finest!\"",
                Description = "This party member now has Analyzer as a passive, marking a random enemy for analysis at the start of each turn if none are marked. At the end of each turn, apply 1 Scar to this party member's marked enemy.",
                IsShopItem = true,
                ShopPrice = 5,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockOsmanNaudiz4"),
                EquippedModifiers = [wearablePassiveAnalyzer],
                TriggerOn = TriggerCalls.OnTurnFinished,
                Effects =
                [
                    Effects.GenerateEffect(AddScars, 1, AnalysisTarget),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Naudiz4_Witness_ACH";
            string unlockID = "AApocrypha_Naudiz4_Witness_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(scanner.item, new ItemModdedUnlockInfo(scanner.Item_ID, ResourceLoader.LoadSprite("UnlockOsmanNaudiz4Locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, scanner.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [scanner.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_OsmanFinalBoss();
            unlockCheck.AddUnlockData("Naudiz4_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Phasic Scanning Module", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementOsmanNaudiz4", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.WitnessTitleLabel);
        }
    }
}
