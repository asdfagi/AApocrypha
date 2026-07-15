using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;

namespace A_Apocrypha.Items
{
    public class SunBox
    {
        public static void Add()
        {
            StatusEffect_ApplyByPrevious_Effect Smouldervious = ScriptableObject.CreateInstance<StatusEffect_ApplyByPrevious_Effect>();
            Smouldervious._Status = StatusField.GetCustomStatusEffect("Smouldering_ID");

            PerformEffect_Item sunbox = new PerformEffect_Item("MirrorcatchBoxSunlight_ID", null, false)
            {
                Item_ID = "MirrorcatchBoxSunlight_SW",
                Name = "Sun-Stamped Mirrorcatch Box",
                Flavour = "\"Yearning, Burning\"",
                Description = "At the start of combat, apply 1-5 Smouldering to each enemy.",
                IsShopItem = true,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("UnlockOsmanAmbrose"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ExtraVariableForNextEffect>(), 1),
                    Effects.GenerateEffect(Smouldervious, 5, Targeting.GenerateSlotTarget([-1, 0, 4])),
                ],
                OnUnlockUsesTHE = true,
            };

            string achievementID = "AApocrypha_Ambrose_Witness_ACH";
            string unlockID = "AApocrypha_Ambrose_Witness_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(sunbox.item, new ItemModdedUnlockInfo(sunbox.Item_ID, ResourceLoader.LoadSprite("UnlockOsmanAmbroseLocked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, sunbox.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [sunbox.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_OsmanFinalBoss();
            unlockCheck.AddUnlockData("Ambrose_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Sun-Stamped Mirrorcatch Box", "Unlocked a new item.", ResourceLoader.LoadSprite("AchievementOsmanAmbrose", null, 16, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.WitnessTitleLabel);
        }
    }
}
