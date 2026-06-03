using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class AdulterineMasonry
    {
        public static void Add()
        {
            FieldEffect_Apply_Effect ShieldApply = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ShieldApply._Field = StatusField.Shield;

            SpecificOpponentSlotsByOccupancyTargeting OpposingEmpty = ScriptableObject.CreateInstance<SpecificOpponentSlotsByOccupancyTargeting>();
            OpposingEmpty._inverted = true;
            OpposingEmpty.getAllUnitSelfSlots = true;
            OpposingEmpty.targetUnitAllySlots = true;

            PerformEffect_Item nobrick = new PerformEffect_Item("AdulterineMasonry_ID", null, false)
            {
                Item_ID = "AdulterineMasonry_TW",
                Name = "Adulterine Masonry",
                Flavour = "\"A brick from no castle.\"",
                Description = "At the start of each turn, apply 6 Shield to all party member positions not Opposing an enemy.",
                IsShopItem = false,
                ShopPrice = -10,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockBlueSkyKneynsberg"),
                TriggerOn = TriggerCalls.OnTurnStart,
                Effects =
                [
                    Effects.GenerateEffect(ShieldApply, 6, OpposingEmpty),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Kneynsberg_Dreamer_ACH";
            string unlockID = "AApocrypha_Kneynsberg_Dreamer_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(nobrick.item, new ItemModdedUnlockInfo(nobrick.Item_ID, ResourceLoader.LoadSprite("UnlockBlueSkyKneynsbergLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, nobrick.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [nobrick.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("BlueSky_BOSS", ResourceLoader.LoadSprite("BlueSkyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Kneynsberg_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Adulterine Masonry", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementBlueSkyKneynsberg", null, 32, null), achievementID);
            unlockAchievement.IsSecret = true;
            unlockAchievement.AddNewAchievementToCUSTOMCategory("BlueSky_BOSS", "The Dreamer");
        }
    }
}
