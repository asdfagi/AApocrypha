using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class DownloadFailure
    {
        public static void Add()
        {
            StatusEffect_ApplyPermanent_Effect poopleEffect = ScriptableObject.CreateInstance<StatusEffect_ApplyPermanent_Effect>();
            poopleEffect._Status = StatusField.OilSlicked;

            PerformEffect_Item downloadFailure = new PerformEffect_Item("DownloadFailure_ID", null, false)
            {
                Item_ID = "DownloadFailure_SW",
                Name = "Download Failure",
                Flavour = "\"Why are all my enemies sliding around?\"",
                Description = "On combat start, permanently apply Oil-Slicked to all enemies.",
                IsShopItem = true,
                ShopPrice = 3,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockMarchAnnaMolly"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(poopleEffect, 1, Targeting.Unit_AllOpponents),
                ],
            };

            string achievementID = "AApocrypha_AnnaMolly_Inevitable_ACH";
            string unlockID = "AApocrypha_AnnaMolly_Inevitable_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(downloadFailure.item, new ItemModdedUnlockInfo(downloadFailure.Item_ID, ResourceLoader.LoadSprite("UnlockMarchAnnaMollyLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, downloadFailure.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [downloadFailure.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("March_BOSS", ResourceLoader.LoadSprite("MarchPearl", null, 32, null));
            unlockCheck.AddUnlockData("ThresholdFool_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Download Failure", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementMarchAnnaMolly", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("InevitableTitleLabel", "The Inevitable");
        }
    }
}
