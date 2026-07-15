using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class FrozenIdol
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect frostbit = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            frostbit._Status = StatusField.GetCustomStatusEffect("Frostbite_ID");
            frostbit._JustOneRandomTarget = true;

            PercentageEffectorCondition Fivety = ScriptableObject.CreateInstance<PercentageEffectorCondition>();
            Fivety.triggerPercentage = 60;

            DoublePerformEffect_Item iceidol = new DoublePerformEffect_Item("FrozenIdol_ID", null, false)
            {
                Item_ID = "FrozenIdol_SW",
                Name = "Frozen Idol",
                Flavour = "\"The ice glimmers fiercely\"",
                Description = "At the start of each turn, apply 1 Frostbite to one of the enemies with the highest health." +
                "\nUpon any party member performing an ability, 60% chance to increase Frostbite intensity on all Frostbitten enemies by 1.",
                IsShopItem = true,
                ShopPrice = 8,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockOsmanHaborym"),
                TriggerOn = TriggerCalls.OnTurnStart,
                Effects =
                [
                    Effects.GenerateEffect(frostbit, 1, Targeting.GenerateUnitTarget_Specific_Health(false, false, false, false)),
                ],
                SecondaryTriggerOn = [TriggerCalls.OnAnyAbilityUsed],
                SecondaryConditions = [ScriptableObject.CreateInstance<IsPlayerTurnEffectorCondition>(), Fivety],
                SecondaryDoesPopUpInfo = true,
                SecondaryEffects = [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetFrostValueModifyEffect>(), 1, Targeting.Unit_AllOpponents),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Haborym_Witness_ACH";
            string unlockID = "AApocrypha_Haborym_Witness_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(iceidol.item, new ItemModdedUnlockInfo(iceidol.Item_ID, ResourceLoader.LoadSprite("UnlockOsmanHaborymLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, iceidol.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [iceidol.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_OsmanFinalBoss();
            unlockCheck.AddUnlockData("Haborym_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Frozen Idol", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementOsmanHaborym", null, 16, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.WitnessTitleLabel);
        }
    }
}
