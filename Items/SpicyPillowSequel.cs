using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    internal class SpicyPillowSequel
    {
        public static void Add()
        {
            if (StatusField.GetCustomFieldEffect("Segfault_ID") == null) { return; }
            FieldEffect_ApplyWithRandomDistribution_Effect Segfaulting = ScriptableObject.CreateInstance<FieldEffect_ApplyWithRandomDistribution_Effect>();
            Segfaulting.field = StatusField.GetCustomFieldEffect("Segfault_ID");
            Segfaulting.usePrevious = true;
            Segfaulting.previousIsRange = true;

            PerformEffect_Item pillow = new PerformEffect_Item("wolliPycipS_ID", null, false)
            {
                Item_ID = "wolliPycipS_SW",
                Name = "wol?iP y██pS",
                Flavour = "\".taolB yrettaB\"",
                Description = "On taking any sort of damage, randomly distribute 0-3 Segfault to all occupied party member positions, and 1-4 Segfault to all occupied enemy positions.",
                IsShopItem = true,
                ShopPrice = 3,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockNobodyAnnaMolly"),
                TriggerOn = TriggerCalls.OnDamaged,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 0),
                    Effects.GenerateEffect(Segfaulting, 3, Targeting.Unit_AllAllySlots),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 1),
                    Effects.GenerateEffect(Segfaulting, 4, Targeting.Unit_AllOpponentSlots),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_AnnaMolly_Forgotten_ACH";
            string unlockID = "AApocrypha_AnnaMolly_Forgotten_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(pillow.item, new ItemModdedUnlockInfo(pillow.Item_ID, ResourceLoader.LoadSprite("UnlockNobodyAnnaMollyLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, pillow.Item_ID);

            UnlockableModData UnlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [pillow.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Nobody_BOSS", ResourceLoader.LoadSprite("NobodyPearl", null, 32, null));
            unlockCheck.AddUnlockData("ThresholdFool_CH", UnlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("wol?iP y██pS", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementNobodyAnnaMolly", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("ForgottenTitleLabel", "The Forgotten");
        }
    }
}
