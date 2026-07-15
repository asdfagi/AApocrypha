using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    internal class PermafrostPactsign
    {
        public static void Add()
        {
            FieldEffect_ApplyWithRandomDistribution_Effect Freeze = ScriptableObject.CreateInstance<FieldEffect_ApplyWithRandomDistribution_Effect>();
            Freeze.field = StatusField.GetCustomFieldEffect("Hoarfrost_ID");

            FieldEffectCheckEffect HasFreeze = ScriptableObject.CreateInstance<FieldEffectCheckEffect>();
            HasFreeze._fields = [Freeze.field];

            DoublePerformEffect_Item pactsign = new DoublePerformEffect_Item("PermafrostPactSign_ID", null, false)
            {
                Item_ID = "PermafrostPactSign_TW",
                Name = "Permafrost Pact-Sign",
                Flavour = "\"Grand Duke of the Frozen Wastes\"",
                Description = "At the start of each turn, apply 1 Hoarfrost to 2 random party member positions, then heal all party members standing in Hoarfrost 3 health.",
                IsShopItem = false,
                ShopPrice = 5,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockHeavenHaborym"),
                TriggerOn = TriggerCalls.OnTurnStart_Early,
                Effects =
                [
                    Effects.GenerateEffect(Freeze, 2, Targeting.GenerateGenericTarget([0, 1, 2, 3, 4], true)),
                ],
                SecondaryTriggerOn = [TriggerCalls.OnTurnStart],
                SecondaryDoesPopUpInfo = true,
                SecondaryEffects = [
                    Effects.GenerateEffect(HasFreeze, 1, Targeting.GenerateGenericTarget([0], true)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 3, Targeting.GenerateGenericTarget([0], true), Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(HasFreeze, 1, Targeting.GenerateGenericTarget([1], true)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 3, Targeting.GenerateGenericTarget([1], true), Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(HasFreeze, 1, Targeting.GenerateGenericTarget([2], true)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 3, Targeting.GenerateGenericTarget([2], true), Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(HasFreeze, 1, Targeting.GenerateGenericTarget([3], true)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 3, Targeting.GenerateGenericTarget([3], true), Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(HasFreeze, 1, Targeting.GenerateGenericTarget([4], true)),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), 3, Targeting.GenerateGenericTarget([4], true), Effects.CheckPreviousEffectCondition(true, 1)),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Haborym_Divine_ACH";
            string unlockID = "AApocrypha_Haborym_Divine_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(pactsign.item, new ItemModdedUnlockInfo(pactsign.Item_ID, ResourceLoader.LoadSprite("UnlockHeavenHaborymLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, pactsign.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [pactsign.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_HeavenFinalBoss();
            unlockCheck.AddUnlockData("Haborym_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Permafrost Pact-Sign", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementHeavenHaborym", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.DivineTitleLabel);
        }
    }
}
