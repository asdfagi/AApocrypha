using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class BrokenFuse
    {
        public static void Add()
        {
            BrokenFuseDamageIncrease_Item robotKill = new BrokenFuseDamageIncrease_Item("BrokenFuse_ID")
            {
                Item_ID = "BrokenFuse_SW",
                Name = "Broken Fuse",
                Flavour = "\"It's a real circuit breaker!\"",
                Description = "This party member deals 1-3 more damage to enemies.\nThis party member instead deals 3-9 more damage if the target is a robot.",
                IsShopItem = true,
                ShopPrice = 5,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockBossAssessorBonus"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                NormalAddition = 3,
                NormalAddition2 = 1,
                RobotAddition = 9,
                RobotAddition2 = 3,
                AffectDamageDealtInsteadOfReceived = true,
                UseSimpleIntegerInsteadOfDamage = false,
                UseRangeFromTo = true,
            };

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(robotKill.item, new ItemModdedUnlockInfo("BrokenFuse_SW", ResourceLoader.LoadSprite("UnlockBossAssessorBonusLocked", null, 32, null), "AApocrypha_Comedy_AssessorRecycling_ACH"));
            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("AApocrypha_Comedy_AssessorRecycling_ACH", "BrokenFuse_SW");

            UnlockableModData assessorComedyUnlockData = new UnlockableModData("ComedyAssessorRecycling")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "AApocrypha_Comedy_AssessorRecycling_ACH",
                hasItemUnlock = true,
                items = ["BrokenFuse_SW"],
            };

            ModdedAchievements assessorcomedyrecyclingachievement = new ModdedAchievements("Reduce, Reuse, Recycle", "Watch the entire field undergo a Factory Reset during a confrontation with the Amalgamated Assessor.", ResourceLoader.LoadSprite("AchievementComedyAssessorRecycling", null, 32, null), "AApocrypha_Comedy_AssessorRecycling_ACH");
            assessorcomedyrecyclingachievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.ComediesTitleLabel);

            Unlocks.AddUnlock_ByID(assessorComedyUnlockData);
        }
    }
}
