using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class StArthurCandle
    {
        public static void Add()
        {
            DoublePerformEffect_Item starthur = new DoublePerformEffect_Item("StArthurCandle_ID", null, false)
            {
                Item_ID = "StArthurCandle_TW",
                Name = "St Arthur's Candle",
                Flavour = "\"It reeks of betrayal.\"",
                Description = "When this party member deals damage to an ally, heal this party member for half of the damage dealt, rounded down.",
                IsShopItem = false,
                ShopPrice = 7, //seven is the number
                DoesPopUpInfo = false,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("StArthurCandle"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                Conditions = [ScriptableObject.CreateInstance<StArthurCandleHitEffectCondition>()],
                Effects = [],
                SecondaryDoesPopUpInfo = false,
                SecondaryTriggerOn = [],
                SecondaryEffects =
                [],
                OnUnlockUsesTHE = false,
            };

            string achievementID = "AApocrypha_Tragedy_StArthur_ACH";
            string unlockID = "AApocrypha_Tragedy_StArthur_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(starthur.item, new ItemModdedUnlockInfo(starthur.Item_ID, ResourceLoader.LoadSprite("CandleItemLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, starthur.Item_ID);
            
            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [starthur.Item_ID],
            };

            ModdedAchievements unlockAchievement = new ModdedAchievements("Dreams of Dark Water", "Make a most unwise decision.", ResourceLoader.LoadSprite("AchievementTragedyStArthur", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.TragediesTitleLabel);

            Unlocks.AddUnlock_ByID(unlockData);
        }
    }
}
