using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class GlisteningAmber
    {
        public static void Add()
        {
            AddRandomPassiveByFoolCategoryEffect amberMeUpBuddy = ScriptableObject.CreateInstance<AddRandomPassiveByFoolCategoryEffect>();
            amberMeUpBuddy._basePassivePool = VaughanCharacter.utilityPassives.ToArray();
            amberMeUpBuddy._supportPassivePool = VaughanCharacter.defensePassives.ToArray();
            amberMeUpBuddy._DPSPassivePool = VaughanCharacter.offensePassives.ToArray();
            amberMeUpBuddy._popup = true;
            amberMeUpBuddy._fixedCap = 10;
            amberMeUpBuddy._miscIsAll = true;

            PerformEffect_Item amber = new PerformEffect_Item("GlisteningAmber_ID", null, false)
            {
                Item_ID = "GlisteningAmber_SW",
                Name = "Chunk of Glistening Amber",
                Flavour = "\"Ancestries trapped like flies in... well, you know.\"",
                Description = "Apply a random Alteration to this party member at the start of combat, suited to their needs.",
                IsShopItem = true,
                ShopPrice = 4,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockOsmanVaughan"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                    [
                        Effects.GenerateEffect(amberMeUpBuddy, 1, Targeting.Slot_SelfSlot),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Vaughan_Witness_ACH";
            string unlockID = "AApocrypha_Vaughan_Witness_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(amber.item, new ItemModdedUnlockInfo(amber.Item_ID, ResourceLoader.LoadSprite("UnlockOsmanVaughanLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, amber.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [amber.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_OsmanFinalBoss();
            unlockCheck.AddUnlockData("Vaughan_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Chunk of Glistening Amber", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementOsmanVaughan", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.WitnessTitleLabel);
        }
    }
}
