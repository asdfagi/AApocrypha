using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class StarvedBanner
    {
        public static void Add()
        {
            PerformEffect_Item banner = new PerformEffect_Item("StarvedBanner_ID", null, false)
            {
                Item_ID = "StarvedBanner_TW",
                Name = "Starved Banner",
                Flavour = "\"SO ARE WEE SHAPEDE\"",
                Description = "Whenever this party member directly heals an ally, apply a random Alteration to the healed unit.",
                IsShopItem = false,
                ShopPrice = 8,
                DoesPopUpInfo = false,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockHeavenVaughan"),
                TriggerOn = TriggerCalls.OnWillApplyHeal,
                Conditions = [ScriptableObject.CreateInstance<StarvedBannerHealEffectCondition>()],
                Effects = [],
                OnUnlockUsesTHE = true,
            };

            banner.item._ItemTypeIDs =
            [
                ItemType_GameIDs.Fabric.ToString(),
            ];

            string achievementID = "AApocrypha_Vaughan_Divine_ACH";
            string unlockID = "AApocrypha_Vaughan_Divine_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(banner.item, new ItemModdedUnlockInfo(banner.Item_ID, ResourceLoader.LoadSprite("UnlockHeavenVaughanLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, banner.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [banner.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_HeavenFinalBoss();
            unlockCheck.AddUnlockData("Vaughan_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Starved Banner", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementHeavenVaughan", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.DivineTitleLabel);
        }
    }
}
